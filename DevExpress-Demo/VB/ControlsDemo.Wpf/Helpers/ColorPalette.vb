Imports System
Imports System.Collections.Generic
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media

Namespace ControlsDemo.Helpers

    Public Class ColorPalette

        Private ReadOnly Shared Palette As System.Collections.Generic.List(Of System.Windows.Media.Color)

        Shared Sub New()
            ControlsDemo.Helpers.ColorPalette.Palette = New System.Collections.Generic.List(Of System.Windows.Media.Color)() From {CType(System.Windows.Media.ColorConverter.ConvertFromString("#4668a5"), System.Windows.Media.Color), CType(System.Windows.Media.ColorConverter.ConvertFromString("#a54671"), System.Windows.Media.Color), CType(System.Windows.Media.ColorConverter.ConvertFromString("#49a4be"), System.Windows.Media.Color), CType(System.Windows.Media.ColorConverter.ConvertFromString("#469ea5"), System.Windows.Media.Color), CType(System.Windows.Media.ColorConverter.ConvertFromString("#5848a5"), System.Windows.Media.Color), CType(System.Windows.Media.ColorConverter.ConvertFromString("#9462ae"), System.Windows.Media.Color), CType(System.Windows.Media.ColorConverter.ConvertFromString("#fcc653"), System.Windows.Media.Color)}
        End Sub

        Public Shared Function GetColor(ByVal number As Integer) As Color
            If number >= ControlsDemo.Helpers.ColorPalette.Palette.Count Then
                Dim coef As Integer = number \ (ControlsDemo.Helpers.ColorPalette.Palette.Count - 1)
                number -= coef * (ControlsDemo.Helpers.ColorPalette.Palette.Count - 1)
            End If

            Return ControlsDemo.Helpers.ColorPalette.Palette(CInt(System.Math.Max(0, number)))
        End Function

        Public Shared Function GetColor(ByVal number As Integer, ByVal opacity As Byte) As Color
            Dim res = ControlsDemo.Helpers.ColorPalette.GetColor(number)
            res.A = opacity
            Return res
        End Function
    End Class

    Public Class ColorPaletteConverter
        Implements System.Windows.Data.IValueConverter

        Public Property Opacity As Byte

        Public Sub New()
            Me.Opacity = 255
        End Sub

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            If TypeOf value Is Integer Then Return ControlsDemo.Helpers.ColorPalette.GetColor(CInt(value), Me.Opacity)
            Return System.Windows.Media.Colors.Transparent
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotImplementedException()
        End Function
    End Class

    Public Class ColorPaletteConverterExtension
        Inherits System.Windows.Markup.MarkupExtension

        Public Property Opacity As Byte

        Public Sub New()
            Me.Opacity = 255
        End Sub

        Public Overrides Function ProvideValue(ByVal serviceProvider As System.IServiceProvider) As Object
            Return New ControlsDemo.Helpers.ColorPaletteConverter() With {.Opacity = Me.Opacity}
        End Function
    End Class
End Namespace
