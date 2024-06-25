using System;

namespace DevExpress.VideoRent.Helpers {
    public abstract class StringEncoder {
        public static string CalcHash(string s) {
#if SL
            return SimpleStringEncoder.CalcHash(s);
#else
            return MD5StringEncoder.CalcHash(s);
#endif
        }
    }
}
