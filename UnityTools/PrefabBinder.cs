
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools
{
    /// <summary>
    /// This component allows prefabs to be bound arbitrarily deep in a given GameObject's transform
    /// hierarchy.  When this component is loaded, the supplied prefabs are bound to the first transform
    /// found whose name matches the one supplied.  If no transform is found with a given name, the
    /// prefab associated with it is not instantiated.
    /// </summary>
    public class PrefabBinder : MonoBehaviour
    {
        [Serializable]
        public class PrefabBinding {
            public string BindingSiteName;
            public GameObject Prefab;
        }

        public bool Enabled = true;
        public bool BindMultiple = false;

        public List <PrefabBinding> Bindings;

        void Awake ()
        {
            if (!Enabled) return;
            
            var sites = new List <Transform> ();

            foreach (var binding in Bindings)
            {
                if (binding.Prefab == null) continue;

                sites.Clear ();

                if (binding.BindingSiteName == "$this") {
                    sites.Add (transform);
                } 
                else if (BindMultiple) {
                    sites.AddRange (transform.FindSubstringRecursive (binding.BindingSiteName));
                }
                else {
                    var site = transform.FindRecursive (binding.BindingSiteName);
                    if (site != null) sites.Add (site);
                }

                foreach (var site in sites) {
                    site.gameObject.InstantiateChild (binding.Prefab);
                }
            }
        }
    }
}