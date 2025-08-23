using Business.Persons;
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
namespace FromSide.Persons.Ctrls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        // Define a custom event handler delegate with parameters
        public event Action<int> OnPersonSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID); // Raise the event with the parameter
            }
        }


        private bool _ShowAddPerson = true;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

        public int PeronID
        {
            get { return ctrlPersoncard1.PersonID ?? 0; }
        }

        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                grbxfilter.Enabled = _FilterEnabled;
            }
        }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent(); 

        }
        private int _PersonID = -1;

        public int PersonID
        {
            get { return ctrlPersoncard1.PersonID ?? 0; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersoncard1.SelectedPersonInfo; }
        }

        public void LoadPersonInfo(int PersonID)
        {
            cmbxFilterBy.SelectedIndex = 0;
            txbxSearch.Text = PersonID.ToString();
            ctrlPersoncard1.ResetPersonInfo();
            FindNow();

        }
        private void FindNow()
        {
            errorProvider1.SetError(txbxSearch, null); // أولاً: أزل أي خطأ قديم
            switch (cmbxFilterBy.Text)
            {
                case "Person ID":

                    if (!string.IsNullOrWhiteSpace(txbxSearch.Text) && int.TryParse(txbxSearch.Text, out int personId))
                    {
                        ctrlPersoncard1.LoadPersonInfo(personId);
                    }
                    else
                    {
                        errorProvider1.SetError(txbxSearch, "Please enter vaild number to search");
                    }
                    break;

                case "Phone":
                    if (!string.IsNullOrWhiteSpace(txbxSearch.Text))
                    {
                        ctrlPersoncard1.LoadPersonInfo(txbxSearch.Text.Trim());
                    }
                    else
                    {
                        errorProvider1.SetError(txbxSearch, "يرجى إدخال قيمة للبحث.");
                    }
                    break;

                default:
                    break;
            }

            if (OnPersonSelected != null && FilterEnabled)
            // Raise the event with a parameter
            {
                if (ctrlPersoncard1.PersonID != null)
                {
                    OnPersonSelected(ctrlPersoncard1.PersonID.Value);
                }
            }
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

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            UIManager.ApplySettingsToControl(this);
            cmbxFilterBy.SelectedIndex = 0;
            txbxSearch.Focus();
        }
        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txbxSearch.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbxSearch, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txbxSearch, null);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();


        }
        private void DataBackEvent(object sender, int PersonID)
        {
            // Handle the data received

            cmbxFilterBy.SelectedIndex = 1;
            txbxSearch.Text = PersonID.ToString();
            ctrlPersoncard1.LoadPersonInfo(PersonID);
        }




        private void btnSearch_Click(object sender, EventArgs e)
        {
            FindNow();
        }



        private void txbxSearch_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSearch.PerformClick();
            }
        }

        private void txbxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }


        }
    }
}
