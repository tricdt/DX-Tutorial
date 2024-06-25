Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Map
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Map

Namespace MapDemo

    <POCOViewModel>
    Public Class PuzzleViewModel

        Private _SolvedItems As IList(Of DevExpress.Xpf.Map.MapItem), _GenerateItemsCommand As ICommand

        Private puzzleGenerator As PuzzleLayoutGenerator

        Private isLoading As Boolean

        Private isEditing As Boolean

        Public Overridable Property Items As IList(Of MapItem)

        Public Property SolvedItems As IList(Of MapItem)
            Get
                Return _SolvedItems
            End Get

            Private Set(ByVal value As IList(Of MapItem))
                _SolvedItems = value
            End Set
        End Property

        Public Property GenerateItemsCommand As ICommand
            Get
                Return _GenerateItemsCommand
            End Get

            Private Set(ByVal value As ICommand)
                _GenerateItemsCommand = value
            End Set
        End Property

        Public Overridable Property AllowSaveActions As Boolean

        Public Overridable Property TranslateItemsCommand As ICommand

        Public Overridable Property ClearSavedActionsCommand As ICommand

        Public Overridable Property ActiveItems As ObservableCollection(Of MapItem)

        Public Overridable Property CoordPointToScreenPoint As Func(Of CoordPoint, Point)

        Public Overridable Property ZoomToRegion As ICommand

        Public Overridable Property ActiveItem As MapItem

        <DependsOnProperties("ActiveItem")>
        Public ReadOnly Property Name As String
            Get
                Return If(ActiveItem IsNot Nothing, ActiveItem.Attributes("NAME_LONG").Value.ToString(), String.Empty)
            End Get
        End Property

        <DependsOnProperties("ActiveItem")>
        Public ReadOnly Property Capital As String
            Get
                Return If(ActiveItem IsNot Nothing, ActiveItem.Attributes("CAPITAL").Value.ToString(), String.Empty)
            End Get
        End Property

        Public Sub New()
            GenerateItemsCommand = New DelegateCommand(AddressOf GenerateItems)
            SolvedItems = New ObservableCollection(Of MapItem)()
        End Sub

        Public Shared Function GetItemLocation(ByVal item As ISupportCoordPoints) As GeoPoint
            Return CType(item.Points(0), GeoPoint)
        End Function

        Private Sub GenerateItems()
            ActiveItem = Nothing
            SolvedItems.Clear()
            ZoomToRegion.Execute(New Object() {New GeoPoint(15, -180), New GeoPoint(-62, -45), 0.05})
            isLoading = True
            AllowSaveActions = False
            Dim pathInfos As IEnumerable(Of MapPathInfo) = puzzleGenerator.GeneratePathInfos()
            Dim items As ObservableCollection(Of MapItem) = New ObservableCollection(Of MapItem)()
            For Each pathInfo As MapPathInfo In pathInfos
                Dim copiedPath As MapPath = CType(pathInfo.Path.Clone(), MapPath)
                copiedPath.Attributes.Add(New MapItemAttribute() With {.Name = "RealLocation", .Type = GetType(GeoPoint), .Value = pathInfo.RealLocation})
                Dim delta As Vector = CoordPointToScreenPoint(pathInfo.GameCenter) - CoordPointToScreenPoint(pathInfo.RealCenter)
                ActiveItems = New ObservableCollection(Of MapItem)() From {copiedPath}
                TranslateItemsCommand.Execute(delta)
                items.Add(copiedPath)
            Next

            Me.Items = items
            AllowSaveActions = True
            isLoading = False
        End Sub

        Private Function CalculateDistance(ByVal point1 As Point, ByVal point2 As Point) As Double
            Dim dx As Double = point1.X - point2.X
            Dim dy As Double = point1.Y - point2.Y
            Return Math.Sqrt(dx * dx + dy * dy)
        End Function

        Private Sub ShowWinMessage()
            Dim result As MessageBoxResult = ThemedMessageBox.Show("You won!", "Well done! All countries are on their places! Restart game?", MessageBoxButton.YesNo)
            If result = MessageBoxResult.Yes Then GenerateItems()
        End Sub

        Private Sub MoveItemToSolveLayer(ByVal mapPath As MapPath)
            Items.Remove(mapPath)
            SolvedItems.Add(mapPath)
            ActiveItem = mapPath
            ActiveItems.Clear()
            If Items.Count = 0 Then ShowWinMessage()
        End Sub

        Public Sub OnItemsLoaded(ByVal items As IEnumerable(Of MapItem))
            puzzleGenerator = New PuzzleLayoutGenerator(items)
            GenerateItems()
        End Sub

        Public Sub OnItemEdited(ByVal items As IEnumerable(Of MapItem))
            If isLoading OrElse isEditing Then Return
            For Each item As MapPath In items
                Dim realLocation As GeoPoint = CType(item.Attributes("RealLocation").Value, GeoPoint)
                Dim currentLocation As GeoPoint = GetItemLocation(item)
                Dim desiredLocation As Point = CoordPointToScreenPoint(realLocation)
                Dim actualLocation As Point = CoordPointToScreenPoint(currentLocation)
                If CalculateDistance(desiredLocation, actualLocation) < 20 Then
                    item.CanMove = False
                    Dim delta As Vector = desiredLocation - actualLocation
                    ActiveItems = New ObservableCollection(Of MapItem)() From {item}
                    isEditing = True
                    TranslateItemsCommand.Execute(delta)
                    isEditing = False
                    MoveItemToSolveLayer(item)
                    ClearSavedActionsCommand.Execute(Nothing)
                End If
            Next
        End Sub
    End Class

    Public Class MapPathInfo

        Private ReadOnly pathField As MapPath

        Private ReadOnly realLocationField As GeoPoint

        Private ReadOnly realCenterField As GeoPoint

        Private ReadOnly gameCenterField As GeoPoint

        Private ReadOnly nameField As String

        Private ReadOnly captial As String

        Public ReadOnly Property Path As MapPath
            Get
                Return pathField
            End Get
        End Property

        Public ReadOnly Property RealLocation As GeoPoint
            Get
                Return realLocationField
            End Get
        End Property

        Public ReadOnly Property RealCenter As GeoPoint
            Get
                Return realCenterField
            End Get
        End Property

        Public ReadOnly Property GameCenter As GeoPoint
            Get
                Return gameCenterField
            End Get
        End Property

        Public ReadOnly Property Name As String
            Get
                Return nameField
            End Get
        End Property

        Public ReadOnly Property Capital As String
            Get
                Return captial
            End Get
        End Property

        Public Sub New(ByVal path As MapPath, ByVal realLocation As GeoPoint, ByVal realCenter As GeoPoint, ByVal gameCenter As GeoPoint)
            pathField = path
            realLocationField = realLocation
            realCenterField = realCenter
            gameCenterField = gameCenter
            nameField = path.Attributes("NAME_LONG").Value.ToString()
            captial = path.Attributes("CAPITAL").Value.ToString()
        End Sub
    End Class
End Namespace
