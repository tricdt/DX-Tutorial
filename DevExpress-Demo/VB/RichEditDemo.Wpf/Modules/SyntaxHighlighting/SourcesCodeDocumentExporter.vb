Imports DevExpress.Office.Internal
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.Export

Namespace RichEditDemo

    Public Class SourcesCodeDocumentExporter
        Inherits PlainTextDocumentExporter

        Public Overrides ReadOnly Property Filter As FileDialogFilter
            Get
                Return SourcesCodeDocumentImporter._filter
            End Get
        End Property

        Public Overrides ReadOnly Property Format As DocumentFormat
            Get
                Return Id
            End Get
        End Property
    End Class
End Namespace
