
using System;
using UnityEngine;

namespace UnityTools
{
    public static class RigidbodyExtensions 
    {
        public static void CapSpeed (this Rigidbody rb, float speed)
        {
            if (rb.velocity.sqrMagnitude > speed*speed) {
                rb.AddForce (-rb.velocity.normalized * (rb.velocity.magnitude - speed), ForceMode.VelocityChange);
            }
        }
    }
}