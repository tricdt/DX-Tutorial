Imports System
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Windows.Media
Imports DevExpress.Xpf.Scheduling
Imports DevExpress.XtraScheduler

Namespace SchedulingDemo

    Public Class GymData

        Private _Trainers As ObservableCollection(Of SchedulingDemo.Trainer), _Trainings As ObservableCollection(Of SchedulingDemo.Training), _GymEvents As ObservableCollection(Of SchedulingDemo.GymEvent)

        Private NotInheritable Class TrainingKind

            Public Const Bodybuilding As Integer = 0

            Public Const Yoga As Integer = 1

            Public Const Zumba As Integer = 2

            Public Const Boxing As Integer = 3

            Public Const Cxworx As Integer = 4

            Public Const BodyPump As Integer = 5

            Public Const Pilates As Integer = 6

            Public Const Abl As Integer = 7

            Public Const Tabata As Integer = 8

            Public Const PrivateTraining As Integer = 9
        End Class

        Private Shared rnd As Random = New Random()

        Private Shared ReadOnly kindOfTraining As Training() = {New Training() With {.Id = TrainingKind.Bodybuilding, .Name = "Bodybuilding", .Color = Color.FromRgb(&HC1, &HF4, &H9C)}, New Training() With {.Id = TrainingKind.Yoga, .Name = "YOGA", .Color = Color.FromRgb(&HFF, &HC2, &HBE), .Description = "Yoga is a group of physical, mental, and spiritual practices or disciplines."}, New Training() With {.Id = TrainingKind.Zumba, .Name = "Zumba", .Color = Color.FromRgb(&HA8, &HD5, &HFF), .Description = "Zumba involves dance and aerobic movements performed to energetic music."}, New Training() With {.Id = TrainingKind.Boxing, .Name = "Boxing", .Color = Color.FromRgb(&H8D, &HE9, &HDF), .Description = "Boxing is a combat sport in which two people, wearing protective gloves, throw punches at each other for a predetermined set of time in a boxing ring."}, New Training() With {.Id = TrainingKind.Cxworx, .Name = "CXWORX", .Color = Color.FromRgb(&HF3, &HE4, &HC7), .Description = "Exercising muscles around the core."}, New Training() With {.Id = TrainingKind.BodyPump, .Name = "BodyPump", .Color = Color.FromRgb(&HF4, &HCE, &H93), .Description = "BodyPump is a barbell workout for anyone looking to get lean, toned and fit – fast."}, New Training() With {.Id = TrainingKind.Pilates, .Name = "Pilates", .Color = Color.FromRgb(&HC7, &HF4, &HFF), .Description = "Pilates classes build strength, flexibility and lean muscle tone with an emphasis on lengthening the body and aligning the spine."}, New Training() With {.Id = TrainingKind.Abl, .Name = "ABL", .Color = Color.FromRgb(&HCF, &HDB, &H98), .Description = "ABL program is specially designed for women. Strength training aims to develop the muscles of the abdomen, buttocks and legs."}, New Training() With {.Id = TrainingKind.Tabata, .Name = "Tabata", .Color = Color.FromRgb(&HE0, &HCF, &HE9), .Description = "Tabata is a form of high intensity interval training designed to get your heart rate up in that very hard anaerobic zone for short periods of time."}, New Training() With {.Id = TrainingKind.PrivateTraining, .Name = "Private Training", .Color = Color.FromRgb(&HFF, &HF7, &HA5)}}

        Private Shared ReadOnly trainerNames As Trainer() = {New Trainer() With {.Id = TrainingKind.Bodybuilding, .Name = "Mark Oliver"}, New Trainer() With {.Id = TrainingKind.Yoga, .Name = "Greta Sims"}, New Trainer() With {.Id = TrainingKind.Zumba, .Name = "Cindy Stanwick"}, New Trainer() With {.Id = TrainingKind.Boxing, .Name = "Robert Reagan"}, New Trainer() With {.Id = TrainingKind.Cxworx, .Name = "Andrew Fuller"}, New Trainer() With {.Id = TrainingKind.BodyPump, .Name = "Sammy Hill"}, New Trainer() With {.Id = TrainingKind.Pilates, .Name = "Laura Callahan"}, New Trainer() With {.Id = TrainingKind.Abl, .Name = "Terry Bradley"}, New Trainer() With {.Id = TrainingKind.Tabata, .Name = "Sandy Bright"}, New Trainer() With {.Id = TrainingKind.PrivateTraining, .Name = "Lucas Smith"}}

        Private Shared Function GenerateEvents(ByVal dayCount As Integer, ByVal startDate As Date) As ObservableCollection(Of GymEvent)
            Dim result As ObservableCollection(Of GymEvent) = GetEvents(0, startDate, 14)
            result.Add(GetBodybuildingEvent(result.Count, startDate, 14))
            Return result
        End Function

        Private Shared recurrenceInfoFormat As String = "<RecurrenceInfo Start=""{0}"" End=""{1}"" WeekDays=""{2}"" OccurrenceCount=""{3}"" Range=""{4}"" Type=""{5}"" Id=""{6}""/>"

        Private Shared Function GetBodybuildingEvent(ByVal id As Integer, ByVal [date] As Date, ByVal dayCount As Integer) As GymEvent
            Dim firstDate As Date = New DateTime([date].Year, [date].Month, [date].Day)
            Dim training As Training = kindOfTraining(TrainingKind.Bodybuilding)
            Dim pattern As GymEvent = GetGymEvent(id, firstDate.AddHours(8), firstDate.AddHours(22), training.Id, training.Id)
            pattern.AllDay = True
            pattern.Type = CInt(AppointmentType.Pattern)
            Dim recInfo As RecurrenceInfo = CType(RecurrenceBuilder.Daily(pattern.StartTime, pattern.EndTime.AddDays(dayCount)).ByDay(WeekDays.EveryDay).Build(), RecurrenceInfo)
            pattern.RecurrenceInfo = String.Format(CultureInfo.InvariantCulture, recurrenceInfoFormat, recInfo.Start, recInfo.End, CInt(recInfo.WeekDays), recInfo.OccurrenceCount, CInt(recInfo.Range), CInt(recInfo.Type), recInfo.Id.ToString())
            Return pattern
        End Function

        Private Shared Function GetEvents(ByVal id As Integer, ByVal [date] As Date, ByVal dayCount As Integer) As ObservableCollection(Of GymEvent)
            Dim result As ObservableCollection(Of GymEvent) = New ObservableCollection(Of GymEvent)()
            Dim firstDate As Date = New DateTime([date].Year, [date].Month, [date].Day)
            Dim lastDate As Date = firstDate.AddDays(dayCount)
            Dim dateTime As Date = firstDate
            While dateTime < lastDate
                Dim startDate As Date = dateTime.AddHours(rnd.Next(9, 12))
                Dim endDate As Date = dateTime.AddHours(22)
                Dim trainingId As Integer = rnd.Next(1, kindOfTraining.Length - 1)
                Dim startTime As Date = startDate
                While startTime < endDate
                    Dim gymEvent As GymEvent = GetGymEvent(id, startTime, startTime.AddHours(1), trainingId, trainingId)
                    result.Add(gymEvent)
                    id += 1
                    trainingId += 1
                    If trainingId >= kindOfTraining.Length Then trainingId = 1
                    startTime += New TimeSpan(rnd.Next(1, 4), 0, 0)
                End While

                dateTime += TimeSpan.FromDays(1)
            End While

            Return result
        End Function

        Private Shared Function GetGymEvent(ByVal id As Integer, ByVal start As Date, ByVal [end] As Date, ByVal trainingId As Integer, ByVal location As Integer) As GymEvent
            Dim training As Training = kindOfTraining(trainingId)
            Dim res = New GymEvent()
            res.Id = id
            res.StartTime = start
            res.EndTime = [end]
            res.TrainerId = trainingId
            res.TrainingId = trainingId
            res.Caption = training.Name
            res.Description = training.Description
            res.Room =(location + 1).ToString()
            res.Type = 0
            Return res
        End Function

        Public Sub New(ByVal dayCount As Integer)
            Trainers = New ObservableCollection(Of Trainer)(trainerNames)
            Trainings = New ObservableCollection(Of Training)(kindOfTraining)
            GymEvents = GenerateEvents(dayCount, Date.Today)
        End Sub

        Public Property Trainers As ObservableCollection(Of Trainer)
            Get
                Return _Trainers
            End Get

            Private Set(ByVal value As ObservableCollection(Of Trainer))
                _Trainers = value
            End Set
        End Property

        Public Property Trainings As ObservableCollection(Of Training)
            Get
                Return _Trainings
            End Get

            Private Set(ByVal value As ObservableCollection(Of Training))
                _Trainings = value
            End Set
        End Property

        Public Property GymEvents As ObservableCollection(Of GymEvent)
            Get
                Return _GymEvents
            End Get

            Private Set(ByVal value As ObservableCollection(Of GymEvent))
                _GymEvents = value
            End Set
        End Property
    End Class

    Public Class Training

        Public Property Id As Integer

        Public Property Name As String

        Public Property Color As Color

        Public Property Description As String
    End Class

    Public Class Trainer

        Public Property Id As Integer

        Public Property Name As String
    End Class

    Public Class GymEvent

        Public Property Id As Integer

        Public Property StartTime As Date

        Public Property EndTime As Date

        Public Property Caption As String

        Public Property TrainerId As Integer

        Public Property TrainingId As Integer

        Public Property Type As Integer?

        Public Property AllDay As Boolean

        Public Property Room As String

        Public Property Description As String

        Public Property RecurrenceInfo As String

        Public Property ReminderInfo As String
    End Class
End Namespace
