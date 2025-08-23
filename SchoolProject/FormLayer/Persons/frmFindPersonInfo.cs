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

    public partial class frmFindPersonInfo : Form
    {
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;
        public int PersonID;
        public frmFindPersonInfo()
        {
            InitializeComponent();
            this.Icon = FontAwesome.Sharp.IconChar.Search.ToIcon();
        }

        private void frmFindPersonInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            PersonID = ctrlPersonCardWithFilter1.PersonID;
            DataBack?.Invoke(this, ctrlPersonCardWithFilter1.PersonID);
        }

        private void frmFindPersonInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmFindPersonInfo_Load(object sender, EventArgs e)
        {
            UIManager.ApplySettingsToForm(this);

        }
    }
}
