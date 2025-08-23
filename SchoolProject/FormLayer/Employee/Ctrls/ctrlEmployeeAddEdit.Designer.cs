namespace FromSide.Employee
{
    partial class ctrlEmployeeAddEdit
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlEmployeeAddEdit));
            checkBox1 = new CheckBox();
            rbtnNotActve = new RadioButton();
            rbtnActive = new RadioButton();
            iconPictureBox8 = new FontAwesome.Sharp.IconPictureBox();
            iconPictureBox5 = new FontAwesome.Sharp.IconPictureBox();
            label6 = new Label();
            iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            iconPictureBox3 = new FontAwesome.Sharp.IconPictureBox();
            txbxPersonID = new TextBox();
            txbxSalary = new TextBox();
            label5 = new Label();
            label3 = new Label();
            label2 = new Label();
            txbxNotes = new TextBox();
            dtTerminationDate = new DateTimePicker();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            label1 = new Label();
            dtHireDate = new DateTimePicker();
            btnSave = new FontAwesome.Sharp.IconButton();
            cmbxJobTitles = new ComboBox();
            btnReset = new FontAwesome.Sharp.IconButton();
            iconPictureBox7 = new FontAwesome.Sharp.IconPictureBox();
            label7 = new Label();
            iconPictureBox4 = new FontAwesome.Sharp.IconPictureBox();
            label4 = new Label();
            errorProvider1 = new ErrorProvider(components);
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // checkBox1
            // 
            resources.ApplyResources(checkBox1, "checkBox1");
            checkBox1.Cursor = Cursors.Hand;
            errorProvider1.SetError(checkBox1, resources.GetString("checkBox1.Error"));
            errorProvider1.SetIconAlignment(checkBox1, (ErrorIconAlignment)resources.GetObject("checkBox1.IconAlignment"));
            errorProvider1.SetIconPadding(checkBox1, (int)resources.GetObject("checkBox1.IconPadding"));
            checkBox1.Name = "checkBox1";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // rbtnNotActve
            // 
            resources.ApplyResources(rbtnNotActve, "rbtnNotActve");
            errorProvider1.SetError(rbtnNotActve, resources.GetString("rbtnNotActve.Error"));
            errorProvider1.SetIconAlignment(rbtnNotActve, (ErrorIconAlignment)resources.GetObject("rbtnNotActve.IconAlignment"));
            errorProvider1.SetIconPadding(rbtnNotActve, (int)resources.GetObject("rbtnNotActve.IconPadding"));
            rbtnNotActve.Name = "rbtnNotActve";
            rbtnNotActve.UseVisualStyleBackColor = true;
            // 
            // rbtnActive
            // 
            resources.ApplyResources(rbtnActive, "rbtnActive");
            rbtnActive.Checked = true;
            errorProvider1.SetError(rbtnActive, resources.GetString("rbtnActive.Error"));
            errorProvider1.SetIconAlignment(rbtnActive, (ErrorIconAlignment)resources.GetObject("rbtnActive.IconAlignment"));
            errorProvider1.SetIconPadding(rbtnActive, (int)resources.GetObject("rbtnActive.IconPadding"));
            rbtnActive.Name = "rbtnActive";
            rbtnActive.TabStop = true;
            rbtnActive.UseVisualStyleBackColor = true;
            // 
            // iconPictureBox8
            // 
            resources.ApplyResources(iconPictureBox8, "iconPictureBox8");
            iconPictureBox8.BackColor = SystemColors.Control;
            errorProvider1.SetError(iconPictureBox8, resources.GetString("iconPictureBox8.Error"));
            iconPictureBox8.ForeColor = SystemColors.ControlText;
            errorProvider1.SetIconAlignment(iconPictureBox8, (ErrorIconAlignment)resources.GetObject("iconPictureBox8.IconAlignment"));
            iconPictureBox8.IconChar = FontAwesome.Sharp.IconChar.IdBadge;
            iconPictureBox8.IconColor = SystemColors.ControlText;
            iconPictureBox8.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(iconPictureBox8, (int)resources.GetObject("iconPictureBox8.IconPadding"));
            iconPictureBox8.Name = "iconPictureBox8";
            iconPictureBox8.TabStop = false;
            // 
            // iconPictureBox5
            // 
            resources.ApplyResources(iconPictureBox5, "iconPictureBox5");
            iconPictureBox5.BackColor = SystemColors.Control;
            errorProvider1.SetError(iconPictureBox5, resources.GetString("iconPictureBox5.Error"));
            iconPictureBox5.ForeColor = SystemColors.ControlText;
            errorProvider1.SetIconAlignment(iconPictureBox5, (ErrorIconAlignment)resources.GetObject("iconPictureBox5.IconAlignment"));
            iconPictureBox5.IconChar = FontAwesome.Sharp.IconChar.FileClipboard;
            iconPictureBox5.IconColor = SystemColors.ControlText;
            iconPictureBox5.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(iconPictureBox5, (int)resources.GetObject("iconPictureBox5.IconPadding"));
            iconPictureBox5.Name = "iconPictureBox5";
            iconPictureBox5.TabStop = false;
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            errorProvider1.SetError(label6, resources.GetString("label6.Error"));
            errorProvider1.SetIconAlignment(label6, (ErrorIconAlignment)resources.GetObject("label6.IconAlignment"));
            errorProvider1.SetIconPadding(label6, (int)resources.GetObject("label6.IconPadding"));
            label6.Name = "label6";
            // 
            // iconPictureBox2
            // 
            resources.ApplyResources(iconPictureBox2, "iconPictureBox2");
            iconPictureBox2.BackColor = SystemColors.Control;
            errorProvider1.SetError(iconPictureBox2, resources.GetString("iconPictureBox2.Error"));
            iconPictureBox2.ForeColor = SystemColors.ControlText;
            errorProvider1.SetIconAlignment(iconPictureBox2, (ErrorIconAlignment)resources.GetObject("iconPictureBox2.IconAlignment"));
            iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.MoneyCheckDollar;
            iconPictureBox2.IconColor = SystemColors.ControlText;
            iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(iconPictureBox2, (int)resources.GetObject("iconPictureBox2.IconPadding"));
            iconPictureBox2.Name = "iconPictureBox2";
            iconPictureBox2.TabStop = false;
            // 
            // iconPictureBox3
            // 
            resources.ApplyResources(iconPictureBox3, "iconPictureBox3");
            iconPictureBox3.BackColor = SystemColors.Control;
            errorProvider1.SetError(iconPictureBox3, resources.GetString("iconPictureBox3.Error"));
            iconPictureBox3.ForeColor = SystemColors.ControlText;
            errorProvider1.SetIconAlignment(iconPictureBox3, (ErrorIconAlignment)resources.GetObject("iconPictureBox3.IconAlignment"));
            iconPictureBox3.IconChar = FontAwesome.Sharp.IconChar.UserCheck;
            iconPictureBox3.IconColor = SystemColors.ControlText;
            iconPictureBox3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(iconPictureBox3, (int)resources.GetObject("iconPictureBox3.IconPadding"));
            iconPictureBox3.Name = "iconPictureBox3";
            iconPictureBox3.TabStop = false;
            // 
            // txbxPersonID
            // 
            resources.ApplyResources(txbxPersonID, "txbxPersonID");
            txbxPersonID.Cursor = Cursors.No;
            errorProvider1.SetError(txbxPersonID, resources.GetString("txbxPersonID.Error"));
            errorProvider1.SetIconAlignment(txbxPersonID, (ErrorIconAlignment)resources.GetObject("txbxPersonID.IconAlignment"));
            errorProvider1.SetIconPadding(txbxPersonID, (int)resources.GetObject("txbxPersonID.IconPadding"));
            txbxPersonID.Name = "txbxPersonID";
            txbxPersonID.ReadOnly = true;
            // 
            // txbxSalary
            // 
            resources.ApplyResources(txbxSalary, "txbxSalary");
            txbxSalary.Cursor = Cursors.IBeam;
            errorProvider1.SetError(txbxSalary, resources.GetString("txbxSalary.Error"));
            errorProvider1.SetIconAlignment(txbxSalary, (ErrorIconAlignment)resources.GetObject("txbxSalary.IconAlignment"));
            errorProvider1.SetIconPadding(txbxSalary, (int)resources.GetObject("txbxSalary.IconPadding"));
            txbxSalary.Name = "txbxSalary";
            txbxSalary.KeyPress += txbxSalary_KeyPress;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            errorProvider1.SetError(label5, resources.GetString("label5.Error"));
            errorProvider1.SetIconAlignment(label5, (ErrorIconAlignment)resources.GetObject("label5.IconAlignment"));
            errorProvider1.SetIconPadding(label5, (int)resources.GetObject("label5.IconPadding"));
            label5.Name = "label5";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            errorProvider1.SetError(label3, resources.GetString("label3.Error"));
            errorProvider1.SetIconAlignment(label3, (ErrorIconAlignment)resources.GetObject("label3.IconAlignment"));
            errorProvider1.SetIconPadding(label3, (int)resources.GetObject("label3.IconPadding"));
            label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            errorProvider1.SetError(label2, resources.GetString("label2.Error"));
            errorProvider1.SetIconAlignment(label2, (ErrorIconAlignment)resources.GetObject("label2.IconAlignment"));
            errorProvider1.SetIconPadding(label2, (int)resources.GetObject("label2.IconPadding"));
            label2.Name = "label2";
            // 
            // txbxNotes
            // 
            resources.ApplyResources(txbxNotes, "txbxNotes");
            txbxNotes.Cursor = Cursors.IBeam;
            errorProvider1.SetError(txbxNotes, resources.GetString("txbxNotes.Error"));
            errorProvider1.SetIconAlignment(txbxNotes, (ErrorIconAlignment)resources.GetObject("txbxNotes.IconAlignment"));
            errorProvider1.SetIconPadding(txbxNotes, (int)resources.GetObject("txbxNotes.IconPadding"));
            txbxNotes.Name = "txbxNotes";
            // 
            // dtTerminationDate
            // 
            resources.ApplyResources(dtTerminationDate, "dtTerminationDate");
            dtTerminationDate.Cursor = Cursors.Hand;
            errorProvider1.SetError(dtTerminationDate, resources.GetString("dtTerminationDate.Error"));
            dtTerminationDate.Format = DateTimePickerFormat.Short;
            errorProvider1.SetIconAlignment(dtTerminationDate, (ErrorIconAlignment)resources.GetObject("dtTerminationDate.IconAlignment"));
            errorProvider1.SetIconPadding(dtTerminationDate, (int)resources.GetObject("dtTerminationDate.IconPadding"));
            dtTerminationDate.Name = "dtTerminationDate";
            dtTerminationDate.Value = new DateTime(2025, 7, 24, 0, 0, 0, 0);
            // 
            // iconPictureBox1
            // 
            resources.ApplyResources(iconPictureBox1, "iconPictureBox1");
            iconPictureBox1.BackColor = SystemColors.Control;
            errorProvider1.SetError(iconPictureBox1, resources.GetString("iconPictureBox1.Error"));
            iconPictureBox1.ForeColor = SystemColors.ControlText;
            errorProvider1.SetIconAlignment(iconPictureBox1, (ErrorIconAlignment)resources.GetObject("iconPictureBox1.IconAlignment"));
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.CalendarMinus;
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
            // dtHireDate
            // 
            resources.ApplyResources(dtHireDate, "dtHireDate");
            dtHireDate.Cursor = Cursors.Hand;
            errorProvider1.SetError(dtHireDate, resources.GetString("dtHireDate.Error"));
            dtHireDate.Format = DateTimePickerFormat.Short;
            errorProvider1.SetIconAlignment(dtHireDate, (ErrorIconAlignment)resources.GetObject("dtHireDate.IconAlignment"));
            errorProvider1.SetIconPadding(dtHireDate, (int)resources.GetObject("dtHireDate.IconPadding"));
            dtHireDate.Name = "dtHireDate";
            dtHireDate.Value = new DateTime(2025, 7, 24, 0, 0, 0, 0);
            // 
            // btnSave
            // 
            resources.ApplyResources(btnSave, "btnSave");
            btnSave.Cursor = Cursors.Hand;
            errorProvider1.SetError(btnSave, resources.GetString("btnSave.Error"));
            errorProvider1.SetIconAlignment(btnSave, (ErrorIconAlignment)resources.GetObject("btnSave.IconAlignment"));
            btnSave.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnSave.IconColor = Color.Black;
            btnSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(btnSave, (int)resources.GetObject("btnSave.IconPadding"));
            btnSave.Name = "btnSave";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // cmbxJobTitles
            // 
            resources.ApplyResources(cmbxJobTitles, "cmbxJobTitles");
            cmbxJobTitles.AllowDrop = true;
            cmbxJobTitles.Cursor = Cursors.Hand;
            cmbxJobTitles.DropDownHeight = 400;
            cmbxJobTitles.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbxJobTitles.DropDownWidth = 20;
            errorProvider1.SetError(cmbxJobTitles, resources.GetString("cmbxJobTitles.Error"));
            cmbxJobTitles.ForeColor = Color.Black;
            cmbxJobTitles.FormattingEnabled = true;
            errorProvider1.SetIconAlignment(cmbxJobTitles, (ErrorIconAlignment)resources.GetObject("cmbxJobTitles.IconAlignment"));
            errorProvider1.SetIconPadding(cmbxJobTitles, (int)resources.GetObject("cmbxJobTitles.IconPadding"));
            cmbxJobTitles.Name = "cmbxJobTitles";
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
            btnReset.Name = "btnReset";
            btnReset.UseVisualStyleBackColor = true;
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
            iconPictureBox4.IconChar = FontAwesome.Sharp.IconChar.Briefcase;
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
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            resources.ApplyResources(errorProvider1, "errorProvider1");
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.BackColor = Color.Black;
            errorProvider1.SetError(panel1, resources.GetString("panel1.Error"));
            errorProvider1.SetIconAlignment(panel1, (ErrorIconAlignment)resources.GetObject("panel1.IconAlignment"));
            errorProvider1.SetIconPadding(panel1, (int)resources.GetObject("panel1.IconPadding"));
            panel1.Name = "panel1";
            // 
            // ctrlEmployeeAddEdit
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Controls.Add(checkBox1);
            Controls.Add(rbtnNotActve);
            Controls.Add(rbtnActive);
            Controls.Add(iconPictureBox8);
            Controls.Add(iconPictureBox5);
            Controls.Add(label6);
            Controls.Add(iconPictureBox2);
            Controls.Add(iconPictureBox3);
            Controls.Add(txbxPersonID);
            Controls.Add(txbxSalary);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txbxNotes);
            Controls.Add(dtTerminationDate);
            Controls.Add(iconPictureBox1);
            Controls.Add(label1);
            Controls.Add(dtHireDate);
            Controls.Add(btnSave);
            Controls.Add(cmbxJobTitles);
            Controls.Add(btnReset);
            Controls.Add(iconPictureBox7);
            Controls.Add(label7);
            Controls.Add(iconPictureBox4);
            Controls.Add(label4);
            errorProvider1.SetError(this, resources.GetString("$this.Error"));
            errorProvider1.SetIconAlignment(this, (ErrorIconAlignment)resources.GetObject("$this.IconAlignment"));
            errorProvider1.SetIconPadding(this, (int)resources.GetObject("$this.IconPadding"));
            Name = "ctrlEmployeeAddEdit";
            Load += ctrlEmployeeAddEdit_Load;
            ((System.ComponentModel.ISupportInitialize)iconPictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox checkBox1;
        private RadioButton rbtnNotActve;
        private RadioButton rbtnActive;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox8;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox5;
        private Label label6;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox3;
        private TextBox txbxPersonID;
        private TextBox txbxSalary;
        private Label label5;
        private Label label3;
        private Label label2;
        private TextBox txbxNotes;
        private DateTimePicker dtTerminationDate;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Label label1;
        private DateTimePicker dtHireDate;
        private FontAwesome.Sharp.IconButton btnSave;
        private ComboBox cmbxJobTitles;
        private FontAwesome.Sharp.IconButton btnReset;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox7;
        private Label label7;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox4;
        private Label label4;
        private ErrorProvider errorProvider1;
        private Panel panel1;
    }
}
