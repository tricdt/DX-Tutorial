Imports System
Imports System.Windows
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.DevAV.Controls
Imports DevExpress.Xpf.Ribbon
Imports DevExpress.XtraReports

Namespace DevExpress.DevAV.Common.View

    Public Class BackstageDocumentPreviewReportService
        Inherits ReportServiceBase

        Public Shared ReadOnly BackstageViewIsOpenProperty As DependencyProperty = DependencyProperty.Register("BackstageViewIsOpen", GetType(Boolean), GetType(BackstageDocumentPreviewReportService), New PropertyMetadata(False, Sub(d, e) CType(d, BackstageDocumentPreviewReportService).OnBackstageViewIsOpenChanged()))

        Public Shared ReadOnly BackstageItemProperty As DependencyProperty = DependencyProperty.Register("BackstageItem", GetType(BackstageItemBase), GetType(BackstageDocumentPreviewReportService), New PropertyMetadata(Nothing, Sub(d, e) CType(d, BackstageDocumentPreviewReportService).OnBackstageItemChanged(CType(e.OldValue, BackstageItemBase), CType(e.NewValue, BackstageItemBase))))

        Public Shared ReadOnly BackstageDocumentPreviewProperty As DependencyProperty = DependencyProperty.Register("BackstageDocumentPreview", GetType(CustomBackstageDocumentPreview), GetType(BackstageDocumentPreviewReportService), New PropertyMetadata(Nothing, Sub(d, e) CType(d, BackstageDocumentPreviewReportService).OnBackstageDocumentPreviewChanged(e)))

        Public Property BackstageViewIsOpen As Boolean
            Get
                Return CBool(GetValue(BackstageViewIsOpenProperty))
            End Get

            Set(ByVal value As Boolean)
                SetValue(BackstageViewIsOpenProperty, value)
            End Set
        End Property

        Public Property BackstageItem As BackstageItemBase
            Get
                Return CType(GetValue(BackstageItemProperty), BackstageItemBase)
            End Get

            Set(ByVal value As BackstageItemBase)
                SetValue(BackstageItemProperty, value)
            End Set
        End Property

        Public Property BackstageDocumentPreview As CustomBackstageDocumentPreview
            Get
                Return CType(GetValue(BackstageDocumentPreviewProperty), CustomBackstageDocumentPreview)
            End Get

            Set(ByVal value As CustomBackstageDocumentPreview)
                SetValue(BackstageDocumentPreviewProperty, value)
            End Set
        End Property

        Private isDocumentFirstLoading As Boolean = True

        Protected Overrides Sub ShowReport(ByVal reportInfo As IReportInfo)
            MyBase.ShowReport(reportInfo)
            Dispatcher.BeginInvoke(CType(AddressOf OpenBackstageView, Action))
        End Sub

        Private Sub OpenBackstageView()
            CType(BackstageItem, BackstageTabItem).Backstage.SelectedTab = CType(BackstageItem, BackstageTabItem)
            BackstageItem.Backstage.IsOpen = True
        End Sub

        Protected Overrides Sub UpdateReportCore(ByVal actualReportInfo As IReportInfo)
            MyBase.UpdateReportCore(actualReportInfo)
            BackstageItem.IsEnabled = actualReportInfo IsNot Nothing
        End Sub

        Private Sub OnBackstageViewIsOpenChanged()
            IsVisible = BackstageViewIsOpen
        End Sub

        Private Sub OnBackstageItemChanged(ByVal oldItem As BackstageItemBase, ByVal newItem As BackstageItemBase)
            If newItem IsNot Nothing Then newItem.IsEnabled = oldItem IsNot Nothing AndAlso oldItem.IsEnabled
        End Sub

        Private Sub OnBackstageDocumentPreviewChanged(ByVal e As DependencyPropertyChangedEventArgs)
            If e.OldValue IsNot Nothing Then RemoveHandler CType(e.OldValue, CustomBackstageDocumentPreview).Loaded, AddressOf BackstageDocumentPreview_Loaded
            If e.NewValue IsNot Nothing Then AddHandler CType(e.NewValue, CustomBackstageDocumentPreview).Loaded, AddressOf BackstageDocumentPreview_Loaded
        End Sub

        Private Sub BackstageDocumentPreview_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim updateReportAction As Action = Sub() CreateReport()
            Dim document = CType(sender, CustomBackstageDocumentPreview)
            If document.IsVisible AndAlso isDocumentFirstLoading Then
                Dispatcher.BeginInvoke(updateReportAction)
                isDocumentFirstLoading = False
            End If
        End Sub

        Protected Overrides Sub SetDocumentSource(ByVal report As IReport)
            BackstageDocumentPreview.DocumentSource = report
        End Sub

        Protected Overrides Sub SetCustomSettingsViewModel(ByVal customSettingsViewModel As Object)
            BackstageDocumentPreview.CustomSettings = customSettingsViewModel
        End Sub
    End Class
End Namespace
