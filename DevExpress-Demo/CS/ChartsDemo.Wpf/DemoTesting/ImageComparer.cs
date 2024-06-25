using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace DevExpress.Xpf.Core.Tests
{
    public static class ImageComparer
    {
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int memcmp(IntPtr b1, IntPtr b2, UIntPtr count);

        static bool CompareMemCmp(Bitmap b1, Bitmap b2)
        {
            if ((b1 == null) != (b2 == null))
                return false;
            if (b1.Size != b2.Size)
                return false;

            var bd1 = b1.LockBits(new Rectangle(new System.Drawing.Point(0, 0), b1.Size),
                                  ImageLockMode.ReadOnly,
                                  System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var bd2 = b2.LockBits(new Rectangle(new System.Drawing.Point(0, 0), b2.Size),
                                  ImageLockMode.ReadOnly,
                                  System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                IntPtr bd1scan0 = bd1.Scan0;
                IntPtr bd2scan0 = bd2.Scan0;

                int stride = bd1.Stride;
                int len = stride * b1.Height;

                return memcmp(bd1scan0, bd2scan0, new UIntPtr((uint)len)) == 0;
            }
            finally
            {
                b1.UnlockBits(bd1);
                b2.UnlockBits(bd2);
            }
        }

        public static bool CompareMemCmp(Stream s1,
                                         Stream s2,
                                         out int width1,
                                         out int width2,
                                         out int height1,
                                         out int height2)
        {
            Bitmap b1 = new Bitmap(s1);
            Bitmap b2 = new Bitmap(s2);
            width1 = b1.Size.Width;
            width2 = b2.Size.Width;
            height1 = b1.Size.Height;
            height2 = b2.Size.Height;
            return CompareMemCmp(b1, b2);
        }
    }
}
