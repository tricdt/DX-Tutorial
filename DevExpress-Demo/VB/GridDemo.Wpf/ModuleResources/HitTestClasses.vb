Imports DevExpress.Xpf.Grid
Imports System.Windows

Namespace GridDemo

    Public Class DemoTableViewHitTestVisitor
        Inherits TableViewHitTestVisitorBase

        Private ReadOnly hitTest As HitTesting

        Public Sub New(ByVal hitTest As HitTesting)
            Me.hitTest = hitTest
        End Sub

        Public Overrides Sub VisitRowIndicator(ByVal rowHandle As Integer, ByVal indicatorState As IndicatorState)
            hitTest.AddHitInfo("RowIndicatorState", indicatorState.ToString())
            StopHitTesting()
        End Sub

        Public Overrides Sub VisitTotalSummary(ByVal column As ColumnBase)
            hitTest.AddTotalSummaryInfo(column)
        End Sub

        Public Overrides Sub VisitFixedTotalSummary(ByVal summaryText As String)
            hitTest.AddFixedTotalSummaryInfo(summaryText)
        End Sub

        Public Overrides Sub VisitGroupValue(ByVal rowHandle As Integer, ByVal columnData As GridColumnData)
            hitTest.AddGroupValueInfo(columnData)
        End Sub

        Public Overrides Sub VisitGroupSummary(ByVal rowHandle As Integer, ByVal summaryData As GridGroupSummaryData)
            hitTest.AddGroupSummaryInfo(summaryData)
        End Sub
    End Class

    Public Class DemoCardViewHitTestVisitor
        Inherits CardViewHitTestVisitorBase

        Private ReadOnly hitTest As HitTesting

        Public Sub New(ByVal hitTest As HitTesting)
            Me.hitTest = hitTest
        End Sub

        Public Overrides Sub VisitTotalSummary(ByVal column As ColumnBase)
            hitTest.AddTotalSummaryInfo(column)
        End Sub

        Public Overrides Sub VisitFixedTotalSummary(ByVal summaryText As String)
            hitTest.AddFixedTotalSummaryInfo(summaryText)
        End Sub

        Public Overrides Sub VisitGroupValue(ByVal rowHandle As Integer, ByVal columnData As GridColumnData)
            hitTest.AddGroupValueInfo(columnData)
        End Sub

        Public Overrides Sub VisitGroupSummary(ByVal rowHandle As Integer, ByVal summaryData As GridGroupSummaryData)
            hitTest.AddGroupSummaryInfo(summaryData)
        End Sub
    End Class

    Public Class HitTestInfo
        Inherits DependencyObject

        Private _Name As String, _Text As String

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property Text As String
            Get
                Return _Text
            End Get

            Private Set(ByVal value As String)
                _Text = value
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal text As String)
            Me.Name = name
            Me.Text = text
        End Sub
    End Class
End Namespace
