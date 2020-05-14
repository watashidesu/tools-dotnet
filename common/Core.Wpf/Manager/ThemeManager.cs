using MaterialDesignThemes.Wpf;
using System;

namespace Core.Wpf.Manager
{
    public class ThemeManager
    {
        public static void ModifyTheme(Action<ITheme> modificationAction)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            modificationAction?.Invoke(theme);
            paletteHelper.SetTheme(theme);
        }
    }
}