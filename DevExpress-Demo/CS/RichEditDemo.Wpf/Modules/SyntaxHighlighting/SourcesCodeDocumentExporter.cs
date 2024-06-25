using DevExpress.Office.Internal;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Export;

namespace RichEditDemo {
    public class SourcesCodeDocumentExporter : PlainTextDocumentExporter {
        public override FileDialogFilter Filter { get { return SourcesCodeDocumentImporter._filter; } }
        public override DocumentFormat Format { get { return SourceCodeDocumentFormat.Id; } }
    }
}
