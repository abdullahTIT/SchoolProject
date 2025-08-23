namespace FormLayer
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            panel2 = new Panel();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            btnPerson = new FontAwesome.Sharp.IconButton();
            pnlInfo = new Panel();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.Controls.Add(iconButton2);
            panel2.Controls.Add(iconButton1);
            panel2.Controls.Add(btnPerson);
            resources.ApplyResources(panel2, "panel2");
            panel2.Name = "panel2";
            // 
            // iconButton2
            // 
            resources.ApplyResources(iconButton2, "iconButton2");
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.Cog;
            iconButton2.IconColor = Color.Black;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.Name = "iconButton2";
            iconButton2.UseVisualStyleBackColor = true;
            iconButton2.Click += iconButton2_Click;
            // 
            // iconButton1
            // 
            resources.ApplyResources(iconButton1, "iconButton1");
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.Users;
            iconButton1.IconColor = Color.Black;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.Name = "iconButton1";
            iconButton1.UseVisualStyleBackColor = true;
            iconButton1.Click += iconButton1_Click;
            // 
            // btnPerson
            // 
            resources.ApplyResources(btnPerson, "btnPerson");
            btnPerson.IconChar = FontAwesome.Sharp.IconChar.PersonCircleQuestion;
            btnPerson.IconColor = Color.Black;
            btnPerson.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnPerson.Name = "btnPerson";
            btnPerson.UseVisualStyleBackColor = true;
            btnPerson.Click += btnPerson_Click;
            // 
            // pnlInfo
            // 
            resources.ApplyResources(pnlInfo, "pnlInfo");
            pnlInfo.Name = "pnlInfo";
            // 
            // frmMain
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlInfo);
            Controls.Add(panel2);
            Name = "frmMain";
            FormClosing += frmMain_FormClosing;
            Load += frmMain_Load;
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private Panel pnlInfo;
        private FontAwesome.Sharp.IconButton btnPerson;
        private FontAwesome.Sharp.IconButton iconButton1;
        private FontAwesome.Sharp.IconButton iconButton2;
    }
}
