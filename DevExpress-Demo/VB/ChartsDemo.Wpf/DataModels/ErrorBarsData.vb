Imports System.Collections.Generic

Namespace ChartsDemo

    Public Class ErrorBarDataItem

        Private _Argument As String, _Value As Integer, _Negative As Integer, _Positive As Integer

        Public Property Argument As String
            Get
                Return _Argument
            End Get

            Private Set(ByVal value As String)
                _Argument = value
            End Set
        End Property

        Public Property Value As Integer
            Get
                Return _Value
            End Get

            Private Set(ByVal value As Integer)
                _Value = value
            End Set
        End Property

        Public Property Negative As Integer
            Get
                Return _Negative
            End Get

            Private Set(ByVal value As Integer)
                _Negative = value
            End Set
        End Property

        Public Property Positive As Integer
            Get
                Return _Positive
            End Get

            Private Set(ByVal value As Integer)
                _Positive = value
            End Set
        End Property

        Public Sub New(ByVal argument As String, ByVal value As Integer, ByVal negative As Integer, ByVal positive As Integer)
            Me.Argument = argument
            Me.Value = value
            Me.Negative = negative
            Me.Positive = positive
        End Sub
    End Class

    Friend Class ErrorBarsData

        Public ReadOnly Property Data As List(Of ErrorBarDataItem)
            Get
                Return New List(Of ErrorBarDataItem)() From {New ErrorBarDataItem("A", 20, 5, 8), New ErrorBarDataItem("B", 50, 3, 5), New ErrorBarDataItem("C", 40, 20, 10), New ErrorBarDataItem("D", 22, 15, 5), New ErrorBarDataItem("E", 30, 5, 8), New ErrorBarDataItem("F", 45, 5, 4), New ErrorBarDataItem("G", 35, 5, 3), New ErrorBarDataItem("H", 28, 4, 2), New ErrorBarDataItem("I", 46, 6, 4), New ErrorBarDataItem("J", 27, 8, 20), New ErrorBarDataItem("K", 20, 5, 8), New ErrorBarDataItem("L", 50, 3, 5), New ErrorBarDataItem("M", 40, 20, 10), New ErrorBarDataItem("N", 22, 15, 5), New ErrorBarDataItem("O", 30, 5, 8), New ErrorBarDataItem("P", 45, 5, 2), New ErrorBarDataItem("Q", 35, 5, 5), New ErrorBarDataItem("R", 28, 4, 4), New ErrorBarDataItem("S", 46, 6, 5), New ErrorBarDataItem("T", 27, 8, 8)}
            End Get
        End Property
    End Class
End Namespace
