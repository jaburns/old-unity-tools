
using System;
using UnityEngine;
using UnityEditor;

namespace UnityTools
{
    public static class AssetPipeline
    {
        public static void CreateAsset <T> (string path) where T : ScriptableObject
        {
            var asset = ScriptableObject.CreateInstance <T> ();
            AssetDatabase.CreateAsset (asset, path);
            AssetDatabase.SaveAssets ();
            EditorUtility.FocusProjectWindow ();
            Selection.activeObject = asset;
        }
    }
}