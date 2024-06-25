using System;

namespace DevExpress.VideoRent.Helpers {
    public class EnumHelper {
        public static T[] GetValues<T>() {
            return (T[])Enum.GetValues(typeof(T));
        }
    }
}
