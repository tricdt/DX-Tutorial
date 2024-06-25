Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm.Gantt

Namespace GanttDemo.Examples

    Public Class ProjectTaskViewModel

        Public Property Tasks As ObservableCollection(Of GanttTask)

        Public Sub New()
            Dim now = Date.Today.AddHours(9)
            Tasks = New ObservableCollection(Of GanttTask) From {New GanttTask() With {.Id = 0, .Name = "Add a new feature", .Progress = .65, .StartDate = now.AddDays(-1), .FinishDate = now.AddDays(6)}, New GanttTask() With {.Id = 1, .ParentId = 0, .Name = "Write the code", .Progress = 1, .StartDate = now.AddDays(-1), .FinishDate = now.AddDays(2)}, New GanttTask() With {.Id = 2, .ParentId = 0, .Name = "Write the docs", .Progress = .3, .StartDate = now.AddDays(2), .FinishDate = now.AddDays(5), .PredecessorLinks = New ObservableCollection(Of GanttPredecessorLink) From {New GanttPredecessorLink() With {.PredecessorTaskId = 1, .LinkType = PredecessorLinkType.FinishToStart}}}, New GanttTask() With {.Id = 3, .ParentId = 0, .Name = "Test the new feature", .Progress = .6, .StartDate = now.AddDays(2), .FinishDate = now.AddDays(5), .PredecessorLinks = New ObservableCollection(Of GanttPredecessorLink) From {New GanttPredecessorLink() With {.PredecessorTaskId = 1, .LinkType = PredecessorLinkType.FinishToStart}}}, New GanttTask() With {.Id = 4, .ParentId = 0, .Name = "Release the new feature", .StartDate = now.AddDays(5), .FinishDate = now.AddDays(6), .PredecessorLinks = New ObservableCollection(Of GanttPredecessorLink) From {New GanttPredecessorLink With {.PredecessorTaskId = 2, .LinkType = PredecessorLinkType.FinishToStart}, New GanttPredecessorLink With {.PredecessorTaskId = 3, .LinkType = PredecessorLinkType.FinishToStart}}}, New GanttTask With {.Id = 5, .ParentId = 0, .Name = "Feature is released", .StartDate = now.AddDays(6), .FinishDate = now.AddDays(6), .PredecessorLinks = New ObservableCollection(Of GanttPredecessorLink) From {New GanttPredecessorLink With {.PredecessorTaskId = 4}}}}
        End Sub
    End Class
End Namespace
