using System;
using System.Collections;

namespace DevExpress.VideoRent.Helpers {
    public class CollectionHelper {
        public static IList CreateList(ICollection collection) {
            return new ArrayList(collection);
        }
    }
}
