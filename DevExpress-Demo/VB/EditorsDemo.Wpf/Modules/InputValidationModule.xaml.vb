Imports DevExpress.Xpf.DemoBase
Imports System.Windows

Namespace EditorsDemo

    <CodeFile("ViewModels/ValidationViewModelBase.(cs)")>
    <CodeFile("ViewModels/InputValidationViewModel.(cs)")>
    Public Partial Class InputValidationModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Overloads Sub OnGotFocus(ByVal sender As Object, ByVal e As RoutedEventArgs)
            settings.DataContext = sender
        End Sub

        Private Sub OnCardTypeChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If txtCardNumber IsNot Nothing Then txtCardNumber.DoValidate()
        End Sub

        Private Sub OnMailChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If txtConfirmMail IsNot Nothing Then txtConfirmMail.DoValidate()
        End Sub
    End Class
End Namespace
