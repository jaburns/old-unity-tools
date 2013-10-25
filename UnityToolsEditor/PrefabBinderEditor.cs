
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityTools
{
    [CustomEditor(typeof(PrefabBinder))]
    public class PrefabBinderEditor : Editor
    {
        PrefabBinder _targ;

        void OnEnable () {
            _targ = target as PrefabBinder;
        }

        public override void OnInspectorGUI()
        {
            _targ.Enabled = EditorGUILayout.Toggle ("Enabled", _targ.Enabled);
            _targ.BindMultiple = EditorGUILayout.Toggle ("Bind Multiple", _targ.BindMultiple);

            PrefabBinder.PrefabBinding deadBinding = null;
            PrefabBinder.PrefabBinding moveUpBinding = null;
            PrefabBinder.PrefabBinding moveDownBinding = null;

            foreach (var binding in _targ.Bindings)
            {
                EditorGUILayout.Separator ();
                binding.BindingSiteName = EditorGUILayout.TextField ("Binding Site Name", binding.BindingSiteName);
                binding.Prefab = EditorGUILayout.ObjectField ("Prefab", binding.Prefab, typeof(GameObject), false) as GameObject;
                EditorGUILayout.BeginHorizontal ();
                    if (GUILayout.Button ("X")) deadBinding = binding;
                    if (GUILayout.Button ("^")) moveUpBinding = binding;
                    if (GUILayout.Button ("v")) moveDownBinding = binding;
                EditorGUILayout.EndHorizontal ();
            }

            if (deadBinding != null) _targ.Bindings.Remove (deadBinding);

            if (moveUpBinding != null) {
                int index = _targ.Bindings.IndexOf (moveUpBinding);
                if (index > 0) index--;
                _targ.Bindings.Remove (moveUpBinding);
                _targ.Bindings.Insert (index, moveUpBinding);
            }

            if (moveDownBinding != null) {
                int index = _targ.Bindings.IndexOf (moveDownBinding);
                if (index < _targ.Bindings.Count - 1) index++;
                _targ.Bindings.Remove (moveDownBinding);
                _targ.Bindings.Insert (index, moveDownBinding);
            }

            EditorGUILayout.Separator ();

            if (GUILayout.Button ("New Prefab Binding")) {
                _targ.Bindings.Add (new PrefabBinder.PrefabBinding ());
            }

            if (GUI.changed) EditorUtility.SetDirty (_targ);
        }
    }
}