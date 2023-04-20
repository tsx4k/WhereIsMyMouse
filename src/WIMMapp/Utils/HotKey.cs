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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WIMMapp.Utils
{
    public class HotKey
    {
        public Key Key { get; }

        public ModifierKeys Modifiers { get; }

        public HotKey(Key key, ModifierKeys modifiers)
        {
            Key = key;
            Modifiers = modifiers;
        }

        public override string ToString()
        {
            var str = new StringBuilder();

            if (Modifiers.HasFlag(ModifierKeys.Control))
                str.Append("CTRL + ");
            if (Modifiers.HasFlag(ModifierKeys.Shift))
                str.Append("SHIFT + ");
            if (Modifiers.HasFlag(ModifierKeys.Alt))
                str.Append("ALT + ");
            if (Modifiers.HasFlag(ModifierKeys.Windows))
                str.Append("WIN + ");

            str.Append(Key);

            return str.ToString();
        }

        public string String => ToString();

        internal static HotKey FromString(string keyStr)
        {
            try
            {
                var gestureConverter = new KeyGestureConverter();
                var gesture = (KeyGesture)gestureConverter.ConvertFromString(keyStr);
                return new HotKey(gesture.Key, gesture.Modifiers);
            } catch { }
            return null;
        }
    }
}
