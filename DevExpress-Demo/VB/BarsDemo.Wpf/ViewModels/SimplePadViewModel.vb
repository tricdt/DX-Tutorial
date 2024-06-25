Imports System.Collections.Generic

Namespace BarsDemo

    Public Class SimplePadViewModel

#Region "properties"
        Private _FontSizes As IEnumerable(Of Double?)

        Public Property FontSizes As IEnumerable(Of Double?)
            Get
                Return _FontSizes
            End Get

            Protected Set(ByVal value As IEnumerable(Of Double?))
                _FontSizes = value
            End Set
        End Property

#End Region
        Public Sub New()
            FontSizes = New Double?() {3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 88, 96, 104, 112, 120, 128, 136, 144}
        End Sub
    End Class
End Namespace
