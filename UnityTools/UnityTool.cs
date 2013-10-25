
using System;
using System.Collections;
using UnityEngine;
using System.Reflection;

namespace UnityTools
{
    public static class UnityTool
    {
        /// <summary>
        /// Hard stop the game with one last error message to the console.  Something is amiss and we shouldn't continue.
        /// Optional parameters ftw.
        /// </summary>
        public static void Fail (string message, UnityEngine.Object context = null)
        {
            if (context != null)
                Debug.LogError( "FAILURE: " + message, context );
            else
                Debug.LogError( "FAILURE: " + message );

            // If we're in the editor, do some dirty reflection to force it to stop.
            if (Application.isEditor) {
                var t = System.Type.GetType ("UnityEditor.EditorApplication,UnityEditor");
                var prop = t.GetProperty ("isPlaying", BindingFlags.Public | BindingFlags.Static);
                prop.SetValue (null, false, null);
            }
        }

        /// <summary>
        /// Bind this function to Unity's Application.RegisterLogCallback() broadcaster to cause the game to quit on exceptions.
        /// </summary>
        public static void LogCallbackFailOnException (string condition, string stackTrace, LogType type)
        {
            // The error should be logged by the time this gets called.
            if (type == LogType.Exception) Fail ("Internal Exception\n" + condition + "\n" + stackTrace);
        }

        /// <summary>
        /// Spawns a simple coroutine which waits 'time' seconds and then invokes the provided action.
        /// </summary>
        public static void WaitAndDo (MonoBehaviour script, float time, Action action) {
            script.StartCoroutine (waitAndDoRoutine (time, action));
        }
        static IEnumerator waitAndDoRoutine (float time, Action action) {
            yield return new WaitForSeconds (time);
            action ();
        }
    }
}
