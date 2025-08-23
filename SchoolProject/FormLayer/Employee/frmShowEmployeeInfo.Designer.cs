namespace FromSide.Persons
{
    partial class frmShowEmployeeInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowEmployeeInfo));
            lblTitle = new Label();
            ctrlEmployeecard1 = new FromSide.Persons.Ctrls.ctrlEmployeecard();
            SuspendLayout();
            // 
            // lblTitle
            // 
            resources.ApplyResources(lblTitle, "lblTitle");
            lblTitle.FlatStyle = FlatStyle.Flat;
            lblTitle.ForeColor = Color.Black;
            lblTitle.Name = "lblTitle";
            // 
            // ctrlEmployeecard1
            // 
            resources.ApplyResources(ctrlEmployeecard1, "ctrlEmployeecard1");
            ctrlEmployeecard1.ForeColor = SystemColors.ControlDarkDark;
            ctrlEmployeecard1.Name = "ctrlEmployeecard1";
            // 
            // frmShowEmployeeInfo
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Inherit;
            Controls.Add(ctrlEmployeecard1);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmShowEmployeeInfo";
            ShowIcon = false;
            Load += frmShowEmployeeInfo_Load;
            KeyDown += frmShowPersonInfo_KeyDown;
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private Ctrls.ctrlEmployeecard ctrlEmployeecard1;
    }
}