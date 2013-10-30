
using System;
using System.Collections.Generic;

namespace UnityTools
{
    static public class IEnumerableExtensions
    {
        /// <summary>
        /// Given an enumerable collection, this function will return a shuffled
        /// copy of its contents.
        /// </summary>
        static public List <T> Shuffled <T> (this IEnumerable <T> collection)
        {
            var input  = new List <T> (collection);
            var output = new List <T> ();

            Random r = new Random();

            while (input.Count > 0) {
                var i = r.Next (input.Count);
                output.Add (input [i]);
                input.RemoveAt (i);
            }

            return output;
        }

        /// <summary>
        /// Compares all the values in a collection and returns the one which compares
        /// positively against all the rest.
        /// </summary>
        static public T Max <T> (this IEnumerable <T> collection) where T : IComparable <T>
        {
            var list = new List <T> (collection);

            if (list.Count == 0) return default (T);
            if (list.Count == 1) return list [0];

            T retval = list [0];

            for (int i = 1 ; i < list.Count ; ++i) {
                if (list[i].CompareTo (retval) > 0) retval = list[i];
            }

            return retval;
        }

        /// <summary>
        /// Picks a random value from an enumerable collection and returns it.
        /// </summary>
        static public T Random <T> (this IEnumerable <T> collection)
        {
            var list = new List <T> (collection);

            return list.Count == 0 ? default(T)
                 : list.Count == 1 ? list[0]
                 : list [ new Random ().Next (0, list.Count) ];
        }

        /// <summary>
        /// Divides a list in to sublists of a given length.  The last list in the result will
        /// contain the remainder and not necessarily be of the provided length.
        /// </summary>
        static public List <List <T>> Divide <T> (this IEnumerable <T> collection, int lengths)
        {
            var input = new Stack <T> (collection);
            var output = new List <List <T>> ();

            int index = 0;
            var curList = new List <T> ();

            while (input.Count > 0)
            {
                curList.Add (input.Pop ());

                if (++index >= lengths) {
                    index = 0;
                    output.Add (curList);
                    curList = new List <T> ();
                }
            }

            if (curList.Count > 0) output.Add (curList);

            return output;
        }
    }
}
