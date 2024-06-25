using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttDemo {
    public static class CSVLoader {
        static void SkipHeader(StreamReader reader) {
            reader.ReadLine();
        }
        public static List<DataLoadingItem> LoadItems(Stream stream) {
            using(var reader = new StreamReader(stream)) {
                SkipHeader(reader);
                var items = new List<DataLoadingItem>();
                while(!reader.EndOfStream) {
                    var item = reader.ReadLine();
                    var elements = item.Split(',');
                    items.Add(new DataLoadingItem(
                                        DateTime.Parse(elements[0], CultureInfo.InvariantCulture),
                                        TimeSpan.FromMilliseconds(double.Parse(elements[5], CultureInfo.InvariantCulture)),
                                        elements[1],
                                        elements[2],
                                        elements[3],
                                        Int32.Parse(elements[4], CultureInfo.InvariantCulture)
                             )
                    );
                }
                return items;
            }
        }
    }
}
