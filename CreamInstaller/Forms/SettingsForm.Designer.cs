using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CreamInstaller.Platforms.Steam;
using CreamInstaller.Utility;

namespace CreamInstaller.Forms;

partial class SettingsForm
{
    private IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components is not null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        SettingsToolTip = new ToolTip();
        appearanceGroup = new GroupBox();
        darkModeCheckBox = new CheckBox();
        gameManagementGroup = new GroupBox();
        blockedGamesCheckBox = new CheckBox();
        sortByNameCheckBox = new CheckBox();
        smokeApiGroup = new GroupBox();
        defaultAppStatusLabel = new Label();
        defaultAppStatusComboBox = new ComboBox();
        updatesGroup = new GroupBox();
        preReleaseCheckBox = new CheckBox();
        checkForUpdatesButton = new Button();
        maintenanceGroup = new GroupBox();
        clearCacheButton = new Button();
        reconfigureSteamCMDButton = new Button();
        saveButton = new Button();
        cancelButton = new Button();
        openLogDirButton = new Button();
        appearanceGroup.SuspendLayout();
        gameManagementGroup.SuspendLayout();
        smokeApiGroup.SuspendLayout();
        updatesGroup.SuspendLayout();
        maintenanceGroup.SuspendLayout();
        SuspendLayout();
        // 
        // settingsToolTip
        // 
        SettingsToolTip.AutoPopDelay = 8000;
        SettingsToolTip.InitialDelay = 500;
        SettingsToolTip.ReshowDelay = 100;
        // 
        // appearanceGroup
        // 
        appearanceGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        appearanceGroup.Controls.Add(darkModeCheckBox);
        appearanceGroup.Location = new Point(12, 12);
        appearanceGroup.Name = "appearanceGroup";
        appearanceGroup.Size = new Size(376, 50);
        appearanceGroup.TabIndex = 0;
        appearanceGroup.TabStop = false;
        appearanceGroup.Text = Locale.Get("Appearance");
        // 
        // darkModeCheckBox
        // 
        darkModeCheckBox.AutoSize = false;
        darkModeCheckBox.FlatStyle = FlatStyle.System;
        darkModeCheckBox.Location = new Point(12, 20);
        darkModeCheckBox.Name = "darkModeCheckBox";
        darkModeCheckBox.Size = new Size(160, 22);
        darkModeCheckBox.TabIndex = 0;
        darkModeCheckBox.Text = Locale.Get("EnableDarkMode");
        darkModeCheckBox.UseVisualStyleBackColor = true;
        SettingsToolTip.SetToolTip(darkModeCheckBox, Locale.Get("EnableDarkModeDescription"));
        // 
        // gameManagementGroup
        // 
        gameManagementGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gameManagementGroup.Controls.Add(blockedGamesCheckBox);
        gameManagementGroup.Controls.Add(sortByNameCheckBox);
        gameManagementGroup.Location = new Point(12, 72);
        gameManagementGroup.Name = "gameManagementGroup";
        gameManagementGroup.Size = new Size(376, 76);
        gameManagementGroup.TabIndex = 1;
        gameManagementGroup.TabStop = false;
        gameManagementGroup.Text = Locale.Get("GameManagement");
        // 
        // blockedGamesCheckBox
        // 
        blockedGamesCheckBox.AutoSize = false;
        blockedGamesCheckBox.FlatStyle = FlatStyle.System;
        blockedGamesCheckBox.Location = new Point(12, 22);
        blockedGamesCheckBox.Name = "blockedGamesCheckBox";
        blockedGamesCheckBox.Size = new Size(260, 22);
        blockedGamesCheckBox.TabIndex = 0;
        blockedGamesCheckBox.Text = Locale.Get("BlockProtectedGames");
        blockedGamesCheckBox.UseVisualStyleBackColor = true;
        SettingsToolTip.SetToolTip(blockedGamesCheckBox, Locale.Get("BlockProtectedGamesTooltip"));
        // 
        // sortByNameCheckBox
        // 
        sortByNameCheckBox.AutoSize = false;
        sortByNameCheckBox.FlatStyle = FlatStyle.System;
        sortByNameCheckBox.Location = new Point(12, 48);
        sortByNameCheckBox.Name = "sortByNameCheckBox";
        sortByNameCheckBox.Size = new Size(200, 22);
        sortByNameCheckBox.TabIndex = 1;
        sortByNameCheckBox.Text = Locale.Get("SortByName");
        sortByNameCheckBox.UseVisualStyleBackColor = true;
        SettingsToolTip.SetToolTip(sortByNameCheckBox, Locale.Get("SortByNameDescription"));
        // 
        // smokeApiGroup
        // 
        smokeApiGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        smokeApiGroup.Controls.Add(defaultAppStatusLabel);
        smokeApiGroup.Controls.Add(defaultAppStatusComboBox);
        smokeApiGroup.Location = new Point(12, 158);
        smokeApiGroup.Name = "smokeApiGroup";
        smokeApiGroup.Size = new Size(376, 55);
        smokeApiGroup.TabIndex = 2;
        smokeApiGroup.TabStop = false;
        smokeApiGroup.Text = Locale.Get("SmokeAPI");
        // 
        // defaultAppStatusLabel
        // 
        defaultAppStatusLabel.AutoSize = true;
        defaultAppStatusLabel.Location = new Point(12, 24);
        defaultAppStatusLabel.Name = "defaultAppStatusLabel";
        defaultAppStatusLabel.Size = new Size(160, 15);
        defaultAppStatusLabel.TabIndex = 0;
        defaultAppStatusLabel.Text = Locale.Get("DefaultAppStatusLabel");
        // 
        // defaultAppStatusComboBox
        // 
        defaultAppStatusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        defaultAppStatusComboBox.Items.AddRange(new object[] {
            Locale.Get("AppStatusUnlocked"),
            Locale.Get("AppStatusLocked"),
            Locale.Get("AppStatusOriginal")});
        defaultAppStatusComboBox.Location = new Point(268, 21);
        defaultAppStatusComboBox.Name = "defaultAppStatusComboBox";
        defaultAppStatusComboBox.Size = new Size(95, 23);
        defaultAppStatusComboBox.TabIndex = 1;
        SettingsToolTip.SetToolTip(defaultAppStatusComboBox, Locale.Get("AppStatusDescription"));
        // 
        // updatesGroup
        // 
        updatesGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        updatesGroup.Controls.Add(preReleaseCheckBox);
        updatesGroup.Controls.Add(checkForUpdatesButton);
        updatesGroup.Location = new Point(12, 223);
        updatesGroup.Name = "updatesGroup";
        updatesGroup.Size = new Size(376, 85);
        updatesGroup.TabIndex = 3;
        updatesGroup.TabStop = false;
        updatesGroup.Text = Locale.Get("Updates");
        // 
        // preReleaseCheckBox
        // 
        preReleaseCheckBox.AutoSize = false;
        preReleaseCheckBox.FlatStyle = FlatStyle.System;
        preReleaseCheckBox.Location = new Point(12, 20);
        preReleaseCheckBox.Name = "preReleaseCheckBox";
        preReleaseCheckBox.Size = new Size(340, 22);
        preReleaseCheckBox.TabIndex = 0;
        preReleaseCheckBox.Text = Locale.Get("CheckPreReleases");
        preReleaseCheckBox.UseVisualStyleBackColor = true;
        SettingsToolTip.SetToolTip(preReleaseCheckBox, Locale.Get("PreReleaseDescription"));
        // 
        // checkForUpdatesButton
        // 
        checkForUpdatesButton.AutoSize = true;
        checkForUpdatesButton.Location = new Point(12, 50);
        checkForUpdatesButton.Name = "checkForUpdatesButton";
        checkForUpdatesButton.Size = new Size(175, 25);
        checkForUpdatesButton.TabIndex = 1;
        checkForUpdatesButton.Text = Locale.Get("CheckForUpdates");
        checkForUpdatesButton.UseVisualStyleBackColor = true;
        checkForUpdatesButton.Click += OnCheckForUpdatesClick;
        SettingsToolTip.SetToolTip(checkForUpdatesButton, Locale.Get("CheckForUpdatesDescription"));
        // 
        // maintenanceGroup
        // 
        maintenanceGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        maintenanceGroup.Controls.Add(clearCacheButton);
        maintenanceGroup.Controls.Add(reconfigureSteamCMDButton);
        maintenanceGroup.Controls.Add(openLogDirButton);
        maintenanceGroup.Location = new Point(12, 318);
        maintenanceGroup.Name = "maintenanceGroup";
        maintenanceGroup.Size = new Size(376, 85);
        maintenanceGroup.TabIndex = 4;
        maintenanceGroup.TabStop = false;
        maintenanceGroup.Text = Locale.Get("Maintenance");
        // 
        // clearCacheButton
        // 
        clearCacheButton.AutoSize = true;
        clearCacheButton.Location = new Point(12, 20);
        clearCacheButton.Name = "clearCacheButton";
        clearCacheButton.Size = new Size(175, 25);
        clearCacheButton.TabIndex = 0;
        clearCacheButton.Text = Locale.Get("ClearCachedData");
        clearCacheButton.UseVisualStyleBackColor = true;
        clearCacheButton.Click += OnClearCacheClick;
        SettingsToolTip.SetToolTip(clearCacheButton, Locale.Get("ClearCacheTooltip"));
        // 
        // reconfigureSteamCMDButton
        // 
        reconfigureSteamCMDButton.AutoSize = true;
        reconfigureSteamCMDButton.Location = new Point(195, 20);
        reconfigureSteamCMDButton.Name = "reconfigureSteamCMDButton";
        reconfigureSteamCMDButton.Size = new Size(175, 25);
        reconfigureSteamCMDButton.TabIndex = 1;
        reconfigureSteamCMDButton.Text = Locale.Get("ReconfigureSteamCMD");
        reconfigureSteamCMDButton.UseVisualStyleBackColor = true;
        reconfigureSteamCMDButton.Click += OnReconfigureSteamCMDClick;
        SettingsToolTip.SetToolTip(reconfigureSteamCMDButton, Locale.Get("ReconfigureSteamCMDTooltip"));
        // 
        // openLogDirButton
        // 
        openLogDirButton.AutoSize = true;
        openLogDirButton.Location = new Point(12, 50);
        openLogDirButton.Name = "openLogDirButton";
        openLogDirButton.Size = new Size(175, 25);
        openLogDirButton.TabIndex = 2;
        openLogDirButton.Text = Locale.Get("OpenLogDirectory");
        openLogDirButton.UseVisualStyleBackColor = true;
        openLogDirButton.Click += OnOpenLogDirClick;
        SettingsToolTip.SetToolTip(openLogDirButton, Locale.Get("OpenLogDirectoryTooltip"));
        // 
        // saveButton
        // 
        saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        saveButton.AutoSize = true;
        saveButton.Location = new Point(232, 413);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(75, 25);
        saveButton.TabIndex = 5;
        saveButton.Text = Locale.Get("Save");
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += OnSaveClick;
        // 
        // cancelButton
        // 
        cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        cancelButton.AutoSize = true;
        cancelButton.Location = new Point(313, 413);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(75, 25);
        cancelButton.TabIndex = 6;
        cancelButton.Text = Locale.Get("Cancel");
        cancelButton.UseVisualStyleBackColor = true;
        cancelButton.Click += OnCancelClick;
        // 
        // SettingsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(400, 448);
        Controls.Add(cancelButton);
        Controls.Add(saveButton);
        Controls.Add(maintenanceGroup);
        Controls.Add(updatesGroup);
        Controls.Add(smokeApiGroup);
        Controls.Add(gameManagementGroup);
        Controls.Add(appearanceGroup);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SettingsForm";
        StartPosition = FormStartPosition.CenterParent;
        appearanceGroup.ResumeLayout(false);
        gameManagementGroup.ResumeLayout(false);
        smokeApiGroup.ResumeLayout(false);
        smokeApiGroup.PerformLayout();
        maintenanceGroup.ResumeLayout(false);
        maintenanceGroup.PerformLayout();
        updatesGroup.ResumeLayout(false);
        updatesGroup.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private GroupBox appearanceGroup;
    private GroupBox gameManagementGroup;
    private GroupBox smokeApiGroup;
    private GroupBox updatesGroup;
    private GroupBox maintenanceGroup;
    private CheckBox preReleaseCheckBox;
    private CheckBox darkModeCheckBox;
    private CheckBox blockedGamesCheckBox;
    private CheckBox sortByNameCheckBox;
    private Label defaultAppStatusLabel;
    private ComboBox defaultAppStatusComboBox;
    private Button clearCacheButton;
    private Button reconfigureSteamCMDButton;
    private Button saveButton;
    private Button cancelButton;
    private Button openLogDirButton;
    private Button checkForUpdatesButton;
    private ToolTip SettingsToolTip;
}
