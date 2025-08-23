using FormLayer.LoginScreen;
using FromSide;
using FromSide.General;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace FormLayer
{
    internal static class Program
    {
        public static IConfiguration Configuration;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LanguageManager.ApplyLanguage();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.EnableVisualStyles();
            ApplicationConfiguration.Initialize();
            Application.Run(new frmLogin());
        }
    }
}