Imports System.Windows.Controls
Imports DevExpress.Xpf.Core.FilteringUI

Namespace GridDemo.Filtering

    Public Partial Class FilterEditorGroupOperations
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

#Region "!"
        Private Sub OnQueryGroupOperations(ByVal sender As Object, ByVal e As QueryGroupOperationsEventArgs)
            e.AllowAddCustomExpression = False
        End Sub
#End Region
    End Class
End Namespace
