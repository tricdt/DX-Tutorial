using System;

namespace DevExpress.WindowsMailClient.Wpf.Internal {
    public class FilePathHelper {
        public static string GetFullPath(string name) {
            var type = Type.GetType("DevExpress.DemoData.Helpers.DataFilesHelper, " + AssemblyInfo.SRAssemblyDemoData + ", Version=" + AssemblyInfo.Version + ", Culture=neutral, PublicKeyToken=" + AssemblyInfo.PublicKeyToken);
            var method = type.GetMethod("FindFile", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            var propValue = "Data";
            return (string)method.Invoke(null, new object[] { name, propValue });
        }
    }
}
