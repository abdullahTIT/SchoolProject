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

namespace FromSide
{

    public partial class frmFindEmployeeInfo : Form
    {
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        public frmFindEmployeeInfo()
        {
            InitializeComponent();
            this.Icon = FontAwesome.Sharp.IconChar.Search.ToIcon();

        }


        private void frmFindPersonInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DataBack?.Invoke(this, ctrlPersonCardWithFilter1.PersonID);
            DataBack?.Invoke(this, ctrlEmployeeWithFilter1.EmployeeID);
        }

        private void frmFindPersonInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmFindEmployeeInfo_Load(object sender, EventArgs e)
        {
            UIManager.ApplySettingsToForm(this);

        }
    }
}
