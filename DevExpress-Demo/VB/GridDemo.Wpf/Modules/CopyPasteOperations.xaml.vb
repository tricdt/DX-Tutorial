Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media.Animation
Imports System.Windows.Threading

Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFileAttribute("ModuleResources/CopyPasteTemplates.xaml")>
    <DevExpress.Xpf.DemoBase.CodeFileAttribute("ModuleResources/CopyPasteClasses.(cs)")>
    Public Partial Class CopyPasteOperations
        Inherits GridDemo.GridDemoModule

        Public Shared ReadOnly PasteCompetedEvent As System.Windows.RoutedEvent

        Friend Property Counter As Integer

        Public Shared ReadOnly PastUnderFocusedRowProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly FocusedGridProperty As System.Windows.DependencyProperty

        Shared Sub New()
            GridDemo.CopyPasteOperations.PastUnderFocusedRowProperty = System.Windows.DependencyProperty.Register("PastUnderFocusedRow", GetType(Boolean), GetType(GridDemo.CopyPasteOperations), New System.Windows.PropertyMetadata(True))
            GridDemo.CopyPasteOperations.FocusedGridProperty = System.Windows.DependencyProperty.Register("FocusedGrid", GetType(GridDemo.FocusedGrid), GetType(GridDemo.CopyPasteOperations), New System.Windows.PropertyMetadata(GridDemo.FocusedGrid.None, Sub(d, e) CType(d, GridDemo.CopyPasteOperations).UpdateAnimationTarget()))
            GridDemo.CopyPasteOperations.PasteCompetedEvent = System.Windows.EventManager.RegisterRoutedEvent("PasteCompeted", System.Windows.RoutingStrategy.Direct, GetType(System.Windows.RoutedEventHandler), GetType(GridDemo.CopyPasteOperations))
        End Sub

        Public Custom Event PasteCompeted As RoutedEventHandler
            AddHandler(ByVal value As RoutedEventHandler)
                Me.[AddHandler](GridDemo.CopyPasteOperations.PasteCompetedEvent, value)
            End AddHandler

            RemoveHandler(ByVal value As RoutedEventHandler)
                Me.[RemoveHandler](GridDemo.CopyPasteOperations.PasteCompetedEvent, value)
            End RemoveHandler

            RaiseEvent(ByVal sender As System.Object, ByVal e As Global.System.Windows.RoutedEventArgs)
            End RaiseEvent
        End Event

        Public Property PastUnderFocusedRow As Boolean
            Get
                Return CBool(Me.GetValue(GridDemo.CopyPasteOperations.PastUnderFocusedRowProperty))
            End Get

            Set(ByVal value As Boolean)
                Me.SetValue(GridDemo.CopyPasteOperations.PastUnderFocusedRowProperty, value)
            End Set
        End Property

        Public Property FocusedGrid As FocusedGrid
            Get
                Return CType(Me.GetValue(GridDemo.CopyPasteOperations.FocusedGridProperty), GridDemo.FocusedGrid)
            End Get

            Set(ByVal value As FocusedGrid)
                Me.SetValue(GridDemo.CopyPasteOperations.FocusedGridProperty, value)
            End Set
        End Property

        Private animationElements As System.Collections.Generic.Dictionary(Of Integer, GridDemo.RowsAnimationElement) = New System.Collections.Generic.Dictionary(Of Integer, GridDemo.RowsAnimationElement)()

        Private firstList As System.ComponentModel.BindingList(Of GridDemo.CopyPasteOutlookData) = New System.ComponentModel.BindingList(Of GridDemo.CopyPasteOutlookData)()

        Private secondList As System.ComponentModel.BindingList(Of GridDemo.CopyPasteOutlookData) = New System.ComponentModel.BindingList(Of GridDemo.CopyPasteOutlookData)()

        Private GridDictionary As System.Collections.Generic.Dictionary(Of GridDemo.FocusedGrid, DevExpress.Xpf.Grid.GridControl) = New System.Collections.Generic.Dictionary(Of GridDemo.FocusedGrid, DevExpress.Xpf.Grid.GridControl)()

        Public Sub New()
            Me.Counter = 0
            Me.InitializeComponent()
            Me.OptionsDataContext = Me
            Me.DemoContentGrid.DataContext = Me
            Dim list = GridDemo.OutlookDataGenerator.CreateOutlookDataList(10)
            Dim objectForCopying As Object() = list.ToArray()
            For i As Integer = 0 To objectForCopying.Length - 1
                Me.firstList.Add(GridDemo.CopyPasteOutlookData.ConvertOutlookDataToCopyPasteOutlookData(CType(objectForCopying(i), GridDemo.OutlookData), Me))
            Next

            Me.firstGrid.ItemsSource = Me.firstList
            Me.secondGrid.ItemsSource = Me.secondList
            Me.allowCopyingtoClipboardCheckEdit.IsChecked = True
            Me.copyHeaderCheckEdit.IsChecked = True
            AddHandler Me.firstGrid.PreviewMouseDown, New System.Windows.Input.MouseButtonEventHandler(AddressOf Me.firstGrid_PreviewMouseDown)
            AddHandler Me.secondGrid.PreviewMouseDown, New System.Windows.Input.MouseButtonEventHandler(AddressOf Me.secondGrid_PreviewMouseDown)
            Me.GridDictionary.Add(GridDemo.FocusedGrid.First, Me.firstGrid)
            Me.GridDictionary.Add(GridDemo.FocusedGrid.Second, Me.secondGrid)
        End Sub

        Private Sub firstGrid_PreviewMouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
            Me.FocusedGrid = GridDemo.FocusedGrid.First
        End Sub

        Private Sub secondGrid_PreviewMouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
            Me.FocusedGrid = GridDemo.FocusedGrid.Second
        End Sub

        Friend Delegate Sub Action(ByVal view As DevExpress.Xpf.Grid.TableView, ByVal list As System.ComponentModel.BindingList(Of GridDemo.CopyPasteOutlookData))

        Private Function CompareForDeleteList(ByVal x As Integer, ByVal y As Integer) As Integer
            If x > y Then
                Return -1
            ElseIf x < y Then
                Return 1
            End If

            Return 0
        End Function

        Protected Overrides Sub HidePopupContent()
            If Me.secondGrid IsNot Nothing Then Me.secondGrid.View.HideColumnChooser()
            MyBase.HidePopupContent()
        End Sub

        Private Sub CopyingRows(ByVal view As DevExpress.Xpf.Grid.TableView, ByVal list As System.ComponentModel.BindingList(Of GridDemo.CopyPasteOutlookData))
            If view IsNot Nothing Then
                If view.Grid.SelectedItems.Count <> 0 Then
                    view.Grid.CopySelectedItemsToClipboard()
                ElseIf view.FocusedRowHandle <> DevExpress.Xpf.Grid.DataControlBase.InvalidRowHandle Then
                    view.Grid.CopyRowsToClipboard(New Integer() {view.FocusedRowHandle})
                End If
            Else
                Me.textEdit.Copy()
            End If
        End Sub

        Private Sub RemoveRow(ByVal view As DevExpress.Xpf.Grid.TableView, ByVal rowHandle As Integer)
            Me.RemoveAnimationElement(view.Grid.GetListIndexByRowHandle(rowHandle))
            view.DeleteRow(rowHandle)
        End Sub

        Private Sub DeleteRows(ByVal view As DevExpress.Xpf.Grid.TableView, ByVal list As System.ComponentModel.BindingList(Of GridDemo.CopyPasteOutlookData))
            If view IsNot Nothing Then
                If view.Grid.SelectedItems.Count <> 0 Then
                    view.Grid.BeginDataUpdate()
                    Dim selectedList As System.Collections.Generic.List(Of Integer) = New System.Collections.Generic.List(Of Integer)(view.Grid.GetSelectedRowHandles())
                    selectedList.Sort(New Global.System.Comparison(Of System.Int32)(AddressOf Me.CompareForDeleteList))
                    For Each row As Integer In selectedList
                        Me.RemoveRow(view, row)
                    Next

                    view.Grid.EndDataUpdate()
                ElseIf view.FocusedRowHandle <> DevExpress.Xpf.Grid.DataControlBase.InvalidRowHandle Then
                    Me.RemoveRow(view, view.FocusedRowHandle)
                End If
            Else
                Me.textEdit.Delete()
            End If
        End Sub

        Private Sub CutRows(ByVal view As DevExpress.Xpf.Grid.TableView, ByVal list As System.ComponentModel.BindingList(Of GridDemo.CopyPasteOutlookData))
            Me.CopyingRows(view, Nothing)
            Me.DeleteRows(view, Nothing)
        End Sub

        Private ReadOnly maxAnimationRowsField As Integer = 30

        Friend ReadOnly Property MaxAnimationRows As Integer
            Get
                Return Me.maxAnimationRowsField
            End Get
        End Property

        Friend Sub PasteRowsWithoutAnimation(ByRef positionNewRow As Integer, ByVal view As DevExpress.Xpf.Grid.GridViewBase, ByVal list As System.ComponentModel.BindingList(Of GridDemo.CopyPasteOutlookData), ByVal objectsForCopy As Object(), ByVal start As Integer, ByVal [end] As Integer)
            view.Grid.BeginDataUpdate()
            Me.PasteRowsWithAnimation(positionNewRow, view, list, objectsForCopy, start, [end])
            view.Grid.EndDataUpdate()
        End Sub

        Private Sub PasteRowsWithAnimation(ByRef positionNewRow As Integer, ByVal view As DevExpress.Xpf.Grid.GridViewBase, ByVal list As System.ComponentModel.BindingList(Of GridDemo.CopyPasteOutlookData), ByVal objectsForCopy As Object(), ByVal start As Integer, ByVal [end] As Integer)
            Dim insertToEndOfRows As Boolean = view.FocusedRowHandle = DevExpress.Xpf.Grid.GridControl.InvalidRowHandle
            For i As Integer = start To [end] - 1
                Dim obj As GridDemo.CopyPasteOutlookData = CType(objectsForCopy(i), GridDemo.CopyPasteOutlookData)
                obj.UniqueID = System.Threading.Interlocked.Increment(Me.Counter)
                If i = Me.maxAnimationRowsField - 1 Then
                    Me.pasteWithoutAnimation = New GridDemo.PasteHelper() With {.List = list, .ObjectsForCopy = objectsForCopy, .Owner = Me, .PositionNewRow = positionNewRow, .View = view}
                End If

                If Me.PastUnderFocusedRow AndAlso (list.Count <> 0) AndAlso Not insertToEndOfRows Then
                    list.Insert(positionNewRow, obj)
                    Me.positionsNewRowsList.Add(positionNewRow)
                    positionNewRow += 1
                Else
                    list.Add(obj)
                    Me.positionsNewRowsList.Add(list.Count - 1)
                End If
            Next
        End Sub

        Private positionsNewRowsList As System.Collections.Generic.List(Of Integer) = New System.Collections.Generic.List(Of Integer)()

        Private isPasting As Boolean = False

        Private pasteWithoutAnimation As GridDemo.PasteHelper = Nothing

        Private animationTarget As GridDemo.FocusedGrid = GridDemo.FocusedGrid.None

        Private Sub UpdateAnimationTarget()
            If Not Me.animation Then Me.animationTarget = Me.FocusedGrid
        End Sub

        Private Sub PasteRows(ByVal view As DevExpress.Xpf.Grid.GridViewBase, ByVal list As System.ComponentModel.BindingList(Of GridDemo.CopyPasteOutlookData))
            If view IsNot Nothing Then
                Dim arrayList As System.Collections.ArrayList = Nothing
                Dim dataObj As System.Windows.IDataObject = Me.GetClipboardDataObject()
                Dim format As String = GetType(System.Collections.ArrayList).FullName
                If dataObj IsNot Nothing AndAlso dataObj.GetDataPresent(format) Then
                    Me.positionsNewRowsList.Clear()
                    Me.pasteWithoutAnimation = Nothing
                    Me.animation = True
                    Me.StartPasteForCanCommands()
                    arrayList = TryCast(dataObj.GetData(format), System.Collections.ArrayList)
                    Dim objectsForCopy As Object() = arrayList.ToArray()
                    Dim positionNewRow As Integer = If(view.FocusedRowHandle = DevExpress.Xpf.Grid.DataControlBase.InvalidRowHandle, 0, view.Grid.GetListIndexByRowHandle(view.FocusedRowHandle) + 1)
                    If Me.maxAnimationRowsField > objectsForCopy.Length Then
                        Me.PasteRowsWithAnimation(positionNewRow, view, list, objectsForCopy, 0, objectsForCopy.Length)
                    Else
                        Me.PasteRowsWithAnimation(positionNewRow, view, list, objectsForCopy, 0, Me.maxAnimationRowsField)
                    End If

                    Me.Dispatcher.BeginInvoke(New System.Action(AddressOf Me.AnimationRowsOfPasted), System.Windows.Threading.DispatcherPriority.Background)
                End If
            Else
                Me.textEdit.Paste()
            End If
        End Sub

        Private Sub StartPasteForCanCommands()
            Me.Cursor = System.Windows.Input.Cursors.Wait
            Me.isPasting = True
            Call System.Windows.Input.CommandManager.InvalidateRequerySuggested()
        End Sub

        Friend Sub EndPasteForCanCommands()
            Me.Cursor = System.Windows.Input.Cursors.Arrow
            Me.isPasting = False
            Call System.Windows.Input.CommandManager.InvalidateRequerySuggested()
        End Sub

        Private Sub AnimationRowsOfPasted()
            For i As Integer = 0 To Me.positionsNewRowsList.Count - 1
                If i = (Me.positionsNewRowsList.Count - 1) Then
                    Me.StartAnimation(Me.positionsNewRowsList(i), Me.pasteWithoutAnimation)
                    If Me.pasteWithoutAnimation Is Nothing Then Me.EndPasteForCanCommands()
                Else
                    Me.StartAnimation(Me.positionsNewRowsList(i), Nothing)
                End If
            Next

            Me.animation = False
            Me.UpdateAnimationTarget()
        End Sub

        Private Sub StartAnimation(ByVal positionNewRow As Integer, ByVal pasteWithoutAnimation As GridDemo.PasteHelper)
            Dim addingStoryboard As System.Windows.Media.Animation.Storyboard = Me.GetStoryboard("newRowStoryboard")
            Dim colorStoryboard As System.Windows.Media.Animation.Storyboard = Me.GetStoryboard("newRowColorStoryboard")
            If pasteWithoutAnimation IsNot Nothing Then
                pasteWithoutAnimation.ColorStoryboard = colorStoryboard
                AddHandler colorStoryboard.Completed, New System.EventHandler(AddressOf pasteWithoutAnimation.ColorStoryboardCompleted)
            End If

            If Me.positionsNewRowsList(Me.positionsNewRowsList.Count - 1) = positionNewRow Then
                Dim pasteHelper As GridDemo.PasteCompetedHelper = New GridDemo.PasteCompetedHelper() With {.Owner = Me, .ColorStoryboard = colorStoryboard}
                AddHandler colorStoryboard.Completed, New System.EventHandler(AddressOf pasteHelper.ColorStoryboardCompleted)
            End If

            Me.StartStoryboard(addingStoryboard, positionNewRow, GridDemo.RowsAnimationElement.NewRowsProgressProperty)
            Me.StartStoryboard(colorStoryboard, positionNewRow, GridDemo.RowsAnimationElement.NewRowsColorProperty)
        End Sub

        Friend Sub RaisePasteCompetedEvent(ByVal e As System.Windows.RoutedEventArgs)
            Me.[RaiseEvent](e)
        End Sub

        Private Sub StartStoryboard(ByVal storyboard As System.Windows.Media.Animation.Storyboard, ByVal indexElement As Integer, ByVal [property] As System.Windows.DependencyProperty)
            Call System.Windows.Media.Animation.Storyboard.SetTargetProperty(storyboard, New System.Windows.PropertyPath([property].Name))
            storyboard.Begin(Me.GetAnimationElement(indexElement), System.Windows.Media.Animation.HandoffBehavior.SnapshotAndReplace)
        End Sub

        Private Function GetStoryboard(ByVal resourceKey As String) As Storyboard
            Return CType(Me.FindResource(CObj((resourceKey))), System.Windows.Media.Animation.Storyboard).Clone()
        End Function

        Private Sub CommandExecute(ByVal actionForCommand As GridDemo.CopyPasteOperations.Action)
            If Me.IsFocusedTextEdit() Then
                actionForCommand(Nothing, Nothing)
            ElseIf Me.FocusedGrid = GridDemo.FocusedGrid.First Then
                actionForCommand(CType(Me.firstGrid.View, DevExpress.Xpf.Grid.TableView), Me.firstList)
            ElseIf Me.FocusedGrid = GridDemo.FocusedGrid.Second Then
                actionForCommand(CType(Me.secondGrid.View, DevExpress.Xpf.Grid.TableView), Me.secondList)
            End If
        End Sub

        Private Function IsFocusedTextEdit() As Boolean
            Return If(((Me.textEdit IsNot Nothing) AndAlso (Me.textEdit.IsKeyboardFocusWithin)), True, False)
        End Function

        Private Function IsSelectRows(ByVal view As DevExpress.Xpf.Grid.TableView) As Boolean
            Return If(((view.Grid.SelectedItems.Count <> 0) OrElse (view.FocusedRowHandle <> DevExpress.Xpf.Grid.DataControlBase.InvalidRowHandle)), True, False)
        End Function

        Private Function CanExecuteOutputCommands() As Boolean
            If Me.IsFocusedTextEdit() Then
                Return(Me.textEdit.SelectionLength <> 0)
            ElseIf Me.FocusedGrid <> GridDemo.FocusedGrid.None Then
                Return Me.IsSelectRows(CType(Me.GridDictionary(CType((Me.FocusedGrid), GridDemo.FocusedGrid)).View, DevExpress.Xpf.Grid.TableView))
            End If

            Return False
        End Function

        Private Function CanExecuteInputCommands() As Boolean
            If Me.isPasting Then Return False
            If Me.IsFocusedTextEdit() Then
                Dim dataObject As System.Windows.IDataObject = System.Windows.Clipboard.GetDataObject()
                Dim text As String = If(dataObject IsNot Nothing, CType(dataObject, System.Windows.DataObject).GetText(), Nothing)
                If Not String.IsNullOrEmpty(text) Then Return True
            End If

            If Me.FocusedGrid = GridDemo.FocusedGrid.None Then Return False
            Dim dataObj As System.Windows.IDataObject = Me.GetClipboardDataObject()
            Dim format As String = GetType(System.Collections.ArrayList).FullName
            If dataObj IsNot Nothing AndAlso dataObj.GetDataPresent(format) Then Return True
            Return False
        End Function

        Private Function CanCopyCommands() As Boolean
            If Me.IsFocusedTextEdit() Then
                Return(Me.textEdit.SelectionLength <> 0)
            ElseIf Me.FocusedGrid <> GridDemo.FocusedGrid.None Then
                Return(If(Me.GridDictionary(CType((Me.FocusedGrid), GridDemo.FocusedGrid)).ClipboardCopyMode <> DevExpress.Xpf.Grid.ClipboardCopyMode.None, True, False))
            End If

            Return False
        End Function

        Private Function GetClipboardDataObject() As IDataObject
            Try
                Return System.Windows.Clipboard.GetDataObject()
            Catch
                Return Nothing
            End Try
        End Function

        Private Sub CopyCommandBinding_Executed(ByVal sender As Object, ByVal e As System.Windows.Input.ExecutedRoutedEventArgs)
            Me.CommandExecute(New Global.GridDemo.CopyPasteOperations.Action(AddressOf Me.CopyingRows))
        End Sub

        Private Sub CutCommandBinding_Executed(ByVal sender As Object, ByVal e As System.Windows.Input.ExecutedRoutedEventArgs)
            Me.CommandExecute(New Global.GridDemo.CopyPasteOperations.Action(AddressOf Me.CutRows))
        End Sub

        Private Sub PasteCommandBinding_Executed(ByVal sender As Object, ByVal e As System.Windows.Input.ExecutedRoutedEventArgs)
            Me.CommandExecute(New Global.GridDemo.CopyPasteOperations.Action(AddressOf Me.PasteRows))
        End Sub

        Private Sub DeleteCommandBinding_Executed(ByVal sender As Object, ByVal e As System.Windows.Input.ExecutedRoutedEventArgs)
            Me.CommandExecute(New Global.GridDemo.CopyPasteOperations.Action(AddressOf Me.DeleteRows))
        End Sub

        Private Sub CopyCommandBinding_CanExecute(ByVal sender As Object, ByVal e As System.Windows.Input.CanExecuteRoutedEventArgs)
            e.CanExecute = Me.CanExecuteOutputCommands()
            If e.CanExecute Then e.CanExecute = Me.CanCopyCommands()
            e.Handled = True
        End Sub

        Private Sub CutCommandBinding_CanExecute(ByVal sender As Object, ByVal e As System.Windows.Input.CanExecuteRoutedEventArgs)
            e.CanExecute = Me.CanExecuteOutputCommands()
            If e.CanExecute Then e.CanExecute = Me.CanCopyCommands()
            e.Handled = True
        End Sub

        Private Sub PasteCommandBinding_CanExecute(ByVal sender As Object, ByVal e As System.Windows.Input.CanExecuteRoutedEventArgs)
            e.CanExecute = Me.CanExecuteInputCommands()
            e.Handled = True
        End Sub

        Private Sub DeleteCommandBinding_CanExecute(ByVal sender As Object, ByVal e As System.Windows.Input.CanExecuteRoutedEventArgs)
            e.CanExecute = Me.CanExecuteOutputCommands()
            e.Handled = True
        End Sub

        Private Sub Grid_CustomUnboundColumnData(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.GridColumnDataEventArgs)
            If e.Column IsNot Nothing AndAlso Equals(e.Column.FieldName, "AnimationElement") Then
                Dim index As Integer = Me.GetIndexForAnimationElements(e.ListSourceRowIndex, e.Source.Equals(Me.secondGrid))
                e.Value = Me.GetAnimationElementCore(index)
            End If
        End Sub

        Private Function GetIndexForAnimationElements(ByVal index As Integer) As Integer
            Return Me.GetIndexForAnimationElements(index, Me.animationTarget = GridDemo.FocusedGrid.Second)
        End Function

        Private Function GetIndexForAnimationElements(ByVal index As Integer, ByVal isSecondGrid As Boolean) As Integer
            Return If(isSecondGrid, Me.secondList(CInt((index))).UniqueID, Me.firstList(CInt((index))).UniqueID)
        End Function

        Private animation As Boolean = False

        Private Function GetAnimationElement(ByVal index As Integer) As RowsAnimationElement
            Return Me.GetAnimationElementCore(Me.GetIndexForAnimationElements(index))
        End Function

        Private Function GetAnimationElementCore(ByVal index As Integer) As RowsAnimationElement
            Dim result As GridDemo.RowsAnimationElement = Nothing
            If Not Me.animationElements.TryGetValue(index, result) Then
                result = New GridDemo.RowsAnimationElement()
                Me.animationElements.Add(index, result)
                If Me.animation Then result.NewRowsProgress = 0
            End If

            Return result
        End Function

        Private Sub RemoveAnimationElement(ByVal index As Integer)
            index = Me.GetIndexForAnimationElements(index)
            If Me.animationElements.ContainsKey(index) Then Me.animationElements.Remove(index)
        End Sub

        Private Sub allowCopyingtoClipboardCheckEdit_Checked(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.SetPropertyToGrids(DevExpress.Xpf.Grid.ClipboardCopyMode.[Default])
        End Sub

        Private Sub allowCopyingtoClipboardCheckEdit_Unchecked(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.SetPropertyToGrids(DevExpress.Xpf.Grid.ClipboardCopyMode.None)
        End Sub

        Private Sub copyHeaderCheckEdit_Checked(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.SetPropertyToGrids(DevExpress.Xpf.Grid.ClipboardCopyMode.IncludeHeader)
        End Sub

        Private Sub copyHeaderCheckEdit_Unchecked(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.SetPropertyToGrids(DevExpress.Xpf.Grid.ClipboardCopyMode.ExcludeHeader)
        End Sub

        Private Sub SetPropertyToGrids(ByVal copyMode As DevExpress.Xpf.Grid.ClipboardCopyMode)
            Me.firstGrid.ClipboardCopyMode = copyMode
            Me.secondGrid.ClipboardCopyMode = copyMode
        End Sub
    End Class
End Namespace
