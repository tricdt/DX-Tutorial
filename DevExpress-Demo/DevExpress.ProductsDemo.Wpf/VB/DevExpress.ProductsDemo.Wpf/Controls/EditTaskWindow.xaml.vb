Imports System
Imports DevExpress.Xpf.Core
Imports ProductsDemo.Modules

Namespace ProductsDemo.Controls

    Public Partial Class EditTaskWindow
        Inherits ThemedWindow

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ThemedWindow_Closed(ByVal sender As Object, ByVal e As EventArgs)
            CType(DataContext, EditTaskViewModel).Close()
        End Sub
    End Class
End Namespace
