Imports System
Imports System.Threading
Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.DemoBase.Helpers.TextColorizer
Imports DevExpress.Xpf.Editors
Imports System.Windows.Controls
Imports DevExpress.Xpf.Editors.Helpers
Imports DevExpress.Xpf.PivotGrid
Imports DevExpress.Xpf.Printing

Namespace PivotGridDemo.Tests

    Public Class PivotGridCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function CanRunModule(ByVal moduleType As Type) As Boolean
            Return moduleType IsNot GetType(ServerMode) AndAlso moduleType IsNot GetType(OLAPBrowser) AndAlso moduleType IsNot GetType(OLAPKPI)
        End Function

        Protected Overrides Function AllowCheckCodeFile(ByVal moduleType As Type, ByVal fileLanguage As CodeLanguage) As Boolean
            Return MyBase.AllowCheckCodeFile(moduleType, fileLanguage) AndAlso Not(moduleType Is GetType(ServerMode) AndAlso fileLanguage = CodeLanguage.VB)
        End Function

        Protected Overrides Sub CreateCheckOptionsAction()
            AddCheck0()
            CreateCheck1()
        End Sub

        Private Sub CreateCheck1()
            If DemoBaseTesting.CurrentDemoModule.GetType() Is GetType(ChartsIntegration) Then Dispatch(New Action(AddressOf HideFilterAction))
            Dispatch(New Action(AddressOf AssertTextEditsWidthB185051))
            Dispatch(New Action(AddressOf TextBlockTextTrimmingB185312))
            If DemoBaseTesting.CurrentDemoModule.GetType() Is GetType(PrintTemplates) Then
                Dim pivotGrid As PivotGridControl = DispatchExpr(Function() FindElement(Of PivotGridControl)(DemoBaseTesting.CurrentDemoModule))
                Dim cont As Window = DispatchExpr(Function() PrintHelper.ShowPrintPreview(pivotGrid, pivotGrid))
                DispatchBusyWait(Function() cont.IsLoaded)
                Thread.Sleep(2000)
                Dispatch(Sub() cont.Close())
            End If
        End Sub

        Private Sub AddCheck0()
            Dispatch(New Action(AddressOf ComboBoxItemsEditableB181973))
            Dispatch(Sub()
                If DemoBaseTesting.CurrentDemoModule.GetType() Is GetType(ChartsIntegration) Then ShowFilterAction()
            End Sub)
            Dispatch(Sub()
                If DemoBaseTesting.CurrentDemoModule.GetType() Is GetType(ChartsIntegration) Then BestFitAction()
            End Sub)
        End Sub

        Private Sub TextBlockTextTrimmingB185312()
            Dim control As FrameworkElement = DemoBaseTesting.CurrentDemoModule.Options
            If control Is Nothing Then Return
            Dim editors As List(Of TextBlock) = FindAllElements(Of TextBlock)(control)
            For Each edit As TextBlock In editors
                Dim trimmed As Boolean = TextBlockService.CalcIsTextTrimmed(edit) AndAlso edit.DesiredSize.Width > edit.ActualWidth + edit.Margin.Left + edit.Margin.Right
                AssertLog.IsFalse(trimmed, "Text is trimmed: " & DemoBaseTesting.CurrentDemoModule.GetType().Name & " text=" & edit.Text & ", Name=" & edit.Name & ", actual " & edit.ActualWidth & "px, desired:" & edit.DesiredSize.Width & "px")
            Next
        End Sub

        Private Sub AssertTextEditsWidthB185051()
            Dim control As FrameworkElement = DemoBaseTesting.CurrentDemoModule.Options
            If control Is Nothing Then Return
            Dim editors As List(Of TextEdit) = FindAllElements(Of TextEdit)(control)
            For Each edit As TextEdit In editors
                If edit.EditMode = EditMode.InplaceInactive OrElse edit.IsPrintingMode = True Then Continue For
                AssertLog.IsFalse(edit.Width = Double.PositiveInfinity, "TextEdit unlimited width: " & DemoBaseTesting.CurrentDemoModule.GetType().Name & " text=" & edit.Text & ", Name=" & edit.Name & " desired " & edit.ActualWidth & "px")
            Next
        End Sub

        Private Sub ComboBoxItemsEditableB181973()
            If DemoBaseTesting.CurrentDemoModule.GetType() Is GetType(ExcelStyleFiltering) Then Return
            Dim control As FrameworkElement = DemoBaseTesting.CurrentDemoModule.Options
            If control Is Nothing Then Return
            Dim editors As List(Of ComboBoxEdit) = FindAllElements(Of ComboBoxEdit)(control)
            For i As Integer = 0 To editors.Count - 1
                AssertLog.IsTrue(editors(i).IsTextEditable = False, "ComboBoxEdit items editable: " & DemoBaseTesting.CurrentDemoModule.GetType().Name & " " & editors(i).Name)
                AssertLog.IsFalse(editors(i).SelectedIndex < 0, "ComboBoxEdit item is not selected: " & DemoBaseTesting.CurrentDemoModule.GetType().Name & " " & editors(i).Name)
            Next
        End Sub

        Private Function ChartGeneralOptionsCondition() As Boolean
            Return DemoBaseTesting.CurrentDemoModule.GetType() Is GetType(ChartsIntegration)
        End Function

        Private Sub BestFitAction()
            Dim pivotGrid As PivotGridControl = FindElement(Of PivotGridControl)(DemoBaseTesting.CurrentDemoModule)
            pivotGrid.BestFit()
        End Sub

        Private Sub HideFilterAction()
            UpdateLayoutAndDoEvents()
            Dim pivotGrid As PivotGridControl = FindElement(Of PivotGridControl)(DemoBaseTesting.CurrentDemoModule)
            If pivotGrid IsNot Nothing AndAlso pivotGrid.PrefilterContainer IsNot Nothing Then pivotGrid.PrefilterContainer.Close()
            UpdateLayoutAndDoEvents()
        End Sub

        Private Sub ShowFilterAction()
            UpdateLayoutAndDoEvents()
            Dim pivotGrid As PivotGridControl = FindElement(Of PivotGridControl)(DemoBaseTesting.CurrentDemoModule)
            If pivotGrid Is Nothing OrElse Not pivotGrid.UseLegacyFilterEditor.HasValue OrElse Not pivotGrid.UseLegacyFilterEditor.Value Then Return
            pivotGrid.ShowFilterEditor()
            UpdateLayoutAndDoEvents()
        End Sub

        Private Function FindAllElements(Of T As FrameworkElement)(ByVal element As FrameworkElement) As List(Of T)
            Dim items As List(Of T) = New List(Of T)()
            Call LayoutHelper.ForEachElement(element, Sub(d) addItem(items, d))
            Return items
        End Function

        Private Function addItem(Of T As FrameworkElement)(ByVal items As List(Of T), ByVal d As FrameworkElement) As Boolean
            If d.GetType() Is GetType(T) Then items.Add(CType(d, T))
            Return True
        End Function

        Private Function FindElement(Of T As FrameworkElement)(ByVal element As FrameworkElement) As T
            Return CType(LayoutHelper.FindElement(element, Function(d) d.GetType() Is GetType(T)), T)
        End Function
    End Class
End Namespace
