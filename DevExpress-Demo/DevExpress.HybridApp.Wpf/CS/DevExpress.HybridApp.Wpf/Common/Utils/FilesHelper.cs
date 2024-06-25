using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DevExpress.DevAV.Common.Utils {
    public static class FilesHelper {
        public static string GetDatabaseFilePath() {
            var filePath = Internal.DataDirectoryHelper.GetFile("devav.sqlite3", Internal.DataDirectoryHelper.DataFolderName);
            try {
                var attributes = File.GetAttributes(filePath);
                if (attributes.HasFlag(FileAttributes.ReadOnly)) {
                    File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                }
            }
            catch { }
            return filePath;
        }
    }
}
