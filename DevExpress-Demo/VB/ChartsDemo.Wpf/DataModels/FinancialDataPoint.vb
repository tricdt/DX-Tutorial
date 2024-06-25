Imports System

Namespace ChartsDemo

    Public Class FinancialDataPoint

        Public Property DateTimeStamp As Date

        Public Property Low As Double

        Public Property High As Double

        Public Property Open As Double

        Public Property Close As Double

        Public Property Volume As Double

        Public ReadOnly Property IsEmpty As Boolean
            Get
                Return DateTimeStamp.Equals(New DateTime())
            End Get
        End Property

        Public Sub New()
        End Sub

        Public Sub New(ByVal [date] As Date, ByVal open As Double, ByVal high As Double, ByVal low As Double, ByVal close As Double, ByVal volume As Double)
            DateTimeStamp = [date]
            Me.Low = low
            Me.High = high
            Me.Open = open
            Me.Close = close
            Me.Volume = volume
        End Sub
    End Class
End Namespace
