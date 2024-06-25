Imports DevExpress.Mvvm.DataAnnotations

Namespace GridDemo

    <POCOViewModel>
    Public Class AlignGroupSummariesByColumnsViewModel

        Protected Sub New()
            HighlightBestSelling = True
            HighlightWorstSelling = True
        End Sub

        Public Overridable Property HighlightBestSelling As Boolean

        Public Overridable Property HighlightWorstSelling As Boolean
    End Class
End Namespace
