using System;

namespace HMG
{
    public static class RandomExtensions
    {
        public static void Shuffle (this Random rng, Array array)
        {
            
            int n = array.Length;
            while (n > 1) {
                int k = rng.Next (n--);
                object temp = array.GetValue (n);
                array.SetValue (array.GetValue (k), n);
                array.SetValue (temp, k);
            }
        }
    }
}

