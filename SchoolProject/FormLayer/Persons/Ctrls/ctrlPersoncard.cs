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
using static FromSide.frmAddEditPersonInfo;

namespace FromSide.Persons.Ctrls
{
    public partial class ctrlPersoncard : UserControl
    {

        private clsPerson _Person;

        private int _PersonID = -1;

        public int? PersonID
        {
            get
            {
                if (int.TryParse(lblPersonID.Text, out int value))
                    return value;
                return null;
            }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }
        public ctrlPersoncard()
        {
            InitializeComponent();
        }

        //private void SomeStyle()
        //{
        //    clsGlobal.SetControlRegionRounded(pcbxPersonImage, 4);
        //    clsGlobal.SetControlRegionRounded(this, 6);
        //    clsGlobal.SetControlRegionRounded(this.label1, 4);
        //    clsGlobal.SetControlRegionRounded(this.panel1, 4);
        //}
        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            ResetPersonInfo();
            if (_Person == null)
            {
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        public void LoadPersonInfo(string Phone)
        {
            _Person = clsPerson.Find(Phone);
            ResetPersonInfo();
            if (_Person == null)
            {
                MessageBox.Show("No Person with Phone = " + Phone, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();
        }

        private void _LoadPersonImage()
        {
            pcbxPersonImage.Image = null;
            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pcbxPersonImage.ImageLocation = ImagePath;
            //else
            //MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void _FillPersonInfo()
        {
            if (_Person == null)
            {
                pcbxPersonImage.IconChar = FontAwesome.Sharp.IconChar.CameraAlt;

                return;
            }
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblIdentifier.Text = _Person.IdentifierID.ToString();
            lblName.Text = _Person.FirstName + " " +
                _Person.SecondName + " " + _Person.LastName;
            lblAddress.Text = _Person.Address;
            lblPhone.Text = _Person.Phone;
            lblEmail.Text = _Person.Email;
            lblBirthDate.Text = _Person.BirthDate.ToString();
            lblGender.Text = _Person.Gender ? "Male" : "Female";
            _LoadPersonImage();
        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "PersonID";
            lblIdentifier.Text = "Identifier";
            lblName.Text = "Name";
            lblAddress.Text = "Address";
            lblPhone.Text = "Phone";
            lblEmail.Text = "Email";
            lblBirthDate.Text = "BirthDate";
            lblGender.Text = "Gender";
            pcbxPersonImage.Image = null;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo(_PersonID);
            frm.ShowDialog();
            LoadPersonInfo(_PersonID);
        }

        private void ctrlPersoncard_Load(object sender, EventArgs e)
        {
            UIManager.ApplySettingsToControl(this);
            //General.clsGlobal.SetRoundedRegionAuto(panel1, .1f);
            //General.clsGlobal.SetRoundedRegionAuto(label1, .1f);
        }


    }
}
