Namespace GaugesDemo

    Public Class WeatherViewModel

        Const cloudyIndex As Integer = 0

        Const snowyIndex As Integer = 1

        Const rainyIndex As Integer = 2

        Const sunnyIndex As Integer = 3

        Friend Enum PressureState
            Low
            Normal
            High
            Undefined
        End Enum

        Friend Enum TemperatureState
            Low
            High
        End Enum

        Private pressure As PressureState = PressureState.Normal

        Private temperature As TemperatureState = TemperatureState.High

        Public Sub LowRangeIndicatorEnter()
            pressure = PressureState.Low
            UpdateWeatherState()
        End Sub

        Public Sub LowRangeIndicatorLeave(ByVal minValue As Double, ByVal value As Double)
            If value < minValue Then
                pressure = PressureState.Undefined
                UpdateWeatherState()
            End If
        End Sub

        Public Sub NormalRangeIndicatorEnter()
            pressure = PressureState.Normal
            UpdateWeatherState()
        End Sub

        Public Sub NormalRangeIndicatorLeave(ByVal minValue As Double, ByVal value As Double)
            pressure = If(value < minValue, PressureState.Low, PressureState.High)
            UpdateWeatherState()
        End Sub

        Public Sub HighRangeIndicatorEnter()
            pressure = PressureState.High
            UpdateWeatherState()
        End Sub

        Public Sub HighRangeIndicatorLeave(ByVal minValue As Double, ByVal value As Double)
            pressure = If(value < minValue, PressureState.Normal, PressureState.Undefined)
            UpdateWeatherState()
        End Sub

        Public Sub HighTemperatureIndicatorEnter()
            temperature = TemperatureState.High
            UpdateWeatherState()
        End Sub

        Public Sub HighTemperatureIndicatorLeave()
            temperature = TemperatureState.Low
            UpdateWeatherState()
        End Sub

        Private Function GetStateIndex() As Integer
            Select Case pressure
                Case PressureState.Low
                    Return If(temperature = TemperatureState.Low, snowyIndex, rainyIndex)
                Case PressureState.Normal
                    Return cloudyIndex
                Case PressureState.High
                    Return sunnyIndex
                Case Else
                    Return -1
            End Select
        End Function

        Private Sub UpdateWeatherState()
            StateIndex = GetStateIndex()
        End Sub

        Public Overridable Property StateIndex As Integer
    End Class
End Namespace
