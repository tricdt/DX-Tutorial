Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Windows
Imports System.Windows.Resources

Namespace ChartsDemo

    Public Class StarsData

        Private starsData As List(Of StarDataItem)

        Public ReadOnly Property Data As List(Of StarDataItem)
            Get
                Return If(starsData, Function()
                    starsData = GetStarsData()
                    Return starsData
                End Function())
            End Get
        End Property

        Private Function GetStarsData() As List(Of StarDataItem)
            Dim fileName As String = "starsdata.csv"
            Dim dataSource = New List(Of StarDataItem)()
            Try
                Dim path As String = "/ChartsDemo;component/Data/" & fileName
                Dim info As StreamResourceInfo = Application.GetResourceStream(New Uri(path, UriKind.RelativeOrAbsolute))
                Using reader As StreamReader = New StreamReader(info.Stream)
                    While Not reader.EndOfStream
                        Dim line As String = reader.ReadLine()
                        Dim values = line.Split(";"c)
                        Dim data As StarDataItem = New StarDataItem() With {.HipID = Integer.Parse(values(0), CultureInfo.InvariantCulture), .Spectr = values(1), .CI = Double.Parse(values(2), CultureInfo.InvariantCulture), .X = Double.Parse(values(3), CultureInfo.InvariantCulture), .Y = Double.Parse(values(4), CultureInfo.InvariantCulture), .Z = Double.Parse(values(5), CultureInfo.InvariantCulture), .Lum = Double.Parse(values(6), CultureInfo.InvariantCulture)}
                        dataSource.Add(data)
                    End While
                End Using
            Catch
                Throw New Exception("It's impossible to load " & fileName)
            End Try

            Return dataSource
        End Function
    End Class

    Public Structure StarDataItem

        Public Property HipID As Integer

        Public Property Spectr As String

        Public Property Lum As Double

        Public Property CI As Double

        Public Property X As Double

        Public Property Y As Double

        Public Property Z As Double
    End Structure
End Namespace
