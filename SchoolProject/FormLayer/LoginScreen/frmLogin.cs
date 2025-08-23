using Business.Employees;
using FromSide.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormLayer.LoginScreen
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            this.Icon = FontAwesome.Sharp.IconChar.School.ToIcon();
        }

        private void txbxUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '_' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txbxUserName_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(txbxUserName.Text, @"^[a-zA-Z0-9_]{4,20}$"))
            {
                errorProvider1.SetError(txbxUserName, "Username must be 4-20 chars, letters, digits or _ only.");
            }
            else
            {
                errorProvider1.SetError(txbxUserName, "");
            }
        }

        private void txbxPasswrod_TextChanged(object sender, EventArgs e)
        {
            if (!IsValidPassword(txbxPassword.Text))
            {
                errorProvider1.SetError(txbxPassword, "Password must be 4-20 chars, include upper, lower, digit & symbol.");
            }
            else
            {
                errorProvider1.SetError(txbxPassword, "");
            }
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (!IsValidUsername(txbxUserName.Text) && IsValidPassword(txbxPassword.Text))
            {
                MessageBox.Show("Please fix errors before login.");
                return;
            }
            string password = clsGlobal.ComputeHash(txbxPassword.Text);
            clsUser user = clsUser.Find(txbxUserName.Text.Trim(), password);
            if (user != null)
            {
                if (cbxSaveLogInfo.Checked)
                    clsGlobal.CredentialManager.RememberUsernameAndPassword(txbxUserName.Text, txbxPassword.Text);
                else
                    clsGlobal.CredentialManager.RememberUsernameAndPassword("", "");


                if (!user.IsActive)
                {
                    txbxUserName.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                clsGlobal.CurrentUser = user;
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();
            }
            else
            {
                txbxUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private bool IsValidUsername(string username)
        {
            return Regex.IsMatch(username, @"^[a-zA-Z0-9_]{4,20}$");
        }
        private bool IsValidPassword(string password)
        {
            if (password.Length < 4 || password.Length > 20)
                return false;

            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpper && hasLower && hasDigit && hasSpecial;
        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {
            //comboBox1.SelectedIndex = 0;
            UIManager.ApplySettingsToForm(this);

            string userName = "", password = "";
            if (clsGlobal.CredentialManager.GetStoredCredential(ref userName, ref password))
            {
                txbxUserName.Text = userName;
                txbxPassword.Text = password;
                cbxSaveLogInfo.Checked = true;
            }
            else
                cbxSaveLogInfo.Checked = false;
        }

       
    }
}
