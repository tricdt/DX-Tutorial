Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Controls
Imports DevExpress.Mvvm.Gantt

Namespace GanttDemo.Examples

    Public Partial Class CustomizeSummaryTaskProgress
        Inherits System.Windows.Controls.UserControl

        Public Sub New()
#Region "Initialization"
            Me.InitializeComponent()
            Dim startDate = System.DateTime.Now.[Date].AddDays(3)
            Dim tasks = New System.Collections.Generic.List(Of DevExpress.Mvvm.Gantt.GanttTask) From {New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 2, .Name = "Feature 1 (implementation - 80%, demos & docs - 20%)"}, New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 3, .ParentId = 2, .Name = "Implementation (80%)", .StartDate = startDate, .FinishDate = startDate + System.TimeSpan.FromDays(4), .Progress = 0.8, .Tag = 0.8}, New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 4, .ParentId = 2, .Name = "Demos & Docs (20%)", .StartDate = startDate + System.TimeSpan.FromDays(4), .FinishDate = startDate + System.TimeSpan.FromDays(7), .Progress = 0.2, .Tag = 0.2}, New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 5, .Name = "Feature 2 (implementation - 30%, demos & docs - 70%)"}, New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 6, .ParentId = 5, .Name = "Implementation (30%)", .StartDate = startDate, .FinishDate = startDate + System.TimeSpan.FromDays(4), .Progress = 0.8, .Tag = 0.3}, New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 7, .ParentId = 5, .Name = "Demos & Docs (70%)", .StartDate = startDate + System.TimeSpan.FromDays(4), .FinishDate = startDate + System.TimeSpan.FromDays(7), .Progress = 0.2, .Tag = 0.7}}
#End Region
            AddHandler Me.gantt.View.CalculateSummaryTaskProgress, Sub(sender, e)
                e.Progress = 0.0
                For Each task In e.Node.Nodes.[Select](Function(x) CType(x.Content, DevExpress.Mvvm.Gantt.GanttTask))
                    Dim taskWeight = CDbl(task.Tag)
                    e.Progress += taskWeight * task.Progress
                Next

                e.Handled = True
            End Sub
#Region "Scheduling"
            Me.gantt.View.AllowRecreateNodesOnEndDataUpdate = False
            Me.gantt.BeginDataUpdate()
            Me.gantt.ItemsSource = tasks
            Me.gantt.View.ScheduleAll()
            Me.gantt.EndDataUpdate()
#End Region
        End Sub
    End Class
End Namespace
