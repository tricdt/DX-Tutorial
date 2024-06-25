Imports System
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataAnnotations

Namespace DevExpress.DevAV.ViewModels

    Partial Class EmployeeTaskViewModel

        Protected Overrides Function CreateEntity() As EmployeeTask
            Dim entity As EmployeeTask = MyBase.CreateEntity()
            entity.StartDate = Date.Now
            entity.DueDate = Date.Now + New TimeSpan(48, 0, 0)
            Return entity
        End Function

        Protected Overrides Sub OnEntityChanged()
            MyBase.OnEntityChanged()
            RaisePropertyChanged(Function(vm) vm.ReminderTime)
            If Entity IsNot Nothing Then Log(String.Format("HybridApp: Edit Task: {0}", If(String.IsNullOrEmpty(Entity.Subject), "<New>", Entity.Subject)))
        End Sub

        Public Property ReminderTime As Date?
            Get
                If Entity Is Nothing OrElse Entity.ReminderDateTime Is Nothing Then Return Nothing
                Return Entity.ReminderDateTime.Value
            End Get

            Set(ByVal value As Date?)
                If Entity Is Nothing OrElse value Is Nothing OrElse Entity.ReminderDateTime Is Nothing Then Return
                Dim reminderDateTime As Date = CDate(Entity.ReminderDateTime)
                Entity.ReminderDateTime = New DateTime(reminderDateTime.Year, reminderDateTime.Month, reminderDateTime.Day, (CDate(value)).Hour, (CDate(value)).Minute, reminderDateTime.Second)
            End Set
        End Property

        Protected Overrides Function GetTitle() As String
            Return If(Entity.Owner IsNot Nothing, Entity.Owner.FullName, String.Empty)
        End Function
    End Class

    Public Class EmployeeTaskStatusWithExternalMetadata

        Public Shared Sub BuildMetadata(ByVal builder As EnumMetadataBuilder(Of EmployeeTaskStatus))
            builder.Member(EmployeeTaskStatus.NotStarted).ImageUri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Status/NotStarted.svg").EndMember().Member(EmployeeTaskStatus.NeedAssistance).ImageUri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Status/NeedAssistance.svg").EndMember().Member(EmployeeTaskStatus.InProgress).ImageUri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Status/InProgress.svg").EndMember().Member(EmployeeTaskStatus.Deferred).ImageUri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Status/Deferred.svg").EndMember().Member(EmployeeTaskStatus.Completed).ImageUri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Status/Completed.svg").EndMember()
        End Sub
    End Class

    Public Class EmployeeTaskPriorityWithExternalMetadata

        Public Shared Sub BuildMetadata(ByVal builder As EnumMetadataBuilder(Of EmployeeTaskPriority))
            builder.Member(EmployeeTaskPriority.High).ImageUri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Priority/MediumPriority.svg").EndMember().Member(EmployeeTaskPriority.Low).ImageUri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Priority/LowPriority.svg").EndMember().Member(EmployeeTaskPriority.Normal).ImageUri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Priority/NormalPriority.svg").EndMember().Member(EmployeeTaskPriority.Urgent).ImageUri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Priority/HighPriority.svg").EndMember()
        End Sub
    End Class
End Namespace
