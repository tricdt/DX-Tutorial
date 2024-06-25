Imports System
Imports System.Windows.Threading
Imports DevExpress.Xpf.Gauges

Namespace GaugesDemo

    Public Class PlayerViewModel

        Public Overridable Property PlayerText As String

        Public Overridable Property Time As Date

        Public Overridable Property IsReading As Boolean

        Public Overridable Property CreepingLineAnimationDirection As CreepingLineDirection

        Const rightToLeftRadioText As String = "RADIO            NOW PLAYING        WAAF FM BOSTON        107.3 MHZ"

        Const leftToRightRadioText As String = "107.3 MHZ         WAAF FM BOSTON        NOW PLAYING        RADIO"

        Const rightToLeftCDSourceInfo As String = "CD          NOW PLAYING              "

        Const leftToRightCDSourceInfo As String = "               NOW PLAYING              CD"

        Const rightToLeftTrackInfo As String = "          AT 320 KBPS         MP3/WMA"

        Const leftToRightTrackInfo As String = "MP3/WMA          AT 320 KBPS         "

        Private Shared rightToLeftTracks As String() = {"THE DARK SIDE OF THE MOON       PINK FLOYD", "SMOKE ON THE WATER       DEEP PURPLE", "BLACK MOUNTAIN SIDE       LED ZEPPELIN", "TRANSILVANIA       IRON MAIDEN", "HARD ROAD       BLACK SABBATH"}

        Private Shared leftToRightTracks As String() = {"PINK FLOYD       THE DARK SIDE OF THE MOON", "DEEP PURPLE       SMOKE ON THE WATER", "LED ZEPPELIN       BLACK MOUNTAIN SIDE", "IRON MAIDEN       TRANSILVANIA", "BLACK SABBATH       HARD ROAD"}

        Private isRadioPlaying As Boolean = True

        Private currentTrack As Integer

        Private ReadOnly timeTimer As DispatcherTimer = New DispatcherTimer()

        Private ReadOnly blinkingTimer As DispatcherTimer = New DispatcherTimer()

        Protected Sub New()
            timeTimer.Interval = TimeSpan.FromSeconds(1)
            blinkingTimer.Interval = TimeSpan.FromSeconds(4)
            CreepingLineAnimationDirection = CreepingLineDirection.RightToLeft
            AddHandler blinkingTimer.Tick, AddressOf OnBlinkingTimedEvent
            UpdateTime()
        End Sub

        Private Sub OnTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            UpdateTime()
        End Sub

        Private Sub OnBlinkingTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            blinkingTimer.Stop()
            IsReading = False
            ChangeText()
        End Sub

        Private Sub UpdateTime()
            Time = Date.Now
        End Sub

        Public Sub ChangeSource()
            IsReading = isRadioPlaying
            isRadioPlaying = Not isRadioPlaying
            If isRadioPlaying Then
                SetPlayerText(Function() leftToRightRadioText, Function() rightToLeftRadioText)
            Else
                PlayerText = "READING"
                blinkingTimer.Start()
            End If
        End Sub

        Private Sub ChangeText()
            If blinkingTimer.IsEnabled Then Return
            If isRadioPlaying Then
                SetPlayerText(Function() leftToRightRadioText, Function() rightToLeftRadioText)
            Else
                SetPlayerText(Function() leftToRightTrackInfo & leftToRightTracks(currentTrack) & leftToRightCDSourceInfo, Function() rightToLeftCDSourceInfo & rightToLeftTracks(currentTrack) & rightToLeftTrackInfo)
            End If
        End Sub

        Private Sub SetPlayerText(ByVal leftToRightText As Func(Of String), ByVal rightToLeftText As Func(Of String))
            PlayerText = If(CreepingLineAnimationDirection.Equals(CreepingLineDirection.LeftToRight), leftToRightText(), rightToLeftText())
        End Sub

        Protected Sub OnCreepingLineAnimationDirectionChanged()
            ChangeText()
        End Sub

        Public Sub SwitchNextTrack()
            If currentTrack < leftToRightTracks.Length - 1 AndAlso Not isRadioPlaying Then
                currentTrack += 1
                ChangeText()
            End If
        End Sub

        Public Sub SwitchPreviousTrack()
            If currentTrack > 0 AndAlso Not isRadioPlaying Then
                currentTrack -= 1
                ChangeText()
            End If
        End Sub

        Public Sub SwitchFirstTrack()
            If currentTrack <> 0 AndAlso Not isRadioPlaying Then
                currentTrack = 0
                ChangeText()
            End If
        End Sub

        Public Sub SwitchLastTrack()
            If currentTrack <> leftToRightTracks.Length - 1 AndAlso Not isRadioPlaying Then
                currentTrack = leftToRightTracks.Length - 1
                ChangeText()
            End If
        End Sub

        Public Sub Start()
            AddHandler timeTimer.Tick, AddressOf OnTimedEvent
            timeTimer.Start()
        End Sub

        Public Sub [Stop]()
            timeTimer.Stop()
            RemoveHandler timeTimer.Tick, AddressOf OnTimedEvent
            blinkingTimer.Stop()
            RemoveHandler blinkingTimer.Tick, AddressOf OnBlinkingTimedEvent
        End Sub
    End Class
End Namespace
