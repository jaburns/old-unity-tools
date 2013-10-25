
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools
{
    public static class TransformExtensions 
    {
        /// <summary>
        /// Provides a fully recursive alternative to the standard Transform.Find function.
        /// </summary>
        static public Transform FindRecursive (this Transform transform, string name)
        {
            if (transform.name == name) return transform;

            var found = transform.Find (name);
            if (found) return found;

            foreach (Transform t in transform) {
                var subFound = t.FindRecursive (name);
                if (subFound != null) return subFound;
            }

            return null;
        }

        /// <summary>
        /// Resets the local position, rotation, and scale of a transform to identity.
        /// </summary>
        static public void Reset (this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale    = Vector3.one;
        }

        /// <summary>
        /// Recurses down the transform and returns a list of all child transforms whose name contains
        /// the provided string.
        /// </summary>
        static public Transform[] FindSubstringRecursive (this Transform transform, string substr)
        {
            List <Transform> results = new List <Transform> ();

            foreach (Transform t in transform) {
                if (t.name.Contains (substr)) results.Add (t);
                results.AddRange (t.FindSubstringRecursive (substr));
            }

            return results.ToArray ();
        }
    }
}