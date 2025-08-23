using System.Windows.Forms;

namespace FromSide.Persons
{
    partial class frmEmployeesLists
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmployeesLists));
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            lblTitle = new Label();
            grbxfilter = new GroupBox();
            dateTimePicker1 = new DateTimePicker();
            btnSearch = new FontAwesome.Sharp.IconButton();
            btnAddPerson = new FontAwesome.Sharp.IconButton();
            txbxSearch = new TextBox();
            cmbxFilterBy = new ComboBox();
            lblName = new Label();
            dtgrdvEmployees = new DataGridView();
            cmsPersons = new ContextMenuStrip(components);
            showDetailsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            addNewPersonToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            sendEmailToolStripMenuItem = new ToolStripMenuItem();
            phoneCallToolStripMenuItem = new ToolStripMenuItem();
            grbxfilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgrdvEmployees).BeginInit();
            cmsPersons.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            resources.ApplyResources(lblTitle, "lblTitle");
            lblTitle.FlatStyle = FlatStyle.Flat;
            lblTitle.ForeColor = Color.Black;
            lblTitle.Name = "lblTitle";
            // 
            // grbxfilter
            // 
            resources.ApplyResources(grbxfilter, "grbxfilter");
            grbxfilter.Controls.Add(dateTimePicker1);
            grbxfilter.Controls.Add(btnSearch);
            grbxfilter.Controls.Add(btnAddPerson);
            grbxfilter.Controls.Add(txbxSearch);
            grbxfilter.Controls.Add(cmbxFilterBy);
            grbxfilter.Controls.Add(lblName);
            grbxfilter.FlatStyle = FlatStyle.Flat;
            grbxfilter.Name = "grbxfilter";
            grbxfilter.TabStop = false;
            // 
            // dateTimePicker1
            // 
            resources.ApplyResources(dateTimePicker1, "dateTimePicker1");
            dateTimePicker1.MaxDate = new DateTime(2050, 1, 1, 0, 0, 0, 0);
            dateTimePicker1.MinDate = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            dateTimePicker1.Name = "dateTimePicker1";
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
            btnSearch.Click += txbxSearch_TextChanged;
            // 
            // btnAddPerson
            // 
            resources.ApplyResources(btnAddPerson, "btnAddPerson");
            btnAddPerson.CausesValidation = false;
            btnAddPerson.Cursor = Cursors.Hand;
            btnAddPerson.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            btnAddPerson.IconColor = Color.Black;
            btnAddPerson.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAddPerson.IconSize = 45;
            btnAddPerson.Name = "btnAddPerson";
            btnAddPerson.UseVisualStyleBackColor = true;
            btnAddPerson.Click += addNewEmployeeToolStripMenuItem_Click;
            // 
            // txbxSearch
            // 
            resources.ApplyResources(txbxSearch, "txbxSearch");
            txbxSearch.Cursor = Cursors.IBeam;
            txbxSearch.Name = "txbxSearch";
            txbxSearch.TextChanged += txbxSearch_TextChanged;
            // 
            // cmbxFilterBy
            // 
            resources.ApplyResources(cmbxFilterBy, "cmbxFilterBy");
            cmbxFilterBy.AllowDrop = true;
            cmbxFilterBy.Cursor = Cursors.Hand;
            cmbxFilterBy.DropDownHeight = 400;
            cmbxFilterBy.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbxFilterBy.DropDownWidth = 20;
            cmbxFilterBy.ForeColor = Color.Black;
            cmbxFilterBy.FormattingEnabled = true;
            cmbxFilterBy.Items.AddRange(new object[] { resources.GetString("cmbxFilterBy.Items"), resources.GetString("cmbxFilterBy.Items1"), resources.GetString("cmbxFilterBy.Items2"), resources.GetString("cmbxFilterBy.Items3"), resources.GetString("cmbxFilterBy.Items4"), resources.GetString("cmbxFilterBy.Items5"), resources.GetString("cmbxFilterBy.Items6"), resources.GetString("cmbxFilterBy.Items7") });
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
            // dtgrdvEmployees
            // 
            resources.ApplyResources(dtgrdvEmployees, "dtgrdvEmployees");
            dtgrdvEmployees.AllowUserToAddRows = false;
            dtgrdvEmployees.AllowUserToDeleteRows = false;
            dtgrdvEmployees.AllowUserToResizeRows = false;
            dtgrdvEmployees.BackgroundColor = Color.White;
            dtgrdvEmployees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgrdvEmployees.ContextMenuStrip = cmsPersons;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dtgrdvEmployees.DefaultCellStyle = dataGridViewCellStyle3;
            dtgrdvEmployees.EditMode = DataGridViewEditMode.EditProgrammatically;
            dtgrdvEmployees.MultiSelect = false;
            dtgrdvEmployees.Name = "dtgrdvEmployees";
            dtgrdvEmployees.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle4.ForeColor = Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dtgrdvEmployees.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dtgrdvEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgrdvEmployees.TabStop = false;
            dtgrdvEmployees.DoubleClick += dtgrdvEmployee_DoubleClick;
            // 
            // cmsPersons
            // 
            resources.ApplyResources(cmsPersons, "cmsPersons");
            cmsPersons.ImageScalingSize = new Size(24, 24);
            cmsPersons.Items.AddRange(new ToolStripItem[] { showDetailsToolStripMenuItem, toolStripSeparator1, addNewPersonToolStripMenuItem, editToolStripMenuItem, deleteToolStripMenuItem, toolStripSeparator2, sendEmailToolStripMenuItem, phoneCallToolStripMenuItem });
            cmsPersons.Name = "cmsPersons";
            cmsPersons.RenderMode = ToolStripRenderMode.Professional;
            // 
            // showDetailsToolStripMenuItem
            // 
            resources.ApplyResources(showDetailsToolStripMenuItem, "showDetailsToolStripMenuItem");
            showDetailsToolStripMenuItem.Name = "showDetailsToolStripMenuItem";
            showDetailsToolStripMenuItem.Click += showDetailsToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // addNewPersonToolStripMenuItem
            // 
            resources.ApplyResources(addNewPersonToolStripMenuItem, "addNewPersonToolStripMenuItem");
            addNewPersonToolStripMenuItem.Name = "addNewPersonToolStripMenuItem";
            addNewPersonToolStripMenuItem.Click += addNewEmployeeToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            resources.ApplyResources(editToolStripMenuItem, "editToolStripMenuItem");
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Click += editToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            resources.ApplyResources(deleteToolStripMenuItem, "deleteToolStripMenuItem");
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // sendEmailToolStripMenuItem
            // 
            resources.ApplyResources(sendEmailToolStripMenuItem, "sendEmailToolStripMenuItem");
            sendEmailToolStripMenuItem.Name = "sendEmailToolStripMenuItem";
            sendEmailToolStripMenuItem.Click += sendEmailToolStripMenuItem_Click;
            // 
            // phoneCallToolStripMenuItem
            // 
            resources.ApplyResources(phoneCallToolStripMenuItem, "phoneCallToolStripMenuItem");
            phoneCallToolStripMenuItem.Name = "phoneCallToolStripMenuItem";
            phoneCallToolStripMenuItem.Click += phoneCallToolStripMenuItem_Click;
            // 
            // frmEmployeesLists
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(dtgrdvEmployees);
            Controls.Add(grbxfilter);
            Controls.Add(lblTitle);
            Name = "frmEmployeesLists";
            WindowState = FormWindowState.Maximized;
            Load += frmEmployeesLists_Load;
            KeyDown += frmEmployeesLists_KeyDown;
            grbxfilter.ResumeLayout(false);
            grbxfilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgrdvEmployees).EndInit();
            cmsPersons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private GroupBox grbxfilter;
        private FontAwesome.Sharp.IconButton btnSearch;
        private FontAwesome.Sharp.IconButton btnAddPerson;
        private TextBox txbxSearch;
        private ComboBox cmbxFilterBy;
        private Label lblName;
        private DataGridView dtgrdvEmployees;
        private ContextMenuStrip cmsPersons;
        private ToolStripMenuItem showDetailsToolStripMenuItem;
        private ToolStripMenuItem addNewPersonToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem sendEmailToolStripMenuItem;
        private ToolStripMenuItem phoneCallToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private DateTimePicker dateTimePicker1;
    }
}