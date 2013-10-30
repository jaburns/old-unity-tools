
using System;

namespace UnityTools 
{
    public static class HFormat
    {
        public static string TimeFromSeconds (int seconds) {
            int s = (seconds % 60).Clamp (0, int.MaxValue);
            int m = (seconds / 60).Clamp (0, int.MaxValue);
            return string.Format ("{0}:{1}", m, (s < 10 ? "0" : "") + s.ToString());
        }
    }
}
