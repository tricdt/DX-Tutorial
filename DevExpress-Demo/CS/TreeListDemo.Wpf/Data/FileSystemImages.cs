using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace TreeListDemo.Data {
    public class FileSystemImages {
        private static readonly Dictionary<string, ImageSource> images = new Dictionary<string, ImageSource>();

        private static ImageSource ImageGet(string name) {
            ImageSource image = null;
            if(images.TryGetValue(name, out image))
                return image;
            image = ImageHelper.GetSvgImage(name);
            images.Add(name, image);
            return image;
        }

        public static ImageSource FileImage {
            get {
                return ImageGet("File");
            }
        }
        public static ImageSource DiskImage {
            get {
                return ImageGet("Local_Disk");
            }
        }

        public static ImageSource ClosedFolderImage {
            get {
                return ImageGet("Folder_Closed");
            }
        }
        public static ImageSource OpenedFolderImage {
            get {
                return ImageGet("Folder_Opened");
            }
        }
    }
}
