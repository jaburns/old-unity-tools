
using System;
using System.Collections.Generic;

namespace UnityTools
{
    static public class BadJson
    {
        /// <summary>
        /// Traverse an annoying tree of Dictionary{string,object}s to recover an array.
        /// </summary>
        static public List <object> Array (object obj, string path) {
            return Value <List <object>> (obj, path);
        }

        /// <summary>
        /// Traverse an annoying tree of Dictionary{string,object}s to recover a value of a
        /// specific type. Returns default value of the provided type if a value cannot be
        /// found.
        /// </summary>
        /// <param name="path">JSON value to access i.e. "data.name.last"</param>
        static public T Value <T> (object obj, string path)
        {
            string[] pathParts = path.Split (new char[]{'.'}, 2);

            var asDict = obj as Dictionary <string, object>;
            if (asDict == null || ! asDict.ContainsKey (pathParts[0])) return default (T);

            var newObj = asDict [pathParts [0]];

            if (pathParts.Length > 1) return Value <T> (newObj, pathParts [1]);

            return (T) Convert.ChangeType (newObj, typeof (T));
        }
    }
}
