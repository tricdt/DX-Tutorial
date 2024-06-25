Imports System.Windows.Controls
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpf.Core.FilteringUI
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.Settings

Namespace GridDemo.Filtering

    Public Partial Class ColumnOperators
        Inherits UserControl

#Region "!"
        Const CustomFunctionName As String = "LastYears"

        Shared Sub New()
            Dim currentYear = Date.Now.Year
            CriteriaOperator.RegisterCustomFunction(CustomFunctionFactory.Create(CustomFunctionName, Function(ByVal [date] As Date, ByVal threshold As Integer) currentYear >= [date].Year AndAlso currentYear - [date].Year <= threshold))
        End Sub

        Private Sub OnExcelStyleFilterQueryOperators(ByVal sender As Object, ByVal e As ExcelStyleFilterElementQueryOperatorsEventArgs)
            If Equals(e.FieldName, "OrderDate") Then
                e.Operators.Clear()
                e.Operators.Add(New ExcelStyleFilterElementOperatorItem(ExcelStyleFilterElementOperatorType.Equal))
                e.Operators.Add(New ExcelStyleFilterElementOperatorItem(ExcelStyleFilterElementOperatorType.NotEqual))
                Dim customFunctionEditSettings = New BaseEditSettings() {New TextEditSettings With {.MaskType = MaskType.Numeric, .Mask = "D", .MaskUseAsDisplayFormat = True}}
                e.Operators.Add(New ExcelStyleFilterElementOperatorItem(CustomFunctionName, customFunctionEditSettings) With {.Caption = "Last Years"})
            End If
        End Sub

#End Region
        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace
