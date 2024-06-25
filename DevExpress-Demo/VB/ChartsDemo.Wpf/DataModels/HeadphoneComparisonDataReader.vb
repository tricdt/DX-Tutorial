Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Windows

Namespace ChartsDemo

    Friend Class HeadphoneComparisonDataReader

        Const FileName As String = "HeadphoneComparison.dat"

        Shared Public Function ReadDataFromFile() As List(Of HeadphoneComparisonPoint)
            Dim dataSource As List(Of HeadphoneComparisonPoint) = New List(Of HeadphoneComparisonPoint)()
            Dim uri As Uri = New Uri("/ChartsDemo;component/Data/" & FileName, UriKind.RelativeOrAbsolute)
            Using stream As Stream = Application.GetResourceStream(uri).Stream
                Dim reader As StreamReader
                Try
                    reader = New StreamReader(stream)
                    While Not reader.EndOfStream
                        Dim line As String = reader.ReadLine()
                        If line.Length = 0 OrElse line.StartsWith("//") Then Continue While
                        Dim cells As String() = line.Split(New String() {","}, StringSplitOptions.None)
                        Dim name As String = cells(0)
                        Dim frequency As Double = Double.Parse(cells(1), CultureInfo.InvariantCulture)
                        Dim spl90Db As Double = Double.Parse(cells(2), CultureInfo.InvariantCulture)
                        Dim spl100Db As Double = Double.Parse(cells(3), CultureInfo.InvariantCulture)
                        dataSource.Add(New HeadphoneComparisonPoint(name, frequency, spl90Db, spl100Db))
                    End While
                Catch
                    Throw New Exception("It's impossible to load " & FileName)
                End Try

                Return dataSource
            End Using
        End Function

        Public Class HeadphoneComparisonPoint

            Private _HeadphonesName As String, _Frequency As Double, _Spl90Db As Double, _Spl100Db As Double

            Public Property HeadphonesName As String
                Get
                    Return _HeadphonesName
                End Get

                Private Set(ByVal value As String)
                    _HeadphonesName = value
                End Set
            End Property

            Public Property Frequency As Double
                Get
                    Return _Frequency
                End Get

                Private Set(ByVal value As Double)
                    _Frequency = value
                End Set
            End Property

            Public Property Spl90Db As Double
                Get
                    Return _Spl90Db
                End Get

                Private Set(ByVal value As Double)
                    _Spl90Db = value
                End Set
            End Property

            Public Property Spl100Db As Double
                Get
                    Return _Spl100Db
                End Get

                Private Set(ByVal value As Double)
                    _Spl100Db = value
                End Set
            End Property

            Public Sub New(ByVal headphoneName As String, ByVal frequency As Double, ByVal spl90Db As Double, ByVal spl100Db As Double)
                HeadphonesName = headphoneName
                Me.Frequency = frequency
                Me.Spl90Db = spl90Db
                Me.Spl100Db = spl100Db
            End Sub
        End Class
    End Class
End Namespace
