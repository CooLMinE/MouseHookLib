using System;
using System.Windows.Forms;

namespace MouseHookLib
{
    static internal class Delegates
    {
        internal delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
    }
}
