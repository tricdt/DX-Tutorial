using System;
using System.Drawing;
using DevExpress.Office.Utils;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Export;
using DevExpress.XtraRichEdit.Import;
using DevExpress.XtraRichEdit.Internal;
using DevExpress.XtraRichEdit.Services;

namespace RichEditDemo {
    public partial class SyntaxHighlighting : RichEditDemoModule {
        public SyntaxHighlighting() {
            InitializeComponent();
        }

        void richEdit_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            richEdit.AddService(typeof(ISyntaxHighlightService), new SyntaxHighlightService(richEdit));
            IRichEditCommandFactoryService commandFactory = richEdit.GetService<IRichEditCommandFactoryService>();
            CustomRichEditCommandFactoryService newCommandFactory = new CustomRichEditCommandFactoryService(commandFactory);
            richEdit.RemoveService(typeof(IRichEditCommandFactoryService));
            richEdit.AddService(typeof(IRichEditCommandFactoryService), newCommandFactory);

            IDocumentImportManagerService importManager = richEdit.GetService<IDocumentImportManagerService>();
            importManager.UnregisterAllImporters();
            importManager.RegisterImporter(new PlainTextDocumentImporter());
            importManager.RegisterImporter(new SourcesCodeDocumentImporter());

            IDocumentExportManagerService exportManager = richEdit.GetService<IDocumentExportManagerService>();
            exportManager.UnregisterAllExporters();
            exportManager.RegisterExporter(new PlainTextDocumentExporter());
            exportManager.RegisterExporter(new SourcesCodeDocumentExporter());

            richEdit.DocumentSource = DemoUtils.GetRelativePath("SyntaxHighlighting.ts");
        }
        void RichEditControl_InitializeDocument(object sender, EventArgs e) {
            Document document = richEdit.Document;
            document.BeginUpdate();
            try {
                document.DefaultCharacterProperties.FontName = "Courier New";
                document.DefaultCharacterProperties.FontSize = 10;
                document.Sections[0].Page.Width = Units.InchesToDocumentsF(100);
                document.Sections[0].LineNumbering.CountBy = 1;
                document.Sections[0].LineNumbering.RestartType = LineNumberingRestart.Continuous;

                SizeF tabSize = richEdit.MeasureSingleLineString("    ", document.DefaultCharacterProperties);
                TabInfoCollection tabs = document.Paragraphs[0].BeginUpdateTabs(true);
                try {
                    for (int i = 1; i <= 30; i++) {
                        TabInfo tab = new TabInfo();
                        tab.Position = i * tabSize.Width;
                        tabs.Add(tab);
                    }
                }
                finally {
                    document.Paragraphs[0].EndUpdateTabs(tabs);
                }
            }
            finally {
                document.EndUpdate();
            }
        }
    }
}
