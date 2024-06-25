Imports DevExpress.Mvvm.POCO
Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Core.Native

Namespace RibbonDemo

    Public Class MVVMRibbonViewModel

        Public Overridable Property Text As String

        Public Overridable Property SelectedText As String

        Public Overridable Property SelectionStart As Integer

        Public Overridable Property SelectionLength As Integer

        Public Overridable Property Categories As ObservableCollection(Of CategoryModel)

        Public Overridable Property MenuItems As ObservableCollection(Of CommandModel)

        Public ReadOnly Property RibbonMergeingService As IRibbonMergeingService
            Get
                Return GetService(Of IRibbonMergeingService)()
            End Get
        End Property

        Public Sub New()
            Categories = New ObservableCollection(Of CategoryModel)()
            MenuItems = New ObservableCollection(Of CommandModel)()
            SelectedText = String.Empty
            Dim homePage As PageModel = ViewModelSource.Create(Function() New PageModel() With {.Name = "Home"})
            Dim category As CategoryModel = ViewModelSource.Create(Of CategoryModel)()
            Categories.Add(category)
            category.Pages.Add(homePage)
            InitializeClipboardGroup(homePage)
            InitializeAdditionGroup(homePage)
        End Sub

        Private Sub InitializeAdditionGroup(ByVal homePage As PageModel)
            Dim addingGroup As PageGroupModel = ViewModelSource.Create(Function() New PageGroupModel() With {.Name = "Addition", .Glyph = GlyphHelper.GetGlyph("SvgImages/Outlook Inspired/AddFile.svg")})
            homePage.Groups.Add(addingGroup)
            Dim addGroupCommand As MyGroupCommand = ViewModelSource.Create(Function() New MyGroupCommand() With {.Caption = "Add", .LargeGlyph = GlyphHelper.GetGlyph("SvgImages/Outlook Inspired/AddFile.svg"), .SmallGlyph = GlyphHelper.GetGlyph("SvgImages/Outlook Inspired/AddFile.svg")})
            Dim parentCommand As MyParentCommand = ViewModelSource.Create(Function() New MyParentCommand(Me, MyParentCommandType.CommandCreation) With {.Caption = "Add New Command", .LargeGlyph = GlyphHelper.GetGlyph("SvgImages/Outlook Inspired/AddFile.svg"), .SmallGlyph = GlyphHelper.GetGlyph("SvgImages/Outlook Inspired/AddFile.svg")})
            Dim parentGroup As MyParentCommand = ViewModelSource.Create(Function() New MyParentCommand(Me, MyParentCommandType.GroupCreation) With {.Caption = "Add New Group", .LargeGlyph = GlyphHelper.GetGlyph("SvgImages/Outlook Inspired/AddFile.svg"), .SmallGlyph = GlyphHelper.GetGlyph("SvgImages/Outlook Inspired/AddFile.svg")})
            Dim parentPage As MyParentCommand = ViewModelSource.Create(Function() New MyParentCommand(Me, MyParentCommandType.PageCreation) With {.Caption = "Add New Page", .LargeGlyph = GlyphHelper.GetGlyph("SvgImages/Outlook Inspired/AddFile.svg"), .SmallGlyph = GlyphHelper.GetGlyph("SvgImages/Outlook Inspired/AddFile.svg")})
            addGroupCommand.Commands.Add(parentCommand)
            addGroupCommand.Commands.Add(parentGroup)
            addGroupCommand.Commands.Add(parentPage)
            addingGroup.Commands.Add(addGroupCommand)
            addingGroup.Commands.Add(parentCommand)
            addingGroup.Commands.Add(parentGroup)
            addingGroup.Commands.Add(parentPage)
            MenuItems.Add(parentCommand)
            MenuItems.Add(parentGroup)
            MenuItems.Add(parentPage)
        End Sub

        Private Sub InitializeClipboardGroup(ByVal homePage As PageModel)
            Dim clipboardGroup As PageGroupModel = ViewModelSource.Create(Function() New PageGroupModel() With {.Name = "Clipboard", .Glyph = GlyphHelper.GetGlyph("SvgImages/Outlook Inspired/Paste.svg")})
            homePage.Groups.Add(clipboardGroup)
            Dim cutCommand As CommandModel = ViewModelSource.Create(Function() New CommandModel(AddressOf cutCommandExecuteFunc) With {.Caption = "Cut", .LargeGlyph = GlyphHelper.GetGlyph("SvgImages/Outlook Inspired/Cut.svg"), .SmallGlyph = GlyphHelper.GetGlyph("SvgImages/Outlook Inspired/Cut.svg")})
            Dim copyCommand As CommandModel = ViewModelSource.Create(Function() New CommandModel(AddressOf copyCommandExecuteFunc) With {.Caption = "Copy", .LargeGlyph = GlyphHelper.GetGlyph("SvgImages/Outlook Inspired/Copy.svg"), .SmallGlyph = GlyphHelper.GetGlyph("SvgImages/Outlook Inspired/Copy.svg")})
            Dim pasteCommand As CommandModel = ViewModelSource.Create(Function() New CommandModel(AddressOf pasteCommandExecuteFunc) With {.Caption = "Paste", .LargeGlyph = GlyphHelper.GetGlyph("SvgImages/Outlook Inspired/Paste.svg"), .SmallGlyph = GlyphHelper.GetGlyph("SvgImages/Outlook Inspired/Paste.svg")})
            Dim selectCommand As CommandModel = ViewModelSource.Create(Function() New CommandModel(AddressOf selectAllCommandExecuteFunc) With {.Caption = "Select All", .LargeGlyph = GlyphHelper.GetGlyph("SvgImages/PDF Viewer/SelectAll.svg"), .SmallGlyph = GlyphHelper.GetGlyph("SvgImages/PDF Viewer/SelectAll.svg")})
            Dim blankCommand As CommandModel = ViewModelSource.Create(Function() New CommandModel(AddressOf blankCommandExecuteFunc) With {.Caption = "Clear Page", .LargeGlyph = GlyphHelper.GetGlyph("SvgImages/Spreadsheet/ClearAll.svg"), .SmallGlyph = GlyphHelper.GetGlyph("SvgImages/Spreadsheet/ClearAll.svg")})
            clipboardGroup.Commands.Add(cutCommand)
            clipboardGroup.Commands.Add(copyCommand)
            clipboardGroup.Commands.Add(pasteCommand)
            clipboardGroup.Commands.Add(selectCommand)
            clipboardGroup.Commands.Add(blankCommand)
        End Sub

        Public Sub Clear()
            For Each cat As CategoryModel In Categories
                cat.Clear()
            Next

            Categories.Clear()
        End Sub

        Public Sub cutCommandExecuteFunc()
            DXClipboard.SetText(SelectedText)
            SelectedText = String.Empty
        End Sub

        Public Sub copyCommandExecuteFunc()
            DXClipboard.SetText(SelectedText)
        End Sub

        Public Sub pasteCommandExecuteFunc()
            SelectedText = DXClipboard.GetText()
            SelectionStart += SelectionLength
            SelectionLength = 0
        End Sub

        Public Sub selectAllCommandExecuteFunc()
            SelectionStart = 0
            SelectionLength = If(Equals(Text, Nothing), 0, Text.Count())
        End Sub

        Public Sub blankCommandExecuteFunc()
            Text = String.Empty
        End Sub
    End Class

    Public Class CategoryModel

        Public Overridable Property Name As String

        Public Overridable Property Pages As ObservableCollection(Of PageModel)

        Public Sub New()
            Pages = New ObservableCollection(Of PageModel)()
            Name = ""
        End Sub

        Public Sub Clear()
            For Each cat As PageModel In Pages
                cat.Clear()
            Next

            Pages.Clear()
        End Sub
    End Class

    <POCOViewModel>
    Public Class PageModel

        Public Sub New()
            Groups = New ObservableCollection(Of PageGroupModel)()
        End Sub

        Public Overridable Property Name As String

        Public Overridable Property Groups As ObservableCollection(Of PageGroupModel)

        Public Sub Clear()
            For Each cat As PageGroupModel In Groups
                cat.Clear()
            Next

            Groups.Clear()
        End Sub
    End Class
End Namespace
