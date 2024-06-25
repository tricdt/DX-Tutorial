using System;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit;
using FormsApplication = System.Windows.Forms.Application;

namespace RichEditDemo {
    public class RichEditDemoExceptionsHandler {
        readonly RichEditControl control;
        public RichEditDemoExceptionsHandler(RichEditControl control) {
            this.control = control;
        }
        public void Install() {
            if (control != null)
                control.UnhandledException += OnRichEditControlUnhandledException;
        }

        void OnRichEditControlUnhandledException(object sender, RichEditUnhandledExceptionEventArgs e) {
            try {
                if (e.Exception != null)
                    throw e.Exception;
            }
            catch (RichEditUnsupportedFormatException ex) {
                ShowMessage(ex.Message);
                e.Handled = true;
            }
            catch (System.Runtime.InteropServices.ExternalException ex) {
                ShowMessage(ex.Message);
                e.Handled = true;
            }
            catch (System.IO.IOException ex) {
                ShowMessage(ex.Message);
                e.Handled = true;
            }
            catch (System.InvalidOperationException ex) {
                if (ex.Message == DevExpress.XtraRichEdit.Localization.XtraRichEditLocalizer.GetString(DevExpress.XtraRichEdit.Localization.XtraRichEditStringId.Msg_MagicNumberNotFound) ||
                    ex.Message == DevExpress.XtraRichEdit.Localization.XtraRichEditLocalizer.GetString(DevExpress.XtraRichEdit.Localization.XtraRichEditStringId.Msg_UnsupportedDocVersion)) {
                    ShowMessage(ex.Message);
                    e.Handled = true;
                }
                else
                    throw ex;
            }
            catch (SystemException ex) {
                ShowMessage(ex.Message);
                e.Handled = true;
            }
        }
        void ShowMessage(string message) {
            ThemedMessageBox.Show(FormsApplication.ProductName, message, MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
