using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Text;

namespace DevExpress.WindowsMailClient.Wpf.Internal {
    static class MailMessageHelper {
        public static string CreateReplySubject(string subject) {
            return string.Format("Re: {0}", subject);
        }
        public static string CreateReplyAddress(string value) {
            var builder = new StringBuilder(value);
            builder.Replace(" (", "_");
            builder.Replace(')', '_');
            builder.Replace(' ', '_');
            builder.Replace('-', '_');
            return builder.ToString();
        }
        public static string CreateReplyText(string from, string to, string subject, string text, DateTime date) {
            using (RichEditDocumentServer server = new RichEditDocumentServer()) {
                server.MhtText = text;
                Document document = server.Document;
                InsertInfo(document, from, to, subject);
                foreach (Paragraph paragraph in document.Paragraphs) {
                    DocumentPosition start = paragraph.Range.Start;
                    if (document.Tables.GetTableCell(start) == null && !paragraph.IsInList)
                        document.InsertHtmlText(start, ">> ");
                }
                var name = DataHelper.GetNameByEmail(from);
                string replyHeader = String.Format("Dear, {0}\n\n{1}, you wrote:\n", string.IsNullOrEmpty(name) ? from : name, date);
                document.InsertText(document.Range.Start, replyHeader);
                return server.MhtText;
            }
        }

        public static string CreateForwardSubject(string subject) {
            return string.Format("Fwd: {0}", subject);
        }
        public static string CreateForwardText(string from, string to, string subject, string text) {
            using (RichEditDocumentServer server = new RichEditDocumentServer()) {
                server.MhtText = text;
                Document document = server.Document;
                InsertInfo(document, from, to, subject);
                string replyHeader = "This is a forwarded message:\n\n=================Original message text===============\n";
                document.InsertText(document.Range.Start, replyHeader);
                document.AppendText("\n\n=================End of original message text===============");
                return server.MhtText;
            }
        }

        static void InsertInfo(Document document, string from, string to, string subject) {
            var range = document.InsertHtmlText(
                document.Range.Start,
                string.Format("<br/><p/><b>From</b>: {0}<p/><b>To</b>: {1}<p/><b>Subject</b>: {2}",
                from, to, subject));
            document.InsertText(range.End, "\n");
        }
    }
}
