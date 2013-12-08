
using System;

namespace UnityTools
{
    static public class Geom
    {
        /// <summary>
        /// Given the coordinates of the end points of two line segments, this function returns
        /// their intersection point, or null if they do not intersect.
        /// </summary>
        /// <returns>Null, or an array containing the intersection point [x, y]</returns>
        static public float[] LineLineIntersect 
            (float p00x, float p00y, float p01x, float p01y,
             float p10x, float p10y, float p11x, float p11y)
        {
            var dx1x3 = p00x - p10x;
            var dy1y3 = p00y - p10y;
            var dx2x1 = p01x - p00x;
            var dy2y1 = p01y - p00y;
            var dx4x3 = p11x - p10x;
            var dy4y3 = p11y - p10y;

            var denom = dy4y3*dx2x1 - dx4x3*dy2y1;
            var numa  = dx4x3*dy1y3 - dy4y3*dx1x3;
            var numb  = dx2x1*dy1y3 - dy2y1*dx1x3;

            if (denom == 0) return null;

            numa /= denom;
            numb /= denom;

            if (numa >= 0 && numa <= 1 && numb >= 0 && numb <= 1)
                return new float [] { p00x + dx2x1*numa, p00y + dy2y1*numa };

            return null;
        }
        
        /// <summary>
        /// Returns true if the supplied point (x,y) lies in the space bounded by two
        /// infinite lines which intersect the points defining the provided line segment, and
        /// are perpendicular to the provided line segment.
        /// </summary>
        static public bool PointInLinePerpSpace (float ax, float ay, float bx, float by, float x, float y)
        {
            float _ax, _ay;
            float _bx, _by;
            float _cx, _cy;
            
            float perpSlope = (ax-bx)/(by-ay);
            
            // If the slope is greater than 1, transpose the coordinate space to avoid infinity.
            if (perpSlope > 1)   
            {
                _ax = ay; _bx = by; _cx = y;
                _ay = ax; _by = bx; _cy = x;
                perpSlope = (_ax-_bx)/(_by-_ay);
            }
            else
            {
                _ax = ax; _bx = bx; _cx = x;
                _ay = ay; _by = by; _cy = y;
            }
            
            float yMin, yMax;
            
            if (_ay > _by)
            {
                yMin = perpSlope*(_cx - _bx) + _by;
                yMax = perpSlope*(_cx - _ax) + _ay;
            }
            else
            {
                yMin = perpSlope*(_cx - _ax) + _ay;
                yMax = perpSlope*(_cx - _bx) + _by;
            }
            
            return _cy > yMin && _cy < yMax;
        }


        const float ACCURACY     = 1000000;
        const float ACCURACY_INV = 1/ACCURACY;

        static public float[] ProjectPointOnLine (float m, float x, float y)
        {
            var result = new float [] { 0, 0 };
            
            if (Math.Abs (m) < ACCURACY_INV) // Zero slope, horizontal line
            {
                result[0] = x;
                result[1] = 0;
            }
            else if (Math.Abs (m) > ACCURACY) // Infinite slope, vertical line
            {
                result[0] = 0;
                result[1] = y;
            }
            else
            {
                result[1] = (y*m+x)*m/(1+m*m);
                result[0] = result[1] / m;
            }
            
            return result;
        }
    }
}
