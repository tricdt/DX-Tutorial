Imports DevExpress.Xpf.Core
Imports System.Windows

Namespace ControlsDemo

    Public Class SplashScreenMainViewModel

        Public Overridable Property SplashScreenType As PredefinedSplashScreenType

        Public Overridable Property StartupLocation As WindowStartupLocation

        Public Overridable Property TrackOwnerPosition As Boolean

        Public Overridable Property InputBlock As InputBlockMode

        Public Overridable Property Owner As SplashScreenManagerModule

        Public Overridable Property IsTrackOwnerPositionEnabled As Boolean

        Public Overridable Property ShowDelay As Integer

        Public Overridable Property MinDuration As Integer

        Public Sub New()
            SplashScreenType = PredefinedSplashScreenType.Fluent
            StartupLocation = WindowStartupLocation.CenterOwner
            InputBlock = InputBlockMode.WindowContent
        End Sub

        Protected Sub OnStartupLocationChanged()
            If StartupLocation = WindowStartupLocation.CenterOwner Then
                IsTrackOwnerPositionEnabled = True
            Else
                IsTrackOwnerPositionEnabled = False
                TrackOwnerPosition = False
            End If
        End Sub
    End Class
End Namespace
