Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data
Imports DevExpress.Xpf.LayoutControl

Namespace LayoutControlDemo

    Public Partial Class pageTileLayoutControl
        Inherits LayoutControlDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Public ReadOnly Property Numbers As IEnumerable(Of Integer)
            Get
                Dim result = New List(Of Integer)()
                For i As Integer = 9 To 0 Step -1
                    result.Add(i)
                Next

                Return result
            End Get
        End Property
    End Class

    Public Class StringCollection
        Inherits List(Of String)

    End Class

    Public Class TimeSpanToSecondsConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return CType(value, TimeSpan).TotalSeconds
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return TimeSpan.FromSeconds(CDbl(value))
        End Function
    End Class

    Public Class ScalablePaddingConverter
        Implements IValueConverter

        Public Sub New()
            MinPadding = 35
        End Sub

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim controlHeight = CDbl(value)
            Dim desiredContentHeight As Double = 3 * Tile.LargeHeight + 2 * TileLayoutControl.DefaultItemSpace + 20
            Dim paddingY As Double = Math.Floor(Math.Max(0R, controlHeight - desiredContentHeight) / 2R)
            paddingY = Math.Max(MinPadding, Math.Min(paddingY, TileLayoutControl.DefaultPadding.Top))
            Dim relativePadding As Double =(paddingY - MinPadding) / (TileLayoutControl.DefaultPadding.Top - MinPadding)
            Dim paddingX As Double = Math.Floor(MinPadding + relativePadding * (TileLayoutControl.DefaultPadding.Left - MinPadding))
            Return New Thickness(paddingX, paddingY, paddingX, paddingY)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Public Property MinPadding As Double
    End Class
End Namespace
