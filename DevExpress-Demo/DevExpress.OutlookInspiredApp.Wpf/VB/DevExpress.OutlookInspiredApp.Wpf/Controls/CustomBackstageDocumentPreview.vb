Imports System.Windows
Imports DevExpress.Xpf.Printing
Imports DevExpress.XtraReports

Namespace DevExpress.DevAV.Controls

    Public Class CustomBackstageDocumentPreview
        Inherits BackstageDocumentPreview

        Public Shared ReadOnly DocumentSourceProperty As DependencyProperty = DependencyProperty.Register("DocumentSource", GetType(IReport), GetType(CustomBackstageDocumentPreview), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property DocumentSource As IReport
            Get
                Return CType(GetValue(DocumentSourceProperty), IReport)
            End Get

            Set(ByVal value As IReport)
                SetValue(DocumentSourceProperty, value)
            End Set
        End Property
    End Class
End Namespace
