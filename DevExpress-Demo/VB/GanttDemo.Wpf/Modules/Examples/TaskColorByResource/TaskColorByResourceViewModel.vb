Imports DevExpress.Mvvm.Gantt
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Windows.Media

Namespace GanttDemo.Examples

    Public Class TaskColorByResourceViewModel

        Private _Tasks As IEnumerable(Of DevExpress.Mvvm.Gantt.GanttTask), _Resources As IEnumerable(Of DevExpress.Mvvm.Gantt.GanttResource)

        Public Sub New()
#Region "initialization"
            Dim startDate = System.DateTime.Now.[Date].AddDays(3)
            Me.Tasks = New System.Collections.Generic.List(Of DevExpress.Mvvm.Gantt.GanttTask) From {New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 0, .StartDate = startDate, .FinishDate = startDate + System.TimeSpan.FromDays(5), .Name = "Market Analysis", .ResourceLinks = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Mvvm.Gantt.GanttResourceLink)() From {New DevExpress.Mvvm.Gantt.GanttResourceLink() With {.ResourceId = 1}}}, New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 1, .Name = "Feature Planning", .StartDate = startDate + System.TimeSpan.FromDays(5), .FinishDate = startDate + System.TimeSpan.FromDays(9), .ResourceLinks = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Mvvm.Gantt.GanttResourceLink)() From {New DevExpress.Mvvm.Gantt.GanttResourceLink() With {.ResourceId = 1}}, .PredecessorLinks = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Mvvm.Gantt.GanttPredecessorLink)() From {New DevExpress.Mvvm.Gantt.GanttPredecessorLink() With {.PredecessorTaskId = 0}}}, New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 2, .Name = "Feature 1", .StartDate = startDate + System.TimeSpan.FromDays(9), .FinishDate = startDate + System.TimeSpan.FromDays(16)}, New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 3, .ParentId = 2, .Name = "Implementation", .StartDate = startDate + System.TimeSpan.FromDays(9), .FinishDate = startDate + System.TimeSpan.FromDays(13), .ResourceLinks = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Mvvm.Gantt.GanttResourceLink)() From {New DevExpress.Mvvm.Gantt.GanttResourceLink() With {.ResourceId = 2}}, .PredecessorLinks = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Mvvm.Gantt.GanttPredecessorLink)() From {New DevExpress.Mvvm.Gantt.GanttPredecessorLink() With {.PredecessorTaskId = 1}}}, New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 4, .ParentId = 2, .Name = "Demos & Docs", .StartDate = startDate + System.TimeSpan.FromDays(13), .FinishDate = startDate + System.TimeSpan.FromDays(16), .ResourceLinks = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Mvvm.Gantt.GanttResourceLink)() From {New DevExpress.Mvvm.Gantt.GanttResourceLink() With {.ResourceId = 4}}, .PredecessorLinks = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Mvvm.Gantt.GanttPredecessorLink)() From {New DevExpress.Mvvm.Gantt.GanttPredecessorLink() With {.PredecessorTaskId = 3}}}, New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 5, .Name = "Feature 2", .StartDate = startDate + System.TimeSpan.FromDays(9), .FinishDate = startDate + System.TimeSpan.FromDays(17)}, New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 6, .ParentId = 5, .Name = "Implementation", .StartDate = startDate + System.TimeSpan.FromDays(9), .FinishDate = startDate + System.TimeSpan.FromDays(13), .ResourceLinks = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Mvvm.Gantt.GanttResourceLink)() From {New DevExpress.Mvvm.Gantt.GanttResourceLink() With {.ResourceId = 2}}, .PredecessorLinks = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Mvvm.Gantt.GanttPredecessorLink)() From {New DevExpress.Mvvm.Gantt.GanttPredecessorLink() With {.PredecessorTaskId = 1}}}, New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 7, .ParentId = 5, .Name = "Demos & Docs", .StartDate = startDate + System.TimeSpan.FromDays(13), .FinishDate = startDate + System.TimeSpan.FromDays(17), .ResourceLinks = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Mvvm.Gantt.GanttResourceLink)() From {New DevExpress.Mvvm.Gantt.GanttResourceLink() With {.ResourceId = 4}}, .PredecessorLinks = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Mvvm.Gantt.GanttPredecessorLink)() From {New DevExpress.Mvvm.Gantt.GanttPredecessorLink() With {.PredecessorTaskId = 6}}}, New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 8, .Name = "Feature 3", .StartDate = startDate + System.TimeSpan.FromDays(16), .FinishDate = startDate + System.TimeSpan.FromDays(19)}, New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 9, .ParentId = 8, .Name = "Implementation", .StartDate = startDate + System.TimeSpan.FromDays(16), .FinishDate = startDate + System.TimeSpan.FromDays(18), .ResourceLinks = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Mvvm.Gantt.GanttResourceLink)() From {New DevExpress.Mvvm.Gantt.GanttResourceLink() With {.ResourceId = 2}}, .PredecessorLinks = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Mvvm.Gantt.GanttPredecessorLink)() From {New DevExpress.Mvvm.Gantt.GanttPredecessorLink() With {.PredecessorTaskId = 4}}}, New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 10, .ParentId = 8, .Name = "Demos & Docs", .StartDate = startDate + System.TimeSpan.FromDays(18), .FinishDate = startDate + System.TimeSpan.FromDays(19), .ResourceLinks = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Mvvm.Gantt.GanttResourceLink)() From {New DevExpress.Mvvm.Gantt.GanttResourceLink() With {.ResourceId = 4}}, .PredecessorLinks = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Mvvm.Gantt.GanttPredecessorLink)() From {New DevExpress.Mvvm.Gantt.GanttPredecessorLink() With {.PredecessorTaskId = 9}}}, New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 11, .Name = "Testing & Bug Fixing", .StartDate = startDate + System.TimeSpan.FromDays(19), .FinishDate = startDate + System.TimeSpan.FromDays(23), .ResourceLinks = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Mvvm.Gantt.GanttResourceLink)() From {New DevExpress.Mvvm.Gantt.GanttResourceLink() With {.ResourceId = 3}}, .PredecessorLinks = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Mvvm.Gantt.GanttPredecessorLink)() From {New DevExpress.Mvvm.Gantt.GanttPredecessorLink() With {.PredecessorTaskId = 7}, New DevExpress.Mvvm.Gantt.GanttPredecessorLink() With {.PredecessorTaskId = 10}}}, New DevExpress.Mvvm.Gantt.GanttTask With {.Id = 12, .Name = "Development finished", .StartDate = startDate + System.TimeSpan.FromDays(23), .FinishDate = startDate + System.TimeSpan.FromDays(23), .PredecessorLinks = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Mvvm.Gantt.GanttPredecessorLink)() From {New DevExpress.Mvvm.Gantt.GanttPredecessorLink() With {.PredecessorTaskId = 11}}}}
            Me.Resources = New System.Collections.Generic.List(Of DevExpress.Mvvm.Gantt.GanttResource) From {New DevExpress.Mvvm.Gantt.GanttResource With {.Name = "Management", .Id = 1, .Color = System.Windows.Media.Colors.Green}, New DevExpress.Mvvm.Gantt.GanttResource With {.Name = "Developers", .Id = 2, .Color = System.Windows.Media.Colors.Red}, New DevExpress.Mvvm.Gantt.GanttResource With {.Name = "Testers", .Id = 3, .Color = System.Windows.Media.Colors.Purple}, New DevExpress.Mvvm.Gantt.GanttResource With {.Name = "Technical Writers", .Id = 4, .Color = System.Windows.Media.Colors.Navy}}
#End Region
        End Sub

        Public Property Tasks As IEnumerable(Of DevExpress.Mvvm.Gantt.GanttTask)
            Get
                Return _Tasks
            End Get

            Private Set(ByVal value As IEnumerable(Of DevExpress.Mvvm.Gantt.GanttTask))
                _Tasks = value
            End Set
        End Property

        Public Property Resources As IEnumerable(Of DevExpress.Mvvm.Gantt.GanttResource)
            Get
                Return _Resources
            End Get

            Private Set(ByVal value As IEnumerable(Of DevExpress.Mvvm.Gantt.GanttResource))
                _Resources = value
            End Set
        End Property
    End Class
End Namespace
