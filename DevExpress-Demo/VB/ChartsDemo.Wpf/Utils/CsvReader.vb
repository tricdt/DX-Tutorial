Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports DevExpress.Utils

Namespace ChartsDemo

    Friend Module CsvReader

        Friend Function ReadFinancialData(ByVal fileName As String) As List(Of FinancialDataPoint)
            Dim longFileName As String = String.Empty
            Dim reader As StreamReader
            Dim dataSource = New List(Of FinancialDataPoint)()
            Dim stream As Stream = AssemblyHelper.GetEmbeddedResourceStream(GetType(CsvReader).Assembly, fileName, False)
            Try
                reader = New StreamReader(stream)
                While Not reader.EndOfStream
                    Dim line As String = reader.ReadLine()
                    Dim values = line.Split(","c)
                    values(1) = values(1).Trim()
                    Dim point = New FinancialDataPoint()
                    point.DateTimeStamp = Date.Parse(values(0))
                    If values(1).Length <= 3 Then Continue While
                    point.Close = Double.Parse(values(1), CultureInfo.InvariantCulture)
                    dataSource.Add(point)
                End While
            Catch
                Throw New Exception("It's impossible to load " & fileName)
            End Try

            Return dataSource
        End Function

        Friend Function ReadCarbonData(ByVal fileName As String) As List(Of CarbonContributionDataPoint)
            Dim longFileName As String = String.Empty
            Dim reader As StreamReader
            Dim dataSource = New List(Of CarbonContributionDataPoint)()
            Dim stream As Stream = AssemblyHelper.GetEmbeddedResourceStream(GetType(CsvReader).Assembly, fileName, False)
            Try
                reader = New StreamReader(stream)
                While Not reader.EndOfStream
                    Dim line As String = reader.ReadLine()
                    Dim values = line.Split(";"c)
                    Dim point = New CarbonContributionDataPoint()
                    point.Year = values(0)
                    point.Contribution = Double.Parse(values(1), CultureInfo.InvariantCulture)
                    point.Factor = values(2)
                    dataSource.Add(point)
                End While
            Catch
                Throw New Exception("It's impossible to load " & fileName)
            End Try

            Return dataSource
        End Function
    End Module
End Namespace
