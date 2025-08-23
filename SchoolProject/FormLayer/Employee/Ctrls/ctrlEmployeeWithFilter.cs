using Business.Employees;
using FromSide.General;
using FromSide.Persons.Ctrls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FromSide.Employee.Ctrls
{
    public partial class ctrlEmployeeWithFilter : UserControl
    {
        public event Action<int> OnEmployeeSelected;

        protected virtual void EmployeeSelected(int EmployeeID)
        {
            Action<int> handler = OnEmployeeSelected;
            if (handler != null)
            {
                handler(EmployeeID); // Raise the event with the parameter
            }
        }
        private bool _ShowAddEmployee = true;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowAddEmployee
        {
            get { return _ShowAddEmployee; }
            set
            {
                _ShowAddEmployee = value;
                btnAddEmployee.Visible = value;
            }
        }
        private bool _FilterEnabled = true;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                grbxfilter.Enabled = value;
            }
        }




        public ctrlEmployeeWithFilter()
        {
            InitializeComponent();
        }

        private int _EmployeeID = -1;

        public int EmployeeID
        {
            get { return ctrlEmployeecard1.EmployeeID; }
        }

        public clsEmployee selectedEmployeeInfo
        {
            get
            {
                return ctrlEmployeecard1.SelectedEmployeeInfo;
            }
        }

        public void LoadEmployeeInfo(int EmployeeID)
        {
            txbxSearch.Text = EmployeeID.ToString();
            ctrlEmployeecard1.ResetPersonInfo();
            FindNow();
        }
 

        private void FindNow()
        {
            cmbxFilterBy.SelectedItem = "Employee ID";
            errorProvider1.SetError(txbxSearch, null);
            switch (cmbxFilterBy.Text)
            {
                case "Employee ID":
                    if (!string.IsNullOrWhiteSpace(txbxSearch.Text) && int.TryParse(txbxSearch.Text, out int employeeId))
                    {
                        var employee =  clsEmployee.Find(employeeId);
                        if (employee != null)
                        {
                            ctrlEmployeecard1.LoadEmployeeInfo(employeeId);
                        }
                        else
                        {
                            MessageBox.Show("No employee found with the provided Employee ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ctrlEmployeecard1.ResetPersonInfo();

                        }
                    }
                    else
                    {
                        errorProvider1.SetError(txbxSearch, "Please enter vaild number to search");
                    }
                    break;
                case "Person ID":
                    if (!string.IsNullOrWhiteSpace(txbxSearch.Text) && int.TryParse(txbxSearch.Text, out int personId))
                    {

                        var employee = clsEmployee.FindByPersonId(personId);

                        if (employee != null)
                        {
                            ctrlEmployeecard1.LoadEmployeeInfo(employee.EmployeeID);
                        }
                        else
                        {
                            ctrlEmployeecard1.ResetPersonInfo();
                            MessageBox.Show("No employee found with the provided Person ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        errorProvider1.SetError(txbxSearch, "Please enter a valid number to search.");
                    }
                    break;


                case "Phone":
                    errorProvider1.Clear(); // تنظيف الأخطاء السابقة

                    if (!string.IsNullOrWhiteSpace(txbxSearch.Text))
                    {
                        var phone = txbxSearch.Text.Trim();

                        var emp = clsEmployee.FindByPhone(phone);

                        if (emp != null)
                        {
                            ctrlEmployeecard1.LoadEmployeeInfo(emp.EmployeeID);
                        }
                        else
                        {
                            ctrlEmployeecard1.ResetPersonInfo();


                            MessageBox.Show("there is no emloyee wiht this phone number", "search result ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                        }
                    }
                    else
                    {

                        errorProvider1.SetError(txbxSearch, "pleace enter phone number:");
                    }
                    break;



                default:
                    break;
            }
            if (OnEmployeeSelected != null && FilterEnabled)
                OnEmployeeSelected(ctrlEmployeecard1.EmployeeID);
        }

        private void cmbxFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbxSearch.Text = "";
            txbxSearch.Focus();
        }


        private void txbxSearch_TextChanged(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            frmAddEditEmployeeInfo frmAddEdit = new frmAddEditEmployeeInfo();
            frmAddEdit.DataBack += DatabackEvent;
            frmAddEdit.ShowDialog();

        }

        private void DatabackEvent(object sender, int EmployeeID)
        {
            cmbxFilterBy.SelectedIndex = 1;
            txbxSearch.Text = EmployeeID.ToString();
            ctrlEmployeecard1.LoadEmployeeInfo(EmployeeID);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FindNow();
        }

        private void txbxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txbxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSearch.PerformClick();
            }
        }

        private void ctrlEmployeeWithFilter_Load(object sender, EventArgs e)
        {
            UIManager.ApplySettingsToControl(this);

            btnSearch.Focus();
            if (cmbxFilterBy.Items.Count > 0)
            {
                cmbxFilterBy.SelectedItem = "Employee ID";
            }
        }
    }

}
