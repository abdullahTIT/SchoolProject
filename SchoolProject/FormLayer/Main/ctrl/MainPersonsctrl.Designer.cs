namespace FormLayer.Main.ctrl
{
    partial class MainPersonctrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPersonctrl));
            panel1 = new Panel();
            btnShowList = new FontAwesome.Sharp.IconButton();
            btnFind = new FontAwesome.Sharp.IconButton();
            btnUpdate = new FontAwesome.Sharp.IconButton();
            btnAddEditPerson = new FontAwesome.Sharp.IconButton();
            label1 = new Label();
            label2 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.Controls.Add(btnShowList);
            panel1.Controls.Add(btnFind);
            panel1.Controls.Add(btnUpdate);
            panel1.Controls.Add(btnAddEditPerson);
            panel1.Name = "panel1";
            // 
            // btnShowList
            // 
            resources.ApplyResources(btnShowList, "btnShowList");
            btnShowList.IconChar = FontAwesome.Sharp.IconChar.UsersViewfinder;
            btnShowList.IconColor = Color.Black;
            btnShowList.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnShowList.Name = "btnShowList";
            btnShowList.UseVisualStyleBackColor = true;
            btnShowList.Click += btnShowList_Click;
            // 
            // btnFind
            // 
            resources.ApplyResources(btnFind, "btnFind");
            btnFind.IconChar = FontAwesome.Sharp.IconChar.Search;
            btnFind.IconColor = Color.Black;
            btnFind.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnFind.Name = "btnFind";
            btnFind.UseVisualStyleBackColor = true;
            btnFind.Click += btnFind_Click;
            // 
            // btnUpdate
            // 
            resources.ApplyResources(btnUpdate, "btnUpdate");
            btnUpdate.IconChar = FontAwesome.Sharp.IconChar.UserPen;
            btnUpdate.IconColor = Color.Black;
            btnUpdate.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnUpdate.Name = "btnUpdate";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnAddEditPerson
            // 
            resources.ApplyResources(btnAddEditPerson, "btnAddEditPerson");
            btnAddEditPerson.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            btnAddEditPerson.IconColor = Color.Black;
            btnAddEditPerson.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAddEditPerson.Name = "btnAddEditPerson";
            btnAddEditPerson.UseVisualStyleBackColor = true;
            btnAddEditPerson.Click += btnAddEditPerson_Click;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.FlatStyle = FlatStyle.Flat;
            label2.ForeColor = SystemColors.ActiveBorder;
            label2.Name = "label2";
            // 
            // MainPersonctrl
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(panel1);
            Name = "MainPersonctrl";
            Load += MainPersonctrl_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private FontAwesome.Sharp.IconButton btnShowList;
        private FontAwesome.Sharp.IconButton btnFind;
        private FontAwesome.Sharp.IconButton btnUpdate;
        private FontAwesome.Sharp.IconButton btnAddEditPerson;
        private Label label1;
        private Label label2;
    }
}
