Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Utils.Text
Imports DevExpress.Xpf.Scheduling
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq

Namespace SchedulingDemo

    Public Class Patient

        Public Shared Function Create() As Patient
            Return DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New SchedulingDemo.Patient())
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property Id As Integer

        Public Overridable Property Name As String

        Public Overridable Property BirthDate As DateTime

        Public Overridable Property Phone As String
    End Class

    Public Class HospitalDepartment

        Private _Doctors As ObservableCollection(Of SchedulingDemo.Doctor)

        Public Shared Function Create(ByVal id As Integer, ByVal name As String) As HospitalDepartment
            Return DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New SchedulingDemo.HospitalDepartment() With {.Id = id, .Name = name})
        End Function

        Protected Sub New()
            Me.Doctors = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.Doctor)()
        End Sub

        Public Overridable Property Id As Integer

        Public Overridable Property Name As String

        Public Property Doctors As ObservableCollection(Of SchedulingDemo.Doctor)
            Get
                Return _Doctors
            End Get

            Private Set(ByVal value As ObservableCollection(Of SchedulingDemo.Doctor))
                _Doctors = value
            End Set
        End Property
    End Class

    Public Class Doctor

        Public Shared Function Create() As Doctor
            Return DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New SchedulingDemo.Doctor())
        End Function

        Public Shared Function Create(ByVal employee As DevExpress.DemoData.Models.Employee) As Doctor
            Return DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New SchedulingDemo.Doctor() With {.Id = CInt(employee.EmployeeID), .Name = employee.FullName, .Phone = employee.HomePhone, .Photo = employee.Photo})
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property Id As Integer

        Public Overridable Property Name As String

        Public Overridable Property Phone As String

        Public Overridable Property Photo As Object

        Public Overridable Property Department As HospitalDepartment

        Public Overridable Property IsVisible As Boolean
    End Class

    Public Class AppointmentLocation

        Public Shared Function Create(ByVal id As Integer, ByVal caption As String) As AppointmentLocation
            Return DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New SchedulingDemo.AppointmentLocation() With {.Id = id, .Caption = caption})
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property Id As Integer

        Public Overridable Property Caption As String
    End Class

    Public Class PaymentState

        Public Shared Function Create(ByVal id As Integer, ByVal caption As String, ByVal brushName As String) As PaymentState
            Return DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New SchedulingDemo.PaymentState() With {.Id = id, .Caption = caption, .BrushName = brushName})
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property Id As Integer

        Public Overridable Property Caption As String

        Public Overridable Property BrushName As String
    End Class

    Public Class MedicalAppointment

        Public Shared Function Create() As MedicalAppointment
            Return DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New SchedulingDemo.MedicalAppointment())
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property Id As Integer

        Public Overridable Property Type As Integer

        Public Overridable Property AllDay As Boolean

        Public Overridable Property StartTime As DateTime

        Public Overridable Property EndTime As DateTime

        Public Overridable Property PatientId As Integer?

        Public Overridable Property DoctorId As Integer?

        Public Overridable Property LocationId As Integer

        Public Overridable Property PaymentStateId As Integer

        Public Overridable Property Subject As String

        Public Overridable Property Purpose As String

        Public Overridable Property Note As String

        Public Overridable Property RecurrenceInfo As String

        Public Overridable Property ReminderInfo As String

        Public Overridable Property TimeZoneId As String
    End Class

    Public Module ReceptionDeskData

        Private _PaymentStatePaid As PaymentState, _PaymentStateOverdue As PaymentState, _PaymentStateNotYetBilled As PaymentState

        Public ReadOnly BaseDate As System.DateTime = System.DateTime.Today

        Public Property Patients As ObservableCollection(Of SchedulingDemo.Patient)

        Public Property Departments As ObservableCollection(Of SchedulingDemo.HospitalDepartment)

        Public Property Doctors As ObservableCollection(Of SchedulingDemo.Doctor)

        Public Property AppointmentLocations As ObservableCollection(Of SchedulingDemo.AppointmentLocation)

        Public Property PaymentStates As ObservableCollection(Of SchedulingDemo.PaymentState)

        Public Property Appointments As ObservableCollection(Of SchedulingDemo.MedicalAppointment)

        Public Property DepartmentPrimaryCare As HospitalDepartment

        Public Property DepartmentOphthalmology As HospitalDepartment

        Public Property DepartmentDermatology As HospitalDepartment

        Public Property DepartmentCardiology As HospitalDepartment

        Private Property AppointmentLocationClinic As AppointmentLocation

        Private Property AppointmentLocationHospital As AppointmentLocation

        Private Property AppointmentLocationHospice As AppointmentLocation

        Private Property AppointmentLocationHouseCall As AppointmentLocation

        Friend Property PaymentStatePaid As PaymentState
            Get
                Return _PaymentStatePaid
            End Get

            Private Set(ByVal value As PaymentState)
                _PaymentStatePaid = value
            End Set
        End Property

        Friend Property PaymentStateOverdue As PaymentState
            Get
                Return _PaymentStateOverdue
            End Get

            Private Set(ByVal value As PaymentState)
                _PaymentStateOverdue = value
            End Set
        End Property

        Friend Property PaymentStateNotYetBilled As PaymentState
            Get
                Return _PaymentStateNotYetBilled
            End Get

            Private Set(ByVal value As PaymentState)
                _PaymentStateNotYetBilled = value
            End Set
        End Property

        Private Property DepartmentPrimaryCare_DurationAndPurposes As Dictionary(Of SchedulingDemo.AppointmentLocation, System.Tuple(Of System.TimeSpan, String)())

        Private Property DepartmentOphthalmology_DurationAndPurposes As Dictionary(Of SchedulingDemo.AppointmentLocation, System.Tuple(Of System.TimeSpan, String)())

        Private Property DepartmentDermatology_DurationAndPurposes As Dictionary(Of SchedulingDemo.AppointmentLocation, System.Tuple(Of System.TimeSpan, String)())

        Private Property DepartmentCardiology_DurationAndPurposes As Dictionary(Of SchedulingDemo.AppointmentLocation, System.Tuple(Of System.TimeSpan, String)())

        Private ReadOnly random As System.Random

        Sub New()
            SchedulingDemo.ReceptionDeskData.random = New System.Random()
            If DevExpress.Mvvm.ViewModelBase.IsInDesignMode Then
                SchedulingDemo.ReceptionDeskData.Patients = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.Patient)()
                SchedulingDemo.ReceptionDeskData.Departments = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.HospitalDepartment)()
                SchedulingDemo.ReceptionDeskData.Doctors = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.Doctor)()
                SchedulingDemo.ReceptionDeskData.AppointmentLocations = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.AppointmentLocation)()
                SchedulingDemo.ReceptionDeskData.PaymentStates = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.PaymentState)()
                SchedulingDemo.ReceptionDeskData.Appointments = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.MedicalAppointment)()
                Return
            End If

            Call SchedulingDemo.ReceptionDeskData.CreatePatients()
            Call SchedulingDemo.ReceptionDeskData.CreateDepartments()
            Call SchedulingDemo.ReceptionDeskData.CreateDoctors()
            Call SchedulingDemo.ReceptionDeskData.CreateAppointmentLocations()
            Call SchedulingDemo.ReceptionDeskData.CreatePaymentStates()
            Call SchedulingDemo.ReceptionDeskData.CreateDurationAndPurposes()
            Call SchedulingDemo.ReceptionDeskData.CreateAppointments()
        End Sub

        Private Sub CreatePatients()
            Dim PatientNames As String() = {"Andrew Glover", "Mark Oliver", "Taylor Riley", "Addison Davis", "Benjamin Hughes", "Lucas Smith", "Robert King", "Laura Callahan", "Miguel Simmons", "Isabella Carter", "Andrew Fuller", "Madeleine Russell", "Steven Buchanan", "Nancy Davolio", "Michael Suyama", "Margaret Peacock", "Janet Leverling", "Ariana Alexander", "Brad Farkus", "Bart Arnaz", "Arnie Schwartz", "Billy Zimmer", "Samantha Piper", "Maggie Boxter", "Terry Bradley", "Greta Sims", "Cindy Stanwick", "Marcus Orbison", "Sandy Bright", "Ken Samuelson", "Brett Wade", "Wally Hobbs", "Brad Jameson", "Karen Goodson", "Morgan Kennedy", "Violet Bailey", "John Heart", "Arthur Miller", "Robert Reagan", "Ed Holmes", "Sammy Hill", "Olivia Peyton", "Jim Packard", "Hannah Brookly", "Harv Mudd", "Todd Hoffman", "Kevin Carter", "Mary Stern", "Robin Cosworth", "Jenny Hobbs", "Dallas Lou"}
            SchedulingDemo.ReceptionDeskData.Patients = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.Patient)()
            Dim patientCount As Integer = PatientNames.Length
            Dim patientId As Integer = 1
            Dim birthday As System.DateTime = New System.DateTime(1975, 2, 5)
            For i As Integer = 0 To patientCount - 1
                Dim patient As SchedulingDemo.Patient = SchedulingDemo.Patient.Create()
                patient.Id = System.Math.Min(System.Threading.Interlocked.Increment(patientId), patientId - 1)
                patient.Name = PatientNames(i)
                patient.BirthDate = birthday.AddMonths(CInt((SchedulingDemo.ReceptionDeskData.random.[Next](CInt((1)), CInt((12)))))).AddYears(SchedulingDemo.ReceptionDeskData.random.[Next](0, 20))
                patient.Phone = "(" & SchedulingDemo.ReceptionDeskData.random.[Next](100, 999) & ") " & SchedulingDemo.ReceptionDeskData.random.[Next](100, 999) & "-" & SchedulingDemo.ReceptionDeskData.random.[Next](1000, 9999)
                Call SchedulingDemo.ReceptionDeskData.Patients.Add(patient)
            Next
        End Sub

        Private Sub CreateDepartments()
            SchedulingDemo.ReceptionDeskData.Departments = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.HospitalDepartment)()
            SchedulingDemo.ReceptionDeskData.DepartmentPrimaryCare = SchedulingDemo.HospitalDepartment.Create(0, "Primary Care")
            SchedulingDemo.ReceptionDeskData.DepartmentOphthalmology = SchedulingDemo.HospitalDepartment.Create(1, "Ophthalmology")
            SchedulingDemo.ReceptionDeskData.DepartmentDermatology = SchedulingDemo.HospitalDepartment.Create(2, "Dermatology")
            SchedulingDemo.ReceptionDeskData.DepartmentCardiology = SchedulingDemo.HospitalDepartment.Create(3, "Cardiology")
            Call SchedulingDemo.ReceptionDeskData.Departments.Add(SchedulingDemo.ReceptionDeskData.DepartmentPrimaryCare)
            Call SchedulingDemo.ReceptionDeskData.Departments.Add(SchedulingDemo.ReceptionDeskData.DepartmentOphthalmology)
            Call SchedulingDemo.ReceptionDeskData.Departments.Add(SchedulingDemo.ReceptionDeskData.DepartmentDermatology)
            Call SchedulingDemo.ReceptionDeskData.Departments.Add(SchedulingDemo.ReceptionDeskData.DepartmentCardiology)
        End Sub

        Private Sub CreateDoctors()
            Dim d0 = SchedulingDemo.Doctor.Create(SchedulingDemo.EmployeesDataHelper.Employees(0))
            Dim d1 = SchedulingDemo.Doctor.Create(SchedulingDemo.EmployeesDataHelper.Employees(1))
            Dim d2 = SchedulingDemo.Doctor.Create(SchedulingDemo.EmployeesDataHelper.Employees(2))
            Dim d3 = SchedulingDemo.Doctor.Create(SchedulingDemo.EmployeesDataHelper.Employees(3))
            Dim d4 = SchedulingDemo.Doctor.Create(SchedulingDemo.EmployeesDataHelper.Employees(4))
            Dim d5 = SchedulingDemo.Doctor.Create(SchedulingDemo.EmployeesDataHelper.Employees(5))
            Dim d6 = SchedulingDemo.Doctor.Create(SchedulingDemo.EmployeesDataHelper.Employees(6))
            Dim d7 = SchedulingDemo.Doctor.Create(SchedulingDemo.EmployeesDataHelper.Employees(7))
            SchedulingDemo.ReceptionDeskData.Doctors = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.Doctor)() From {d0, d1, d2, d3, d4, d5, d6, d7}
            Call SchedulingDemo.ReceptionDeskData.DepartmentPrimaryCare.Doctors.Add(d0)
            Call SchedulingDemo.ReceptionDeskData.DepartmentPrimaryCare.Doctors.Add(d1)
            Call SchedulingDemo.ReceptionDeskData.DepartmentPrimaryCare.Doctors.Add(d2)
            Call SchedulingDemo.ReceptionDeskData.DepartmentOphthalmology.Doctors.Add(d3)
            Call SchedulingDemo.ReceptionDeskData.DepartmentOphthalmology.Doctors.Add(d4)
            Call SchedulingDemo.ReceptionDeskData.DepartmentDermatology.Doctors.Add(d5)
            Call SchedulingDemo.ReceptionDeskData.DepartmentDermatology.Doctors.Add(d6)
            Call SchedulingDemo.ReceptionDeskData.DepartmentCardiology.Doctors.Add(d6)
            d0.Department = SchedulingDemo.ReceptionDeskData.DepartmentPrimaryCare
            d1.Department = SchedulingDemo.ReceptionDeskData.DepartmentPrimaryCare
            d2.Department = SchedulingDemo.ReceptionDeskData.DepartmentPrimaryCare
            d3.Department = SchedulingDemo.ReceptionDeskData.DepartmentOphthalmology
            d4.Department = SchedulingDemo.ReceptionDeskData.DepartmentOphthalmology
            d5.Department = SchedulingDemo.ReceptionDeskData.DepartmentDermatology
            d6.Department = SchedulingDemo.ReceptionDeskData.DepartmentDermatology
            d7.Department = SchedulingDemo.ReceptionDeskData.DepartmentCardiology
        End Sub

        Private Sub CreateAppointmentLocations()
            SchedulingDemo.ReceptionDeskData.AppointmentLocations = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.AppointmentLocation)()
            SchedulingDemo.ReceptionDeskData.AppointmentLocationClinic = SchedulingDemo.AppointmentLocation.Create(0, "Clinic")
            SchedulingDemo.ReceptionDeskData.AppointmentLocationHospital = SchedulingDemo.AppointmentLocation.Create(1, "Hospital")
            SchedulingDemo.ReceptionDeskData.AppointmentLocationHospice = SchedulingDemo.AppointmentLocation.Create(2, "Hospice")
            SchedulingDemo.ReceptionDeskData.AppointmentLocationHouseCall = SchedulingDemo.AppointmentLocation.Create(3, "House Call")
            Call SchedulingDemo.ReceptionDeskData.AppointmentLocations.Add(SchedulingDemo.ReceptionDeskData.AppointmentLocationClinic)
            Call SchedulingDemo.ReceptionDeskData.AppointmentLocations.Add(SchedulingDemo.ReceptionDeskData.AppointmentLocationHospital)
            Call SchedulingDemo.ReceptionDeskData.AppointmentLocations.Add(SchedulingDemo.ReceptionDeskData.AppointmentLocationHospice)
            Call SchedulingDemo.ReceptionDeskData.AppointmentLocations.Add(SchedulingDemo.ReceptionDeskData.AppointmentLocationHouseCall)
        End Sub

        Private Sub CreatePaymentStates()
            SchedulingDemo.ReceptionDeskData.PaymentStates = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.PaymentState)()
            SchedulingDemo.ReceptionDeskData.PaymentStatePaid = SchedulingDemo.PaymentState.Create(0, "Paid", DevExpress.Xpf.Scheduling.DefaultBrushNames.StatusBusy)
            SchedulingDemo.ReceptionDeskData.PaymentStateOverdue = SchedulingDemo.PaymentState.Create(1, "Overdue", DevExpress.Xpf.Scheduling.DefaultBrushNames.StatusOutOfOffice)
            SchedulingDemo.ReceptionDeskData.PaymentStateNotYetBilled = SchedulingDemo.PaymentState.Create(2, "Not Yet Billed", DevExpress.Xpf.Scheduling.DefaultBrushNames.StatusTentative)
            Call SchedulingDemo.ReceptionDeskData.PaymentStates.Add(SchedulingDemo.ReceptionDeskData.PaymentStatePaid)
            Call SchedulingDemo.ReceptionDeskData.PaymentStates.Add(SchedulingDemo.ReceptionDeskData.PaymentStateOverdue)
            Call SchedulingDemo.ReceptionDeskData.PaymentStates.Add(SchedulingDemo.ReceptionDeskData.PaymentStateNotYetBilled)
        End Sub

        Private Sub CreateDurationAndPurposes()
            Dim phoneConsultation = System.Tuple.Create(System.TimeSpan.FromMinutes(20), "Phone Consultation")
            Dim caseReview = System.Tuple.Create(System.TimeSpan.FromMinutes(20), "Case Review")
            Dim consultation = System.Tuple.Create(System.TimeSpan.FromMinutes(40), "Consultation")
            Dim vaccination = System.Tuple.Create(System.TimeSpan.FromMinutes(20), "Vaccination")
            Dim reviewOfTestResults = System.Tuple.Create(System.TimeSpan.FromMinutes(30), "Review of Test Results")
            Dim annualCheckUp = System.Tuple.Create(System.TimeSpan.FromMinutes(30), "Annual Check-Up")
            Dim surgicalReferral = System.Tuple.Create(System.TimeSpan.FromMinutes(60), "Surgical Referral")
            Dim eyeExam = System.Tuple.Create(System.TimeSpan.FromMinutes(20), "Eye Exam")
            Dim glaucomaSurgery = System.Tuple.Create(System.TimeSpan.FromMinutes(60), "Glaucoma Surgery")
            Dim cataractSurgery = System.Tuple.Create(System.TimeSpan.FromMinutes(60), "Cataract Surgery")
            Dim biopsy = System.Tuple.Create(System.TimeSpan.FromMinutes(40), "Biopsy")
            Dim cancerScreening = System.Tuple.Create(System.TimeSpan.FromMinutes(60), "Cancer Screening")
            Dim bypassSurgery = System.Tuple.Create(System.TimeSpan.FromMinutes(60), "Bypass Surgery")
            Dim electrocardiogramTest = System.Tuple.Create(System.TimeSpan.FromMinutes(30), "Electrocardiogram Test")
            Dim pacemakerImplantation = System.Tuple.Create(System.TimeSpan.FromMinutes(60), "Pacemaker Implantation")
            SchedulingDemo.ReceptionDeskData.DepartmentPrimaryCare_DurationAndPurposes = New System.Collections.Generic.Dictionary(Of SchedulingDemo.AppointmentLocation, System.Tuple(Of System.TimeSpan, String)())() From {{SchedulingDemo.ReceptionDeskData.AppointmentLocationClinic, {vaccination, reviewOfTestResults, consultation, phoneConsultation, annualCheckUp, surgicalReferral}}, {SchedulingDemo.ReceptionDeskData.AppointmentLocationHospital, New System.Tuple(Of System.TimeSpan, String)() {}}, {SchedulingDemo.ReceptionDeskData.AppointmentLocationHospice, {vaccination, annualCheckUp, caseReview, consultation}}, {SchedulingDemo.ReceptionDeskData.AppointmentLocationHouseCall, {annualCheckUp, caseReview, consultation}}}
            SchedulingDemo.ReceptionDeskData.DepartmentOphthalmology_DurationAndPurposes = New System.Collections.Generic.Dictionary(Of SchedulingDemo.AppointmentLocation, System.Tuple(Of System.TimeSpan, String)())() From {{SchedulingDemo.ReceptionDeskData.AppointmentLocationClinic, {eyeExam, consultation, phoneConsultation}}, {SchedulingDemo.ReceptionDeskData.AppointmentLocationHospital, {glaucomaSurgery, cataractSurgery}}, {SchedulingDemo.ReceptionDeskData.AppointmentLocationHospice, {caseReview, consultation}}, {SchedulingDemo.ReceptionDeskData.AppointmentLocationHouseCall, {caseReview, consultation}}}
            SchedulingDemo.ReceptionDeskData.DepartmentDermatology_DurationAndPurposes = New System.Collections.Generic.Dictionary(Of SchedulingDemo.AppointmentLocation, System.Tuple(Of System.TimeSpan, String)())() From {{SchedulingDemo.ReceptionDeskData.AppointmentLocationClinic, {reviewOfTestResults, consultation, phoneConsultation}}, {SchedulingDemo.ReceptionDeskData.AppointmentLocationHospital, {biopsy, cancerScreening}}, {SchedulingDemo.ReceptionDeskData.AppointmentLocationHospice, {consultation, caseReview}}, {SchedulingDemo.ReceptionDeskData.AppointmentLocationHouseCall, {consultation, caseReview}}}
            SchedulingDemo.ReceptionDeskData.DepartmentCardiology_DurationAndPurposes = New System.Collections.Generic.Dictionary(Of SchedulingDemo.AppointmentLocation, System.Tuple(Of System.TimeSpan, String)())() From {{SchedulingDemo.ReceptionDeskData.AppointmentLocationClinic, {consultation, phoneConsultation, electrocardiogramTest}}, {SchedulingDemo.ReceptionDeskData.AppointmentLocationHospital, {bypassSurgery, pacemakerImplantation}}, {SchedulingDemo.ReceptionDeskData.AppointmentLocationHospice, {consultation, caseReview}}, {SchedulingDemo.ReceptionDeskData.AppointmentLocationHouseCall, {consultation, caseReview}}}
        End Sub

        Private Sub CreateAppointments()
            SchedulingDemo.ReceptionDeskData.Appointments = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.MedicalAppointment)()
            Dim appointmentId As Integer = 0
            Dim patientIndex As Integer = 0
            Dim firstDate As System.DateTime = SchedulingDemo.ReceptionDeskData.BaseDate.AddDays(-30)
            Dim lastDate As System.DateTime = SchedulingDemo.ReceptionDeskData.BaseDate.AddDays(30)
            For Each doctor As SchedulingDemo.Doctor In SchedulingDemo.ReceptionDeskData.Doctors
                Dim [date] As System.DateTime = firstDate
                While [date] < lastDate
                    Dim startTime As System.DateTime = [date].AddHours(9)
                    Dim endTime As System.DateTime = [date].AddHours(18)
                    Dim time As System.DateTime = startTime.AddMinutes(SchedulingDemo.ReceptionDeskData.random.[Next](0, 8) * 10)
                    While time < endTime
                        Dim patient = SchedulingDemo.ReceptionDeskData.Patients(patientIndex)
                        Dim location = GetRandomItem(SchedulingDemo.ReceptionDeskData.AppointmentLocations)
                        Dim durationAndPurpose = SchedulingDemo.ReceptionDeskData.GetDurationAndPurpose(doctor, location)
                        If durationAndPurpose Is Nothing Then Continue While
                        Dim duration = durationAndPurpose.Item1
                        Dim purpose = durationAndPurpose.Item2
                        Dim paymentState = SchedulingDemo.ReceptionDeskData.GetPaymentState(time, duration)
                        If time + duration > endTime Then Exit While
                        Dim apt = SchedulingDemo.MedicalAppointment.Create()
                        apt.Id = appointmentId
                        apt.StartTime = time
                        apt.EndTime = time.Add(duration)
                        apt.PaymentStateId = paymentState.Id
                        apt.Subject = patient.Name
                        apt.PatientId = patient.Id
                        apt.DoctorId = doctor.Id
                        apt.LocationId = location.Id
                        apt.Purpose = purpose
                        Call SchedulingDemo.ReceptionDeskData.Appointments.Add(apt)
                        appointmentId += 1
                        patientIndex += 1
                        If patientIndex >= SchedulingDemo.ReceptionDeskData.Patients.Count - 1 Then patientIndex = 0
                        time = time.Add(CType((duration), System.TimeSpan)).AddMinutes(SchedulingDemo.ReceptionDeskData.random.[Next](2, 8) * 10)
                    End While

                    [date] = [date].AddDays(1)
                End While
            Next
        End Sub

        Private Function GetPaymentState(ByVal time As System.DateTime, ByVal duration As System.TimeSpan) As PaymentState
            If time + duration >= System.DateTime.Now Then Return SchedulingDemo.ReceptionDeskData.PaymentStateNotYetBilled
            If time.[Date] = System.DateTime.Now.[Date] Then Return If(GetRandomItem({0, 0, 1}) = 0, SchedulingDemo.ReceptionDeskData.PaymentStatePaid, SchedulingDemo.ReceptionDeskData.PaymentStateOverdue)
            Return If(GetRandomItem({0, 0, 0, 1}) = 0, SchedulingDemo.ReceptionDeskData.PaymentStatePaid, SchedulingDemo.ReceptionDeskData.PaymentStateOverdue)
        End Function

        Private Function GetDurationAndPurpose(ByVal doctor As SchedulingDemo.Doctor, ByVal type As SchedulingDemo.AppointmentLocation) As Tuple(Of System.TimeSpan, String)
            Dim durationAndPurposes As System.Collections.Generic.Dictionary(Of SchedulingDemo.AppointmentLocation, System.Tuple(Of System.TimeSpan, String)()) = Nothing
            If doctor.Department Is SchedulingDemo.ReceptionDeskData.DepartmentPrimaryCare Then
                durationAndPurposes = SchedulingDemo.ReceptionDeskData.DepartmentPrimaryCare_DurationAndPurposes
            ElseIf doctor.Department Is SchedulingDemo.ReceptionDeskData.DepartmentOphthalmology Then
                durationAndPurposes = SchedulingDemo.ReceptionDeskData.DepartmentOphthalmology_DurationAndPurposes
            ElseIf doctor.Department Is SchedulingDemo.ReceptionDeskData.DepartmentDermatology Then
                durationAndPurposes = SchedulingDemo.ReceptionDeskData.DepartmentDermatology_DurationAndPurposes
            ElseIf doctor.Department Is SchedulingDemo.ReceptionDeskData.DepartmentCardiology Then
                durationAndPurposes = SchedulingDemo.ReceptionDeskData.DepartmentCardiology_DurationAndPurposes
            End If

            Return GetRandomItem(durationAndPurposes(type))
        End Function

        Private Function GetRandomItem(Of T)(ByVal source As System.Collections.Generic.IEnumerable(Of T)) As T
            If source.Count() = 0 Then Return DirectCast(Nothing, T)
            Return source.ElementAt(SchedulingDemo.ReceptionDeskData.random.[Next](0, source.Count()))
        End Function
    End Module
End Namespace
