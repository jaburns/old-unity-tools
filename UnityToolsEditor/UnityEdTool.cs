
using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace UnityTools
{
    static public class UnityEdTool
    {
        static public void CreateAsset <T> (string path) where T : ScriptableObject
        {
            var asset = ScriptableObject.CreateInstance <T> ();
            AssetDatabase.CreateAsset (asset, path);
            AssetDatabase.SaveAssets ();
            EditorUtility.FocusProjectWindow ();
            Selection.activeObject = asset;
        }

        static public bool DefaultHandlesHidden
        {
            get {
                Type type = typeof(Tools);
                FieldInfo field = type.GetField("s_Hidden", BindingFlags.NonPublic | BindingFlags.Static);
                return ((bool)field.GetValue(null));
            }
            set {
                Type type = typeof(Tools);
                FieldInfo field = type.GetField("s_Hidden", BindingFlags.NonPublic | BindingFlags.Static);
                field.SetValue(null, value);
            }
        }
    }
}
