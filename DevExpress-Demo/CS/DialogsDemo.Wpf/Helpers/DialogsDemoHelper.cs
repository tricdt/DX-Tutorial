using DevExpress.Internal;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace DialogsDemo.Helpers {
    public static class DialogsDemoHelper {
        public static string GetDialogsDataPath(string relativePath) {
            return Path.GetFullPath(DataDirectoryHelper.GetFile("Dialogs\\" + relativePath, DataDirectoryHelper.DataFolderName));
        }
        public static string GetPhotosPath(string relativePath) {
            return Path.GetFullPath(DataDirectoryHelper.GetFile("Photos\\" + relativePath, DataDirectoryHelper.DataFolderName));
        }
        public static Stream GetDataStream(string dataFileName) {
            string path = GetDialogsDataPath(dataFileName);
            return File.OpenRead(path);
        }
        public static IList<MessageBoxResult> GetMessageBoxResults(MessageBoxButton buttons) {
            var resultButtons = new List<MessageBoxResult>();
            switch (buttons) {
                case MessageBoxButton.OK:
                    resultButtons.Add(MessageBoxResult.OK);
                    break;
                case MessageBoxButton.OKCancel:
                    resultButtons.Add(MessageBoxResult.OK);
                    resultButtons.Add(MessageBoxResult.Cancel);
                    break;
                case MessageBoxButton.YesNoCancel:
                    resultButtons.Add(MessageBoxResult.Yes);
                    resultButtons.Add(MessageBoxResult.No);
                    resultButtons.Add(MessageBoxResult.Cancel);
                    break;
                case MessageBoxButton.YesNo:
                    resultButtons.Add(MessageBoxResult.Yes);
                    resultButtons.Add(MessageBoxResult.No);
                    break;
            }
            return resultButtons;
        }
    }
}
