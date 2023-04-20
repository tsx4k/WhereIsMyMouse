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
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WIMMapp.Utils;

namespace WIMMapp.Controls
{
    /// <summary>
    /// Interaction logic for HotKeyEdit.xaml
    /// </summary>
    public partial class HotKeyEdit : UserControl, INotifyPropertyChanged
    {
        public HotKeyEdit()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public static readonly DependencyProperty HotKeyProperty =
                DependencyProperty.Register(
        nameof(HotKey),
                    typeof(HotKey),
                    typeof(HotKeyEdit),
                    new FrameworkPropertyMetadata(
                        default(HotKey),
                        FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
        ));

        public HotKey HotKey
        {
            get => (HotKey)GetValue(HotKeyProperty);
            set
            {
                if (IsValidHotKey(value))
                {
                    SetValue(HotKeyProperty, value);
                    OnPropertyChanged("HotKey");
                }
            }
        }

        private bool IsValidHotKey(HotKey newHotKey)
        {
            try
            {
                // e.g. simple "P" is not a valid HotKey for us, but CTRL+P is
                if (
                    newHotKey.Modifiers.HasFlag(ModifierKeys.Control) ||
                    newHotKey.Modifiers.HasFlag(ModifierKeys.Alt) ||
                    newHotKey.Modifiers.HasFlag(ModifierKeys.Shift) ||
                    newHotKey.Modifiers.HasFlag(ModifierKeys.Windows))
                {
                    var gestureConverter = new KeyGestureConverter();
                    KeyGesture gesture = (KeyGesture)gestureConverter.ConvertFromString(newHotKey.ToString()); // additionaly this can cause an exception
                    return true;
                }
            } catch { }
            return false;
        }

        private void HotkeyTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;

            var modifiers = Keyboard.Modifiers;
            var key = e.Key;

            if (key == Key.System)
            {
                key = e.SystemKey;
            }

            if (modifiers == ModifierKeys.None &&
                (key == Key.Delete || key == Key.Back || key == Key.Escape))
            {
                HotKey = null;
                return;
            }

            if (key == Key.LeftCtrl ||
                key == Key.RightCtrl ||
                key == Key.LeftAlt ||
                key == Key.RightAlt ||
                key == Key.LeftShift ||
                key == Key.RightShift ||
                key == Key.LWin ||
                key == Key.RWin ||
                key == Key.Clear ||
                key == Key.OemClear ||
                key == Key.Apps)
            {
                return;
            }
            HotKey = new HotKey(key, modifiers);
        }
    }
}
