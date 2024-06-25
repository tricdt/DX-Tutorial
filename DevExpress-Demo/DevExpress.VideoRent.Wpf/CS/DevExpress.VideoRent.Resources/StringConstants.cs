using System;
using System.Text;
using DevExpress.Utils;
using System.Reflection;

namespace DevExpress.VideoRent.Resources {
    public class ConstStrings {
        public static string Get(string name) {
            return GetDefault(name);
        }
        static string GetDefault(string name) {
            PropertyInfo pi = typeof(DevExpress.VideoRent.Resources.Properties.Resources).GetProperty(name);
            return string.Format("{0}", pi.GetValue(null, new object[] {}));
        }
    }
}
