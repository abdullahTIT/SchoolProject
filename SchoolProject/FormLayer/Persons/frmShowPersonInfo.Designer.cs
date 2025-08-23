namespace FromSide.Persons
{
    partial class frmShowPersonInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowPersonInfo));
            lblTitle = new Label();
            ctrlPersoncard1 = new FromSide.Persons.Ctrls.ctrlPersoncard();
            SuspendLayout();
            // 
            // lblTitle
            // 
            resources.ApplyResources(lblTitle, "lblTitle");
            lblTitle.FlatStyle = FlatStyle.Flat;
            lblTitle.ForeColor = Color.Black;
            lblTitle.Name = "lblTitle";
            // 
            // ctrlPersoncard1
            // 
            resources.ApplyResources(ctrlPersoncard1, "ctrlPersoncard1");
            ctrlPersoncard1.ForeColor = SystemColors.ControlDarkDark;
            ctrlPersoncard1.Name = "ctrlPersoncard1";
            // 
            // frmShowPersonInfo
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            Controls.Add(ctrlPersoncard1);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmShowPersonInfo";
            ShowIcon = false;
            Load += frmShowPersonInfo_Load;
            KeyDown += frmShowPersonInfo_KeyDown;
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private Ctrls.ctrlPersoncard ctrlPersoncard1;
    }
}