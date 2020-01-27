using MouseHook.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseHook
{
    public class MouseHook : IDisposable
    {
        #region Private Fields

        private bool _isHookInstalled;
        private IntPtr _hookID;
        private LowLevelMouseProc _proc;

        internal delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        #endregion

        #region Private Methods

        private void Initialize(bool installHook)
        {
            if (installHook)
            {
                SetHook();
            }
            else
            {
                _isHookInstalled = false;
            }
        }

        private bool SetHook()
        {
            if (!_isHookInstalled)
            {
                _proc = new LowLevelMouseProc(this.HookCallback);

                using (Process curProcess = Process.GetCurrentProcess())
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    _hookID = WinAPI.SetWindowsHookEx(HookType.WH_MOUSE_LL, _proc, WinAPI.GetModuleHandle(curModule.ModuleName), 0);

                    _isHookInstalled = true;

                    return true;
                }
            }

            return false;
        }

        private bool ClearHook()
        {
            if (_isHookInstalled)
            {
                if (_hookID != IntPtr.Zero)
                {
                    WinAPI.UnhookWindowsHookEx(_hookID);
                    _hookID = IntPtr.Zero;
                    _isHookInstalled = false;
                    return true;
                }
            }

            return false;
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                NewMouseEvent(wParam, hookStruct);
            }

            return WinAPI.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void NewMouseEvent(IntPtr wParam, MSLLHOOKSTRUCT hookStruct)
        {
            MouseEvent?.Invoke(this, new LowLevelMouseEventArgs((EventType)wParam, hookStruct));
        }


        #endregion

        #region Public

        public MouseHook()
        {
            Initialize(false);
        }

        public MouseHook(bool installHook)
        {
            Initialize(installHook);
        }

        public bool InstallHook()
        {
            return SetHook();
        }

        public bool UninstallHook()
        {
            return ClearHook();
        }

        public bool IsHookInstalled
        {
            get { return _isHookInstalled; }
        }

        public event System.EventHandler<LowLevelMouseEventArgs> MouseEvent;

        #endregion

        #region IDisposable Support

        ~MouseHook()
        {
            Dispose(false);
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ClearHook();
                }

                disposed = true;
            }
        }

        #endregion IDisposable Support
    }
}
