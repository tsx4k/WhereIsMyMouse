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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIMMapp.Utils
{
    public enum MouseGestures
    {
        None = 0,
        Shaking,
    }

    public class MouseGestureDetector : IDisposable
    {
        const int CollectingTime = 2000; // in ms
        List<Tuple<DateTime, Point>> mouseData = new List<Tuple<DateTime, Point>>();

        private double sensitivity = 1.0;
        public MouseGestureDetector(double sensitivity)
        {
            this.sensitivity = sensitivity == 0 ? 0.1 : sensitivity;
        }

        public void Dispose()
        {
            mouseData.Clear();
            mouseData = null;
        }

        internal MouseGestures AnalyzeGesture()
        {
            var result = MouseGestures.None;

            double delta = 0, directionChanges = 0;
            bool directionA = false;

            Tuple<DateTime, Point> lastEntry = null;
            foreach(var entry in mouseData)
            {
                if(lastEntry == null)
                {
                    lastEntry = entry;
                    continue;
                }

                if (entry.Item2.X > lastEntry.Item2.X)
                {
                    if (!directionA) directionChanges++;
                    directionA = true;
                    delta += Math.Abs(entry.Item2.X - lastEntry.Item2.X);
                } else
                if (entry.Item2.X < lastEntry.Item2.X)
                {
                    if (directionA) directionChanges++;
                    directionA = false;
                    delta += Math.Abs(lastEntry.Item2.X - entry.Item2.X);
                }
                lastEntry = entry;
            }

            delta *= directionChanges;

            //Console.WriteLine($"DELTA: {delta} / {directionChanges}");
            if(delta * sensitivity > 80000.0d)
                result = MouseGestures.Shaking;

            return result;
        }

        internal void CollectData(DateTime now, Point pos)
        {
            // delete older entries
            mouseData.RemoveAll(x => now.Subtract(x.Item1).TotalMilliseconds > CollectingTime);
            // ad new one
            mouseData.Add(new Tuple<DateTime, Point>(now, pos));
        }
    }
}
