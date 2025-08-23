namespace FormLayer.LoginScreen
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            btnLogIn = new FontAwesome.Sharp.IconButton();
            txbxUserName = new TextBox();
            txbxPassword = new TextBox();
            cbxSaveLogInfo = new CheckBox();
            errorProvider1 = new ErrorProvider(components);
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // btnLogIn
            // 
            btnLogIn.Cursor = Cursors.Hand;
            resources.ApplyResources(btnLogIn, "btnLogIn");
            btnLogIn.IconChar = FontAwesome.Sharp.IconChar.LockOpen;
            btnLogIn.IconColor = Color.Black;
            btnLogIn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnLogIn.IconSize = 40;
            btnLogIn.Name = "btnLogIn";
            btnLogIn.UseVisualStyleBackColor = true;
            btnLogIn.Click += btnLogIn_Click;
            // 
            // txbxUserName
            // 
            txbxUserName.Cursor = Cursors.IBeam;
            resources.ApplyResources(txbxUserName, "txbxUserName");
            txbxUserName.Name = "txbxUserName";
            txbxUserName.TextChanged += txbxUserName_TextChanged;
            txbxUserName.KeyPress += txbxUserName_KeyPress;
            // 
            // txbxPassword
            // 
            txbxPassword.Cursor = Cursors.IBeam;
            resources.ApplyResources(txbxPassword, "txbxPassword");
            txbxPassword.Name = "txbxPassword";
            txbxPassword.TextChanged += txbxPasswrod_TextChanged;
            // 
            // cbxSaveLogInfo
            // 
            resources.ApplyResources(cbxSaveLogInfo, "cbxSaveLogInfo");
            cbxSaveLogInfo.Cursor = Cursors.Hand;
            cbxSaveLogInfo.FlatAppearance.BorderSize = 0;
            cbxSaveLogInfo.Name = "cbxSaveLogInfo";
            cbxSaveLogInfo.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // frmLogin
            // 
            AutoScaleMode = AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            Controls.Add(label1);
            Controls.Add(cbxSaveLogInfo);
            Controls.Add(txbxPassword);
            Controls.Add(btnLogIn);
            Controls.Add(txbxUserName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmLogin";
            SizeGripStyle = SizeGripStyle.Hide;
            Load += LoginScreen_Load;
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FontAwesome.Sharp.IconButton btnLogIn;
        private TextBox txbxUserName;
        private TextBox txbxPassword;
        private CheckBox cbxSaveLogInfo;
        private ErrorProvider errorProvider1;
        private Label label1;
    }
}