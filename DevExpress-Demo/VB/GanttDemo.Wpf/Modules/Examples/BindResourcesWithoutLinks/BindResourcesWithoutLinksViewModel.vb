Imports System
Imports System.Collections.Generic

Namespace GanttDemo.Examples

    Public Class BindResourcesWithoutLinksViewModel

        Private _Tasks As IEnumerable(Of GanttDemo.Examples.GanttDataItemWithResources), _Resources As IEnumerable(Of GanttDemo.Examples.GanttResourceItem)

        Public Sub New()
#Region "initialization"
            Dim startDate = System.DateTime.Now.[Date].AddDays(3)
            Me.Tasks = New System.Collections.Generic.List(Of GanttDemo.Examples.GanttDataItemWithResources) From {New GanttDemo.Examples.GanttDataItemWithResources With {.StartDate = startDate, .FinishDate = startDate + System.TimeSpan.FromDays(5), .Name = "Market Analysis", .ResourceIds = New System.Collections.Generic.List(Of Integer)() From {1}}, New GanttDemo.Examples.GanttDataItemWithResources With {.Name = "Feature Planning", .StartDate = startDate + System.TimeSpan.FromDays(5), .FinishDate = startDate + System.TimeSpan.FromDays(9), .ResourceIds = New System.Collections.Generic.List(Of Integer)() From {1, 2}}, New GanttDemo.Examples.GanttDataItemWithResources With {.Name = "Feature 1", .StartDate = startDate + System.TimeSpan.FromDays(9), .FinishDate = startDate + System.TimeSpan.FromDays(16), .Children = {New GanttDemo.Examples.GanttDataItemWithResources With {.Name = "Implementation", .StartDate = startDate + System.TimeSpan.FromDays(9), .FinishDate = startDate + System.TimeSpan.FromDays(13), .ResourceIds = New System.Collections.Generic.List(Of Integer)() From {2}}, New GanttDemo.Examples.GanttDataItemWithResources With {.Name = "Demos & Docs", .StartDate = startDate + System.TimeSpan.FromDays(13), .FinishDate = startDate + System.TimeSpan.FromDays(16), .ResourceIds = New System.Collections.Generic.List(Of Integer)() From {4, 2}}}}, New GanttDemo.Examples.GanttDataItemWithResources With {.Name = "Feature 2", .StartDate = startDate + System.TimeSpan.FromDays(9), .FinishDate = startDate + System.TimeSpan.FromDays(17), .Children = {New GanttDemo.Examples.GanttDataItemWithResources With {.Name = "Implementation", .StartDate = startDate + System.TimeSpan.FromDays(9), .FinishDate = startDate + System.TimeSpan.FromDays(13), .ResourceIds = New System.Collections.Generic.List(Of Integer)() From {2}}, New GanttDemo.Examples.GanttDataItemWithResources With {.Name = "Demos & Docs", .StartDate = startDate + System.TimeSpan.FromDays(13), .FinishDate = startDate + System.TimeSpan.FromDays(17), .ResourceIds = New System.Collections.Generic.List(Of Integer)() From {4, 2}}}}, New GanttDemo.Examples.GanttDataItemWithResources With {.Name = "Feature 3", .StartDate = startDate + System.TimeSpan.FromDays(16), .FinishDate = startDate + System.TimeSpan.FromDays(19), .Children = {New GanttDemo.Examples.GanttDataItemWithResources With {.Name = "Implementation", .StartDate = startDate + System.TimeSpan.FromDays(16), .FinishDate = startDate + System.TimeSpan.FromDays(18), .ResourceIds = New System.Collections.Generic.List(Of Integer)() From {2}}, New GanttDemo.Examples.GanttDataItemWithResources With {.Id = 10, .Name = "Demos & Docs", .StartDate = startDate + System.TimeSpan.FromDays(18), .FinishDate = startDate + System.TimeSpan.FromDays(19), .ResourceIds = New System.Collections.Generic.List(Of Integer)() From {4, 2}}}}, New GanttDemo.Examples.GanttDataItemWithResources With {.Name = "Testing & Bug Fixing", .StartDate = startDate + System.TimeSpan.FromDays(19), .FinishDate = startDate + System.TimeSpan.FromDays(23), .ResourceIds = New System.Collections.Generic.List(Of Integer)() From {3, 2}}, New GanttDemo.Examples.GanttDataItemWithResources With {.Name = "Development finished", .StartDate = startDate + System.TimeSpan.FromDays(23), .FinishDate = startDate + System.TimeSpan.FromDays(23)}}
            Me.Resources = New System.Collections.Generic.List(Of GanttDemo.Examples.GanttResourceItem) From {New GanttDemo.Examples.GanttResourceItem With {.Name = "Management", .Id = 1}, New GanttDemo.Examples.GanttResourceItem With {.Name = "Developers", .Id = 2}, New GanttDemo.Examples.GanttResourceItem With {.Name = "Testers", .Id = 3}, New GanttDemo.Examples.GanttResourceItem With {.Name = "Technical Writers", .Id = 4}}
#End Region
        End Sub

        Public Property Tasks As IEnumerable(Of GanttDemo.Examples.GanttDataItemWithResources)
            Get
                Return _Tasks
            End Get

            Private Set(ByVal value As IEnumerable(Of GanttDemo.Examples.GanttDataItemWithResources))
                _Tasks = value
            End Set
        End Property

        Public Property Resources As IEnumerable(Of GanttDemo.Examples.GanttResourceItem)
            Get
                Return _Resources
            End Get

            Private Set(ByVal value As IEnumerable(Of GanttDemo.Examples.GanttResourceItem))
                _Resources = value
            End Set
        End Property
    End Class

    Public Class GanttDataItemWithResources
        Inherits GanttDemo.Examples.GanttDataItem

        Public Property ResourceIds As List(Of Integer)
    End Class

    Public Class GanttResourceItem

        Public Property Name As String

        Public Property Id As Integer
    End Class
End Namespace
