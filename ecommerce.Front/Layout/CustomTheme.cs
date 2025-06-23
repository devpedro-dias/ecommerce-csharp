using MudBlazor;

namespace ScreenSound.Web.Layout;

public static class CustomTheme
{
    public static MudTheme DarkTheme => new MudTheme()
    {
        PaletteDark = new PaletteDark()
        {
            Primary = "#0D47A1",
            Secondary = "#1565C0",
            Tertiary = "#90CAF9"
        }
    };
}
