Imports DevExpress.Mvvm.POCO
Imports System.Windows
Imports DevExpress.Mvvm.DataAnnotations

Namespace RibbonDemo

    <POCOViewModel>
    Public Class PaintPanelViewModel
        Inherits PanelViewModel

        Protected Sub New()
        End Sub

        Public Shared Function Create(ByVal caption As String, ByVal location As Point, ByVal parentViewModel As RibbonMergingViewModel) As PaintPanelViewModel
            Dim instance As PaintPanelViewModel = ViewModelSource.Create(Function() New PaintPanelViewModel())
            instance.Caption = caption
            instance.Location = location
            instance.ParentViewModel = parentViewModel
            Return instance
        End Function
    End Class
End Namespace
