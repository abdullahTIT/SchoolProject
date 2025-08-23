namespace FormLayer.Main.ctrl
{
    partial class SettingsCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsCtrl));
            label1 = new Label();
            label2 = new Label();
            cbxLanguage = new ComboBox();
            cbxTheme = new ComboBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // cbxLanguage
            // 
            cbxLanguage.FormattingEnabled = true;
            cbxLanguage.Items.AddRange(new object[] { resources.GetString("cbxLanguage.Items"), resources.GetString("cbxLanguage.Items1") });
            resources.ApplyResources(cbxLanguage, "cbxLanguage");
            cbxLanguage.Name = "cbxLanguage";
            cbxLanguage.SelectedIndexChanged += cbxLanguage_SelectedIndexChanged;
            // 
            // cbxTheme
            // 
            cbxTheme.FormattingEnabled = true;
            cbxTheme.Items.AddRange(new object[] { resources.GetString("cbxTheme.Items"), resources.GetString("cbxTheme.Items1"), resources.GetString("cbxTheme.Items2") });
            resources.ApplyResources(cbxTheme, "cbxTheme");
            cbxTheme.Name = "cbxTheme";
            cbxTheme.SelectedIndexChanged += cbxTheme_SelectedIndexChanged;
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // SettingsCtrl
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label4);
            Controls.Add(cbxTheme);
            Controls.Add(cbxLanguage);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "SettingsCtrl";
            Load += SettingsCtrl_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox cbxLanguage;
        private ComboBox cbxTheme;
        private Label label4;
    }
}
