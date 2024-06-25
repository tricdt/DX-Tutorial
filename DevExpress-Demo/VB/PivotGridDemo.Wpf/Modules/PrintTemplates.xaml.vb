Imports System
Imports System.Windows
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo

    Public Partial Class PrintTemplates
        Inherits PivotGridDemoModule

        Shared Sub New()
            Call CriteriaOperator.RegisterCustomFunction(New MoonPhaseCustomFunction())
        End Sub

        Public Sub New()
            InitializeComponent()
            ResetLayout()
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            pivotGrid.ShowPrintPreview(Window.GetWindow(Me))
        End Sub

        Private Sub ResetLayout()
            fieldCategory.Area = FieldArea.RowArea
            fieldMoonPhase.Area = FieldArea.ColumnArea
            fieldMoonPhase.Visible = False
            fieldMoonPhase.FilterValues.Clear()
            fieldYear.Area = FieldArea.ColumnArea
            fieldYear.Visible = True
            fieldYear.AreaIndex = 0
            fieldYear.FilterValues.Clear()
            fieldQuarter.Area = FieldArea.ColumnArea
            fieldQuarter.Visible = True
            fieldQuarter.AreaIndex = 1
            fieldQuarter.FilterValues.Clear()
            fieldSales.Area = FieldArea.DataArea
            fieldSales.Visible = True
            fieldSales.FilterValues.Clear()
        End Sub

        Private Sub templatesList_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not IsInitialized Then Return
            ResetLayout()
            If templatesList.SelectedIndex = 1 Then
                fieldYear.Visible = False
                fieldQuarter.Visible = False
                fieldMoonPhase.Visible = True
            End If
        End Sub
    End Class

    Public Class MoonPhaseCustomFunction
        Implements ICustomFunctionOperator

        Private ReadOnly Property Name As String Implements ICustomFunctionOperator.Name
            Get
                Return "MoonPhase"
            End Get
        End Property

        Private Function Evaluate(ParamArray operands As Object()) As Object Implements ICustomFunctionOperator.Evaluate
            Return CalculateMoonPhase(CDate(operands(0)))
        End Function

        Private Function ResultType(ParamArray operands As Type()) As Type Implements ICustomFunctionOperator.ResultType
            Return GetType(MoonPhase)
        End Function
    End Class
End Namespace
