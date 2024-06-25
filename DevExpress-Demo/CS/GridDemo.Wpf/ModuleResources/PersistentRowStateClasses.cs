using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace GridDemo {
    public class ResizeableDataRow : Control, IResizeHelperOwner {
        static readonly Random random = new Random(1);
        public static readonly DependencyProperty RowHeightProperty;
        static ResizeableDataRow() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizeableDataRow), new FrameworkPropertyMetadata(typeof(ResizeableDataRow)));
            RowHeightProperty = DependencyProperty.RegisterAttached("RowHeight", typeof(double), typeof(ResizeableDataRow), new PropertyMetadata(0d));
            RowData.RowDataProperty.OverrideMetadata(typeof(ResizeableDataRow), new FrameworkPropertyMetadata(OnScrollViewerVerticalOffsetChanged));
        }
        static void OnScrollViewerVerticalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((ResizeableDataRow)d).OnRowDataChanged((RowData)e.OldValue, (RowData)e.NewValue);
        }
        public static void SetRowHeight(DependencyObject element, double value) {
            element.SetValue(RowHeightProperty, value);
        }
        public static double GetRowHeight(DependencyObject element) {
            return (double)element.GetValue(RowHeightProperty);
        }
        ResizeHelper resizeHelper;
        RowData RowData { get { return (RowData)DataContext; } }
        double RowHeight { get { return GetRowHeight(RowData.RowState); } set { SetRowHeight(RowData.RowState, value); } }
        public ResizeableDataRow() {
            resizeHelper = new ResizeHelper(this);
        }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            Thumb resizer = (Thumb)GetTemplateChild("PART_Resizer");
            resizeHelper.Init(resizer);
            InitializeRowHeight();
        }
        void OnRowDataChanged(RowData oldValue, RowData newValue) {
            if(oldValue != null)
                oldValue.ContentChanged -= new EventHandler(RowData_ContentChanged);
            if(newValue != null) {
                newValue.ContentChanged += new EventHandler(RowData_ContentChanged);
                InitializeRowHeight();
            }
        }
        void RowData_ContentChanged(object sender, EventArgs e) {
            InitializeRowHeight();
        }
        void InitializeRowHeight() {
            if(RowHeight == 0)
                RowHeight = 75 + 80 * random.NextDouble();
        }
        #region IResizeHelperOwner Members
        double IResizeHelperOwner.ActualSize { get { return RowHeight; } set { RowHeight = value; } }
        void IResizeHelperOwner.ChangeSize(double delta) {
            RowHeight = Math.Min(300, Math.Max(20, RowHeight + delta));
        }
        void IResizeHelperOwner.OnDoubleClick() { }
        void IResizeHelperOwner.SetIsResizing(bool isResizing) { }
        SizeHelperBase IResizeHelperOwner.SizeHelper { get { return VerticalSizeHelper.Instance; } }
        #endregion
    }
    public class ResizeableCard : Control {
        public static readonly DependencyProperty ScaleFactorProperty;
        static ResizeableCard() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizeableCard), new FrameworkPropertyMetadata(typeof(ResizeableCard)));
            ScaleFactorProperty = DependencyProperty.RegisterAttached("ScaleFactor", typeof(double), typeof(ResizeableCard), new PropertyMetadata(1d));
        }
        public static void SetScaleFactor(DependencyObject element, double value) {
            element.SetValue(ScaleFactorProperty, value);

        }
        public static double GetScaleFactor(DependencyObject element) {
            return (double)element.GetValue(ScaleFactorProperty);
        }
    }
    public static class UnsafeNativeMethods {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetCursorPos(int x, int y);
    }
    public class CommandManagerAttachedBehavior : Behavior<FrameworkElement> {
        public static readonly DependencyProperty CanExecuteHandlerCommandProperty;
        static CommandManagerAttachedBehavior() {
            CanExecuteHandlerCommandProperty = DependencyProperty.Register("CanExecuteHandlerCommand", typeof(ICommand), typeof(CommandManagerAttachedBehavior));
        }
        protected override void OnAttached() {
            base.OnAttached();
            CommandManager.AddCanExecuteHandler(AssociatedObject, CanExecute);
        }
        protected override void OnDetaching() {
            CommandManager.RemoveCanExecuteHandler(AssociatedObject, CanExecute);
            base.OnDetaching();
        }
        private void CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if(CanExecuteHandlerCommand == null) return;
            CanExecuteHandlerCommand.Execute(e);
        }
        public ICommand CanExecuteHandlerCommand {
            get { return (ICommand)GetValue(CanExecuteHandlerCommandProperty); }
            set { SetValue(CanExecuteHandlerCommandProperty, value); }
        }
    }
}
