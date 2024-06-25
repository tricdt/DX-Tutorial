Imports System
Imports System.Data.Common
Imports System.Data.OleDb

Namespace DevExpress.Demos.SalesDBGenerator

    Public Class SalesGenerator
        Implements SalesDemo.Model.IDataGenerator

        Private currentDate As Date = Date.Today.AddDays(40)

        Private minRequiredDate As Date = Date.Today.AddDays(-360)

        Private maxId As Integer = 0

        Public Function Run(ByVal connectionString As String) As Boolean
            Using connection = New OleDbConnection(connectionString)
                connection.Open()
                Dim helper = New SqlHelper(Of OleDbConnection, OleDbCommand)()
                Dim minDate As Date = helper.GetDate(helper.ReadValue(connection, "select min(sale_date) from sales"))
                Dim maxDate As Date = helper.GetDate(helper.ReadValue(connection, "select max(sale_date) from sales"))
                maxId = helper.GetInt(helper.ReadValue(connection, "select max(id) from sales"))
                Dim startDate As Date = minRequiredDate
                If minDate > startDate AndAlso maxDate <> Date.MinValue Then
                    startDate = maxDate.AddDays(1)
                End If

                If maxDate > Date.Today.AddDays(2) Then Return True
                Dim daysCount As Double = currentDate.Subtract(startDate).TotalDays
                RaiseStart()
                Try
                    Generate(connection, startDate, CInt(daysCount))
                Finally
                    RaiseComplete()
                End Try

                connection.Close()
            End Using

            Return True
        End Function

        Private random As Random = New Random(100)

        Private Sub Generate(ByVal connection As OleDbConnection, ByVal startDate As Date, ByVal daysCount As Integer)
            Using command As OleDbCommand = New OleDbCommand()
                command.Connection = connection
                command.CommandText = "insert into sales (id, units, cost_per_unit, discount, total_cost, sale_date, productId, RegionId, ChannelId, SectorId) values (@id, @units, @cost_per_unit, @discount, @total_cost, @sale_date, @productId, @RegionId, @ChannelId, @SectorId)"
                command.Parameters.AddRange(New OleDbParameter() {New OleDbParameter("@id", OleDbType.Integer), New OleDbParameter("@units", OleDbType.Integer), New OleDbParameter("@cost_per_unit", OleDbType.Integer), New OleDbParameter("@discount", OleDbType.Integer), New OleDbParameter("@total_cost", OleDbType.Integer), New OleDbParameter("@sale_date", OleDbType.Date), New OleDbParameter("@productId", OleDbType.Integer), New OleDbParameter("@RegionId", OleDbType.Integer), New OleDbParameter("@ChannelId", OleDbType.Integer), New OleDbParameter("@SectorId", OleDbType.Integer)})
                command.Prepare()
                For n As Integer = 0 To daysCount - 1
                    Console.Write("{0} of {1}" & Microsoft.VisualBasic.Constants.vbCr, n + 1, daysCount)
                    GenerateDay(command, startDate.AddDays(n))
                    RaiseProgress(CDbl(n) / CDbl(daysCount))
                Next
            End Using
        End Sub

        Private timeInterval As Integer = 15

        Private startTime As Integer = 8

        Private endTime As Integer = 17

        Private dailySalesGrowth As Double = 0.007R

        Private Sub GenerateDay(ByVal command As DbCommand, ByVal day As Date)
            Dim totalDays As Double = day.Subtract(minRequiredDate).TotalDays
            Dim salesPerDay As Integer = random.Next(180, CInt(200 * (1 + totalDays * dailySalesGrowth / 10)))
            Dim generateIntervals As Integer() = GenerateTimeIntervals(salesPerDay)
            Dim start As Date = New DateTime(day.Year, day.Month, day.Day, startTime, 0, 0)
            For d As Integer = 0 To generateIntervals.Length - 1
                For x As Integer = 0 To generateIntervals(d) - 1
                    GenerateSale(command, start)
                Next

                start = start.AddMinutes(timeInterval)
            Next
        End Sub

        Private Sub GenerateSale(ByVal command As DbCommand, ByVal [date] As Date)
            Dim id As Integer = GetId()
            Dim region As Integer = regions(rndRegions(random.Next(0, rndRegions.Length))).Id
            Dim channel As Integer = channels(rndChannels(random.Next(0, rndChannels.Length))).Id
            Dim sector As Integer = sectors(rndSectors(random.Next(0, rndSectors.Length))).Id
            Dim product As Product = products(rndProducts(random.Next(0, rndProducts.Length)))
            Dim productPrice As Integer = product.Price
            Dim discount As Integer = rndDiscounts(random.Next(0, rndDiscounts.Length))
            Dim units As Integer = rndUnits(random.Next(0, rndUnits.Length))
            Dim totalPrice As Integer = productPrice * units - discount
            command.Parameters("@id").Value = id
            command.Parameters("@units").Value = units
            command.Parameters("@cost_per_unit").Value = productPrice
            command.Parameters("@discount").Value = discount
            command.Parameters("@total_cost").Value = totalPrice
            command.Parameters("@sale_date").Value = [date]
            command.Parameters("@productId").Value = product.Id
            command.Parameters("@RegionId").Value = region
            command.Parameters("@ChannelId").Value = channel
            command.Parameters("@SectorId").Value = sector
            command.ExecuteNonQuery()
        End Sub

        Private Function GetId() As Integer
            Return Threading.Interlocked.Increment(maxId)
        End Function

        Private Function GenerateTimeIntervals(ByVal salesPerDay As Integer) As Integer()
            Dim count As Integer =(endTime - startTime) * (60 \ timeInterval)
            Dim res As Integer() = New Integer(count + 1 - 1) {}
            Dim salesPerInterval As Integer = salesPerDay \ res.Length
            Dim n As Integer = 10
            While True
                Dim num As Integer = random.Next(salesPerInterval - CInt(salesPerInterval * 0.8), salesPerInterval + CInt(salesPerInterval * 0.3))
                If num > salesPerDay Then num = salesPerDay
                salesPerDay -= num
                res(n) += num
                If num < 1 Then Exit While
                If n >= res.Length - 1 Then n = 0
                n += 1
            End While

            Return res
        End Function

#Region "Progress"
        Public Event GenerationStart As SalesDemo.Model.ProgressEventHandler Implements SalesDemo.Model.IDataGenerator.GenerationStart

        Public Event GenerationProgress As SalesDemo.Model.ProgressEventHandler Implements SalesDemo.Model.IDataGenerator.GenerationProgress

        Public Event GenerationComplete As SalesDemo.Model.ProgressEventHandler Implements SalesDemo.Model.IDataGenerator.GenerationComplete

        Private Sub RaiseStart()
            RaiseEvent GenerationStart(Me, New SalesDemo.Model.ProgressEventArgs(0))
        End Sub

        Private Sub RaiseProgress(ByVal progress As Double)
            RaiseEvent GenerationProgress(Me, New SalesDemo.Model.ProgressEventArgs(CInt(100.0 * progress)))
        End Sub

        Private Sub RaiseComplete()
            RaiseEvent GenerationComplete(Me, New SalesDemo.Model.ProgressEventArgs(100))
        End Sub

#End Region  ' Progress
#Region "Sample Data"
        Private rndDiscounts As Integer() = New Integer() {100, 200, 300, 400, 500, 600, 700, 800}

        Private rndUnits As Integer() = New Integer() {1, 1, 2, 2, 3, 4}

        Private rndRegions As Integer() = New Integer() {0, 0, 0, 0, 0, 1, 2, 2, 2, 3, 3, 4}

        Private rndChannels As Integer() = New Integer() {0, 0, 0, 0, 1, 2, 3, 3, 4, 4}

        Private rndSectors As Integer() = New Integer() {0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3, 3, 4, 4, 5, 5, 5}

        Private rndProducts As Integer() = New Integer() {0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 5, 5}

        Private products As Product() = New Product(5) {New Product() With {.Id = 1, .Name = "Eco Max", .Price = 2500}, New Product() With {.Id = 2, .Name = "Eco Supreme", .Price = 2000}, New Product() With {.Id = 3, .Name = "EnviroCare", .Price = 1750}, New Product() With {.Id = 4, .Name = "EnviroCare Max", .Price = 2800}, New Product() With {.Id = 5, .Name = "SolarOne", .Price = 1500}, New Product() With {.Id = 6, .Name = "SolarMax", .Price = 2250}}

        Private regions As Region() = New Region(4) {New Region() With {.Id = 1, .Name = "North America"}, New Region() With {.Id = 2, .Name = "South America"}, New Region() With {.Id = 3, .Name = "Europe"}, New Region() With {.Id = 4, .Name = "Asia"}, New Region() With {.Id = 5, .Name = "Australia"}}

        Private channels As Channel() = New Channel(4) {New Channel() With {.Id = 1, .Name = "Direct"}, New Channel() With {.Id = 2, .Name = "VARs"}, New Channel() With {.Id = 3, .Name = "Consultants"}, New Channel() With {.Id = 4, .Name = "Resellers"}, New Channel() With {.Id = 5, .Name = "Retail"}}

        Private sectors As Sector() = New Sector(5) {New Sector() With {.Id = 1, .Name = "Energy"}, New Sector() With {.Id = 2, .Name = "Manufacturing"}, New Sector() With {.Id = 3, .Name = "Telecom"}, New Sector() With {.Id = 4, .Name = "Insurance"}, New Sector() With {.Id = 5, .Name = "Banking"}, New Sector() With {.Id = 6, .Name = "Health"}}
#End Region  ' Sample Data
    End Class

    Public Class Product

        Public Property Id As Integer

        Public Property Name As String

        Public Property Price As Integer

        Public Property Discount As Integer
    End Class

    Public Class Region

        Public Property Id As Integer

        Public Property Name As String
    End Class

    Public Class Channel

        Public Property Id As Integer

        Public Property Name As String
    End Class

    Public Class Sector

        Public Property Id As Integer

        Public Property Name As String
    End Class
End Namespace
