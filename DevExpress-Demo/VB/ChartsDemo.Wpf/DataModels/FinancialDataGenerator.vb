Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Windows
Imports System.Windows.Resources

Namespace ChartsDemo

    Friend Class FinancialDataGenerator

        Const InitialDataPointsCount As Integer = 3652

        Public Shared Function Generate() As List(Of FinancialDataPoint)
            Dim list As List(Of FinancialDataPoint) = New List(Of FinancialDataPoint)()
            Dim stamp As Date = Date.Now
            Dim uri As Uri = New Uri("/ChartsDemo;component/Data/InitialFinData.csv", UriKind.RelativeOrAbsolute)
            Dim info As StreamResourceInfo = Application.GetResourceStream(uri)
            Dim reader As StreamReader = New StreamReader(info.Stream)
            For i As Integer = 0 To InitialDataPointsCount - 1
                If Not reader.EndOfStream Then
                    Dim line As String = reader.ReadLine()
                    Dim values As String() = line.Split(","c)
                    Dim point As FinancialDataPoint = New FinancialDataPoint()
                    stamp = stamp.AddDays(-1)
                    point.DateTimeStamp = stamp
                    point.Open = Double.Parse(values(0), CultureInfo.InvariantCulture)
                    point.High = Double.Parse(values(1), CultureInfo.InvariantCulture)
                    point.Low = Double.Parse(values(2), CultureInfo.InvariantCulture)
                    point.Close = Double.Parse(values(3), CultureInfo.InvariantCulture)
                    point.Volume = Double.Parse(values(4), CultureInfo.InvariantCulture)
                    list.Insert(0, point)
                End If
            Next

            Return list
        End Function
    End Class
End Namespace
