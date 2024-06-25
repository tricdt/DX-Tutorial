using System;
using System.IO;

namespace DevExpress.VideoRent.Helpers {
    public class MD5StringEncoder {
        public static string CalcHash(string data) {
            string ret = "";
            try {
                using(MemoryStream mem = new MemoryStream()) {
                    BinaryWriter bWriter = new BinaryWriter(mem);
                    bWriter.Write(data);
                    mem.Position = 0;
                    System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                    byte[] res = md5.ComputeHash(mem);
                    bWriter.Close();
                    for(int i = 0; i < res.Length; i++)
                        ret += (char)res[i];
                }
            } catch { ret = "N/A"; }
            return ret;
        }
    }
}
