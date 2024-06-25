using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils;
using DevExpress.Utils.About;
using DevExpress.Xpf;
using DevExpress.Xpf.Accordion;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Bars.Native;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.DemoBase.Helpers.TextColorizer;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.PropertyGrid;
using Microsoft.Win32;

namespace VisualStudioDocking.ViewModels {
    public class DocumentViewModel : PanelWorkspaceViewModel {
        public DocumentViewModel() {
            IsClosed = false;
        }
        public DocumentViewModel(string displayName, string text) : this() {
            DisplayName = displayName;
            CodeLanguageText = new CodeLanguageText(CodeLanguage.CS, text);
        }

        public CodeLanguage ModelCodeLanguage { get; private set; }
        public CodeLanguageText CodeLanguageText { get; private set; }
        public string Description { get; protected set; }
        public string FilePath { get; protected set; }
        public string Footer { get; protected set; }
        protected override string WorkspaceName { get { return "DocumentHost"; } }

        public bool OpenFile() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Visual C# Files (*.cs)|*.cs|XAML Files (*.xaml)|*.xaml";
            openFileDialog.FilterIndex = 1;
            bool? dialogResult = openFileDialog.ShowDialog();
            bool dialogResultOK = dialogResult.HasValue && dialogResult.Value;
            if(dialogResultOK) {
                DisplayName = openFileDialog.SafeFileName;
                FilePath = openFileDialog.FileName;
                SetCodeLanguageProperties(Path.GetExtension(openFileDialog.SafeFileName));
                Stream fileStream = File.OpenRead(openFileDialog.FileName);
                using(StreamReader reader = new StreamReader(fileStream)) {
                    CodeLanguageText = new CodeLanguageText(ModelCodeLanguage, reader.ReadToEnd());
                }
                fileStream.Close();
            }
            return dialogResultOK;
        }
        public override void OpenItemByPath(string path) {
            DisplayName = Path.GetFileName(path);
            FilePath = path;
            SetCodeLanguageProperties(Path.GetExtension(path));
            CodeLanguageText = new CodeLanguageText(ModelCodeLanguage, () => { return GetCodeTextByPath(path); });
            IsActive = true;
        }
        string GenerateClassText(string className) {
            string text = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualStudioDocking {{
    class {0} {{
    }}
}}";
            return string.Format(text, className);
        }
        CodeLanguage GetCodeLanguage(string fileExtension) {
            switch(fileExtension) {
                case ".cs": return CodeLanguage.CS;
                case ".vb": return CodeLanguage.VB;
                case ".xaml": return CodeLanguage.XAML;
                default: return CodeLanguage.Plain;
            }
        }
        string GetCodeTextByPath(string path) {
            Assembly assembly = typeof(DocumentViewModel).Assembly;
            using(Stream stream = AssemblyHelper.GetResourceStream(assembly, path, true)) {
                if(stream == null)
                    return GenerateClassText(Path.GetFileNameWithoutExtension(path));
                using(StreamReader reader = new StreamReader(stream))
                    return reader.ReadToEnd();
            }
        }
        string GetDescription(CodeLanguage codeLanguage) {
            switch(codeLanguage) {
                case CodeLanguage.CS: return "Visual C# Source file";
                case CodeLanguage.VB: return "Visual Basic Source file";
                case CodeLanguage.XAML: return "Windows Markup File";
                default: return "Other file";
            }
        }
        void SetCodeLanguageProperties(string fileExtension) {
            ModelCodeLanguage = GetCodeLanguage(fileExtension);
            Description = GetDescription(ModelCodeLanguage);
            Footer = DisplayName;
            Glyph = ModelCodeLanguage.Equals(CodeLanguage.XAML) ? Images.FileXaml : ModelCodeLanguage.Equals(CodeLanguage.CS) ? Images.FileCS : null;
        }
    }
    public class MainViewModel {
        CommandViewModel errorList;
        PanelWorkspaceViewModel lastOpenedItem;
        CommandViewModel loadLayout;
        CommandViewModel newFile;
        CommandViewModel newProject;
        CommandViewModel openFile;
        CommandViewModel openProject;
        CommandViewModel output;
        CommandViewModel properties;
        CommandViewModel save;
        CommandViewModel saveAll;
        CommandViewModel saveLayout;
        CommandViewModel searchResults;
        CommandViewModel solutionExplorer;
        SolutionExplorerViewModel solutionExplorerViewModel;
        CommandViewModel toolbox;
        ObservableCollection<WorkspaceViewModel> workspaces;

        public MainViewModel() {
            ErrorListViewModel = CreatePanelWorkspaceViewModel<ErrorListViewModel>();
            OutputViewModel = CreatePanelWorkspaceViewModel<OutputViewModel>();
            PropertiesViewModel = CreatePanelWorkspaceViewModel<PropertiesViewModel>();
            SearchResultsViewModel = CreatePanelWorkspaceViewModel<SearchResultsViewModel>();
            ToolboxViewModel = CreatePanelWorkspaceViewModel<ToolboxViewModel>();
            Bars = new ReadOnlyCollection<BarModel>(CreateBars());
            InitDefaultLayout();
        }

        public ReadOnlyCollection<BarModel> Bars { get; private set; }
        public ErrorListViewModel ErrorListViewModel { get; private set; }
        public OutputViewModel OutputViewModel { get; private set; }
        public PropertiesViewModel PropertiesViewModel { get; private set; }
        public SearchResultsViewModel SearchResultsViewModel { get; set; }
        public SolutionExplorerViewModel SolutionExplorerViewModel {
            get {
                if(solutionExplorerViewModel == null) {
                    solutionExplorerViewModel = CreatePanelWorkspaceViewModel<SolutionExplorerViewModel>();
                    solutionExplorerViewModel.ItemOpening += SolutionExplorerViewModel_ItemOpening;
                    solutionExplorerViewModel.Solution = Solution.Create();
                    OpenItem(solutionExplorerViewModel.Solution.LastOpenedItem.FilePath);
                }
                return solutionExplorerViewModel;
            }
        }
        public ToolboxViewModel ToolboxViewModel { get; private set; }
        public ObservableCollection<WorkspaceViewModel> Workspaces {
            get {
                if(workspaces == null) {
                    workspaces = new ObservableCollection<WorkspaceViewModel>();
                    workspaces.CollectionChanged += OnWorkspacesChanged;
                }
                return workspaces;
            }
        }
        protected virtual IDockingSerializationDialogService SaveLoadLayoutService { get { return null; } }

        protected virtual List<CommandViewModel> CreateAboutCommands() {
            var showAboutCommnad = new DelegateCommand(ShowAbout);
            return new List<CommandViewModel>() { new CommandViewModel("About", showAboutCommnad) { Glyph = Images.About } };
        }
        protected virtual List<CommandViewModel> CreateEditCommands() {
            var findCommand = new CommandViewModel("Find") { Glyph = Images.Find, KeyGesture = new KeyGesture(Key.F, ModifierKeys.Control) };
            var replaceCommand = new CommandViewModel("Replace") { Glyph = Images.Replace, KeyGesture = new KeyGesture(Key.H, ModifierKeys.Control) };
            var findInFilesCommand = new CommandViewModel("Find in Files") {
                Glyph = Images.FindInFiles,
                KeyGesture = new KeyGesture(Key.F, ModifierKeys.Control | ModifierKeys.Shift)
            };
            var list = new List<CommandViewModel>() { findCommand, replaceCommand, findInFilesCommand };
            CommandViewModel findReplaceDocument = new CommandViewModel("Find and Replace", list);
            findReplaceDocument.IsEnabled = true;
            findReplaceDocument.IsSubItem = true;
            return new List<CommandViewModel>() { findReplaceDocument };
        }
        protected virtual List<CommandViewModel> CreateLayoutCommands() {
            loadLayout = new CommandViewModel("Load Layout...", new DelegateCommand(OnLoadLayout)) { Glyph = Images.LoadLayout };
            saveLayout = new CommandViewModel("Save Layout...", new DelegateCommand(OnSaveLayout)) { Glyph = Images.SaveLayout };
            return new List<CommandViewModel>() { loadLayout, saveLayout };
        }
        protected T CreatePanelWorkspaceViewModel<T>() where T : PanelWorkspaceViewModel {
            return ViewModelSource<T>.Create();
        }
        protected virtual List<CommandViewModel> CreateViewCommands() {
            toolbox = GetShowCommand(ToolboxViewModel);
            solutionExplorer = GetShowCommand(SolutionExplorerViewModel);
            properties = GetShowCommand(PropertiesViewModel);
            errorList = GetShowCommand(ErrorListViewModel);
            output = GetShowCommand(OutputViewModel);
            searchResults = GetShowCommand(SearchResultsViewModel);
            return new List<CommandViewModel>() {
                toolbox,
                solutionExplorer,
                properties,
                errorList,
                output,
                searchResults,
            };
        }
        List<CommandViewModel> CreateThemesCommands() {
            var themesCommands = new List<CommandViewModel>();
            var converter = new ThemePaletteGlyphConverter();
            foreach (Theme theme in Theme.Themes.Where(x => x.Category == Theme.VisualStudioCategory && x.Name.StartsWith("VS2019"))) {
                var themeName = theme.Name;
                var paletteCommands = new List<CommandViewModel>();
                var defaultPalette = new CommandViewModel("Default", new DelegateCommand<Theme>(t => SetTheme(theme))) {
                    Glyph = (ImageSource)converter.Convert(themeName, null, null, CultureInfo.CurrentCulture)
                };
                paletteCommands.Add(defaultPalette);
                foreach (var palette in GetPalettes(theme)) {
                    var paletteTheme = Theme.Themes.FirstOrDefault(x => x.Name == string.Format("{0}{1}", palette.Name, themeName));
                    if (paletteTheme != null) {
                        var command = new CommandViewModel(palette.Name, new DelegateCommand<Theme>(t => SetTheme(paletteTheme))) {
                            Glyph = (ImageSource)converter.Convert(paletteTheme.Name, null, null, CultureInfo.CurrentCulture)
                        };
                        paletteCommands.Add(command);
                    }
                }
                themesCommands.Add(new CommandViewModel(theme.Name.Replace("VS2019", ""), paletteCommands) {
                    IsEnabled = true, IsSubItem = true, Glyph = (ImageSource)(new SvgImageSourceExtension() {Uri = theme.SvgGlyph}.ProvideValue(null))
                });
            }
            return themesCommands;
        }
        void SetTheme(Theme theme) {
            ApplicationThemeHelper.ApplicationThemeName = theme.Name;
        }
        IEnumerable<PredefinedThemePalette> GetPalettes(Theme theme) {
            switch (theme.Name) {
                case Theme.VS2019LightName:
                    return PredefinedThemePalettes.VS2019LightPalettes;
                case Theme.VS2019DarkName:
                    return PredefinedThemePalettes.VS2019DarkPalettes;
                default:
                    return PredefinedThemePalettes.VS2019BluePalettes;
            }
        }
        protected void OpenOrCloseWorkspace(PanelWorkspaceViewModel workspace, bool activateOnOpen = true) {
            if (Workspaces.Contains(workspace)) {
                workspace.IsClosed = !workspace.IsClosed;
            }
            else {
                Workspaces.Add(workspace);
                workspace.IsClosed = false;
            }
            if (activateOnOpen && workspace.IsOpened)
                SetActiveWorkspace(workspace);
        }
        bool ActivateDocument(string path) {
            var document = GetDocument(path);
            bool isFound = document != null;
            if (isFound) document.IsActive = true;
            return isFound;
        }
        List<BarModel> CreateBars() {
            return new List<BarModel>() {
                new BarModel("Main") { IsMainMenu = true, Commands = CreateCommands() },
                new BarModel("Standard") { Commands = CreateToolbarCommands() }
            };
        }
        List<CommandViewModel> CreateCommands() {
            return new List<CommandViewModel> {
                new CommandViewModel("File", CreateFileCommands()),
                new CommandViewModel("Edit", CreateEditCommands()),
                new CommandViewModel("Layouts", CreateLayoutCommands()),
                new CommandViewModel("View", CreateViewCommands()),
                new CommandViewModel("Help", CreateAboutCommands()),
                new CommandViewModel("Themes", CreateThemesCommands())
            };
        }
        DocumentViewModel CreateDocumentViewModel() {
            return CreatePanelWorkspaceViewModel<DocumentViewModel>();
        }
        List<CommandViewModel> CreateFileCommands() {
            var fileExecutedCommand = new DelegateCommand<object>(OnNewFileExecuted);
            var fileOpenCommand = new DelegateCommand<object>(OnFileOpenExecuted);

            CommandViewModel newCommand = new CommandViewModel("New") { IsSubItem = true };
            newProject = new CommandViewModel("Project...", fileExecutedCommand) { Glyph = Images.NewProject, KeyGesture = new KeyGesture(Key.N, ModifierKeys.Control | ModifierKeys.Shift), IsEnabled = false };
            newFile = new CommandViewModel("New File", fileExecutedCommand) { Glyph = Images.File, KeyGesture = new KeyGesture(Key.N, ModifierKeys.Control) };
            newCommand.Commands = new List<CommandViewModel>() { newProject, newFile };

            CommandViewModel openCommand = new CommandViewModel("Open") { IsSubItem = true, };
            openProject = new CommandViewModel("Project/Solution...") {
                Glyph = Images.OpenSolution,
                IsEnabled = false,
                KeyGesture = new KeyGesture(Key.O, ModifierKeys.Control | ModifierKeys.Shift),
            };
            openFile = new CommandViewModel("Open File", fileOpenCommand) { Glyph = Images.OpenFile, KeyGesture = new KeyGesture(Key.O, ModifierKeys.Control) };
            openCommand.Commands = new List<CommandViewModel>() { openProject, openFile };

            CommandViewModel closeFile = new CommandViewModel("Close");
            CommandViewModel closeSolution = new CommandViewModel("Close Solution") { Glyph = Images.CloseSolution };
            save = new CommandViewModel("Save") { Glyph = Images.Save, KeyGesture = new KeyGesture(Key.S, ModifierKeys.Control) };
            saveAll = new CommandViewModel("Save All") { Glyph = Images.SaveAll, KeyGesture = new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift) };

            return new List<CommandViewModel>() { newCommand, openCommand, GetSeparator(), closeFile, closeSolution, GetSeparator(), save, saveAll };
        }
        List<CommandViewModel> CreateToolbarCommands() {
            CommandViewModel start = new CommandViewModel("Start") {
                Glyph = Images.Run,
                KeyGesture = new KeyGesture(Key.F5, ModifierKeys.None),
                DisplayMode = BarItemDisplayMode.ContentAndGlyph
            };
            CommandViewModel combo = new CommandViewModel("Configuration") { IsSubItem = true, IsComboBox = true };
            combo.Commands = new List<CommandViewModel>() { new CommandViewModel("Debug"), new CommandViewModel("Release") };

            CommandViewModel cut = new CommandViewModel("Cut") { Glyph = Images.Cut, KeyGesture = new KeyGesture(Key.X, ModifierKeys.Control) };
            CommandViewModel copy = new CommandViewModel("Copy") { Glyph = Images.Copy, KeyGesture = new KeyGesture(Key.C, ModifierKeys.Control) };
            CommandViewModel paste = new CommandViewModel("Paste") { Glyph = Images.Paste, KeyGesture = new KeyGesture(Key.V, ModifierKeys.Control) };

            CommandViewModel undo = new CommandViewModel("Undo") { Glyph = Images.Undo, KeyGesture = new KeyGesture(Key.Z, ModifierKeys.Control) };
            CommandViewModel redo = new CommandViewModel("Redo") { Glyph = Images.Redo, KeyGesture = new KeyGesture(Key.Y, ModifierKeys.Control) };

            return new List<CommandViewModel>() {
                newProject, newFile, openFile, save, saveAll, GetSeparator(), combo, start,
                GetSeparator(), cut, copy, paste, GetSeparator(), undo, redo, GetSeparator(),
                toolbox, solutionExplorer, properties, errorList, output, searchResults,
                GetSeparator(), loadLayout, saveLayout
            };
        }
        DocumentViewModel GetDocument(string filePath) {
            return Workspaces.OfType<DocumentViewModel>().FirstOrDefault(x => x.FilePath == filePath);
        }
        CommandViewModel GetSeparator() {
            return new CommandViewModel() { IsSeparator = true };
        }
        CommandViewModel GetShowCommand(PanelWorkspaceViewModel viewModel) {
            return new CommandViewModel(viewModel, new DelegateCommand(() => OpenOrCloseWorkspace(viewModel)));
        }
        void InitDefaultLayout() {
            var panels = new List<PanelWorkspaceViewModel> { ToolboxViewModel, SolutionExplorerViewModel, PropertiesViewModel, ErrorListViewModel };
            foreach(var panel in panels) {
                OpenOrCloseWorkspace(panel, false);
            }
        }
        void OnFileOpenExecuted(object param) {
            var document = CreateDocumentViewModel();
            if(!document.OpenFile() || ActivateDocument(document.FilePath)) {
                document.Dispose();
                return;
            }
            OpenOrCloseWorkspace(document);
        }
        void OnLoadLayout() {
            SaveLoadLayoutService.LoadLayout();
        }
        void OnNewFileExecuted(object param) {
            string newItemName = solutionExplorerViewModel.Solution.AddNewItemToRoot();
            OpenItem(newItemName);
        }
        void OnSaveLayout() {
            SaveLoadLayoutService.SaveLayout();
        }
        void OnWorkspaceRequestClose(object sender, EventArgs e) {
            var workspace = sender as PanelWorkspaceViewModel;
            if(workspace != null) {
                workspace.IsClosed = true;
                if(workspace is DocumentViewModel) {
                    workspace.Dispose();
                    Workspaces.Remove(workspace);
                }
            }
        }
        void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if(e.NewItems != null && e.NewItems.Count != 0)
                foreach(WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += OnWorkspaceRequestClose;
            if(e.OldItems != null && e.OldItems.Count != 0)
                foreach(WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= OnWorkspaceRequestClose;
        }
        void OpenItem(string filePath) {
            if(ActivateDocument(filePath)) return;
            lastOpenedItem = CreateDocumentViewModel();
            lastOpenedItem.OpenItemByPath(filePath);
            OpenOrCloseWorkspace(lastOpenedItem);
        }
        void SetActiveWorkspace(WorkspaceViewModel workspace) {
            workspace.IsActive = true;
        }
        void ShowAbout() {
            About.ShowAbout(ProductKind.DXperienceWPF);
        }
        void SolutionExplorerViewModel_ItemOpening(object sender, SolutionItemOpeningEventArgs e) {
            OpenItem(e.SolutionItem.FilePath);
        }
    }
    abstract public class PanelWorkspaceViewModel : WorkspaceViewModel, IMVVMDockingProperties {
        string _targetName;

        protected PanelWorkspaceViewModel() {
            _targetName = WorkspaceName;
        }

        abstract protected string WorkspaceName { get; }
        string IMVVMDockingProperties.TargetName {
            get { return _targetName; }
            set { _targetName = value; }
        }

        public virtual void OpenItemByPath(string path) { }
    }
    public abstract class WorkspaceViewModel : ViewModel {
        protected WorkspaceViewModel() {
            IsClosed = true;
        }

        public event EventHandler RequestClose;

        public virtual bool IsActive { get; set; }
        [BindableProperty(OnPropertyChangedMethodName = "OnIsClosedChanged")]
        public virtual bool IsClosed { get; set; }
        public virtual bool IsOpened { get; set; }

        public void Close() {
            EventHandler handler = RequestClose;
            if(handler != null)
                handler(this, EventArgs.Empty);
        }
        protected virtual void OnIsClosedChanged() {
            IsOpened = !IsClosed;
        }
    }

    public abstract class ViewModel : IDisposable {
        public string BindableName { get { return GetBindableName(DisplayName); } }
        public virtual string DisplayName { get; protected set; }
        public virtual ImageSource Glyph { get; set; }

        string GetBindableName(string name) { return "_" + Regex.Replace(name, @"\W", ""); }

        #region IDisposable Members
        public void Dispose() {
            OnDispose();
        }
        protected virtual void OnDispose() { }
#if DEBUG
        ~ViewModel() {
            string msg = string.Format("{0} ({1}) ({2}) Finalized", GetType().Name, DisplayName, GetHashCode());
            System.Diagnostics.Debug.WriteLine(msg);
        }
#endif
        #endregion 
    }

    #region Tool Panels
    public class ErrorListViewModel : PanelWorkspaceViewModel {
        public ErrorListViewModel() {
            DisplayName = "Error List";
            Glyph = Images.TaskList;
            Error = Images.Error;
            Warning = Images.Warning;
            Info = Images.Info;
        }

        public ImageSource Error { get; set; }
        public ImageSource Info { get; set; }
        public ImageSource Warning { get; set; }
        protected override string WorkspaceName { get { return "BottomHost"; } }
    }

    public class OutputViewModel : PanelWorkspaceViewModel {
        public OutputViewModel() {
            DisplayName = "Output";
            Glyph = Images.Output;
            Text = @"1>------ Build started: Project: VisualStudioInspiredUIDemo, Configuration: Debug Any CPU ------
1>  DockingDemo -> C:\VisualStudioInspiredUIDemo.exe
========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========";
        }

        public string Text { get; private set; }
        protected override string WorkspaceName { get { return "BottomHost"; } }
    }

    public class PropertiesViewModel : PanelWorkspaceViewModel {
        public PropertiesViewModel() {
            DisplayName = "Properties";
            Glyph = Images.PropertiesWindow;
            SelectedItem = new PropertyItem(new PropertyGridControl());
            Items = new List<PropertyItem> {
                SelectedItem,
                new PropertyItem(new AccordionControl()),
                new PropertyItem(new DocumentPanel()),
                new PropertyItem(new DocumentGroup()),
                new PropertyItem(new DevExpress.Xpf.Docking.LayoutPanel())
            };
        }

        public List<PropertyItem> Items { get; set; }
        public virtual PropertyItem SelectedItem { get; set; }
        protected override string WorkspaceName { get { return "RightHost"; } }
    }

    public class PropertyItem {
        public PropertyItem(object data) {
            Data = data;
            Name = Data.ToString();
        }

        public object Data { get; set; }
        public string Name { get; set; }
    }

    public class SearchResultsViewModel : PanelWorkspaceViewModel {
        public SearchResultsViewModel() {
            DisplayName = "Search Results";
            Glyph = Images.FindInFilesWindow;
            Text = @"Matching lines: 0    Matching files: 0    Total files searched: 61";
        }

        public string Text { get; private set; }
        protected override string WorkspaceName { get { return "BottomHost"; } }
    }

    public class Solution : BindableBase {
        string[] codePaths = new string[] {
            "MainWindow.xaml",
            "MainWindow.xaml.cs",
            "Resources.xaml",
            "BarTemplateSelector.cs",
            "MainViewModel.cs"
        };
        int newItemsCount;

        protected Solution() {
            SolutionItem root = SolutionItem.Create(this, "WPFApplication", ImagePaths.SolutionExplorer);
            SolutionItem properties = SolutionItem.Create(this, "Properties", ImagePaths.PropertiesWindow);
            SolutionItem references = SolutionItem.Create(this, "References", ImagePaths.References);
            root.Items.Add(properties);
            root.Items.Add(references);
            var files = GetCodeFiles();
            foreach(SolutionItem file in files) {
                root.Items.Add(file);
            }
            LastOpenedItem = files.FirstOrDefault();
            Items = new ObservableCollection<SolutionItem> { root };
        }

        public ObservableCollection<SolutionItem> Items { get; private set; }
        public virtual SolutionItem LastOpenedItem { get; set; }

        public static Solution Create() {
            return ViewModelSource.Create(() => new Solution());
        }
        public string AddNewItemToRoot() {
            newItemsCount++;
            string newItemName = string.Format("Class{0}.cs", newItemsCount);
            var solutionItem = SolutionItem.CreateFile(this, newItemName);
            (Items[0] as SolutionItem).Items.Add(solutionItem);
            return newItemName;
        }
        List<SolutionItem> GetCodeFiles() {
            var result = new List<SolutionItem>();
            var subFiles = new List<SolutionItem>();
            foreach(var codePath in codePaths)
                if(codePath.EndsWith(".xaml.cs") || codePath.EndsWith(".xaml.vb"))
                    subFiles.Add(SolutionItem.CreateFile(this, codePath));
                else
                    result.Add(SolutionItem.CreateFile(this, codePath));
            foreach(var subFile in subFiles) {
                var xamlFile = result.FirstOrDefault(x => x.FilePath == subFile.FilePath.Replace(".xaml.cs", ".xaml").Replace(".xaml.vb", ".xaml"));
                if(xamlFile == null)
                    result.Add(subFile);
                else
                    xamlFile.Items.Add(subFile);
            }
            return result;
        }
    }

    public class SolutionExplorerViewModel : PanelWorkspaceViewModel {
        public SolutionExplorerViewModel() {
            DisplayName = "Solution Explorer";
            Glyph = Images.SolutionExplorer;
            PropertiesWindow = Images.PropertiesWindow;
            ShowAllFiles = Images.ShowAllFiles;
            Refresh = Images.Refresh;
        }

        public event EventHandler<SolutionItemOpeningEventArgs> ItemOpening;

        public ImageSource PropertiesWindow { get; set; }
        public ImageSource Refresh { get; set; }
        public ImageSource ShowAllFiles { get; set; }
        public Solution Solution { get; set; }
        protected override string WorkspaceName { get { return "RightHost"; } }

        public void OpenItem(SolutionItem item) {
            if(item != null && item.IsFile && ItemOpening != null)
                ItemOpening.Invoke(this, new SolutionItemOpeningEventArgs(item));
        }
    }

    public class SolutionItem {
        readonly Solution solution;

        protected SolutionItem(Solution solution) {
            this.solution = solution;
            Items = new ObservableCollection<SolutionItem>();
        }

        public string DisplayName { get; private set; }
        public string FilePath { get; private set; }
        public string GlyphPath { get; private set; }
        public bool IsFile { get { return FilePath != null; } }
        public ObservableCollection<SolutionItem> Items { get; private set; }

        public static SolutionItem Create(Solution solution, string displayName, string glyph) {
            var solutionItem = ViewModelSource.Create(() => new SolutionItem(solution));
            solutionItem.Do(x => {
                x.DisplayName = displayName;
                x.GlyphPath = glyph;
            });
            return solutionItem;
        }
        public static SolutionItem CreateFile(Solution solution, string path) {
            var solutionItem = ViewModelSource.Create(() => new SolutionItem(solution));
            solutionItem.Do(x => {
                x.DisplayName = Path.GetFileName(path);
                x.GlyphPath = Path.GetExtension(path) == ".cs" ? ImagePaths.FileCS : ImagePaths.FileXaml;
                x.FilePath = path;
            });
            return solutionItem;
        }
    }

    public class SolutionItemOpeningEventArgs : EventArgs {
        public SolutionItemOpeningEventArgs(SolutionItem solutionItem) {
            SolutionItem = solutionItem;
        }

        public SolutionItem SolutionItem { get; set; }
    }

    public class ToolboxViewModel : PanelWorkspaceViewModel {
        public ToolboxViewModel() {
            DisplayName = "Toolbox";
            Glyph = Images.Toolbox;
        }

        protected override string WorkspaceName { get { return "Toolbox"; } }
    }
    #endregion

    #region Bars
    public class BarModel : ViewModel {
        public BarModel(string displayName) {
            DisplayName = displayName;
        }
        public List<CommandViewModel> Commands { get; set; }
        public bool IsMainMenu { get; set; }
    }

    public class CommandViewModel : ViewModel {
        public CommandViewModel() { }
        public CommandViewModel(string displayName, List<CommandViewModel> subCommands)
            : this(displayName, null, null, subCommands) {
        }
        public CommandViewModel(string displayName, ICommand command = null)
            : this(displayName, null, command, null) {
        }
        public CommandViewModel(WorkspaceViewModel owner, ICommand command)
            : this(string.Empty, owner, command) {
        }
        private CommandViewModel(string displayName, WorkspaceViewModel owner = null, ICommand command = null, List<CommandViewModel> subCommands = null) {
            IsEnabled = true;
            Owner = owner;
            if(Owner != null) {
                DisplayName = Owner.DisplayName;
                Glyph = Owner.Glyph;
            } else DisplayName = displayName;
            Command = command;
            Commands = subCommands;
        }

        public ICommand Command { get; private set; }
        public List<CommandViewModel> Commands { get; set; }
        public BarItemDisplayMode DisplayMode { get; set; }
        public bool IsComboBox { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsSeparator { get; set; }
        public bool IsSubItem { get; set; }
        public KeyGesture KeyGesture { get; set; }
        public WorkspaceViewModel Owner { get; private set; }
    }
    #endregion

    #region Images
    public static class ImagePaths {        
        public const string About = folderSvg + "About.svg";        
        public const string CloseSolution = folderSvg + "CloseSolution.svg";        
        public const string Copy = folderSvg + "Copy.svg";        
        public const string Cut = folderSvg + "Cut.svg";
        public const string DevExpressLogo = folder + "DevExpressLogo.png";        
        public const string Error = folderSvg + "Error.svg";        
        public const string File = folderSvg + "File.svg";        
        public const string FileCS = folderSvg + "FileCS.svg";        
        public const string FileXaml = folderSvg + "FileXaml.svg";        
        public const string Find = folderSvg + "Find.svg";        
        public const string FindInFiles = folderSvg + "FindInFiles.svg";        
        public const string FindInFilesWindow = folderSvg + "FindInFilesWindow.svg";        
        public const string Info = folderSvg + "Info.svg";        
        public const string LoadLayout = folderSvg + "LoadLayout.svg";        
        public const string NewProject = folderSvg + "NewProject.svg";        
        public const string OpenFile = folderSvg + "OpenFile.svg";
        public const string OpenSolution = folderSvg + "OpenSolution.svg";        
        public const string Output = folderSvg + "Output.svg";        
        public const string Paste = folderSvg + "Paste.svg";        
        public const string PropertiesWindow = folderSvg + "PropertiesWindow.svg";        
        public const string Redo = folderSvg + "Redo.svg";        
        public const string References = folderSvg + "References.svg";        
        public const string Refresh = folderSvg + "Refresh.svg";        
        public const string Replace = folderSvg + "Replace.svg";        
        public const string Run = folderSvg + "Run.svg";        
        public const string Save = folderSvg + "Save.svg";       
        public const string SaveAll = folderSvg + "SaveAll.svg";        
        public const string SaveLayout = folderSvg + "SaveLayout.svg";        
        public const string ShowAllFiles = folderSvg + "ShowAllFiles.svg";        
        public const string SolutionExplorer = folderSvg + "SolutionExplorer.svg";        
        public const string TaskList = folderSvg + "TaskList.svg";        
        public const string Toolbox = folderSvg + "Toolbox.svg";       
        public const string Undo = folderSvg + "Undo.svg";        
        public const string Warning = folderSvg + "Warning.svg";
        const string folderSvg = "pack://application:,,,/VisualStudioDocking;component/Images/Docking/";
        const string folder = "/VisualStudioDocking;component/Images/Docking/";        
    }
    
    public static class Images {
        public static ImageSource About { get { return GetSvgImage(ImagePaths.About); } }
        public static ImageSource CloseSolution { get { return GetSvgImage(ImagePaths.CloseSolution); } }
        public static ImageSource Copy { get { return GetSvgImage(ImagePaths.Copy); } }
        public static ImageSource Cut { get { return GetSvgImage(ImagePaths.Cut); } }
        public static ImageSource DevExpressLogo { get { return GetImage(ImagePaths.DevExpressLogo); } }
        public static ImageSource Error { get { return GetSvgImage(ImagePaths.Error); } }
        public static ImageSource File { get { return GetSvgImage(ImagePaths.File); } }
        public static ImageSource FileCS { get { return GetSvgImage(ImagePaths.FileCS); } }
        public static ImageSource FileXaml { get { return GetSvgImage(ImagePaths.FileXaml); } }
        public static ImageSource Find { get { return GetSvgImage(ImagePaths.Find); } }
        public static ImageSource FindInFiles { get { return GetSvgImage(ImagePaths.FindInFiles); } }
        public static ImageSource FindInFilesWindow { get { return GetSvgImage(ImagePaths.FindInFilesWindow); } }
        public static ImageSource Info { get { return GetSvgImage(ImagePaths.Info); } }
        public static ImageSource LoadLayout { get { return GetSvgImage(ImagePaths.LoadLayout); } }
        public static ImageSource NewProject { get { return GetSvgImage(ImagePaths.NewProject); } }        
        public static ImageSource OpenFile { get { return GetSvgImage(ImagePaths.OpenFile); } }
        public static ImageSource OpenSolution { get { return GetSvgImage(ImagePaths.OpenSolution); } }
        public static ImageSource Output { get { return GetSvgImage(ImagePaths.Output); } }
        public static ImageSource Paste { get { return GetSvgImage(ImagePaths.Paste); } }
        public static ImageSource PropertiesWindow { get { return GetSvgImage(ImagePaths.PropertiesWindow); } }
        public static ImageSource Redo { get { return GetSvgImage(ImagePaths.Redo); } }
        public static ImageSource References { get { return GetSvgImage(ImagePaths.References); } }
        public static ImageSource Refresh { get { return GetSvgImage(ImagePaths.Refresh); } }
        public static ImageSource Replace { get { return GetSvgImage(ImagePaths.Replace); } }
        public static ImageSource Run { get { return GetSvgImage(ImagePaths.Run); } }
        public static ImageSource Save { get { return GetSvgImage(ImagePaths.Save); } }
        public static ImageSource SaveAll { get { return GetSvgImage(ImagePaths.SaveAll); } }
        public static ImageSource SaveLayout { get { return GetSvgImage(ImagePaths.SaveLayout); } }
        public static ImageSource ShowAllFiles { get { return GetSvgImage(ImagePaths.ShowAllFiles); } }
        public static ImageSource SolutionExplorer { get { return GetSvgImage(ImagePaths.SolutionExplorer); } }
        public static ImageSource TaskList { get { return GetSvgImage(ImagePaths.TaskList); } }
        public static ImageSource Toolbox { get { return GetSvgImage(ImagePaths.Toolbox); } }
        public static ImageSource Undo { get { return GetSvgImage(ImagePaths.Undo); } }
        public static ImageSource Warning { get { return GetSvgImage(ImagePaths.Warning); } }

        static ImageSource GetImage(string path) {
            return new BitmapImage(new Uri(path, UriKind.Relative));
        }

        static ImageSource GetSvgImage(string path) {
            var svgImageSource = new SvgImageSourceExtension() { Uri = new Uri(path) }.ProvideValue(null);
            return (ImageSource)svgImageSource;
        }
    }
    #endregion
}
