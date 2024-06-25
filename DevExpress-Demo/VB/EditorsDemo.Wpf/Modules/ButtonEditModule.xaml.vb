Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Editors

Namespace EditorsDemo

    Public Partial Class ButtonEditModule
        Inherits EditorsDemoModule

        Private logQueue As Queue(Of String) = New Queue(Of String)()

        Const maxLogEntriesCount As Integer = 42

#Region "Sample 2"
        Private controlList As List(Of String) = New List(Of String)(New String() {"CheckEdit", "ProgressBarEdit", "TrackBarEdit", "ListBoxEdit", "TextEdit", "ButtonEdit", "ComboBoxEdit", "SpinEdit", "MemoEdit", "DateEdit"})

        Private selectedIndexField As Integer = 0

        Private Property SelectedIndex As Integer
            Get
                Return selectedIndexField
            End Get

            Set(ByVal value As Integer)
                UpdateSelectedIndex(value)
            End Set
        End Property

        Private Sub UpdateSelectedIndex(ByVal value As Integer)
            selectedIndexField = Math.Min(Math.Max(0, value), controlList.Count - 1)
            Update()
        End Sub

        Private Sub Update()
            UpdateValue()
            UpdateButtons()
        End Sub

        Private Sub UpdateValue()
            editor2.EditValue = controlList(SelectedIndex)
        End Sub

        Private Sub UpdateButtons()
            editor2.Buttons(0).IsEnabled = SelectedIndex > 0
            editor2.Buttons(1).IsEnabled = SelectedIndex < controlList.Count - 1
        End Sub

        Private Sub LeftButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SelectedIndex -= 1
        End Sub

        Private Sub RightButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SelectedIndex += 1
        End Sub

#End Region
        Public Sub New()
            InitializeComponent()
            DataContext = CreateButtonsSource()
            SubscribeButtons()
            Update()
        End Sub

        Private Function CreateButtonsSource() As List(Of ButtonsViewModel)
            Return New List(Of ButtonsViewModel)() From {New ButtonsViewModel() With {.ToolTip = "Button 3", .GlyphKind = GlyphKind.Redo, .Index = 3}, New ButtonsViewModel() With {.ToolTip = "Button 1", .GlyphKind = GlyphKind.Apply}, New ButtonsViewModel() With {.ToolTip = "Button 4", .GlyphKind = GlyphKind.Cancel, .Index = 4}, New ButtonsViewModel() With {.ToolTip = "Button 2", .GlyphKind = GlyphKind.Undo}, New ButtonsViewModel() With {.ToolTip = "Button 5", .GlyphKind = GlyphKind.Left, .IsLeft = True}}
        End Function

        Private Sub SubscribeButtons()
            SubscribeButtonEdit(editor1)
            SubscribeButtonEdit(editor2)
            SubscribeButtonEdit(editor3)
        End Sub

        Private Sub SubscribeButtonEdit(ByVal edit As ButtonEdit)
            For Each info As ButtonInfoBase In edit.Buttons
                AddHandler CType(info, CommonButtonInfo).Click, AddressOf OnButtonClick
            Next
        End Sub

        Private Sub OnButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim buttonInfo = TryCast(sender, CommonButtonInfo)
            Dim senderName As String = If(buttonInfo IsNot Nothing, buttonInfo.Name, CType(CType(sender, FrameworkElement).DataContext, CommonButtonInfo).Name)
            Me.AddToLog("ButtonClick: " & senderName & Microsoft.VisualBasic.Constants.vbLf)
        End Sub

        Private Sub AddToLog(ByVal meassage As String)
            EnqueueLogMessage(meassage)
            RepopulateLog()
            ScrollLogToEnd()
        End Sub

        Private Sub EnqueueLogMessage(ByVal text As String)
            logQueue.Enqueue(text)
            If logQueue.Count > maxLogEntriesCount Then logQueue.Dequeue()
        End Sub

        Private Sub RepopulateLog()
            log.Clear()
            Dim builder As StringBuilder = New StringBuilder()
            For Each logText As String In logQueue
                builder.Append(logText)
            Next

            log.Text = builder.ToString()
        End Sub

        Private Function IsScrollViewer(ByVal element As FrameworkElement) As Boolean
            Return TypeOf element Is ScrollViewer
        End Function

        Private Sub ScrollLogToEnd()
            Dim sv As ScrollViewer = CType(LayoutHelper.FindElement(log, New Predicate(Of FrameworkElement)(AddressOf IsScrollViewer)), ScrollViewer)
            If sv Is Nothing Then Return
            sv.ScrollToVerticalOffset(sv.ScrollableHeight)
        End Sub

        Private Sub ClearButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            logQueue.Clear()
            log.Clear()
        End Sub
    End Class

    <POCOViewModel>
    Public Class ButtonsViewModel
        Inherits ViewModelBase

        Public Property GlyphKind As GlyphKind

        Public Property ToolTip As String

        Public Property Index As Integer

        Public Property IsLeft As Boolean
    End Class
End Namespace
