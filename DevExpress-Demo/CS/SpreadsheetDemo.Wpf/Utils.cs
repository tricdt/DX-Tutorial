using System;
using System.IO;
using System.Windows;
using System.Diagnostics;
using DevExpress.Xpf.Core;

using System.Windows.Forms;
using DevExpress.DemoData.Helpers;
using System.Reflection;


namespace SpreadsheetDemo {
    public static class DocumentLoadHelper {
        public static Stream GetDocument(string path) {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream(path);
        }
    }
    #region DemoUtils
    public class DemoUtils {
        public static string GetRelativePath(string name) {
            name = "Data\\" + name;
            string path = DataFilesHelper.DataDirectory;
            string s = "\\";
            for (int i = 0; i <= 10; i++) {
                if (System.IO.File.Exists(path + s + name))
                    return System.IO.Path.GetFullPath(path + s + name);
                else
                    s += "..\\";
            }
            return "";
        }

        public static string GetRelativeDirectoryPath(string name) {
            name = "Data\\" + name;
            string path = DataFilesHelper.DataDirectory;
            string s = "\\";
            for (int i = 0; i <= 10; i++) {
                if (System.IO.Directory.Exists(path + s + name))
                    return (path + s + name);
                else
                    s += "..\\";
            }
            return "";
        }

        public static Stream GetDataStream(string fileName) {
            string path = DemoUtils.GetRelativePath(fileName);
            if (!String.IsNullOrEmpty(path)) {
                FileAccess fileAccess = (File.GetAttributes(path) & FileAttributes.ReadOnly) != 0 ? FileAccess.Read : FileAccess.ReadWrite;
                return new FileStream(path, FileMode.Open, fileAccess);
            }
            return null;
        }

        public static void SetDatabasePath() {
            const string dbName = "nwind.mdb";
            const string pathToDbTag = "|pathToDb|";
            string path = GetRelativePath(dbName);
            if (String.IsNullOrEmpty(path))
                return;
            string connectionString = global::SpreadsheetDemo.Properties.Settings.Default["nwindConnectionString"] as string;
            if (String.IsNullOrEmpty(connectionString))
                return;
            connectionString = connectionString.Replace(pathToDbTag, path);
            global::SpreadsheetDemo.Properties.Settings.Default["nwindConnectionString"] = connectionString;
        }
    }
    #endregion
    #region DialogHelper
    public class DialogHelper {
        public static MessageBoxResult ShowQuestionDialog(string message) {
            return ThemedMessageBox.Show(System.Windows.Forms.Application.ProductName, message, MessageBoxButton.OKCancel, MessageBoxImage.Question);
        }
        public static void ShowErrorDialog(string message) {
            ThemedMessageBox.Show(System.Windows.Forms.Application.ProductName, message, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    #endregion
    #region FileOpenHelper
    public class FileOpenHelper {
        public static void ShowFile(string fileName) {
            if (!File.Exists(fileName))
                return;

#if DXCORE3
            ProcessStartInfo startInfo = new ProcessStartInfo(fileName);
            startInfo.UseShellExecute = true;
            Process.Start(startInfo);
#else
            Process.Start(fileName);
#endif
        }
    }
#endregion
#region FileSaveHelper
    public class FileSaveHelper {
        public static string GetSaveFileName(string filter, string defaultName) {
            SaveFileDialog sfDialog = new SaveFileDialog();
            sfDialog.Filter = filter;
            sfDialog.FileName = defaultName;
            if (sfDialog.ShowDialog() != DialogResult.OK)
                return String.Empty;
            return sfDialog.FileName;
        }
    }
#endregion
}
