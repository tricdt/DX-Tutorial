Imports System
Imports System.Linq
Imports System.Windows.Media
Imports DevExpress.Xpf.Core
Imports System.Runtime.CompilerServices

Namespace ChartsDemo

    Friend Module ColorUtils

#Region "class ColorHSL"
        Private Class ColorHSL

            Const minLuminanceField As Single = 0.0F

            Const maxLuminanceField As Single = 1.0F

            Private ReadOnly _hue As Single

            Private ReadOnly _saturation As Single

            Private _luminance As Single

            Public Property Luminance As Single
                Get
                    Return _luminance
                End Get

                Set(ByVal value As Single)
                    _luminance = value
                End Set
            End Property

            Public ReadOnly Property MinLuminance As Single
                Get
                    Return Math.Min(minLuminanceField, Luminance * 0.9F)
                End Get
            End Property

            Public ReadOnly Property MaxLuminance As Single
                Get
                    Return Math.Max(maxLuminanceField, Luminance + (1.0F - Luminance) * 0.15F)
                End Get
            End Property

            Public Sub New(ByVal hue As Single, ByVal saturation As Single, ByVal luminance As Single)
                _hue = hue
                _saturation = saturation
                _luminance = luminance
            End Sub

            Private Function GetComponent(ByVal q As Single, ByVal p As Single, ByVal t As Single) As Byte
                Const oneDivSix As Single = 1.0F / 6.0F
                Const twoDivThree As Single = 2.0F / 3.0F
                While t < 0.0F
                    t += 1.0F
                End While

                While t > 1.0F
                    t -= 1.0F
                End While

                Dim result As Single
                If t < oneDivSix Then
                    result = p + (q - p) * 6.0F * t
                ElseIf t < 0.5F Then
                    result = q
                ElseIf t < twoDivThree Then
                    result = p + (q - p) * (twoDivThree - t) * 6.0F
                Else
                    result = p
                End If

                Return CByte(Math.Round(result * 255.0F))
            End Function

            Friend Function ToColor() As Color
                Const oneDivThree As Single = 1.0F / 3.0F
                Dim q As Single = If(_luminance < 0.5F, Luminance * (1.0F + _saturation), _luminance + _saturation - _luminance * _saturation)
                Dim p As Single = 2.0F * Luminance - q
                Dim hueScaled As Single = _hue / 360.0F
                Return Color.FromArgb(255, GetComponent(q, p, hueScaled + oneDivThree), GetComponent(q, p, hueScaled), GetComponent(q, p, hueScaled - oneDivThree))
            End Function
        End Class

#End Region
        Private darkThemeNames As String() = New String() {Theme.MetropolisDark.Name, Theme.TouchlineDark.Name, Theme.Office2016BlackSE.Name, Theme.Office2016Black.Name}

        Private Function ToColorHSL(ByVal color As Color) As ColorHSL
            Return New ColorHSL(color.GetHue(), color.GetSaturation(), color.GetBrightness())
        End Function

        Private Function MixChannel(ByVal fromValue As Byte, ByVal toValue As Byte, ByVal ratio As Double) As Byte
            Return CByte(fromValue * (1.0 - ratio) + toValue * ratio)
        End Function

        Friend Function ChangeColorLuminance(ByVal color As Color, ByVal ratio As Single) As Color
            Dim colorHSL As ColorHSL = ToColorHSL(color)
            colorHSL.Luminance = Math.Min(0.95F, ratio)
            Return colorHSL.ToColor()
        End Function

        Friend Function ColorizeSeaIceSeries(ByVal color As Color, ByVal seriesIndex As Integer, ByVal theme As String) As Color
            Dim ratio As Single
            Dim hundredth As Integer = seriesIndex \ 100
            If Not darkThemeNames.Contains(theme) Then
                ratio = If(seriesIndex <= 10, 0.2F + CSng(Math.Ceiling(seriesIndex / 2R)) * 0.09F, 0.7F + hundredth)
            Else
                ratio = If(seriesIndex <= 10, 1.0F - CSng(Math.Ceiling(seriesIndex / 2R)) * 0.09F, 0.5F - hundredth)
            End If

            Return ChangeColorLuminance(color, ratio)
        End Function

        Friend Function InterpolateColors(ByVal fromUnit As Color, ByVal toUnit As Color, ByVal ratio As Double) As Color
            Return Color.FromArgb(MixChannel(fromUnit.A, toUnit.A, ratio), MixChannel(fromUnit.R, toUnit.R, ratio), MixChannel(fromUnit.G, toUnit.G, ratio), MixChannel(fromUnit.B, toUnit.B, ratio))
        End Function
    End Module

    Public Module ColorExtensions

        <Extension()>
        Public Function GetHue(ByVal c As Color) As Single
            Return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B).GetHue()
        End Function

        <Extension()>
        Public Function GetBrightness(ByVal c As Color) As Single
            Return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B).GetBrightness()
        End Function

        <Extension()>
        Public Function GetSaturation(ByVal c As Color) As Single
            Return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B).GetSaturation()
        End Function
    End Module
End Namespace
