namespace FromSide.Persons.Ctrls
{
    partial class ctrlPersonCardWithFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlPersonCardWithFilter));
            ctrlPersoncard1 = new ctrlPersoncard();
            grbxfilter = new GroupBox();
            btnSearch = new FontAwesome.Sharp.IconButton();
            btnAddPerson = new FontAwesome.Sharp.IconButton();
            txbxSearch = new TextBox();
            cmbxFilterBy = new ComboBox();
            lblName = new Label();
            errorProvider1 = new ErrorProvider(components);
            grbxfilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // ctrlPersoncard1
            // 
            resources.ApplyResources(ctrlPersoncard1, "ctrlPersoncard1");
            errorProvider1.SetError(ctrlPersoncard1, resources.GetString("ctrlPersoncard1.Error"));
            ctrlPersoncard1.ForeColor = SystemColors.ControlDarkDark;
            errorProvider1.SetIconAlignment(ctrlPersoncard1, (ErrorIconAlignment)resources.GetObject("ctrlPersoncard1.IconAlignment"));
            errorProvider1.SetIconPadding(ctrlPersoncard1, (int)resources.GetObject("ctrlPersoncard1.IconPadding"));
            ctrlPersoncard1.Name = "ctrlPersoncard1";
            // 
            // grbxfilter
            // 
            resources.ApplyResources(grbxfilter, "grbxfilter");
            grbxfilter.Controls.Add(btnSearch);
            grbxfilter.Controls.Add(btnAddPerson);
            grbxfilter.Controls.Add(txbxSearch);
            grbxfilter.Controls.Add(cmbxFilterBy);
            grbxfilter.Controls.Add(lblName);
            errorProvider1.SetError(grbxfilter, resources.GetString("grbxfilter.Error"));
            grbxfilter.FlatStyle = FlatStyle.Flat;
            errorProvider1.SetIconAlignment(grbxfilter, (ErrorIconAlignment)resources.GetObject("grbxfilter.IconAlignment"));
            errorProvider1.SetIconPadding(grbxfilter, (int)resources.GetObject("grbxfilter.IconPadding"));
            grbxfilter.Name = "grbxfilter";
            grbxfilter.TabStop = false;
            // 
            // btnSearch
            // 
            resources.ApplyResources(btnSearch, "btnSearch");
            btnSearch.CausesValidation = false;
            btnSearch.Cursor = Cursors.Hand;
            errorProvider1.SetError(btnSearch, resources.GetString("btnSearch.Error"));
            errorProvider1.SetIconAlignment(btnSearch, (ErrorIconAlignment)resources.GetObject("btnSearch.IconAlignment"));
            btnSearch.IconChar = FontAwesome.Sharp.IconChar.Search;
            btnSearch.IconColor = Color.Black;
            btnSearch.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(btnSearch, (int)resources.GetObject("btnSearch.IconPadding"));
            btnSearch.IconSize = 40;
            btnSearch.Name = "btnSearch";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnAddPerson
            // 
            resources.ApplyResources(btnAddPerson, "btnAddPerson");
            btnAddPerson.CausesValidation = false;
            btnAddPerson.Cursor = Cursors.Hand;
            errorProvider1.SetError(btnAddPerson, resources.GetString("btnAddPerson.Error"));
            errorProvider1.SetIconAlignment(btnAddPerson, (ErrorIconAlignment)resources.GetObject("btnAddPerson.IconAlignment"));
            btnAddPerson.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            btnAddPerson.IconColor = Color.Black;
            btnAddPerson.IconFont = FontAwesome.Sharp.IconFont.Auto;
            errorProvider1.SetIconPadding(btnAddPerson, (int)resources.GetObject("btnAddPerson.IconPadding"));
            btnAddPerson.IconSize = 45;
            btnAddPerson.Name = "btnAddPerson";
            btnAddPerson.UseVisualStyleBackColor = true;
            btnAddPerson.Click += btnAdd_Click;
            // 
            // txbxSearch
            // 
            resources.ApplyResources(txbxSearch, "txbxSearch");
            txbxSearch.Cursor = Cursors.IBeam;
            errorProvider1.SetError(txbxSearch, resources.GetString("txbxSearch.Error"));
            errorProvider1.SetIconAlignment(txbxSearch, (ErrorIconAlignment)resources.GetObject("txbxSearch.IconAlignment"));
            errorProvider1.SetIconPadding(txbxSearch, (int)resources.GetObject("txbxSearch.IconPadding"));
            txbxSearch.Name = "txbxSearch";
            txbxSearch.TextChanged += txbxSearch_TextChanged;
            txbxSearch.KeyDown += txbxSearch_KeyDown;
            txbxSearch.KeyPress += txbxSearch_KeyPress;
            // 
            // cmbxFilterBy
            // 
            resources.ApplyResources(cmbxFilterBy, "cmbxFilterBy");
            cmbxFilterBy.AllowDrop = true;
            cmbxFilterBy.Cursor = Cursors.Hand;
            cmbxFilterBy.DropDownHeight = 400;
            cmbxFilterBy.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbxFilterBy.DropDownWidth = 20;
            errorProvider1.SetError(cmbxFilterBy, resources.GetString("cmbxFilterBy.Error"));
            cmbxFilterBy.ForeColor = Color.Black;
            cmbxFilterBy.FormattingEnabled = true;
            errorProvider1.SetIconAlignment(cmbxFilterBy, (ErrorIconAlignment)resources.GetObject("cmbxFilterBy.IconAlignment"));
            errorProvider1.SetIconPadding(cmbxFilterBy, (int)resources.GetObject("cmbxFilterBy.IconPadding"));
            cmbxFilterBy.Items.AddRange(new object[] { resources.GetString("cmbxFilterBy.Items"), resources.GetString("cmbxFilterBy.Items1") });
            cmbxFilterBy.Name = "cmbxFilterBy";
            cmbxFilterBy.SelectedIndexChanged += cmbxFilterBy_SelectedIndexChanged;
            // 
            // lblName
            // 
            resources.ApplyResources(lblName, "lblName");
            errorProvider1.SetError(lblName, resources.GetString("lblName.Error"));
            lblName.FlatStyle = FlatStyle.Flat;
            lblName.ForeColor = Color.Black;
            errorProvider1.SetIconAlignment(lblName, (ErrorIconAlignment)resources.GetObject("lblName.IconAlignment"));
            errorProvider1.SetIconPadding(lblName, (int)resources.GetObject("lblName.IconPadding"));
            lblName.Name = "lblName";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            resources.ApplyResources(errorProvider1, "errorProvider1");
            // 
            // ctrlPersonCardWithFilter
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Inherit;
            Controls.Add(grbxfilter);
            Controls.Add(ctrlPersoncard1);
            errorProvider1.SetError(this, resources.GetString("$this.Error"));
            errorProvider1.SetIconAlignment(this, (ErrorIconAlignment)resources.GetObject("$this.IconAlignment"));
            errorProvider1.SetIconPadding(this, (int)resources.GetObject("$this.IconPadding"));
            Name = "ctrlPersonCardWithFilter";
            Load += ctrlPersonCardWithFilter_Load;
            grbxfilter.ResumeLayout(false);
            grbxfilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ctrlPersoncard ctrlPersoncard1;
        private GroupBox grbxfilter;
        private FontAwesome.Sharp.IconButton btnSearch;
        private FontAwesome.Sharp.IconButton btnAddPerson;
        private TextBox txbxSearch;
        private ComboBox cmbxFilterBy;
        private Label lblName;
        private ErrorProvider errorProvider1;
    }
}
