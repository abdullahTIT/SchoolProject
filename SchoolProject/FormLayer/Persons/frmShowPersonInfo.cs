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
    public partial class frmShowPersonInfo : Form
    {
        public frmShowPersonInfo(int PersonID)
        {
            InitializeComponent();
            ctrlPersoncard1.LoadPersonInfo(PersonID);
        }
        public frmShowPersonInfo(string Phone)
        {
            InitializeComponent();
            ctrlPersoncard1.LoadPersonInfo(Phone);
        }

        private void frmShowPersonInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Delete) // أو استخدم مفتاح آخر مثلاً Keys.F1
                this.Close();
        }

        private void frmShowPersonInfo_Load(object sender, EventArgs e)
        {
            UIManager.ApplySettingsToForm(this);

        }
    }
}
