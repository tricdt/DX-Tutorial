using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SankeyDemo{
    public class ProductTransfer{
        public string TypeFrom { get; set; }
        public string TypeTo { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public double TotalPrice { get; set; }
    }

    public class ProductTransfers{

        public static List<ProductTransfer> GetData() {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ProductTransfer>));

            string path = Utils.GetRelativePath("ProductTransfers.xml");
            using (Stream reader = new FileStream(path, FileMode.Open)){
                var data = (List<ProductTransfer>) serializer.Deserialize(reader);
                return data;
            }
        }
    }
}
