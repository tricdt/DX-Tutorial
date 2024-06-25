using System;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.LayoutControl;

namespace LayoutControlDemo {
    [TemplateVisualState(Name = SampleLayoutItem.UnselectedStateName, GroupName = SampleLayoutItem.SelectionStatesGroupName)]
    [TemplateVisualState(Name = SampleLayoutItem.SelectedStateName, GroupName = SampleLayoutItem.SelectionStatesGroupName)]
    public class SampleLayoutItem : ControlBase, ISampleLayoutItem {
        #region Dependency Properties

        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(SampleLayoutItem), null);

        #endregion Dependency Properties

        private bool _IsSelected;

        public SampleLayoutItem() {
            DefaultStyleKey = typeof(SampleLayoutItem);
        }

        public string Caption {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }
        public bool IsMaximized {
            get {
                return Parent is FlowLayoutControl && ((FlowLayoutControl)Parent).MaximizedElement == this;
            }
            set {
                if (IsMaximized != value & Parent is FlowLayoutControl)
                    ((FlowLayoutControl)Parent).MaximizedElement = value ? this : null;
            }
        }
        public bool IsSelected {
            get { return _IsSelected; }
            set {
                if (IsSelected == value)
                    return;
                _IsSelected = value;
                UpdateState(true);
                if (IsSelectedChanged != null)
                    IsSelectedChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler IsSelectedChanged;

        #region Template

        internal const string SelectionStatesGroupName = "SelectionStates";
        internal const string UnselectedStateName = "Unselected";
        internal const string SelectedStateName = "Selected";

        #endregion Template

        protected override ControlControllerBase CreateController() {
            return new SampleLayoutItemController(this);
        }

        protected override void UpdateState(bool useTransitions) {
            base.UpdateState(useTransitions);
            GoToState(IsSelected ? SelectedStateName : UnselectedStateName, true);
        }
    }

    public interface ISampleLayoutItem : IControl {
        bool IsSelected { get; set; }
    }

    public class SampleLayoutItemController : ControlControllerBase {
        public SampleLayoutItemController(ISampleLayoutItem control)
            : base(control) {
        }

        public ISampleLayoutItem ISampleLayoutItem { get { return IControl as ISampleLayoutItem; } }

        #region Keyboard and Mouse Handling

        protected override void OnMouseLeftButtonDown(DXMouseButtonEventArgs e) {
            base.OnMouseLeftButtonDown(e);
            ISampleLayoutItem.IsSelected = true;
        }

        #endregion Keyboard and Mouse Handling
    }
}
