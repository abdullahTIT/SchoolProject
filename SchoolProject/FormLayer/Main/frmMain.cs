using FormLayer.LoginScreen;
using FormLayer.Main.ctrl;
using FromSide;
using FromSide.General;
using FromSide.Persons;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Resources;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;

namespace FormLayer
{
    public partial class frmMain : Form
    {
        frmLogin _frmLogin;
        public frmMain(frmLogin frm)
        {
            InitializeComponent();
            _frmLogin = frm;
            this.Icon = FontAwesome.Sharp.IconChar.School.ToIcon();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnPerson_Click(object sender, EventArgs e)
        {
            pnlInfo.Controls.Clear();
            MainPersonctrl m = new MainPersonctrl();
            m.Dock = DockStyle.Fill;
            pnlInfo.Controls.Add(m);

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            pnlInfo.Controls.Clear();
            MainEmployeeCtrl m = new MainEmployeeCtrl();
            m.Dock = DockStyle.Fill;
            pnlInfo.Controls.Add(m);
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            pnlInfo.Controls.Clear();
            SettingsCtrl m = new SettingsCtrl();
            m.Dock = DockStyle.Fill;
            pnlInfo.Controls.Add(m);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            UIManager.ApplySettingsToForm(this);
        }
    }
}
