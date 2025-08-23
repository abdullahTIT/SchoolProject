using FontAwesome.Sharp;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace FromSide.General
{
    public static class ConfigManager
    {
        private static IConfigurationRoot _config;
        private static string _jsonPath;

        static ConfigManager()
        {
            if (!IsInDesignMode())
            {
                _jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                _config = builder.Build();
            }
        }
        private static bool IsInDesignMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
                   System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv";
        }
        public static string GetLanguage() =>
            _config["AppSettings:Language"] ?? "en";

        public static string GetTheme() =>
            _config["AppSettings:Theme"] ?? "Light";

        public static void SetLanguage(string lang) =>
            UpdateJsonValue("AppSettings:Language", lang);

        public static void SetTheme(string theme) =>
            UpdateJsonValue("AppSettings:Theme", theme);

        public static Dictionary<string, string> GetThemeColors(string themeName)
        {
            var themeSection = _config.GetSection($"Themes:{themeName}");
            var colors = new Dictionary<string, string>();
            foreach (var kvp in themeSection.GetChildren())
                colors[kvp.Key] = kvp.Value;
            return colors;
        }

        private static void UpdateJsonValue(string key, string value)
        {
            var json = File.ReadAllText(_jsonPath);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            var parts = key.Split(':');
            dynamic section = jsonObj;
            for (int i = 0; i < parts.Length - 1; i++)
            {
                section = section[parts[i]];
            }

            section[parts[^1]] = value;
            File.WriteAllText(_jsonPath, Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented));
        }
    }
    public static class AppFonts
    {
        public static PrivateFontCollection pfc = new PrivateFontCollection();
        public static List<FontFamily> CairoFontFamilies = new List<FontFamily>();
        public static Font DefaultFont;

        static AppFonts()
        {
            try
            {
                string fontPath = Path.Combine(Application.StartupPath, "General", "Cairo", "static", "CarioMain.ttf");
                pfc.AddFontFile(fontPath);
                DefaultFont = new Font(pfc.Families[0], 10f, FontStyle.Regular);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
        public static void ApplyFontToAllControls(Control parent, Font font)
        {

            parent.Font = font;
            foreach (Control c in parent.Controls)
            {
                ApplyFontToAllControls(c, font);
            }
        }
        public static void ApplyFontWithStyleCheck(Control parent, Font baseFont)
        {
            FontStyle style = parent.Font.Bold ? FontStyle.Bold : FontStyle.Regular;

            parent.Font = new Font(baseFont.FontFamily, baseFont.Size, style);

            foreach (Control c in parent.Controls)
            {
                ApplyFontWithStyleCheck(c, baseFont);
            }
        }
        public static void ApplyFontToForm(Control parent, FontFamily fontFamily, float fontSize = 10f)
        {
            // نحدد ستايل الخط الأصلي (Bold أو Regular)
            FontStyle style = parent.Font.Bold ? FontStyle.Bold : FontStyle.Regular;

            // نطبق الخط الجديد مع الحجم والستايل المطلوبين
            parent.Font = new Font(fontFamily, fontSize, style);

            // نكرر نفس العملية لكل الكنترولز الفرعية
            foreach (Control c in parent.Controls)
            {
                ApplyFontToForm(c, fontFamily, fontSize);
            }
        }
        public static void ApplyFontToControl(Control control, FontFamily fontFamily, float fontSize = 10f)
        {
            FontStyle style = control.Font.Bold ? FontStyle.Bold : FontStyle.Regular;
            control.Font = new Font(fontFamily, fontSize, style);
        }
    }

    public static class LanguageManager
    {
        public static string CurrentLanguage { get; private set; }

        public static void ApplyLanguage()
        {
            string langCode = ConfigManager.GetLanguage();
            CurrentLanguage = langCode;

            var culture = new CultureInfo(langCode);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }
    }

    public static class ThemeManager
    {
        public static void ApplyTheme(Form form)
        {
            // 1. قراءة الإعدادات مرة واحدة فقط في البداية
            string themeName = ConfigManager.GetTheme();
            var colors = ConfigManager.GetThemeColors(themeName);

            // 2. تحويل الألوان من HTML إلى كائنات Color مرة واحدة
            Color backColor = ColorTranslator.FromHtml(colors["BackColor"]);
            Color foreColor = ColorTranslator.FromHtml(colors["ForeColor"]);
            Color buttonBack = ColorTranslator.FromHtml(colors["ButtonBackColor"]);
            Color buttonFore = ColorTranslator.FromHtml(colors["ButtonForeColor"]);

            form.BackColor = backColor;
            form.ForeColor = foreColor;

            // 4. تطبيق الثيم على كل العناصر داخل النموذج باستخدام الدالة المساعدة
            foreach (Control ctrl in form.Controls)
            {
                ApplyThemeToControl(ctrl, backColor, foreColor, buttonBack, buttonFore);
            }
        }

        public static void ApplyThemeToControl(Control control)
        {
            string themeName = ConfigManager.GetTheme();
            var colors = ConfigManager.GetThemeColors(themeName);

            Color backColor = ColorTranslator.FromHtml(colors["BackColor"]);
            Color foreColor = ColorTranslator.FromHtml(colors["ForeColor"]);
            Color buttonBack = ColorTranslator.FromHtml(colors["ButtonBackColor"]);
            Color buttonFore = ColorTranslator.FromHtml(colors["ButtonForeColor"]);

            ApplyThemeToControl(control, backColor, foreColor, buttonBack, buttonFore);
        }

        private static void ApplyThemeToControl(Control control, Color backColor, Color foreColor, Color buttonBack, Color buttonFore)
        {
            switch (control)
            {
                case IconButton ibtn:
                    ibtn.BackColor = buttonBack;
                    ibtn.ForeColor = buttonFore;
                    ibtn.IconColor = buttonFore;
                    //ibtn.FlatStyle = FlatStyle.Flat;
                    //ibtn.FlatAppearance.BorderSize = 0;
                    break;

                case Button btn when !(btn is IconButton):
                    btn.BackColor = buttonBack;
                    btn.ForeColor = buttonFore;
                    //btn.FlatStyle = FlatStyle.Flat;
                    //btn.FlatAppearance.BorderSize = 0;
                    break;

                case ComboBox cb:
                    cb.BackColor = buttonBack;
                    cb.ForeColor = buttonFore;
                    //cb.FlatStyle = FlatStyle.Flat;
                    break;

                case IconPictureBox ipic:
                    ipic.BackColor = Color.Transparent;
                    ipic.ForeColor = buttonFore;
                    ipic.IconColor = buttonFore;
                    break;
                case DataGridView dgv:
                    dgv.BackgroundColor = backColor;
                    dgv.ForeColor = foreColor;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = buttonBack;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = buttonFore;
                    dgv.RowsDefaultCellStyle.BackColor = backColor;
                    dgv.RowsDefaultCellStyle.ForeColor = foreColor;
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = buttonBack; // للصفوف المتناوبة
                    dgv.EnableHeadersVisualStyles = false; // ضروري لتطبيق ألوان العناوين
                    break;
                default:
                    control.BackColor = backColor;
                    control.ForeColor = foreColor;
                    break;

            }

            foreach (Control child in control.Controls)
            {
                ApplyThemeToControl(child, backColor, foreColor, buttonBack, buttonFore);
            }
        }

    }
    public static class UIManager
    {
        public static void ApplySettingsToForm(Form form)
        {
            if (LanguageManager.CurrentLanguage == "ar")
            {
                AppFonts.ApplyFontWithStyleCheck(form, AppFonts.DefaultFont);
                MirrorControls(form);
            }
            else
            {
                form.RightToLeft = RightToLeft.No;
                form.RightToLeftLayout = false;
            }

            ThemeManager.ApplyTheme(form);
        }

        public static void MirrorControls(Control parent)
        {
            int parentWidth = parent.ClientSize.Width;

            foreach (Control ctrl in parent.Controls)
            {
                int newX = parentWidth - ctrl.Width - ctrl.Left;
                ctrl.Left = newX;
                if (ctrl.RightToLeft == RightToLeft.No)
                {
                    ctrl.RightToLeft = RightToLeft.Yes;
                }

                if (ctrl.HasChildren)
                {
                    MirrorControls(ctrl);
                }
            }

        }


        private static void ApplyRightToLeftToControls(Control.ControlCollection controls, RightToLeft direction)
        {
            foreach (Control control in controls)
            {
                control.RightToLeft = direction;
                if (control.HasChildren)
                    ApplyRightToLeftToControls(control.Controls, direction);
            }
        }
        public static void ApplySettingsToControl(Control control)
        {
            if (LanguageManager.CurrentLanguage == "ar")
            {
                AppFonts.ApplyFontWithStyleCheck(control, AppFonts.DefaultFont);
                control.RightToLeft = RightToLeft.Yes;
            }
            else
            {
                control.RightToLeft = RightToLeft.No;
            }

            ThemeManager.ApplyThemeToControl(control);
        }

    }
}
