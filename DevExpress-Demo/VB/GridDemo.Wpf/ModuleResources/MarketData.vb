Imports DevExpress.Mvvm
Imports System

Namespace GridDemo

    Public Class MarketData
        Inherits BindableBase

        Private _Ticker As String, _Last As Double, _ChgPercent As Double, _Open As Double, _High As Double, _Low As Double, _DayVal As Double

        Private ReadOnly Shared rnd As Random = New Random()

        Const Max As Double = 950

        Const Min As Double = 350

        Public Sub New(ByVal name As String)
            Ticker = name
            Open = NextRnd() * (Max - Min) + Min
            DayVal = Open
            UpdateInternal(Open)
        End Sub

        Public Property Ticker As String
            Get
                Return _Ticker
            End Get

            Private Set(ByVal value As String)
                _Ticker = value
            End Set
        End Property

        Public Property Last As Double
            Get
                Return _Last
            End Get

            Private Set(ByVal value As Double)
                _Last = value
            End Set
        End Property

        Public Property ChgPercent As Double
            Get
                Return _ChgPercent
            End Get

            Private Set(ByVal value As Double)
                _ChgPercent = value
            End Set
        End Property

        Public Property Open As Double
            Get
                Return _Open
            End Get

            Private Set(ByVal value As Double)
                _Open = value
            End Set
        End Property

        Public Property High As Double
            Get
                Return _High
            End Get

            Private Set(ByVal value As Double)
                _High = value
            End Set
        End Property

        Public Property Low As Double
            Get
                Return _Low
            End Get

            Private Set(ByVal value As Double)
                _Low = value
            End Set
        End Property

        Public Property DayVal As Double
            Get
                Return _DayVal
            End Get

            Private Set(ByVal value As Double)
                _DayVal = value
            End Set
        End Property

        Public Sub Update()
            Dim value As Double = DayVal - (Max - Min) * 0.05 + NextRnd() * (Max - Min) * 0.1
            If value <= Min Then value = Min
            If value >= Max Then value = Max
            UpdateInternal(value)
        End Sub

        Private Sub UpdateInternal(ByVal value As Double)
            Last = DayVal
            DayVal = value
            ChgPercent =(DayVal - Last) * 100.0 / DayVal
            High = Math.Max(Open, Math.Max(DayVal, Last))
            Low = Math.Min(Open, Math.Min(DayVal, Last))
            RaisePropertyChanged("")
        End Sub

        Private Shared Function NextRnd() As Double
            Dim value As Double = 0
            For i As Integer = 0 To 5 - 1
                value += rnd.NextDouble()
            Next

            Return value / 5
        End Function
    End Class
End Namespace
