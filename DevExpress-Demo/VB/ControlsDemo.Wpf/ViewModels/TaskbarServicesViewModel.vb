Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Shell
Imports CommonDemo.Helpers
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace ControlsDemo

    Public Class TaskbarServicesViewModel
        Implements IDisposable

        Public Sub New()
            overlayIconsField = New NamedIcon() {New NamedIcon() With {.Caption = "Moon", .Icon = ImagesHelper.GetSvgImage("pack://application:,,,/ControlsDemo;component/Images/Moon.svg", New Size(16, 16))}, New NamedIcon() With {.Caption = "Sun", .Icon = ImagesHelper.GetSvgImage("pack://application:,,,/ControlsDemo;component/Images/Sun.svg", New Size(16, 16))}}
            buttonPropertiesField = New ObservableCollection(Of Boolean) From {True, True, True, False, True}
            AddHandler buttonPropertiesField.CollectionChanged, AddressOf ButtonPropertyChanged
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            TaskbarButtonService.Description = Nothing
            TaskbarButtonService.OverlayIcon = Nothing
            TaskbarButtonService.ProgressState = TaskbarItemProgressState.None
            TaskbarButtonService.ThumbButtonInfos.Clear()
            TaskbarButtonService.ThumbnailClipMarginCallback = Nothing
            TaskbarButtonService.ThumbnailClipMargin = New Thickness()
            ApplicationJumpListService.Items.Clear()
            ApplicationJumpListService.Apply()
        End Sub

        Private ReadOnly overlayIconsField As IEnumerable(Of NamedIcon)

        Public ReadOnly Property OverlayIcons As IEnumerable(Of NamedIcon)
            Get
                Return overlayIconsField
            End Get
        End Property

        Private ReadOnly buttonPropertiesField As ObservableCollection(Of Boolean)

        Public ReadOnly Property ButtonProperties As IEnumerable(Of Boolean)
            Get
                Return buttonPropertiesField
            End Get
        End Property

        Public Overridable Property ThumbnailClipMarginMultipliyer As Double

        Public Overridable Property ThumbButtonsCreate As Boolean

        Public Overridable Property ThumbnailClipMargin As Thickness

        <Required>
        Protected Overridable ReadOnly Property TaskbarButtonService As ITaskbarButtonService
            Get
                Return Nothing
            End Get
        End Property

        <Required>
        Protected Overridable ReadOnly Property ApplicationJumpListService As IApplicationJumpListService
            Get
                Return Nothing
            End Get
        End Property

        <Required>
        Protected Overridable ReadOnly Property DialogService As IDialogService
            Get
                Return Nothing
            End Get
        End Property

        <Required>
        Protected Overridable ReadOnly Property MessageBoxService As IMessageBoxService
            Get
                Return Nothing
            End Get
        End Property

        Protected Overridable Sub OnThumbnailClipMarginMultipliyerChanged()
            TaskbarButtonService.UpdateThumbnailClipMargin()
        End Sub

        Protected Overridable Sub OnThumbButtonsCreateChanged()
            If ThumbButtonsCreate Then
                TaskbarButtonService.ThumbButtonInfos.Add(New TaskbarThumbButtonInfo With {.Description = "Zoom out", .ImageSource = ImagesHelper.GetSvgImage("pack://application:,,,/ControlsDemo;component/Images/TaskbarServices/ZoomOut.svg", New Size(32, 32)), .Action = Sub() DecreaseThumbnailClipMarginMultipliyer()})
                TaskbarButtonService.ThumbButtonInfos.Add(New TaskbarThumbButtonInfo With {.Description = "Zoom in", .ImageSource = ImagesHelper.GetSvgImage("pack://application:,,,/ControlsDemo;component/Images/TaskbarServices/ZoomIn.svg", New Size(32, 32)), .Action = Sub() IncreaseThumbnailClipMarginMultipliyer()})
                SetButtonsProperties()
            Else
                TaskbarButtonService.ThumbButtonInfos.Clear()
            End If
        End Sub

        Private Sub ButtonPropertyChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
            SetButtonsProperties()
        End Sub

        Private Sub SetButtonsProperties()
            For Each item As TaskbarThumbButtonInfo In TaskbarButtonService.ThumbButtonInfos
                item.IsEnabled = buttonPropertiesField(0)
                item.IsInteractive = buttonPropertiesField(1)
                item.IsBackgroundVisible = buttonPropertiesField(2)
                item.DismissWhenClicked = buttonPropertiesField(3)
                item.Visibility = If(buttonPropertiesField(4), Visibility.Visible, Visibility.Collapsed)
            Next
        End Sub

        Private Function DecreaseThumbnailClipMarginMultipliyer() As Boolean
            ThumbnailClipMarginMultipliyer = If(ThumbnailClipMarginMultipliyer >= 10, ThumbnailClipMarginMultipliyer - 10, 0)
            TaskbarButtonService.UpdateThumbnailClipMargin()
            Return True
        End Function

        Private Function IncreaseThumbnailClipMarginMultipliyer() As Boolean
            ThumbnailClipMarginMultipliyer = If(ThumbnailClipMarginMultipliyer <= 90, ThumbnailClipMarginMultipliyer + 10, 100)
            TaskbarButtonService.UpdateThumbnailClipMargin()
            Return True
        End Function

        Private Function GetThumbnailClipMargin(ByVal size As Size) As Thickness
            Return CSharpImpl.__Assign(ThumbnailClipMargin, New Thickness With {.Left = 3.0 * CDbl(ThumbnailClipMarginMultipliyer) * size.Width / (5.0 * 100.0), .Top = 2.0 * CDbl(ThumbnailClipMarginMultipliyer) * size.Height / (5.0 * 100.0), .Right = 2.0 * CDbl(ThumbnailClipMarginMultipliyer) * size.Width / (5.0 * 100.0), .Bottom = 3.0 * CDbl(ThumbnailClipMarginMultipliyer) * size.Height / (5.0 * 100.0)})
        End Function

        Private Sub AddTask(ByVal task As NewJumpTaskWindowViewModel)
            Dim customCategory As String = If(String.IsNullOrEmpty(task.CustomCategory), Nothing, task.CustomCategory)
            ApplicationJumpListService.Items.AddOrReplace(customCategory, task.Title, task.Icon.Icon, task.Description, Sub() MessageBoxService.ShowMessage(task.MessageText))
            Dim rejectedItems As IEnumerable(Of RejectedApplicationJumpItem) = ApplicationJumpListService.Apply()
            For Each rejectedItem In rejectedItems
                Dim rejectedTask = CType(rejectedItem.JumpItem, ApplicationJumpTaskInfo)
                MessageBoxService.ShowMessage(String.Format("Error: {0}", rejectedItem.Reason), rejectedTask.Title, MessageButton.OK, MessageIcon.Error)
            Next
        End Sub

        Public Sub OnLoaded()
            ThumbButtonsCreate = True
            ThumbnailClipMarginMultipliyer = 20
            TaskbarButtonService.ThumbnailClipMarginCallback = AddressOf GetThumbnailClipMargin
        End Sub

        Public Sub OpenTaskWindow()
            Dim taskAddition As NewJumpTaskWindowViewModel = ViewModelSource.Create(Function() New NewJumpTaskWindowViewModel())
            Dim errorInfo As IDataErrorInfo = CType(taskAddition, IDataErrorInfo)
            Dim okCommand As UICommand = New UICommand() With {.Caption = "OK", .IsCancel = False, .IsDefault = True, .Command = New DelegateCommand(Of CancelEventArgs)(Sub(x)
            End Sub, Function(x) String.IsNullOrEmpty(errorInfo("Title")))}
            Dim cancelCommand As UICommand = New UICommand() With {.Caption = "Cancel", .IsCancel = True, .IsDefault = False}
            If DialogService.ShowDialog(New List(Of UICommand)() From {okCommand, cancelCommand}, "Add Jump List Task", "NewJumpTaskWindow", taskAddition) Is okCommand Then AddTask(taskAddition)
        End Sub

        Private Class CSharpImpl

            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class

    Public Class NamedIcon

        Public Property Caption As String

        Public Property Icon As ImageSource
    End Class
End Namespace
