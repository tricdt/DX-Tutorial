Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel

Namespace ProductsDemo.Modules

    Public Class Appointment
        Implements INotifyPropertyChanged

        Private eventTypeField As Integer?

        Public Property EventType As Integer?
            Get
                Return eventTypeField
            End Get

            Set(ByVal value As Integer?)
                If eventTypeField = value Then Return
                eventTypeField = value
                RaisePropertyChanged("EventType")
            End Set
        End Property

        Private startDateField As Date?

        Public Property StartDate As Date?
            Get
                Return startDateField
            End Get

            Set(ByVal value As Date?)
                If startDateField = value Then Return
                startDateField = value
                RaisePropertyChanged("StartDate")
            End Set
        End Property

        Private endDateField As Date?

        Public Property EndDate As Date?
            Get
                Return endDateField
            End Get

            Set(ByVal value As Date?)
                If endDateField = value Then Return
                endDateField = value
                RaisePropertyChanged("EndDate")
            End Set
        End Property

        Private allDayField As Boolean?

        Public Property AllDay As Boolean?
            Get
                Return allDayField
            End Get

            Set(ByVal value As Boolean?)
                If allDayField = value Then Return
                allDayField = value
                RaisePropertyChanged("AllDay")
            End Set
        End Property

        Private subjectField As String

        Public Property Subject As String
            Get
                Return subjectField
            End Get

            Set(ByVal value As String)
                If Equals(subjectField, value) Then Return
                subjectField = value
                RaisePropertyChanged("Subject")
            End Set
        End Property

        Private locationField As String

        Public Property Location As String
            Get
                Return locationField
            End Get

            Set(ByVal value As String)
                If Equals(locationField, value) Then Return
                locationField = value
                RaisePropertyChanged("Location")
            End Set
        End Property

        Private descriptionField As String

        Public Property Description As String
            Get
                Return descriptionField
            End Get

            Set(ByVal value As String)
                If Equals(descriptionField, value) Then Return
                descriptionField = value
                RaisePropertyChanged("Description")
            End Set
        End Property

        Private statusField As Integer?

        Public Property Status As Integer?
            Get
                Return statusField
            End Get

            Set(ByVal value As Integer?)
                If statusField = value Then Return
                statusField = value
                RaisePropertyChanged("Status")
            End Set
        End Property

        Private labelField As Integer?

        Public Property Label As Integer?
            Get
                Return labelField
            End Get

            Set(ByVal value As Integer?)
                If labelField = value Then Return
                labelField = value
                RaisePropertyChanged("Label")
            End Set
        End Property

        Private recurrenceInfoField As String

        Public Property RecurrenceInfo As String
            Get
                Return recurrenceInfoField
            End Get

            Set(ByVal value As String)
                If Equals(recurrenceInfoField, value) Then Return
                recurrenceInfoField = value
                RaisePropertyChanged("RecurrenceInfo")
            End Set
        End Property

        Private reminderInfoField As String

        Public Property ReminderInfo As String
            Get
                Return reminderInfoField
            End Get

            Set(ByVal value As String)
                If Equals(reminderInfoField, value) Then Return
                reminderInfoField = value
                RaisePropertyChanged("ReminderInfo")
            End Set
        End Property

        Private contactInfoField As String

        Public Property ContactInfo As String
            Get
                Return contactInfoField
            End Get

            Set(ByVal value As String)
                If Equals(contactInfoField, value) Then Return
                contactInfoField = value
                RaisePropertyChanged("ContactInfo")
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private Sub RaisePropertyChanged(ByVal propertyName As String)
            If TypeDescriptor.GetProperties(Me)(propertyName) Is Nothing Then Throw New ArgumentException(propertyName & " doesn't exist in " & [GetType]().Name & " type")
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class

    Public Class CalendarModel

        Public ReadOnly Property Appointments As ObservableCollection(Of Appointment)
            Get
                Return DataBaseHelper.Instance.Appointments
            End Get
        End Property

        Private Shared modelField As CalendarModel = Nothing

        Public Shared ReadOnly Property Model As CalendarModel
            Get
                If modelField Is Nothing Then modelField = New CalendarModel()
                Return modelField
            End Get
        End Property
    End Class
End Namespace
