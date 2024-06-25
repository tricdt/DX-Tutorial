Imports System.Windows.Controls
Imports DevExpress.Xpf.Core.FilteringUI

Namespace GridDemo.Filtering

    Public Partial Class FilterEditorCustomizeOperandValues
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

#Region "!"
        Private Sub FilterEditorControl_OnQueryOperands(ByVal sender As Object, ByVal e As QueryOperandsEventArgs)
            Select Case e.FieldName
                Case "RequiredDate", "ShippedDate"
                    e.AllowPropertyOperand = True
            End Select
        End Sub
#End Region
    End Class
End Namespace
