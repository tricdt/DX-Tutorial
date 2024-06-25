Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Core.Tests
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports System
Imports System.IO
Imports System.Linq
Imports System.Windows
Imports System.Windows.Threading

Namespace ChartsDemo.Tests

    Public Class ChartDemoModuleAccessor
        Inherits DemoModulesAccessor(Of ChartsDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub

        Public ReadOnly Property ChartModule As ChartsDemoModule
            Get
                Return DemoModule
            End Get
        End Property
    End Class

    Public Class ChartsCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Private ReadOnly modulesAccessor As ChartDemoModuleAccessor

        Public Sub New()
            modulesAccessor = New ChartDemoModuleAccessor(Me)
        End Sub

        Private Sub WaitAnimationComplete(ByVal chart As ChartControlBase, ByVal demoModuleOrTab As FrameworkElement)
            DispatchBusyWait(Function() chart.IsAnimationCompleted, "charts animation")
            Dim tabItemModule As TabItemModule = TryCast(demoModuleOrTab, TabItemModule)
            If tabItemModule IsNot Nothing Then DispatchBusyWait(Function() tabItemModule.IsAnimationCompleted, "charts demo tab animation")
            Dim timer As DispatcherTimer = New DispatcherTimer()
            For i As Integer = 0 To 10 - 1
                DispatchBusyWait(Function() True, "charts update layout")
            Next
        End Sub

        Private Sub CreateTabsActions()
            Dispatch(Sub()
                Dim tabControl = TryCast(LayoutHelper.FindElement(modulesAccessor.ChartModule, Function(element) TypeOf element Is DXTabControl), DXTabControl)
                If tabControl IsNot Nothing Then
                    For Each i As Integer In Enumerable.Range(1, tabControl.Items.Count - 1)
                        Dim prevTabPage = DispatchExpr(Function() tabControl.SelectedContainer)
                        Dispatch(Sub() tabControl.SelectedIndex = i)
                        DispatchBusyWait(Function() tabControl.SelectedContainer IsNot prevTabPage, "demo tab")
                        If tabControl.SelectedItemContent IsNot Nothing Then ExportDemoToImage(TryCast(tabControl.SelectedItemContent, FrameworkElement), tabControl.SelectedItemContent.GetType())
                    Next
                Else
                    ExportDemoToImage(modulesAccessor.ChartModule, modulesAccessor.ChartModule.GetType())
                End If
            End Sub)
        End Sub

        Protected Sub ExportDemoToImage(ByVal root As FrameworkElement, ByVal demoModule As Type)
            Try
                Dim chartControl = TryCast(LayoutHelper.FindElement(root, Function(element) TypeOf element Is ChartControlBase), ChartControlBase)
                WaitAnimationComplete(chartControl, root)
                Dim demoModuleName As String = demoModule.Name
                Dim actualImagePath As String = GetExportImagePath(demoModuleName)
                If chartControl IsNot Nothing Then
                    If File.Exists(actualImagePath) Then File.Delete(actualImagePath)
                    chartControl.ExportToImage(actualImagePath)
                    CompareImages(actualImagePath, GetExpectedImagePath(demoModuleName))
                End If
            Catch __unusedException1__ As Exception
            End Try
        End Sub

        Const sharedFolderPath As String = "\\corp\internal\common\4Kalachik\DemoTesting\"

        Protected Function GetExportImagePath(ByVal demoModuleName As String) As String
            Dim fileName As String = demoModuleName & ".png"
            Return Path.Combine(String.Format("{0}WpfCharts{1}ActualImages", sharedFolderPath, AssemblyInfo.VersionId), fileName)
        End Function

        Protected Function GetExpectedImagePath(ByVal demoModuleName As String) As String
            Dim fileName As String = demoModuleName & ".png"
            Return Path.Combine(String.Format("{0}WpfCharts{1}ExpectedImages", sharedFolderPath, AssemblyInfo.VersionId), fileName)
        End Function

        Private stopList As String() = New String() {"RealTimeChartDemo", "MvvmFinancialChartingDemo", "Point3DTab", "Bubble3DTab", "Area3DTab", "StackedArea3DTab", "FullStackedArea3DTab", "Pie3DTab", "LegendsDemo", "FinancialIndicatorsDemo", "MovingAverageAndRegressionLineDemo", "Charting3dDemo", "DrillDownDemo", "LargeSeriesNumberDemo", "AdvancedCustomizationDemo", "FullStackedBarTab", "SideBySideStackedBarTab", "SideBySideFullStackedBarTab", "BubbleTab", "RangeAreaTab", "FunnelTab", "ConstantLinesAndStripsDemo", "SimpleDiagramTitlesTab", "SeriesTemplateBindingDemo", "BindingIndividualSeriesDemo", "EmptyPointsDemo", "ErrorBarsDemo", "TooltipAndCrosshairCursorDemo", "RealTime3DChartDemo", "Bar3DSeriesViewTab", "Bubble3DSeriesViewTab", "PointSeriesViewTab", "DataPoint3DBindingTab", "GridPaneLayoutTab", "HistogramDemo", "SeriesPointMovingDemo", "SegmentColorizerDemo", "AxisAndSeriesLabelsDemo", "ColorizerDemo", "DataGridChartingDemo", "AnimationDemo", "SecondaryAxesDemo", "LargeDataSourceDemo", "ScaleBreaksDemo", "MvvmStyleBindingDemo", "BoxPlotDemo", "DateTimeScaleDemo", "AnnotationsDemo", "LogarithmicScaleDemo", "WaterfallDemo", "RangeControlIntegrationDemo"}

        Protected Sub CompareImages(ByVal actualImagePath As String, ByVal expectedImagePath As String)
            If stopList.Any(Function(element) actualImagePath.Contains(element)) Then Return
            If Not File.Exists(actualImagePath) Then
                AddErrorToLog(String.Format("image not exist: {0}", actualImagePath))
                Return
            End If

            If Not File.Exists(expectedImagePath) Then
                AddErrorToLog(String.Format("image not exist: {0}", expectedImagePath))
                Return
            End If

            Using actual As FileStream = New FileStream(actualImagePath, FileMode.OpenOrCreate, FileAccess.Read)
                Using expected As FileStream = New FileStream(expectedImagePath, FileMode.OpenOrCreate, FileAccess.Read)
                    Dim w1, w2, h1, h2 As Integer
                    Dim result = CompareMemCmp(expected, actual, w1, w2, h1, h2)
                    If Not result Then AddErrorToLog(String.Format("images are different: {0} and {1}", expectedImagePath, actualImagePath))
                End Using
            End Using
        End Sub

        Protected Overrides Function AllowSwitchToTheTheme(ByVal moduleType As Type, ByVal theme As Theme) As Boolean
            If Equals(theme.Name, "Base") OrElse Equals(theme.Name, "Super") OrElse Equals(theme.Name, Theme.TouchlineDarkName) OrElse theme Is Theme.HybridApp OrElse Equals(theme.Category, Theme.Office2013TouchCategory) OrElse Equals(theme.Category, Theme.Office2016SETouchCategory) OrElse Equals(theme.Category, Theme.Office2016TouchCategory) Then Return False
            Return MyBase.AllowSwitchToTheTheme(moduleType, theme)
        End Function

        Private leakedModules As String() = New String() {"RealTime3DChartDemo", "RealTimeChartDemo", "MvvmFinancialChartingDemo", "LargeSeriesNumberDemo", "LinesDemo", "PointsAndBubblesDemo", "AxisAndSeriesLabelsDemo", "SeriesTemplateBindingDemo", "BindingIndividualSeriesDemo", "DataGridChartingDemo", "SelectionDemo", "TitlesDemo", "DataFilteringDemo", "MovingAverageAndRegressionLineDemo", "AnimationDemo", "DataBindingDemo", "Charting3dDemo", "BarsDemo", "AreasDemo", "TooltipAndCrosshairCursorDemo", "PanesDemo", "ScaleBreaksDemo", "AnnotationsDemo", "TopNAndOthersDemo", "HistogramDemo", "LegendsDemo", "ErrorBarsDemo", "EmptyPointsDemo", "PolarAndRadarDemo", "AdvancedCustomizationDemo", "PiesDonutsAndFunnelsDemo", "SeriesViewsDemo", "ConstantLinesAndStripsDemo"}

        Protected Overrides Function CheckMemoryLeaks(ByVal moduleType As Type) As Boolean
            Return Not leakedModules.Any(Function([module]) Equals([module], moduleType.Name))
        End Function

        Protected Overrides Sub AdditionalChecks()
        End Sub
    End Class
End Namespace
