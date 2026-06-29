using System.Drawing;
using System.Windows.Forms;

namespace CreamInstaller.Utility;

internal static class LogTextBox
{
    internal static Color Background => ThemeManager.IsDark ? ColorTranslator.FromHtml("#1E1E1E") : SystemColors.Window;
    internal static Color Operation => ThemeManager.IsDark ? ColorTranslator.FromHtml("#E0E0E0") : ColorTranslator.FromHtml("#222222");
    internal static Color Action => ThemeManager.IsDark ? ColorTranslator.FromHtml("#87CEFA") : ColorTranslator.FromHtml("#0047AB");
    internal static Color Success => ThemeManager.IsDark ? ColorTranslator.FromHtml("#98FB98") : ColorTranslator.FromHtml("#006400");
    internal static Color Cleanup => ThemeManager.IsDark ? ColorTranslator.FromHtml("#ADFF2F") : ColorTranslator.FromHtml("#4A6B00");
    internal static Color Failure => ThemeManager.IsDark ? ColorTranslator.FromHtml("#FF6B6B") : ColorTranslator.FromHtml("#8B0000");
    internal static Color Warning => ThemeManager.IsDark ? ColorTranslator.FromHtml("#FFD700") : ColorTranslator.FromHtml("#8B4500");
    internal static Color Error => ThemeManager.IsDark ? ColorTranslator.FromHtml("#FFA500") : ColorTranslator.FromHtml("#B22222");

    internal static void AppendText(this RichTextBox textBox, string text, Color color, bool scroll = false)
    {
        textBox.SelectionStart = textBox.TextLength;
        textBox.SelectionLength = 0;
        textBox.SelectionColor = color;
        if (scroll)
            textBox.ScrollToCaret();
        textBox.AppendText(text);
        if (scroll)
            textBox.ScrollToCaret();
        textBox.SelectionColor = textBox.ForeColor;
        textBox.Invalidate();
    }
}