
using System;
using UnityEngine;

namespace UnityTools
{
    public class LinearTransform : MonoBehaviour
    {
        public Vector3 DeltaPosition;
        public Vector3 DeltaRotation;
        public Vector3 DeltaScale;

        void Update ()
        {
            transform.localRotation = Quaternion.Euler (transform.localEulerAngles + DeltaRotation * Time.deltaTime);
            transform.localPosition += DeltaPosition * Time.deltaTime;
            transform.localScale    += DeltaScale * Time.deltaTime;
        }
    }
}