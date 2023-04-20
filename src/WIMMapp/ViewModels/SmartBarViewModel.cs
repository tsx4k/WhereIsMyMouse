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
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using WIMMapp.Configuration;
using WIMMapp.Handlers;
using WIMMapp.Interfaces;
using WIMMapp.Models.Application;
using WIMMapp.Models.Configuration;
using WIMMapp.Themes;
using WIMMapp.Utils;

namespace WIMMapp.ViewModels
{
    public class SmartBarViewModel: INotifyPropertyChanged
    {
        public enum Tabs
        {
            About = 0,
            Settings,
            Extras
        }

        public Screen Screen { get; internal set; }
        public SmartBarView Window { get; internal set; }
        
        public Themes.ThemeManager ThemeManager { get; internal set; }


        private double width = 1000, height = 4, indicatorwidth = 100, indicatortop=0, opacity = 1.0;
        public double BarWidth { get { return width; } set { width = value; OnPropertyChanged("BarWidth"); } }
        public double BarHeight { get { return height; } set { height = value; OnPropertyChanged("BarHeight"); } }
        public double IndicatorWidth { get { return indicatorwidth; } set { indicatorwidth = value; OnPropertyChanged("IndicatorWidth"); } }
        public double IndicatorTop { get { return indicatortop; } set { indicatortop = value; OnPropertyChanged("IndicatorTop"); } }
        public double BarOpacity { get { return opacity; } set { opacity = value; OnPropertyChanged("BarOpacity"); } }

        private System.Windows.Media.Brush barbrush;
        public System.Windows.Media.Brush BarBrush { get { return barbrush; } set { barbrush = value; OnPropertyChanged("BarBrush"); } }

        private System.Windows.Media.Brush bartextbrush;
        public System.Windows.Media.Brush BarTextBrush { get { return bartextbrush; } set { bartextbrush = value; OnPropertyChanged("BarTextBrush"); } }

        private System.Windows.Media.Color bartextcolor;
        public System.Windows.Media.Color BarTextColor { get { return bartextcolor; } set { bartextcolor = value; OnPropertyChanged("BarTextColor"); } }

        private System.Windows.Media.Pen bartextpen;
        public System.Windows.Media.Pen BarTextPen { get { return bartextpen; } set { bartextpen = value; OnPropertyChanged("BarTextPen"); } }


        private System.Windows.Media.Brush indicatorbrush;
        public System.Windows.Media.Brush IndicatorBrush { get { return indicatorbrush; } set { indicatorbrush = value; OnPropertyChanged("IndicatorBrush"); } }

        private System.Windows.Media.Color indicatorcolor;
        public System.Windows.Media.Color IndicatorColor { get { return indicatorcolor; } set { indicatorcolor = value; OnPropertyChanged("IndicatorColor"); } }

        private bool isActive = false;
        public bool IsActive => isActive;

        System.Windows.Media.Brush activeBrush = null, inactiveBrush = null;

        public AppInformation AppInfo => new AppInformation();

        public Config Config { get; set; }

        SmartBarView view;

        private HotKeyHelper hotkeyHelper;

        public IRunner Runner;


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public SmartBarViewModel()
        {
            Config config = new Config();
            Screen screen = Screen.AllScreens.First();

            this.Config = config;
            this.Screen = screen;
            ThemeManager.CurrentTheme = config.Settings.Theme;
        }

        public SmartBarViewModel(Config config, Screen screen, ThemeManager themeManager, IRunner runner)
        {
            this.Runner = runner;
            this.Config = config;
            this.Screen = screen;
            this.ThemeManager = themeManager;
            ThemeManager.CurrentTheme = config.Settings.Theme;
        }

        int tabSelectedIndex = 0;
        public int TabSelectedIndex { get { return tabSelectedIndex; } set { tabSelectedIndex = value; OnPropertyChanged("TabSelectedIndex"); } }


        public ICommand ButtonCommand { get { return new RelayCommand(OnButton); } }
        private void OnButton(object param)
        {
            switch(param as string)
            {
                case "Settings":
                    TabSelectedIndex = (int)Tabs.Settings;
                    break;
                case "About":
                    TabSelectedIndex = (int)Tabs.About;
                    break;
                case "Extras":
                    TabSelectedIndex = (int)Tabs.Extras;
                    break;
                case "Hide":
                    //Console.WriteLine("HIDEBUTTON");
                    HideBox(true);
                    break;
            }
        }

        public void ThemeChanged()
        {
            if (view == null) return;

            Config.Settings.Theme = ThemeManager.CurrentTheme;
            Config.SaveConfig();

            IndicatorWidth = ThemeManager.CurrentTheme.IndicatorSettings.Width;
            BarHeight = ThemeManager.CurrentTheme.SmartBarSettings.Height;
            BarBrush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(
                IsActive ? ThemeManager.CurrentTheme.SmartBarSettings.ActiveColor : ThemeManager.CurrentTheme.SmartBarSettings.InactiveColor
                ));
            BarTextBrush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(
                ThemeManager.CurrentTheme.SmartBarSettings.TextColor));
            BarTextColor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(
                ThemeManager.CurrentTheme.SmartBarSettings.TextColor);
            BarTextPen = new System.Windows.Media.Pen(BarTextBrush, 1);

            IndicatorColor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(
                ThemeManager.CurrentTheme.IndicatorSettings.Color);
            IndicatorBrush = new SolidColorBrush(IndicatorColor);

            IndicatorTop = -1 * (this.Window.cursorIndicator.Height - BarHeight);

            activeBrush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(
                ThemeManager.CurrentTheme.SmartBarSettings.ActiveColor
            ));
            activeBrush.Opacity = (double)ThemeManager.CurrentTheme.SmartBarSettings.ActiveOpacity / 100.0;
            inactiveBrush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(
                ThemeManager.CurrentTheme.SmartBarSettings.InactiveColor
            ));
            inactiveBrush.Opacity = (double)ThemeManager.CurrentTheme.SmartBarSettings.InactiveOpacity / 100.0;
        }

        public void ArrangeWindow()
        {
            if(this.Window.Top != Screen.Bounds.Top)
                this.Window.Top = Screen.Bounds.Top;
            if(this.Window.Left != Screen.Bounds.Left)
                this.Window.Left = Screen.Bounds.Left;
            if(this.Window.Width != Screen.Bounds.Width || this.Window.Width != BarWidth || BarWidth != Screen.Bounds.Width)
                this.Window.Width = BarWidth = Screen.Bounds.Width;
            if (this.Window.Height != Screen.Bounds.Height)
                this.Window.Height = Screen.Bounds.Height;
        }

        public void AddHandlers()
        {
            this.Window.cursorIndicator.MouseLeave += CursorIndicator_MouseLeave;
            this.Window.cursorIndicator.MouseEnter += CursorIndicator_MouseEnter;
            this.Window.cursorIndicator.MouseMove += CursorIndicator_MouseMove;

            this.Config.Settings.OnSettingsChanged += Settings_OnSettingsChanged;
        }

        private void Settings_OnSettingsChanged(Settings newSettings, string propertyName, object propertyValue)
        {
            switch(propertyName)
            {
                case "HotKey":
                    Runner.SetHotKey((string)propertyValue);
                    Config.SaveConfig();
                    break;
            }
        }

        DateTime lastMouseMove = DateTime.MinValue;
        private void CursorIndicator_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            lastMouseMove = DateTime.Now;
            //Console.WriteLine("SHOWBOX MM");
            ShowBox();
        }

        private void CursorIndicator_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Console.WriteLine("SHOWBOX ME");
            ShowBox();
        }

        bool boxVisible = false;
        bool boxVisibilityChanging = false;
        bool hideAfterChanging = false;
        DateTime showBoxTime = DateTime.MaxValue;
        DateTime hideBoxTime = DateTime.MaxValue;
        public bool IsBoxVisible => boxVisible || boxVisibilityChanging;

        public void ShowBox(bool force = false)
        {
            if(!IsBoxVisible)
            {
                if(showBoxTime == DateTime.MaxValue)
                    showBoxTime = DateTime.Now;
                if(!force)
                    return;
            }

            hideAfterChanging = false;
            if (boxVisibilityChanging) return;

            hideBoxTime = DateTime.MaxValue;
            showBoxTime = DateTime.MaxValue;


            boxVisible = true;
            boxVisibilityChanging = true;
            double to = -1;
            var da = AnimationFactory.GetAnimation(to, AnimationMode.Fast);
            da.Completed += (o, e) =>
            {
                boxVisibilityChanging = false;
                if (hideAfterChanging)
                    HideBox();
            };
            this.Window.cursorIndicator.BeginAnimation(Canvas.TopProperty, da);
            //IndicatorTop = -1;
            this.Window.cursorIndicator.Effect = this.Window.Resources["ShadowEffect"] as DropShadowEffect;
        }

        private void CursorIndicator_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Console.WriteLine("HIDEBOX ML");
            HideBox();
        }

        public void HideBox(bool force = false)
        {
            showBoxTime = DateTime.MaxValue;
            if (IsBoxVisible)
            {
                if (hideBoxTime == DateTime.MaxValue)
                    hideBoxTime = DateTime.Now;
                if (!force)
                    return;
            }

            hideBoxTime = DateTime.MaxValue;
            if (!IsBoxVisible) return;

            hideAfterChanging = true;
            if (!force && boxVisibilityChanging) return;

            boxVisibilityChanging = true;
            double to = -1 * (this.Window.cursorIndicator.Height - BarHeight);
            var da = AnimationFactory.GetAnimation(to, AnimationMode.Fast);
            da.Completed += (o, e) => {
                this.Window.cursorIndicator.Effect = null;
                boxVisibilityChanging = false; boxVisible = false;
                if(hideAfterChanging) HideBox();
                hideAfterChanging = false;
            };
            this.Window.cursorIndicator.BeginAnimation(Canvas.TopProperty, da);
            //IndicatorTop = -1 * (this.Window.cursorIndicator.Height - BarHeight);
        }

        double currentIndicatorValueX = 0;
        public double CurrentIndicatorValueX
        {
            get { return currentIndicatorValueX; }
            set
            {
                if (IsBoxVisible) return;

                currentIndicatorValueX = value;

                DoubleAnimation da = AnimationFactory.GetAnimation(value, AnimationFactory.FromString(ThemeManager.CurrentTheme.IndicatorSettings.AnimationMode));
                this.Window.cursorIndicator.BeginAnimation(Canvas.LeftProperty, da);

                //Canvas.SetLeft(Window.cursorIndicator, currentIndicatorValueX);

                //Console.WriteLine($"{DateTime.Now.ToString("dd-MM-yyy  HH:mm:ss.ffff")}");

                OnPropertyChanged("CurrentIndicatorValueX");
            }
        }


        double currentFollowerValueX = 0;
        public double CurrentFollowerValueX
        {
            get { return currentFollowerValueX; }
            set
            {
                currentFollowerValueX = value;

                //DoubleAnimation da = AnimationFactory.GetAnimation(value, AnimationMode.Fast);// AnimationFactory.FromString(themeManager.CurrentTheme.IndicatorSettings.AnimationMode));
                //this.Window.cursorFollower.BeginAnimation(Canvas.LeftProperty, da);
                Canvas.SetLeft(this.Window.cursorFollower, value);

                OnPropertyChanged("CurrentFollowerValueX");
            }
        }

        double currentFollowerValueY = 0;
        public double CurrentFollowerValueY
        {
            get { return currentFollowerValueY; }
            set
            {
                currentFollowerValueY = value;

                //DoubleAnimation da = AnimationFactory.GetAnimation(value, AnimationMode.Fast); // AnimationFactory.FromString(themeManager.CurrentTheme.IndicatorSettings.AnimationMode));
                //this.Window.cursorFollower.BeginAnimation(Canvas.TopProperty, da);
                Canvas.SetTop(this.Window.cursorFollower, value);

                OnPropertyChanged("CurrentFollowerValueY");
            }
        }

        internal void MousePosition(System.Drawing.Point position)
        {
            // fix Y=-1 issue
            position.X = Math.Abs(position.X);
            position.Y = Math.Abs(position.Y);


            bool newIsActive = false;
            if(Screen.Bounds.Contains(position) 
                || (
                position.Y >= Screen.Bounds.Top && position.Y <= Screen.Bounds.Bottom 
                &&
                position.X >= Screen.Bounds.Left && position.X <= Screen.Bounds.Right
                ))
            {
                newIsActive = true;
            } else
            {
                newIsActive = false;
                HideBox(true);
            }
            //Console.WriteLine($"{Screen.DeviceName} = {position.X},{position.Y} in {Screen.Bounds} = {newIsActive}");

            if (newIsActive != isActive)
            {
                isActive = newIsActive;
                BarBrush = isActive ? activeBrush : inactiveBrush;
            }

            double ivalueX = (double)position.X - (double)Screen.Bounds.X - (this.Window.cursorIndicator.ActualWidth / 2);
            if(CurrentIndicatorValueX != ivalueX)
                CurrentIndicatorValueX = ivalueX;

            double fvalueX = (double)position.X - (double)Screen.Bounds.X - (this.Window.cursorFollower.Width / 2);
            if (CurrentFollowerValueX != fvalueX)
                CurrentFollowerValueX = fvalueX;
            double fvalueY = (double)position.Y - (double)Screen.Bounds.Y - (this.Window.cursorFollower.Height / 2);
            if (CurrentFollowerValueY != fvalueY)
                CurrentFollowerValueY = fvalueY;
        }

        DateTime heartbeatTime = DateTime.MinValue;
        internal void CursorHeartbeat()
        {
            if (DateTime.Now.Subtract(heartbeatTime).TotalMilliseconds < 3000) return;
            heartbeatTime = DateTime.Now;
            double newvalue = (this.Window.cursorFollower.Width / 2) * 0.8;
            //this.Window.cursorFollower.BeginAnimation(Shape.StrokeThicknessProperty, null);
            //this.Window.cursorFollower.StrokeThickness = 0;
            DoubleAnimation da = AnimationFactory.GetAnimation(newvalue, AnimationMode.Fast);
            da.RepeatBehavior = new RepeatBehavior(5.0);
            da.AutoReverse = true;
            da.FillBehavior = FillBehavior.HoldEnd;
            this.Window.cursorFollower.BeginAnimation(Shape.StrokeThicknessProperty, da);
        }

        internal void UpdateView(SmartBarView view)
        {
            this.view = view;
            ThemeChanged();
        }

        DateTime lastArrangeWindow = DateTime.MinValue;

        internal void TickTimer()
        {
            if(showBoxTime != DateTime.MaxValue && DateTime.Now.Subtract(showBoxTime).TotalMilliseconds >= 2000)
            {
                //Console.WriteLine("SHOWBOX TIMER showBoxTime");
                ShowBox(true);
            }
            if (hideBoxTime != DateTime.MaxValue && DateTime.Now.Subtract(hideBoxTime).TotalMilliseconds >= 1000)
            {
                //Console.WriteLine("HIDe By TIME");
                HideBox(true);
            }

            // fix issue with box visible but somewhere outside the screen (after very fast mouse shaking)
            // hide box after
            if (IsBoxVisible && DateTime.Now.Subtract(lastMouseMove).TotalMilliseconds >= 5000)
            {
                //Console.WriteLine("HIDe TIEMOUT");
                HideBox(true);
            }

            // make sure that the windows/smartbars are on their screens (sometimes they can jump on some events like lockscreen, monitor turnon/off etc.)
            if (DateTime.Now.Subtract(lastArrangeWindow).TotalMilliseconds >= 3000)
            {
                lastArrangeWindow = DateTime.Now;
                ArrangeWindow();
            }
        }

        bool barVisibility = true;
        public void VisibilityChange(bool show)
        {
            if (IsBoxVisible) return; // do not hide when box is visible

            if (show && barVisibility || !show && !barVisibility) return;

            DoubleAnimation da = null;
            if (show)
            {
                barVisibility = true;
                da = AnimationFactory.GetAnimation(0, AnimationMode.Fast);
            }
            else
            {
                barVisibility = false;
                da = AnimationFactory.GetAnimation(BarHeight * -2, AnimationMode.Fast);
            }
            this.Window.BeginAnimation(Canvas.TopProperty, da);
        }

        internal void SetHotKeyHelper(HotKeyHelper hotkeyHelper)
        {
            this.hotkeyHelper = hotkeyHelper;
        }
    }
}
