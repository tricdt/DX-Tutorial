using DevExpress.DevAV;
using DevExpress.Xpf.Core.Native;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
#if DXCORE3
using Microsoft.EntityFrameworkCore;
using DevExpress.Internal;
#else
using System.Data.Entity;
#endif

namespace DevExpress.WindowsMailClient.Wpf.Internal {
    public static class DataHelper {
        static List<Employee> employees = null;
        static RichEditDocumentServer server = new RichEditDocumentServer();
        static ImageSource unknownUserPicture;

        internal static List<Employee> Employees {
            get {
                if (employees == null) {
#if DXCORE3
                    SetFilePath();
                    var devAvDb = new DevAVDb(string.Format("Data Source={0}", filePath));
                    devAvDb.Pictures.Load();
#else
                    var devAvDb = new DevAVDb();
#endif
                    devAvDb.Employees.Load();
                    employees = devAvDb.Employees.Local.ToList();
                }
                return employees;
            }
        }
#if DXCORE3
        static string filePath;
        static void SetFilePath()
        {
            if (filePath == null)
                filePath = DataDirectoryHelper.GetFile("devav.sqlite3", DataDirectoryHelper.DataFolderName);
            try
            {
                var attributes = File.GetAttributes(filePath);
                if (attributes.HasFlag(FileAttributes.ReadOnly))
                {
                    File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                }
            }
            catch { }
        }
#endif
        internal static ImageSource UnknownUserPicture {
            get {
                if (unknownUserPicture == null) {
                    unknownUserPicture = new BitmapImage(ImageSourceHelper.CreateUri("Unknown-user.png", UriKind.RelativeOrAbsolute));
                    return unknownUserPicture;
                }
                return unknownUserPicture;
            }
        }
        public static string GetNameByEmail(string mail, bool isDesignMode = false) {
            if (string.IsNullOrEmpty(mail) || isDesignMode)
                return string.Empty;
            var employee = Employees.FirstOrDefault(p => p.Email == mail);
            return employee == null ? string.Empty : employee.FullName;
        }
        public static ImageSource GetPictureByEmail(string mail, bool isDesignMode = false) {
            if (string.IsNullOrEmpty(mail) || isDesignMode)
                return UnknownUserPicture;
            var employee = Employees.FirstOrDefault(p => p.Email == mail);
            if (employee != null && employee.Picture != null && employee.Picture.Data != null)
                return ImageHelper.CreateImageFromStream(new MemoryStream(employee.Picture.Data as byte[]));
            return UnknownUserPicture;
        }
        public static string GetPlainTextFromMHT(string mhtText) {
            server.MhtText = mhtText;
            return server.Text.TrimStart();
        }
        public static string GetMHTTextFromHTML(string htmlText) {
            server.HtmlText = htmlText;
            return server.MhtText;
        }
        public static bool HasImages(string mhtText) {
            server.MhtText = mhtText;
            return server.Document.Images.Any();
        }
    }
}
