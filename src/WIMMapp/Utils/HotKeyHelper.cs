/*

MIT Creator Revision License v1.0 (MITCRL1.0)

Copyright (c) 2023 Tomasz Szynkar (tsx4k [TSX], tszynkar@tlen.pl, https://github.com/tsx4k)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute copies of the Software
and to permit persons to whom the Software is furnished to do so, 
subject to the following conditions:

1. The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
2. There are no permissions, and/or no rights to fork, make similar Software,
sublicense, and/or sell copies of the Software, and/or any part of it.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;

namespace WIMMapp.Utils
{
    public class HotKeyHelper : IDisposable
    {
        Window view;
        int hkIdCounter = 19288;


        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, HotkeyFlags modifiers, uint vkey);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        [Flags]
        internal enum HotkeyFlags : uint
        {
            None = 0x0000,
            Alt = 0x0001,
            Control = 0x0002,
            Shift = 0x0004,
            Windows = 0x0008,
            NoRepeat = 0x4000
        }
        private const int WM_HOTKEY = 0x0312;

        List<Tuple<int, HotkeyHandler>> _hotkeys = new List<Tuple<int, HotkeyHandler>> ();
        WindowInteropHelper _windowInteropHelper;
        private HwndSource _source;

        public HotKeyHelper(Window view)
        {
            this.view = view;
            _windowInteropHelper= new WindowInteropHelper(view);
            _source = HwndSource.FromHwnd(_windowInteropHelper.Handle);
            _source.AddHook(HwndHook);
        }

        public delegate void HotkeyHandler(int id);

        private readonly KeyGestureConverter _gestureConverter = new KeyGestureConverter();


        private HotkeyFlags GetFlags(ModifierKeys modifiers, bool norepeat)
        {
            var flags = HotkeyFlags.None;
            if (modifiers.HasFlag(ModifierKeys.Shift))
                flags |= HotkeyFlags.Shift;
            if (modifiers.HasFlag(ModifierKeys.Control))
                flags |= HotkeyFlags.Control;
            if (modifiers.HasFlag(ModifierKeys.Alt))
                flags |= HotkeyFlags.Alt;
            if (modifiers.HasFlag(ModifierKeys.Windows))
                flags |= HotkeyFlags.Windows;
            if (norepeat)
                flags |= HotkeyFlags.NoRepeat;
            return flags;
        }

        private ModifierKeys GetModifiers(HotkeyFlags flags)
        {
            var modifiers = ModifierKeys.None;
            if (flags.HasFlag(HotkeyFlags.Shift))
                modifiers |= ModifierKeys.Shift;
            if (flags.HasFlag(HotkeyFlags.Control))
                modifiers |= ModifierKeys.Control;
            if (flags.HasFlag(HotkeyFlags.Alt))
                modifiers |= ModifierKeys.Alt;
            if (flags.HasFlag(HotkeyFlags.Windows))
                modifiers |= ModifierKeys.Windows;
            return modifiers;
        }
        public int AddHotKey(string hotKey, HotkeyHandler onHotKey)
        {
            KeyGesture gesture = (KeyGesture)_gestureConverter.ConvertFromString(hotKey);
            var vk = (uint)KeyInterop.VirtualKeyFromKey(gesture.Key);
            int id = hkIdCounter++;
            if (RegisterHotKey(_windowInteropHelper.Handle, id, GetFlags(gesture.Modifiers, false), vk))
            {
                _hotkeys.Add(new Tuple<int, HotkeyHandler>(id, onHotKey));
                return id;
            }
            return -1;
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_HOTKEY:
                    var hotkey = _hotkeys.Find(x => x.Item1 == wParam.ToInt32());
                    if(hotkey != null) {
                        handled = true;
                        hotkey.Item2?.Invoke(hotkey.Item1);
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        public bool RemoveHotKey(int id)
        {
            var hotkey = _hotkeys.Find(x => x.Item1 == id);
            if(hotkey != null)
            {
                return UnregisterHotKey(_windowInteropHelper.Handle, hotkey.Item1);
            }
            return false;
        }

        public void ClearHotKeys()
        {
            foreach (var hotkey in _hotkeys)
            {
                UnregisterHotKey(_windowInteropHelper.Handle, hotkey.Item1);
            }
            _hotkeys.Clear();
        }

        public void Dispose()
        {
            ClearHotKeys();
            _hotkeys = null;
            _source.RemoveHook(HwndHook);
            _source = null;
        }
    }
}
