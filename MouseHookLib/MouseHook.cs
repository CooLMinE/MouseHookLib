using MouseHookLib.Utilities;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MouseHookLib
{
    public class MouseHook
    {
        private MouseHookLib.Delegates.LowLevelMouseProc mouseProc;
        private static IntPtr hHook = IntPtr.Zero;
        public event Delegates.OnMouseMove MouseMove;
        public event Delegates.OnMouseClickDown MouseClickDown;
        public event Delegates.OnMouseClickUp MouseClickUp;

        public MouseHook()
        {
        }

        /// <summary>
        /// Install the application-defined hook procedure into a hook chain.
        /// </summary>
        public void Hook()
        {
            mouseProc = new Delegates.LowLevelMouseProc(HookCallback);

            using (ProcessModule currentModule = Process.GetCurrentProcess().MainModule)
                hHook = NativeMethods.SetWindowsHookEx(WindowsMessages.WH_MOUSE_LL, mouseProc, NativeMethods.GetModuleHandle(currentModule.ModuleName), 0);

            if (hHook == IntPtr.Zero)
                throw new Exception("Unable to register mouse hook.");
        }

        /// <summary>
        /// Remove the hook from the hook chain.
        /// </summary>
        public void UnHook()
        {
            NativeMethods.UnhookWindowsHookEx(hHook);
        }

        /// <summary>
        /// The hook callback method.
        /// </summary>
        /// <param name="nCode">The message code being received.</param>
        /// <param name="wParam">Additional information about the message.</param>
        /// <param name="lParam">Additional information about the message.</param>
        /// <returns></returns>
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                switch ((MouseMessages)wParam)
                {
                    case MouseMessages.WM_LBUTTONDOWN:
                        if (MouseClickDown != null)
                            MouseClickDown(this, new MouseEventArgs(MouseButtons.Left, 1, hookStruct.pt.x, hookStruct.pt.y, 0));
                        break;
                    case MouseMessages.WM_RBUTTONDOWN:
                        if (MouseClickDown != null)
                            MouseClickDown(this, new MouseEventArgs(MouseButtons.Right, 1, hookStruct.pt.x, hookStruct.pt.y, 0));
                        break;
                    case MouseMessages.WM_LBUTTONUP:
                        if (MouseClickUp != null)
                            MouseClickUp(this, new MouseEventArgs(MouseButtons.Left, 1, hookStruct.pt.x, hookStruct.pt.y, 0));
                        break;
                    case MouseMessages.WM_RBUTTONUP:
                        if (MouseClickUp != null)
                            MouseClickUp(this, new MouseEventArgs(MouseButtons.Right, 1, hookStruct.pt.x, hookStruct.pt.y, 0));
                        break;
                    case MouseMessages.WM_MOUSEMOVE:
                        if (MouseMove != null)
                            MouseMove(this, new MouseEventArgs(MouseButtons.None, 1, hookStruct.pt.x, hookStruct.pt.y, 0));
                        break;
                }
            }
            return NativeMethods.CallNextHookEx(hHook, nCode, wParam, lParam);
        }
    }
}
