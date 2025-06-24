using MudBlazor;

namespace ScreenSound.Web.Layout;

public static class CustomTheme
{
    // Opção 1: Oceano Profundo e Púrpura
    public static MudTheme DarkTheme => new MudTheme()
    {
        PaletteDark = new PaletteDark()
        {
            // Azul-Petróleo/Ciano Escuro para elementos primários (botões, cabeçalhos)
            Primary = "#00796B", // Um verde-azulado escuro
            // Um tom de púrpura vibrante para elementos secundários e interativos, criando contraste
            Secondary = "#7C4DFF",
            // Um verde-azulado mais claro para acentos, texto secundário ou itens selecionados
            Tertiary = "#80CBC4",

            // Cores base para o tema escuro
            Background = "#121212", // Padrão MudBlazor Dark
            AppbarBackground = "#181818", // Um pouco mais claro que o background para a barra superior
            Surface = "#1E1E1E", // Para cards, papers, e superfícies elevadas
            TextPrimary = "#E0E0E0", // Cor do texto principal
            TextSecondary = "#BDBDBD", // Cor do texto secundário
            ActionDefault = "#BDBDBD", // Cor padrão para ícones/ações
            ActionDisabled = "rgba(255,255,255, 0.26)",
            ActionDisabledBackground = "rgba(255,255,255, 0.12)",
            DrawerBackground = "#181818",
            DrawerText = "#E0E0E0",
            Divider = "#363636",
            DividerLight = "#2C2C2C",
            TableLines = "#363636",
            LinesDefault = "#2C2C2C",
            LinesInputs = "#BDBDBD",
            DarkLighten = "#252525",
            GrayDefault = "#BDBDBD",
            GrayLight = "#9E9E9E",
            Info = "#2196F3", // Cor para informações
            Success = "#4CAF50", // Cor para sucesso
            Warning = "#FFC107", // Cor para avisos
            Error = "#F44336", // Cor para erros
        }
    };
}