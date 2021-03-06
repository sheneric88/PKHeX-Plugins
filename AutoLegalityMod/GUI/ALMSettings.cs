﻿using System;
using System.IO;
using System.Windows.Forms;
using PKHeX.Core.AutoMod;

namespace AutoModPlugins.GUI
{
    public partial class ALMSettings : Form
    {
        public ALMSettings(object obj)
        {
            InitializeComponent();
            PG_Settings.SelectedObject = obj;
            WinFormsTranslator.TranslateInterface(this, WinFormsTranslator.CurrentLanguage);

            this.CenterToForm(FindForm());
        }

        private void SettingsEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W && ModifierKeys == Keys.Control)
                Close();
        }

        private void ALMSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ALM Settings
            ShowdownSetLoader.SetAPILegalitySettings();

            Properties.AutoLegality.Default.Save();
        }

        private void RunBulkTests_Click(object sender, System.EventArgs e)
        {
            if (!Directory.Exists(TeamTest.TestPath))
            {
                WinFormsUtil.Error("Valid Test Path does not exist");
            }
            else
            {
                var results = TeamTest.VerifyFiles();
                var finalstr = "";
                foreach (var res in results)
                    finalstr += $"{Path.GetFileName(res.Key)} : Legal - {res.Value["legal"].Length} | Illegal - {res.Value["illegal"].Length}\n";
                WinFormsUtil.Alert(finalstr.TrimEnd());
            }
        }
    }
}