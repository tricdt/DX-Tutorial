Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Mvvm.POCO

Namespace ChartsDemo

    Public Class DrillDownViewModel

        Private _HierarchicalData As List(Of ChartsDemo.DevAVBranch)

        Public Shared Function Create() As DrillDownViewModel
            Return ViewModelSource.Create(Function() New DrillDownViewModel())
        End Function

        Public Property HierarchicalData As List(Of DevAVBranch)
            Get
                Return _HierarchicalData
            End Get

            Protected Set(ByVal value As List(Of DevAVBranch))
                _HierarchicalData = value
            End Set
        End Property

        Public Overridable Property DiagramRotated As Boolean

        Public Overridable Property ShowValuesZeroLevel As Boolean

        Public Overridable Property SideMargins As Double

        Public Overridable Property ToolTipEnabled As Boolean

        Public Overridable Property CrosshairEnabled As Boolean

        Public Overridable Property AxisYTextPattern As String

        Protected Sub New()
            Dim sales As DevAVSales = New DevAVSales()
            HierarchicalData = sales.GetHierarchicalSalesData()
        End Sub

        Public Sub OnDrillDownStateChanged(ByVal breadcrumbPath As List(Of Object))
            Dim last As Object = breadcrumbPath.Last()
            If last Is Nothing Then
                DiagramRotated = True
                ShowValuesZeroLevel = True
                SideMargins = 0.5
                CrosshairEnabled = False
                ToolTipEnabled = True
                AxisYTextPattern = "${V:0,,.##}M"
            ElseIf TypeOf last Is DevAVBranch Then
                DiagramRotated = False
                ShowValuesZeroLevel = True
                SideMargins = 0.5
                CrosshairEnabled = True
                ToolTipEnabled = False
                AxisYTextPattern = "${V:0,.##}K"
            ElseIf TypeOf last Is DevAVProductCategory Then
                DiagramRotated = False
                ShowValuesZeroLevel = True
                SideMargins = 0
                CrosshairEnabled = True
                ToolTipEnabled = False
                AxisYTextPattern = "${V:0,.##}K"
            ElseIf TypeOf last Is DevAVProduct Then
                DiagramRotated = False
                ShowValuesZeroLevel = False
                SideMargins = 0
                CrosshairEnabled = True
                ToolTipEnabled = False
                AxisYTextPattern = "${V:0.##}"
            End If
        End Sub
    End Class
End Namespace
