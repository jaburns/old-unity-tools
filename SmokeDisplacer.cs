using UnityEngine;

public class SmokeDisplacer
{
    const int ITERATIONS  = 15;

    readonly float _innerRadius;
    readonly float _restDist;
    readonly Vector2[] _points;

    public SmokeDisplacer(Vector2 initPos, float innerRadius, float outerRadius, int vertices)
    {
        _innerRadius = innerRadius;

        float theta = 0f;
        _points = new Vector2[vertices];
        for (int i = 0; i < _points.Length; ++i) {
            theta += 2*Mathf.PI / vertices;
            _points[i] = initPos + new Vector2 {
                x = outerRadius * Mathf.Cos(theta),
                y = outerRadius * Mathf.Sin(theta)
            };
        }

        _restDist = (_points[0] - _points[1]).magnitude;
    }

    public Vector2[] Simulate(Vector2 pos)
    {
        for (int loops = 0; loops < ITERATIONS; ++loops) {
            for (int i = 0; i < _points.Length; ++i) {
                var p0 = i == 0 ? _points[_points.Length-1] : _points[i-1];
                var p1 = _points[i];

                var pdist = (p0 - p1).magnitude;
                var mdist = (p1 - pos).magnitude;

                Vector2 dPos;

                if (mdist < _innerRadius) {
                    dPos = p1 - pos;
                    dPos *= _innerRadius / mdist;
                    p1 = pos + dPos;
                }

                dPos = p1 - p0;
                dPos *= _restDist / pdist;
                var fix = p0 + dPos - p1;
                p1 += fix / 2;
                p0 -= fix / 2;

                _points[i] = p1.AsVector3(0);
                if (i == 0) {
                    _points[_points.Length-1] = p0.AsVector3(0);
                } else {
                    _points[i-1] = p0.AsVector3(0);
                }
            }
        }
        return _points.Clone() as Vector2[];
    }
}
