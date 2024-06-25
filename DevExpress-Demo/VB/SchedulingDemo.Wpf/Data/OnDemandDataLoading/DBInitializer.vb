Imports System
Imports System.Collections.Generic

Namespace SchedulingDemo

    Public Class DBInitializer

        Public Shared Sub Init()
            Call New DBInitializer().InitializeDBContext()
        End Sub

        Private Sub New()
        End Sub

        Private dbContext As SchedulingContext

        Private maxValueCore As Integer = 80

        Private resCount As Integer = 10

        Private [step] As Integer = 20

        Private random As Random = New Random()

        Private Sub InitializeDBContext()
            If SchedulingContext.IsExist() Then Return
            dbContext = New SchedulingContext()
            Try
                dbContext.SetAutoDetectChangesEnabled(False)
                dbContext.ExecuteSql(SQLCreateResourcesTable)
                dbContext.ExecuteSql(SQLCreateAptsTable)
                Generate()
            Finally
                dbContext.Dispose()
            End Try
        End Sub

        Private Sub Generate()
            Dim resId As Integer = 1
            For i As Integer = 1 To 6 - 1
                resId += 1
                dbContext.ResourceEntities.Add(New ResourceEntity() With {.Id = resId, .Description = "Room #" & 100 + i, .Group = "Main floor"})
            Next

            For i As Integer = 1 To 5 - 1
                resId += 1
                dbContext.ResourceEntities.Add(New ResourceEntity() With {.Id = resId, .Description = "Room #" & 200 + i, .Group = "Second floor"})
            Next

            Dim start As Integer = 0
            While start < maxValueCore
                GenerateByStep(start, start + [step])
                start += [step]
            End While
        End Sub

        Private Sub GenerateByStep(ByVal start As Integer, ByVal [end] As Integer)
            Dim appts = New List(Of AppointmentEntity)()
            For day As Integer = start To [end] - 1
                For res As Integer = 1 To resCount + 1 - 1
                    GenerateApptsForDay(appts, day, res)
                    GenerateApptsForDay(appts, -day - 1, res)
                Next
            Next

            dbContext.AppointmentEntities.AddRange(appts)
            dbContext.SaveChanges()
        End Sub

        Private Sub GenerateApptsForDay(ByVal appts As List(Of AppointmentEntity), ByVal day As Integer, ByVal res As Integer)
            Dim [date] As Date = Date.Today.AddDays(day)
            Dim shift As Integer = day + res
            Dim generate1 As Boolean = [date] > Date.Today.AddDays(-4) AndAlso [date] < Date.Today.AddDays(4) AndAlso (res = 1 OrElse res = 2)
            Dim generate2 As Boolean = generate1 OrElse shift Mod 7 > 1 OrElse shift Mod 7 < -5
            Dim generate3 As Boolean = generate1 OrElse generate2 AndAlso random.Next(0, 3) = 0
            If generate1 Then appts.Add(CreateAppt(res, [date], 6 + shift Mod 2, 8 + shift Mod 2))
            If generate2 Then appts.Add(CreateAppt(res, [date], 10 + shift Mod 3, 12 + shift Mod 3))
            If generate3 Then appts.Add(CreateAppt(res, [date], 15 + shift Mod 5, 18 + shift Mod 5))
        End Sub

        Private Function CreateAppt(ByVal res As Integer, ByVal [date] As Date, ByVal start As Integer, ByVal [end] As Integer) As AppointmentEntity
            Return New AppointmentEntity() With {.Subject = Names(random.Next(0, Names.Length)), .ResourceId = res, .Start = [date].AddHours(start), .QueryStart = [date].AddHours(start), .[End] = [date].AddHours([end]), .QueryEnd = [date].AddHours([end])}
        End Function

        Const SQLCreateResourcesTable As String = "
            CREATE TABLE ResourceEntities (
                `Id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
                `Description` TEXT,
                `Group` TEXT
                )"

        Const SQLCreateAptsTable As String = "
            CREATE TABLE AppointmentEntities (
                `Id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
                `AppointmentType` INTEGER,
                `AllDay` INTEGER,
                `Description` TEXT,
                `Location` TEXT,
                `End` REAL,
                `QueryEnd` REAL,
                `Label` INTEGER,
                `Status` INTEGER,
                `RecurrenceInfo` TEXT,
                `ReminderInfo` TEXT,
                `ResourceId` INTEGER,
                `Start` REAL,
                `QueryStart` REAL,
                `Subject` TEXT,
                `TimeZoneId` TEXT
                )"

        Private Shared ReadOnly Names As String() = {"Andrew Glover", "Mark Oliver", "Taylor Riley", "Addison Davis", "Benjamin Hughes", "Lucas Smith", "Robert King", "Laura Callahan", "Miguel Simmons", "Isabella Carter", "Andrew Fuller", "Madeleine Russell", "Steven Buchanan", "Nancy Davolio", "Michael Suyama", "Margaret Peacock", "Janet Leverling", "Ariana Alexander", "Brad Farkus", "Bart Arnaz", "Arnie Schwartz", "Billy Zimmer", "Samantha Piper", "Maggie Boxter", "Terry Bradley", "Greta Sims", "Cindy Stanwick", "Marcus Orbison", "Sandy Bright", "Ken Samuelson", "Brett Wade", "Wally Hobbs", "Brad Jameson", "Karen Goodson", "Morgan Kennedy", "Violet Bailey", "John Heart", "Arthur Miller", "Robert Reagan", "Ed Holmes", "Sammy Hill", "Olivia Peyton", "Jim Packard", "Hannah Brookly", "Harv Mudd", "Todd Hoffman", "Kevin Carter", "Mary Stern", "Robin Cosworth", "Jenny Hobbs", "Dallas Lou"}
    End Class
End Namespace
