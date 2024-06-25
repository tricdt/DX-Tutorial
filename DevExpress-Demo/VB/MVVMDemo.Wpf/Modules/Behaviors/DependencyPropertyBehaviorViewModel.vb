Imports System.ComponentModel.DataAnnotations
Imports System.Windows

Namespace MVVMDemo.Behaviors

    Public Class DependencyPropertyBehaviorViewModel

        Public Overridable Property SelectedText As String

        Public Sub ShowSelectedText()
            MessageBox.Show(SelectedText)
        End Sub

        Public Function CanShowSelectedText() As Boolean
            Return Not String.IsNullOrEmpty(SelectedText)
        End Function
    End Class
End Namespace
