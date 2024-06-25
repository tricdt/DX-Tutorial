Imports System.Windows.Controls
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpf.Core.FilteringUI
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.Settings

Namespace GridDemo.Filtering

    Public Partial Class FilterEditorOperators
        Inherits UserControl

        Const CustomFunctionName As String = "LastYears"

        Shared Sub New()
#Region "!"
            Dim currentYear = Date.Now.Year
            CriteriaOperator.RegisterCustomFunction(CustomFunctionFactory.Create(CustomFunctionName, Function(ByVal [date] As Date, ByVal threshold As Integer) currentYear >= [date].Year AndAlso currentYear - [date].Year <= threshold))
#End Region
        End Sub

        Public Sub New()
            InitializeComponent()
        End Sub

#Region "!"
        Private Sub OnQueryOperators(ByVal sender As Object, ByVal e As FilterEditorQueryOperatorsEventArgs)
            If Equals(e.FieldName, "OrderDate") Then
                e.Operators.Clear()
                e.Operators.Add(New FilterEditorOperatorItem(FilterEditorOperatorType.Equal))
                e.Operators.Add(New FilterEditorOperatorItem(FilterEditorOperatorType.NotEqual))
                Dim customFunctionEditSettings = New BaseEditSettings() {New TextEditSettings With {.MaskType = MaskType.Numeric, .Mask = "D", .MaskUseAsDisplayFormat = True}}
                e.Operators.Add(New FilterEditorOperatorItem(CustomFunctionName, customFunctionEditSettings) With {.Caption = "Last Years"})
            End If
        End Sub
#End Region
    End Class
End Namespace
