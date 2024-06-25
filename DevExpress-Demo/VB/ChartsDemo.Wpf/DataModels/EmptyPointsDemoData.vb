Imports System
Imports System.Collections.Generic

Namespace ChartsDemo

    Public Class WeatherInWashington

        Private Shared dataField As List(Of WeatherPoint)

        Public Shared ReadOnly Property Data As List(Of WeatherPoint)
            Get
                If dataField Is Nothing Then Call InitCollection()
                Return dataField
            End Get
        End Property

        Private Shared Sub InitCollection()
            dataField = New List(Of WeatherPoint)()
            Dim lastYear As Integer = Date.Now.Year - 1
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 10), .DayTemperature = 23, .NightTemperature = 22, .Pressure = 732})
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 11), .DayTemperature = 28, .NightTemperature = 22, .Wind = 5, .Pressure = 729})
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 12), .DayTemperature = 27, .NightTemperature = 22, .Wind = 5, .Pressure = 734})
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 13), .DayTemperature = 29, .Wind = 6, .Pressure = 734})
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 14), .DayTemperature = 28, .NightTemperature = 25, .Wind = 3, .Pressure = 735})
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 15)})
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 16), .DayTemperature = 29, .NightTemperature = 27, .Wind = 6, .Pressure = 731})
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 17), .DayTemperature = 22, .NightTemperature = 21, .Wind = 5, .Pressure = 731})
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 18), .DayTemperature = 26, .NightTemperature = 19, .Wind = 5, .Pressure = 734})
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 19), .DayTemperature = 27, .NightTemperature = 26, .Wind = 4, .Pressure = 733})
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 20), .DayTemperature = 27, .NightTemperature = 26, .Wind = 3, .Pressure = 731})
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 21), .DayTemperature = 26, .Wind = 7, .Pressure = 731})
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 22), .DayTemperature = 23, .NightTemperature = 23, .Wind = 2, .Pressure = 735})
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 23), .DayTemperature = 25, .Pressure = 735})
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 24), .DayTemperature = 26, .NightTemperature = 26, .Pressure = 732})
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 25), .DayTemperature = 37, .NightTemperature = 27, .Wind = 4, .Pressure = 729})
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 26), .DayTemperature = 30, .NightTemperature = 27, .Wind = 5, .Pressure = 731})
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 27), .DayTemperature = 30, .NightTemperature = 28, .Wind = 4, .Pressure = 731})
            Call dataField.Add(New WeatherPoint() With {.[Date] = New DateTime(lastYear, 7, 28), .DayTemperature = 28, .NightTemperature = 28, .Wind = 4, .Pressure = 731})
        End Sub

        Public Class WeatherPoint

            Public Property [Date] As Date

            Public Property NightTemperature As Double?

            Public Property DayTemperature As Double?

            Public Property Wind As Double?

            Public Property Pressure As Double?
        End Class
    End Class
End Namespace
