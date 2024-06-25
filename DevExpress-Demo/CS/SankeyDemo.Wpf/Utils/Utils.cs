using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace SankeyDemo {
    public static class Utils {
        public static string GetRelativePath(string name) {
            name = "Data\\" + name;
            DirectoryInfo dir = new DirectoryInfo(DevExpress.DemoData.Helpers.DataFilesHelper.DataDirectory);
            for(int i = 0; i <= 10; i++) {
                string filePath = Path.Combine(dir.FullName, name);
                if(File.Exists(filePath))
                    return filePath;
                dir = Directory.GetParent(dir.FullName);
            }
            return string.Empty;
        }
        public static DataTable CreateDataSet(string xmlFileName) {
            string filePath = Utils.GetRelativePath(xmlFileName);
            if(!string.IsNullOrWhiteSpace(filePath)) {
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(filePath);
                if(dataSet.Tables.Count > 0)
                    return dataSet.Tables[0];
            }
            return null;
        }
        public static Uri GetFileUri(string fileName) {
            return new Uri("/SankeyDemo;component/Data/" + fileName, UriKind.RelativeOrAbsolute);
        }
        public static void SetDatabasePath() {
            const string dbName = "nwind.mdb";
            const string pathToDbTag = "|pathToDb|";
            string path = GetRelativePath(dbName);
            if(String.IsNullOrEmpty(path))
                return;
            string connectionString = Properties.Settings.Default["nwindConnectionString"] as string;
            if(String.IsNullOrEmpty(connectionString))
                return;
            connectionString = connectionString.Replace(pathToDbTag, path);
            Properties.Settings.Default["nwindConnectionString"] = connectionString;
        }
    }
    public static class DataLoader {
        static Stream GetStream(string fileName) {
            Uri uri = GetResourceUri(fileName);
            return Application.GetResourceStream(uri).Stream;
        }
        public static Uri GetResourceUri(string fileName) {
            fileName = "/SankeyDemo;component" + fileName;
            return new Uri(fileName, UriKind.RelativeOrAbsolute);
        }
        public static XDocument LoadXDocumentFromResources(string fileName) {
            try {
                return XDocument.Load(GetStream(fileName));
            }
            catch {
                return null;
            }
        }
        public static XmlDocument LoadXmlDocumentFromResources(string fileName) {
            XmlDocument document = new XmlDocument();
            try {
                document.Load(GetStream(fileName));
                return document;
            }
            catch {
                return null;
            }
        }
    }
    public class Export {
        public string Exporter { get; set; }
        public string Importer { get; set; }
        public double Sum { get; set; }

        public Export(string from, string to, double weight) {
            this.Exporter = from;
            this.Importer = to;
            this.Sum = weight;
        }
    }
}
