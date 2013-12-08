
using System;
using UnityEngine;

namespace UnityTools
{
    static public class RigidbodyExtensions 
    {
        static public void CapSpeed (this Rigidbody rb, float speed)
        {
            if (rb.velocity.sqrMagnitude > speed*speed) {
                rb.AddForce (-rb.velocity.normalized * (rb.velocity.magnitude - speed), ForceMode.VelocityChange);
            }
        }
    }
}