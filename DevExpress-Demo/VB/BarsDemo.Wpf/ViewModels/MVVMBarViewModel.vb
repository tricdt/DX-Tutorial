Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Core.Native

Namespace BarsDemo

    Public Class MVVMBarViewModel

        Public ReadOnly Property MessageBoxService As IMessageBoxService
            Get
                Return GetService(Of IMessageBoxService)()
            End Get
        End Property

        Public Sub New()
            Bars = New ObservableCollection(Of BarViewModel)()
            SelectedText = String.Empty
            Text = String.Empty
            InitializeClipboardBar()
            InitializeAdditionBar()
        End Sub

        Private Sub InitializeAdditionBar()
            Dim addingBar As BarViewModel = ViewModelSource.Create(Function() New BarViewModel() With {.Name = "Addition"})
            Bars.Add(addingBar)
            Dim addGroupCommand = ViewModelSource.Create(Function() New GroupBarButtonInfo() With {.Caption = "Add", .SmallGlyph = GetDXImage("SvgImages/Outlook Inspired/AddFile.svg"), .LargeGlyph = GetDXImage("SvgImages/Outlook Inspired/AddFile.svg")})
            Dim parentCommand = ViewModelSource.Create(Function() New ParentBarButtonInfo(Me, MyParentCommandType.CommandCreation) With {.Caption = "Add Command", .SmallGlyph = GetDXImage("SvgImages/Outlook Inspired/AddFile.svg"), .LargeGlyph = GetDXImage("SvgImages/Outlook Inspired/AddFile.svg")})
            Dim parentBar = ViewModelSource.Create(Function() New ParentBarButtonInfo(Me, MyParentCommandType.BarCreation) With {.Caption = "Add Bar", .SmallGlyph = GetDXImage("SvgImages/Outlook Inspired/AddFile.svg"), .LargeGlyph = GetDXImage("SvgImages/Outlook Inspired/AddFile.svg")})
            addGroupCommand.Commands.Add(parentCommand)
            addGroupCommand.Commands.Add(parentBar)
            addingBar.Commands.Add(addGroupCommand)
            addingBar.Commands.Add(parentCommand)
            addingBar.Commands.Add(parentBar)
        End Sub

        Private Sub InitializeClipboardBar()
            Dim clipboardBar As BarViewModel = ViewModelSource.Create(Function() New BarViewModel() With {.Name = "Clipboard"})
            Bars.Add(clipboardBar)
            Dim cutCommand = ViewModelSource.Create(Function() New BarButtonInfo(AddressOf cutCommandExecuteFunc) With {.Caption = "Cut", .SmallGlyph = GetDXImage("SvgImages/Outlook Inspired/Cut.svg"), .LargeGlyph = GetDXImage("SvgImages/Outlook Inspired/Cut.svg")})
            Dim copyCommand = ViewModelSource.Create(Function() New BarButtonInfo(AddressOf copyCommandExecuteFunc) With {.Caption = "Copy", .SmallGlyph = GetDXImage("SvgImages/Outlook Inspired/Copy.svg"), .LargeGlyph = GetDXImage("SvgImages/Outlook Inspired/Copy.svg")})
            Dim pasteCommand = ViewModelSource.Create(Function() New BarButtonInfo(AddressOf pasteCommandExecuteFunc) With {.Caption = "Paste", .SmallGlyph = GetDXImage("SvgImages/Outlook Inspired/Paste.svg"), .LargeGlyph = GetDXImage("SvgImages/Outlook Inspired/Paste.svg")})
            Dim selectCommand = ViewModelSource.Create(Function() New BarButtonInfo(AddressOf selectAllCommandExecuteFunc) With {.Caption = "Select All", .SmallGlyph = GetDXImage("SvgImages/PDF Viewer/SelectAll.svg"), .LargeGlyph = GetDXImage("SvgImages/PDF Viewer/SelectAll.svg")})
            Dim blankCommand = ViewModelSource.Create(Function() New BarButtonInfo(AddressOf blankCommandExecuteFunc) With {.Caption = "Clear Page", .SmallGlyph = GetDXImage("SvgImages/Icon Builder/Actions_Clear.svg"), .LargeGlyph = GetDXImage("SvgImages/Icon Builder/Actions_Clear.svg")})
            clipboardBar.Commands.Add(cutCommand)
            clipboardBar.Commands.Add(copyCommand)
            clipboardBar.Commands.Add(pasteCommand)
            clipboardBar.Commands.Add(selectCommand)
            clipboardBar.Commands.Add(blankCommand)
        End Sub

        Public Overridable Property Bars As ObservableCollection(Of BarViewModel)

        Public Overridable Property Text As String

        Public Overridable Property SelectedText As String

        Public Overridable Property SelectionStart As Integer

        Public Overridable Property SelectionLength As Integer

#Region "CommandFuncs"
        Public Sub cutCommandExecuteFunc()
            OnCopyExecute()
            SelectedText = String.Empty
        End Sub

        Public Sub copyCommandExecuteFunc()
            OnCopyExecute()
        End Sub

        Public Sub pasteCommandExecuteFunc()
            SelectedText = DXClipboard.GetText()
            SelectionStart += SelectionLength
            SelectionLength = 0
        End Sub

        Public Sub selectAllCommandExecuteFunc()
            SelectionStart = 0
            SelectionLength = If(String.IsNullOrEmpty(Text), 0, Text.Count())
        End Sub

        Public Sub blankCommandExecuteFunc()
            Text = String.Empty
        End Sub

#End Region
        Private Sub OnCopyExecute()
            DXClipboard.SetText(SelectedText)
        End Sub
    End Class

    <POCOViewModel>
    Public Class BarViewModel

        Public Sub New()
            Name = ""
            Commands = New ObservableCollection(Of BarButtonInfo)()
        End Sub

        Public Overridable Property Name As String

        Public Overridable Property Commands As ObservableCollection(Of BarButtonInfo)
    End Class
End Namespace
