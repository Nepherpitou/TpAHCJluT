using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using WindowsInput;
using WindowsInput.Native;

namespace TpaHCJluT.modules
{
    class HookManager
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        private const int MAIN_HOOK = 0x9000;
        private const int WM_HOTKEY = 0x0312;
        private static readonly uint MOD_ALT = 0x0001;
        private static readonly uint MOD_WIN = 0x0008;
        private static readonly uint VK_OEM_4 = 0xDB;

        private IntPtr _windowHandle;

        public event EventHandler HotkeyPressed;

        public HookManager(Window parentWindow)
        {
            parentWindow.SourceInitialized += (sender, args) =>
            {
                _windowHandle = new WindowInteropHelper((Window) sender).Handle;
                HwndSource source = HwndSource.FromHwnd(_windowHandle);
                source?.AddHook(HwndHook);
                RegisterHotKey(_windowHandle, MAIN_HOOK, MOD_WIN, VK_OEM_4);
            };

            parentWindow.Closing += (sender, args) =>
            {
                UnregisterHotKey(_windowHandle, MAIN_HOOK);
            };
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case MAIN_HOOK:
                            int vkey = ((int)lParam >> 16) & 0xFFFF;
                            if (vkey == VK_OEM_4)
                            {
                                HotkeyPressed?.Invoke(this, EventArgs.Empty);
                            }
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }
    }
}
