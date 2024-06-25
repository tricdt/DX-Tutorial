Imports System

Namespace ControlsDemo

    Public Module SunCalculatior

        ' Algorithm source:
        ' Almanac for Computers, 1990
        ' published by Nautical Almanac Office
        ' United States Naval Observatory
        ' Washington, DC 20392
        Const Zenith As Double = 90.83

        Public Function SunRise(ByVal [date] As Date, ByVal utcOffset As Double, ByVal lat As Double, ByVal lon As Double) As Date?
            Return Calculate([date], utcOffset, lat, lon, True)
        End Function

        Public Function SunSet(ByVal [date] As Date, ByVal utcOffset As Double, ByVal lat As Double, ByVal lon As Double) As Date?
            Return Calculate([date], utcOffset, lat, lon, False)
        End Function

        Private Function Calculate(ByVal [date] As Date, ByVal utcOffset As Double, ByVal lat As Double, ByVal lon As Double, ByVal rise As Boolean) As Date?
            Dim N As Integer = [date].DayOfYear
            Dim lonHour As Double = lon / 15.0
            Dim t As Double = If(rise, N + (6.0 - lonHour) / 24.0, N + (18.0 - lonHour) / 24.0)
            Dim M As Double = 0.9856 * t - 3.289
            Dim L As Double = M + (1.916 * Math.Sin(DegToRad(M))) + (0.020 * Math.Sin(DegToRad(2.0 * M))) + 282.634
            L = ToRange(L, 0, 360)
            Dim RA As Double = RadToDeg(Math.Atan(0.91764 * Math.Tan(DegToRad(L))))
            RA = ToRange(RA, 0, 360)
            Dim Lquadrant As Double = Math.Floor(L / 90.0) * 90.0
            Dim RAquadrant As Double = Math.Floor(RA / 90.0) * 90.0
            RA = RA + (Lquadrant - RAquadrant)
            RA = RA / 15.0
            Dim sinDec As Double = 0.39782 * Math.Sin(DegToRad(L))
            Dim cosDec As Double = Math.Cos(Math.Asin(sinDec))
            Dim cosH As Double =(Math.Cos(DegToRad(Zenith)) - (sinDec * Math.Sin(DegToRad(lat)))) / (cosDec * Math.Cos(DegToRad(lat)))
            If cosH > 1.0 OrElse cosH < -1.0 Then Return Nothing
            Dim H As Double = If(rise, 360.0 - RadToDeg(Math.Acos(cosH)), RadToDeg(Math.Acos(cosH)))
            H = H / 15.0
            Dim _T As Double = H + RA - 0.06571 * t - 6.622
            Dim UT As Double = _T - lonHour
            UT += utcOffset
            UT = ToRange(UT, 0, 24)
            Return [date].Date.AddSeconds(Math.Round(UT * 3600.0))
        End Function

        Private Function DegToRad(ByVal angle As Double) As Double
            Return angle * Math.PI / 180.0
        End Function

        Private Function RadToDeg(ByVal angle As Double) As Double
            Return angle * 180.0 / Math.PI
        End Function

        Private Function ToRange(ByVal value As Double, ByVal min As Double, ByVal max As Double) As Double
            Dim range As Double = max - min
            While value < min
                value += range
            End While

            While value >= max
                value -= range
            End While

            Return value
        End Function
    End Module
End Namespace
