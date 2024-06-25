Imports System.Windows.Controls
Imports DevExpress.Xpf.Core.FilteringUI

Namespace GridDemo.Filtering

    Public Partial Class FilterEditorConditionOperations
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

#Region "!"
        Private Sub OnQueryConditionOperations(ByVal sender As Object, ByVal e As QueryConditionOperationsEventArgs)
            e.AllowRemoveCondition = False
        End Sub
#End Region
    End Class
End Namespace
