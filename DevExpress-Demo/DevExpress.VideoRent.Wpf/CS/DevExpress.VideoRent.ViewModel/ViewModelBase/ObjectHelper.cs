using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.VideoRent.Helpers;
using DevExpress.Xpo;
using System.Diagnostics;
using DevExpress.VideoRent.Resources;

namespace DevExpress.VideoRent.ViewModel.ViewModelBase {
    public class ObjectHelper {
        public static bool SafeDelete(VideoRentBaseObject obj, string shureToDeleteMessage, IExceptionProcesser exceptionProcesser) {
            return SafeDelete(obj, shureToDeleteMessage, exceptionProcesser, null);
        }
        public static bool SafeDelete(VideoRentBaseObject obj, string shureToDeleteMessage, IExceptionProcesser exceptionProcesser, Action<object> action) {
            UnitOfWork uow = SafeDeleteNoCommit(obj, shureToDeleteMessage, action);
            if(uow == null) return false;
            SessionHelper.CommitSession(uow, exceptionProcesser);
            return true;
        }
        public static UnitOfWork SafeDeleteNoCommit(VideoRentBaseObject obj, string shureToDeleteMessage) {
            return SafeDeleteNoCommit(obj, shureToDeleteMessage, null);
        }
        public static UnitOfWork SafeDeleteNoCommit(VideoRentBaseObject obj, string shureToDeleteMessage, Action<object> action) {
            if(!obj.AllowDelete) {
                MessageBox.Show(ConstStrings.Get("ObjectCanNotBeDeleted"), ConstStrings.Get("Warning"), MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
            if(!IsSureToDelete(shureToDeleteMessage))
                return null;
            if(action != null)
                action(null);
            UnitOfWork uow = (UnitOfWork)obj.Session;
            obj.Delete();
            return uow;
        }
        static bool IsSureToDelete(string message) {
            if(string.IsNullOrEmpty(message)) return true;
            MessageBoxResult result = MessageBox.Show(message, null, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            return result == MessageBoxResult.Yes;
        }
        public static void ShowWebSite(string url) {
            if(url == null) return;
            string processName = url.Replace(" ", string.Empty);
            if(processName.Length == 0) return;
            string[] protocols = new string[] { "http://", "https://" };
            foreach(string protocol in protocols) {
                if(processName.IndexOf(protocol, StringComparison.InvariantCultureIgnoreCase) == 0) {
                    StartProcess(processName);
                    return;
                }
            }
            StartProcess(string.Concat(protocols[0], processName));
        }
        public static void SendMessageTo(string email) {
            StartProcess(string.Format("MailTo:{0}", email));
        }
        static void StartProcess(string processName) {
#if !SL //TODO
            try {
                Process.Start(processName);
            } catch(Exception ex) {
                DevExpress.VideoRent.ViewModel.ViewModelBase.MessageBox.Show(ex.Message, ConstStrings.Get("Error"), DevExpress.VideoRent.ViewModel.ViewModelBase.MessageBoxButton.OK, DevExpress.VideoRent.ViewModel.ViewModelBase.MessageBoxImage.Error);
            }
#endif
        }
    }
}
