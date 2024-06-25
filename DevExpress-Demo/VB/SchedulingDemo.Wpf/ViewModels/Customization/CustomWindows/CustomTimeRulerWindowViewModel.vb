Imports System
Imports DevExpress.Xpf.Scheduling
Imports DevExpress.Xpf.Scheduling.VisualData

Namespace SchedulingDemo.ViewModels

    Public Class CustomTimeRulerWindowViewModel
        Inherits TimeRulerWindowViewModel

        Private ReadOnly timeRuler As TimeRuler

        Public Sub New(ByVal timeRuler As TimeRuler, ByVal defaultTimeZone As TimeZoneInfo)
            MyBase.New(timeRuler, defaultTimeZone)
            Me.timeRuler = timeRuler
            ShowMinutes = Me.timeRuler.ShowMinutes
            AlwaysShowTimeDesignator = Me.timeRuler.AlwaysShowTimeDesignator
        End Sub

        Public Property ShowMinutes As Boolean
            Get
                Return GetProperty(Function() Me.ShowMinutes)
            End Get

            Set(ByVal value As Boolean)
                SetProperty(Function() ShowMinutes, value)
            End Set
        End Property

        Public Property AlwaysShowTimeDesignator As Boolean
            Get
                Return GetProperty(Function() Me.AlwaysShowTimeDesignator)
            End Get

            Set(ByVal value As Boolean)
                SetProperty(Function() AlwaysShowTimeDesignator, value)
            End Set
        End Property

        Protected Overrides Sub Save()
            MyBase.Save()
            timeRuler.ShowMinutes = ShowMinutes
            timeRuler.AlwaysShowTimeDesignator = AlwaysShowTimeDesignator
        End Sub
    End Class
End Namespace
