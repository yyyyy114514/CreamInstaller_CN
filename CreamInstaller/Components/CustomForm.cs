using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using CreamInstaller.Forms;
using CreamInstaller.Utility;

namespace CreamInstaller.Components;

internal class CustomForm : Form
{
    internal CustomForm()
    {
        Icon = Properties.Resources.Icon;
        KeyPreview = true;
        KeyPress += OnKeyPress;
        ResizeRedraw = true;
        HelpButton = true;
        HelpButtonClicked += OnHelpButtonClicked;
    }

    internal CustomForm(IWin32Window owner) : this()
    {
        if (owner is not Form form)
            return;
        Owner = form;
        InheritLocation(form);
        SizeChanged += (_, _) => InheritLocation(form);
        form.Activated += OnActivation;
        FormClosing += (_, _) => form.Activated -= OnActivation;
        TopLevel = true;
    }

    protected override CreateParams CreateParams // Double buffering for all controls
    {
        get
        {
            CreateParams handleParam = base.CreateParams;
            handleParam.ExStyle |= 0x02; // WS_EX_COMPOSITED       
            return handleParam;
        }
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        ThemeManager.Apply(this); // apply current theme (initial or toggled)
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        ThemeManager.Apply(this); // ensure late-added controls also themed
    }

    private void OnHelpButtonClicked(object sender, EventArgs args)
    {
        using DialogForm helpDialog = new(this);
        helpDialog.HelpButton = false;
        string repository = $"https://github.com/{Program.RepositoryOwner}/{Program.RepositoryName}";
        _ = helpDialog.Show(SystemIcons.Information,
            Locale.Format("HelpText", repository));
    }

    private void OnActivation(object sender, EventArgs args) => Activate();

    internal void BringToFrontWithoutActivation()
    {
        bool topMost = TopMost;
        NativeImports.SetWindowPos(Handle, NativeImports.HWND_TOPMOST, 0, 0, 0, 0,
            NativeImports.SWP_NOACTIVATE | NativeImports.SWP_SHOWWINDOW | NativeImports.SWP_NOMOVE |
            NativeImports.SWP_NOSIZE);
        if (!topMost)
            NativeImports.SetWindowPos(Handle, NativeImports.HWND_NOTOPMOST, 0, 0, 0, 0,
                NativeImports.SWP_NOACTIVATE | NativeImports.SWP_SHOWWINDOW | NativeImports.SWP_NOMOVE |
                NativeImports.SWP_NOSIZE);
    }

    internal void InheritLocation(Form fromForm)
    {
        if (fromForm is null)
            return;
        int X = fromForm.Location.X + fromForm.Size.Width / 2 - Size.Width / 2;
        int Y = fromForm.Location.Y + fromForm.Size.Height / 2 - Size.Height / 2;
        Location = new(X, Y);
    }

    private void OnKeyPress(object s, KeyPressEventArgs e)
    {
        if (e.KeyChar != 'S')
            return; // Shift + S
        UpdateBounds();
        Rectangle bounds = Bounds;
        using Bitmap bitmap = new(Size.Width - 14, Size.Height - 7);
        using Graphics graphics = Graphics.FromImage(bitmap);
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        using EncoderParameters encoding = new(1);
        using EncoderParameter encoderParam = new(Encoder.Quality, 100L);
        encoding.Param[0] = encoderParam;
        graphics.CopyFromScreen(new(bounds.Left + 7, bounds.Top), Point.Empty, new(Size.Width - 14, Size.Height - 7));
        Clipboard.SetImage(bitmap);
        e.Handled = true;
    }
}