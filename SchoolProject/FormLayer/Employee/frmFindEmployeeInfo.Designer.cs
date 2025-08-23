namespace FromSide
{
    partial class frmFindEmployeeInfo
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
            lblTitle = new Label();
            ctrlEmployeeWithFilter1 = new FromSide.Employee.Ctrls.ctrlEmployeeWithFilter();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.FlatStyle = FlatStyle.Flat;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(563, 50);
            lblTitle.TabIndex = 1000;
            lblTitle.Text = "Find Employee";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ctrlEmployeeWithFilter1
            // 
            ctrlEmployeeWithFilter1.Font = new Font("Segoe UI", 12F);
            ctrlEmployeeWithFilter1.Location = new Point(0, 54);
            ctrlEmployeeWithFilter1.Margin = new Padding(4);
            ctrlEmployeeWithFilter1.Name = "ctrlEmployeeWithFilter1";
            ctrlEmployeeWithFilter1.Size = new Size(563, 592);
            ctrlEmployeeWithFilter1.TabIndex = 1001;
            // 
            // frmFindEmployeeInfo
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            ClientSize = new Size(563, 644);
            Controls.Add(ctrlEmployeeWithFilter1);
            Controls.Add(lblTitle);
            Font = new Font("Segoe UI", 12F);
            KeyPreview = true;
            Margin = new Padding(4);
            MaximizeBox = false;
            MaximumSize = new Size(700, 700);
            MinimizeBox = false;
            MinimumSize = new Size(585, 490);
            Name = "frmFindEmployeeInfo";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Find Employee";
            FormClosing += frmFindPersonInfo_FormClosing;
            Load += frmFindEmployeeInfo_Load;
            KeyDown += frmFindPersonInfo_KeyDown;
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private Employee.Ctrls.ctrlEmployeeWithFilter ctrlPersonCardWithFilter1;
        private Employee.Ctrls.ctrlEmployeeWithFilter ctrlEmployeeWithFilter1;
    }
}