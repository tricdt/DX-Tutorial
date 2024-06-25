Imports DevExpress.Office.Internal
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.Import

Namespace RichEditDemo

    Public Class SourcesCodeDocumentImporter
        Inherits PlainTextDocumentImporter

        Friend Shared ReadOnly _filter As FileDialogFilter = New FileDialogFilter("Source Files", New String() {"ts"})

        Public Overrides ReadOnly Property Filter As FileDialogFilter
            Get
                Return _filter
            End Get
        End Property

        Public Overrides ReadOnly Property Format As DocumentFormat
            Get
                Return Id
            End Get
        End Property
    End Class
End Namespace
