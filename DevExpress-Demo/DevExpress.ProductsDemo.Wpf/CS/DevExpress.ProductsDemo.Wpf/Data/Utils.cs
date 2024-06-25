using System;
using System.IO;
using System.Reflection;
using System.Windows;
using DevExpress.DemoData.Helpers;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit;
using System.Windows.Markup;

namespace ProductsDemo {
    public class Utils {
        public static string GetRelativePath(string name) {
            name = "Data\\" + name;
            string dataPath = DataFilesHelper.DataDirectory;
            string s = "\\";
            for(int i = 0; i <= 10; i++) {
                if(File.Exists(dataPath + s + name))
                    return Path.GetFullPath(dataPath + s + name);
                else
                    s += "..\\";
            }
            return "";
        }
        public static Stream GetDataStream(string fileName) {
            string path = GetRelativePath(fileName);
            if(!String.IsNullOrEmpty(path)) {
                FileAccess fileAccess = (File.GetAttributes(path) & FileAttributes.ReadOnly) != 0 ? FileAccess.Read : FileAccess.ReadWrite;
                return new FileStream(path, FileMode.Open, fileAccess);
            }
            return null;
        }
    }
    public class OpenXmlLoadHelper {
        public static void Load(String fileName, RichEditControl richEditControl) {
            string path = Utils.GetRelativePath(fileName);
            if(!String.IsNullOrEmpty(path))
                richEditControl.LoadDocument(path, DocumentFormat.OpenXml);
        }
    }
    public class PathProvider : MarkupExtension {
        readonly string relativePath;
        public PathProvider(string fileName) {
            this.relativePath = Utils.GetRelativePath(fileName);
        }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this.relativePath;
        }
    }
}
