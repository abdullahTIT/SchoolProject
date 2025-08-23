namespace FromSide
{
    partial class frmAddEditPersonInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Panel pnlFullInfo;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddEditPersonInfo));
            txbxPhone = new TextBox();
            dtBirthDate = new DateTimePicker();
            btnRemoveImage = new FontAwesome.Sharp.IconButton();
            btnAddImage = new FontAwesome.Sharp.IconButton();
            btnSave = new FontAwesome.Sharp.IconButton();
            btnReset = new FontAwesome.Sharp.IconButton();
            PcbxPerson = new FontAwesome.Sharp.IconPictureBox();
            txbxAddress = new TextBox();
            txBxEmail = new TextBox();
            iconPictureBox9 = new FontAwesome.Sharp.IconPictureBox();
            iconPictureBox8 = new FontAwesome.Sharp.IconPictureBox();
            rdbtnFemale = new RadioButton();
            rdbtnMale = new RadioButton();
            txbxSecondName = new TextBox();
            txbxThirdName = new TextBox();
            txbxLastName = new TextBox();
            txbxFirstName = new TextBox();
            iconPictureBox5 = new FontAwesome.Sharp.IconPictureBox();
            label5 = new Label();
            iconPictureBox6 = new FontAwesome.Sharp.IconPictureBox();
            label6 = new Label();
            iconPictureBox7 = new FontAwesome.Sharp.IconPictureBox();
            label7 = new Label();
            iconPictureBox4 = new FontAwesome.Sharp.IconPictureBox();
            label4 = new Label();
            iconPictureBox3 = new FontAwesome.Sharp.IconPictureBox();
            label3 = new Label();
            iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            label2 = new Label();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            label1 = new Label();
            pnlIdintifiers = new Panel();
            btnSkip = new FontAwesome.Sharp.IconButton();
            btnAdd = new FontAwesome.Sharp.IconButton();
            txbxIdentifierID = new TextBox();
            cmbxIdentifierTypes = new ComboBox();
            lblTitle = new Label();
            openFileDialog1 = new OpenFileDialog();
            errorProvider1 = new ErrorProvider(components);
            pnlFullInfo = new Panel();
            pnlFullInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PcbxPerson).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            pnlIdintifiers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // pnlFullInfo
            // 
            resources.ApplyResources(pnlFullInfo, "pnlFullInfo");
            pnlFullInfo.Controls.Add(txbxPhone);
            pnlFullInfo.Controls.Add(dtBirthDate);
            pnlFullInfo.Controls.Add(btnRemoveImage);
            pnlFullInfo.Controls.Add(btnAddImage);
            pnlFullInfo.Controls.Add(btnSave);
            pnlFullInfo.Controls.Add(btnReset);
            pnlFullInfo.Controls.Add(PcbxPerson);
            pnlFullInfo.Controls.Add(txbxAddress);
            pnlFullInfo.Controls.Add(txBxEmail);
            pnlFullInfo.Controls.Add(iconPictureBox9);
            pnlFullInfo.Controls.Add(iconPictureBox8);
            pnlFullInfo.Controls.Add(rdbtnFemale);
            pnlFullInfo.Controls.Add(rdbtnMale);
            pnlFullInfo.Controls.Add(txbxSecondName);
            pnlFullInfo.Controls.Add(txbxThirdName);
            pnlFullInfo.Controls.Add(txbxLastName);
            pnlFullInfo.Controls.Add(txbxFirstName);
            pnlFullInfo.Controls.Add(iconPictureBox5);
            pnlFullInfo.Controls.Add(label5);
            pnlFullInfo.Controls.Add(iconPictureBox6);
            pnlFullInfo.Controls.Add(label6);
            pnlFullInfo.Controls.Add(iconPictureBox7);
            pnlFullInfo.Controls.Add(label7);
            pnlFullInfo.Controls.Add(iconPictureBox4);
            pnlFullInfo.Controls.Add(label4);
            pnlFullInfo.Controls.Add(iconPictureBox3);
            pnlFullInfo.Controls.Add(label3);
            pnlFullInfo.Controls.Add(iconPictureBox2);
            pnlFullInfo.Controls.Add(label2);
            pnlFullInfo.Controls.Add(iconPictureBox1);
            pnlFullInfo.Controls.Add(label1);
            pnlFullInfo.Cursor = Cursors.IBeam;
            errorProvider1.SetError(pnlFullInfo, resources.GetString("pnlFullInfo.Error"));
            errorProvider1.SetIconAlignment(pnlFullInfo, (ErrorIconAlignment)resources.GetObject("pnlFullInfo.IconAlignment"));
            errorProvider1.SetIconPadding(pnlFullInfo, (int)resources.GetObject("pnlFullInfo.IconPadding"));
            pnlFullInfo.Name = "pnlFullInfo";
            // 
            // txbxPhone
            // 
            resources.ApplyResources(txbxPhone, "txbxPhone");
            txbxPhone.Cursor = Cursors.IBeam;
            errorProvider1.SetError(txbxPhone, resources.GetString("txbxPhone.Error"));
            errorProvider1.SetIconAlignment(txbxPhone, (ErrorIconAlignment)resources.GetObject("txbxPhone.IconAlignment"));
            errorProvider1.SetIconPadding(txbxPhone, (int)resources.GetObject("txbxPhone.IconPadding"));
            txbxPhone.Name = "txbxPhone";
            txbxPhone.KeyPress += txbxPhone_KeyPress;
            txbxPhone.Validating += validatePhoneTextBox;
            // 
            // dtBirthDate
            // 
            resources.ApplyResources(dtBirthDate, "dtBirthDate");
            errorProvider1.SetError(dtBirthDate, resources.GetString("dtBirthDate.Error"));
            dtBirthDate.Format = DateTimePickerFormat.Short;
            errorProvider1.SetIconAlignment(dtBirthDate, (ErrorIconAlignment)resources.GetObject("dtBirthDate.IconAlignment"));
            errorProvider1.SetIconPadding(dtBirthDate, (int)resources.GetObject("dtBirthDate.IconPadding"));
            dtBirthDate.Name = "dtBirthDate";
            dtBirthDate.Value = new DateTime(2025, 7, 24, 0, 0, 0, 0);
            // 
            // btnRemoveImage
            // 
            resources.ApplyResources(btnRemoveImage, "btnRemoveImage");
            btnRemoveImage.CausesValidation = false;
            btnRemoveImage.Cursor = Cursors.Hand;
            errorProvider1.SetError(btnRemoveImage, resources.GetString("btnRemoveImage.Error"));
            errorProvider1.SetIconAlignment(btnRemoveImage, (ErrorIconAlignment)resources.GetObject("btnRemoveImage.IconAlignment"));
            btnRemoveImage.IconChar = FontAwesome.Sharp.IconChar.Trash;
            btnRemoveImage.IconColor = Color.Black;
            btnRemoveImage.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(btnRemoveImage, (int)resources.GetObject("btnRemoveImage.IconPadding"));
            btnRemoveImage.Name = "btnRemoveImage";
            btnRemoveImage.UseVisualStyleBackColor = true;
            btnRemoveImage.Click += btnRemoveImage_Click;
            // 
            // btnAddImage
            // 
            resources.ApplyResources(btnAddImage, "btnAddImage");
            btnAddImage.CausesValidation = false;
            btnAddImage.Cursor = Cursors.Hand;
            errorProvider1.SetError(btnAddImage, resources.GetString("btnAddImage.Error"));
            errorProvider1.SetIconAlignment(btnAddImage, (ErrorIconAlignment)resources.GetObject("btnAddImage.IconAlignment"));
            btnAddImage.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            btnAddImage.IconColor = Color.Black;
            btnAddImage.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(btnAddImage, (int)resources.GetObject("btnAddImage.IconPadding"));
            btnAddImage.Name = "btnAddImage";
            btnAddImage.UseVisualStyleBackColor = true;
            btnAddImage.Click += btnAddImage_Click;
            // 
            // btnSave
            // 
            resources.ApplyResources(btnSave, "btnSave");
            btnSave.Cursor = Cursors.Hand;
            errorProvider1.SetError(btnSave, resources.GetString("btnSave.Error"));
            errorProvider1.SetIconAlignment(btnSave, (ErrorIconAlignment)resources.GetObject("btnSave.IconAlignment"));
            btnSave.IconChar = FontAwesome.Sharp.IconChar.PersonCirclePlus;
            btnSave.IconColor = Color.Black;
            btnSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(btnSave, (int)resources.GetObject("btnSave.IconPadding"));
            btnSave.IconSize = 64;
            btnSave.Name = "btnSave";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnReset
            // 
            resources.ApplyResources(btnReset, "btnReset");
            btnReset.CausesValidation = false;
            btnReset.Cursor = Cursors.Hand;
            errorProvider1.SetError(btnReset, resources.GetString("btnReset.Error"));
            errorProvider1.SetIconAlignment(btnReset, (ErrorIconAlignment)resources.GetObject("btnReset.IconAlignment"));
            btnReset.IconChar = FontAwesome.Sharp.IconChar.Broom;
            btnReset.IconColor = Color.Black;
            btnReset.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(btnReset, (int)resources.GetObject("btnReset.IconPadding"));
            btnReset.IconSize = 64;
            btnReset.Name = "btnReset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += frmAddEditPersonInfo_Load;
            // 
            // PcbxPerson
            // 
            resources.ApplyResources(PcbxPerson, "PcbxPerson");
            PcbxPerson.BackColor = SystemColors.Control;
            errorProvider1.SetError(PcbxPerson, resources.GetString("PcbxPerson.Error"));
            PcbxPerson.ForeColor = SystemColors.ControlText;
            errorProvider1.SetIconAlignment(PcbxPerson, (ErrorIconAlignment)resources.GetObject("PcbxPerson.IconAlignment"));
            PcbxPerson.IconChar = FontAwesome.Sharp.IconChar.CameraAlt;
            PcbxPerson.IconColor = SystemColors.ControlText;
            PcbxPerson.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(PcbxPerson, (int)resources.GetObject("PcbxPerson.IconPadding"));
            PcbxPerson.IconSize = 165;
            PcbxPerson.Name = "PcbxPerson";
            PcbxPerson.TabStop = false;
            // 
            // txbxAddress
            // 
            resources.ApplyResources(txbxAddress, "txbxAddress");
            txbxAddress.Cursor = Cursors.IBeam;
            errorProvider1.SetError(txbxAddress, resources.GetString("txbxAddress.Error"));
            errorProvider1.SetIconAlignment(txbxAddress, (ErrorIconAlignment)resources.GetObject("txbxAddress.IconAlignment"));
            errorProvider1.SetIconPadding(txbxAddress, (int)resources.GetObject("txbxAddress.IconPadding"));
            txbxAddress.Name = "txbxAddress";
            // 
            // txBxEmail
            // 
            resources.ApplyResources(txBxEmail, "txBxEmail");
            txBxEmail.Cursor = Cursors.IBeam;
            errorProvider1.SetError(txBxEmail, resources.GetString("txBxEmail.Error"));
            errorProvider1.SetIconAlignment(txBxEmail, (ErrorIconAlignment)resources.GetObject("txBxEmail.IconAlignment"));
            errorProvider1.SetIconPadding(txBxEmail, (int)resources.GetObject("txBxEmail.IconPadding"));
            txBxEmail.Name = "txBxEmail";
            txBxEmail.Validating += txtEmail_Validating;
            // 
            // iconPictureBox9
            // 
            resources.ApplyResources(iconPictureBox9, "iconPictureBox9");
            iconPictureBox9.BackColor = SystemColors.Control;
            errorProvider1.SetError(iconPictureBox9, resources.GetString("iconPictureBox9.Error"));
            iconPictureBox9.ForeColor = SystemColors.ControlText;
            errorProvider1.SetIconAlignment(iconPictureBox9, (ErrorIconAlignment)resources.GetObject("iconPictureBox9.IconAlignment"));
            iconPictureBox9.IconChar = FontAwesome.Sharp.IconChar.Venus;
            iconPictureBox9.IconColor = SystemColors.ControlText;
            iconPictureBox9.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(iconPictureBox9, (int)resources.GetObject("iconPictureBox9.IconPadding"));
            iconPictureBox9.Name = "iconPictureBox9";
            iconPictureBox9.TabStop = false;
            // 
            // iconPictureBox8
            // 
            resources.ApplyResources(iconPictureBox8, "iconPictureBox8");
            iconPictureBox8.BackColor = SystemColors.Control;
            errorProvider1.SetError(iconPictureBox8, resources.GetString("iconPictureBox8.Error"));
            iconPictureBox8.ForeColor = SystemColors.ControlText;
            errorProvider1.SetIconAlignment(iconPictureBox8, (ErrorIconAlignment)resources.GetObject("iconPictureBox8.IconAlignment"));
            iconPictureBox8.IconChar = FontAwesome.Sharp.IconChar.Mars;
            iconPictureBox8.IconColor = SystemColors.ControlText;
            iconPictureBox8.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(iconPictureBox8, (int)resources.GetObject("iconPictureBox8.IconPadding"));
            iconPictureBox8.Name = "iconPictureBox8";
            iconPictureBox8.TabStop = false;
            // 
            // rdbtnFemale
            // 
            resources.ApplyResources(rdbtnFemale, "rdbtnFemale");
            errorProvider1.SetError(rdbtnFemale, resources.GetString("rdbtnFemale.Error"));
            errorProvider1.SetIconAlignment(rdbtnFemale, (ErrorIconAlignment)resources.GetObject("rdbtnFemale.IconAlignment"));
            errorProvider1.SetIconPadding(rdbtnFemale, (int)resources.GetObject("rdbtnFemale.IconPadding"));
            rdbtnFemale.Name = "rdbtnFemale";
            rdbtnFemale.UseVisualStyleBackColor = true;
            // 
            // rdbtnMale
            // 
            resources.ApplyResources(rdbtnMale, "rdbtnMale");
            rdbtnMale.Checked = true;
            errorProvider1.SetError(rdbtnMale, resources.GetString("rdbtnMale.Error"));
            errorProvider1.SetIconAlignment(rdbtnMale, (ErrorIconAlignment)resources.GetObject("rdbtnMale.IconAlignment"));
            errorProvider1.SetIconPadding(rdbtnMale, (int)resources.GetObject("rdbtnMale.IconPadding"));
            rdbtnMale.Name = "rdbtnMale";
            rdbtnMale.TabStop = true;
            rdbtnMale.UseVisualStyleBackColor = true;
            // 
            // txbxSecondName
            // 
            resources.ApplyResources(txbxSecondName, "txbxSecondName");
            txbxSecondName.Cursor = Cursors.IBeam;
            errorProvider1.SetError(txbxSecondName, resources.GetString("txbxSecondName.Error"));
            errorProvider1.SetIconAlignment(txbxSecondName, (ErrorIconAlignment)resources.GetObject("txbxSecondName.IconAlignment"));
            errorProvider1.SetIconPadding(txbxSecondName, (int)resources.GetObject("txbxSecondName.IconPadding"));
            txbxSecondName.Name = "txbxSecondName";
            txbxSecondName.KeyPress += txbx_KeyPressJustLetter;
            txbxSecondName.Validating += ValidateEmptyTextBox;
            // 
            // txbxThirdName
            // 
            resources.ApplyResources(txbxThirdName, "txbxThirdName");
            txbxThirdName.Cursor = Cursors.IBeam;
            errorProvider1.SetError(txbxThirdName, resources.GetString("txbxThirdName.Error"));
            errorProvider1.SetIconAlignment(txbxThirdName, (ErrorIconAlignment)resources.GetObject("txbxThirdName.IconAlignment"));
            errorProvider1.SetIconPadding(txbxThirdName, (int)resources.GetObject("txbxThirdName.IconPadding"));
            txbxThirdName.Name = "txbxThirdName";
            txbxThirdName.KeyPress += txbx_KeyPressJustLetter;
            txbxThirdName.Validating += ValidateEmptyTextBox;
            // 
            // txbxLastName
            // 
            resources.ApplyResources(txbxLastName, "txbxLastName");
            txbxLastName.Cursor = Cursors.IBeam;
            errorProvider1.SetError(txbxLastName, resources.GetString("txbxLastName.Error"));
            errorProvider1.SetIconAlignment(txbxLastName, (ErrorIconAlignment)resources.GetObject("txbxLastName.IconAlignment"));
            errorProvider1.SetIconPadding(txbxLastName, (int)resources.GetObject("txbxLastName.IconPadding"));
            txbxLastName.Name = "txbxLastName";
            txbxLastName.KeyPress += txbx_KeyPressJustLetter;
            txbxLastName.Validating += ValidateEmptyTextBox;
            // 
            // txbxFirstName
            // 
            resources.ApplyResources(txbxFirstName, "txbxFirstName");
            txbxFirstName.Cursor = Cursors.IBeam;
            errorProvider1.SetError(txbxFirstName, resources.GetString("txbxFirstName.Error"));
            errorProvider1.SetIconAlignment(txbxFirstName, (ErrorIconAlignment)resources.GetObject("txbxFirstName.IconAlignment"));
            errorProvider1.SetIconPadding(txbxFirstName, (int)resources.GetObject("txbxFirstName.IconPadding"));
            txbxFirstName.Name = "txbxFirstName";
            txbxFirstName.KeyPress += txbx_KeyPressJustLetter;
            txbxFirstName.Validating += ValidateEmptyTextBox;
            // 
            // iconPictureBox5
            // 
            resources.ApplyResources(iconPictureBox5, "iconPictureBox5");
            iconPictureBox5.BackColor = SystemColors.Control;
            errorProvider1.SetError(iconPictureBox5, resources.GetString("iconPictureBox5.Error"));
            iconPictureBox5.ForeColor = SystemColors.ControlText;
            errorProvider1.SetIconAlignment(iconPictureBox5, (ErrorIconAlignment)resources.GetObject("iconPictureBox5.IconAlignment"));
            iconPictureBox5.IconChar = FontAwesome.Sharp.IconChar.CameraAlt;
            iconPictureBox5.IconColor = SystemColors.ControlText;
            iconPictureBox5.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(iconPictureBox5, (int)resources.GetObject("iconPictureBox5.IconPadding"));
            iconPictureBox5.Name = "iconPictureBox5";
            iconPictureBox5.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            errorProvider1.SetError(label5, resources.GetString("label5.Error"));
            errorProvider1.SetIconAlignment(label5, (ErrorIconAlignment)resources.GetObject("label5.IconAlignment"));
            errorProvider1.SetIconPadding(label5, (int)resources.GetObject("label5.IconPadding"));
            label5.Name = "label5";
            // 
            // iconPictureBox6
            // 
            resources.ApplyResources(iconPictureBox6, "iconPictureBox6");
            iconPictureBox6.BackColor = SystemColors.Control;
            errorProvider1.SetError(iconPictureBox6, resources.GetString("iconPictureBox6.Error"));
            iconPictureBox6.ForeColor = SystemColors.ControlText;
            errorProvider1.SetIconAlignment(iconPictureBox6, (ErrorIconAlignment)resources.GetObject("iconPictureBox6.IconAlignment"));
            iconPictureBox6.IconChar = FontAwesome.Sharp.IconChar.Envelope;
            iconPictureBox6.IconColor = SystemColors.ControlText;
            iconPictureBox6.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(iconPictureBox6, (int)resources.GetObject("iconPictureBox6.IconPadding"));
            iconPictureBox6.Name = "iconPictureBox6";
            iconPictureBox6.TabStop = false;
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            errorProvider1.SetError(label6, resources.GetString("label6.Error"));
            errorProvider1.SetIconAlignment(label6, (ErrorIconAlignment)resources.GetObject("label6.IconAlignment"));
            errorProvider1.SetIconPadding(label6, (int)resources.GetObject("label6.IconPadding"));
            label6.Name = "label6";
            // 
            // iconPictureBox7
            // 
            resources.ApplyResources(iconPictureBox7, "iconPictureBox7");
            iconPictureBox7.BackColor = SystemColors.Control;
            errorProvider1.SetError(iconPictureBox7, resources.GetString("iconPictureBox7.Error"));
            iconPictureBox7.ForeColor = SystemColors.ControlText;
            errorProvider1.SetIconAlignment(iconPictureBox7, (ErrorIconAlignment)resources.GetObject("iconPictureBox7.IconAlignment"));
            iconPictureBox7.IconChar = FontAwesome.Sharp.IconChar.CalendarPlus;
            iconPictureBox7.IconColor = SystemColors.ControlText;
            iconPictureBox7.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(iconPictureBox7, (int)resources.GetObject("iconPictureBox7.IconPadding"));
            iconPictureBox7.Name = "iconPictureBox7";
            iconPictureBox7.TabStop = false;
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            errorProvider1.SetError(label7, resources.GetString("label7.Error"));
            errorProvider1.SetIconAlignment(label7, (ErrorIconAlignment)resources.GetObject("label7.IconAlignment"));
            errorProvider1.SetIconPadding(label7, (int)resources.GetObject("label7.IconPadding"));
            label7.Name = "label7";
            // 
            // iconPictureBox4
            // 
            resources.ApplyResources(iconPictureBox4, "iconPictureBox4");
            iconPictureBox4.BackColor = SystemColors.Control;
            errorProvider1.SetError(iconPictureBox4, resources.GetString("iconPictureBox4.Error"));
            iconPictureBox4.ForeColor = SystemColors.ControlText;
            errorProvider1.SetIconAlignment(iconPictureBox4, (ErrorIconAlignment)resources.GetObject("iconPictureBox4.IconAlignment"));
            iconPictureBox4.IconChar = FontAwesome.Sharp.IconChar.MapMarkerAlt;
            iconPictureBox4.IconColor = SystemColors.ControlText;
            iconPictureBox4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(iconPictureBox4, (int)resources.GetObject("iconPictureBox4.IconPadding"));
            iconPictureBox4.Name = "iconPictureBox4";
            iconPictureBox4.TabStop = false;
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            errorProvider1.SetError(label4, resources.GetString("label4.Error"));
            errorProvider1.SetIconAlignment(label4, (ErrorIconAlignment)resources.GetObject("label4.IconAlignment"));
            errorProvider1.SetIconPadding(label4, (int)resources.GetObject("label4.IconPadding"));
            label4.Name = "label4";
            // 
            // iconPictureBox3
            // 
            resources.ApplyResources(iconPictureBox3, "iconPictureBox3");
            iconPictureBox3.BackColor = SystemColors.Control;
            errorProvider1.SetError(iconPictureBox3, resources.GetString("iconPictureBox3.Error"));
            iconPictureBox3.ForeColor = SystemColors.ControlText;
            errorProvider1.SetIconAlignment(iconPictureBox3, (ErrorIconAlignment)resources.GetObject("iconPictureBox3.IconAlignment"));
            iconPictureBox3.IconChar = FontAwesome.Sharp.IconChar.PhoneSquareAlt;
            iconPictureBox3.IconColor = SystemColors.ControlText;
            iconPictureBox3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(iconPictureBox3, (int)resources.GetObject("iconPictureBox3.IconPadding"));
            iconPictureBox3.Name = "iconPictureBox3";
            iconPictureBox3.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            errorProvider1.SetError(label3, resources.GetString("label3.Error"));
            errorProvider1.SetIconAlignment(label3, (ErrorIconAlignment)resources.GetObject("label3.IconAlignment"));
            errorProvider1.SetIconPadding(label3, (int)resources.GetObject("label3.IconPadding"));
            label3.Name = "label3";
            // 
            // iconPictureBox2
            // 
            resources.ApplyResources(iconPictureBox2, "iconPictureBox2");
            iconPictureBox2.BackColor = SystemColors.Control;
            errorProvider1.SetError(iconPictureBox2, resources.GetString("iconPictureBox2.Error"));
            iconPictureBox2.ForeColor = SystemColors.ControlText;
            errorProvider1.SetIconAlignment(iconPictureBox2, (ErrorIconAlignment)resources.GetObject("iconPictureBox2.IconAlignment"));
            iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.Genderless;
            iconPictureBox2.IconColor = SystemColors.ControlText;
            iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(iconPictureBox2, (int)resources.GetObject("iconPictureBox2.IconPadding"));
            iconPictureBox2.Name = "iconPictureBox2";
            iconPictureBox2.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            errorProvider1.SetError(label2, resources.GetString("label2.Error"));
            errorProvider1.SetIconAlignment(label2, (ErrorIconAlignment)resources.GetObject("label2.IconAlignment"));
            errorProvider1.SetIconPadding(label2, (int)resources.GetObject("label2.IconPadding"));
            label2.Name = "label2";
            // 
            // iconPictureBox1
            // 
            resources.ApplyResources(iconPictureBox1, "iconPictureBox1");
            iconPictureBox1.BackColor = SystemColors.Control;
            errorProvider1.SetError(iconPictureBox1, resources.GetString("iconPictureBox1.Error"));
            iconPictureBox1.ForeColor = SystemColors.ControlText;
            errorProvider1.SetIconAlignment(iconPictureBox1, (ErrorIconAlignment)resources.GetObject("iconPictureBox1.IconAlignment"));
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.User;
            iconPictureBox1.IconColor = SystemColors.ControlText;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(iconPictureBox1, (int)resources.GetObject("iconPictureBox1.IconPadding"));
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            errorProvider1.SetError(label1, resources.GetString("label1.Error"));
            errorProvider1.SetIconAlignment(label1, (ErrorIconAlignment)resources.GetObject("label1.IconAlignment"));
            errorProvider1.SetIconPadding(label1, (int)resources.GetObject("label1.IconPadding"));
            label1.Name = "label1";
            // 
            // pnlIdintifiers
            // 
            resources.ApplyResources(pnlIdintifiers, "pnlIdintifiers");
            pnlIdintifiers.Controls.Add(btnSkip);
            pnlIdintifiers.Controls.Add(btnAdd);
            pnlIdintifiers.Controls.Add(txbxIdentifierID);
            pnlIdintifiers.Controls.Add(cmbxIdentifierTypes);
            errorProvider1.SetError(pnlIdintifiers, resources.GetString("pnlIdintifiers.Error"));
            errorProvider1.SetIconAlignment(pnlIdintifiers, (ErrorIconAlignment)resources.GetObject("pnlIdintifiers.IconAlignment"));
            errorProvider1.SetIconPadding(pnlIdintifiers, (int)resources.GetObject("pnlIdintifiers.IconPadding"));
            pnlIdintifiers.Name = "pnlIdintifiers";
            // 
            // btnSkip
            // 
            resources.ApplyResources(btnSkip, "btnSkip");
            btnSkip.CausesValidation = false;
            btnSkip.Cursor = Cursors.Hand;
            errorProvider1.SetError(btnSkip, resources.GetString("btnSkip.Error"));
            errorProvider1.SetIconAlignment(btnSkip, (ErrorIconAlignment)resources.GetObject("btnSkip.IconAlignment"));
            btnSkip.IconChar = FontAwesome.Sharp.IconChar.MailForward;
            btnSkip.IconColor = Color.Black;
            btnSkip.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(btnSkip, (int)resources.GetObject("btnSkip.IconPadding"));
            btnSkip.Name = "btnSkip";
            btnSkip.UseVisualStyleBackColor = true;
            btnSkip.Click += btnSkip_Click;
            // 
            // btnAdd
            // 
            resources.ApplyResources(btnAdd, "btnAdd");
            btnAdd.CausesValidation = false;
            btnAdd.Cursor = Cursors.Hand;
            errorProvider1.SetError(btnAdd, resources.GetString("btnAdd.Error"));
            errorProvider1.SetIconAlignment(btnAdd, (ErrorIconAlignment)resources.GetObject("btnAdd.IconAlignment"));
            btnAdd.IconChar = FontAwesome.Sharp.IconChar.PlusSquare;
            btnAdd.IconColor = Color.Black;
            btnAdd.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(btnAdd, (int)resources.GetObject("btnAdd.IconPadding"));
            btnAdd.Name = "btnAdd";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // txbxIdentifierID
            // 
            resources.ApplyResources(txbxIdentifierID, "txbxIdentifierID");
            txbxIdentifierID.Cursor = Cursors.IBeam;
            errorProvider1.SetError(txbxIdentifierID, resources.GetString("txbxIdentifierID.Error"));
            errorProvider1.SetIconAlignment(txbxIdentifierID, (ErrorIconAlignment)resources.GetObject("txbxIdentifierID.IconAlignment"));
            errorProvider1.SetIconPadding(txbxIdentifierID, (int)resources.GetObject("txbxIdentifierID.IconPadding"));
            txbxIdentifierID.Name = "txbxIdentifierID";
            txbxIdentifierID.TextChanged += txbxIdentifierID_TextChanged;
            // 
            // cmbxIdentifierTypes
            // 
            resources.ApplyResources(cmbxIdentifierTypes, "cmbxIdentifierTypes");
            cmbxIdentifierTypes.AllowDrop = true;
            cmbxIdentifierTypes.Cursor = Cursors.Hand;
            cmbxIdentifierTypes.DropDownHeight = 400;
            cmbxIdentifierTypes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbxIdentifierTypes.DropDownWidth = 20;
            errorProvider1.SetError(cmbxIdentifierTypes, resources.GetString("cmbxIdentifierTypes.Error"));
            cmbxIdentifierTypes.ForeColor = Color.Black;
            cmbxIdentifierTypes.FormattingEnabled = true;
            errorProvider1.SetIconAlignment(cmbxIdentifierTypes, (ErrorIconAlignment)resources.GetObject("cmbxIdentifierTypes.IconAlignment"));
            errorProvider1.SetIconPadding(cmbxIdentifierTypes, (int)resources.GetObject("cmbxIdentifierTypes.IconPadding"));
            cmbxIdentifierTypes.Name = "cmbxIdentifierTypes";
            // 
            // lblTitle
            // 
            resources.ApplyResources(lblTitle, "lblTitle");
            errorProvider1.SetError(lblTitle, resources.GetString("lblTitle.Error"));
            lblTitle.FlatStyle = FlatStyle.Flat;
            lblTitle.ForeColor = Color.Black;
            errorProvider1.SetIconAlignment(lblTitle, (ErrorIconAlignment)resources.GetObject("lblTitle.IconAlignment"));
            errorProvider1.SetIconPadding(lblTitle, (int)resources.GetObject("lblTitle.IconPadding"));
            lblTitle.Name = "lblTitle";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            resources.ApplyResources(openFileDialog1, "openFileDialog1");
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            resources.ApplyResources(errorProvider1, "errorProvider1");
            // 
            // frmAddEditPersonInfo
            // 
            AcceptButton = btnSave;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Inherit;
            Controls.Add(lblTitle);
            Controls.Add(pnlIdintifiers);
            Controls.Add(pnlFullInfo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmAddEditPersonInfo";
            ShowInTaskbar = false;
            Load += frmAddEditPersonInfo_Load;
            KeyDown += frmAddEditPersonInfo_KeyDown;
            pnlFullInfo.ResumeLayout(false);
            pnlFullInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PcbxPerson).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            pnlIdintifiers.ResumeLayout(false);
            pnlIdintifiers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlFullInfo;
        private Panel pnlIdintifiers;
        private ComboBox cmbxIdentifierTypes;
        private Label lblTitle;
        private TextBox txbxIdentifierID;
        private FontAwesome.Sharp.IconButton btnAdd;
        private FontAwesome.Sharp.IconButton btnSkip;
        private Label label1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox3;
        private Label label3;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private Label label2;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox6;
        private Label label6;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox7;
        private Label label7;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox4;
        private Label label4;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox5;
        private Label label5;
        private TextBox txbxFirstName;
        private TextBox txbxSecondName;
        private TextBox txbxThirdName;
        private TextBox txbxLastName;
        private RadioButton rdbtnFemale;
        private RadioButton rdbtnMale;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox9;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox8;
        private TextBox txBxEmail;
        private TextBox txbxAddress;
        private FontAwesome.Sharp.IconPictureBox PcbxPerson;
        private FontAwesome.Sharp.IconButton btnSave;
        private FontAwesome.Sharp.IconButton btnReset;
        private FontAwesome.Sharp.IconButton btnAddImage;
        private FontAwesome.Sharp.IconButton btnRemoveImage;
        private DateTimePicker dtBirthDate;
        private OpenFileDialog openFileDialog1;
        private ErrorProvider errorProvider1;
        private TextBox txbxPhone;
    }
}