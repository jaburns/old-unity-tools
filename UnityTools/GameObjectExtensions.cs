
using System;
using UnityEngine;

namespace UnityTools
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// This method can be used to find a component on a gameobject which implements some interface.
        /// </summary>
        public static T GetComponentI <T> (this GameObject go) where T : class {
            return go.GetComponent (typeof (T)) as T;
        }

        /// <summary>
        /// Returns all the components on a gameobject which implement the specified interface.
        /// </summary>
        public static T[] GetComponentsI <T> (this GameObject go) where T : class {
            return go.GetComponents (typeof (T)) as T[];
        }

        /// <summary>
        /// Attempts to find a specific component in the transform hierachy above a given object.
        /// </summary>
        public static T GetComponentInParents <T> (this GameObject go) where T : Component {
            var c = go.GetComponent <T> ();
            if (c != null) return c;
            if (go.transform.parent == null) return null;
            return go.transform.parent.gameObject.GetComponentInParents <T> ();
        }

        /// <summary>
        /// Execute an action with a specific component interface if it's present on the game object.
        /// </summary>
        public static void IfComponentI <T> (this GameObject go, Action <T> action) where T : class {
            var i = go.GetComponentI <T> ();
            if (i != null) action (i);
        }

        /// <summary>
        /// Execute an action with a specific component if it's present on the game object.
        /// </summary>
        public static void IfComponent <T> (this GameObject go, Action <T> action) where T : Component {
            var c = go.GetComponent <T> ();
            if (c != null) action (c);
        }

        /// <summary>
        /// </summary>
        public static GameObject InstantiateChild (this GameObject parent, GameObject prototype, bool preserveScale = false)
        {
            var child = UnityEngine.Object.Instantiate (prototype) as GameObject;
            var rotCache = child.transform.rotation;
            var scaleCache = child.transform.localScale;
            child.transform.position = parent.transform.position;
            child.transform.parent = parent.transform;
            if (!preserveScale) child.transform.localScale = scaleCache;
            child.transform.localRotation = rotCache;
            return child;
        }
    }
}
