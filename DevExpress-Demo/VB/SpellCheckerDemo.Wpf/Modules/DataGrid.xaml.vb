Imports System.Windows
Imports System
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.SpellChecker
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Core

Namespace SpellCheckerDemo

    Public Partial Class DataGrid
        Inherits SpellCheckerDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class GridControlSpellChecker
        Inherits DXSpellCheckerBase(Of GridControl)

        Private ReadOnly Property Grid As GridControl
            Get
                Return AssociatedObject
            End Get
        End Property

        Private stopChecking As Boolean

        Public Sub Check()
            stopChecking = False
            SubscribeToEvents()
            CheckCell(0, 0)
        End Sub

        Private Sub SubscribeToEvents()
            AddHandler SpellChecker.CheckCompleteFormShowing, New DevExpress.XtraSpellChecker.FormShowingEventHandler(AddressOf Checker_CheckCompleteFormShowing)
        End Sub

        Private Sub UnsubscribeFromEvents()
            RemoveHandler SpellChecker.CheckCompleteFormShowing, New DevExpress.XtraSpellChecker.FormShowingEventHandler(AddressOf Checker_CheckCompleteFormShowing)
        End Sub

        Private Sub Checker_CheckCompleteFormShowing(ByVal sender As Object, ByVal e As DevExpress.XtraSpellChecker.FormShowingEventArgs)
            e.Handled = True
        End Sub

        Private Sub CheckCell(ByVal rowIndex As Integer, ByVal columnIndex As Integer)
            Dim column As GridColumn = Grid.Columns(columnIndex)
            If column.IsGrouped Then Return
            Grid.CurrentColumn = column
            Grid.View.FocusedRowHandle = rowIndex
            Grid.View.ShowEditor()
            Grid.UpdateLayout()
            Dim activeEditor As BaseEdit = Grid.View.ActiveEditor
            If activeEditor Is Nothing OrElse Not SpellChecker.CanCheck(activeEditor) Then
                CheckNextCell()
            Else
                UnsubscribeFromEvents()
                AddHandler SpellChecker.CheckCompleteFormShowing, New DevExpress.XtraSpellChecker.FormShowingEventHandler(AddressOf Checker_CheckCompleteFormShowing)
                AddHandler SpellChecker.AfterCheck, AddressOf Checker_AfterCheck
                CheckActiveEditor(activeEditor)
            End If
        End Sub

        Private Sub CheckActiveEditor(ByVal activeEditor As BaseEdit)
            If activeEditor.IsLoaded Then
                SpellChecker.Check(activeEditor)
            Else
                AddHandler activeEditor.Loaded, AddressOf OnActiveEditorLoaded
            End If
        End Sub

        Private Sub OnActiveEditorLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim activeEditor As BaseEdit = CType(sender, BaseEdit)
            RemoveHandler activeEditor.Loaded, AddressOf OnActiveEditorLoaded
            SpellChecker.Check(activeEditor)
        End Sub

        Private Sub Checker_AfterCheck(ByVal sender As Object, ByVal e As EventArgs)
            RemoveHandler SpellChecker.AfterCheck, AddressOf Checker_AfterCheck
            RemoveHandler SpellChecker.CheckCompleteFormShowing, New DevExpress.XtraSpellChecker.FormShowingEventHandler(AddressOf Checker_CheckCompleteFormShowing)
            SubscribeToEvents()
            stopChecking = TryCast(e, DevExpress.XtraSpellChecker.AfterCheckEventArgs).Reason = DevExpress.XtraSpellChecker.StopCheckingReason.User
            CheckNextCell()
        End Sub

        Private Sub CheckNextCell()
            CheckNextCell(Grid.View.FocusedRowHandle, CType(Grid.CurrentColumn, GridColumn))
        End Sub

        Private Sub CheckNextCell(ByVal rowIndex As Integer, ByVal column As GridColumn)
            If stopChecking Then
                UnsubscribeFromEvents()
                Return
            End If

            Dim columnIndex As Integer = Grid.Columns.IndexOf(column)
            Dim nextColumnIndex As Integer = If(columnIndex = Grid.Columns.Count - 1, 0, columnIndex + 1)
            Dim nextRowIndex As Integer = If(nextColumnIndex = 0, rowIndex + 1, rowIndex)
            If Not Grid.IsValidRowHandle(nextRowIndex) Then
                UnsubscribeFromEvents()
                ThemedMessageBox.Show(Window.GetWindow(Grid), "Spelling", "Spelling check is complete", MessageBoxButton.OK, Nothing, MessageBoxImage.None)
            Else
                CheckCell(nextRowIndex, nextColumnIndex)
            End If
        End Sub
    End Class
End Namespace
