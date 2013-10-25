
using System;
using UnityEngine;

namespace UnityTools
{
    public class Gravitate : MonoBehaviour
    {
        public string TargetName;
        public float ForceValue;
        public ForceMode UseForceMode;
        public float TopSpeed;

        GameObject _targetObject;

        void Awake () {
            Go ();
        }

        public void Go () {
            var go = GameObject.Find (TargetName);
            if (go == null) return;
            _targetObject = go;
        }

        void FixedUpdate ()
        {
            if (_targetObject == null) return;

            var delta = _targetObject.transform.position - transform.position;
            var unit = delta.normalized;
            var d2 = delta.sqrMagnitude;

            if (d2 < 50) d2 = 50;

            rigidbody.AddForce (ForceValue * unit / d2, UseForceMode);
            rigidbody.CapSpeed (TopSpeed);
        }
    }
}