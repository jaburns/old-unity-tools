
using System;
using UnityEngine;

namespace UnityTools
{
    [RequireComponent(typeof(Rigidbody))]
    public class SimpleSpring : MonoBehaviour
    {
        public Rigidbody Target;
        public float SpringConst;

        float _targetDist;

        public void Configure (Rigidbody target, float springConst) 
        {
            Target = target;
            SpringConst = springConst;
            _targetDist = (target.transform.position - transform.position).magnitude;
        }

        void FixedUpdate ()
        {
            var delta = Target.transform.position - transform.position; 
            var force = delta.normalized * SpringConst * (delta.magnitude - _targetDist);

            rigidbody.AddForce ( force, ForceMode.Force);
            Target   .AddForce (-force, ForceMode.Force);
        }
    }
}