Imports System
Imports System.Runtime.InteropServices

Namespace ControlsDemo

    Public Module MoonCalculator

        Private arc As Double = 206264.8062

        Private Function Frac(ByVal x As Double) As Double
            x = x - Math.Floor(x)
            Return If(x < 0, x + 1, x)
        End Function

        Private Function CalculateSun(ByVal t As Double) As Double
            Dim m As Double = Math.PI * 2 * Frac(0.993133 + 99.997361 * t)
            Dim dl As Double = 6893.0 * Math.Sin(m) + 72.0 * Math.Sin(2 * m)
            Dim l As Double = Math.PI * 2 * Frac(0.7859453 + m / 2 / Math.PI + (6191.2 * t + dl) / 1296E3)
            Return l * 180 / Math.PI
        End Function

        Private Sub CalculateMoon(ByVal t As Double, <Out> ByRef l As Double, <Out> ByRef b As Double)
            Dim l0 As Double = Frac(0.606433 + 1336.855225 * t)
            Dim p2 As Double = Math.PI * 2
            l = p2 * Frac(0.374897 + 1325.552410 * t)
            Dim ls As Double = p2 * Frac(0.993133 + 99.997361 * t)
            Dim d As Double = p2 * Frac(0.827361 + 1236.853086 * t)
            Dim f As Double = p2 * Frac(0.259086 + 1342.227825 * t)
            Dim dl As Double = +22640 * Math.Sin(l) - 4586 * Math.Sin(l - 2 * d) + 2370 * Math.Sin(2 * d) + 769 * Math.Sin(2 * l) - 668 * Math.Sin(ls) - 412 * Math.Sin(2 * f) - 212 * Math.Sin(2 * l - 2 * d) - 206 * Math.Sin(l + ls - 2 * d) + 192 * Math.Sin(l + 2 * d) - 165 * Math.Sin(ls - 2 * d) - 125 * Math.Sin(d) - 110 * Math.Sin(l + ls) + 148 * Math.Sin(l - ls) - 55 * Math.Sin(2 * f - 2 * d)
            Dim s As Double = f + (dl + 412 * Math.Sin(2 * f) + 541 * Math.Sin(ls)) / arc
            Dim h As Double = f - 2 * d
            Dim n As Double = -526 * Math.Sin(h) + 44 * Math.Sin(l + h) - 31 * Math.Sin(-l + h) - 23 * Math.Sin(ls + h) + 11 * Math.Sin(-ls + h) - 25 * Math.Sin(-2 * l + f) + 21 * Math.Sin(-l + f)
            l = p2 * Frac(l0 + dl / 1296E3)
            l = l * 180.0 / Math.PI
            b =(18520.0 * Math.Sin(s) + n) / arc
            b = b * 180.0 / Math.PI
        End Sub

        Public Function GetPhase(ByVal currentDate As Date) As Double
            Dim t As Double =(currentDate - New DateTime(2000, 1, 1)).Duration().TotalDays / 36525
            Dim ls As Double = CalculateSun(t)
            Dim l, b As Double
            Call CalculateMoon(t, l, b)
            Dim cgam As Double = Math.Cos((ls - l) * Math.PI / 180) * Math.Cos(b * Math.PI / 180)
            Dim sgam As Double = Math.Sqrt(1.0 - cgam * cgam)
            Dim s As Double = Math.Atan2(sgam, 0.0025 - cgam) * 180.0 / Math.PI
            t = ls - l
            t = If(t < 0, t + 360, t)
            If t < 180 Then Return -Math.Pow(Math.Cos(s / 2 * Math.PI / 180), 2)
            Return Math.Pow(Math.Cos(s / 2 * Math.PI / 180), 2)
        End Function
    End Module
End Namespace
