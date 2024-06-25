Imports DevExpress.Mvvm.DataAnnotations

Namespace MapDemo

    <POCOViewModel>
    Public Class ProductGroupViewModel

        Public Overridable Property Name As String

        Public Overridable Property Value As Double

        Public Sub New(ByVal value As Double, ByVal productName As String)
            Me.Value = value
            Name = productName
        End Sub
    End Class
End Namespace
