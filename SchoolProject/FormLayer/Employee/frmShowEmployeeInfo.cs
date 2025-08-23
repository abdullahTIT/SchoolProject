using Business.Employees;
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

namespace FromSide.Persons
{
    public partial class frmShowEmployeeInfo : Form
    {
        public frmShowEmployeeInfo(int EmployeeID)
        {
            InitializeComponent();
            ctrlEmployeecard1.LoadEmployeeInfo(EmployeeID);
        }
        public frmShowEmployeeInfo(string Phone)
        {
            InitializeComponent();

            ctrlEmployeecard1.LoadEmployeeInfo(clsEmployee.FindByPhone(Phone).EmployeeID);
        }


        private void frmShowPersonInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Delete)
                this.Close();
        }

        private void frmShowEmployeeInfo_Load(object sender, EventArgs e)
        {
            UIManager.ApplySettingsToForm(this);

        }
    }
}
