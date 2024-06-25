Imports System.Windows

Namespace PivotGridDemo

    Public Partial Class PrintOptions
        Inherits PivotGridDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            pivotGrid.BestFit(True, False)
            pivotGrid.ShowPrintPreview(Window.GetWindow(Me))
        End Sub
    End Class
End Namespace
