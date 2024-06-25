Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Windows
Imports System.Windows.Resources

Namespace ChartsDemo

    Friend Module SeaIceAreaDataReader

        Public Function ReadDataFromFile() As List(Of ChartsDemo.SeaIceAreaDataPoint)
            Dim dataSource As System.Collections.Generic.List(Of ChartsDemo.SeaIceAreaDataPoint) = New System.Collections.Generic.List(Of ChartsDemo.SeaIceAreaDataPoint)()
            Dim info As System.Windows.Resources.StreamResourceInfo = System.Windows.Application.GetResourceStream(New System.Uri("/ChartsDemo;component/Data/nsidc_global_nt_final_and_nrt.dat", System.UriKind.RelativeOrAbsolute))
            Using reader As System.IO.StreamReader = New System.IO.StreamReader(info.Stream)
                Try
                    While Not reader.EndOfStream
                        Dim line As String = reader.ReadLine()
                        If line(0) <> "1"c AndAlso line(0) <> "2"c Then Continue While
                        Dim cells As String() = line.Split(New String() {", "}, System.StringSplitOptions.None)
                        If Equals(cells(CInt((3))).Trim(), "nan") Then Continue While
                        Dim year As String = cells(CInt((0))).Split("-"c)(0)
                        Dim dayOfYear As Double = Double.Parse(cells(1), System.Globalization.CultureInfo.InvariantCulture)
                        Dim area As Double = Double.Parse(cells(3), System.Globalization.CultureInfo.InvariantCulture)
                        dataSource.Add(New ChartsDemo.SeaIceAreaDataPoint(System.Convert.ToDateTime(cells(0), System.Globalization.CultureInfo.InvariantCulture), year, dayOfYear, area))
                    End While
                Catch
                    Throw New System.Exception("It's impossible to load " & "nsidc_global_nt_final_and_nrt.dat")
                End Try
            End Using

            Return dataSource
        End Function
    End Module

    Public Class SeaIceAreaDataPoint

        Private _FullDate As DateTime, _Year As String, _DayOfYear As Double, _IceArea As Double

        Public Property FullDate As DateTime
            Get
                Return _FullDate
            End Get

            Private Set(ByVal value As DateTime)
                _FullDate = value
            End Set
        End Property

        Public Property Year As String
            Get
                Return _Year
            End Get

            Private Set(ByVal value As String)
                _Year = value
            End Set
        End Property

        Public Property DayOfYear As Double
            Get
                Return _DayOfYear
            End Get

            Private Set(ByVal value As Double)
                _DayOfYear = value
            End Set
        End Property

        Public Property IceArea As Double
            Get
                Return _IceArea
            End Get

            Private Set(ByVal value As Double)
                _IceArea = value
            End Set
        End Property

        Friend Sub New(ByVal fullDate As System.DateTime, ByVal year As String, ByVal dayOfYear As Double, ByVal iceArea As Double)
            Me.FullDate = fullDate
            Me.Year = year
            Me.DayOfYear = dayOfYear
            Me.IceArea = iceArea
        End Sub
    End Class
End Namespace
