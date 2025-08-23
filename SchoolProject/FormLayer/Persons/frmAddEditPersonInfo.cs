using Business.Persons;
using DataAccess.Persons;
using FromSide.General;
using Microsoft.Win32;
using Models.Persons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
namespace FromSide
{
    public partial class frmAddEditPersonInfo : Form
    {
        private string extraInfo;

        [Category("Custom")]
        [Description("Some extra information for the button.")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ExtraInfo
        {
            get { return extraInfo; }
            set { extraInfo = value; }
        }

        private bool isImportant;

        [Category("Custom")]
        [Description("Indicates if this button is important.")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool IsImportant
        {
            get { return isImportant; }
            set { isImportant = value; }
        }
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 };
        public enum enGendor { Male = 0, Female = 1 };

        private enMode _Mode;
        private int _PersonID = -1;
        public int? GeneratedPersonID { get; private set; }

        clsPerson _Person;

        public frmAddEditPersonInfo()
        {
            InitializeComponent();


            _Mode = enMode.AddNew;
            this.Icon = FontAwesome.Sharp.IconChar.UserPlus.ToIcon();

        }

        public frmAddEditPersonInfo(int PersonID)
        {
            InitializeComponent();
            this.Icon = FontAwesome.Sharp.IconChar.UserPlus.ToIcon();
            _Mode = enMode.Update;
            _PersonID = PersonID;


        }

        private void _FillIdentifierTypesInComPobox()
        {
            List<IdentifierTypeDTO> identifierTypes = IdentifierType.GetAll();

            cmbxIdentifierTypes.DataSource = identifierTypes;
            cmbxIdentifierTypes.DisplayMember = "IdentifierName";
            cmbxIdentifierTypes.ValueMember = "IdentifierTypeID";
            cmbxIdentifierTypes.SelectedIndex = (cmbxIdentifierTypes.Items.Count > 0) ? 0 : -1;

        }

        public static void ClearAllErrors(Control parent, ErrorProvider errorProvider)
        {
            foreach (Control c in parent.Controls)
            {
                errorProvider.SetError(c, null);
                if (c.HasChildren)
                {
                    ClearAllErrors(c, errorProvider);
                }
            }
        }

        private void ResetFrom()
        {
            _FillIdentifierTypesInComPobox();
            dtBirthDate.MinDate = DateTime.Now.AddYears(-100);
            dtBirthDate.MaxDate = DateTime.Now.AddYears(-3);
            lblTitle.Text = "Add New Person";
            _Person = new clsPerson();
            btnRemoveImage.Visible = false;
            cmbxIdentifierTypes.SelectedIndex = 0;
            txbxIdentifierID.Text = "";
            txbxFirstName.Text = "";
            txbxSecondName.Text = "";
            txbxThirdName.Text = "";
            txbxLastName.Text = "";
            txBxEmail.Text = "";
            txbxPhone.Text = "";
            txbxAddress.Text = "";
            dtBirthDate.Value = DateTime.Now.AddYears(-5);
            rdbtnMale.Checked = true;
            PcbxPerson.ImageLocation = null;
            DisbledControlsForAddNewMode();
        }

        private void _LoadData()
        {
            lblTitle.Text = "Update Person with ID: " + _PersonID.ToString();
            _FillIdentifierTypesInComPobox();

            _Person = clsPerson.Find(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            txbxIdentifierID.Text = _Person.IdentifierID?.ToString() ?? "Null";
            if (_Person.IdentifierID.HasValue)
            {
                var identifier = clsIdentifier.Find(_Person.IdentifierID.Value);
                if (identifier != null)
                {
                    cmbxIdentifierTypes.SelectedValue = identifier.IdentifierTypeID;
                }
            }

            txbxFirstName.Text = _Person.FirstName.Trim();
            txbxSecondName.Text = _Person.SecondName.Trim();
            txbxThirdName.Text = _Person.ThirdName.Trim();
            txbxLastName.Text = _Person.LastName.Trim();
            txBxEmail.Text = _Person?.Email?.Trim() ?? "";
            txbxPhone.Text = _Person?.Phone?.Trim() ?? "";
            txbxAddress.Text = _Person?.Address?.Trim() ?? "";
            dtBirthDate.Value = _Person.BirthDate ?? DateTime.Now.AddYears(-5);
            rdbtnMale.Checked = _Person.Gender;
            rdbtnFemale.Checked = !_Person.Gender;
            PcbxPerson.ImageLocation = string.IsNullOrWhiteSpace(_Person.ImagePath) ? null : _Person.ImagePath;

        }

        private async void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            UIManager.ApplySettingsToForm(this);
            ResetFrom();

            if (_Mode == enMode.Update)
            {
                _LoadData();
                EnabledControlsForUpdateMode();
                btnRemoveImage.Visible = (PcbxPerson.ImageLocation != null);
            }
            //if (LanguageManager.CurrentLanguage == "ar")
            //{
            //    AppFonts.ApplyFontWithStyleCheck(this, AppFonts.DefaultFont);
            //    this.RightToLeft = RightToLeft.Yes;
            //    this.RightToLeftLayout = true;
            //}
            //else
            //{
            //    this.RightToLeft = RightToLeft.No;
            //    this.RightToLeftLayout = false;
            //}

        }
        private void DisbledControlsForAddNewMode()
        {
            txbxFirstName.Enabled = false;
            txbxSecondName.Enabled = false;
            txbxThirdName.Enabled = false;
            txbxLastName.Enabled = false;
            txbxPhone.Enabled = false;
            rdbtnMale.Enabled = false;
            rdbtnFemale.Enabled = false;
            dtBirthDate.Enabled = false;
            txbxAddress.Enabled = false;
            txBxEmail.Enabled = false;
            PcbxPerson.Enabled = false;
            btnAddImage.Enabled = false;
            btnRemoveImage.Enabled = false;
            btnSave.Enabled = false;
            btnReset.Enabled = false;
            cmbxIdentifierTypes.Enabled = true;
            txbxIdentifierID.Enabled = true;
            btnAdd.Enabled = true;
            btnSkip.Enabled = true;
        }

        private void EnabledControlsForUpdateMode()
        {
            txbxFirstName.Enabled = true;
            txbxSecondName.Enabled = true;
            txbxThirdName.Enabled = true;
            txbxLastName.Enabled = true;
            txbxPhone.Enabled = true;
            rdbtnMale.Enabled = true;
            rdbtnFemale.Enabled = true;
            dtBirthDate.Enabled = true;
            txbxAddress.Enabled = true;
            txBxEmail.Enabled = true;
            PcbxPerson.Enabled = true;
            btnAddImage.Enabled = true;
            btnRemoveImage.Enabled = true;
            btnSave.Enabled = true;
            btnReset.Enabled = true;
            cmbxIdentifierTypes.Enabled = true;
            txbxIdentifierID.Enabled = true;
            btnAdd.Enabled = true;
            btnSkip.Enabled = true;
        }

        private bool _HandlePersonImage()
        {
            try
            {
                if (!string.IsNullOrEmpty(_Person.ImagePath) &&
                    _Person.ImagePath != PcbxPerson.ImageLocation &&
                    File.Exists(_Person.ImagePath))
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (UnauthorizedAccessException uaex)
                    {
                        MessageBox.Show("can't remove old imge", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (IOException ioex)
                    {
                        MessageBox.Show("error in removeing old imge", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // إذا هناك صورة جديدة في PictureBox، ننسخها إلى مجلد المشروع
                if (!string.IsNullOrEmpty(PcbxPerson.ImageLocation))
                {
                    string sourceImageFile = PcbxPerson.ImageLocation;

                    if (!File.Exists(sourceImageFile))
                    {
                        MessageBox.Show("new path not exist.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (clsUtil.CopyImageToProjectImagesFolder(ref sourceImageFile))
                    {
                        // تحديث المسار في PictureBox
                        PcbxPerson.ImageLocation = sourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("error when try to copy imge path.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                // لا توجد صورة جديدة -> نعتبر العملية ناجحة
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while handling the person image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            _Person.IdentifierID = null;
            txbxIdentifierID.Text = default;
            EnabledControlsForUpdateMode();
            cmbxIdentifierTypes.Enabled = false;
            txbxIdentifierID.Enabled = false;
            btnAdd.Enabled = false;
            btnSkip.Enabled = false;

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbxIdentifierTypes.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an Identifier Type.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txbxIdentifierID.Text))
            {
                errorProvider1.SetError(txbxIdentifierID, "Identifier ID cannot be empty.you can press skip");
                return;
            }
            clsIdentifier identifier = new clsIdentifier();
            identifier.IdentifierTypeID = (int)cmbxIdentifierTypes.SelectedValue;
            identifier.IdentifierValue = txbxIdentifierID.Text.Trim();
            if (identifier.DoesIdentifierExist(identifier.IdentifierID))
            {
                MessageBox.Show("This Identifier already exists.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbxIdentifierID.Focus();
                return;
            }
            if (identifier.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbxIdentifierTypes.Enabled = false;
                txbxIdentifierID.Enabled = false;
                btnSkip.Enabled = false;
                btnAdd.Enabled = false;
                _Person.IdentifierID = identifier.IdentifierID;
                EnabledControlsForUpdateMode();
                cmbxIdentifierTypes.Enabled = false;
                txbxIdentifierID.Enabled = false;
                btnAdd.Enabled = false;
                btnSkip.Enabled = false;
                txbxFirstName.Focus();
            }
            else
            {
                MessageBox.Show("Error saving data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show(
                    "Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (!_HandlePersonImage())
                return;

            if (string.IsNullOrWhiteSpace(txbxFirstName.Text)
                || string.IsNullOrWhiteSpace(txbxSecondName.Text)
                || string.IsNullOrWhiteSpace(txbxThirdName.Text)
                || string.IsNullOrWhiteSpace(txbxLastName.Text)
                || string.IsNullOrWhiteSpace(txbxPhone.Text))
            {
                MessageBox.Show("please full the txet boxes", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // يلغي عمل الزر
            }


            _Person.FirstName = txbxFirstName.Text.Trim();
            _Person.SecondName = txbxSecondName.Text.Trim();
            _Person.ThirdName = txbxThirdName.Text.Trim();
            _Person.LastName = txbxLastName.Text.Trim();
            string rawPhone = txbxPhone.Text ?? "";

            string digitsOnly = new string(rawPhone.Where(char.IsDigit).ToArray());

            _Person.Phone = string.IsNullOrWhiteSpace(digitsOnly) ? null : digitsOnly;
            _Person.Email = string.IsNullOrWhiteSpace(txBxEmail.Text) ? null : txBxEmail.Text.Trim();
            _Person.Address = string.IsNullOrWhiteSpace(txbxAddress.Text) ? null : txbxAddress.Text.Trim();

            _Person.Gender = rdbtnMale.Checked;
            _Person.BirthDate = dtBirthDate.Value.Date;
            _Person.ImagePath = string.IsNullOrWhiteSpace(PcbxPerson.ImageLocation) ? null : PcbxPerson.ImageLocation;


            if (_Person.Save())
            {
                lblTitle.Text = "Update Person with ID: " + _Person.PersonID.ToString();

                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _Person.PersonID);
                _PersonID = _Person.PersonID;
                GeneratedPersonID = _Person.PersonID;
            }

            else
                MessageBox.Show("Error: Data  not Saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //if (dtBirthDate.Value.Date == DateTime.Now.AddYears(-5))
            //{
            //    errorProvider1.SetError(dtBirthDate, "Please select a valid date of birth.");
            //    dtBirthDate.Focus();
            //    return;
            //}
        }
        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            PcbxPerson.ImageLocation = null;
            btnRemoveImage.Visible = false;
            PcbxPerson.Image = null;
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                PcbxPerson.Load(selectedFilePath);
                btnRemoveImage.Visible = true;
            }

        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;
            string text = Temp.Text.Trim();

            if (!clsValidatoin.ContainsOnlyLetters(text))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field cannot contain any digits or special characters, letters only!");
                return;
            }

            if (string.IsNullOrEmpty(text))
            {
                //e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }

        private void validatePhoneTextBox(object sender, CancelEventArgs e)
        {
            TextBox temp = (TextBox)sender;


            string phoneWithMask = temp.Text.Trim();

            string digitsOnly = Regex.Replace(phoneWithMask, @"\D", "");

            if (string.IsNullOrWhiteSpace(phoneWithMask) || digitsOnly.Length == 0 || phoneWithMask.Contains('_'))
            {
                //e.Cancel = true;
                errorProvider1.SetError(temp, "Phone number is required!");
            }
            else if (digitsOnly.Length != 9)
            {
                //e.Cancel = true;
                errorProvider1.SetError(temp, "Phone number must be exactly 9 digits.");
            }
            else if (!digitsOnly.StartsWith("5"))
            {
                //e.Cancel = true;
                errorProvider1.SetError(temp, "Phone number must start with 5.");
            }
            else if (clsPerson.DoesPhoneExist(digitsOnly, _Person.PersonID))
            {
                //e.Cancel = true;
                errorProvider1.SetError(temp, "Phone number already exists.");
            }
            else
            {
                errorProvider1.SetError(temp, null);
            }

        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {

            string email = txBxEmail.Text.Trim();

            // السماح بالفراغ
            if (string.IsNullOrWhiteSpace(email))
            {
                errorProvider1.SetError(txBxEmail, null);
                return;
            }

            // التحقق من الصيغة
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                //e.Cancel = true;
                errorProvider1.SetError(txBxEmail, "Invalid email format.");
                return;
            }

            // التحقق من التكرار مع السماح بالبريد إذا كان للشخص نفسه (وقت التحديث)
            if (clsPersonData.DoesEmailExist(email, _Person.PersonID))
            {
                //e.Cancel = true;
                errorProvider1.SetError(txBxEmail, "This email is already used by another person.");
            }
            else
            {
                errorProvider1.SetError(txBxEmail, null);
            }
        }

        private void txbxIdentifierID_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            int selectionStart = txt.SelectionStart;

            string digitsOnly = new string(txt.Text.Where(char.IsDigit).ToArray());

            if (txt.Text != digitsOnly)
            {
                txt.Text = digitsOnly;

                txt.SelectionStart = selectionStart > txt.Text.Length ? txt.Text.Length : selectionStart;
            }
        }
        private void txbx_KeyPressJustLetter(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                //e.Handled = true;
            }
        }

        private void frmAddEditPersonInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                btnSave.PerformClick();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                btnAdd.PerformClick();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                btnReset.PerformClick();
                e.Handled = true;
            }
        }

        private void txbxPhone_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
