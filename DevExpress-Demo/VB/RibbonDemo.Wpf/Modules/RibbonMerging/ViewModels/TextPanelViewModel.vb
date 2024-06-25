Imports DevExpress.Mvvm.POCO
Imports System.Windows
Imports DevExpress.Mvvm.DataAnnotations

Namespace RibbonDemo

    <POCOViewModel>
    Public Class TextPanelViewModel
        Inherits PanelViewModel

        Protected Sub New()
        End Sub

        Public Shared Function Create(ByVal caption As String, ByVal location As Point, ByVal parentViewModel As RibbonMergingViewModel) As TextPanelViewModel
            Dim instance As TextPanelViewModel = ViewModelSource.Create(Function() New TextPanelViewModel())
            instance.ParentViewModel = parentViewModel
            instance.Caption = caption
            instance.Location = location
            Return instance
        End Function
    End Class
End Namespace
