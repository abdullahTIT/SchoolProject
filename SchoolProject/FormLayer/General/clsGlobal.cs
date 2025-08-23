using Business.Employees;
using FontAwesome.Sharp;
using GlobalLayer;
using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dar = System.Drawing;

namespace FromSide.General
{
    public static class clsGlobal
    {
        public static clsUser CurrentUser;
        public static dar.Icon ToIcon(this IconChar iconChar, int size = 32, Color? color = null)
        {
            using (var iconPictureBox = new IconPictureBox())
            {
                iconPictureBox.IconChar = iconChar;
                iconPictureBox.IconColor = color ?? Color.Black;
                iconPictureBox.IconSize = size;
                iconPictureBox.BackColor = Color.Transparent;

                // نرسم الأيقونة في Bitmap
                Bitmap bmp = new Bitmap(size, size);
                iconPictureBox.DrawToBitmap(bmp, new Rectangle(0, 0, size, size));

                // نحول الـ Bitmap إلى Icon
                nint hIcon = bmp.GetHicon();
                dar.Icon createdIcon = dar.Icon.FromHandle(hIcon);

                // ننسخ الأيقونة النهائية لمنع مشاكل في الذاكرة
                dar.Icon finalIcon = (dar.Icon)createdIcon.Clone();

                // نحذف المؤشر اليدوي بعد التحويل
                DestroyIcon(hIcon);

                return finalIcon;
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DestroyIcon(nint hIcon);
        public static Bitmap ToBitmap(this IconChar iconChar, int size, Color color)
        {
            // إنشاء عنصر IconPictureBox مؤقت (لا يظهر)
            using IconPictureBox icon = new IconPictureBox
            {
                IconChar = iconChar,
                IconColor = color,
                IconFont = IconFont.Solid, // أو IconFont.Regular أو IconFont.Brands حسب نوع الأيقونة
                Size = new Size(size, size)
            };

            // إنشاء Bitmap بالحجم المطلوب
            Bitmap bitmap = new Bitmap(icon.Width, icon.Height);

            // رسم الأيقونة على الـ Bitmap
            icon.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));

            return bitmap;
        }

        public static void SetControlRegionRounded(Control control, int radius)
        {
            if (control == null)
                return;

            Rectangle bounds = new Rectangle(0, 0, control.Width, control.Height);
            GraphicsPath path = new GraphicsPath();

            int diameter = radius * 2;

            // Top-left arc
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            // Top-right arc
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            // Bottom-right arc
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            // Bottom-left arc
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseAllFigures();

            control.Region = new Region(path);
        }
        public static void SetRoundedRegionAuto(Control control, float percentage = 0.1f)
        {
            int minDimension = Math.Min(control.Width, control.Height);
            int radius = (int)(minDimension * percentage);
            SetControlRegionRounded(control, radius);
        }

        public static string ComputeHash(string data)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hasbyte = sha.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hasbyte).Replace("-", "").ToLower();
            }
        }


        public static class CredentialManager
        {
            private static string keyPath = @"Software\SchoolPro\User";
            private static string userValueName = "UserName";
            private static string passValueName = "Password";

            // تشفير كلمة المرور
            private static string EncryptString(string input)
            {
                byte[] data = Encoding.Unicode.GetBytes(input);
                byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
                return Convert.ToBase64String(encrypted);
            }

            // فك تشفير كلمة المرور
            private static string DecryptString(string encrypted)
            {
                byte[] data = Convert.FromBase64String(encrypted);
                byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
                return Encoding.Unicode.GetString(decrypted);
            }

            public static bool RememberUsernameAndPassword(string username, string password)
            {
                try
                {
                    using (RegistryKey key = Registry.CurrentUser.CreateSubKey(keyPath))
                    {
                        if (key == null)
                            return false;

                        if (string.IsNullOrEmpty(username))
                        {
                            key.SetValue(userValueName, "");
                            key.SetValue(passValueName, "");
                        }
                        else
                        {
                            key.SetValue(userValueName, EncryptString(username));
                            key.SetValue(passValueName, EncryptString(password));
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    clsErrorLogger.LogError(ex);
                    MessageBox.Show($"An error occurred: {ex.Message}");
                    return false;
                }
            }

            public static bool GetStoredCredential(ref string username, ref string password)
            {
                username = "";
                password = "";

                try
                {
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath))
                    {
                        if (key == null)
                            return false;

                        object usernameObj = key.GetValue(userValueName);
                        object passwordObj = key.GetValue(passValueName);

                        // إذا أي قيمة فارغة أو غير موجودة، تجاوز فك التشفير
                        if (usernameObj == null || passwordObj == null ||
                            string.IsNullOrWhiteSpace(usernameObj.ToString()) ||
                            string.IsNullOrWhiteSpace(passwordObj.ToString()))
                        {
                            return false;
                        }

                        username = DecryptString(usernameObj.ToString());
                        password = DecryptString(passwordObj.ToString());
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    clsErrorLogger.LogError(ex);
                    MessageBox.Show($"An error occurred: {ex.Message}");
                    return false;
                }
            }

            //public static bool GetStoredCredential(ref string username, ref string password)
            //{
            //    try
            //    {
            //        using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath))
            //        {
            //            if (key == null)
            //                return false;

            //            object usernameObj = key.GetValue(userValueName);
            //            object passwordObj = key.GetValue(passValueName);

            //            if (usernameObj == null || passwordObj == null)
            //                return false;

            //            username = DecryptString(usernameObj.ToString());
            //            password = DecryptString(passwordObj.ToString());
            //        }
            //        return true;
            //    }
            //    catch (Exception ex)
            //    {
            //        clsErrorLogger.LogError(ex);
            //        MessageBox.Show($"An error occurred: {ex.Message}");
            //        return false;
            //    }
            //}
        }

    }


}
