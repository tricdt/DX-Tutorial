Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports DevExpress.DemoData
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Scheduling

Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFileAttribute("ModuleResources/DragDropCustomDataViewModel.(cs)")>
    Public Partial Class DragDropCustomData
        Inherits GridDemo.GridDemoModule

        Public Sub New()
            Me.InitializeComponent()
            AddHandler Unloaded, AddressOf Me.OnDragDropCustomDataUnloaded
        End Sub

        Private Sub OnDragDropCustomDataUnloaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.scheduler.DataSource = Nothing
        End Sub

        Private Sub OnStartRecordDrag(ByVal sender As Object, ByVal e As DevExpress.Xpf.Core.StartRecordDragEventArgs)
            e.Data.SetData(GetType(System.Collections.Generic.IEnumerable(Of GridDemo.SchedulerTask)), e.Records.Cast(Of GridDemo.SchedulerTask)())
            e.Handled = True
        End Sub

        Private Sub OnDragRecordOver(ByVal sender As Object, ByVal e As DevExpress.Xpf.Core.DragRecordOverEventArgs)
            If e.IsFromOutside AndAlso e.Data.GetDataPresent(GetType(System.Collections.Generic.IEnumerable(Of DevExpress.Xpf.Scheduling.AppointmentItemData))) Then
                e.Effects = System.Windows.DragDropEffects.Move
                e.Handled = True
            End If
        End Sub

        Private Sub OnDropRecord(ByVal sender As Object, ByVal e As DevExpress.Xpf.Core.DropRecordEventArgs)
            If e.Data.GetDataPresent(GetType(System.Collections.Generic.IEnumerable(Of DevExpress.Xpf.Scheduling.AppointmentItemData))) Then
                Dim appointments = CType(e.Data.GetData(GetType(System.Collections.Generic.IEnumerable(Of DevExpress.Xpf.Scheduling.AppointmentItemData))), System.Collections.Generic.IEnumerable(Of DevExpress.Xpf.Scheduling.AppointmentItemData))
                Dim dataObject = New System.Windows.DataObject()
                dataObject.SetData(New DevExpress.Xpf.Core.RecordDragDropData(appointments.[Select](Function(x) Me.CreateTask(x)).ToArray()))
                e.Data = dataObject
                e.Effects = System.Windows.DragDropEffects.Move
            End If
        End Sub

        Private Sub OnStartAppointmentDragFromOutside(ByVal sender As Object, ByVal e As DevExpress.Xpf.Scheduling.StartAppointmentDragFromOutsideEventArgs)
            If e.Data.GetDataPresent(GetType(System.Collections.Generic.IEnumerable(Of GridDemo.SchedulerTask))) Then System.Linq.Enumerable.ToList(Of GridDemo.SchedulerTask)(CType(e.Data.GetData(CType((GetType(System.Collections.Generic.IEnumerable(Of GridDemo.SchedulerTask))), System.Type)), System.Collections.Generic.IEnumerable(Of GridDemo.SchedulerTask))).ForEach(Sub(x) e.DragAppointments.Add(Me.CreateAppointment(x)))
        End Sub

        Private Sub OnResizeAppointmentOver(ByVal sender As Object, ByVal e As DevExpress.Xpf.Scheduling.ResizeAppointmentOverEventArgs)
            If e.ResizeAppointment.Duration.TotalHours < 1 Then
                If e.ResizeHandle = DevExpress.Xpf.Scheduling.ResizeHandle.[End] Then
                    e.ResizeAppointment.[End] += System.TimeSpan.FromHours(1) - e.ResizeAppointment.Duration
                Else
                    Dim [end] As System.DateTime = e.ResizeAppointment.[End]
                    e.ResizeAppointment.Start -= System.TimeSpan.FromHours(1) - e.ResizeAppointment.Duration
                    e.ResizeAppointment.[End] = [end]
                End If
            End If
        End Sub

        Private Function CreateAppointment(ByVal task As GridDemo.SchedulerTask) As AppointmentItem
            Return New DevExpress.Xpf.Scheduling.AppointmentItem With {.Subject = task.Subject, .LabelId = task.Priority, .Description = task.Description, .Start = New System.DateTime(), .[End] = New System.DateTime() + task.Duration, .AllDay = task.AllDay}
        End Function

        Private Function CreateTask(ByVal appointment As DevExpress.Xpf.Scheduling.AppointmentItemData) As SchedulerTask
            Return New GridDemo.SchedulerTask With {.Subject = appointment.Subject, .Priority = If(TypeOf appointment.LabelId Is DevExpress.DemoData.IssuePriority, CType(appointment.LabelId, DevExpress.DemoData.IssuePriority), DevExpress.DemoData.IssuePriority.Low), .Description = appointment.Description, .Duration = appointment.[End] - appointment.Start, .AllDay = appointment.AllDay}
        End Function
    End Class
End Namespace
