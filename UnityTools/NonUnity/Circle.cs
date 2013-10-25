
using System;

namespace HMG
{
    public struct Circle
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Radius;

        public Circle (float x, float y, float radius) {
            X = x;
            Y = y;
            Radius = radius;
        }

        public bool Intersects (Circle c) {
            var dx = c.X - X;
            var dy = c.Y - Y;
            var r  = c.Radius + Radius;
            return dx*dx + dy*dy <= r*r;
        }
    }
}