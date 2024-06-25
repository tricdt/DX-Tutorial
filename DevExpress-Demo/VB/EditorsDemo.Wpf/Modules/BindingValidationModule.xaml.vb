Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors
Imports System.Windows
Imports System.Windows.Data

Namespace EditorsDemo

    <CodeFile("ViewModels/ValidationViewModelBase.(cs)")>
    <CodeFile("ViewModels/BindingValidationViewModel.(cs)")>
    Public Partial Class BindingValidationModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            DataContext = New BindingValidationViewModel()
        End Sub

        Private Sub OnMailChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim expression As BindingExpression = BindingOperations.GetBindingExpression(txtConfirmMail, BaseEdit.EditValueProperty)
            If expression IsNot Nothing Then expression.UpdateTarget()
        End Sub

        Private Sub OnConfirmMailChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim expression As BindingExpression = BindingOperations.GetBindingExpression(txtMail, BaseEdit.EditValueProperty)
            If expression IsNot Nothing Then expression.UpdateTarget()
        End Sub

        Private Sub OnCardTypeChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim expression As BindingExpression = BindingOperations.GetBindingExpression(txtCardNumber, BaseEdit.EditValueProperty)
            If expression IsNot Nothing Then expression.UpdateTarget()
        End Sub

        Private Sub OnClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MessageBox.Show("Thank you!", "Joined", MessageBoxButton.OK)
        End Sub
    End Class
End Namespace
