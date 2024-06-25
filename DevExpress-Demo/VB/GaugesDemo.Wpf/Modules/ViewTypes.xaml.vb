Imports System
Imports System.Windows.Threading

Namespace GaugesDemo

    Public Partial Class ViewTypes
        Inherits GaugesDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class CurrentTimeViewModel

        Private ReadOnly timer As DispatcherTimer = New DispatcherTimer()

        Public Overridable Property CurrentTime As String

        Protected Sub New()
            UpdateTime()
            timer.Interval = TimeSpan.FromSeconds(1)
        End Sub

        Private Sub OnTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            UpdateTime()
        End Sub

        Private Sub UpdateTime()
            CurrentTime = String.Format("{0:H:mm:ss}", Date.Now)
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
