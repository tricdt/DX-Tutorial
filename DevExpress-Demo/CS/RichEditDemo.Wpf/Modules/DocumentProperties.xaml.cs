using System;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit;
using DevExpress.Xpf.Bars;

namespace RichEditDemo {
    public partial class DocumentProperties : RichEditDemoModule {
        public static readonly DocumentPropertyInfo[] Properties = new DocumentPropertyInfo[] {
            new DocumentPropertyInfo("Category", "DOCPROPERTY Category"),
            new DocumentPropertyInfo("Created", "CREATEDATE"),
            new DocumentPropertyInfo("Creator", "AUTHOR"),
            new DocumentPropertyInfo("Description", "COMMENTS"),
            new DocumentPropertyInfo("Keywords"),
            new DocumentPropertyInfo("LastModifiedBy", "LASTSAVEDBY"),
            new DocumentPropertyInfo("LastPrinted", "PRINTDATE"),
            new DocumentPropertyInfo("Modified", "SAVEDATE"),
            new DocumentPropertyInfo("Revision", "REVNUM"),
            new DocumentPropertyInfo("Subject"),
            new DocumentPropertyInfo("Title"),
        };

        public DocumentProperties() {
            InitializeComponent();
            Document.Fields.Update();
        }

        Document Document { get { return richEdit.Document; } }

        void OnDocumentPropertiesChanged(object sender, EventArgs e) {
            Document.Fields.Update();
        }
        void OnCalculateDocumentVariable(object sender, CalculateDocumentVariableEventArgs e) {
            if (e.Arguments.Count == 0 || e.VariableName != "CustomProperty")
                return;

            string name = e.Arguments[0].Value;
            object customProperty = Document.CustomProperties[name];
            if (customProperty != null)
                e.Value = customProperty.ToString();
            e.Handled = true;
        }

        void OnPropertyItemClick(object sender, ItemClickEventArgs e) {
            var propertyInfo = (DocumentPropertyInfo)e.Item.DataContext;
            Document.BeginUpdate();
            Field field = Document.Fields.Create(richEdit.Document.CaretPosition, propertyInfo.Name);
            field.Update();
            Document.EndUpdate();
        }
    }
}
