
using System;
using UnityEngine;

namespace UnityTools
{
    public static class MiscExtensions
    {
        public static bool Intersects (this Rect a, Rect b) {
            return a.x < b.xMax
                && a.xMax > b.x
                && a.y < b.yMax
                && a.yMax > b.y;
        }
    }
}