Imports System
Imports System.Drawing.Imaging
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Charts.Designer
Imports DevExpress.Charts.Native
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.Ribbon
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Native

Namespace ChartsDemo

    Public Partial Class ChartsDemoRibbon
        Inherits UserControl

        Public Shared ReadOnly SelectedPageOnMergingProperty As DependencyProperty

        Shared Sub New()
            SelectedPageOnMergingProperty = DependencyProperty.Register("SelectedPageOnMerging", GetType(SelectedPageOnMerging), GetType(ChartsDemoRibbon), New PropertyMetadata(SelectedPageOnMerging.SelectedPage))
        End Sub

        Private chartReference As WeakReference

        Public Sub New()
            InitializeComponent()
            DataContext = ViewModelSource.Create(Function() New ChartsDemoRibbonViewModel(Me))
        End Sub

        Public Property Chart As ChartControlBase
            Get
                If chartReference Is Nothing Then Return Nothing
                Return If(chartReference.IsAlive, CType(chartReference.Target, ChartControlBase), Nothing)
            End Get

            Set(ByVal value As ChartControlBase)
                chartReference = New WeakReference(value)
            End Set
        End Property

        Public Property SelectedPageOnMerging As SelectedPageOnMerging
            Get
                Return CType(GetValue(SelectedPageOnMergingProperty), SelectedPageOnMerging)
            End Get

            Set(ByVal value As SelectedPageOnMerging)
                SetValue(SelectedPageOnMergingProperty, value)
            End Set
        End Property

        Public Property ShowPaletteButton As Boolean
            Get
                Return CType(DataContext, ChartsDemoRibbonViewModel).ShowPaletteButton
            End Get

            Set(ByVal value As Boolean)
                CType(DataContext, ChartsDemoRibbonViewModel).ShowPaletteButton = value
            End Set
        End Property

        Public Property ShowRunChartDesignerButton As Boolean
            Get
                Return CType(DataContext, ChartsDemoRibbonViewModel).ShowRunChartDesignerButton
            End Get

            Set(ByVal value As Boolean)
                CType(DataContext, ChartsDemoRibbonViewModel).ShowRunChartDesignerButton = value
            End Set
        End Property

#Region "Nested classes: PalettesViewModel, PaletteViewModel"
        Public Class ChartsDemoRibbonViewModel
            Inherits ViewModelBase

            Private _RunChartDesignerCommand As DelegateCommand, _ExportToImageCommand As DelegateCommand, _ExportToXlsCommand As DelegateCommand, _ExportToDocxCommand As DelegateCommand, _ExportToXlsxCommand As DelegateCommand, _ExportToMhtCommand As DelegateCommand, _ExportToHtmlCommand As DelegateCommand, _ExportToPdfCommand As DelegateCommand, _ExportToRtfCommand As DelegateCommand, _ExportToXpsCommand As DelegateCommand

            Const sTR_DXChart As String = "Exported DXChart"

            Private ReadOnly owner As WeakReference

            Private showPaletteButtonField As Boolean = True

            Private showChartGroupField As Boolean = True

            Private showRunChartDesignerButtonField As Boolean = False

            Protected Sub CheckParentModel()
                If CType(Me, ISupportParentViewModel).ParentViewModel Is Nothing Then CType(Me, ISupportParentViewModel).ParentViewModel = DevExpress.Xpf.Core.Native.LayoutHelper.FindAmongParents(Of MainWindow)(CType(owner.Target, DependencyObject), Nothing).DataContext
            End Sub

            Private ReadOnly Property SaveFileDialogService As ISaveFileDialogService
                Get
                    CheckParentModel()
                    Return GetService(Of ISaveFileDialogService)()
                End Get
            End Property

            Private ReadOnly Property MessageBoxService As IMessageBoxService
                Get
                    CheckParentModel()
                    Return GetService(Of IMessageBoxService)(ServiceSearchMode.PreferParents)
                End Get
            End Property

            Public ReadOnly Property Chart As ChartControlBase
                Get
                    Return TryCast(OwnerChartsDemoRibbon.Chart, ChartControlBase)
                End Get
            End Property

            Public ReadOnly Property OwnerChartsDemoRibbon As ChartsDemoRibbon
                Get
                    Return If(owner.IsAlive, CType(owner.Target, ChartsDemoRibbon), Nothing)
                End Get
            End Property

            Public Property RunChartDesignerCommand As DelegateCommand
                Get
                    Return _RunChartDesignerCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _RunChartDesignerCommand = value
                End Set
            End Property

            Public Property ExportToImageCommand As DelegateCommand
                Get
                    Return _ExportToImageCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _ExportToImageCommand = value
                End Set
            End Property

            Public Property ExportToXlsCommand As DelegateCommand
                Get
                    Return _ExportToXlsCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _ExportToXlsCommand = value
                End Set
            End Property

            Public Property ExportToDocxCommand As DelegateCommand
                Get
                    Return _ExportToDocxCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _ExportToDocxCommand = value
                End Set
            End Property

            Public Property ExportToXlsxCommand As DelegateCommand
                Get
                    Return _ExportToXlsxCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _ExportToXlsxCommand = value
                End Set
            End Property

            Public Property ExportToMhtCommand As DelegateCommand
                Get
                    Return _ExportToMhtCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _ExportToMhtCommand = value
                End Set
            End Property

            Public Property ExportToHtmlCommand As DelegateCommand
                Get
                    Return _ExportToHtmlCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _ExportToHtmlCommand = value
                End Set
            End Property

            Public Property ExportToPdfCommand As DelegateCommand
                Get
                    Return _ExportToPdfCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _ExportToPdfCommand = value
                End Set
            End Property

            Public Property ExportToRtfCommand As DelegateCommand
                Get
                    Return _ExportToRtfCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _ExportToRtfCommand = value
                End Set
            End Property

            Public Property ExportToXpsCommand As DelegateCommand
                Get
                    Return _ExportToXpsCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _ExportToXpsCommand = value
                End Set
            End Property

            Public Property ShowPaletteButton As Boolean
                Get
                    Return showPaletteButtonField
                End Get

                Set(ByVal value As Boolean)
                    If showPaletteButtonField <> value Then
                        SetProperty(showPaletteButtonField, value, Function() ShowPaletteButton)
                        SetChartGroupVisibility()
                    End If
                End Set
            End Property

            Public Property ShowChartGroup As Boolean
                Get
                    Return showChartGroupField
                End Get

                Set(ByVal value As Boolean)
                    If showChartGroupField <> value Then SetProperty(showChartGroupField, value, Function() ShowChartGroup)
                End Set
            End Property

            Public Property ShowRunChartDesignerButton As Boolean
                Get
                    Return showRunChartDesignerButtonField
                End Get

                Set(ByVal value As Boolean)
                    If showRunChartDesignerButtonField <> value Then
                        SetProperty(showRunChartDesignerButtonField, value, Function() ShowRunChartDesignerButton)
                        SetChartGroupVisibility()
                    End If
                End Set
            End Property

            Public ReadOnly Property ShowExportButton As Boolean
                Get
                    Return True
                End Get
            End Property

            Public Sub New(ByVal cdr As ChartsDemoRibbon)
                owner = New WeakReference(cdr)
                RunChartDesignerCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControl = TryCast(OwnerChartsDemoRibbon.Chart, ChartControl)
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't run the designer because the Chart is null or it is a 3D Chart Control.")
                        Return
                    End If

                    Dim designer As ChartDesigner = New ChartDesigner(chartControl)
                    Dim window As Window = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject(Of Window)(OwnerChartsDemoRibbon)
                    designer.Show(window)
                End Sub)
                ExportToImageCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControlBase = OwnerChartsDemoRibbon.Chart
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.")
                        Return
                    End If

                    SaveFileDialogService.DefaultFileName = sTR_DXChart
                    SaveFileDialogService.Filter = "BMP Image|*.BMP|Gif Image|*.GIF|JPEG Image|*.JPG|PNG Image|*.PNG|TIFF Image|*.TIFF"
                    Dim dialogResult = SaveFileDialogService.ShowDialog()
                    If Not dialogResult Then
                        Return
                    Else
                        Dim options As ImageExportOptions = New ImageExportOptions()
                        Select Case SaveFileDialogService.FilterIndex
                            Case 1
                                options.Format = ImageFormat.Bmp
                            Case 2
                                options.Format = ImageFormat.Gif
                            Case 3
                                options.Format = ImageFormat.Jpeg
                            Case 4
                                options.Format = ImageFormat.Png
                            Case 5
                                options.Format = ImageFormat.Tiff
                            Case Else
                                MessageBoxService.ShowMessage("The selected format is not supported.", "Error", MessageButton.OK, MessageIcon.Error)
                                Return
                        End Select

                        Dim fileName = SaveFileDialogService.GetFullFileName()
                        chartControl.ExportToImage(fileName, options)
                        AskAndOpenResultFile(fileName)
                    End If
                End Sub)
                ExportToXlsxCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControlBase = OwnerChartsDemoRibbon.Chart
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.")
                        Return
                    End If

                    SaveFileDialogService.DefaultFileName = sTR_DXChart
                    SaveFileDialogService.Filter = "XLSX Document|*.XLSX"
                    Dim dialogResult = SaveFileDialogService.ShowDialog()
                    If Not dialogResult Then
                        Return
                    Else
                        Dim fileName = SaveFileDialogService.GetFullFileName()
                        chartControl.ExportToXlsx(fileName)
                        AskAndOpenResultFile(fileName)
                    End If
                End Sub)
                ExportToXlsCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControlBase = OwnerChartsDemoRibbon.Chart
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.")
                        Return
                    End If

                    SaveFileDialogService.DefaultFileName = sTR_DXChart
                    SaveFileDialogService.Filter = "XLS Document|*.XLS"
                    Dim dialogResult = SaveFileDialogService.ShowDialog()
                    If Not dialogResult Then
                        Return
                    Else
                        Dim fileName = SaveFileDialogService.GetFullFileName()
                        chartControl.ExportToXls(fileName)
                        AskAndOpenResultFile(fileName)
                    End If
                End Sub)
                ExportToDocxCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControlBase = OwnerChartsDemoRibbon.Chart
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.")
                        Return
                    End If

                    SaveFileDialogService.DefaultFileName = sTR_DXChart
                    SaveFileDialogService.Filter = "DOCX Document|*.DOCX"
                    Dim dialogResult = SaveFileDialogService.ShowDialog()
                    If Not dialogResult Then
                        Return
                    Else
                        Dim fileName = SaveFileDialogService.GetFullFileName()
                        chartControl.ExportToDocx(fileName)
                        AskAndOpenResultFile(fileName)
                    End If
                End Sub)
                ExportToMhtCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControlBase = OwnerChartsDemoRibbon.Chart
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.")
                        Return
                    End If

                    SaveFileDialogService.DefaultFileName = sTR_DXChart
                    SaveFileDialogService.Filter = "MHT Document|*.MHT"
                    Dim dialogResult = SaveFileDialogService.ShowDialog()
                    If Not dialogResult Then
                        Return
                    Else
                        Dim fileName = SaveFileDialogService.GetFullFileName()
                        chartControl.ExportToMht(fileName)
                        AskAndOpenResultFile(fileName)
                    End If
                End Sub)
                ExportToHtmlCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControlBase = OwnerChartsDemoRibbon.Chart
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.")
                        Return
                    End If

                    SaveFileDialogService.DefaultFileName = sTR_DXChart
                    SaveFileDialogService.Filter = "HTML Document|*.HTML"
                    Dim dialogResult = SaveFileDialogService.ShowDialog()
                    If Not dialogResult Then
                        Return
                    Else
                        Dim fileName = SaveFileDialogService.GetFullFileName()
                        chartControl.ExportToHtml(fileName)
                        AskAndOpenResultFile(fileName)
                    End If
                End Sub)
                ExportToPdfCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControlBase = OwnerChartsDemoRibbon.Chart
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.")
                        Return
                    End If

                    SaveFileDialogService.DefaultFileName = sTR_DXChart
                    SaveFileDialogService.Filter = "PDF Document|*.PDF"
                    Dim dialogResult = SaveFileDialogService.ShowDialog()
                    If Not dialogResult Then
                        Return
                    Else
                        Dim fileName = SaveFileDialogService.GetFullFileName()
                        chartControl.ExportToPdf(fileName)
                        AskAndOpenResultFile(fileName)
                    End If
                End Sub)
                ExportToRtfCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControlBase = OwnerChartsDemoRibbon.Chart
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.")
                        Return
                    End If

                    SaveFileDialogService.DefaultFileName = sTR_DXChart
                    SaveFileDialogService.Filter = "RTF Document|*.RTF"
                    Dim dialogResult = SaveFileDialogService.ShowDialog()
                    If Not dialogResult Then
                        Return
                    Else
                        Dim fileName = SaveFileDialogService.GetFullFileName()
                        chartControl.ExportToRtf(fileName)
                        AskAndOpenResultFile(fileName)
                    End If
                End Sub)
                ExportToXpsCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControlBase = OwnerChartsDemoRibbon.Chart
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.")
                        Return
                    End If

                    SaveFileDialogService.DefaultFileName = sTR_DXChart
                    SaveFileDialogService.Filter = "XPS Document|*.XPS"
                    Dim dialogResult = SaveFileDialogService.ShowDialog()
                    If Not dialogResult Then
                        Return
                    Else
                        Dim fileName = SaveFileDialogService.GetFullFileName()
                        chartControl.ExportToXps(fileName)
                        AskAndOpenResultFile(fileName)
                    End If
                End Sub)
            End Sub

            Private Sub AskAndOpenResultFile(ByVal fileName As String)
                If MessageBoxService.ShowMessage("Do you want to open the result file?", String.Empty, MessageButton.YesNo, MessageIcon.Question) = MessageResult.Yes Then ProcessLaunchHelper.StartConfirmed(fileName)
            End Sub

            Private Sub SetChartGroupVisibility()
                ShowChartGroup = ShowPaletteButton OrElse ShowRunChartDesignerButton OrElse ShowExportButton
            End Sub
        End Class
#End Region
    End Class

    Public Class MarkedRibbonControl
        Inherits RibbonControl

    End Class
End Namespace
