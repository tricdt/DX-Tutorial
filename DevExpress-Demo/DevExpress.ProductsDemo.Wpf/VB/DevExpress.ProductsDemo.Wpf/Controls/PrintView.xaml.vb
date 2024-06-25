Imports System.Windows
Imports System.Windows.Controls

Namespace ProductsDemo

    Public Partial Class PrintView
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
            AddHandler IsVisibleChanged, AddressOf PrintView_IsVisibleChanged
        End Sub

        Private ReadOnly Property PrintViewModel As PrintViewModel
            Get
                Return TryCast(DataContext, PrintViewModel)
            End Get
        End Property

        Private Sub PrintView_IsVisibleChanged(ByVal sender As Object, ByVal e As DependencyPropertyChangedEventArgs)
            If IsVisible AndAlso PrintViewModel IsNot Nothing Then PrintViewModel.UpdatePrintableControlLink()
        End Sub
    End Class
End Namespace
