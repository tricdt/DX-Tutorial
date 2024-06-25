Imports DevExpress.Mvvm.Gantt
Imports System
Imports System.Collections.Generic

Namespace GanttDemo.Examples

    Public Class CustomSnappingViewModel

        Private _Tasks As IEnumerable(Of DevExpress.Mvvm.Gantt.GanttTask)

        Public Sub New()
#Region "initialization"
            Dim startDate = System.DateTime.Today
            Me.Tasks = New System.Collections.Generic.List(Of DevExpress.Mvvm.Gantt.GanttTask) From {New DevExpress.Mvvm.Gantt.GanttTask With {.StartDate = startDate, .FinishDate = startDate.AddHours(CDbl((5))).AddMinutes(45), .Name = "Night Light"}, New DevExpress.Mvvm.Gantt.GanttTask With {.StartDate = startDate.AddHours(CDbl((6))).AddMinutes(45), .FinishDate = startDate.AddHours(7), .Name = "Teapot"}, New DevExpress.Mvvm.Gantt.GanttTask With {.StartDate = startDate.AddHours(8), .FinishDate = startDate.AddHours(12), .Name = "PC"}, New DevExpress.Mvvm.Gantt.GanttTask With {.StartDate = startDate.AddHours(13), .FinishDate = startDate.AddHours(17), .Name = "Notebook"}, New DevExpress.Mvvm.Gantt.GanttTask With {.StartDate = startDate.AddHours(18), .FinishDate = startDate.AddHours(22), .Name = "TV"}, New DevExpress.Mvvm.Gantt.GanttTask With {.StartDate = startDate.AddHours(19), .FinishDate = startDate.AddDays(1), .Name = "Light"}}
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

        Public Shared ReadOnly Property SnapUnit As TimeSpan
            Get
                Return System.TimeSpan.FromMinutes(15)
            End Get
        End Property
    End Class
End Namespace
