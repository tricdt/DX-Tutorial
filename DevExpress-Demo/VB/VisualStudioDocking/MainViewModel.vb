Imports System
Imports System.Text.RegularExpressions
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Utils
Imports DevExpress.Utils.About
Imports DevExpress.Xpf
Imports DevExpress.Xpf.Accordion
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Bars.Native
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.DemoBase.Helpers.TextColorizer
Imports DevExpress.Xpf.Docking
Imports DevExpress.Xpf.PropertyGrid
Imports Microsoft.Win32

Namespace VisualStudioDocking.ViewModels

    Public Class DocumentViewModel
        Inherits PanelWorkspaceViewModel

        Private _ModelCodeLanguage As CodeLanguage, _CodeLanguageText As CodeLanguageText, _Description As String, _FilePath As String, _Footer As String

        Public Sub New()
            IsClosed = False
        End Sub

        Public Sub New(ByVal displayName As String, ByVal text As String)
            Me.New()
            Me.DisplayName = displayName
            CodeLanguageText = New CodeLanguageText(CodeLanguage.CS, text)
        End Sub

        Public Property ModelCodeLanguage As CodeLanguage
            Get
                Return _ModelCodeLanguage
            End Get

            Private Set(ByVal value As CodeLanguage)
                _ModelCodeLanguage = value
            End Set
        End Property

        Public Property CodeLanguageText As CodeLanguageText
            Get
                Return _CodeLanguageText
            End Get

            Private Set(ByVal value As CodeLanguageText)
                _CodeLanguageText = value
            End Set
        End Property

        Public Property Description As String
            Get
                Return _Description
            End Get

            Protected Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        Public Property FilePath As String
            Get
                Return _FilePath
            End Get

            Protected Set(ByVal value As String)
                _FilePath = value
            End Set
        End Property

        Public Property Footer As String
            Get
                Return _Footer
            End Get

            Protected Set(ByVal value As String)
                _Footer = value
            End Set
        End Property

        Protected Overrides ReadOnly Property WorkspaceName As String
            Get
                Return "DocumentHost"
            End Get
        End Property

        Public Function OpenFile() As Boolean
            Dim openFileDialog As OpenFileDialog = New OpenFileDialog()
            openFileDialog.Filter = "Visual C# Files (*.cs)|*.cs|XAML Files (*.xaml)|*.xaml"
            openFileDialog.FilterIndex = 1
            Dim dialogResult As Boolean? = openFileDialog.ShowDialog()
            Dim dialogResultOK As Boolean = dialogResult.HasValue AndAlso dialogResult.Value
            If dialogResultOK Then
                DisplayName = openFileDialog.SafeFileName
                FilePath = openFileDialog.FileName
                SetCodeLanguageProperties(Path.GetExtension(openFileDialog.SafeFileName))
                Dim fileStream As Stream = IO.File.OpenRead(openFileDialog.FileName)
                Using reader As StreamReader = New StreamReader(fileStream)
                    CodeLanguageText = New CodeLanguageText(ModelCodeLanguage, reader.ReadToEnd())
                End Using

                fileStream.Close()
            End If

            Return dialogResultOK
        End Function

        Public Overrides Sub OpenItemByPath(ByVal path As String)
            DisplayName = IO.Path.GetFileName(path)
            FilePath = path
            SetCodeLanguageProperties(IO.Path.GetExtension(path))
            CodeLanguageText = New CodeLanguageText(ModelCodeLanguage, Function() GetCodeTextByPath(path))
            IsActive = True
        End Sub

        Private Function GenerateClassText(ByVal className As String) As String
            Dim text As String = "
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualStudioDocking {{
    class {0} {{
    }}
}}"
            Return String.Format(text, className)
        End Function

        Private Function GetCodeLanguage(ByVal fileExtension As String) As CodeLanguage
            Select Case fileExtension
                Case ".cs"
                    Return CodeLanguage.CS
                Case ".vb"
                    Return CodeLanguage.VB
                Case ".xaml"
                    Return CodeLanguage.XAML
                Case Else
                    Return CodeLanguage.Plain
            End Select
        End Function

        Private Function GetCodeTextByPath(ByVal path As String) As String
            Dim assembly As Assembly = GetType(DocumentViewModel).Assembly
            Using stream As Stream = AssemblyHelper.GetResourceStream(assembly, path, True)
                If stream Is Nothing Then Return GenerateClassText(IO.Path.GetFileNameWithoutExtension(path))
                Using reader As StreamReader = New StreamReader(stream)
                    Return reader.ReadToEnd()
                End Using
            End Using
        End Function

        Private Function GetDescription(ByVal codeLanguage As CodeLanguage) As String
            Select Case codeLanguage
                Case CodeLanguage.CS
                    Return "Visual C# Source file"
                Case CodeLanguage.VB
                    Return "Visual Basic Source file"
                Case CodeLanguage.XAML
                    Return "Windows Markup File"
                Case Else
                    Return "Other file"
            End Select
        End Function

        Private Sub SetCodeLanguageProperties(ByVal fileExtension As String)
            ModelCodeLanguage = GetCodeLanguage(fileExtension)
            Description = GetDescription(ModelCodeLanguage)
            Footer = DisplayName
            Glyph = If(ModelCodeLanguage.Equals(CodeLanguage.XAML), Images.FileXaml, If(ModelCodeLanguage.Equals(CodeLanguage.CS), Images.FileCS, Nothing))
        End Sub
    End Class

    Public Class MainViewModel

        Private _Bars As ReadOnlyCollection(Of VisualStudioDocking.ViewModels.BarModel), _ErrorListViewModel As ErrorListViewModel, _OutputViewModel As OutputViewModel, _PropertiesViewModel As PropertiesViewModel, _ToolboxViewModel As ToolboxViewModel

        Private errorList As CommandViewModel

        Private lastOpenedItem As PanelWorkspaceViewModel

        Private loadLayout As CommandViewModel

        Private newFile As CommandViewModel

        Private newProject As CommandViewModel

        Private openFile As CommandViewModel

        Private openProject As CommandViewModel

        Private output As CommandViewModel

        Private properties As CommandViewModel

        Private save As CommandViewModel

        Private saveAll As CommandViewModel

        Private saveLayout As CommandViewModel

        Private searchResults As CommandViewModel

        Private solutionExplorer As CommandViewModel

        Private solutionExplorerViewModelField As SolutionExplorerViewModel

        Private toolbox As CommandViewModel

        Private workspacesField As ObservableCollection(Of WorkspaceViewModel)

        Public Sub New()
            ErrorListViewModel = CreatePanelWorkspaceViewModel(Of ErrorListViewModel)()
            OutputViewModel = CreatePanelWorkspaceViewModel(Of OutputViewModel)()
            PropertiesViewModel = CreatePanelWorkspaceViewModel(Of PropertiesViewModel)()
            SearchResultsViewModel = CreatePanelWorkspaceViewModel(Of SearchResultsViewModel)()
            ToolboxViewModel = CreatePanelWorkspaceViewModel(Of ToolboxViewModel)()
            Bars = New ReadOnlyCollection(Of BarModel)(CreateBars())
            InitDefaultLayout()
        End Sub

        Public Property Bars As ReadOnlyCollection(Of BarModel)
            Get
                Return _Bars
            End Get

            Private Set(ByVal value As ReadOnlyCollection(Of BarModel))
                _Bars = value
            End Set
        End Property

        Public Property ErrorListViewModel As ErrorListViewModel
            Get
                Return _ErrorListViewModel
            End Get

            Private Set(ByVal value As ErrorListViewModel)
                _ErrorListViewModel = value
            End Set
        End Property

        Public Property OutputViewModel As OutputViewModel
            Get
                Return _OutputViewModel
            End Get

            Private Set(ByVal value As OutputViewModel)
                _OutputViewModel = value
            End Set
        End Property

        Public Property PropertiesViewModel As PropertiesViewModel
            Get
                Return _PropertiesViewModel
            End Get

            Private Set(ByVal value As PropertiesViewModel)
                _PropertiesViewModel = value
            End Set
        End Property

        Public Property SearchResultsViewModel As SearchResultsViewModel

        Public ReadOnly Property SolutionExplorerViewModel As SolutionExplorerViewModel
            Get
                If solutionExplorerViewModelField Is Nothing Then
                    solutionExplorerViewModelField = CreatePanelWorkspaceViewModel(Of SolutionExplorerViewModel)()
                    AddHandler solutionExplorerViewModelField.ItemOpening, AddressOf SolutionExplorerViewModel_ItemOpening
                    solutionExplorerViewModelField.Solution = Solution.Create()
                    OpenItem(solutionExplorerViewModelField.Solution.LastOpenedItem.FilePath)
                End If

                Return solutionExplorerViewModelField
            End Get
        End Property

        Public Property ToolboxViewModel As ToolboxViewModel
            Get
                Return _ToolboxViewModel
            End Get

            Private Set(ByVal value As ToolboxViewModel)
                _ToolboxViewModel = value
            End Set
        End Property

        Public ReadOnly Property Workspaces As ObservableCollection(Of WorkspaceViewModel)
            Get
                If workspacesField Is Nothing Then
                    workspacesField = New ObservableCollection(Of WorkspaceViewModel)()
                    AddHandler workspacesField.CollectionChanged, AddressOf OnWorkspacesChanged
                End If

                Return workspacesField
            End Get
        End Property

        Protected Overridable ReadOnly Property SaveLoadLayoutService As IDockingSerializationDialogService
            Get
                Return Nothing
            End Get
        End Property

        Protected Overridable Function CreateAboutCommands() As List(Of CommandViewModel)
            Dim showAboutCommnad = New DelegateCommand(AddressOf ShowAbout)
            Return New List(Of CommandViewModel)() From {New CommandViewModel("About", showAboutCommnad) With {.Glyph = Images.About}}
        End Function

        Protected Overridable Function CreateEditCommands() As List(Of CommandViewModel)
            Dim findCommand = New CommandViewModel("Find") With {.Glyph = Images.Find, .KeyGesture = New KeyGesture(Key.F, ModifierKeys.Control)}
            Dim replaceCommand = New CommandViewModel("Replace") With {.Glyph = Images.Replace, .KeyGesture = New KeyGesture(Key.H, ModifierKeys.Control)}
            Dim findInFilesCommand = New CommandViewModel("Find in Files") With {.Glyph = Images.FindInFiles, .KeyGesture = New KeyGesture(Key.F, ModifierKeys.Control Or ModifierKeys.Shift)}
            Dim list = New List(Of CommandViewModel)() From {findCommand, replaceCommand, findInFilesCommand}
            Dim findReplaceDocument As CommandViewModel = New CommandViewModel("Find and Replace", list)
            findReplaceDocument.IsEnabled = True
            findReplaceDocument.IsSubItem = True
            Return New List(Of CommandViewModel)() From {findReplaceDocument}
        End Function

        Protected Overridable Function CreateLayoutCommands() As List(Of CommandViewModel)
            loadLayout = New CommandViewModel("Load Layout...", New DelegateCommand(AddressOf Me.OnLoadLayout)) With {.Glyph = Images.LoadLayout}
            saveLayout = New CommandViewModel("Save Layout...", New DelegateCommand(AddressOf Me.OnSaveLayout)) With {.Glyph = Images.SaveLayout}
            Return New List(Of CommandViewModel)() From {loadLayout, saveLayout}
        End Function

        Protected Function CreatePanelWorkspaceViewModel(Of T As PanelWorkspaceViewModel)() As T
            Return ViewModelSource(Of T).Create()
        End Function

        Protected Overridable Function CreateViewCommands() As List(Of CommandViewModel)
            toolbox = GetShowCommand(ToolboxViewModel)
            solutionExplorer = GetShowCommand(SolutionExplorerViewModel)
            properties = GetShowCommand(PropertiesViewModel)
            errorList = GetShowCommand(ErrorListViewModel)
            output = GetShowCommand(OutputViewModel)
            searchResults = GetShowCommand(SearchResultsViewModel)
            Return New List(Of CommandViewModel)() From {toolbox, solutionExplorer, properties, errorList, output, searchResults}
        End Function

        Private Function CreateThemesCommands() As List(Of CommandViewModel)
            Dim themesCommands = New List(Of CommandViewModel)()
            Dim converter = New ThemePaletteGlyphConverter()
            For Each theme As Theme In Theme.Themes.Where(Function(x) Equals(x.Category, Theme.VisualStudioCategory) AndAlso x.Name.StartsWith("VS2019"))
                Dim themeName = theme.Name
                Dim paletteCommands = New List(Of CommandViewModel)()
                Dim defaultPalette = New CommandViewModel("Default", New DelegateCommand(Of Theme)(Sub(t) SetTheme(theme))) With {.Glyph = CType(converter.Convert(themeName, Nothing, Nothing, CultureInfo.CurrentCulture), ImageSource)}
                paletteCommands.Add(defaultPalette)
                For Each palette In GetPalettes(theme)
                    Dim paletteTheme = Theme.Themes.FirstOrDefault(Function(x) Equals(x.Name, String.Format("{0}{1}", palette.Name, themeName)))
                    If paletteTheme IsNot Nothing Then
                        Dim command = New CommandViewModel(palette.Name, New DelegateCommand(Of Theme)(Sub(t) SetTheme(paletteTheme))) With {.Glyph = CType(converter.Convert(paletteTheme.Name, Nothing, Nothing, CultureInfo.CurrentCulture), ImageSource)}
                        paletteCommands.Add(command)
                    End If
                Next

                themesCommands.Add(New CommandViewModel(theme.Name.Replace("VS2019", ""), paletteCommands) With {.IsEnabled = True, .IsSubItem = True, .Glyph = CType((New SvgImageSourceExtension() With {.Uri = theme.SvgGlyph}.ProvideValue(Nothing)), ImageSource)})
            Next

            Return themesCommands
        End Function

        Private Sub SetTheme(ByVal theme As Theme)
            ApplicationThemeHelper.ApplicationThemeName = theme.Name
        End Sub

        Private Function GetPalettes(ByVal theme As Theme) As IEnumerable(Of PredefinedThemePalette)
            Select Case theme.Name
                Case Theme.VS2019LightName
                    Return PredefinedThemePalettes.VS2019LightPalettes
                Case Theme.VS2019DarkName
                    Return PredefinedThemePalettes.VS2019DarkPalettes
                Case Else
                    Return PredefinedThemePalettes.VS2019BluePalettes
            End Select
        End Function

        Protected Sub OpenOrCloseWorkspace(ByVal workspace As PanelWorkspaceViewModel, ByVal Optional activateOnOpen As Boolean = True)
            If Workspaces.Contains(workspace) Then
                workspace.IsClosed = Not workspace.IsClosed
            Else
                Workspaces.Add(workspace)
                workspace.IsClosed = False
            End If

            If activateOnOpen AndAlso workspace.IsOpened Then SetActiveWorkspace(workspace)
        End Sub

        Private Function ActivateDocument(ByVal path As String) As Boolean
            Dim document = GetDocument(path)
            Dim isFound As Boolean = document IsNot Nothing
            If isFound Then document.IsActive = True
            Return isFound
        End Function

        Private Function CreateBars() As List(Of BarModel)
            Return New List(Of BarModel)() From {New BarModel("Main") With {.IsMainMenu = True, .Commands = CreateCommands()}, New BarModel("Standard") With {.Commands = CreateToolbarCommands()}}
        End Function

        Private Function CreateCommands() As List(Of CommandViewModel)
            Return New List(Of CommandViewModel) From {New CommandViewModel("File", CreateFileCommands()), New CommandViewModel("Edit", CreateEditCommands()), New CommandViewModel("Layouts", CreateLayoutCommands()), New CommandViewModel("View", CreateViewCommands()), New CommandViewModel("Help", CreateAboutCommands()), New CommandViewModel("Themes", CreateThemesCommands())}
        End Function

        Private Function CreateDocumentViewModel() As DocumentViewModel
            Return CreatePanelWorkspaceViewModel(Of DocumentViewModel)()
        End Function

        Private Function CreateFileCommands() As List(Of CommandViewModel)
            Dim fileExecutedCommand = New DelegateCommand(Of Object)(AddressOf OnNewFileExecuted)
            Dim fileOpenCommand = New DelegateCommand(Of Object)(AddressOf OnFileOpenExecuted)
            Dim newCommand As CommandViewModel = New CommandViewModel("New") With {.IsSubItem = True}
            newProject = New CommandViewModel("Project...", fileExecutedCommand) With {.Glyph = Images.NewProject, .KeyGesture = New KeyGesture(Key.N, ModifierKeys.Control Or ModifierKeys.Shift), .IsEnabled = False}
            newFile = New CommandViewModel("New File", fileExecutedCommand) With {.Glyph = Images.File, .KeyGesture = New KeyGesture(Key.N, ModifierKeys.Control)}
            newCommand.Commands = New List(Of CommandViewModel)() From {newProject, newFile}
            Dim openCommand As CommandViewModel = New CommandViewModel("Open") With {.IsSubItem = True}
            openProject = New CommandViewModel("Project/Solution...") With {.Glyph = Images.OpenSolution, .IsEnabled = False, .KeyGesture = New KeyGesture(Key.O, ModifierKeys.Control Or ModifierKeys.Shift)}
            openFile = New CommandViewModel("Open File", fileOpenCommand) With {.Glyph = Images.OpenFile, .KeyGesture = New KeyGesture(Key.O, ModifierKeys.Control)}
            openCommand.Commands = New List(Of CommandViewModel)() From {openProject, openFile}
            Dim closeFile As CommandViewModel = New CommandViewModel("Close")
            Dim closeSolution As CommandViewModel = New CommandViewModel("Close Solution") With {.Glyph = Images.CloseSolution}
            save = New CommandViewModel("Save") With {.Glyph = Images.Save, .KeyGesture = New KeyGesture(Key.S, ModifierKeys.Control)}
            saveAll = New CommandViewModel("Save All") With {.Glyph = Images.SaveAll, .KeyGesture = New KeyGesture(Key.S, ModifierKeys.Control Or ModifierKeys.Shift)}
            Return New List(Of CommandViewModel)() From {newCommand, openCommand, GetSeparator(), closeFile, closeSolution, GetSeparator(), save, saveAll}
        End Function

        Private Function CreateToolbarCommands() As List(Of CommandViewModel)
            Dim start As CommandViewModel = New CommandViewModel("Start") With {.Glyph = Images.Run, .KeyGesture = New KeyGesture(Key.F5, ModifierKeys.None), .DisplayMode = BarItemDisplayMode.ContentAndGlyph}
            Dim combo As CommandViewModel = New CommandViewModel("Configuration") With {.IsSubItem = True, .IsComboBox = True}
            combo.Commands = New List(Of CommandViewModel)() From {New CommandViewModel("Debug"), New CommandViewModel("Release")}
            Dim cut As CommandViewModel = New CommandViewModel("Cut") With {.Glyph = Images.Cut, .KeyGesture = New KeyGesture(Key.X, ModifierKeys.Control)}
            Dim copy As CommandViewModel = New CommandViewModel("Copy") With {.Glyph = Images.Copy, .KeyGesture = New KeyGesture(Key.C, ModifierKeys.Control)}
            Dim paste As CommandViewModel = New CommandViewModel("Paste") With {.Glyph = Images.Paste, .KeyGesture = New KeyGesture(Key.V, ModifierKeys.Control)}
            Dim undo As CommandViewModel = New CommandViewModel("Undo") With {.Glyph = Images.Undo, .KeyGesture = New KeyGesture(Key.Z, ModifierKeys.Control)}
            Dim redo As CommandViewModel = New CommandViewModel("Redo") With {.Glyph = Images.Redo, .KeyGesture = New KeyGesture(Key.Y, ModifierKeys.Control)}
            Return New List(Of CommandViewModel)() From {newProject, newFile, openFile, save, saveAll, GetSeparator(), combo, start, GetSeparator(), cut, copy, paste, GetSeparator(), undo, redo, GetSeparator(), toolbox, solutionExplorer, properties, errorList, output, searchResults, GetSeparator(), loadLayout, saveLayout}
        End Function

        Private Function GetDocument(ByVal filePath As String) As DocumentViewModel
            Return Workspaces.OfType(Of DocumentViewModel)().FirstOrDefault(Function(x) Equals(x.FilePath, filePath))
        End Function

        Private Function GetSeparator() As CommandViewModel
            Return New CommandViewModel() With {.IsSeparator = True}
        End Function

        Private Function GetShowCommand(ByVal viewModel As PanelWorkspaceViewModel) As CommandViewModel
            Return New CommandViewModel(viewModel, New DelegateCommand(Sub() OpenOrCloseWorkspace(viewModel)))
        End Function

        Private Sub InitDefaultLayout()
            Dim panels = New List(Of PanelWorkspaceViewModel) From {ToolboxViewModel, SolutionExplorerViewModel, PropertiesViewModel, ErrorListViewModel}
            For Each panel In panels
                OpenOrCloseWorkspace(panel, False)
            Next
        End Sub

        Private Sub OnFileOpenExecuted(ByVal param As Object)
            Dim document = CreateDocumentViewModel()
            If Not document.OpenFile() OrElse ActivateDocument(document.FilePath) Then
                document.Dispose()
                Return
            End If

            OpenOrCloseWorkspace(document)
        End Sub

        Private Sub OnLoadLayout()
            SaveLoadLayoutService.LoadLayout()
        End Sub

        Private Sub OnNewFileExecuted(ByVal param As Object)
            Dim newItemName As String = solutionExplorerViewModelField.Solution.AddNewItemToRoot()
            OpenItem(newItemName)
        End Sub

        Private Sub OnSaveLayout()
            SaveLoadLayoutService.SaveLayout()
        End Sub

        Private Sub OnWorkspaceRequestClose(ByVal sender As Object, ByVal e As EventArgs)
            Dim workspace = TryCast(sender, PanelWorkspaceViewModel)
            If workspace IsNot Nothing Then
                workspace.IsClosed = True
                If TypeOf workspace Is DocumentViewModel Then
                    workspace.Dispose()
                    Workspaces.Remove(workspace)
                End If
            End If
        End Sub

        Private Sub OnWorkspacesChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
            If e.NewItems IsNot Nothing AndAlso e.NewItems.Count <> 0 Then
                For Each workspace As WorkspaceViewModel In e.NewItems
                    AddHandler workspace.RequestClose, AddressOf OnWorkspaceRequestClose
                Next
            End If

            If e.OldItems IsNot Nothing AndAlso e.OldItems.Count <> 0 Then
                For Each workspace As WorkspaceViewModel In e.OldItems
                    RemoveHandler workspace.RequestClose, AddressOf OnWorkspaceRequestClose
                Next
            End If
        End Sub

        Private Sub OpenItem(ByVal filePath As String)
            If ActivateDocument(filePath) Then Return
            lastOpenedItem = CreateDocumentViewModel()
            lastOpenedItem.OpenItemByPath(filePath)
            OpenOrCloseWorkspace(lastOpenedItem)
        End Sub

        Private Sub SetActiveWorkspace(ByVal workspace As WorkspaceViewModel)
            workspace.IsActive = True
        End Sub

        Private Sub ShowAbout()
            DevExpress.Xpf.About.ShowAbout(ProductKind.DXperienceWPF)
        End Sub

        Private Sub SolutionExplorerViewModel_ItemOpening(ByVal sender As Object, ByVal e As SolutionItemOpeningEventArgs)
            OpenItem(e.SolutionItem.FilePath)
        End Sub
    End Class

    MustInherit Public Class PanelWorkspaceViewModel
        Inherits WorkspaceViewModel
        Implements IMVVMDockingProperties

        Private _targetName As String

        Protected Sub New()
            _targetName = WorkspaceName
        End Sub

        MustOverride Protected ReadOnly Property WorkspaceName As String

        Private Property TargetName As String Implements IMVVMDockingProperties.TargetName
            Get
                Return _targetName
            End Get

            Set(ByVal value As String)
                _targetName = value
            End Set
        End Property

        Public Overridable Sub OpenItemByPath(ByVal path As String)
        End Sub
    End Class

    Public MustInherit Class WorkspaceViewModel
        Inherits ViewModel

        Protected Sub New()
            IsClosed = True
        End Sub

        Public Event RequestClose As EventHandler

        Public Overridable Property IsActive As Boolean

        <BindableProperty(OnPropertyChangedMethodName:="OnIsClosedChanged")>
        Public Overridable Property IsClosed As Boolean

        Public Overridable Property IsOpened As Boolean

        Public Sub Close()
            Dim handler As EventHandler = RequestCloseEvent
            If handler IsNot Nothing Then handler(Me, EventArgs.Empty)
        End Sub

        Protected Overridable Sub OnIsClosedChanged()
            IsOpened = Not IsClosed
        End Sub
    End Class

    Public MustInherit Class ViewModel
        Implements IDisposable

        Public ReadOnly Property BindableName As String
            Get
                Return GetBindableName(DisplayName)
            End Get
        End Property

        Public Overridable Property DisplayName As String

        Public Overridable Property Glyph As ImageSource

        Private Function GetBindableName(ByVal name As String) As String
            Return "_" & Regex.Replace(name, "\W", "")
        End Function

#Region "IDisposable Members"
        Public Sub Dispose() Implements IDisposable.Dispose
            OnDispose()
        End Sub

        Protected Overridable Sub OnDispose()
        End Sub

#If DEBUG
        Protected Overrides Sub Finalize()
            Dim msg As String = String.Format("{0} ({1}) ({2}) Finalized", [GetType]().Name, DisplayName, GetHashCode())
            System.Diagnostics.Debug.WriteLine(msg)
        End Sub
#End If
#End Region
    End Class

#Region "Tool Panels"
    Public Class ErrorListViewModel
        Inherits PanelWorkspaceViewModel

        Public Sub New()
            DisplayName = "Error List"
            Glyph = Images.TaskList
            [Error] = Images.Error
            Warning = Images.Warning
            Info = Images.Info
        End Sub

        Public Property [Error] As ImageSource

        Public Property Info As ImageSource

        Public Property Warning As ImageSource

        Protected Overrides ReadOnly Property WorkspaceName As String
            Get
                Return "BottomHost"
            End Get
        End Property
    End Class

    Public Class OutputViewModel
        Inherits PanelWorkspaceViewModel

        Private _Text As String

        Public Sub New()
            DisplayName = "Output"
            Glyph = Images.Output
            Text = "1>------ Build started: Project: VisualStudioInspiredUIDemo, Configuration: Debug Any CPU ------
1>  DockingDemo -> C:\VisualStudioInspiredUIDemo.exe
========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped =========="
        End Sub

        Public Property Text As String
            Get
                Return _Text
            End Get

            Private Set(ByVal value As String)
                _Text = value
            End Set
        End Property

        Protected Overrides ReadOnly Property WorkspaceName As String
            Get
                Return "BottomHost"
            End Get
        End Property
    End Class

    Public Class PropertiesViewModel
        Inherits PanelWorkspaceViewModel

        Public Sub New()
            DisplayName = "Properties"
            Glyph = Images.PropertiesWindow
            SelectedItem = New PropertyItem(New PropertyGridControl())
            Items = New List(Of PropertyItem) From {SelectedItem, New PropertyItem(New AccordionControl()), New PropertyItem(New DocumentPanel()), New PropertyItem(New DocumentGroup()), New PropertyItem(New Docking.LayoutPanel())}
        End Sub

        Public Property Items As List(Of PropertyItem)

        Public Overridable Property SelectedItem As PropertyItem

        Protected Overrides ReadOnly Property WorkspaceName As String
            Get
                Return "RightHost"
            End Get
        End Property
    End Class

    Public Class PropertyItem

        Public Sub New(ByVal data As Object)
            Me.Data = data
            Name = Me.Data.ToString()
        End Sub

        Public Property Data As Object

        Public Property Name As String
    End Class

    Public Class SearchResultsViewModel
        Inherits PanelWorkspaceViewModel

        Private _Text As String

        Public Sub New()
            DisplayName = "Search Results"
            Glyph = Images.FindInFilesWindow
            Text = "Matching lines: 0    Matching files: 0    Total files searched: 61"
        End Sub

        Public Property Text As String
            Get
                Return _Text
            End Get

            Private Set(ByVal value As String)
                _Text = value
            End Set
        End Property

        Protected Overrides ReadOnly Property WorkspaceName As String
            Get
                Return "BottomHost"
            End Get
        End Property
    End Class

    Public Class Solution
        Inherits BindableBase

        Private _Items As ObservableCollection(Of VisualStudioDocking.ViewModels.SolutionItem)

        Private codePaths As String() = New String() {"MainWindow.xaml", "MainWindow.xaml.cs", "Resources.xaml", "BarTemplateSelector.cs", "MainViewModel.cs"}

        Private newItemsCount As Integer

        Protected Sub New()
            Dim root As SolutionItem = SolutionItem.Create(Me, "WPFApplication", ImagePaths.SolutionExplorer)
            Dim properties As SolutionItem = SolutionItem.Create(Me, "Properties", ImagePaths.PropertiesWindow)
            Dim references As SolutionItem = SolutionItem.Create(Me, "References", ImagePaths.References)
            root.Items.Add(properties)
            root.Items.Add(references)
            Dim files = GetCodeFiles()
            For Each file As SolutionItem In files
                root.Items.Add(file)
            Next

            LastOpenedItem = files.FirstOrDefault()
            Items = New ObservableCollection(Of SolutionItem) From {root}
        End Sub

        Public Property Items As ObservableCollection(Of SolutionItem)
            Get
                Return _Items
            End Get

            Private Set(ByVal value As ObservableCollection(Of SolutionItem))
                _Items = value
            End Set
        End Property

        Public Overridable Property LastOpenedItem As SolutionItem

        Public Shared Function Create() As Solution
            Return ViewModelSource.Create(Function() New Solution())
        End Function

        Public Function AddNewItemToRoot() As String
            newItemsCount += 1
            Dim newItemName As String = String.Format("Class{0}.cs", newItemsCount)
            Dim solutionItem = ViewModels.SolutionItem.CreateFile(Me, newItemName)
            TryCast(Items(0), SolutionItem).Items.Add(solutionItem)
            Return newItemName
        End Function

        Private Function GetCodeFiles() As List(Of SolutionItem)
            Dim result = New List(Of SolutionItem)()
            Dim subFiles = New List(Of SolutionItem)()
            For Each codePath In codePaths
                If codePath.EndsWith(".xaml.cs") OrElse codePath.EndsWith(".xaml.vb") Then
                    subFiles.Add(SolutionItem.CreateFile(Me, codePath))
                Else
                    result.Add(SolutionItem.CreateFile(Me, codePath))
                End If
            Next

            For Each subFile In subFiles
                Dim xamlFile = result.FirstOrDefault(Function(x) Equals(x.FilePath, subFile.FilePath.Replace(".xaml.cs", ".xaml").Replace(".xaml.vb", ".xaml")))
                If xamlFile Is Nothing Then
                    result.Add(subFile)
                Else
                    xamlFile.Items.Add(subFile)
                End If
            Next

            Return result
        End Function
    End Class

    Public Class SolutionExplorerViewModel
        Inherits PanelWorkspaceViewModel

        Public Sub New()
            DisplayName = "Solution Explorer"
            Glyph = Images.SolutionExplorer
            PropertiesWindow = Images.PropertiesWindow
            ShowAllFiles = Images.ShowAllFiles
            Refresh = Images.Refresh
        End Sub

        Public Event ItemOpening As EventHandler(Of SolutionItemOpeningEventArgs)

        Public Property PropertiesWindow As ImageSource

        Public Property Refresh As ImageSource

        Public Property ShowAllFiles As ImageSource

        Public Property Solution As Solution

        Protected Overrides ReadOnly Property WorkspaceName As String
            Get
                Return "RightHost"
            End Get
        End Property

        Public Sub OpenItem(ByVal item As SolutionItem)
            If item IsNot Nothing AndAlso item.IsFile AndAlso ItemOpeningEvent IsNot Nothing Then RaiseEvent ItemOpening(Me, New SolutionItemOpeningEventArgs(item))
        End Sub
    End Class

    Public Class SolutionItem

        Private _DisplayName As String, _FilePath As String, _GlyphPath As String, _Items As ObservableCollection(Of VisualStudioDocking.ViewModels.SolutionItem)

        Private ReadOnly solution As Solution

        Protected Sub New(ByVal solution As Solution)
            Me.solution = solution
            Items = New ObservableCollection(Of SolutionItem)()
        End Sub

        Public Property DisplayName As String
            Get
                Return _DisplayName
            End Get

            Private Set(ByVal value As String)
                _DisplayName = value
            End Set
        End Property

        Public Property FilePath As String
            Get
                Return _FilePath
            End Get

            Private Set(ByVal value As String)
                _FilePath = value
            End Set
        End Property

        Public Property GlyphPath As String
            Get
                Return _GlyphPath
            End Get

            Private Set(ByVal value As String)
                _GlyphPath = value
            End Set
        End Property

        Public ReadOnly Property IsFile As Boolean
            Get
                Return Not Equals(FilePath, Nothing)
            End Get
        End Property

        Public Property Items As ObservableCollection(Of SolutionItem)
            Get
                Return _Items
            End Get

            Private Set(ByVal value As ObservableCollection(Of SolutionItem))
                _Items = value
            End Set
        End Property

        Public Shared Function Create(ByVal solution As Solution, ByVal displayName As String, ByVal glyph As String) As SolutionItem
            Dim solutionItem = ViewModelSource.Create(Function() New SolutionItem(solution))
            solutionItem.[Do](Sub(x)
                x.DisplayName = displayName
                x.GlyphPath = glyph
            End Sub)
            Return solutionItem
        End Function

        Public Shared Function CreateFile(ByVal solution As Solution, ByVal path As String) As SolutionItem
            Dim solutionItem = ViewModelSource.Create(Function() New SolutionItem(solution))
            solutionItem.[Do](Sub(x)
                x.DisplayName = IO.Path.GetFileName(path)
                x.GlyphPath = If(Equals(IO.Path.GetExtension(path), ".cs"), ImagePaths.FileCS, ImagePaths.FileXaml)
                x.FilePath = path
            End Sub)
            Return solutionItem
        End Function
    End Class

    Public Class SolutionItemOpeningEventArgs
        Inherits EventArgs

        Public Sub New(ByVal solutionItem As SolutionItem)
            Me.SolutionItem = solutionItem
        End Sub

        Public Property SolutionItem As SolutionItem
    End Class

    Public Class ToolboxViewModel
        Inherits PanelWorkspaceViewModel

        Public Sub New()
            DisplayName = "Toolbox"
            Glyph = Images.Toolbox
        End Sub

        Protected Overrides ReadOnly Property WorkspaceName As String
            Get
                Return "Toolbox"
            End Get
        End Property
    End Class

#End Region
#Region "Bars"
    Public Class BarModel
        Inherits ViewModel

        Public Sub New(ByVal displayName As String)
            Me.DisplayName = displayName
        End Sub

        Public Property Commands As List(Of CommandViewModel)

        Public Property IsMainMenu As Boolean
    End Class

    Public Class CommandViewModel
        Inherits ViewModel

        Private _Command As ICommand, _Owner As WorkspaceViewModel

        Public Sub New()
        End Sub

        Public Sub New(ByVal displayName As String, ByVal subCommands As List(Of CommandViewModel))
            Me.New(displayName, Nothing, Nothing, subCommands)
        End Sub

        Public Sub New(ByVal displayName As String, ByVal Optional command As ICommand = Nothing)
            Me.New(displayName, Nothing, command, Nothing)
        End Sub

        Public Sub New(ByVal owner As WorkspaceViewModel, ByVal command As ICommand)
            Me.New(String.Empty, owner, command)
        End Sub

        Private Sub New(ByVal displayName As String, ByVal Optional owner As WorkspaceViewModel = Nothing, ByVal Optional command As ICommand = Nothing, ByVal Optional subCommands As List(Of CommandViewModel) = Nothing)
            IsEnabled = True
            Me.Owner = owner
            If Me.Owner IsNot Nothing Then
                Me.DisplayName = Me.Owner.DisplayName
                Glyph = Me.Owner.Glyph
            Else
                Me.DisplayName = displayName
            End If

            Me.Command = command
            Commands = subCommands
        End Sub

        Public Property Command As ICommand
            Get
                Return _Command
            End Get

            Private Set(ByVal value As ICommand)
                _Command = value
            End Set
        End Property

        Public Property Commands As List(Of CommandViewModel)

        Public Property DisplayMode As BarItemDisplayMode

        Public Property IsComboBox As Boolean

        Public Property IsEnabled As Boolean

        Public Property IsSeparator As Boolean

        Public Property IsSubItem As Boolean

        Public Property KeyGesture As KeyGesture

        Public Property Owner As WorkspaceViewModel
            Get
                Return _Owner
            End Get

            Private Set(ByVal value As WorkspaceViewModel)
                _Owner = value
            End Set
        End Property
    End Class

#End Region
#Region "Images"
    Public Module ImagePaths

        Public Const About As String = folderSvg & "About.svg"

        Public Const CloseSolution As String = folderSvg & "CloseSolution.svg"

        Public Const Copy As String = folderSvg & "Copy.svg"

        Public Const Cut As String = folderSvg & "Cut.svg"

        Public Const DevExpressLogo As String = folder & "DevExpressLogo.png"

        Public Const [Error] As String = folderSvg & "Error.svg"

        Public Const File As String = folderSvg & "File.svg"

        Public Const FileCS As String = folderSvg & "FileCS.svg"

        Public Const FileXaml As String = folderSvg & "FileXaml.svg"

        Public Const Find As String = folderSvg & "Find.svg"

        Public Const FindInFiles As String = folderSvg & "FindInFiles.svg"

        Public Const FindInFilesWindow As String = folderSvg & "FindInFilesWindow.svg"

        Public Const Info As String = folderSvg & "Info.svg"

        Public Const LoadLayout As String = folderSvg & "LoadLayout.svg"

        Public Const NewProject As String = folderSvg & "NewProject.svg"

        Public Const OpenFile As String = folderSvg & "OpenFile.svg"

        Public Const OpenSolution As String = folderSvg & "OpenSolution.svg"

        Public Const Output As String = folderSvg & "Output.svg"

        Public Const Paste As String = folderSvg & "Paste.svg"

        Public Const PropertiesWindow As String = folderSvg & "PropertiesWindow.svg"

        Public Const Redo As String = folderSvg & "Redo.svg"

        Public Const References As String = folderSvg & "References.svg"

        Public Const Refresh As String = folderSvg & "Refresh.svg"

        Public Const Replace As String = folderSvg & "Replace.svg"

        Public Const Run As String = folderSvg & "Run.svg"

        Public Const Save As String = folderSvg & "Save.svg"

        Public Const SaveAll As String = folderSvg & "SaveAll.svg"

        Public Const SaveLayout As String = folderSvg & "SaveLayout.svg"

        Public Const ShowAllFiles As String = folderSvg & "ShowAllFiles.svg"

        Public Const SolutionExplorer As String = folderSvg & "SolutionExplorer.svg"

        Public Const TaskList As String = folderSvg & "TaskList.svg"

        Public Const Toolbox As String = folderSvg & "Toolbox.svg"

        Public Const Undo As String = folderSvg & "Undo.svg"

        Public Const Warning As String = folderSvg & "Warning.svg"

        Const folderSvg As String = "pack://application:,,,/VisualStudioDocking;component/Images/Docking/"

        Const folder As String = "/VisualStudioDocking;component/Images/Docking/"
    End Module

    Public Module Images

        Public ReadOnly Property About As ImageSource
            Get
                Return GetSvgImage(ImagePaths.About)
            End Get
        End Property

        Public ReadOnly Property CloseSolution As ImageSource
            Get
                Return GetSvgImage(ImagePaths.CloseSolution)
            End Get
        End Property

        Public ReadOnly Property Copy As ImageSource
            Get
                Return GetSvgImage(ImagePaths.Copy)
            End Get
        End Property

        Public ReadOnly Property Cut As ImageSource
            Get
                Return GetSvgImage(ImagePaths.Cut)
            End Get
        End Property

        Public ReadOnly Property DevExpressLogo As ImageSource
            Get
                Return GetImage(ImagePaths.DevExpressLogo)
            End Get
        End Property

        Public ReadOnly Property [Error] As ImageSource
            Get
                Return GetSvgImage(ImagePaths.Error)
            End Get
        End Property

        Public ReadOnly Property File As ImageSource
            Get
                Return GetSvgImage(ImagePaths.File)
            End Get
        End Property

        Public ReadOnly Property FileCS As ImageSource
            Get
                Return GetSvgImage(ImagePaths.FileCS)
            End Get
        End Property

        Public ReadOnly Property FileXaml As ImageSource
            Get
                Return GetSvgImage(ImagePaths.FileXaml)
            End Get
        End Property

        Public ReadOnly Property Find As ImageSource
            Get
                Return GetSvgImage(ImagePaths.Find)
            End Get
        End Property

        Public ReadOnly Property FindInFiles As ImageSource
            Get
                Return GetSvgImage(ImagePaths.FindInFiles)
            End Get
        End Property

        Public ReadOnly Property FindInFilesWindow As ImageSource
            Get
                Return GetSvgImage(ImagePaths.FindInFilesWindow)
            End Get
        End Property

        Public ReadOnly Property Info As ImageSource
            Get
                Return GetSvgImage(ImagePaths.Info)
            End Get
        End Property

        Public ReadOnly Property LoadLayout As ImageSource
            Get
                Return GetSvgImage(ImagePaths.LoadLayout)
            End Get
        End Property

        Public ReadOnly Property NewProject As ImageSource
            Get
                Return GetSvgImage(ImagePaths.NewProject)
            End Get
        End Property

        Public ReadOnly Property OpenFile As ImageSource
            Get
                Return GetSvgImage(ImagePaths.OpenFile)
            End Get
        End Property

        Public ReadOnly Property OpenSolution As ImageSource
            Get
                Return GetSvgImage(ImagePaths.OpenSolution)
            End Get
        End Property

        Public ReadOnly Property Output As ImageSource
            Get
                Return GetSvgImage(ImagePaths.Output)
            End Get
        End Property

        Public ReadOnly Property Paste As ImageSource
            Get
                Return GetSvgImage(ImagePaths.Paste)
            End Get
        End Property

        Public ReadOnly Property PropertiesWindow As ImageSource
            Get
                Return GetSvgImage(ImagePaths.PropertiesWindow)
            End Get
        End Property

        Public ReadOnly Property Redo As ImageSource
            Get
                Return GetSvgImage(ImagePaths.Redo)
            End Get
        End Property

        Public ReadOnly Property References As ImageSource
            Get
                Return GetSvgImage(ImagePaths.References)
            End Get
        End Property

        Public ReadOnly Property Refresh As ImageSource
            Get
                Return GetSvgImage(ImagePaths.Refresh)
            End Get
        End Property

        Public ReadOnly Property Replace As ImageSource
            Get
                Return GetSvgImage(ImagePaths.Replace)
            End Get
        End Property

        Public ReadOnly Property Run As ImageSource
            Get
                Return GetSvgImage(ImagePaths.Run)
            End Get
        End Property

        Public ReadOnly Property Save As ImageSource
            Get
                Return GetSvgImage(ImagePaths.Save)
            End Get
        End Property

        Public ReadOnly Property SaveAll As ImageSource
            Get
                Return GetSvgImage(ImagePaths.SaveAll)
            End Get
        End Property

        Public ReadOnly Property SaveLayout As ImageSource
            Get
                Return GetSvgImage(ImagePaths.SaveLayout)
            End Get
        End Property

        Public ReadOnly Property ShowAllFiles As ImageSource
            Get
                Return GetSvgImage(ImagePaths.ShowAllFiles)
            End Get
        End Property

        Public ReadOnly Property SolutionExplorer As ImageSource
            Get
                Return GetSvgImage(ImagePaths.SolutionExplorer)
            End Get
        End Property

        Public ReadOnly Property TaskList As ImageSource
            Get
                Return GetSvgImage(ImagePaths.TaskList)
            End Get
        End Property

        Public ReadOnly Property Toolbox As ImageSource
            Get
                Return GetSvgImage(ImagePaths.Toolbox)
            End Get
        End Property

        Public ReadOnly Property Undo As ImageSource
            Get
                Return GetSvgImage(ImagePaths.Undo)
            End Get
        End Property

        Public ReadOnly Property Warning As ImageSource
            Get
                Return GetSvgImage(ImagePaths.Warning)
            End Get
        End Property

        Private Function GetImage(ByVal path As String) As ImageSource
            Return New BitmapImage(New Uri(path, UriKind.Relative))
        End Function

        Private Function GetSvgImage(ByVal path As String) As ImageSource
            Dim svgImageSource = New SvgImageSourceExtension() With {.Uri = New Uri(path)}.ProvideValue(Nothing)
            Return CType(svgImageSource, ImageSource)
        End Function
    End Module
#End Region
End Namespace
