using System;
using DevExpress.Xpo.Metadata;
using System.IO;

namespace DevExpress.VideoRent.Helpers {
    public class MemoryStreamValueConverter : ValueConverter {
        public override object ConvertFromStorageType(object val) {
            try {
                if(val is byte[]) {
                    MemoryStream stream = new System.IO.MemoryStream(val as byte[]);
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return formatter.Deserialize(stream);
                }
            } catch { }
            return null;
        }
        public override object ConvertToStorageType(object val) {
            try {
                if(val == null) return null;
                MemoryStream stream = new MemoryStream();
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(stream, val);
                return stream.GetBuffer();
            } catch { }
            return null;
        }
        public override Type StorageType {
            get { return typeof(byte[]); }
        }
    }
}
