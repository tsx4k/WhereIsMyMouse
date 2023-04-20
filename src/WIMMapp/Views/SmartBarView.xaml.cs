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
using System.Linq;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WIMMapp.Themes;

namespace WIMMapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SmartBarView : Window
    {
        readonly ViewModels.SmartBarViewModel model;
        public ViewModels.SmartBarViewModel Model => model;

        readonly WindowInteropHelper windowInteropHelper;
        private bool canClose = false;

        public SmartBarView(ViewModels.SmartBarViewModel model)
        {
            this.model = model;
            this.DataContext = model;
            this.Closing += SmartBarView_Closing;
            model.Window = this;
            InitializeComponent();
            windowInteropHelper = new WindowInteropHelper(this);
            model.ArrangeWindow();
            model.AddHandlers();
            Title = $"{model.AppInfo.CodeName} bar on '{model.Screen.DeviceName}' screen";
        }

        private void SmartBarView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !canClose;
        }

        public void ForceClose()
        {
            canClose = true;
            Close();
        }

        public IntPtr Handle => windowInteropHelper.Handle;

        public void MousePosition(System.Drawing.Point position)
        {
            model.MousePosition(position);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            model.BarWidth = e.NewSize.Width;
            Height = e.NewSize.Height;
        }

        public void VisibilityChange(bool show)
        {
            model.VisibilityChange(show);
        }

        internal void CursorHeartbeat()
        {
            model.CursorHeartbeat();
        }

        internal void TickTimer()
        {
            model.TickTimer();
        }

        private void CursorIndicator_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            model.ShowBox(true);
        }
    }
}
