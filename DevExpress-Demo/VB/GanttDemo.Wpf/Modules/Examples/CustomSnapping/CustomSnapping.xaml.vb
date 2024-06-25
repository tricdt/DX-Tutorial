Imports DevExpress.Xpf.Gantt
Imports System
Imports System.Collections.Generic
Imports System.Windows.Controls

Namespace GanttDemo.Examples

    Public Partial Class CustomSnapping
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

#Region "!"
        Private Sub TaskMoving(ByVal sender As Object, ByVal e As TaskMovingEventArgs)
            e.StartDate = Round(e.StartDate)
        End Sub

        Private Sub TaskFinishDateMoving(ByVal sender As Object, ByVal e As TaskFinishDateMovingEventArgs)
            e.FinishDate = Round(e.FinishDate, CType(e.Node, GanttNode).StartDate.Value)
        End Sub

        Private Sub TaskMoved(ByVal sender As Object, ByVal e As TaskMovedEventArgs)
            e.StartDate = Round(e.StartDate)
        End Sub

        Private Sub TaskFinishDateMoved(ByVal sender As Object, ByVal e As TaskFinishDateMovedEventArgs)
            e.FinishDate = Round(e.FinishDate, CType(e.Node, GanttNode).StartDate.Value)
        End Sub

        Private Sub BeginNewTaskDraw(ByVal sender As Object, ByVal e As BeginNewTaskDrawEventArgs)
            e.StartDate = Round(e.StartDate)
        End Sub

        Private Sub NewTaskDrawing(ByVal sender As Object, ByVal e As NewTaskDrawingEventArgs)
            e.FinishDate = Round(e.FinishDate, e.StartDate)
        End Sub

        Private Sub NewTaskDrawn(ByVal sender As Object, ByVal e As NewTaskDrawnEventArgs)
            e.FinishDate = Round(e.FinishDate, e.StartDate)
        End Sub

        Private Function UnitCount(ByVal value As Date) As Double
            Return(value - Date.MinValue).TotalMilliseconds / CustomSnappingViewModel.SnapUnit.TotalMilliseconds
        End Function

        Private Function Round(ByVal value As Date) As Date
            Return DateTimeByUnitCount(UnitCount(value))
        End Function

        Private Function DateTimeByUnitCount(ByVal unitCount As Double) As Date
            Return Date.MinValue + TimeSpan.FromMilliseconds(Math.Round(Math.Round(unitCount) * CustomSnappingViewModel.SnapUnit.TotalMilliseconds))
        End Function

        Private Function Round(ByVal value As Date, ByVal limit As Date) As Date
            Return DateTimeByUnitCount(Math.Max(1.0 + UnitCount(limit), UnitCount(value)))
        End Function

#End Region
        Private Sub RequestTimescaleRulers(ByVal sender As Object, ByVal e As RequestTimescaleRulersEventArgs)
            e.TimescaleRulers = New List(Of TimescaleRuler)() From {New TimescaleRuler(TimescaleUnit.Day, "D"), New TimescaleRuler(TimescaleUnit.Hour)}
        End Sub
    End Class
End Namespace
