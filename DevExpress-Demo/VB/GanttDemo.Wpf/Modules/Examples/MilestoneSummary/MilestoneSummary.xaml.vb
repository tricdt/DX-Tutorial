Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Controls
Imports DevExpress.Mvvm.Gantt

Namespace GanttDemo.Examples

    Public Partial Class MilestoneSummary
        Inherits UserControl

        Public Sub New()
#Region "Initialization"
            InitializeComponent()
            Dim projectStartDate = Date.Now.Date.AddDays(3)
            Dim tasks = New List(Of GanttTask) From {New GanttTask With {.Id = 1, .Name = "Finalize the work"}, New GanttTask With {.Id = 2, .ParentId = 1, .Name = "Beta testing", .StartDate = projectStartDate, .FinishDate = projectStartDate + TimeSpan.FromDays(5)}, New GanttTask With {.Id = 3, .ParentId = 1, .Name = "Create a demo", .StartDate = projectStartDate, .FinishDate = projectStartDate + TimeSpan.FromDays(5)}}
#End Region
            AddHandler gantt.View.CalculateSummaryTaskBounds, Sub(sender, e)
                Dim childTasks = e.Node.Nodes.[Select](Function(x) CType(x.Content, GanttTask)).Where(Function(x) x.StartDate.HasValue)
                e.StartDate = childTasks.Aggregate(Date.MaxValue, Function(startDate, task) If(startDate < task.StartDate.Value, startDate, task.StartDate.Value))
                e.FinishDate = e.StartDate
                e.Handled = True
            End Sub
#Region "Scheduling"
            gantt.View.AllowRecreateNodesOnEndDataUpdate = False
            gantt.BeginDataUpdate()
            gantt.ItemsSource = tasks
            gantt.View.ScheduleAll()
            gantt.EndDataUpdate()
#End Region
        End Sub
    End Class
End Namespace
