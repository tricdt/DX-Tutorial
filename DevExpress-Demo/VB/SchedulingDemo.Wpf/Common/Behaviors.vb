Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Scheduling
Imports DevExpress.XtraScheduler
Imports SchedulingDemo.ViewModels
Imports XpfScheduler = DevExpress.Xpf.Scheduling.SchedulerControl

Namespace SchedulingDemo

    Public MustInherit Class BarItemBehaviorBase(Of T As BarItem)
        Inherits Behavior(Of T)

        Protected ReadOnly Property Scheduler As XpfScheduler
            Get
                Return XpfScheduler.GetScheduler(AssociatedObject)
            End Get
        End Property

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            If Scheduler Is Nothing Then
                AddHandler AssociatedObject.Loaded, AddressOf OnLoaded
                Return
            End If

            Init()
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            RemoveHandler AssociatedObject.Loaded, AddressOf OnLoaded
        End Sub

        Protected Overridable Sub Init()
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RemoveHandler AssociatedObject.Loaded, AddressOf OnLoaded
            Init()
        End Sub
    End Class

    Public MustInherit Class BarSubItem_EnumBehaviorBase(Of TItem)
        Inherits BarItemBehaviorBase(Of BarSubItem)

        Private _Items As List(Of SchedulingDemo.BarSubItem_EnumBehaviorBase(Of TItem).ItemInfo)

        Protected Property Items As List(Of ItemInfo)
            Get
                Return _Items
            End Get

            Private Set(ByVal value As List(Of ItemInfo))
                _Items = value
            End Set
        End Property

        Public Sub New()
            Items = New List(Of ItemInfo)(BuildItems())
        End Sub

        Protected MustOverride Function BuildItems() As IEnumerable(Of ItemInfo)

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AssociatedObject.ItemLinksSource = Items
        End Sub

        Public Class ItemInfo
            Inherits BindableBase

            Private _Value As TItem

            Public Property Value As TItem
                Get
                    Return _Value
                End Get

                Private Set(ByVal value As TItem)
                    _Value = value
                End Set
            End Property

            Public Property IsChecked As Boolean
                Get
                    Return GetProperty(Function() Me.IsChecked)
                End Get

                Set(ByVal value As Boolean)
                    SetProperty(Function() IsChecked, value)
                End Set
            End Property

            Public Sub New(ByVal day As TItem)
                Value = day
            End Sub
        End Class
    End Class

    Public MustInherit Class BarSubItem_NotFlagEnumBehaviorBase(Of TItem As Structure)
        Inherits BarSubItem_EnumBehaviorBase(Of TItem)

        Protected Overrides Function BuildItems() As IEnumerable(Of ItemInfo)
            Dim res = New List(Of ItemInfo)()
            For Each item As TItem In [Enum].GetValues(GetType(TItem))
                res.Add(New ItemInfo(item))
            Next

            Return res
        End Function

        Protected Overrides Sub Init()
            MyBase.Init()
            For Each item In Items
                item.IsChecked = Equals(item.Value, GetSchedulerValue())
            Next

            Items.ForEach(Sub(x) AddHandler x.PropertyChanged, AddressOf OnItemPropertyChanged)
        End Sub

        Protected MustOverride Function GetSchedulerValue() As TItem

        Protected MustOverride Sub SetSchedulerValue(ByVal value As TItem)

        Private lockOnItemPropertyChanged As Boolean

        Private Sub OnItemPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
            If lockOnItemPropertyChanged Then Return
            lockOnItemPropertyChanged = True
            Try
                Dim senderItem As ItemInfo = CType(sender, ItemInfo)
                If Not senderItem.IsChecked Then
                    senderItem.IsChecked = True
                    Return
                End If

                For Each item In Items
                    If item Is senderItem Then Continue For
                    item.IsChecked = False
                Next

                SetSchedulerValue(senderItem.Value)
            Finally
                lockOnItemPropertyChanged = False
            End Try
        End Sub
    End Class

    Public Class BarEditItem_WorkTimeEditBehavior
        Inherits BarItemBehaviorBase(Of BarEditItem)

        Public Enum EditMode
            Start
            [End]
        End Enum

        Public Shared ReadOnly ModeProperty As DependencyProperty = DependencyProperty.Register("Mode", GetType(EditMode), GetType(BarEditItem_WorkTimeEditBehavior), New PropertyMetadata(EditMode.Start))

        Public Property Mode As EditMode
            Get
                Return CType(GetValue(ModeProperty), EditMode)
            End Get

            Set(ByVal value As EditMode)
                SetValue(ModeProperty, value)
            End Set
        End Property

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.EditValueChanged, AddressOf OnEditValueChanged
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            RemoveHandler AssociatedObject.EditValueChanged, AddressOf OnEditValueChanged
        End Sub

        Protected Overrides Sub Init()
            If Mode = EditMode.Start Then
                AssociatedObject.EditValue = Scheduler.WorkTime.Start
            Else
                AssociatedObject.EditValue = Scheduler.WorkTime.End
            End If
        End Sub

        Private Sub OnEditValueChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If AssociatedObject.EditValue Is Nothing OrElse Scheduler Is Nothing Then Return
            Dim editValue As TimeSpan
            Dim start As TimeSpan
            Dim [end] As TimeSpan
            If TypeOf AssociatedObject.EditValue Is TimeSpan Then
                editValue = CType(AssociatedObject.EditValue, TimeSpan)
            Else
                editValue = TimeSpan.FromTicks((CDate(AssociatedObject.EditValue)).Ticks)
            End If

            If Mode = EditMode.Start Then
                start = editValue
                [end] = Scheduler.WorkTime.End
            Else
                start = Scheduler.WorkTime.Start
                [end] = editValue
            End If

            Dim workTime As TimeSpanRange = New TimeSpanRange(start, [end])
            If workTime.IsValid AndAlso Not workTime.IsZero Then Scheduler.WorkTime = workTime
        End Sub
    End Class

    Public Class BarSubItem_WorkDaysEditBehavior
        Inherits BarSubItem_EnumBehaviorBase(Of WeekDays)

        Protected Overrides Function BuildItems() As IEnumerable(Of BarSubItem_EnumBehaviorBase(Of Global.DevExpress.XtraScheduler.WeekDays).ItemInfo)
            Dim res = New List(Of BarSubItem_EnumBehaviorBase(Of Global.DevExpress.XtraScheduler.WeekDays).ItemInfo)()
            res.Add(New BarSubItem_EnumBehaviorBase(Of Global.DevExpress.XtraScheduler.WeekDays).ItemInfo(WeekDays.Sunday))
            res.Add(New BarSubItem_EnumBehaviorBase(Of Global.DevExpress.XtraScheduler.WeekDays).ItemInfo(WeekDays.Monday))
            res.Add(New BarSubItem_EnumBehaviorBase(Of Global.DevExpress.XtraScheduler.WeekDays).ItemInfo(WeekDays.Tuesday))
            res.Add(New BarSubItem_EnumBehaviorBase(Of Global.DevExpress.XtraScheduler.WeekDays).ItemInfo(WeekDays.Wednesday))
            res.Add(New BarSubItem_EnumBehaviorBase(Of Global.DevExpress.XtraScheduler.WeekDays).ItemInfo(WeekDays.Thursday))
            res.Add(New BarSubItem_EnumBehaviorBase(Of Global.DevExpress.XtraScheduler.WeekDays).ItemInfo(WeekDays.Friday))
            res.Add(New BarSubItem_EnumBehaviorBase(Of Global.DevExpress.XtraScheduler.WeekDays).ItemInfo(WeekDays.Saturday))
            Return res
        End Function

        Protected Overrides Sub Init()
            MyBase.Init()
            Items(0).IsChecked = Scheduler.WorkDays.HasFlag(WeekDays.Sunday)
            Items(1).IsChecked = Scheduler.WorkDays.HasFlag(WeekDays.Monday)
            Items(2).IsChecked = Scheduler.WorkDays.HasFlag(WeekDays.Tuesday)
            Items(3).IsChecked = Scheduler.WorkDays.HasFlag(WeekDays.Wednesday)
            Items(4).IsChecked = Scheduler.WorkDays.HasFlag(WeekDays.Thursday)
            Items(5).IsChecked = Scheduler.WorkDays.HasFlag(WeekDays.Friday)
            Items(6).IsChecked = Scheduler.WorkDays.HasFlag(WeekDays.Saturday)
            Items.ForEach(Sub(x) AddHandler x.PropertyChanged, AddressOf OnItemPropertyChanged)
        End Sub

        Private Sub OnItemPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
            If Not Items.Any(Function(x) x.IsChecked) Then
                CType(sender, BarSubItem_EnumBehaviorBase(Of Global.DevExpress.XtraScheduler.WeekDays).ItemInfo).IsChecked = True
                Return
            End If

            Dim res As WeekDays? = Nothing
            For Each day As BarSubItem_EnumBehaviorBase(Of Global.DevExpress.XtraScheduler.WeekDays).ItemInfo In Items
                If Not day.IsChecked Then Continue For
                If res Is Nothing Then
                    res = day.Value
                Else
                    res = res Or day.Value
                End If
            Next

            Scheduler.WorkDays = res.Value
        End Sub
    End Class

    Public Class BarSubItem_NavigationButtonsVisibility
        Inherits BarSubItem_NotFlagEnumBehaviorBase(Of NavigationButtonVisibility)

        Protected Overrides Function GetSchedulerValue() As NavigationButtonVisibility
            Return Scheduler.ActiveView.SchedulerViewBase.NavigationButtonsVisibility
        End Function

        Protected Overrides Sub SetSchedulerValue(ByVal value As NavigationButtonVisibility)
            Scheduler.ActiveView.SchedulerViewBase.NavigationButtonsVisibility = value
        End Sub
    End Class

    Public Class BarSubItem_MoreButtonsVisibility
        Inherits BarSubItem_NotFlagEnumBehaviorBase(Of MoreButtonVisibility)

        Protected Overrides Function GetSchedulerValue() As MoreButtonVisibility
            Return Scheduler.ActiveView.SchedulerViewBase.MoreButtonsVisibility
        End Function

        Protected Overrides Sub SetSchedulerValue(ByVal value As MoreButtonVisibility)
            Scheduler.ActiveView.SchedulerViewBase.MoreButtonsVisibility = value
        End Sub
    End Class

    Public Class BarSubItem_SnapToCellsMode
        Inherits BarSubItem_NotFlagEnumBehaviorBase(Of SnapToCellsMode)

        Protected Overrides Function GetSchedulerValue() As SnapToCellsMode
            Return Scheduler.ActiveView.DayViewBase.SnapToCellsMode
        End Function

        Protected Overrides Sub SetSchedulerValue(ByVal value As SnapToCellsMode)
            Scheduler.ActiveView.DayViewBase.SnapToCellsMode = value
        End Sub
    End Class

    Public Class BarSubItem_FirstDayOfWeek
        Inherits BarSubItem_NotFlagEnumBehaviorBase(Of DayOfWeek)

        Protected Overrides Function GetSchedulerValue() As DayOfWeek
            Return Scheduler.FirstDayOfWeek
        End Function

        Protected Overrides Sub SetSchedulerValue(ByVal value As DayOfWeek)
            Scheduler.FirstDayOfWeek = value
        End Sub
    End Class

    Public Class BarSubItem_ReportTemplate
        Inherits BarSubItem_NotFlagEnumBehaviorBase(Of ReportTemplate)

        Public Shared ReadOnly ReportTemplateProperty As DependencyProperty = DependencyProperty.Register("ReportTemplate", GetType(ReportTemplate), GetType(BarSubItem_ReportTemplate), New PropertyMetadata(ReportTemplate.DailyStyle))

        Public Property ReportTemplate As ReportTemplate
            Get
                Return CType(GetValue(ReportTemplateProperty), ReportTemplate)
            End Get

            Set(ByVal value As ReportTemplate)
                SetValue(ReportTemplateProperty, value)
            End Set
        End Property

        Protected Overrides Function GetSchedulerValue() As ReportTemplate
            Return ReportTemplate
        End Function

        Protected Overrides Sub SetSchedulerValue(ByVal value As ReportTemplate)
            ReportTemplate = value
        End Sub
    End Class

    Public Class BarSubItem_DisplayUnit
        Inherits BarSubItem_NotFlagEnumBehaviorBase(Of MonthViewDisplayUnit)

        Protected Overrides Function GetSchedulerValue() As MonthViewDisplayUnit
            Return Scheduler.ActiveView.MonthView.DisplayUnit
        End Function

        Protected Overrides Sub SetSchedulerValue(ByVal value As MonthViewDisplayUnit)
            Scheduler.ActiveView.MonthView.DisplayUnit = value
        End Sub
    End Class

    Public Class ComboBoxResourceSelectionBehavior
        Inherits Behavior(Of ComboBoxEdit)

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.PopupContentSelectionChanged, AddressOf OnPopupContentSelectionChanged
        End Sub

        Private Sub OnPopupContentSelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
            Dim listBox = CType(AssociatedObject.PopupElement, ListBox)
            If e.AddedItems.Contains(TimeRegionsDemoViewModel.AnyResource) AndAlso listBox.SelectedItems.Count > 1 Then
                Dispatcher.BeginInvoke(New Action(Sub()
                    listBox.UnselectAll()
                    listBox.SelectedItems.Add(TimeRegionsDemoViewModel.AnyResource)
                End Sub))
            ElseIf e.AddedItems.Count > 0 AndAlso Not e.AddedItems.Contains(TimeRegionsDemoViewModel.AnyResource) Then
                Dispatcher.BeginInvoke(New Action(Sub() listBox.SelectedItems.Remove(TimeRegionsDemoViewModel.AnyResource)))
            End If
        End Sub
    End Class
End Namespace
