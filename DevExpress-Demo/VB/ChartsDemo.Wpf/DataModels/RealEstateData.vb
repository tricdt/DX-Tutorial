Imports System
Imports System.Collections.Generic

Namespace ChartsDemo

    Public Module RealEstateData

        Private Function GenerateMonthlyData(ByVal randomSeek As Integer, ByVal min As Integer, ByVal max As Integer) As List(Of RealEstateDataMonthlyPoint)
            Dim rnd As Random = New Random(randomSeek)
            Dim list As List(Of RealEstateDataMonthlyPoint) = New List(Of RealEstateDataMonthlyPoint)()
            For i As Integer = 0 To 12 - 1
                list.Add(New RealEstateDataMonthlyPoint() With {.Month = CType(i, Months).ToString(), .Income = rnd.Next(min, max)})
            Next

            Return list
        End Function

        Public Function GetAnnualData() As List(Of RealEstateDataAnnualPoint)
            Dim rnd As Random = New Random(1)
            Dim list As List(Of RealEstateDataAnnualPoint) = New List(Of RealEstateDataAnnualPoint)()
            Dim currentYear As Integer = Date.Now.Year
            For year As Integer = currentYear - 3 To currentYear - 1
                list.Add(New RealEstateDataAnnualPoint() With {.Employee = "Andrew Fuller", .MonthlyData = GenerateMonthlyData(year + 4, rnd.Next(5000, 50000), rnd.Next(50000, 500000)), .Year = year.ToString()})
                list.Add(New RealEstateDataAnnualPoint() With {.Employee = "Anne Dodsworth", .MonthlyData = GenerateMonthlyData(year + 5, rnd.Next(5000, 50000), rnd.Next(50000, 500000)), .Year = year.ToString()})
                list.Add(New RealEstateDataAnnualPoint() With {.Employee = "Margaret Peacock", .MonthlyData = GenerateMonthlyData(year + 6, rnd.Next(5000, 50000), rnd.Next(50000, 500000)), .Year = year.ToString()})
                list.Add(New RealEstateDataAnnualPoint() With {.Employee = "Michael Suyama", .MonthlyData = GenerateMonthlyData(year + 7, rnd.Next(5000, 50000), rnd.Next(50000, 500000)), .Year = year.ToString()})
                list.Add(New RealEstateDataAnnualPoint() With {.Employee = "Nancy Davolio", .MonthlyData = GenerateMonthlyData(year + 8, rnd.Next(5000, 50000), rnd.Next(50000, 500000)), .Year = year.ToString()})
                list.Add(New RealEstateDataAnnualPoint() With {.Employee = "Robert King", .MonthlyData = GenerateMonthlyData(year + 9, rnd.Next(5000, 50000), rnd.Next(50000, 500000)), .Year = year.ToString()})
            Next

            Return list
        End Function
    End Module

    Public Class RealEstateDataAnnualPoint

        Public Property Employee As String

        Public Property Year As String

        Public ReadOnly Property Income As Double
            Get
                Dim sum As Double = 0.0
                For Each point As RealEstateDataMonthlyPoint In MonthlyData
                    sum += point.Income
                Next

                Return sum
            End Get
        End Property

        Public Property MonthlyData As List(Of RealEstateDataMonthlyPoint)
    End Class

    Public Class RealEstateDataMonthlyPoint

        Public Property Month As String

        Public Property Income As Double
    End Class
End Namespace
