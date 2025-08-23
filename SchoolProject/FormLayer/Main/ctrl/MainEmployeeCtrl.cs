using FromSide;
using FromSide.General;
using FromSide.Persons;
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
    public partial class MainEmployeeCtrl : UserControl
    {
        public MainEmployeeCtrl()
        {
            InitializeComponent();
        }

        private void btnAddEditEmployee_Click(object sender, EventArgs e)
        {
            frmAddEditEmployeeInfo frmAddEditEmployeeInfo = new frmAddEditEmployeeInfo();
            frmAddEditEmployeeInfo.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            frmAddEditEmployeeInfo newEmployee = new frmAddEditEmployeeInfo(); newEmployee.ShowDialog();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            frmFindEmployeeInfo frmFindEmployeeInfo = new frmFindEmployeeInfo();
            frmFindEmployeeInfo.ShowDialog();
        }

        private void btnShowList_Click(object sender, EventArgs e)
        {
            frmEmployeesLists frmEmployeesLists = new frmEmployeesLists();
            frmEmployeesLists.ShowDialog();
        }

        private void MainEmployeeCtrl_Load(object sender, EventArgs e)
        {
            UIManager.ApplySettingsToControl(this);

        }
    }
}
