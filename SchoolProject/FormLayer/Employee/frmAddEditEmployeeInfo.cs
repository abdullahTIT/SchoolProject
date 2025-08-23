using Business;
using Business.Employees;
using Business.Employees;
using Business.Persons;
using DataAccess.Emloyees;
using DataAccess.Persons;
using FromSide.General;
using Microsoft.Win32;
using Models.Employees;
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
using static System.Net.Mime.MediaTypeNames;
namespace FromSide
{
    public partial class frmAddEditEmployeeInfo : Form
    {
        public delegate void DataBackEventHandler(object sender, int EmployeeID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _EmployeeID = -1;
        clsEmployee _Employee;
        private int _PersonID = -1;

        public frmAddEditEmployeeInfo()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
            this.Icon = FontAwesome.Sharp.IconChar.UserPlus.ToIcon();
        }

        public frmAddEditEmployeeInfo(int EmployeeID)
        {

            InitializeComponent();
            this.Icon = FontAwesome.Sharp.IconChar.UserEdit.ToIcon();
            _Mode = enMode.Update;
            _EmployeeID = EmployeeID;
        }

        private void btnEditPersonInfo_Click(object sender, EventArgs e)
        {
            if (_Employee.PersonID != null)
            {
                frmAddEditPersonInfo form = new frmAddEditPersonInfo(_Employee.PersonID);
                form.ShowDialog();
                return;
            }
            else
            {
                frmAddEditEmployeeInfo frm = new frmAddEditEmployeeInfo();
                frm.ShowDialog();
                _PersonID = frm._PersonID;
                _Employee.PersonID = frm._PersonID;
                txbxPersonID.Text = _PersonID.ToString();
            }
            
        }

        private void _FillJobTitlesInComPobox()
        {
            List<JobTitleDTO> jobTitles = clsJobTitle.GetAll();

            // إضافة خيار None في الأعلى
            JobTitleDTO noneOption = new JobTitleDTO
            {
                JobTitleID = -1,
                JobTitleName = "None"
            };

            jobTitles.Insert(0, noneOption);

            cmbxJobTitles.DataSource = jobTitles;
            cmbxJobTitles.DisplayMember = "JobTitleName";
            cmbxJobTitles.ValueMember = "JobTitleID";
            cmbxJobTitles.SelectedIndex = (cmbxJobTitles.Items.Count > 0) ? 0 : -1;
        }

        private void ResetFrom()
        {
            _FillJobTitlesInComPobox();
            dtHireDate.Value = DateTime.Now;
            dtHireDate.MinDate = DateTime.Now.AddYears(-10);
            dtHireDate.MaxDate = DateTime.Now.AddYears(11);

            lblTitle.Text = "Add New Employee";
            _Employee = new clsEmployee();
            txbxNotes.Text = "";
            txbxPersonID.Text = "";
            txbxSalary.Text = "";
            rbtnActive.Checked = true;
            rbtnNotActve.Checked = false;

        }

        private void _LoadData()
        {
            lblTitle.Text = "Update Employee with ID: " + _EmployeeID.ToString();
            _FillJobTitlesInComPobox();

            _Employee = clsEmployee.Find(_EmployeeID);
            if (_Employee == null)
            {
                MessageBox.Show("No Employee with ID = " + _Employee, "Employee Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            cmbxJobTitles.SelectedValue = _Employee.JobTitleID;

            txbxPersonID.Text = _Employee.PersonID.ToString();
            txbxNotes.Text = _Employee.Notes.ToString() ?? "";
            txbxSalary.Text = _Employee.Salary.ToString() ?? "$0";
            dtHireDate.Value = _Employee.HireDate;
            if (_Employee.TerminationDate != null)
                dtTerminationDate.Value = _Employee.TerminationDate.Value;
            if (_Employee.IsActive)
            {
                rbtnActive.Checked = true;
                rbtnNotActve.Checked = false;
            }
            else
            {
                rbtnActive.Checked = false;
                rbtnNotActve.Checked = true;
            }
        }

        private void frmAddEditEmployeeInfo_Load(object sender, EventArgs e)
        {
            UIManager.ApplySettingsToForm(this);
            ResetFrom();
            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
            else
            {
                frmFindPersonInfo frm = new frmFindPersonInfo();
                frm.ShowDialog();
                _Employee.PersonID = frm.PersonID;
                txbxPersonID.Text = frm.PersonID.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show(
                    "Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txbxPersonID.Text))
            {
                MessageBox.Show("There is no person selecte yet", "Select person", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
                frm.ShowDialog();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _Employee.PersonID = frm.GeneratedPersonID ?? -1;
                }
                return;
            }


            _Employee.JobTitleID = cmbxJobTitles.SelectedIndex;
            _Employee.HireDate = dtHireDate.Value;
            if (checkBox1.Enabled)
            {
                _Employee.TerminationDate = dtTerminationDate.Value;
            }
            _Employee.IsActive = rbtnActive.Checked;
            if (string.IsNullOrEmpty(txbxNotes.Text))
            {
                _Employee.Notes = txbxNotes.Text;
            }
            _Employee.Salary = decimal.TryParse(txbxSalary.Text, out decimal salary) ? salary : 0;
            if (_Employee.Save())
            {
                lblTitle.Text = "Update Employee with ID: " + _Employee.EmployeeID.ToString();

                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _Employee.PersonID);
                _EmployeeID = _Employee.EmployeeID;
            }
            else
                MessageBox.Show("Error: Data  not Saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;
            string text = Temp.Text.Trim();



            if (string.IsNullOrEmpty(text))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dtTerminationDate.Enabled = true;
            }
            else
            {
                dtTerminationDate.Enabled = false;
            }
        }

        private void txbxSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void frmAddEditEmployeeInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                Control activeControl = this.ActiveControl;
                if (!(activeControl is TextBox) &&
                !(activeControl is RichTextBox) &&
                !(activeControl is ComboBox) &&
                !(activeControl is MaskedTextBox))
                {
                    // إغلاق الفورم
                    this.Close();
                }
            }
        }
    }
}
