using System;
using System.Windows.Forms;

namespace MouseHookLib
{
    public class Delegates
    {
        internal delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        public delegate void OnMouseClickDown(object o, MouseEventArgs e);
        public delegate void OnMouseClickUp(object o, MouseEventArgs e);
        public delegate void OnMouseMove(object o, MouseEventArgs e);
    }
}
