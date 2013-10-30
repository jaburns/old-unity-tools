
using System;
using System.Collections.Generic;

namespace UnityTools
{
    static public class HMath
    {
        /// <summary>
        /// Returns an inclusive single-stepped list containing a range of integers.
        /// </summary>
        static public List <int> IntRange (int a, int b)
        {
            if (a == b) return new List <int> { a };
            var result = new List <int> (1 + Math.Abs (a - b));
            int step = a < b ? 1 : -1 ;
            for (int v = a ; v != b + step ; v += step) result.Add (v);
            return result;
        }

        /// <summary>
        /// Restricts a comparable type a specified range.
        /// </summary>
        static public T Clamp <T> (this T val, T min, T max) where T : IComparable <T>  {
            if (val.CompareTo (min) < 0) return min;
            if (val.CompareTo (max) > 0) return max;
            return val;
        }

        /// <summary>
        /// Returns a random point which lies within the unit circle.  Repeated calls
        /// to this function will produce an even distribution in Cartesian space.
        /// </summary>
        /// <returns>Two doubles: [x, y]</returns>
        static public double[] RandPtInUnitCircle (Func<double> randGen = null)
        {
            if (randGen == null) randGen = () => new Random().NextDouble();
            double t = 2.0 * Math.PI * randGen();
            double u = randGen() + randGen();
            double r = u > 1.0 ? 2.0 - u : u ;
            return new double [] { r*Math.Cos(t), r*Math.Sin(t) };
        }
    }
}