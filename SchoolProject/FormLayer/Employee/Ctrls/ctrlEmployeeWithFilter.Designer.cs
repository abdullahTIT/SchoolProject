namespace FromSide.Employee.Ctrls
{
    partial class ctrlEmployeeWithFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlEmployeeWithFilter));
            btnAddEmployee = new FontAwesome.Sharp.IconButton();
            grbxfilter = new GroupBox();
            btnSearch = new FontAwesome.Sharp.IconButton();
            txbxSearch = new TextBox();
            cmbxFilterBy = new ComboBox();
            lblName = new Label();
            ctrlEmployeecard1 = new FromSide.Persons.Ctrls.ctrlEmployeecard();
            errorProvider1 = new ErrorProvider(components);
            grbxfilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // btnAddEmployee
            // 
            resources.ApplyResources(btnAddEmployee, "btnAddEmployee");
            btnAddEmployee.CausesValidation = false;
            btnAddEmployee.Cursor = Cursors.Hand;
            btnAddEmployee.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            btnAddEmployee.IconColor = Color.Black;
            btnAddEmployee.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAddEmployee.IconSize = 45;
            btnAddEmployee.Name = "btnAddEmployee";
            btnAddEmployee.UseVisualStyleBackColor = true;
            btnAddEmployee.Click += btnAddEmployee_Click;
            // 
            // grbxfilter
            // 
            resources.ApplyResources(grbxfilter, "grbxfilter");
            grbxfilter.Controls.Add(btnAddEmployee);
            grbxfilter.Controls.Add(btnSearch);
            grbxfilter.Controls.Add(txbxSearch);
            grbxfilter.Controls.Add(cmbxFilterBy);
            grbxfilter.Controls.Add(lblName);
            grbxfilter.FlatStyle = FlatStyle.Flat;
            grbxfilter.Name = "grbxfilter";
            grbxfilter.TabStop = false;
            // 
            // btnSearch
            // 
            resources.ApplyResources(btnSearch, "btnSearch");
            btnSearch.CausesValidation = false;
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.IconChar = FontAwesome.Sharp.IconChar.Search;
            btnSearch.IconColor = Color.Black;
            btnSearch.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSearch.IconSize = 40;
            btnSearch.Name = "btnSearch";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // txbxSearch
            // 
            resources.ApplyResources(txbxSearch, "txbxSearch");
            txbxSearch.Cursor = Cursors.IBeam;
            txbxSearch.Name = "txbxSearch";
            txbxSearch.TextChanged += txbxSearch_TextChanged;
            txbxSearch.KeyDown += txbxSearch_KeyDown;
            txbxSearch.KeyPress += txbxSearch_KeyPress;
            // 
            // cmbxFilterBy
            // 
            cmbxFilterBy.AllowDrop = true;
            resources.ApplyResources(cmbxFilterBy, "cmbxFilterBy");
            cmbxFilterBy.Cursor = Cursors.Hand;
            cmbxFilterBy.DropDownHeight = 400;
            cmbxFilterBy.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbxFilterBy.DropDownWidth = 20;
            cmbxFilterBy.ForeColor = Color.Black;
            cmbxFilterBy.FormattingEnabled = true;
            cmbxFilterBy.Items.AddRange(new object[] { resources.GetString("cmbxFilterBy.Items"), resources.GetString("cmbxFilterBy.Items1"), resources.GetString("cmbxFilterBy.Items2") });
            cmbxFilterBy.Name = "cmbxFilterBy";
            cmbxFilterBy.SelectedIndexChanged += cmbxFilterBy_SelectedIndexChanged;
            // 
            // lblName
            // 
            resources.ApplyResources(lblName, "lblName");
            lblName.FlatStyle = FlatStyle.Flat;
            lblName.ForeColor = Color.Black;
            lblName.Name = "lblName";
            // 
            // ctrlEmployeecard1
            // 
            resources.ApplyResources(ctrlEmployeecard1, "ctrlEmployeecard1");
            ctrlEmployeecard1.ForeColor = SystemColors.ControlDarkDark;
            ctrlEmployeecard1.Name = "ctrlEmployeecard1";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // ctrlEmployeeWithFilter
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ctrlEmployeecard1);
            Controls.Add(grbxfilter);
            Name = "ctrlEmployeeWithFilter";
            Load += ctrlEmployeeWithFilter_Load;
            grbxfilter.ResumeLayout(false);
            grbxfilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private FontAwesome.Sharp.IconButton btnAddEmployee;
        private GroupBox grbxfilter;
        private FontAwesome.Sharp.IconButton btnSearch;
        private TextBox txbxSearch;
        private ComboBox cmbxFilterBy;
        private Label lblName;
        private Persons.Ctrls.ctrlEmployeecard ctrlEmployeecard1;
        private ErrorProvider errorProvider1;
    }
}
