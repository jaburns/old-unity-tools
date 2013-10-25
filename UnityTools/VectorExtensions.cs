
using System;
using UnityEngine;

namespace UnityTools
{
    public static class VectorExtensions
    {
        static public Vector3 WithX (this Vector3 vec, float x) { return new Vector3 (x, vec.y, vec.z); }
        static public Vector3 WithY (this Vector3 vec, float y) { return new Vector3 (vec.x, y, vec.z); }
        static public Vector3 WithZ (this Vector3 vec, float z) { return new Vector3 (vec.x, vec.y, z); }

        static public Vector3 WithXY (this Vector3 vec, float x, float y) { return new Vector3 (x, y, vec.z); }
        static public Vector3 WithYZ (this Vector3 vec, float y, float z) { return new Vector3 (vec.x, y, z); }
        static public Vector3 WithXZ (this Vector3 vec, float x, float z) { return new Vector3 (x, vec.y, z); }

        static readonly float COS45 = (float)Mathf.Cos (Mathf.PI / 4);
        static readonly float SIN45 = (float)Mathf.Sin (Mathf.PI / 4);

        static public Vector2 Rotate90 (this Vector2 vec) {
            return new Vector2 (-vec.y, vec.x);
        }

        static public Vector2 Rotate45 (this Vector2 vec) {
            return new Vector2 (vec.x * COS45 - vec.y * SIN45 , vec.x * SIN45 + vec.y * COS45);
        }

        static public float Dot (this Vector2 a, Vector2 b) {
            return a.x*b.x + a.y*b.y;
        }

        static public Vector2 Reflect (this Vector2 vec, Vector2 unitNormal) { return vec.Reflect (unitNormal, 1, 1); }
        static public Vector2 Reflect (this Vector2 vec, Vector2 unitNormal, float normalScale, float tangentScale) 
        {
            Vector2 unitTangent = unitNormal.Rotate90 ();
           
            float normComponent = vec.Dot (unitNormal)  * -normalScale;
            float tangComponent = vec.Dot (unitTangent) *  tangentScale;

            return unitNormal * normComponent + unitTangent * tangComponent;
        }
    }
}