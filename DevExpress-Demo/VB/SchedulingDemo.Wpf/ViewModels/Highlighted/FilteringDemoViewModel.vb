Imports DevExpress.Mvvm
Imports System.Collections.ObjectModel

Namespace SchedulingDemo.ViewModels

    Public Class FilteringDemoViewModel

        Protected Sub New()
            If ViewModelBase.IsInDesignMode Then Return
            For Each x In ReceptionDeskData.Doctors
                x.IsVisible = False
            Next

            For Each x In DepartmentPrimaryCare.Doctors
                x.IsVisible = True
            Next

            For Each x In DepartmentOphthalmology.Doctors
                x.IsVisible = True
            Next
        End Sub

        Public ReadOnly Property StartDate As Date
            Get
                Return BaseDate
            End Get
        End Property

        Public Overridable ReadOnly Property Doctors As ObservableCollection(Of Doctor)
            Get
                Return ReceptionDeskData.Doctors
            End Get
        End Property

        Public Overridable ReadOnly Property MedicalAppointments As ObservableCollection(Of MedicalAppointment)
            Get
                Return ReceptionDeskData.Appointments
            End Get
        End Property

        Public Overridable ReadOnly Property HospitalDepartments As ObservableCollection(Of HospitalDepartment)
            Get
                Return Departments
            End Get
        End Property

        Public Overridable ReadOnly Property Patients As ObservableCollection(Of Patient)
            Get
                Return ReceptionDeskData.Patients
            End Get
        End Property

        Public Overridable ReadOnly Property Locations As ObservableCollection(Of AppointmentLocation)
            Get
                Return AppointmentLocations
            End Get
        End Property

        Public Overridable ReadOnly Property PaymentStates As ObservableCollection(Of PaymentState)
            Get
                Return ReceptionDeskData.PaymentStates
            End Get
        End Property
    End Class
End Namespace
