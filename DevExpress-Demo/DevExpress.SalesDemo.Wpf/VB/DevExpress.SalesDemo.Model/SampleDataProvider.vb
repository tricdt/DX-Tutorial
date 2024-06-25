Imports System
Imports System.Collections.Generic

Namespace DevExpress.SalesDemo.Model

    Public Class SampleDataProvider
        Implements IDataProvider

        Private rnd As Random = New Random()

        Private channels As List(Of String) = New List(Of String)() From {"Consultants", "Direct", "Resellers", "Retail", "VARs"}

        Private products As List(Of String) = New List(Of String)() From {"Eco Max", "Eco Supreme", "EnviroCare", "EnviroCare Max", "SolarMax", "SolarOne"}

        Private sectors As List(Of String) = New List(Of String)() From {"Banking", "Energy", "Health", "Insurance", "Manufacturing", "Telecom"}

        Private regions As List(Of String) = New List(Of String)() From {"Asia", "Australia", "Europe", "North America", "South America"}

        Public Function GetTotalSalesByRange(ByVal start As Date, ByVal [end] As Date) As SalesGroup Implements IDataProvider.GetTotalSalesByRange
            Dim diff As TimeSpan = [end] - start
            Dim totalHours As Integer = CInt(diff.TotalHours)
            Dim amount As Integer = start.Day + rnd.Next(CInt(0.5 * totalHours), CInt(1.5 * totalHours)) * 10000
            Return New SalesGroup("Total Sales by Range", amount, amount \ 15, start, [end])
        End Function

        Public Function GetYtdSalesForecast() As Decimal
            Return 432123456.78D
        End Function

        Public Function GetSales(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup) Implements IDataProvider.GetSales
            Dim [step] As TimeSpan
            Dim actualStart As Date
            Dim randomStart As Integer
            Dim randomEnd As Integer
            Select Case period
                Case GroupingPeriod.Hour
                    [step] = New TimeSpan(1, 0, 0)
                    actualStart = New DateTime(start.Year, start.Month, start.Day, start.Hour, 0, 0)
                    randomStart = 50000
                    randomEnd = 250000
                Case GroupingPeriod.Day
                    [step] = New TimeSpan(24, 0, 0)
                    actualStart = New DateTime(start.Year, start.Month, start.Day, 0, 0, 0)
                    randomStart = 900000
                    randomEnd = 150000000
                Case GroupingPeriod.All
                    Return Nothing
                Case Else
                    Throw New Exception()
            End Select

            Dim list As List(Of SalesGroup) = New List(Of SalesGroup)()
            Dim [date] As Date = actualStart
            While [date] <= [end]
                If period = GroupingPeriod.Hour AndAlso ([date].Hour < 8 OrElse [date].Hour > 18) Then Continue While
                Dim amount As Decimal = rnd.Next(randomStart, randomEnd)
                Dim units As Integer = CInt(amount) \ 15
                Dim sg As SalesGroup = New SalesGroup(period.ToString(), amount, units, [date], [date] + [step])
                list.Add(sg)
                [date] += [step]
            End While

            Return list
        End Function

        Public Function GetSalesByChannel(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup) Implements IDataProvider.GetSalesByChannel
            Dim list As List(Of SalesGroup) = New List(Of SalesGroup)()
            If period = GroupingPeriod.All Then
                For Each channel As String In channels
                    Dim amount As Decimal = rnd.Next(4750, 87756)
                    Dim units As Integer = CInt(amount) \ 15
                    Dim sg As SalesGroup = New SalesGroup(channel, amount, units, start, [end])
                    list.Add(sg)
                Next

                Return list
            Else
                Dim [step] As TimeSpan
                Dim actualStart As Date
                Dim randomStart As Integer
                Dim randomEnd As Integer
                Select Case period
                    Case GroupingPeriod.Hour
                        [step] = New TimeSpan(1, 0, 0)
                        actualStart = New DateTime(start.Year, start.Month, start.Day, start.Hour, 0, 0)
                        randomStart = 50000
                        randomEnd = 250000
                    Case GroupingPeriod.Day
                        [step] = New TimeSpan(24, 0, 0)
                        actualStart = New DateTime(start.Year, start.Month, start.Day, 0, 0, 0)
                        randomStart = 900000
                        randomEnd = 150000000
                    Case GroupingPeriod.All
                        Return Nothing
                    Case Else
                        Throw New Exception()
                End Select

                For Each channel As String In channels
                    Dim [date] As Date = actualStart
                    While [date] <= [end]
                        If period = GroupingPeriod.Hour AndAlso ([date].Hour < 8 OrElse [date].Hour > 18) Then Continue While
                        Dim amount As Decimal = rnd.Next(randomStart, randomEnd)
                        Dim units As Integer = CInt(amount) \ 15
                        Dim sg As SalesGroup = New SalesGroup(channel, amount, units, [date], [date] + [step])
                        list.Add(sg)
                        [date] += [step]
                    End While
                Next

                Return list
            End If
        End Function

        Public Function GetSalesByProduct(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup) Implements IDataProvider.GetSalesByProduct
            Dim list As List(Of SalesGroup) = New List(Of SalesGroup)()
            For Each channel As String In channels
                Dim amount As Decimal = rnd.Next(4750, 87756)
                Dim units As Integer = CInt(amount) \ 15
                Dim sg As SalesGroup = New SalesGroup(channel, amount, units, start, [end])
                list.Add(sg)
            Next

            Return list
        End Function

        Public Function GetSalesByRegion(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup) Implements IDataProvider.GetSalesByRegion
            Dim list As List(Of SalesGroup) = New List(Of SalesGroup)()
            For Each region As String In regions
                Dim amount As Decimal = rnd.Next(4750, 87756)
                Dim units As Integer = CInt(amount) \ 15
                Dim sg As SalesGroup = New SalesGroup(region, amount, units, start, [end])
                list.Add(sg)
            Next

            Return list
        End Function

        Public Function GetSalesBySector(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup) Implements IDataProvider.GetSalesBySector
            Dim list As List(Of SalesGroup) = New List(Of SalesGroup)()
            For Each sector As String In sectors
                Dim amount As Decimal = rnd.Next(4750, 87756)
                Dim units As Integer = CInt(amount) \ 15
                Dim sg As SalesGroup = New SalesGroup(sector, amount, units, start, [end])
                list.Add(sg)
            Next

            Return list
        End Function
    End Class
End Namespace
