using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Core.Tests;
using DevExpress.Xpf.DemoBase.DemoTesting;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace ChartsDemo.Tests {
    public class ChartDemoModuleAccessor : DemoModulesAccessor<ChartsDemoModule> {
        public ChartDemoModuleAccessor(BaseDemoTestingFixture fixture) : base(fixture) {
        }
        public ChartsDemoModule ChartModule { get { return base.DemoModule; } }
    }

    public class ChartsCheckAllDemosFixture : CheckAllDemosFixture {
        readonly ChartDemoModuleAccessor modulesAccessor;

        public ChartsCheckAllDemosFixture() {
            modulesAccessor = new ChartDemoModuleAccessor(this);
        }
        void WaitAnimationComplete(ChartControlBase chart, FrameworkElement demoModuleOrTab) {
            DispatchBusyWait(() => chart.IsAnimationCompleted, "charts animation");
            TabItemModule tabItemModule = demoModuleOrTab as TabItemModule;
            if (tabItemModule != null) DispatchBusyWait(() => tabItemModule.IsAnimationCompleted, "charts demo tab animation");
            DispatcherTimer timer = new DispatcherTimer();
            for (int i = 0; i < 10; i++)
                DispatchBusyWait(() => true, "charts update layout");
        }
        void CreateTabsActions() {
            Dispatch(delegate () {
                var tabControl = LayoutHelper.FindElement(modulesAccessor.ChartModule, element => element is DXTabControl) as DXTabControl;
                if (tabControl != null) {
                    foreach (int i in Enumerable.Range(1, tabControl.Items.Count - 1)) {
                        var prevTabPage = DispatchExpr(() => tabControl.SelectedContainer);
                        Dispatch(() => tabControl.SelectedIndex = i);
                        DispatchBusyWait(() => tabControl.SelectedContainer != prevTabPage, "demo tab");
                        if (tabControl.SelectedItemContent != null)
                            ExportDemoToImage(tabControl.SelectedItemContent as FrameworkElement,
                                              tabControl.SelectedItemContent.GetType());
                    }
                }
                else ExportDemoToImage(modulesAccessor.ChartModule, modulesAccessor.ChartModule.GetType());
            });
        }
        protected void ExportDemoToImage(FrameworkElement root, Type demoModule) {
            try {
                var chartControl = LayoutHelper.FindElement(root, element => element is ChartControlBase) as ChartControlBase;
                WaitAnimationComplete(chartControl, root);

                string demoModuleName = demoModule.Name;
                string actualImagePath = GetExportImagePath(demoModuleName);
                if (chartControl != null) {
                    if (File.Exists(actualImagePath))
                        File.Delete(actualImagePath);
                    chartControl.ExportToImage(actualImagePath);
                    CompareImages(actualImagePath, GetExpectedImagePath(demoModuleName));
                }
            }
            catch (Exception) {
            }
        }
        const string sharedFolderPath = @"\\corp\internal\common\4Kalachik\DemoTesting\";
        protected string GetExportImagePath(string demoModuleName) {
            string fileName = demoModuleName + ".png";
            return Path.Combine(string.Format(@"{0}WpfCharts{1}ActualImages", sharedFolderPath, AssemblyInfo.VersionId), fileName);

        }
        protected string GetExpectedImagePath(string demoModuleName) {
            string fileName = demoModuleName + ".png";
            return Path.Combine(string.Format(@"{0}WpfCharts{1}ExpectedImages", sharedFolderPath, AssemblyInfo.VersionId), fileName);
        }
        string[] stopList = new string[]
        {"RealTimeChartDemo",
                                "MvvmFinancialChartingDemo",
                                "Point3DTab",
                                "Bubble3DTab",
                                "Area3DTab",
                                "StackedArea3DTab",
                                "FullStackedArea3DTab",
                                "Pie3DTab",
                                "LegendsDemo",
                                "FinancialIndicatorsDemo",
                                "MovingAverageAndRegressionLineDemo",
                                "Charting3dDemo",
                                "DrillDownDemo",
                                "LargeSeriesNumberDemo",
                                "AdvancedCustomizationDemo",
                                "FullStackedBarTab",
                                "SideBySideStackedBarTab",
                                "SideBySideFullStackedBarTab",
                                "BubbleTab",
                                "RangeAreaTab",
                                "FunnelTab",
                                "ConstantLinesAndStripsDemo",
                                "SimpleDiagramTitlesTab",
                                "SeriesTemplateBindingDemo",
                                "BindingIndividualSeriesDemo",
                                "EmptyPointsDemo",
                                "ErrorBarsDemo",
                                "TooltipAndCrosshairCursorDemo",
                                "RealTime3DChartDemo",
                                "Bar3DSeriesViewTab",
                                "Bubble3DSeriesViewTab",
                                "PointSeriesViewTab",
                                "DataPoint3DBindingTab",
                                "GridPaneLayoutTab", 
                                "HistogramDemo",
                                "SeriesPointMovingDemo",

                                "SegmentColorizerDemo",
                                "AxisAndSeriesLabelsDemo",
                                "ColorizerDemo",
                                "DataGridChartingDemo",
                                "AnimationDemo",
                                "SecondaryAxesDemo",
                                "LargeDataSourceDemo",
                                "ScaleBreaksDemo",
                                "MvvmStyleBindingDemo",
                                "BoxPlotDemo",

                                "DateTimeScaleDemo",

                                "AnnotationsDemo",
                                "LogarithmicScaleDemo",
                                "WaterfallDemo",

                                "RangeControlIntegrationDemo",
        };
        protected void CompareImages(string actualImagePath, string expectedImagePath) {
            if (stopList.Any(element => actualImagePath.Contains(element)))
                return;
            if (!File.Exists(actualImagePath)) {
                AddErrorToLog(string.Format("image not exist: {0}", actualImagePath));
                return;
            }
            if (!File.Exists(expectedImagePath)) {
                AddErrorToLog(string.Format("image not exist: {0}", expectedImagePath));
                return;
            }

            using (FileStream actual = new FileStream(actualImagePath, FileMode.OpenOrCreate, FileAccess.Read)) {
                using (FileStream expected = new FileStream(expectedImagePath, FileMode.OpenOrCreate, FileAccess.Read)) {
                    int w1, w2, h1, h2;
                    var result = ImageComparer.CompareMemCmp(expected, actual, out w1, out w2, out h1, out h2);
                    if (!result)
                        AddErrorToLog(string.Format("images are different: {0} and {1}",
                                                    expectedImagePath,
                                                    actualImagePath));
                }
            }
        }

        protected override bool AllowSwitchToTheTheme(Type moduleType, Theme theme) {
            if (theme.Name == "Base" ||
                theme.Name == "Super" ||
                theme.Name == Theme.TouchlineDarkName ||
                theme == Theme.HybridApp ||
                theme.Category == Theme.Office2013TouchCategory ||
                theme.Category == Theme.Office2016SETouchCategory ||
                theme.Category == Theme.Office2016TouchCategory)
                return false;
            return base.AllowSwitchToTheTheme(moduleType, theme);
        }
        string[] leakedModules = new string[]
        {"RealTime3DChartDemo",
                                     "RealTimeChartDemo",
                                     "MvvmFinancialChartingDemo",
                                     "LargeSeriesNumberDemo",
                                     "LinesDemo",
                                     "PointsAndBubblesDemo",
                                     "AxisAndSeriesLabelsDemo",
                                     "SeriesTemplateBindingDemo",
                                     "BindingIndividualSeriesDemo",
                                     "DataGridChartingDemo",
                                     "SelectionDemo",
                                     "TitlesDemo",
                                     "DataFilteringDemo",
                                     "MovingAverageAndRegressionLineDemo",
                                     "AnimationDemo",
                                     "DataBindingDemo",
                                     "Charting3dDemo",
                                     "BarsDemo",
                                     "AreasDemo",
                                     "TooltipAndCrosshairCursorDemo",
                                     "PanesDemo",
                                     "ScaleBreaksDemo",
                                     "AnnotationsDemo",
                                     "TopNAndOthersDemo",
                                     "HistogramDemo",
                                     "LegendsDemo",
                                     "ErrorBarsDemo",
                                     "EmptyPointsDemo",
                                     "PolarAndRadarDemo", 
                                     "AdvancedCustomizationDemo",
                                     "PiesDonutsAndFunnelsDemo",
                                     "SeriesViewsDemo",
                                     "ConstantLinesAndStripsDemo"
        };
        protected override bool CheckMemoryLeaks(Type moduleType) {
            return !leakedModules.Any(module => module == moduleType.Name);
        }
        protected override void AdditionalChecks() {
            
        }
    }
}
