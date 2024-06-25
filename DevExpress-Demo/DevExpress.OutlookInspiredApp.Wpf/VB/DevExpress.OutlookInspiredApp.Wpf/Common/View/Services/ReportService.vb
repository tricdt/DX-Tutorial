Imports System.ComponentModel
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.DocumentViewer
Imports DevExpress.Xpf.Printing
Imports DevExpress.XtraReports

Namespace DevExpress.DevAV.Common.View

    Public MustInherit Class ReportServiceBase
        Inherits ServiceBase
        Implements IReportService

        Private isVisibleField As Boolean

        Private defaultReportInfo As IReportInfo

        Private reportInfo As IReportInfo

        Private actualReportInfo As IReportInfo

        Protected Property IsVisible As Boolean
            Get
                Return isVisibleField
            End Get

            Set(ByVal value As Boolean)
                isVisibleField = value
                UpdateReport()
                If Not isVisibleField Then reportInfo = Nothing
            End Set
        End Property

        Protected Overridable Sub SetDefaultReport(ByVal reportInfo As IReportInfo)
            defaultReportInfo = reportInfo
            UpdateReport()
        End Sub

        Protected Overridable Sub ShowReport(ByVal reportInfo As IReportInfo)
            Me.reportInfo = reportInfo
            UpdateReport()
        End Sub

        Private Sub UpdateReport()
            UpdateReportCore(If(IsVisible, If(reportInfo, defaultReportInfo), Nothing))
        End Sub

        Protected Overridable Sub UpdateReportCore(ByVal actualReportInfo As IReportInfo)
            UnsubscribeFromParametersViewModel()
            Me.actualReportInfo = actualReportInfo
            SubscribeToParametersViewModel()
            If Me.actualReportInfo Is Nothing Then
                DestroyReport()
            Else
                CreateReport()
            End If
        End Sub

        Private Sub OnParametersViewModelPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
            CreateReport()
        End Sub

        Protected Sub CreateReport()
            Dim report As IReport = actualReportInfo.CreateReport()
            If report Is Nothing Then Return
            SetCustomSettingsViewModel(actualReportInfo.ParametersViewModel)
            SetDocumentSource(report)
            report.PrintingSystemBase.ClearContent()
            report.CreateDocument(True)
        End Sub

        Private Sub DestroyReport()
            SetCustomSettingsViewModel(Nothing)
        End Sub

        Protected MustOverride Sub SetDocumentSource(ByVal report As IReport)

        Protected MustOverride Sub SetCustomSettingsViewModel(ByVal customSettingsViewModel As Object)

        Private ReadOnly Property ActualParametersViewModel As Object
            Get
                Return If(actualReportInfo Is Nothing, Nothing, actualReportInfo.ParametersViewModel)
            End Get
        End Property

        Private Sub SubscribeToParametersViewModel()
            Dim parametersViewModel As INotifyPropertyChanged = TryCast(ActualParametersViewModel, INotifyPropertyChanged)
            If parametersViewModel IsNot Nothing Then AddHandler parametersViewModel.PropertyChanged, AddressOf OnParametersViewModelPropertyChanged
        End Sub

        Private Sub UnsubscribeFromParametersViewModel()
            Dim parametersViewModel As INotifyPropertyChanged = TryCast(ActualParametersViewModel, INotifyPropertyChanged)
            If parametersViewModel IsNot Nothing Then RemoveHandler parametersViewModel.PropertyChanged, AddressOf OnParametersViewModelPropertyChanged
        End Sub

#Region "IReportService"
        Private Sub IReportService_SetDefaultReport(ByVal reportInfo As IReportInfo) Implements IReportService.SetDefaultReport
            SetDefaultReport(reportInfo)
        End Sub

        Private Sub IReportService_ShowReport(ByVal reportInfo As IReportInfo) Implements IReportService.ShowReport
            ShowReport(reportInfo)
        End Sub
#End Region
    End Class

    Public Class DocumentViewerReportService
        Inherits ReportServiceBase

        Private ReadOnly Property DocumentViewer As DocumentPreviewControl
            Get
                Return CType(AssociatedObject, DocumentPreviewControl)
            End Get
        End Property

        Public Property ZoomMode As ZoomMode

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            IsVisible = True
            ZoomMode = ZoomMode.FitToWidth
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            IsVisible = False
        End Sub

        Protected Overrides Sub SetCustomSettingsViewModel(ByVal customSettingsViewModel As Object)
        End Sub

        Protected Overrides Sub SetDocumentSource(ByVal report As IReport)
            DocumentViewer.DocumentSource = report
            DocumentViewer.ZoomMode = ZoomMode
        End Sub
    End Class
End Namespace
