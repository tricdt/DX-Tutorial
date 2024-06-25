Imports DevExpress.Mvvm
Imports System
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media.Animation
Imports System.Windows.Threading

Namespace GaugesDemo

    Public Class ElectricityViewModel

        Private ReadOnly rand As Random = New Random()

        Private ReadOnly timer As DispatcherTimer = New DispatcherTimer()

        Public Overridable Property Voltage As Double

        Public Overridable Property Amperage As Double

        Public Overridable Property Power As Double

        Public ReadOnly Property MaxVoltage As Integer
            Get
                Return 10
            End Get
        End Property

        Public ReadOnly Property MaxAmperage As Integer
            Get
                Return 3
            End Get
        End Property

        Protected Sub New()
            timer.Interval = TimeSpan.FromSeconds(3)
        End Sub

        Private Sub Timer_Tick(ByVal source As Object, ByVal e As EventArgs)
            Voltage = rand.Next(1, MaxVoltage)
            Amperage = CDbl(rand.Next(3, MaxAmperage * 10)) / 10.0
            Power = Voltage * Amperage
        End Sub

        Public Sub Start()
            AddHandler timer.Tick, AddressOf Timer_Tick
            timer.Start()
        End Sub

        Public Sub [Stop]()
            timer.Stop()
            RemoveHandler timer.Tick, AddressOf Timer_Tick
        End Sub
    End Class

    <ContentProperty("EasingFunction")>
    Public Class EasingFunctionItem
        Inherits BindableBase

        Public Property EasingFunction As IEasingFunction

        Public Overrides Function ToString() As String
            Return If(EasingFunction IsNot Nothing, EasingFunction.GetType().Name, "Default")
        End Function
    End Class

    Public Class DoubleToTimeSpanConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

#Region "IValueConvector implementation"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return TimeSpan.FromSeconds(Math.Ceiling(CDbl(value)))
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
#End Region
    End Class
End Namespace
