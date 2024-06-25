using System;
using System.IO;
using System.Windows;

namespace GanttDemo {
    public static class ResourceUtils {
        public static Stream GetResourceStream(string resourceName) {
            return Application.GetResourceStream(new Uri("/GanttDemo;component\\" + resourceName, UriKind.RelativeOrAbsolute)).Stream;
        }
    }
}
