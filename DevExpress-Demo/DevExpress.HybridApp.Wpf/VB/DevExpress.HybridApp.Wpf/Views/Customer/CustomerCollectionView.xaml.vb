Imports System.Windows.Controls
Imports System.Windows

Namespace DevExpress.DevAV.Views

    Public Partial Class CustomerCollectionView
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
        End Sub
    End Class

    Public Class SlideViewTemplateSelector
        Inherits DataTemplateSelector

        Public Property ContactsTemplate As DataTemplate

        Public Property StoresTemplate As DataTemplate

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            If TypeOf item Is CustomerEmployee Then Return ContactsTemplate
            If TypeOf item Is CustomerStore Then Return StoresTemplate
            Return MyBase.SelectTemplate(item, container)
        End Function
    End Class
End Namespace
