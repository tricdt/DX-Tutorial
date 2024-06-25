using System.IO;
using System.Reflection;

namespace ProductsDemo {
    public static class ModelAssemblyHelper {
        static Assembly assembly = null;
        public static Assembly CurrentAssembly {
            get {
                if(assembly == null)
                    assembly = typeof(ModelAssemblyHelper).Assembly;
                return assembly;
            }
        }
        public static string GetResourcePath(string resourceName) {
            return @"ProductsDemo.SalesDemoResources." + resourceName;
        }
        public static Stream GetResourceStream(string resourceName) {
            return CurrentAssembly.GetManifestResourceStream(GetResourcePath(resourceName));
        }
    }
}
