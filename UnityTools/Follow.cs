
using System;
using UnityEngine;

namespace UnityTools
{
    /// <summary>
    /// Causes the game object this component is attached to to follow the provided target
    /// along the specified axes.  If a rigidbody component is present then MovePosition is
    /// used, otherwise the transform is modified directly.
    /// </summary>
    public class Follow : MonoBehaviour
    {
        public Transform Target;
        public bool XAxis, YAxis, ZAxis;
        
        Vector3 computeTarget () {
            return new Vector3 (
                XAxis ? Target.position.x : transform.position.x,
                YAxis ? Target.position.y : transform.position.y,
                ZAxis ? Target.position.z : transform.position.z
            );
        }

        void FixedUpdate ()
        {
            if (rigidbody != null) {
                rigidbody.MovePosition (computeTarget ());
            }
        }

        void Update ()
        {
            if (rigidbody == null) {
                transform.position = computeTarget ();
            }
        }
    }
}