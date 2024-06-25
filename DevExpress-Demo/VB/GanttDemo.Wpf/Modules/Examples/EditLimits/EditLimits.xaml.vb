Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Gantt
Imports DevExpress.Utils
Imports DevExpress.Xpf.Gantt
Imports System
Imports System.Collections.Generic
Imports System.Windows.Controls
Imports System.Windows.Media

Namespace GanttDemo.Examples

    Public Partial Class EditLimits
        Inherits System.Windows.Controls.UserControl

        Public Sub New()
            Me.InitializeComponent()
        End Sub
    End Class

    <DevExpress.Mvvm.DataAnnotations.POCOViewModelAttribute>
    Public Class EditLimitsViewModel

        Private _Tasks As IEnumerable(Of GanttDemo.Examples.LimitedGanttTask)

        Public Sub New()
#Region "initialization"
            Dim startDate = System.DateTime.Today
            Me.Tasks = New System.Collections.Generic.List(Of GanttDemo.Examples.LimitedGanttTask) From {New GanttDemo.Examples.LimitedGanttTask With {.StartDate = startDate, .FinishDate = startDate.AddDays(1), .Name = "Task 1", .GreenRange = New DevExpress.Mvvm.DateTimeRange(startDate.AddDays(-2), startDate.AddDays(3)), .YellowRange = New DevExpress.Mvvm.DateTimeRange(startDate.AddDays(-4), startDate.AddDays(5)), .RedRange = New DevExpress.Mvvm.DateTimeRange(startDate.AddDays(-5), startDate.AddDays(6))}, New GanttDemo.Examples.LimitedGanttTask With {.StartDate = startDate.AddDays(1), .FinishDate = startDate.AddDays(2), .Name = "Task 2", .GreenRange = New DevExpress.Mvvm.DateTimeRange(startDate.AddDays(-1), startDate.AddDays(4)), .YellowRange = New DevExpress.Mvvm.DateTimeRange(startDate.AddDays(-3), startDate.AddDays(6)), .RedRange = New DevExpress.Mvvm.DateTimeRange(startDate.AddDays(-4), startDate.AddDays(7))}, New GanttDemo.Examples.LimitedGanttTask With {.StartDate = startDate.AddDays(2), .FinishDate = startDate.AddDays(3), .Name = "Task 3", .GreenRange = New DevExpress.Mvvm.DateTimeRange(startDate, startDate.AddDays(5)), .YellowRange = New DevExpress.Mvvm.DateTimeRange(startDate.AddDays(-2), startDate.AddDays(7)), .RedRange = New DevExpress.Mvvm.DateTimeRange(startDate.AddDays(-3), startDate.AddDays(8))}}
#End Region
        End Sub

        Public Property Tasks As IEnumerable(Of GanttDemo.Examples.LimitedGanttTask)
            Get
                Return _Tasks
            End Get

            Private Set(ByVal value As IEnumerable(Of GanttDemo.Examples.LimitedGanttTask))
                _Tasks = value
            End Set
        End Property

        Public Overridable Property Limits As IEnumerable(Of GanttDemo.Examples.TaskLimit)

        Public Overridable Sub UpdateLimits(ByVal selectedTask As GanttDemo.Examples.LimitedGanttTask)
            Me.Limits = If(selectedTask Is Nothing, Nothing, New System.Collections.Generic.List(Of GanttDemo.Examples.TaskLimit) From {New GanttDemo.Examples.TaskLimit(selectedTask.GreenRange.Start, System.Windows.Media.Colors.Lime), New GanttDemo.Examples.TaskLimit(selectedTask.GreenRange.[End], System.Windows.Media.Colors.Lime), New GanttDemo.Examples.TaskLimit(selectedTask.YellowRange.Start, System.Windows.Media.Colors.Yellow), New GanttDemo.Examples.TaskLimit(selectedTask.YellowRange.[End], System.Windows.Media.Colors.Yellow), New GanttDemo.Examples.TaskLimit(selectedTask.RedRange.Start, System.Windows.Media.Colors.Red), New GanttDemo.Examples.TaskLimit(selectedTask.RedRange.[End], System.Windows.Media.Colors.Red)})
        End Sub

        Public Function StartDateChanging(ByVal startDate As System.DateTime, ByVal view As DevExpress.Xpf.Gantt.GanttView, ByVal node As DevExpress.Xpf.Gantt.GanttNode) As DateTime
            Return CType(node.Content, GanttDemo.Examples.LimitedGanttTask).StartDateChanging(startDate, view, node.Duration)
        End Function

        Public Function StartDateChanged(ByVal startDate As System.DateTime, ByVal view As DevExpress.Xpf.Gantt.GanttView, ByVal node As DevExpress.Xpf.Gantt.GanttNode) As DateTime
            Me.UpdateLimits(Nothing)
            Return CType(node.Content, GanttDemo.Examples.LimitedGanttTask).StartDateChanged(startDate, view, node.Duration)
        End Function
    End Class

    Public Enum TaskState
        Red
        Yellow
        Green
    End Enum

    Public Class TaskLimit
        Inherits DevExpress.Utils.ImmutableObject

        Private ReadOnly limitField As System.DateTime

        Public ReadOnly Property Limit As DateTime
            Get
                Return Me.limitField
            End Get
        End Property

        Private ReadOnly colorField As System.Windows.Media.Color

        Public ReadOnly Property Color As Color
            Get
                Return Me.colorField
            End Get
        End Property

        Public Sub New(ByVal limit As System.DateTime, ByVal color As System.Windows.Media.Color)
            Me.limitField = limit
            Me.colorField = color
        End Sub
    End Class

#Region "!"
    Public Class LimitedGanttTask
        Inherits DevExpress.Mvvm.Gantt.GanttTask

        Private editStateField As GanttDemo.Examples.TaskState = GanttDemo.Examples.TaskState.Green

        Public Property EditState As TaskState
            Get
                Return Me.editStateField
            End Get

            Set(ByVal value As TaskState)
                If Me.editStateField <> value Then
                    Me.editStateField = value
                    Me.RaisePropertyChanged("EditState")
                End If
            End Set
        End Property

        Private stateField As GanttDemo.Examples.TaskState = GanttDemo.Examples.TaskState.Green

        Public Property State As TaskState
            Get
                Return Me.stateField
            End Get

            Set(ByVal value As TaskState)
                If Me.stateField <> value Then
                    Me.stateField = value
                    Me.RaisePropertyChanged("State")
                End If
            End Set
        End Property

        Public Property GreenRange As DateTimeRange

        Public Property YellowRange As DateTimeRange

        Public Property RedRange As DateTimeRange

        Private Shared Function Max(ByVal value1 As System.DateTime, ByVal value2 As System.DateTime) As DateTime
            Return If(value1 > value2, value1, value2)
        End Function

        Private Shared Function Min(ByVal value1 As System.DateTime, ByVal value2 As System.DateTime) As DateTime
            Return If(value1 < value2, value1, value2)
        End Function

        Private Function GetTaskState(ByVal range As DevExpress.Mvvm.DateTimeRange) As TaskState
            Dim rangeStates = New System.Collections.Generic.Dictionary(Of DevExpress.Mvvm.DateTimeRange, GanttDemo.Examples.TaskState)() From {{Me.GreenRange, GanttDemo.Examples.TaskState.Green}, {Me.YellowRange, GanttDemo.Examples.TaskState.Yellow}, {Me.RedRange, GanttDemo.Examples.TaskState.Red}}
            For Each rangeState In rangeStates
                If rangeState.Key.Contains(range) Then Return rangeState.Value
            Next

            Return GanttDemo.Examples.TaskState.Red
        End Function

        Private Function UpdateState(ByVal startDate As System.DateTime, ByVal view As DevExpress.Xpf.Gantt.GanttView, ByVal duration As System.TimeSpan, ByVal setState As System.Action(Of GanttDemo.Examples.TaskState)) As DateTime
            Dim finishDate = view.CalculateFinishDate(startDate, duration)
            Dim range = New DevExpress.Mvvm.DateTimeRange(startDate, finishDate)
            setState(Me.GetTaskState(range))
            Return If(Me.RedRange.Contains(range), startDate, GanttDemo.Examples.LimitedGanttTask.Min(view.CalculateStartDate(Me.RedRange.[End], duration), GanttDemo.Examples.LimitedGanttTask.Max(startDate, Me.RedRange.Start)))
        End Function

        Public Function StartDateChanging(ByVal startDate As System.DateTime, ByVal view As DevExpress.Xpf.Gantt.GanttView, ByVal duration As System.TimeSpan) As DateTime
            Return Me.UpdateState(startDate, view, duration, Sub(x) Me.EditState = x)
        End Function

        Public Function StartDateChanged(ByVal startDate As System.DateTime, ByVal view As DevExpress.Xpf.Gantt.GanttView, ByVal duration As System.TimeSpan) As DateTime
            Return Me.UpdateState(startDate, view, duration, Sub(x) Me.State = x)
        End Function
    End Class
#End Region
End Namespace
