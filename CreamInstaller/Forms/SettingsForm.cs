using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using CreamInstaller.Components;
using CreamInstaller.Platforms.Steam;
using CreamInstaller.Utility;

namespace CreamInstaller.Forms;

internal sealed partial class SettingsForm : CustomForm
{
    private bool wasDarkModeEnabled;
    private bool wasSortByName;

    private SettingsForm()
    {
        InitializeComponent();
        Text = Locale.Get("Settings");
    }

    internal static void Show(Form owner)
    {
        using SettingsForm form = new();
        form.Owner = owner;
        form.StartPosition = FormStartPosition.CenterParent;
        form.LoadSettings();
        form.ShowDialog(owner);
    }

    private void LoadSettings()
    {
        darkModeCheckBox.Checked = Program.DarkModeEnabled;
        blockedGamesCheckBox.Checked = Program.BlockProtectedGames;
        sortByNameCheckBox.Checked = Program.SortByName;
        preReleaseCheckBox.Checked = Program.CheckPreReleases;
        defaultAppStatusComboBox.SelectedIndex = (int)Program.DefaultAppStatus;
        wasDarkModeEnabled = Program.DarkModeEnabled;
        wasSortByName = Program.SortByName;
    }

    private void OnSaveClick(object sender, EventArgs e)
    {
        Program.DarkModeEnabled = darkModeCheckBox.Checked;
        Program.BlockProtectedGames = blockedGamesCheckBox.Checked;
        Program.SortByName = sortByNameCheckBox.Checked;
        Program.CheckPreReleases = preReleaseCheckBox.Checked;
        Program.DefaultAppStatus = (DefaultAppStatus)defaultAppStatusComboBox.SelectedIndex;

        ProgramData.SaveSettings(Program.AppSettings);

        if (wasDarkModeEnabled != darkModeCheckBox.Checked)
        {
            ThemeManager.ApplyToAllOpenForms();
            if (DebugForm.IsOpen)
                ThemeManager.Apply(DebugForm.Current);
        }

        if (wasSortByName != sortByNameCheckBox.Checked)
            MainForm.Current?.UpdateSortOrder(sortByNameCheckBox.Checked);

        DialogResult = DialogResult.OK;
        Close();
    }

    private void OnCancelClick(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void OnClearCacheClick(object sender, EventArgs e)
    {
        using DialogForm confirm = new(this);
        if (confirm.Show(SystemIcons.Warning,
                Locale.Get("ClearCacheDescription"),
                acceptButtonText: Locale.Get("ClearCache"), cancelButtonText: Locale.Get("Cancel"), customFormText: Locale.Get("ClearCachedData")) != DialogResult.OK)
            return;

        string cachePath = ProgramData.DirectoryPath + @"\Cache";
        if (Directory.Exists(cachePath))
        {
            foreach (string file in Directory.GetFiles(cachePath))
            {
                if (Path.GetFileName(file).Equals("settings.json", StringComparison.OrdinalIgnoreCase))
                    continue;
                try { File.Delete(file); } catch { /* skip locked files */ }
            }
            foreach (string dir in Directory.GetDirectories(cachePath))
            {
                try { Directory.Delete(dir, true); } catch { /* skip locked dirs */ }
            }
        }

        ProgramData.CacheCleared = true;
        ProgramData.SaveSettings(Program.AppSettings);
        DialogResult = DialogResult.OK;
    }

    private async void OnReconfigureSteamCMDClick(object sender, EventArgs e)
    {
        using DialogForm confirm = new(this);
        if (confirm.Show(SystemIcons.Warning,
                Locale.Get("ReconfigureSteamCMDDescription"),
                acceptButtonText: Locale.Get("Reconfigure"), cancelButtonText: Locale.Get("Cancel"), customFormText: Locale.Get("ReconfigureSteamCMD")) != DialogResult.OK)
            return;

        reconfigureSteamCMDButton.Enabled = false;
        reconfigureSteamCMDButton.Text = Locale.Get("Reconfiguring");

        string steamCmdPath = ProgramData.DirectoryPath + @"\SteamCMD";
        await Task.Run(() =>
        {
            try { Directory.Delete(steamCmdPath, true); } catch { /* directory may not exist */ }
        });

        Progress<int> progress = new();
        await SteamCMD.Setup(progress);

        Color originalColor = reconfigureSteamCMDButton.ForeColor;
        reconfigureSteamCMDButton.Text = Locale.Get("Complete");
        reconfigureSteamCMDButton.ForeColor = Color.Green;
        await Task.Delay(2000);
        reconfigureSteamCMDButton.Text = Locale.Get("ReconfigureSteamCMD");
        reconfigureSteamCMDButton.ForeColor = originalColor;
        reconfigureSteamCMDButton.Enabled = true;
    }

    private void OnOpenLogDirClick(object sender, EventArgs e) => Diagnostics.OpenDirectoryInFileExplorer(ProgramData.LogsPath);

    private void OnCheckForUpdatesClick(object sender, EventArgs e) => UpdateForm.ShowUpdateCheck(this);
}
