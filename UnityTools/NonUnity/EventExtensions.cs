
using System;

namespace UnityTools
{
    public static class EventExtensions 
    {
        static public void Raise (this EventHandler handler, object sender = null, EventArgs e = null) {
            if (handler != null) handler (sender, e);
        }
    }
}