using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using DevExpress.Utils;
using DevExpress.Utils.Zip;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.DemoBase.Helpers.TextColorizer;
using DevExpress.Xpf.RichEdit;
using DevExpress.Xpf.SpellChecker;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSpellChecker;

namespace SpellCheckerDemo {
    public class DemoUtils {
        public static readonly string PathToDemoData = "SpellCheckerDemo.Data";
        public static readonly string PathToDictionaries = PathToDemoData + ".Dictionaries";

        public static string GetPathToResource(string path, string name) {
            if (DemoHelper.GetDemoLanguage(typeof(DemoUtils).Assembly) == CodeLanguage.CS)
                return String.Format("{0}.{1}", path, name);
            else
                return name;
        }
        public static Stream GetDataStream(string path, string name) {
            string fullPath = DemoUtils.GetPathToResource(path, name);
            if (!String.IsNullOrEmpty(fullPath))
                return Assembly.GetExecutingAssembly().GetManifestResourceStream(fullPath);
            return null;
        }
    }

    public class DocumentLoadHelper {
        public static Stream Load(string fileName) {
            string path = DemoUtils.GetPathToResource(DemoUtils.PathToDemoData, fileName);
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
        }
    }

    public class Employees : System.ComponentModel.INotifyPropertyChanged {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public double Salary { get; set; }
        public bool OnVacation { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public int ReportsTo { get; set; }

        #region INotifyPropertyChanged Members
        System.ComponentModel.PropertyChangedEventHandler onPropertyChanged;
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged { add { onPropertyChanged += value; } remove { onPropertyChanged -= value; } }
        #endregion
    }

    public static class EmployeesData {
        static IList dataSource;

        public static IList DataSource {
            get {
                if (dataSource == null) {
                    dataSource = GetDataSource();
                    MakeMistakes(dataSource);
                }
                return dataSource;
            }
        }
        static IList GetDataSource() {
            XmlSerializer s = new XmlSerializer(typeof(List<Employees>), new XmlRootAttribute("nwind"));
            Stream stream = typeof(EmployeesData).Assembly.GetManifestResourceStream(DemoUtils.GetPathToResource(DemoUtils.PathToDemoData, "nwind.xml"));
            return (IList)s.Deserialize(stream);
        }
        static void MakeMistakes(IList dataSet) {
            foreach (Employees employee in dataSet) {
                StringBuilder text = new StringBuilder(employee.Notes);
                List<char> charSet = CreateCharSet(text);
                Random random = new Random(Environment.TickCount);
                for (int i = text.Length - 1; i >= 0; i -= 30) {
                    if (!Char.IsLetter(text[i]))
                        continue;
                    char ch = GetRandomChar(charSet);
                    if (Char.IsUpper(text[i]))
                        ch = Char.ToUpper(ch);
                    if (text[i] == ch)
                        text.Remove(i, 1);
                    else
                        text[i] = ch;
                }
                employee.Notes = text.ToString();
            }
        }
        static List<char> CreateCharSet(StringBuilder text) {
            List<char> result = new List<char>();
            int length = text.Length;
            for (int i = 0; i < length; i++) {
                char ch = text[i];
                if (!Char.IsLetter(ch))
                    continue;
                ch = Char.ToLower(ch);
                int index = result.BinarySearch(ch);
                if (index < 0)
                    result.Insert(~index, ch);
            }
            return result;
        }
        static char GetRandomChar(List<char> charSet) {
            Random random = new Random(Environment.TickCount);
            int index = random.Next(0, charSet.Count - 1);
            return charSet[index];
        }
    }
}
