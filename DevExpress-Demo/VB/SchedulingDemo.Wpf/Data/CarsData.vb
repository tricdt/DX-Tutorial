Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Scheduling
Imports DevExpress.XtraScheduler

Namespace SchedulingDemo

    Public Class Car

        Public Sub New(ByVal carId As Integer, ByVal caption As String)
            Me.Id = carId
            Me.Caption = caption
        End Sub

        Public Property Id As Integer

        Public Property Caption As String
    End Class

    Public Class CarScheduling

        Public Property Id As Integer

        Public Property AllDay As Boolean

        Public Property StartTime As DateTime

        Public Property EndTime As DateTime

        Public Property Description As String

        Public Property Status As Integer

        Public Property Label As Integer

        Public Property EventType As Integer

        Public Property Location As String

        Public Property Subject As String

        Public Property RecurrenceInfo As String

        Public Property ReminderInfo As String

        Public Property CarId As Integer?

        Public Property Price As Double

        Public Property Image As ImageSource
    End Class

    Public Class CarsData

        Private _Cars As List(Of SchedulingDemo.Car), _Events As List(Of SchedulingDemo.CarScheduling)

        Private NotInheritable Class CarBrand

            Public Const MercedesBenz As Integer = 1

            Public Const Audi As Integer = 2

            Public Const Chevrolet As Integer = 3

            Public Const Lexus As Integer = 4

            Public Const Toyota As Integer = 5

            Public Const Nissan As Integer = 6

            Public Const Ford As Integer = 7
        End Class

        Private NotInheritable Class CarDescription

            Public Const Rent As String = "Rent this car"

            Public Const RentAllDay As String = "Rent this car for the all day"

            Public Const Repair As String = "Scheduled repair of this car"

            Public Const CheckUp As String = "Check up after maintenance"

            Public Const Wash As String = "Wash this car in the garage"
        End Class

        Private NotInheritable Class CarLabel

            Public Const None As Integer = 0

            Public Const Important As Integer = 1
        End Class

        Private NotInheritable Class CarStatus

            Public Const Free As Integer = 0

            Public Const Tentative As Integer = 1

            Public Const Busy As Integer = 2

            Public Const OutOfOffice As Integer = 3

            Public Const WorkingElsewhere As Integer = 4
        End Class

        Private NotInheritable Class CarLocation

            Public Const Garage As String = "Garage"

            Public Const ServiceCenter As String = "Service Center"

            Public Const City As String = "City"

            Public Const OutOfTown As String = "Out-Of-Town"
        End Class

        Private NotInheritable Class CarImages

            Public Shared ReadOnly CheckUp As System.Windows.Media.ImageSource

            Public Shared ReadOnly Free As System.Windows.Media.ImageSource

            Public Shared ReadOnly Maintance As System.Windows.Media.ImageSource

            Public Shared ReadOnly Rent As System.Windows.Media.ImageSource

            Public Shared ReadOnly Wash As System.Windows.Media.ImageSource

            Shared Sub New()
                SchedulingDemo.CarsData.CarImages.CheckUp = SchedulingDemo.CarsData.CarImages.GetImage("CheckUp")
                SchedulingDemo.CarsData.CarImages.Free = SchedulingDemo.CarsData.CarImages.GetImage("Free")
                SchedulingDemo.CarsData.CarImages.Maintance = SchedulingDemo.CarsData.CarImages.GetImage("Maintance")
                SchedulingDemo.CarsData.CarImages.Rent = SchedulingDemo.CarsData.CarImages.GetImage("Rent")
                SchedulingDemo.CarsData.CarImages.Wash = SchedulingDemo.CarsData.CarImages.GetImage("Wash")
            End Sub

            Private Shared Function GetImage(ByVal imageName As String) As ImageSource
                Dim uri As System.Uri = New System.Uri(String.Format("pack://application:,,,/SchedulingDemo;component/Images/Cars/{0}.svg", imageName))
                Dim extension = New DevExpress.Xpf.Core.SvgImageSourceExtension() With {.Uri = uri, .Size = New System.Windows.Size(16, 16)}
                Dim image = CType(extension.ProvideValue(Nothing), System.Windows.Media.ImageSource)
                image.Freeze()
                Return image
            End Function
        End Class

        Private Shared Function CreateCars() As List(Of SchedulingDemo.Car)
            Dim cars As System.Collections.Generic.List(Of SchedulingDemo.Car) = New System.Collections.Generic.List(Of SchedulingDemo.Car)()
            cars.Add(New SchedulingDemo.Car(SchedulingDemo.CarsData.CarBrand.MercedesBenz, "Mercedes-Benz Slk350"))
            cars.Add(New SchedulingDemo.Car(SchedulingDemo.CarsData.CarBrand.Chevrolet, "Chevrolet Camaro"))
            cars.Add(New SchedulingDemo.Car(SchedulingDemo.CarsData.CarBrand.Audi, "Audi S8"))
            cars.Add(New SchedulingDemo.Car(SchedulingDemo.CarsData.CarBrand.Lexus, "Lexus IS 350"))
            cars.Add(New SchedulingDemo.Car(SchedulingDemo.CarsData.CarBrand.Toyota, "Toyota Tundra 4x4 Reg Cab"))
            cars.Add(New SchedulingDemo.Car(SchedulingDemo.CarsData.CarBrand.Nissan, "Nissan Murano"))
            cars.Add(New SchedulingDemo.Car(SchedulingDemo.CarsData.CarBrand.Ford, "Ford Mustang GT Coupe"))
            Return cars
        End Function

        Private Shared Function CreateAppointment(ByVal startDate As System.DateTime) As List(Of SchedulingDemo.CarScheduling)
            Dim recurrenceFormat As String = "<RecurrenceInfo Start=""{0}"" End=""{1}"" WeekOfMonth=""{2}"" WeekDays=""{3}"" Month=""{4}"" OccurrenceCount=""{5}"" Range=""{6}"" Type=""{7}"" Id=""{8}""/>"
            Dim changedOccurrenceFormat As String = "<RecurrenceInfo Id=""{0}"" Index=""{1}""/>"
            Dim appts As System.Collections.Generic.List(Of SchedulingDemo.CarScheduling) = New System.Collections.Generic.List(Of SchedulingDemo.CarScheduling)()
            Dim apt1 As SchedulingDemo.CarScheduling = New SchedulingDemo.CarScheduling()
            apt1.EventType = CInt(DevExpress.XtraScheduler.AppointmentType.Normal)
            apt1.StartTime = startDate.Add(New System.TimeSpan(3, 08, 15, 00))
            apt1.EndTime = startDate.Add(New System.TimeSpan(5, 16, 40, 00))
            apt1.Description = SchedulingDemo.CarsData.CarDescription.Repair
            apt1.Location = SchedulingDemo.CarsData.CarLocation.ServiceCenter
            apt1.CarId = SchedulingDemo.CarsData.CarBrand.Audi
            apt1.Status = SchedulingDemo.CarsData.CarStatus.Tentative
            apt1.Subject = "Repair"
            apt1.Price = 90
            apt1.Image = SchedulingDemo.CarsData.CarImages.Maintance
            appts.Add(apt1)
            Dim apt2 As SchedulingDemo.CarScheduling = New SchedulingDemo.CarScheduling()
            apt2.EventType = CInt(DevExpress.XtraScheduler.AppointmentType.Normal)
            apt2.StartTime = startDate.Add(New System.TimeSpan(10, 00, 00))
            apt2.EndTime = startDate.Add(New System.TimeSpan(2, 11, 45, 00))
            apt2.Description = SchedulingDemo.CarsData.CarDescription.Rent
            apt2.Location = SchedulingDemo.CarsData.CarLocation.OutOfTown
            apt2.CarId = SchedulingDemo.CarsData.CarBrand.Chevrolet
            apt2.Status = SchedulingDemo.CarsData.CarStatus.Tentative
            apt2.Subject = "Mrs.Black"
            apt2.Price = 5
            apt2.Image = SchedulingDemo.CarsData.CarImages.Rent
            appts.Add(apt2)
            Dim apt3 As SchedulingDemo.CarScheduling = New SchedulingDemo.CarScheduling()
            apt3.EventType = CInt(DevExpress.XtraScheduler.AppointmentType.Normal)
            apt3.StartTime = startDate.Add(New System.TimeSpan(1, 13, 00, 00))
            apt3.EndTime = startDate.Add(New System.TimeSpan(1, 14, 30, 00))
            apt3.Description = SchedulingDemo.CarsData.CarDescription.Rent
            apt3.Location = SchedulingDemo.CarsData.CarLocation.OutOfTown
            apt3.CarId = SchedulingDemo.CarsData.CarBrand.Chevrolet
            apt3.Status = SchedulingDemo.CarsData.CarStatus.OutOfOffice
            apt3.Subject = "Mrs.Black"
            apt3.Price = 6
            apt3.Image = SchedulingDemo.CarsData.CarImages.Rent
            appts.Add(apt3)
            Dim apt4 As SchedulingDemo.CarScheduling = New SchedulingDemo.CarScheduling()
            apt4.EventType = CInt(DevExpress.XtraScheduler.AppointmentType.Normal)
            apt4.StartTime = startDate.Add(New System.TimeSpan(2, 15, 30, 00))
            apt4.EndTime = startDate.Add(New System.TimeSpan(3, 14, 00, 00))
            apt4.Description = SchedulingDemo.CarsData.CarDescription.Rent
            apt4.Location = SchedulingDemo.CarsData.CarLocation.OutOfTown
            apt4.CarId = SchedulingDemo.CarsData.CarBrand.Chevrolet
            apt4.Status = SchedulingDemo.CarsData.CarStatus.OutOfOffice
            apt4.Subject = "Mrs.Black"
            apt4.Price = 4
            apt4.Image = SchedulingDemo.CarsData.CarImages.Rent
            appts.Add(apt4)
            Dim apt5 As SchedulingDemo.CarScheduling = New SchedulingDemo.CarScheduling()
            apt5.EventType = CInt(DevExpress.XtraScheduler.AppointmentType.Pattern)
            apt5.StartTime = startDate.Add(New System.TimeSpan(07, 30, 00))
            apt5.EndTime = startDate.Add(New System.TimeSpan(08, 45, 00))
            apt5.Description = SchedulingDemo.CarsData.CarDescription.Wash
            apt5.Location = SchedulingDemo.CarsData.CarLocation.Garage
            apt5.CarId = SchedulingDemo.CarsData.CarBrand.Chevrolet
            apt5.Status = SchedulingDemo.CarsData.CarStatus.Tentative
            apt5.Price = 4
            apt5.Image = SchedulingDemo.CarsData.CarImages.Wash
            apt5.Subject = "Wash"
            Dim recInfo As DevExpress.XtraScheduler.RecurrenceInfo = CType(DevExpress.Xpf.Scheduling.RecurrenceBuilder.Weekly(CDate((startDate.Add(CType((New System.TimeSpan(CInt((07)), CInt((00)), CInt((00)))), System.TimeSpan)))), CDate((startDate.Add(CType((New System.TimeSpan(CInt((44)), CInt((01)), CInt((00)), CInt((00)))), System.TimeSpan))))).ByDay(CType((DevExpress.XtraScheduler.WeekDays.Monday Or DevExpress.XtraScheduler.WeekDays.Wednesday Or DevExpress.XtraScheduler.WeekDays.Friday), DevExpress.XtraScheduler.WeekDays)).Build(), DevExpress.XtraScheduler.RecurrenceInfo)
            apt5.RecurrenceInfo = System.[String].Format(System.Globalization.CultureInfo.InvariantCulture, recurrenceFormat, recInfo.Start, recInfo.[End], CInt(recInfo.WeekOfMonth), CInt(recInfo.WeekDays), recInfo.Month, recInfo.OccurrenceCount, CInt(recInfo.Range), CInt(recInfo.Type), recInfo.Id.ToString())
            appts.Add(apt5)
            Dim apt6 As SchedulingDemo.CarScheduling = New SchedulingDemo.CarScheduling()
            apt6.EventType = CInt(DevExpress.XtraScheduler.AppointmentType.ChangedOccurrence)
            apt6.StartTime = startDate.Add(New System.TimeSpan(8, 01, 30, 00))
            apt6.EndTime = startDate.Add(New System.TimeSpan(8, 09, 00, 00))
            apt6.Description = SchedulingDemo.CarsData.CarDescription.Wash
            apt6.Location = SchedulingDemo.CarsData.CarLocation.Garage
            apt6.CarId = SchedulingDemo.CarsData.CarBrand.Chevrolet
            apt6.Status = SchedulingDemo.CarsData.CarStatus.Tentative
            apt6.Subject = "Wash"
            apt6.RecurrenceInfo = System.[String].Format(System.Globalization.CultureInfo.InvariantCulture, changedOccurrenceFormat, recInfo.Id.ToString(), 4)
            apt6.Price = 6
            apt6.Image = SchedulingDemo.CarsData.CarImages.Wash
            appts.Add(apt6)
            Dim apt7 As SchedulingDemo.CarScheduling = New SchedulingDemo.CarScheduling()
            apt7.EventType = CInt(DevExpress.XtraScheduler.AppointmentType.Normal)
            apt7.StartTime = startDate
            apt7.EndTime = startDate.AddDays(1)
            apt7.AllDay = True
            apt7.Description = SchedulingDemo.CarsData.CarDescription.RentAllDay
            apt7.Location = SchedulingDemo.CarsData.CarLocation.City
            apt7.CarId = SchedulingDemo.CarsData.CarBrand.MercedesBenz
            apt7.Status = SchedulingDemo.CarsData.CarStatus.Busy
            apt7.Subject = "Mr.Green"
            apt7.Price = 6
            apt7.Image = SchedulingDemo.CarsData.CarImages.Rent
            appts.Add(apt7)
            Dim apt8 As SchedulingDemo.CarScheduling = New SchedulingDemo.CarScheduling()
            apt8.EventType = CInt(DevExpress.XtraScheduler.AppointmentType.Normal)
            apt8.StartTime = startDate.Add(New System.TimeSpan(1, 11, 05, 00))
            apt8.EndTime = startDate.Add(New System.TimeSpan(1, 14, 30, 00))
            apt8.Description = SchedulingDemo.CarsData.CarDescription.Rent
            apt8.Location = SchedulingDemo.CarsData.CarLocation.City
            apt8.CarId = SchedulingDemo.CarsData.CarBrand.MercedesBenz
            apt8.Status = SchedulingDemo.CarsData.CarStatus.OutOfOffice
            apt8.Subject = "Mr.Brown"
            apt8.Price = 8
            apt8.Image = SchedulingDemo.CarsData.CarImages.Rent
            appts.Add(apt8)
            Dim apt9 As SchedulingDemo.CarScheduling = New SchedulingDemo.CarScheduling()
            apt9.EventType = CInt(DevExpress.XtraScheduler.AppointmentType.Normal)
            apt9.StartTime = startDate.Add(New System.TimeSpan(2, 10, 00, 00))
            apt9.EndTime = startDate.Add(New System.TimeSpan(4, 17, 00, 00))
            apt9.Description = SchedulingDemo.CarsData.CarDescription.Rent
            apt9.Location = SchedulingDemo.CarsData.CarLocation.City
            apt9.CarId = SchedulingDemo.CarsData.CarBrand.MercedesBenz
            apt9.Status = SchedulingDemo.CarsData.CarStatus.OutOfOffice
            apt9.Subject = "Mr.White"
            apt9.Price = 10
            apt9.Image = SchedulingDemo.CarsData.CarImages.Rent
            appts.Add(apt9)
            Dim apt10 As SchedulingDemo.CarScheduling = New SchedulingDemo.CarScheduling()
            apt10.EventType = CInt(DevExpress.XtraScheduler.AppointmentType.Normal)
            apt10.StartTime = startDate.Add(New System.TimeSpan(4, 19, 45, 00))
            apt10.EndTime = startDate.Add(New System.TimeSpan(4, 22, 45, 00))
            apt10.Description = SchedulingDemo.CarsData.CarDescription.CheckUp
            apt10.Location = SchedulingDemo.CarsData.CarLocation.ServiceCenter
            apt10.CarId = SchedulingDemo.CarsData.CarBrand.MercedesBenz
            apt10.Status = SchedulingDemo.CarsData.CarStatus.WorkingElsewhere
            apt10.Subject = "Check up"
            apt10.Price = 45
            apt10.Image = SchedulingDemo.CarsData.CarImages.CheckUp
            appts.Add(apt10)
            Dim apt11 As SchedulingDemo.CarScheduling = New SchedulingDemo.CarScheduling()
            apt11.EventType = CInt(DevExpress.XtraScheduler.AppointmentType.Pattern)
            apt11.StartTime = startDate.Add(New System.TimeSpan(-6, 16, 40, 00))
            apt11.EndTime = startDate.Add(New System.TimeSpan(-6, 17, 50, 00))
            apt11.Description = SchedulingDemo.CarsData.CarDescription.Wash
            apt11.Label = SchedulingDemo.CarsData.CarLabel.Important
            apt11.Location = SchedulingDemo.CarsData.CarLocation.Garage
            apt11.CarId = SchedulingDemo.CarsData.CarBrand.MercedesBenz
            apt11.Status = SchedulingDemo.CarsData.CarStatus.Tentative
            apt11.Subject = "Wash"
            apt11.Image = SchedulingDemo.CarsData.CarImages.Wash
            apt11.Price = 7
            Dim recInfo1 As DevExpress.XtraScheduler.RecurrenceInfo = CType(DevExpress.Xpf.Scheduling.RecurrenceBuilder.Weekly(CDate((startDate.Add(CType((New System.TimeSpan(CInt((-6)), CInt((16)), CInt((30)), CInt((00)))), System.TimeSpan)))), CDate((startDate.Add(CType((New System.TimeSpan(CInt((21)), CInt((00)), CInt((00)), CInt((00)))), System.TimeSpan))))).ByDay(CType((DevExpress.XtraScheduler.WeekDays.WorkDays), DevExpress.XtraScheduler.WeekDays)).Build(), DevExpress.XtraScheduler.RecurrenceInfo)
            apt11.RecurrenceInfo = System.[String].Format(System.Globalization.CultureInfo.InvariantCulture, recurrenceFormat, recInfo1.Start, recInfo1.[End], CInt(recInfo1.WeekOfMonth), CInt(recInfo1.WeekDays), recInfo1.Month, recInfo1.OccurrenceCount, CInt(recInfo1.Range), CInt(recInfo1.Type), recInfo1.Id.ToString())
            appts.Add(apt11)
            Dim apt12 As SchedulingDemo.CarScheduling = New SchedulingDemo.CarScheduling()
            apt12.EventType = CInt(DevExpress.XtraScheduler.AppointmentType.ChangedOccurrence)
            apt12.StartTime = startDate.Add(New System.TimeSpan(2, 18, 30, 00))
            apt12.EndTime = startDate.Add(New System.TimeSpan(2, 20, 00, 00))
            apt12.Description = SchedulingDemo.CarsData.CarDescription.Wash
            apt12.Label = SchedulingDemo.CarsData.CarLabel.Important
            apt12.Location = SchedulingDemo.CarsData.CarLocation.Garage
            apt12.CarId = SchedulingDemo.CarsData.CarBrand.MercedesBenz
            apt12.Status = SchedulingDemo.CarsData.CarStatus.Tentative
            apt12.Subject = "Wash"
            apt12.RecurrenceInfo = System.[String].Format(System.Globalization.CultureInfo.InvariantCulture, changedOccurrenceFormat, recInfo1.Id.ToString(), 6)
            apt12.Price = 5
            apt12.Image = SchedulingDemo.CarsData.CarImages.Wash
            appts.Add(apt12)
            Dim apt13 As SchedulingDemo.CarScheduling = New SchedulingDemo.CarScheduling()
            apt13.EventType = CInt(DevExpress.XtraScheduler.AppointmentType.DeletedOccurrence)
            apt13.StartTime = startDate.Add(New System.TimeSpan(5, 16, 20, 00))
            apt13.EndTime = startDate.Add(New System.TimeSpan(5, 17, 40, 00))
            apt13.Description = SchedulingDemo.CarsData.CarDescription.Wash
            apt13.Label = SchedulingDemo.CarsData.CarLabel.Important
            apt13.Location = SchedulingDemo.CarsData.CarLocation.Garage
            apt13.CarId = SchedulingDemo.CarsData.CarBrand.MercedesBenz
            apt13.Status = SchedulingDemo.CarsData.CarStatus.Tentative
            apt13.Subject = "Wash"
            apt13.RecurrenceInfo = System.[String].Format(System.Globalization.CultureInfo.InvariantCulture, changedOccurrenceFormat, recInfo1.Id.ToString(), 7)
            apt13.Price = 5
            apt13.Image = SchedulingDemo.CarsData.CarImages.Wash
            appts.Add(apt13)
            Dim apt14 As SchedulingDemo.CarScheduling = New SchedulingDemo.CarScheduling()
            apt14.EventType = CInt(DevExpress.XtraScheduler.AppointmentType.ChangedOccurrence)
            apt14.StartTime = startDate.Add(New System.TimeSpan(9, 15, 00, 00))
            apt14.EndTime = startDate.Add(New System.TimeSpan(9, 16, 30, 00))
            apt14.Description = SchedulingDemo.CarsData.CarDescription.Wash
            apt14.Label = SchedulingDemo.CarsData.CarLabel.Important
            apt14.Location = SchedulingDemo.CarsData.CarLocation.Garage
            apt14.CarId = SchedulingDemo.CarsData.CarBrand.MercedesBenz
            apt14.Status = SchedulingDemo.CarsData.CarStatus.Tentative
            apt14.Subject = "Wash"
            apt14.RecurrenceInfo = System.[String].Format(System.Globalization.CultureInfo.InvariantCulture, changedOccurrenceFormat, recInfo1.Id.ToString(), 11)
            apt14.Price = 5
            apt14.Image = SchedulingDemo.CarsData.CarImages.Wash
            appts.Add(apt14)
            Dim apt15 As SchedulingDemo.CarScheduling = New SchedulingDemo.CarScheduling()
            apt15.EventType = CInt(DevExpress.XtraScheduler.AppointmentType.ChangedOccurrence)
            apt15.StartTime = startDate.Add(New System.TimeSpan(13, 16, 30, 00))
            apt15.EndTime = startDate.Add(New System.TimeSpan(13, 17, 00, 00))
            apt15.Description = SchedulingDemo.CarsData.CarDescription.Wash
            apt15.Label = SchedulingDemo.CarsData.CarLabel.Important
            apt15.Location = SchedulingDemo.CarsData.CarLocation.Garage
            apt15.CarId = SchedulingDemo.CarsData.CarBrand.MercedesBenz
            apt15.Status = SchedulingDemo.CarsData.CarStatus.Tentative
            apt15.Subject = "Wash"
            apt15.RecurrenceInfo = System.[String].Format(System.Globalization.CultureInfo.InvariantCulture, changedOccurrenceFormat, recInfo1.Id.ToString(), 13)
            apt15.Price = 5
            apt15.Image = SchedulingDemo.CarsData.CarImages.Wash
            appts.Add(apt15)
            Dim apt16 As SchedulingDemo.CarScheduling = New SchedulingDemo.CarScheduling()
            apt16.EventType = CInt(DevExpress.XtraScheduler.AppointmentType.DeletedOccurrence)
            apt16.StartTime = startDate.Add(New System.TimeSpan(2, 16, 25, 00))
            apt16.EndTime = startDate.Add(New System.TimeSpan(2, 17, 45, 00))
            apt16.Description = SchedulingDemo.CarsData.CarDescription.Wash
            apt16.Label = SchedulingDemo.CarsData.CarLabel.Important
            apt16.Location = SchedulingDemo.CarsData.CarLocation.Garage
            apt16.CarId = SchedulingDemo.CarsData.CarBrand.MercedesBenz
            apt16.Status = SchedulingDemo.CarsData.CarStatus.Tentative
            apt16.Subject = "Wash"
            apt16.RecurrenceInfo = System.[String].Format(System.Globalization.CultureInfo.InvariantCulture, changedOccurrenceFormat, recInfo1.Id.ToString(), 4)
            apt16.Price = 6
            apt16.Image = SchedulingDemo.CarsData.CarImages.Wash
            appts.Add(apt16)
            Return appts
        End Function

        Public Property Cars As List(Of SchedulingDemo.Car)
            Get
                Return _Cars
            End Get

            Private Set(ByVal value As List(Of SchedulingDemo.Car))
                _Cars = value
            End Set
        End Property

        Public Property Events As List(Of SchedulingDemo.CarScheduling)
            Get
                Return _Events
            End Get

            Private Set(ByVal value As List(Of SchedulingDemo.CarScheduling))
                _Events = value
            End Set
        End Property

        Public Sub New()
            Me.Cars = SchedulingDemo.CarsData.CreateCars()
            Me.Events = SchedulingDemo.CarsData.CreateAppointment(System.DateTime.Today)
        End Sub
    End Class
End Namespace
