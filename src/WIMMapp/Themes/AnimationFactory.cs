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
using System.Windows;
using System.Windows.Media.Animation;

namespace WIMMapp.Themes
{
    public enum AnimationMode
    {
        None = 0,
        Bounce,
        Slow,
        Fast
    }

    public class AnimationFactory
    {
        public static IEasingFunction GetEasing(AnimationMode mode)
        {
            switch (mode)
            {
                case AnimationMode.None:
                    return null;
                default:
                case AnimationMode.Bounce:
                    return new ElasticEase() { EasingMode = EasingMode.EaseOut, Oscillations = 10, Springiness = 10 };
                case AnimationMode.Slow:
                    //return new QuadraticEase() { EasingMode = EasingMode.EaseOut };
                    return new CircleEase() { EasingMode = EasingMode.EaseOut };
                case AnimationMode.Fast:
                    return new CircleEase() { EasingMode = EasingMode.EaseOut };
            }
        }

        public static AnimationMode FromString(string mode)
        {
            try
            {
                return (AnimationMode)Enum.Parse(typeof(AnimationMode), mode);
            } catch { }
            return AnimationMode.None;
        }

        public static DoubleAnimation GetAnimation(double to, AnimationMode mode)
        {
            Duration duration = TimeSpan.FromSeconds(1);
            switch (mode)
            {
                case AnimationMode.None:
                    return null;
                default:
                case AnimationMode.Bounce:
                    duration = TimeSpan.FromSeconds(1.2);
                    break;
                case AnimationMode.Slow:
                    duration = TimeSpan.FromSeconds(2.0);
                    break;
                case AnimationMode.Fast:
                    duration = TimeSpan.FromSeconds(0.2);
                    break;
            }

            DoubleAnimation da = new DoubleAnimation()
            {
                FillBehavior = FillBehavior.HoldEnd,
                To = to,
                EasingFunction = GetEasing(mode),
                Duration = duration
            };
            Timeline.SetDesiredFrameRate(da, 60);
            return da;
        }


    }
}
