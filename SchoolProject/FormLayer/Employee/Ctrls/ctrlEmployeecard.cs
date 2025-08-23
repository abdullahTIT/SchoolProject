using Business.Employees;
using Business.Persons;
using FontAwesome.Sharp;
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
using static FromSide.frmAddEditEmployeeInfo;

namespace FromSide.Persons.Ctrls
{
    public partial class ctrlEmployeecard : UserControl
    {
        private clsEmployee _Employee;
        private int _EmployeeID = -1;

        public int EmployeeID
        {
            get { return _EmployeeID; }
        }

        public clsEmployee SelectedEmployeeInfo
        {
            get { return _Employee; }
        }
        public ctrlEmployeecard()
        {
            InitializeComponent();
        }


        public void LoadEmployeeInfo(int EmployeeID)
        {
            _Employee = clsEmployee.Find(EmployeeID);
            ResetPersonInfo();
            if (_Employee == null)
            {
                MessageBox.Show("No Employee with EmployeeID = " + EmployeeID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillEmployeeInfo();
        }

        private void _FillEmployeeInfo()
        {
            if (_Employee == null)
            {
                return;
            }
            _EmployeeID = _Employee.EmployeeID;
            lblEmployeeID.Text = _EmployeeID.ToString();
            lblHireDate.Text = _Employee.HireDate.ToString();
            lblSalary.Text = _Employee.Salary.ToString() + " $";
            lblJobTile.Text = clsJobTitle.Find(_Employee.JobTitleID).JobTitleName.ToString();
            pcbxCanTeach.IconChar = clsJobTitle.Find(_Employee.JobTitleID).CanTeach ? IconChar.Check : IconChar.X;
            pcbxIsActive.IconChar = _Employee.IsActive ? IconChar.Check : IconChar.X;
            pcbxIsAdmin.IconChar = clsJobTitle.Find(_Employee.JobTitleID).IsAdministrative ? IconChar.Check : IconChar.X;
            lblHireDate.Text = _Employee.TerminationDate.HasValue
                ? _Employee.TerminationDate.Value.ToString()
                : "Still work";
            if (_Employee.PersonID != null)
                ctrlPersoncard1.LoadPersonInfo(_Employee.PersonID);
        }

        public void ResetPersonInfo()
        {
            ctrlPersoncard1.ResetPersonInfo();

            _EmployeeID = -1;

            lblEmployeeID.Text = "EmployeeID";
            lblHireDate.Text = "Hire Date";
            lblJobTile.Text = "Job Tilte";
            lblSalary.Text = "Salary $";
            pcbxCanTeach.IconChar = IconChar.Square;
            pcbxIsActive.IconChar = IconChar.Square;
            pcbxIsAdmin.IconChar = IconChar.Square;
            lblTerminationDate.Text = "Termination Date";
        }

        private void btnEditAll_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Does not finished it yet");
        }

        private void ctrlEmployeecard_Load(object sender, EventArgs e)
        {
            UIManager.ApplySettingsToControl(this);

        }
    }
}
