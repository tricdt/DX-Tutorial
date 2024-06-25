Imports System

Namespace PivotGridDemo

    Public Enum MoonPhase
        NewMoon
        WaxingCrescentMoon
        FirstQuarterMoon
        WaxingGibbousMoon
        FullMoon
        WaningGibbousMoon
        LastQuarterMoon
        WaningCrescentMoon
    End Enum

    Public Module MoonPhaseCalculator

        Public Function CalculateMoonPhase(ByVal [date] As Date) As MoonPhase
            Dim year As Integer = [date].Year
            Dim month As Integer = [date].Month
            Dim day As Integer = [date].Day
            Dim moonCycle As Double = 29.53
            Dim daysInMonth As Double = 30.6
            Dim daysInYear As Double = 365.25
            Dim phasesCount As Integer = 8
            If month < 3 Then
                year -= 1
                month += 12
            End If

            month += 1
            Dim totalDaysElapsed As Double = daysInYear * year + daysInMonth * month + day - 694039.09
            Dim phase As Double = totalDaysElapsed / moonCycle
            Dim result As Integer = CInt(Math.Round(GetFracPart(phase) * phasesCount))
            Return MoonPhaseFromInt(result Mod phasesCount)
        End Function

        Private Function GetFracPart(ByVal value As Double) As Double
            Return value - Math.Floor(value)
        End Function

        Private Function MoonPhaseFromInt(ByVal phase As Integer) As MoonPhase
            Select Case phase
                Case 0
                    Return MoonPhase.NewMoon
                Case 1
                    Return MoonPhase.WaxingCrescentMoon
                Case 2
                    Return MoonPhase.FirstQuarterMoon
                Case 3
                    Return MoonPhase.WaxingGibbousMoon
                Case 4
                    Return MoonPhase.FullMoon
                Case 5
                    Return MoonPhase.WaningGibbousMoon
                Case 6
                    Return MoonPhase.LastQuarterMoon
                Case 7
                    Return MoonPhase.WaningCrescentMoon
                Case Else
                    Throw New ArgumentException("Phase must be between 0 and 7", "phase")
            End Select
        End Function
    End Module
End Namespace
