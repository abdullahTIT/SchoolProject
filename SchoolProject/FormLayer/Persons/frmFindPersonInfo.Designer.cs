namespace FromSide
{
    partial class frmFindPersonInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFindPersonInfo));
            lblTitle = new Label();
            ctrlPersonCardWithFilter1 = new FromSide.Persons.Ctrls.ctrlPersonCardWithFilter();
            SuspendLayout();
            // 
            // lblTitle
            // 
            resources.ApplyResources(lblTitle, "lblTitle");
            lblTitle.FlatStyle = FlatStyle.Flat;
            lblTitle.ForeColor = Color.Black;
            lblTitle.Name = "lblTitle";
            // 
            // ctrlPersonCardWithFilter1
            // 
            resources.ApplyResources(ctrlPersonCardWithFilter1, "ctrlPersonCardWithFilter1");
            ctrlPersonCardWithFilter1.Name = "ctrlPersonCardWithFilter1";
            ctrlPersonCardWithFilter1.ShowAddPerson = true;
            // 
            // frmFindPersonInfo
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Inherit;
            Controls.Add(ctrlPersonCardWithFilter1);
            Controls.Add(lblTitle);
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmFindPersonInfo";
            ShowIcon = false;
            ShowInTaskbar = false;
            FormClosing += frmFindPersonInfo_FormClosing;
            Load += frmFindPersonInfo_Load;
            KeyDown += frmFindPersonInfo_KeyDown;
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private Persons.Ctrls.ctrlPersonCardWithFilter ctrlPersonCardWithFilter1;
    }
}