using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Mvvm.UI.Native;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Scheduling;
using DevExpress.XtraScheduler;
using SchedulingDemo.ViewModels;
using XpfScheduler = DevExpress.Xpf.Scheduling.SchedulerControl;

namespace SchedulingDemo {
    public abstract class BarItemBehaviorBase<T> : Behavior<T> where T : BarItem {
        protected XpfScheduler Scheduler { get { return XpfScheduler.GetScheduler(AssociatedObject); } }
        protected override void OnAttached() {
            base.OnAttached();
            if (Scheduler == null) {
                AssociatedObject.Loaded += OnLoaded;
                return;
            }
            Init();
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.Loaded -= OnLoaded;
        }
        protected virtual void Init() { }
        void OnLoaded(object sender, RoutedEventArgs e) {
            AssociatedObject.Loaded -= OnLoaded;
            Init();
        }
    }
    public abstract class BarSubItem_EnumBehaviorBase<TItem> : BarItemBehaviorBase<BarSubItem> {
        protected List<ItemInfo> Items { get; private set; }
        public BarSubItem_EnumBehaviorBase() {
            Items = new List<ItemInfo>(BuildItems());
        }
        protected abstract IEnumerable<ItemInfo> BuildItems();
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.ItemLinksSource = Items;
        }

        public class ItemInfo : BindableBase {
            public TItem Value { get; private set; }
            public bool IsChecked { get { return GetProperty(() => IsChecked); } set { SetProperty(() => IsChecked, value); } }
            public ItemInfo(TItem day) {
                Value = day;
            }
        }
    }
    public abstract class BarSubItem_NotFlagEnumBehaviorBase<TItem> : BarSubItem_EnumBehaviorBase<TItem>
        where TItem : struct {
        protected override IEnumerable<ItemInfo> BuildItems() {
            var res = new List<ItemInfo>();
            foreach(TItem item in Enum.GetValues(typeof(TItem))) {
                res.Add(new ItemInfo(item));
            }
            return res;
        }
        protected override void Init() {
            base.Init();
            foreach(var item in Items) {
                item.IsChecked = object.Equals(item.Value, GetSchedulerValue());
            }
            Items.ForEach(x => x.PropertyChanged += OnItemPropertyChanged);
        }
        protected abstract TItem GetSchedulerValue();
        protected abstract void SetSchedulerValue(TItem value);

        bool lockOnItemPropertyChanged;
        void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.lockOnItemPropertyChanged) return;
            this.lockOnItemPropertyChanged = true;
            try {
                ItemInfo senderItem = (ItemInfo)sender;
                if (!senderItem.IsChecked) {
                    senderItem.IsChecked = true;
                    return;
                }

                foreach (var item in Items) {
                    if (item == senderItem) continue;
                    item.IsChecked = false;
                }

                SetSchedulerValue(senderItem.Value);
            } finally {
                this.lockOnItemPropertyChanged = false;
            }
        }
    }

    public class BarEditItem_WorkTimeEditBehavior : BarItemBehaviorBase<BarEditItem> {
        public enum EditMode { Start, End }
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(EditMode), typeof(BarEditItem_WorkTimeEditBehavior), new PropertyMetadata(EditMode.Start));

        public EditMode Mode { get { return (EditMode)GetValue(ModeProperty); } set { SetValue(ModeProperty, value); } }

        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.EditValueChanged += OnEditValueChanged;
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.EditValueChanged -= OnEditValueChanged;
        }
        protected override void Init() {
            if (Mode == EditMode.Start)
                AssociatedObject.EditValue = Scheduler.WorkTime.Start;
            else AssociatedObject.EditValue = Scheduler.WorkTime.End;
        }
        void OnEditValueChanged(object sender, RoutedEventArgs e) {
            if (AssociatedObject.EditValue == null || Scheduler == null) return;
            TimeSpan editValue;
            TimeSpan start;
            TimeSpan end;
            if (AssociatedObject.EditValue is TimeSpan)
                editValue = (TimeSpan)AssociatedObject.EditValue;
            else editValue = TimeSpan.FromTicks(((DateTime)AssociatedObject.EditValue).Ticks);
            if (Mode == EditMode.Start) {
                start = editValue;
                end = Scheduler.WorkTime.End;
            } else {
                start = Scheduler.WorkTime.Start;
                end = editValue;
            }
            TimeSpanRange workTime = new TimeSpanRange(start, end);
            if (workTime.IsValid && !workTime.IsZero)
                Scheduler.WorkTime = workTime;
        }
    }
    public class BarSubItem_WorkDaysEditBehavior : BarSubItem_EnumBehaviorBase<WeekDays> {
        protected override IEnumerable<ItemInfo> BuildItems() {
            var res = new List<ItemInfo>();
            res.Add(new ItemInfo(WeekDays.Sunday));
            res.Add(new ItemInfo(WeekDays.Monday));
            res.Add(new ItemInfo(WeekDays.Tuesday));
            res.Add(new ItemInfo(WeekDays.Wednesday));
            res.Add(new ItemInfo(WeekDays.Thursday));
            res.Add(new ItemInfo(WeekDays.Friday));
            res.Add(new ItemInfo(WeekDays.Saturday));
            return res;
        }
        protected override void Init() {
            base.Init();
            Items[0].IsChecked = Scheduler.WorkDays.HasFlag(WeekDays.Sunday);
            Items[1].IsChecked = Scheduler.WorkDays.HasFlag(WeekDays.Monday);
            Items[2].IsChecked = Scheduler.WorkDays.HasFlag(WeekDays.Tuesday);
            Items[3].IsChecked = Scheduler.WorkDays.HasFlag(WeekDays.Wednesday);
            Items[4].IsChecked = Scheduler.WorkDays.HasFlag(WeekDays.Thursday);
            Items[5].IsChecked = Scheduler.WorkDays.HasFlag(WeekDays.Friday);
            Items[6].IsChecked = Scheduler.WorkDays.HasFlag(WeekDays.Saturday);
            Items.ForEach(x => x.PropertyChanged += OnItemPropertyChanged);
        }
        void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (!Items.Any(x => x.IsChecked)) {
                ((ItemInfo)sender).IsChecked = true;
                return;
            }
            WeekDays? res = null;
            foreach (ItemInfo day in Items) {
                if (!day.IsChecked) continue;
                if (res == null)
                    res = day.Value;
                else res |= day.Value;
            }
            Scheduler.WorkDays = res.Value;
        }
    }
    public class BarSubItem_NavigationButtonsVisibility : BarSubItem_NotFlagEnumBehaviorBase<NavigationButtonVisibility> {
        protected override NavigationButtonVisibility GetSchedulerValue() {
            return Scheduler.ActiveView.SchedulerViewBase.NavigationButtonsVisibility;
        }
        protected override void SetSchedulerValue(NavigationButtonVisibility value) {
            Scheduler.ActiveView.SchedulerViewBase.NavigationButtonsVisibility = value;
        }
    }
    public class BarSubItem_MoreButtonsVisibility : BarSubItem_NotFlagEnumBehaviorBase<MoreButtonVisibility> {
        protected override MoreButtonVisibility GetSchedulerValue() {
            return Scheduler.ActiveView.SchedulerViewBase.MoreButtonsVisibility;
        }
        protected override void SetSchedulerValue(MoreButtonVisibility value) {
            Scheduler.ActiveView.SchedulerViewBase.MoreButtonsVisibility = value;
        }
    }
    public class BarSubItem_SnapToCellsMode : BarSubItem_NotFlagEnumBehaviorBase<SnapToCellsMode> {
        protected override SnapToCellsMode GetSchedulerValue() {
            return Scheduler.ActiveView.DayViewBase.SnapToCellsMode;
        }
        protected override void SetSchedulerValue(SnapToCellsMode value) {
            Scheduler.ActiveView.DayViewBase.SnapToCellsMode = value;
        }
    }
    public class BarSubItem_FirstDayOfWeek : BarSubItem_NotFlagEnumBehaviorBase<DayOfWeek> {
        protected override DayOfWeek GetSchedulerValue() {
            return Scheduler.FirstDayOfWeek;
        }
        protected override void SetSchedulerValue(DayOfWeek value) {
            Scheduler.FirstDayOfWeek = value;
        }
    }
    public class BarSubItem_ReportTemplate : BarSubItem_NotFlagEnumBehaviorBase<ReportTemplate> {
        public static readonly DependencyProperty ReportTemplateProperty =
            DependencyProperty.Register("ReportTemplate", typeof(ReportTemplate), typeof(BarSubItem_ReportTemplate), new PropertyMetadata(ReportTemplate.DailyStyle));
        public ReportTemplate ReportTemplate { get { return (ReportTemplate)GetValue(ReportTemplateProperty); } set { SetValue(ReportTemplateProperty, value); } }

        protected override ReportTemplate GetSchedulerValue() {
            return ReportTemplate;
        }
        protected override void SetSchedulerValue(ReportTemplate value) {
            ReportTemplate = value;
        }
    }
    public class BarSubItem_DisplayUnit : BarSubItem_NotFlagEnumBehaviorBase<MonthViewDisplayUnit> {
        protected override MonthViewDisplayUnit GetSchedulerValue() {
            return Scheduler.ActiveView.MonthView.DisplayUnit;
        }
        protected override void SetSchedulerValue(MonthViewDisplayUnit value) {
            Scheduler.ActiveView.MonthView.DisplayUnit = value;
        }
    }

    public class ComboBoxResourceSelectionBehavior : Behavior<ComboBoxEdit> {
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.PopupContentSelectionChanged += OnPopupContentSelectionChanged;
        }

        void OnPopupContentSelectionChanged(object sender, SelectionChangedEventArgs e) {
            var listBox = (ListBox)AssociatedObject.PopupElement;
            if (e.AddedItems.Contains(TimeRegionsDemoViewModel.AnyResource) && listBox.SelectedItems.Count > 1) {
                Dispatcher.BeginInvoke(new Action(() => {
                    listBox.UnselectAll();
                    listBox.SelectedItems.Add(TimeRegionsDemoViewModel.AnyResource);
                }));
            }
            else if (e.AddedItems.Count > 0 && !e.AddedItems.Contains(TimeRegionsDemoViewModel.AnyResource))
                Dispatcher.BeginInvoke(new Action(() => {
                    listBox.SelectedItems.Remove(TimeRegionsDemoViewModel.AnyResource);
                }));
        }
    }
}
