Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Xml.Serialization

Namespace DevExpress.Diagram.Demos

    Public Class TournamentViewModel

        Public Sub New()
            Dim originalGames As List(Of Game) = New List(Of Game)()
            For i As Integer = 0 To TournamentsData.Teams.Count - 1 - 1 Step 2
                Dim first = TournamentsData.Teams(i)
                Dim second = TournamentsData.Teams(i + 1)
                originalGames.Add(gameFactory.Invoke(CreateId(first, second), first, second))
            Next

            GenerateTable(originalGames)
        End Sub

        Shared Sub New()
            gameFactory = Mvvm.POCO.ViewModelSource.Factory(Of Integer, TeamData, TeamData, Game)(Function(id, first, second) New Game(id, first, second))
        End Sub

        Private Shared gameFactory As Func(Of Integer, TeamData, TeamData, Game)

        Private ReadOnly gamesField As List(Of Game) = New List(Of Game)()

        Private ReadOnly gamePairs As List(Of GamePair) = New List(Of GamePair)()

        Private ReadOnly relationshipsField As List(Of TournamentRelationInfo) = New List(Of TournamentRelationInfo)()

        Private Sub GenerateTable(ByVal games As List(Of Game))
            If Not games.Any() Then Return
            gamesField.AddRange(games)
            If games.Count = 1 Then Return
            Dim newGamePairs As List(Of GamePair) = New List(Of GamePair)()
            Dim gamePairFactory = Mvvm.POCO.ViewModelSource.Factory(Of Game, Game, Game, GamePair)(Function(f, s, child) New GamePair(f, s, child))
            For i As Integer = 0 To games.Count - 1 - 1 Step 2
                Dim first = games(i)
                Dim second = games(i + 1)
                Dim gamePair = gamePairFactory.Invoke(first, second, gameFactory.Invoke(CreateId(first, second), Nothing, Nothing))
                newGamePairs.Add(gamePair)
                relationshipsField.Add(New TournamentRelationInfo(gamePair.Result, first))
                relationshipsField.Add(New TournamentRelationInfo(gamePair.Result, second))
            Next

            Dim newGames = newGamePairs.[Select](Function(game) game.Result).ToList()
            gamePairs.AddRange(newGamePairs)
            GenerateTable(newGames)
        End Sub

        Private Shared Function CreateId(ByVal first As IID, ByVal second As IID) As Integer
            Return first.Id * 100 + second.Id * 10
        End Function

        Public ReadOnly Property Games As IEnumerable(Of Game)
            Get
                Return gamesField
            End Get
        End Property

        Public ReadOnly Property Relationships As IEnumerable(Of TournamentRelationInfo)
            Get
                Return relationshipsField
            End Get
        End Property
    End Class

    Public MustInherit Class DataPair(Of T As Class)
        Implements IID

        Protected Sub New(ByVal id As Integer, ByVal first As T, ByVal second As T, ByVal result As T)
            Me.Id = id
            Me.Result = result
            Me.First = first
            Me.Second = second
        End Sub

        Public Property Id As Integer Implements IID.Id

        Public Overridable Property First As T

        Public Overridable Property Second As T

        Public Overridable Property Result As T

        Protected Sub OnFirstChanged(ByVal oldValue As T)
            If oldValue IsNot Nothing Then UnsubscribeEvents(oldValue)
            If First IsNot Nothing Then SubscribeEvents(First)
            UpdatedResult(First)
        End Sub

        Protected Sub OnSecondChanged(ByVal oldValue As T)
            If oldValue IsNot Nothing Then UnsubscribeEvents(oldValue)
            If Second IsNot Nothing Then SubscribeEvents(Second)
            UpdatedResult(Second)
        End Sub

        Private Sub SubscribeEvents(ByVal data As T)
            Dim notifyObject = TryCast(data, INotifyPropertyChanged)
            If notifyObject Is Nothing Then Return
            AddHandler notifyObject.PropertyChanged, AddressOf NotifyObject_PropertyChanged
        End Sub

        Private Sub UnsubscribeEvents(ByVal data As T)
            Dim notifyObject = TryCast(data, INotifyPropertyChanged)
            If notifyObject Is Nothing Then Return
            RemoveHandler notifyObject.PropertyChanged, AddressOf NotifyObject_PropertyChanged
        End Sub

        Private Sub NotifyObject_PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
            UpdatedResult(TryCast(sender, T))
        End Sub

        Protected Overridable Sub UpdatedResult(ByVal sender As T)
        End Sub
    End Class

    Public Class GamePair
        Inherits DataPair(Of Game)

        Public Sub New(ByVal first As Game, ByVal second As Game, ByVal child As Game)
            MyBase.New(0, first, second, child)
        End Sub

        Protected Overrides Sub UpdatedResult(ByVal pair As Game)
            MyBase.UpdatedResult(pair)
            If pair Is Nothing Then Return
            If pair Is First Then
                Result.First = pair.Result
                If pair.Result Is Nothing Then Result.Team1Score = Nothing
            Else
                Result.Second = pair.Result
                If pair.Result Is Nothing Then Result.Team2Score = Nothing
            End If
        End Sub
    End Class

    Public Class Game
        Inherits DataPair(Of TeamData)

        Public Sub New(ByVal id As Integer, ByVal first As TeamData, ByVal second As TeamData)
            MyBase.New(id, first, second, Nothing)
            Team1Score = GetTeamScoreString(first)
            Team2Score = GetTeamScoreString(second)
        End Sub

        Public Overridable Property Team1Score As String

        Public Overridable Property Team2Score As String

        Protected Sub OnTeam1ScoreChanged()
            UpdatedResult(First)
        End Sub

        Protected Sub OnTeam2ScoreChanged()
            UpdatedResult(Second)
        End Sub

        Private Shared Function GetTeamScoreString(ByVal data As TeamData) As String
            Return If(data Is Nothing, Nothing, data.Score)
        End Function

        Private Shared Function GetTeamScoreNumericValue(ByVal stringScore As String) As Integer?
            Dim tempScore As Integer = 0
            Return If(Equals(stringScore, Nothing) OrElse Not Integer.TryParse(stringScore, tempScore), Nothing, New Integer?(tempScore))
        End Function

        Protected Overrides Sub UpdatedResult(ByVal data As TeamData)
            MyBase.UpdatedResult(data)
            Dim team1Score = GetTeamScoreNumericValue(Me.Team1Score)
            Dim team2Score = GetTeamScoreNumericValue(Me.Team2Score)
            If Not team1Score.HasValue OrElse Not team2Score.HasValue OrElse team1Score.Value = team2Score.Value Then
                Result = Nothing
            Else
                Result = If(team1Score.Value > team2Score.Value, First, Second)
            End If
        End Sub
    End Class

    Public Interface IID

        Property Id As Integer

    End Interface

    Public Class TournamentRelationInfo

        Private _Source As Game, _Target As Game

        Public Sub New(ByVal source As Game, ByVal target As Game)
            Me.Source = source
            Me.Target = target
        End Sub

        Public Property Source As Game
            Get
                Return _Source
            End Get

            Private Set(ByVal value As Game)
                _Source = value
            End Set
        End Property

        Public Property Target As Game
            Get
                Return _Target
            End Get

            Private Set(ByVal value As Game)
                _Target = value
            End Set
        End Property
    End Class

    Public Class TeamData
        Implements IID

        Public Property Id As Integer Implements IID.Id

        Public Property Name As String

        Public Overridable Property Score As String

        Public Property ImageData As Byte()
    End Class

    <XmlRoot("Teams")>
    Public Class Teams
        Inherits List(Of TeamData)

    End Class

    Public Module TournamentsData

        Sub New()
            Using stream = GetDataStream("TournamentBracket.xml")
                Dim serializer = New XmlSerializer(GetType(Teams))
                Teams = CType(serializer.Deserialize(stream), Teams)
            End Using
        End Sub

        Public ReadOnly Teams As Teams
    End Module
End Namespace
