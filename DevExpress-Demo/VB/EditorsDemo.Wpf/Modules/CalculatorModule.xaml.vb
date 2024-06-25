Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native

Namespace EditorsDemo

    Public Partial Class CalculatorModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub CalculatorCustomErrorText(ByVal sender As Object, ByVal e As CalculatorCustomErrorTextEventArgs)
            If cbCustomErrorText.IsChecked = True Then e.ErrorText += " !!!"
        End Sub

        Private Sub ShowCalculatorWindow(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim calculator As Calculator = New Calculator() With {.ShowBorder = False}
            Call New ThemedWindow() With {.Owner = WindowShowHelper.GetOwner(editor), .Content = calculator, .SizeToContent = SizeToContent.WidthAndHeight, .Title = "Calculator", .WindowStartupLocation = WindowStartupLocation.CenterOwner}.ShowDialog()
            calculator.Focus()
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            calculator.ClearHistory()
        End Sub
    End Class

#Region "Move to Utils"
    Public Class ObjectList
        Inherits List(Of Object)

    End Class
#End Region
End Namespace
