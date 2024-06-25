using System;
using System.IO;
using System.DirectoryServices;
using System.Net.NetworkInformation;
using DevExpress.DemoData.Helpers;
using DevExpress.XtraRichEdit.Services;

namespace RichEditDemo {
    public class DemoUtils {
        public static string GetRelativePath(string name) {
            name = "Data\\" + name;
            string path = DataFilesHelper.DataDirectory;
            string s = "\\";
            for (int i = 0; i <= 10; i++) {
                if (File.Exists(path + s + name))
                    return Path.GetFullPath(path + s + name);
                else
                    s += "..\\";
            }
            return "";
        }
    }
    public class UserAccountService : IUserAccountService {
        string userName;
        string IUserAccountService.GetUserName() {
            if (userName == null)
                userName = GetCurrentUserDisplayName();
            return userName;
        }
        string GetCurrentUserDisplayName() {
            try {
                return GetCurrentUserDisplayNameCore();
            }
            catch {
                return null;
            }
        }
        string GetCurrentDomainPath() {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            return "LDAP://" + properties.DomainName;
        }
        string GetCurrentUserDisplayNameCore() {
            using (DirectoryEntry de = new DirectoryEntry(GetCurrentDomainPath())) {
                using (DirectorySearcher ds = new DirectorySearcher(de)) {
                    ds.ClientTimeout = TimeSpan.FromSeconds(3);
                    ds.PropertiesToLoad.Add("displayname");
                    ds.Filter = string.Format("(&(objectClass=person)(objectCategory=user)(samaccountname={0}))", Environment.UserName);
                    var result = ds.FindOne();
                    if (result != null)
                        return result.Properties["displayname"][0].ToString();
                    return null;
                }
            }
        }
    }
}
