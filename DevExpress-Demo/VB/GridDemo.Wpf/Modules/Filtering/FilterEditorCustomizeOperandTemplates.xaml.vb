Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Core.FilteringUI

Namespace GridDemo.Filtering

    Public Partial Class FilterEditorCustomizeOperandTemplates
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

#Region "!"
        Private Sub OnQueryOperators(ByVal sender As Object, ByVal e As FilterEditorQueryOperatorsEventArgs)
            If Equals(e.FieldName, "Quantity") Then
                Dim template = CType(FindResource("ternaryTemplate"), DataTemplate)
                e.Operators(FilterEditorOperatorType.Between).OperandTemplate = template
                e.Operators(FilterEditorOperatorType.NotBetween).OperandTemplate = template
            End If
        End Sub
#End Region
    End Class
End Namespace
