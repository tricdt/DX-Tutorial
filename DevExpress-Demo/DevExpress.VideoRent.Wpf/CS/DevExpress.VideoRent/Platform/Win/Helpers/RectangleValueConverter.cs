using System;
using System.Drawing;
using DevExpress.Xpo.Metadata;

namespace DevExpress.VideoRent.Helpers {
    public class RectangleValueConverter : ValueConverter {
        public override Type StorageType { get { return typeof(string); } }
        public override object ConvertToStorageType(object val) {
            if(!(val is Rectangle)) return null;
            Rectangle rect = (Rectangle)val;
            return string.Format("{0}@{1}@{2}@{3}", rect.X, rect.Y, rect.Width, rect.Height);
        }
        public override object ConvertFromStorageType(object val) {
            string[] data = string.Format("{0}", val).Split('@');
            if(data.Length < 2) return null;
            return new Rectangle(Convert.ToInt32(data[0]), Convert.ToInt32(data[1]), Convert.ToInt32(data[2]), Convert.ToInt32(data[3]));
        }
    }
}
