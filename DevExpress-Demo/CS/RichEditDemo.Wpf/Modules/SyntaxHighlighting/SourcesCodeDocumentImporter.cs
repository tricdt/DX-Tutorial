using DevExpress.Office.Internal;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Import;

namespace RichEditDemo {
    public class SourcesCodeDocumentImporter : PlainTextDocumentImporter {
        internal static readonly FileDialogFilter _filter = new FileDialogFilter("Source Files", new string[] { "ts" });
        public override FileDialogFilter Filter { get { return _filter; } }
        public override DocumentFormat Format { get { return SourceCodeDocumentFormat.Id; } }
    }
}
