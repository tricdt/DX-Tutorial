Imports System.Windows.Controls

Namespace EditorsDemo.FilterBehavior

    Public Partial Class FilterBehaviorScheduler
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
            scheduler.DataSource.AppointmentsSource = AppointmentsGenerator.CreateAppointments()
        End Sub
    End Class
End Namespace
