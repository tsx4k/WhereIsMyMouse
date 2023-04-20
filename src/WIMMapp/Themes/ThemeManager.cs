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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WIMMapp.Models.Configuration;

namespace WIMMapp.Themes
{
    public class ThemeManager
    {
        public delegate void ThemeChanged(Models.Configuration.Theme newTheme);
        public event ThemeChanged OnThemeChanged;


        private Models.Configuration.Theme currentTheme = DefaultTheme;
        public Models.Configuration.Theme CurrentTheme { get { return currentTheme; } set { currentTheme = value; OnThemeChanged?.Invoke(value); } }
        public ObservableCollection<Theme> Themes { get; set; } = new ObservableCollection<Theme>();

        public ThemeManager() 
        {
            PrepareDefaultThemes();
        }

        private void PrepareDefaultThemes()
        {
            Themes.Add(LightLimeTheme);
            Themes.Add(DarkLimeTheme);
            Themes.Add(LightOrangeTheme);
            Themes.Add(DarkOrangeTheme);
            Themes.Add(LightRedTheme);
            Themes.Add(DarkRedTheme);
            Themes.Add(LightBlueTheme);
            Themes.Add(DarkBlueTheme);
            Themes.Add(LightVioletTheme);
            Themes.Add(DarkVioletTheme);
            Themes.Add(WhiteTheme);
            Themes.Add(BlackTheme);
        }

        public static Theme DefaultTheme => DarkVioletTheme;

        public static Theme LightLimeTheme =>
            new Models.Configuration.Theme()
            {
                ID = "LIGHTLIME",
                Name = i18n.Texts.Get("THEMES.LIGHTLIME.NAME"),
                Description = i18n.Texts.Get("THEMES.LIGHTLIME.DESCRIPTION"),
                IndicatorSettings = new Models.Configuration.Indicator()
                {
                    Visible = true,
                    AnimationMode = AnimationMode.Bounce.ToString(),
                    Color = "#00ff0f",
                    Width = 100,
                    Image = null
                },
                SmartBarSettings = new Models.Configuration.SmartBar()
                {
                    ActiveColor = "#80df8f",
                    ActiveOpacity = 80,
                    InactiveColor = "#80df8f",
                    InactiveOpacity = 20,
                    Height = 4,
                    Image = null,
                    TextColor = "#107f1f"
                }
            };

        public static Theme DarkLimeTheme =>
            new Models.Configuration.Theme()
            {
                ID = "DARKLIME",
                Name = i18n.Texts.Get("THEMES.DARKLIME.NAME", "DarkLime"),
                Description = i18n.Texts.Get("THEMES.DARKLIME.DESCRIPTION"),
                IndicatorSettings = new Models.Configuration.Indicator()
                {
                    Visible = true,
                    AnimationMode = AnimationMode.Bounce.ToString(),
                    Color = "#00ff0f",
                    Width = 100,
                    Image = null
                },
                SmartBarSettings = new Models.Configuration.SmartBar()
                {
                    ActiveColor = "#004f0f",
                    ActiveOpacity = 80,
                    InactiveColor = "#004f0f",
                    InactiveOpacity = 20,
                    Height = 4,
                    Image = null,
                    TextColor = "#afffaf"
                }
            };


        public static Theme LightOrangeTheme =>
            new Models.Configuration.Theme()
            {
                ID = "LIGHTORANGE",
                Name = i18n.Texts.Get("THEMES.LIGHTORANGE.NAME"),
                Description = i18n.Texts.Get("THEMES.LIGHTORANGE.DESCRIPTION"),
                IndicatorSettings = new Models.Configuration.Indicator()
                {
                    Visible = true,
                    AnimationMode = AnimationMode.Bounce.ToString(),
                    Color = "#ffb000",
                    Width = 100,
                    Image = null
                },
                SmartBarSettings = new Models.Configuration.SmartBar()
                {
                    ActiveColor = "#d0d0d0",
                    ActiveOpacity = 80,
                    InactiveColor = "#f0f0f0",
                    InactiveOpacity = 20,
                    Height = 4,
                    Image = null,
                    TextColor = "#ffffff"
                }
            };

        public static Theme DarkOrangeTheme =>
            new Models.Configuration.Theme()
            {
                ID = "DARKORANGE",
                Name = i18n.Texts.Get("THEMES.DARKORANGE.NAME"),
                Description = i18n.Texts.Get("THEMES.DARKORANGE.DESCRIPTION"),
                IndicatorSettings = new Models.Configuration.Indicator()
                {
                    Visible = true,
                    AnimationMode = AnimationMode.Bounce.ToString(),
                    Color = "#ffc000",
                    Width = 100,
                    Image = null
                },
                SmartBarSettings = new Models.Configuration.SmartBar()
                {
                    ActiveColor = "#af6f00",
                    ActiveOpacity = 80,
                    InactiveColor = "#af6f00",
                    InactiveOpacity = 20,
                    Height = 4,
                    Image = null,
                    TextColor = "#ffffff"
                }
            };


        public static Theme LightRedTheme =>
            new Models.Configuration.Theme()
            {
                ID = "LIGHTRED",
                Name = i18n.Texts.Get("THEMES.LIGHTRED.NAME"),
                Description = i18n.Texts.Get("THEMES.LIGHTRED.DESCRIPTION"),
                IndicatorSettings = new Models.Configuration.Indicator()
                {
                    Visible = true,
                    AnimationMode = AnimationMode.Bounce.ToString(),
                    Color = "#ff0000",
                    Width = 100,
                    Image = null
                },
                SmartBarSettings = new Models.Configuration.SmartBar()
                {
                    ActiveColor = "#d0d0d0",
                    ActiveOpacity = 80,
                    InactiveColor = "#f0f0f0",
                    InactiveOpacity = 20,
                    Height = 4,
                    Image = null,
                    TextColor = "#ffffff"
                }
            };

        public static Theme DarkRedTheme =>
            new Models.Configuration.Theme()
            {
                ID = "DARKRED",
                Name = i18n.Texts.Get("THEMES.DARKRED.NAME"),
                Description = i18n.Texts.Get("THEMES.DARKRED.DESCRIPTION"),
                IndicatorSettings = new Models.Configuration.Indicator()
                {
                    Visible = true,
                    AnimationMode = AnimationMode.Bounce.ToString(),
                    Color = "#ff0000",
                    Width = 100,
                    Image = null
                },
                SmartBarSettings = new Models.Configuration.SmartBar()
                {
                    ActiveColor = "#6f0000",
                    ActiveOpacity = 80,
                    InactiveColor = "#6f0000",
                    InactiveOpacity = 20,
                    Height = 4,
                    Image = null,
                    TextColor = "#ffffff"
                }
            };

        public static Theme LightBlueTheme =>
            new Models.Configuration.Theme()
            {
                ID = "LIGHTBLUE",
                Name = i18n.Texts.Get("THEMES.LIGHTBLUE.NAME"),
                Description = i18n.Texts.Get("THEMES.LIGHTBLUE.DESCRIPTION"),
                IndicatorSettings = new Models.Configuration.Indicator()
                {
                    Visible = true,
                    AnimationMode = AnimationMode.Bounce.ToString(),
                    Color = "#00a0ff",
                    Width = 100,
                    Image = null
                },
                SmartBarSettings = new Models.Configuration.SmartBar()
                {
                    ActiveColor = "#d0d0d0",
                    ActiveOpacity = 80,
                    InactiveColor = "#f0f0f0",
                    InactiveOpacity = 20,
                    Height = 4,
                    Image = null,
                    TextColor = "#ffffff"
                }
            };

        public static Theme DarkBlueTheme =>
            new Models.Configuration.Theme()
            {
                ID = "DARKBLUE",
                Name = i18n.Texts.Get("THEMES.DARKBLUE.NAME"),
                Description = i18n.Texts.Get("THEMES.DARKBLUE.DESCRIPTION"),
                IndicatorSettings = new Models.Configuration.Indicator()
                {
                    Visible = true,
                    AnimationMode = AnimationMode.Bounce.ToString(),
                    Color = "#00a0ff",
                    Width = 100,
                    Image = null
                },
                SmartBarSettings = new Models.Configuration.SmartBar()
                {
                    ActiveColor = "#00206f",
                    ActiveOpacity = 80,
                    InactiveColor = "#00206f",
                    InactiveOpacity = 20,
                    Height = 4,
                    Image = null,
                    TextColor = "#ffffff"
                }
            };

        public static Theme LightVioletTheme =>
                new Models.Configuration.Theme()
                {
                    ID = "LIGHTVIOLET",
                    Name = i18n.Texts.Get("THEMES.LIGHTVIOLET.NAME"),
                    Description = i18n.Texts.Get("THEMES.LIGHTVIOLET.DESCRIPTION"),
                    IndicatorSettings = new Models.Configuration.Indicator()
                    {
                        Visible = true,
                        AnimationMode = AnimationMode.Bounce.ToString(),
                        Color = "#ff00e8",
                        Width = 100,
                        Image = null
                    },
                    SmartBarSettings = new Models.Configuration.SmartBar()
                    {
                        ActiveColor = "#d0d0d0",
                        ActiveOpacity = 80,
                        InactiveColor = "#f0f0f0",
                        InactiveOpacity = 20,
                        Height = 4,
                        Image = null,
                        TextColor = "#ffffff"
                    }
                };

        public static Theme DarkVioletTheme =>
            new Models.Configuration.Theme()
            {
                ID = "DARKVIOLET",
                Name = i18n.Texts.Get("THEMES.DARKVIOLET.NAME"),
                Description = i18n.Texts.Get("THEMES.DARKVIOLET.DESCRIPTION"),
                IndicatorSettings = new Models.Configuration.Indicator()
                {
                    Visible = true,
                    AnimationMode = AnimationMode.Bounce.ToString(),
                    Color = "#ff00e8",
                    Width = 100,
                    Image = null
                },
                SmartBarSettings = new Models.Configuration.SmartBar()
                {
                    ActiveColor = "#62155b",
                    ActiveOpacity = 80,
                    InactiveColor = "#62155b",
                    InactiveOpacity = 20,
                    Height = 4,
                    Image = null,
                    TextColor = "#ffffff"
                }
            };


        public static Theme WhiteTheme =>
            new Models.Configuration.Theme()
            {
                ID = "WHITE",
                Name = i18n.Texts.Get("THEMES.WHITE.NAME"),
                Description = i18n.Texts.Get("THEMES.WHITE.DESCRIPTION"),
                IndicatorSettings = new Models.Configuration.Indicator()
                {
                    Visible = true,
                    AnimationMode = AnimationMode.Bounce.ToString(),
                    Color = "#ffffff",
                    Width = 100,
                    Image = null
                },
                SmartBarSettings = new Models.Configuration.SmartBar()
                {
                    ActiveColor = "#ffffff",
                    ActiveOpacity = 80,
                    InactiveColor = "#222222",
                    InactiveOpacity = 20,
                    Height = 4,
                    Image = null,
                    TextColor = "#222222"
                }
            };

        public static Theme BlackTheme =>
            new Models.Configuration.Theme()
            {
                ID = "BLACK",
                Name = i18n.Texts.Get("THEMES.BLACK.NAME"),
                Description = i18n.Texts.Get("THEMES.BLACK.DESCRIPTION"),
                IndicatorSettings = new Models.Configuration.Indicator()
                {
                    Visible = true,
                    AnimationMode = AnimationMode.Bounce.ToString(),
                    Color = "#000000",
                    Width = 100,
                    Image = null
                },
                SmartBarSettings = new Models.Configuration.SmartBar()
                {
                    ActiveColor = "#444444",
                    ActiveOpacity = 80,
                    InactiveColor = "#444444",
                    InactiveOpacity = 20,
                    Height = 4,
                    Image = null,
                    TextColor = "#dddddd"
                }
            };

    }
}
