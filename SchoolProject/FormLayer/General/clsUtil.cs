using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace FromSide.General
{

    public class clsUtil
    {
        public static string GenerateGUID()
        {
            return Guid.NewGuid().ToString();
        }

        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {

            // Check if the folder exists
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    // If it doesn't exist, create the folder
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    GlobalLayer.clsErrorLogger.LogError(ex);
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }

            return true;

        }

        public static string ReplaceFileNameWithGUID(string sourceFile)
        {
            string fileName = sourceFile;
            FileInfo fi = new FileInfo(fileName);
            string extn = fi.Extension;
            return GenerateGUID() + extn;

        }

        public static bool CopyImageToProjectImagesFolder(ref string sourceFile)
        {
            if (string.IsNullOrEmpty(sourceFile) || !File.Exists(sourceFile))
            {
                return false;
            }
            //string DestinationFolder = @"C:\SchoolProSourses\Persons\Images\";
            string DestinationFolder = ConfigurationManager.AppSettings["DestinationFolder"];
            DestinationFolder = (DestinationFolder ?? "C:\\SchoolPro") + "Persons\\Images\\";

            // إنشاء المجلد إذا لم يكن موجودًا
            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }
            string destinationFile = Path.Combine(DestinationFolder, ReplaceFileNameWithGUID(sourceFile));
            try
            {
                File.Copy(sourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                GlobalLayer.clsErrorLogger.LogError(iox);
                return false;
            }
            catch (UnauthorizedAccessException uaex)
            {
                GlobalLayer.clsErrorLogger.LogError(uaex);
                return false;
            }
            sourceFile = destinationFile;
            return true;
        }
    }
}
