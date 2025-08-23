using FromSide.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormLayer.Main.ctrl
{
    public partial class SettingsCtrl : UserControl
    {
        public SettingsCtrl()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cbxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbxLanguage.Text)
            {
                case "English":
                    if (MessageBox.Show("Are you sure app wil restart?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        ConfigManager.SetLanguage("en");
                        Application.Restart();
                    }
                    break;
                case "Arabic":
                    if (MessageBox.Show("Are you sure app wil restart?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        ConfigManager.SetLanguage("ar");
                        Application.Restart();
                    }
                    break;
                default:

                    break;
            }
        }

        private void cbxTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbxTheme.Text)
            {
                case "Dark":
                    if (MessageBox.Show("Are you sure app wil restart?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        ConfigManager.SetTheme("Dark");
                        Application.Restart();
                    }
                    break;
                case "Light":
                    if (MessageBox.Show("Are you sure app wil restart?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        ConfigManager.SetTheme("light");
                        Application.Restart();
                    }
                    break;
                case "Blue":
                    if (MessageBox.Show("Are you sure app wil restart?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        ConfigManager.SetTheme("blue");
                        Application.Restart();
                    }
                    break;
                default:

                    break;
            }

        }

        private void SettingsCtrl_Load(object sender, EventArgs e)
        {
            UIManager.ApplySettingsToControl(this);

        }
    }
}
