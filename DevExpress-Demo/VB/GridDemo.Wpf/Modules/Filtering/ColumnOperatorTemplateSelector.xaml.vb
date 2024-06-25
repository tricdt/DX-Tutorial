Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Core.FilteringUI

Namespace GridDemo.Filtering

    Public Partial Class ColumnOperatorTemplateSelector
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

#Region "!"
        Private Sub OnExcelStyleFilterQueryOperators(ByVal sender As Object, ByVal e As ExcelStyleFilterElementQueryOperatorsEventArgs)
            If Equals(e.FieldName, "Quantity") Then
                Dim template = CType(FindResource("ternaryTemplate"), DataTemplate)
                e.Operators(ExcelStyleFilterElementOperatorType.Between).OperandTemplate = template
                e.Operators(ExcelStyleFilterElementOperatorType.NotBetween).OperandTemplate = template
            End If
        End Sub
#End Region
    End Class
End Namespace
