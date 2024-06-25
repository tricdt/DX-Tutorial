Imports System
Imports System.Windows.Controls

Namespace DevExpress.DevAV.Views

    Public Partial Class ProductView
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub PdfViewerControl_ManipulationBoundaryFeedback(ByVal sender As Object, ByVal e As Windows.Input.ManipulationBoundaryFeedbackEventArgs)
            e.Handled = True
        End Sub
    End Class
End Namespace
