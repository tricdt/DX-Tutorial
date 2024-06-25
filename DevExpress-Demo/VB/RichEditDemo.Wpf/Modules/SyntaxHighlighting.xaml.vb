Imports System
Imports System.Drawing
Imports DevExpress.Office.Utils
Imports DevExpress.XtraRichEdit.API.Native
Imports DevExpress.XtraRichEdit.Export
Imports DevExpress.XtraRichEdit.Import
Imports DevExpress.XtraRichEdit.Internal
Imports DevExpress.XtraRichEdit.Services

Namespace RichEditDemo

    Public Partial Class SyntaxHighlighting
        Inherits RichEditDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub richEdit_Loaded(ByVal sender As Object, ByVal e As Windows.RoutedEventArgs)
            richEdit.AddService(GetType(ISyntaxHighlightService), New SyntaxHighlightService(richEdit))
            Dim commandFactory As IRichEditCommandFactoryService = richEdit.GetService(Of IRichEditCommandFactoryService)()
            Dim newCommandFactory As CustomRichEditCommandFactoryService = New CustomRichEditCommandFactoryService(commandFactory)
            richEdit.RemoveService(GetType(IRichEditCommandFactoryService))
            richEdit.AddService(GetType(IRichEditCommandFactoryService), newCommandFactory)
            Dim importManager As IDocumentImportManagerService = richEdit.GetService(Of IDocumentImportManagerService)()
            importManager.UnregisterAllImporters()
            importManager.RegisterImporter(New PlainTextDocumentImporter())
            importManager.RegisterImporter(New SourcesCodeDocumentImporter())
            Dim exportManager As IDocumentExportManagerService = richEdit.GetService(Of IDocumentExportManagerService)()
            exportManager.UnregisterAllExporters()
            exportManager.RegisterExporter(New PlainTextDocumentExporter())
            exportManager.RegisterExporter(New SourcesCodeDocumentExporter())
            richEdit.DocumentSource = DemoUtils.GetRelativePath("SyntaxHighlighting.ts")
        End Sub

        Private Sub RichEditControl_InitializeDocument(ByVal sender As Object, ByVal e As EventArgs)
            Dim document As Document = richEdit.Document
            document.BeginUpdate()
            Try
                document.DefaultCharacterProperties.FontName = "Courier New"
                document.DefaultCharacterProperties.FontSize = 10
                document.Sections(0).Page.Width = Units.InchesToDocumentsF(100)
                document.Sections(0).LineNumbering.CountBy = 1
                document.Sections(0).LineNumbering.RestartType = LineNumberingRestart.Continuous
                Dim tabSize As SizeF = richEdit.MeasureSingleLineString("    ", document.DefaultCharacterProperties)
                Dim tabs As TabInfoCollection = document.Paragraphs(0).BeginUpdateTabs(True)
                Try
                    For i As Integer = 1 To 30
                        Dim tab As TabInfo = New TabInfo()
                        tab.Position = i * tabSize.Width
                        tabs.Add(tab)
                    Next
                Finally
                    document.Paragraphs(0).EndUpdateTabs(tabs)
                End Try
            Finally
                document.EndUpdate()
            End Try
        End Sub
    End Class
End Namespace
