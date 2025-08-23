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
    public partial class MainPersonctrl : UserControl
    {
        public MainPersonctrl()
        {
            InitializeComponent();
        }

        private void btnAddEditPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frmAddEditPersonInfo = new frmAddEditPersonInfo();
            frmAddEditPersonInfo.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo newPerson = new frmAddEditPersonInfo(); newPerson.ShowDialog();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            frmFindPersonInfo frmFindPersonInfo = new frmFindPersonInfo();
            frmFindPersonInfo.ShowDialog();
        }

        private void btnShowPerson_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(23); frm.ShowDialog();
        }

        private void btnShowList_Click(object sender, EventArgs e)
        {
            frmPersonsLists frmShowList = new frmPersonsLists();
            frmShowList.ShowDialog();
        }

        private void MainPersonctrl_Load(object sender, EventArgs e)
        {
            UIManager.ApplySettingsToControl(this);

        }
    }
}
