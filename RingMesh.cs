using System.Collections.Generic;
using UnityEngine;

public class RingMesh : MonoBehaviour
{
    public int VertexCount = 30;
    public Material MeshMaterial;

    MeshFilter _mesh;
    EdgeCollider2D _collider;
    Vector2[] _points;
    Vector2[] _lastPoints;
    Vector3 _center;
    bool _hasInit;

    void Awake()
    {
        _hasInit = false;
    }

    void Update()
    {
        if (!_hasInit) return;
        var verts = _mesh.sharedMesh.vertices;
        for (int i = 0; i < _points.Length; ++i) {
            var vertPos = Vector3.Lerp(_lastPoints[i], _points[i], (Time.time - Time.fixedTime) / Time.fixedDeltaTime);
            verts[2*i] = vertPos - transform.position;
            verts[2*i+1] = _center + 10*(vertPos - _center) - transform.position;
        }
        _mesh.sharedMesh.vertices = verts;
    }

    void initRing(Vector2[] points)
    {
        if (_hasInit) return;

        _points = new Vector2[points.Length];
        _lastPoints = new Vector2[points.Length];
        points.CopyTo(_points, 0);
        points.CopyTo(_lastPoints, 0);

        generateMesh(points.Length * 2);
        _collider = gameObject.AddComponent<EdgeCollider2D>();
        _collider.isTrigger = true;
        _collider.points = points.Clone() as Vector2[];

        _hasInit = true;
    }

    public void UpdateRing(Vector2[] points)
    {
        if (!_hasInit) {
            initRing(points);
            return;
        }

        _points.CopyTo(_lastPoints, 0);
        points.CopyTo(_points, 0);

        var colPoints = new Vector2[points.Length + 1];
        for (int i = 0; i < colPoints.Length; ++i)  {
            colPoints[i] = points[i % points.Length] - transform.position.AsVector2();
        }
        _collider.points = colPoints;

        _center = Vector3.zero;
        for (int i = 0; i < _points.Length; ++i)  {
            _center += _points[i].AsVector3();
        }
        _center /= _points.Length;
    }

    void OnTriggerEnter2D()
    {
        if (!TimeGod.Instance.Rifting && TimeGod.Instance.Reversing) {
            HeroController.Kill();
        }
    }

    int[] getTriangleIndices(int numVertices)
    {
        var ts = new List<int>();

        for (int i = 0; i < numVertices; ++i) {
            ts.Add(i);
            ts.Add((i+1) % numVertices);
            ts.Add((i+2) % numVertices);
        }

        return ts.ToArray();
    }

    void generateMesh(int numVertices)
    {
        var mesh = new Mesh();
        mesh.vertices = new Vector3[numVertices];
        mesh.triangles = getTriangleIndices(numVertices);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        _mesh = gameObject.AddComponent<MeshFilter>();
        _mesh.sharedMesh = mesh;

        gameObject.AddComponent<MeshRenderer>().sharedMaterial = MeshMaterial;
    }
}
