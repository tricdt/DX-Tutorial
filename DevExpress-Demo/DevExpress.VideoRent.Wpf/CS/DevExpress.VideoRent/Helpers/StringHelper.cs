using System;
using System.Collections.Generic;

namespace DevExpress.VideoRent.Helpers {
    public class StringHelper {
        public static string GetStringByArrayList(List<string> array) {
            string ret = string.Empty;
            if(array == null) return ret;
            array.Sort();
            for(int i = 0; i < array.Count; i++)
                ret += string.Format("{0}{1}", array[i], i == array.Count - 1 ? string.Empty : ", ");
            return ret;
        }
    }
}
