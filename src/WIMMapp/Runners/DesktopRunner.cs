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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using WIMMapp.Configuration;
using WIMMapp.i18n;
using WIMMapp.Interfaces;
using WIMMapp.Models.Application;
using WIMMapp.Models.Configuration;
using WIMMapp.Themes;
using WIMMapp.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace WIMMapp.Runners
{
    internal class DesktopRunner : Interfaces.IRunner, IDisposable
    {
        List<SmartBarView> smartBars;
        List<System.Windows.Forms.Screen> allScreens;
        DispatcherTimer timer;
        DateTime lastScreensChangeDetection = DateTime.MinValue;
        VirtualDesktopManager virtualDesktopManager;
        Config config;
        MouseGestureDetector mouseGestureDetector;
        DateTime lastMousePositionChanged = DateTime.MinValue;
        Point lastMousePosition = new Point(0, 0);
        DateTime lastShakingDetected = DateTime.MinValue;
        ThemeManager themeManager;
        HotKeyHelper hotkeyHelper;
        bool isRunning = false;
        int hotkeyId = -1;


        public DesktopRunner() 
        {
            smartBars = new List<SmartBarView>();
            allScreens = new List<System.Windows.Forms.Screen>();
            virtualDesktopManager = new VirtualDesktopManager();

            timer = new DispatcherTimer() { 
                Interval = TimeSpan.FromMilliseconds(1),
            };
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            if (now.Subtract(lastScreensChangeDetection).TotalMilliseconds > 3000)
            {
                lastScreensChangeDetection = now;
                DetectScreensChange();
            }

            GrabMousePosition();
            now = DateTime.Now;

            if ((config.Settings.HideAfter != 0 && now.Subtract(lastMousePositionChanged).TotalSeconds > config.Settings.HideAfter) 
                || (config.Settings.ShowOnlyAfterMouseShaking && now.Subtract(lastShakingDetected).TotalSeconds > config.Settings.HideAfter))
            {
                HideSmartBars();
            }

            TickTimers();
        }

        private void TickTimers()
        {
            foreach (var smartBar in smartBars)
            {
                smartBar.TickTimer();
            }
        }

        bool smartBarsHidden = false;
        private void HideSmartBars()
        {
            smartBarsHidden = true;
            //Console.WriteLine("HIDE");
            // update smartbars
            foreach (var smartBar in smartBars)
            {
                smartBar.VisibilityChange(false);
            }
        }
        private void UnHideSmartBars()
        {
            lastMousePositionChanged = DateTime.Now;
            //Console.WriteLine("SHOW");
            smartBarsHidden = false;
            // update smartbars
            foreach (var smartBar in smartBars)
            {
                smartBar.VisibilityChange(true);
            }
        }



        private void GrabMousePosition()
        {
            var pos = System.Windows.Forms.Control.MousePosition;
            //Console.WriteLine($"X={pos.X}, Y={pos.Y}");

            DateTime now = DateTime.Now;
            if (lastMousePosition.X != pos.X || lastMousePosition.Y != pos.Y)
            {
                lastMousePosition.X = pos.X;
                lastMousePosition.Y = pos.Y;
                lastMousePositionChanged = now;
            }
            {
                // update smartbars, if hidden use current loop
                foreach (var smartBar in smartBars)
                {
                    if(smartBarsHidden)
                    {
                        //Console.WriteLine("SHOW 2");
                        smartBar.VisibilityChange(true);
                    }

                    // DPI correction
                    //var dpi = VisualTreeHelper.GetDpi(smartBar);
                    //pos.X = (int)Math.Round((double)pos.X / dpi.DpiScaleX);
                    //pos.Y = (int)Math.Round((double)pos.Y / dpi.DpiScaleY);

                    smartBar.MousePosition(pos);
                }
                smartBarsHidden = false;
            }

            mouseGestureDetector?.CollectData(now, pos);
            if (mouseGestureDetector?.AnalyzeGesture() == MouseGestures.Shaking)
            {
                lastShakingDetected = now;
                // do something
                //Console.WriteLine("SHAKING DTECTED!");
                foreach (var smartBar in smartBars)
                {
                     smartBar.CursorHeartbeat();
                }
            }
            else
            {
                //Console.WriteLine("NONE");
            }
        }

        private void DetectScreensChange()
        {
            var newScreens = System.Windows.Forms.Screen.AllScreens;
            bool changeDetected = allScreens.Count != newScreens.Length;
            if (!changeDetected)
            {
                foreach (var newScreen in newScreens)
                {
                    bool found = false;
                    foreach (var oldScreen in allScreens)
                    {
                        if (oldScreen.DeviceName == newScreen.DeviceName && oldScreen.Bounds == newScreen.Bounds)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        changeDetected = true;
                        break;
                    }
                }
            }
            if (changeDetected)
            {
                Console.WriteLine("Screen(s) changed. Restarting...");
                Restart();
                return;
            }
        }

        public void Dispose()
        {
            Stop();
        }

        private void CloseAllSmartBars()
        {
            foreach (var smartBar in smartBars)
            {
                smartBar.ForceClose();
            }
            smartBars.Clear();
        }

        private void ThemeManager_OnThemeChanged(Theme newTheme)
        {
            foreach (var smartBar in smartBars)
            {
                smartBar.Model.ThemeChanged();
            }
        }



        public void Run() 
        {
            config = new Config();
            if (config.Settings.GlowMouseOnShaking)
            {
                mouseGestureDetector = new MouseGestureDetector((double)config.Settings.MouseShakeDetectionSensitivity / 100.0d);
            }

            themeManager = new ThemeManager();
            themeManager.OnThemeChanged += ThemeManager_OnThemeChanged;


            var screens = System.Windows.Forms.Screen.AllScreens;
            allScreens.Clear();
            foreach (var screen in screens)
            {
                var model = new ViewModels.SmartBarViewModel(config, screen, themeManager, this)
                {
                };
                var view = new SmartBarView(model);
                model.UpdateView(view);
                smartBars.Add(view);
                allScreens.Add(screen);
                view.Show();
                if (hotkeyHelper == null)
                {
                    hotkeyHelper = new HotKeyHelper(view);
                    model.SetHotKeyHelper(hotkeyHelper);
                    SetHotKey(config.Settings.HotKey);
                }

                // handle Virtual Desktops
                if (config.Settings.ShowOnVirtualDesktops && !virtualDesktopManager.IsWindowPinned(view.Handle))
                {
                     virtualDesktopManager.PinWindow(view.Handle);
                }
            }

            timer.Start();
            
            isRunning = true;
        }

        public void OnHotKey(int id)
        {
            if (id != hotkeyId) return;

            UnHideSmartBars();
            foreach (var smartBar in smartBars)
            {
                smartBar.CursorHeartbeat();
            }
        }

        public void Stop()
        {
            if (!isRunning) return;
            hotkeyHelper.ClearHotKeys();
            hotkeyHelper = null;
            CloseAllSmartBars();
            allScreens.Clear();
            timer.Stop();
            themeManager = null;
            mouseGestureDetector = null;
            config = null;
            isRunning = false;
        }

        public void Restart()
        {
            Stop();
            Run();
        }

        public bool HandleException(Exception exception)
        {
            if (config.Settings.ShowErrors)
            {
                var appInfo = new AppInformation();

                if (smartBars.Count > 0)
                {
                    System.Windows.MessageBox.Show((System.Windows.Window)smartBars.First(), $"Exception occured:\n{exception}", appInfo.Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    System.Windows.MessageBox.Show($"Exception occured:\n{exception}", appInfo.Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // TODO: log errors depends on LogLevel
            }
            return true;
        }

        public void SetHotKey(string hotKey)
        {
            hotkeyHelper.ClearHotKeys();
            hotkeyId = hotkeyHelper.AddHotKey(hotKey, OnHotKey);
        }
    }
}
