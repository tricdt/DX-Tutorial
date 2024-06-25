Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Scheduling
Imports System.Collections.ObjectModel
Imports System.Linq

Namespace SchedulingDemo

    Public Class MedicalAppointmentWindowViewModel
        Inherits AppointmentWindowViewModel

        Public Shared Function Create(ByVal appointmentItem As AppointmentItem, ByVal scheduler As SchedulerControl) As MedicalAppointmentWindowViewModel
            Return ViewModelSource.Create(Function() New MedicalAppointmentWindowViewModel(appointmentItem, scheduler))
        End Function

        Protected Sub New(ByVal appointmentItem As AppointmentItem, ByVal scheduler As SchedulerControl)
            MyBase.New(appointmentItem, scheduler)
            patientField = Patients.FirstOrDefault(Function(x) x.Id.Equals(CustomFields("PatientId")))
        End Sub

        Public ReadOnly Property Patients As ObservableCollection(Of Patient)
            Get
                Return ReceptionDeskData.Patients
            End Get
        End Property

        Public ReadOnly Property Locations As ObservableCollection(Of AppointmentLocation)
            Get
                Return AppointmentLocations
            End Get
        End Property

        Private patientField As Patient

        <BindableProperty>
        Public Overridable Property Patient As Patient
            Get
                Return patientField
            End Get

            Set(ByVal value As Patient)
                Dim newPatient As Patient = value
                If patientField Is newPatient Then Return
                patientField = newPatient
                CustomFields("PatientId") = newPatient.Id
                Subject = newPatient.Name
            End Set
        End Property
    End Class
End Namespace
