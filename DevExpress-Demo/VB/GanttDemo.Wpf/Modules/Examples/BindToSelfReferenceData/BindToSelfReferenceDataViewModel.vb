Imports System
Imports System.Collections.Generic

Namespace GanttDemo.Examples

    Public Class BindToSelfReferenceDataViewModel

        Private _Tasks As IEnumerable(Of GanttDemo.Examples.GanttTaskItem)

        Public Sub New()
#Region "initialization"
            Dim startDate = System.DateTime.Now.[Date].AddDays(3)
            Me.Tasks = New System.Collections.Generic.List(Of GanttDemo.Examples.GanttTaskItem) From {New GanttDemo.Examples.GanttTaskItem With {.Id = 0, .StartDate = startDate, .FinishDate = startDate + System.TimeSpan.FromDays(5), .Progress = 1, .Name = "Market Analysis"}, New GanttDemo.Examples.GanttTaskItem With {.Id = 1, .Name = "Feature Planning", .StartDate = startDate + System.TimeSpan.FromDays(5), .FinishDate = startDate + System.TimeSpan.FromDays(9), .Progress = 1, .Links = {New GanttDemo.Examples.GanttTaskLink(0)}}, New GanttDemo.Examples.GanttTaskItem With {.Id = 2, .Name = "Feature 1", .StartDate = startDate + System.TimeSpan.FromDays(9), .FinishDate = startDate + System.TimeSpan.FromDays(16)}, New GanttDemo.Examples.GanttTaskItem With {.Id = 3, .ParentId = 2, .Name = "Implementation", .StartDate = startDate + System.TimeSpan.FromDays(9), .FinishDate = startDate + System.TimeSpan.FromDays(13), .Progress = 1, .Links = {New GanttDemo.Examples.GanttTaskLink(1)}}, New GanttDemo.Examples.GanttTaskItem With {.Id = 4, .ParentId = 2, .Name = "Demos & Docs", .StartDate = startDate + System.TimeSpan.FromDays(13), .FinishDate = startDate + System.TimeSpan.FromDays(16), .Progress = 1, .Links = {New GanttDemo.Examples.GanttTaskLink(3)}}, New GanttDemo.Examples.GanttTaskItem With {.Id = 5, .Name = "Feature 2", .StartDate = startDate + System.TimeSpan.FromDays(9), .FinishDate = startDate + System.TimeSpan.FromDays(17), .Progress = 0.85}, New GanttDemo.Examples.GanttTaskItem With {.Id = 6, .ParentId = 5, .Name = "Implementation", .StartDate = startDate + System.TimeSpan.FromDays(9), .FinishDate = startDate + System.TimeSpan.FromDays(13), .Progress = 1, .Links = {New GanttDemo.Examples.GanttTaskLink(1)}}, New GanttDemo.Examples.GanttTaskItem With {.Id = 7, .ParentId = 5, .Name = "Demos & Docs", .StartDate = startDate + System.TimeSpan.FromDays(13), .FinishDate = startDate + System.TimeSpan.FromDays(17), .Progress = 0.7, .Links = {New GanttDemo.Examples.GanttTaskLink(6)}}, New GanttDemo.Examples.GanttTaskItem With {.Id = 8, .Name = "Feature 3", .StartDate = startDate + System.TimeSpan.FromDays(16), .FinishDate = startDate + System.TimeSpan.FromDays(19), .Progress = 0.4}, New GanttDemo.Examples.GanttTaskItem With {.Id = 9, .ParentId = 8, .Name = "Implementation", .StartDate = startDate + System.TimeSpan.FromDays(16), .FinishDate = startDate + System.TimeSpan.FromDays(18), .Progress = 0.8, .Links = {New GanttDemo.Examples.GanttTaskLink(4)}}, New GanttDemo.Examples.GanttTaskItem With {.Id = 10, .ParentId = 8, .Name = "Demos & Docs", .StartDate = startDate + System.TimeSpan.FromDays(18), .FinishDate = startDate + System.TimeSpan.FromDays(19), .Progress = 0, .Links = {New GanttDemo.Examples.GanttTaskLink(9)}}, New GanttDemo.Examples.GanttTaskItem With {.Id = 11, .Name = "Testing & Bug Fixing", .StartDate = startDate + System.TimeSpan.FromDays(19), .FinishDate = startDate + System.TimeSpan.FromDays(23), .Links = {New GanttDemo.Examples.GanttTaskLink(7), New GanttDemo.Examples.GanttTaskLink(10)}}, New GanttDemo.Examples.GanttTaskItem With {.Id = 12, .Name = "Development finished", .StartDate = startDate + System.TimeSpan.FromDays(23), .FinishDate = startDate + System.TimeSpan.FromDays(23), .Links = {New GanttDemo.Examples.GanttTaskLink(11)}}}
#End Region
        End Sub

        Public Property Tasks As IEnumerable(Of GanttDemo.Examples.GanttTaskItem)
            Get
                Return _Tasks
            End Get

            Private Set(ByVal value As IEnumerable(Of GanttDemo.Examples.GanttTaskItem))
                _Tasks = value
            End Set
        End Property
    End Class

    Public Class GanttTaskItem

        Public Property StartDate As DateTime

        Public Property FinishDate As DateTime

        Public Property Id As Integer

        Public Property ParentId As Integer?

        Public Property Name As String

        Public Property Progress As Double

        Public Property Links As IEnumerable(Of GanttDemo.Examples.GanttTaskLink)
    End Class

    Public Class GanttTaskLink

        Private _PredecessorId As Integer

        Public Sub New(ByVal predecessorId As Integer)
            Me.PredecessorId = predecessorId
        End Sub

        Public Property PredecessorId As Integer
            Get
                Return _PredecessorId
            End Get

            Private Set(ByVal value As Integer)
                _PredecessorId = value
            End Set
        End Property
    End Class
End Namespace
