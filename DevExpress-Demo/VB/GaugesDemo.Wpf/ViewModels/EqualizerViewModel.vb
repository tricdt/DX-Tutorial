Imports System
Imports System.Windows.Threading

Namespace GaugesDemo

    Public Class EqualizerViewModel

        Public Overridable Property Frequency32 As Double

        Public Overridable Property Frequency64 As Double

        Public Overridable Property Frequency125 As Double

        Public Overridable Property Frequency250 As Double

        Public Overridable Property Frequency500 As Double

        Public Overridable Property Frequency1K As Double

        Public Overridable Property Frequency2K As Double

        Public Overridable Property Frequency4K As Double

        Public Overridable Property Frequency8K As Double

        Public Overridable Property Frequency16K As Double

        Const MaxValue As Integer = 100

        Private ReadOnly random As Random = New Random()

        Private ReadOnly timer As DispatcherTimer = New DispatcherTimer()

        Protected Sub New()
            timer.Interval = TimeSpan.FromMilliseconds(500)
            UpdateData()
        End Sub

        Private Sub UpdateData()
            Frequency32 = random.Next(MaxValue)
            Frequency64 = random.Next(MaxValue)
            Frequency125 = random.Next(MaxValue)
            Frequency250 = random.Next(MaxValue)
            Frequency500 = random.Next(MaxValue)
            Frequency1K = random.Next(MaxValue)
            Frequency2K = random.Next(MaxValue)
            Frequency4K = random.Next(MaxValue)
            Frequency8K = random.Next(MaxValue)
            Frequency16K = random.Next(MaxValue)
        End Sub

        Private Sub OnTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            UpdateData()
        End Sub

        Public Sub Start()
            AddHandler timer.Tick, AddressOf OnTimedEvent
            timer.Start()
        End Sub

        Public Sub [Stop]()
            timer.Stop()
            RemoveHandler timer.Tick, AddressOf OnTimedEvent
        End Sub
    End Class
End Namespace
