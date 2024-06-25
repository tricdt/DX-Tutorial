using System;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Charts.Designer;
using DevExpress.Charts.Native;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Ribbon;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Native;

namespace ChartsDemo {
    public partial class ChartsDemoRibbon : UserControl {
        public static readonly DependencyProperty SelectedPageOnMergingProperty;
        static ChartsDemoRibbon() {
            SelectedPageOnMergingProperty = DependencyProperty.Register(
                "SelectedPageOnMerging", typeof(SelectedPageOnMerging), typeof(ChartsDemoRibbon), new PropertyMetadata(SelectedPageOnMerging.SelectedPage));
        }
        
        WeakReference chartReference;

        public ChartsDemoRibbon() {
            InitializeComponent();
            DataContext = ViewModelSource.Create(() => new ChartsDemoRibbonViewModel(this));
        }

        public ChartControlBase Chart {
            get {
                if (this.chartReference == null)
                    return null;
                return this.chartReference.IsAlive ? (ChartControlBase)this.chartReference.Target : null;
            }
            set {
                this.chartReference = new WeakReference(value);
            }
        }
        public SelectedPageOnMerging SelectedPageOnMerging {
            get { return (SelectedPageOnMerging)GetValue(SelectedPageOnMergingProperty); }
            set { SetValue(SelectedPageOnMergingProperty, value); }
        }
        public bool ShowPaletteButton {
            get { return ((ChartsDemoRibbonViewModel)DataContext).ShowPaletteButton; }
            set { ((ChartsDemoRibbonViewModel)DataContext).ShowPaletteButton = value; }
        }
        public bool ShowRunChartDesignerButton {
            get { return ((ChartsDemoRibbonViewModel)DataContext).ShowRunChartDesignerButton; }
            set { ((ChartsDemoRibbonViewModel)DataContext).ShowRunChartDesignerButton = value; }
        }

        #region Nested classes: PalettesViewModel, PaletteViewModel
        public class ChartsDemoRibbonViewModel : ViewModelBase {
            const string sTR_DXChart = "Exported DXChart";
            readonly WeakReference owner;
            bool showPaletteButton = true;
            bool showChartGroup = true;
            bool showRunChartDesignerButton = false;
            protected void CheckParentModel() {
                if (((ISupportParentViewModel)this).ParentViewModel == null)
                    ((ISupportParentViewModel)this).ParentViewModel = DevExpress.Xpf.Core.Native.LayoutHelper.FindAmongParents<MainWindow>((DependencyObject)owner.Target, null).DataContext;
            }
            ISaveFileDialogService SaveFileDialogService {
                get {
                    CheckParentModel();
                    return GetService<ISaveFileDialogService>(); }
            }
            IMessageBoxService MessageBoxService {
                get {
                    CheckParentModel();
                    return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); }
            }

            public ChartControlBase Chart { get { return OwnerChartsDemoRibbon.Chart as ChartControlBase; } }
            public ChartsDemoRibbon OwnerChartsDemoRibbon {
                get { return this.owner.IsAlive ? (ChartsDemoRibbon)this.owner.Target : null; }
            }
            public DelegateCommand RunChartDesignerCommand {
                get;
                private set;
            }
            public DelegateCommand ExportToImageCommand {
                get;
                private set;
            }
            public DelegateCommand ExportToXlsCommand {
                get;
                private set;
            }
            public DelegateCommand ExportToDocxCommand {
                get;
                private set;
            }
            public DelegateCommand ExportToXlsxCommand {
                get;
                private set;
            }
            public DelegateCommand ExportToMhtCommand {
                get;
                private set;
            }
            public DelegateCommand ExportToHtmlCommand {
                get;
                private set;
            }
            public DelegateCommand ExportToPdfCommand {
                get;
                private set;
            }
            public DelegateCommand ExportToRtfCommand {
                get;
                private set;
            }
            public DelegateCommand ExportToXpsCommand {
                get;
                private set;
            }
            public bool ShowPaletteButton {
                get { return this.showPaletteButton; }
                set {
                    if (this.showPaletteButton != value) {
                        SetProperty(ref this.showPaletteButton, value, () => ShowPaletteButton);
                        SetChartGroupVisibility();
                    }
                }
            }
            public bool ShowChartGroup {
                get { return this.showChartGroup; }
                set {
                    if (this.showChartGroup != value)
                        SetProperty(ref this.showChartGroup, value, () => ShowChartGroup);
                }
            }
            public bool ShowRunChartDesignerButton {
                get { return this.showRunChartDesignerButton; }
                set {
                    if (this.showRunChartDesignerButton != value) {
                        SetProperty(ref this.showRunChartDesignerButton, value, () => ShowRunChartDesignerButton);
                        SetChartGroupVisibility();
                    }
                }
            }
            public bool ShowExportButton {
                get { return true; }
            }

            public ChartsDemoRibbonViewModel(ChartsDemoRibbon cdr) {
                this.owner = new WeakReference(cdr);
                RunChartDesignerCommand = new DelegateCommand(() => {
                    ChartControl chartControl = OwnerChartsDemoRibbon.Chart as ChartControl;
                    if (chartControl == null) {
                        ChartDebug.WriteWarning("ChartDemoRibbon can't run the designer because the Chart is null or it is a 3D Chart Control.");
                        return;
                    }
                    ChartDesigner designer = new ChartDesigner(chartControl);
                    Window window = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<Window>(OwnerChartsDemoRibbon);
                    designer.Show(window);
                });
                ExportToImageCommand = new DelegateCommand(() => {
                    ChartControlBase chartControl = OwnerChartsDemoRibbon.Chart;
                    if (chartControl == null) {
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.");
                        return;
                    }
                    SaveFileDialogService.DefaultFileName = sTR_DXChart;
                    SaveFileDialogService.Filter = "BMP Image|*.BMP|Gif Image|*.GIF|JPEG Image|*.JPG|PNG Image|*.PNG|TIFF Image|*.TIFF";
                    var dialogResult = SaveFileDialogService.ShowDialog();
                    if (!dialogResult)
                        return;
                    else {
                        ImageExportOptions options = new ImageExportOptions();
                        switch (SaveFileDialogService.FilterIndex) {
                            case 1:
                                options.Format = ImageFormat.Bmp;
                                break;
                            case 2:
                                options.Format = ImageFormat.Gif;
                                break;
                            case 3:
                                options.Format = ImageFormat.Jpeg;
                                break;
                            case 4:
                                options.Format = ImageFormat.Png;
                                break;
                            case 5:
                                options.Format = ImageFormat.Tiff;
                                break;
                            default:
                                MessageBoxService.ShowMessage("The selected format is not supported.", "Error", MessageButton.OK, MessageIcon.Error);
                                return;
                        }
                        var fileName = SaveFileDialogService.GetFullFileName();
                        chartControl.ExportToImage(fileName, options);
                        AskAndOpenResultFile(fileName);

                    }
                });
                ExportToXlsxCommand = new DelegateCommand(() => {
                    ChartControlBase chartControl = OwnerChartsDemoRibbon.Chart;
                    if (chartControl == null) {
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.");
                        return;
                    }
                    SaveFileDialogService.DefaultFileName = sTR_DXChart;
                    SaveFileDialogService.Filter = "XLSX Document|*.XLSX";
                    var dialogResult = SaveFileDialogService.ShowDialog();
                    if (!dialogResult)
                        return;
                    else {
                        var fileName = SaveFileDialogService.GetFullFileName();
                        chartControl.ExportToXlsx(fileName);
                        AskAndOpenResultFile(fileName);

                    }
                });
                ExportToXlsCommand = new DelegateCommand(() => {
                    ChartControlBase chartControl = OwnerChartsDemoRibbon.Chart;
                    if (chartControl == null) {
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.");
                        return;
                    }
                    SaveFileDialogService.DefaultFileName = sTR_DXChart;
                    SaveFileDialogService.Filter = "XLS Document|*.XLS";
                    var dialogResult = SaveFileDialogService.ShowDialog();
                    if (!dialogResult)
                        return;
                    else {
                        var fileName = SaveFileDialogService.GetFullFileName();
                        chartControl.ExportToXls(fileName);
                        AskAndOpenResultFile(fileName);
                    }
                });
                ExportToDocxCommand = new DelegateCommand(() => {
                    ChartControlBase chartControl = OwnerChartsDemoRibbon.Chart;
                    if (chartControl == null) {
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.");
                        return;
                    }
                    SaveFileDialogService.DefaultFileName = sTR_DXChart;
                    SaveFileDialogService.Filter = "DOCX Document|*.DOCX";
                    var dialogResult = SaveFileDialogService.ShowDialog();
                    if (!dialogResult)
                        return;
                    else {
                        var fileName = SaveFileDialogService.GetFullFileName();
                        chartControl.ExportToDocx(fileName);
                        AskAndOpenResultFile(fileName);
                    }

                });
                ExportToMhtCommand = new DelegateCommand(() => {
                    ChartControlBase chartControl = OwnerChartsDemoRibbon.Chart;
                    if (chartControl == null) {
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.");
                        return;
                    }
                    SaveFileDialogService.DefaultFileName = sTR_DXChart;
                    SaveFileDialogService.Filter = "MHT Document|*.MHT";
                    var dialogResult = SaveFileDialogService.ShowDialog();
                    if (!dialogResult)
                        return;
                    else {
                        var fileName = SaveFileDialogService.GetFullFileName();
                        chartControl.ExportToMht(fileName);
                        AskAndOpenResultFile(fileName);
                    }

                });
                ExportToHtmlCommand = new DelegateCommand(() => {
                    ChartControlBase chartControl = OwnerChartsDemoRibbon.Chart;
                    if (chartControl == null) {
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.");
                        return;
                    }
                    SaveFileDialogService.DefaultFileName = sTR_DXChart;
                    SaveFileDialogService.Filter = "HTML Document|*.HTML";
                    var dialogResult = SaveFileDialogService.ShowDialog();
                    if (!dialogResult)
                        return;
                    else {
                        var fileName = SaveFileDialogService.GetFullFileName();
                        chartControl.ExportToHtml(fileName);
                        AskAndOpenResultFile(fileName);
                    }
                });
                ExportToPdfCommand = new DelegateCommand(() => {
                    ChartControlBase chartControl = OwnerChartsDemoRibbon.Chart;
                    if (chartControl == null) {
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.");
                        return;
                    }
                    SaveFileDialogService.DefaultFileName = sTR_DXChart;
                    SaveFileDialogService.Filter = "PDF Document|*.PDF";
                    var dialogResult = SaveFileDialogService.ShowDialog();
                    if (!dialogResult)
                        return;
                    else {
                        var fileName = SaveFileDialogService.GetFullFileName();
                        chartControl.ExportToPdf(fileName);
                        AskAndOpenResultFile(fileName);
                    }
                });
                ExportToRtfCommand = new DelegateCommand(() => {
                    ChartControlBase chartControl = OwnerChartsDemoRibbon.Chart;
                    if (chartControl == null) {
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.");
                        return;
                    }
                    SaveFileDialogService.DefaultFileName = sTR_DXChart;
                    SaveFileDialogService.Filter = "RTF Document|*.RTF";
                    var dialogResult = SaveFileDialogService.ShowDialog();
                    if (!dialogResult)
                        return;
                    else {
                        var fileName = SaveFileDialogService.GetFullFileName();
                        chartControl.ExportToRtf(fileName);
                        AskAndOpenResultFile(fileName);
                    }
                });
                ExportToXpsCommand = new DelegateCommand(() => {
                    ChartControlBase chartControl = OwnerChartsDemoRibbon.Chart;
                    if (chartControl == null) {
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.");
                        return;
                    }
                    SaveFileDialogService.DefaultFileName = sTR_DXChart;
                    SaveFileDialogService.Filter = "XPS Document|*.XPS";
                    var dialogResult = SaveFileDialogService.ShowDialog();
                    if (!dialogResult)
                        return;
                    else {
                        var fileName = SaveFileDialogService.GetFullFileName();
                        chartControl.ExportToXps(fileName);
                        AskAndOpenResultFile(fileName);
                    }
                });
            }

            private void AskAndOpenResultFile(string fileName) {
                if (MessageBoxService.ShowMessage("Do you want to open the result file?", string.Empty, MessageButton.YesNo, MessageIcon.Question) == MessageResult.Yes)
                    ProcessLaunchHelper.StartConfirmed(fileName);
            }

            void SetChartGroupVisibility() {
                ShowChartGroup = ShowPaletteButton || ShowRunChartDesignerButton || ShowExportButton;
            }
        }
        #endregion
       
    }
    public class MarkedRibbonControl : RibbonControl { 

    }
}
