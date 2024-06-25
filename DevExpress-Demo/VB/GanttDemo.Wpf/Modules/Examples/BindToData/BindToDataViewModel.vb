Imports System
Imports System.Collections.Generic

Namespace GanttDemo.Examples

    Public Class BindToDataViewModel

        Private _Tasks As IEnumerable(Of GanttDemo.Examples.GanttDataItem), _Links As IEnumerable(Of GanttDemo.Examples.GanttDataLink)

        Public Sub New()
#Region "initialization"
            Dim startDate = System.DateTime.Now.[Date].AddDays(3)
            Me.Tasks = New System.Collections.Generic.List(Of GanttDemo.Examples.GanttDataItem) From {New GanttDemo.Examples.GanttDataItem With {.Id = 0, .StartDate = startDate, .FinishDate = startDate + System.TimeSpan.FromDays(5), .Progress = 1, .Name = "Market Analysis"}, New GanttDemo.Examples.GanttDataItem With {.Id = 1, .Name = "Feature Planning", .StartDate = startDate + System.TimeSpan.FromDays(5), .FinishDate = startDate + System.TimeSpan.FromDays(9), .Progress = 1}, New GanttDemo.Examples.GanttDataItem With {.Id = 2, .Name = "Feature 1", .StartDate = startDate + System.TimeSpan.FromDays(9), .FinishDate = startDate + System.TimeSpan.FromDays(16), .Children = {New GanttDemo.Examples.GanttDataItem With {.Id = 3, .Name = "Implementation", .StartDate = startDate + System.TimeSpan.FromDays(9), .FinishDate = startDate + System.TimeSpan.FromDays(13), .Progress = 1}, New GanttDemo.Examples.GanttDataItem With {.Id = 4, .Name = "Demos & Docs", .StartDate = startDate + System.TimeSpan.FromDays(13), .FinishDate = startDate + System.TimeSpan.FromDays(16), .Progress = 1}}}, New GanttDemo.Examples.GanttDataItem With {.Id = 5, .Name = "Feature 2", .StartDate = startDate + System.TimeSpan.FromDays(9), .FinishDate = startDate + System.TimeSpan.FromDays(17), .Progress = 0.85, .Children = {New GanttDemo.Examples.GanttDataItem With {.Id = 6, .Name = "Implementation", .StartDate = startDate + System.TimeSpan.FromDays(9), .FinishDate = startDate + System.TimeSpan.FromDays(13), .Progress = 1}, New GanttDemo.Examples.GanttDataItem With {.Id = 7, .Name = "Demos & Docs", .StartDate = startDate + System.TimeSpan.FromDays(13), .FinishDate = startDate + System.TimeSpan.FromDays(17), .Progress = 0.7}}}, New GanttDemo.Examples.GanttDataItem With {.Id = 8, .Name = "Feature 3", .StartDate = startDate + System.TimeSpan.FromDays(16), .FinishDate = startDate + System.TimeSpan.FromDays(19), .Progress = 0.4, .Children = {New GanttDemo.Examples.GanttDataItem With {.Id = 9, .Name = "Implementation", .StartDate = startDate + System.TimeSpan.FromDays(16), .FinishDate = startDate + System.TimeSpan.FromDays(18), .Progress = 0.8}, New GanttDemo.Examples.GanttDataItem With {.Id = 10, .Name = "Demos & Docs", .StartDate = startDate + System.TimeSpan.FromDays(18), .FinishDate = startDate + System.TimeSpan.FromDays(19), .Progress = 0}}}, New GanttDemo.Examples.GanttDataItem With {.Id = 11, .Name = "Testing & Bug Fixing", .StartDate = startDate + System.TimeSpan.FromDays(19), .FinishDate = startDate + System.TimeSpan.FromDays(23)}, New GanttDemo.Examples.GanttDataItem With {.Id = 12, .Name = "Development finished", .StartDate = startDate + System.TimeSpan.FromDays(23), .FinishDate = startDate + System.TimeSpan.FromDays(23)}}
            Me.Links = New System.Collections.Generic.List(Of GanttDemo.Examples.GanttDataLink) From {New GanttDemo.Examples.GanttDataLink(0, 1), New GanttDemo.Examples.GanttDataLink(1, 3), New GanttDemo.Examples.GanttDataLink(3, 4), New GanttDemo.Examples.GanttDataLink(4, 9), New GanttDemo.Examples.GanttDataLink(9, 10), New GanttDemo.Examples.GanttDataLink(10, 11), New GanttDemo.Examples.GanttDataLink(1, 6), New GanttDemo.Examples.GanttDataLink(6, 7), New GanttDemo.Examples.GanttDataLink(7, 11), New GanttDemo.Examples.GanttDataLink(11, 12)}
#End Region
        End Sub

        Public Property Tasks As IEnumerable(Of GanttDemo.Examples.GanttDataItem)
            Get
                Return _Tasks
            End Get

            Private Set(ByVal value As IEnumerable(Of GanttDemo.Examples.GanttDataItem))
                _Tasks = value
            End Set
        End Property

        Public Property Links As IEnumerable(Of GanttDemo.Examples.GanttDataLink)
            Get
                Return _Links
            End Get

            Private Set(ByVal value As IEnumerable(Of GanttDemo.Examples.GanttDataLink))
                _Links = value
            End Set
        End Property
    End Class

    Public Class GanttDataItem

        Public Property StartDate As DateTime

        Public Property FinishDate As DateTime

        Public Property Id As Integer

        Public Property Name As String

        Public Property Progress As Double

        Public Property Children As IEnumerable(Of GanttDemo.Examples.GanttDataItem)
    End Class

    Public Class GanttDataLink

        Private _SourceTaskId As Integer, _TargetTaskId As Integer

        Public Sub New(ByVal sourceId As Integer, ByVal targetId As Integer)
            Me.SourceTaskId = sourceId
            Me.TargetTaskId = targetId
        End Sub

        Public Property SourceTaskId As Integer
            Get
                Return _SourceTaskId
            End Get

            Private Set(ByVal value As Integer)
                _SourceTaskId = value
            End Set
        End Property

        Public Property TargetTaskId As Integer
            Get
                Return _TargetTaskId
            End Get

            Private Set(ByVal value As Integer)
                _TargetTaskId = value
            End Set
        End Property
    End Class
End Namespace
