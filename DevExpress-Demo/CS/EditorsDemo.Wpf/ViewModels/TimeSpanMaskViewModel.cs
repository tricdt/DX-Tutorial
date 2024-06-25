using System.Windows;
using System.Windows.Input;
using DevExpress.Data.Mask;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;

namespace EditorsDemo {
    public class TimeSpanMaskViewModel : ViewModelBase {
        public TextEdit FocusedEditor {
            get { return GetProperty(() => FocusedEditor); }
            set { SetProperty(() => FocusedEditor, value, OnFocusedEditorChanged); }
        }
        public string Mask {
            get { return GetProperty(() => Mask); }
            set { SetProperty(() => Mask, value, OnMaskChanged); }
        }
        public int MaskTypeIndex {
            get { return GetProperty(() => MaskTypeIndex); }
            set { SetProperty(() => MaskTypeIndex, value, OnMaskTypeIndexChanged); }
        }
        public TimeSpanPart? DefaultPart {
            get { return GetProperty(() => DefaultPart); }
            set { SetProperty(() => DefaultPart, value, OnDefaultPartChanged); }
        }
        public TimeSpanInputMode InputMode {
            get { return GetProperty(() => InputMode); }
            set { SetProperty(() => InputMode, value, OnAllowLargeValueInputChanged); }
        }
        public bool ChangeNextPartOnCycleValueChange {
            get { return GetProperty(() => ChangeNextPartOnCycleValueChange); }
            set { SetProperty(() => ChangeNextPartOnCycleValueChange, value, OnChangeNextPartOnCycleValueChangeChanged); }
        }
        public bool AssignValueToEnteredLiteral {
            get { return GetProperty(() => AssignValueToEnteredLiteral); }
            set { SetProperty(() => AssignValueToEnteredLiteral, value, OnAssignValueToEnteredLiteralChanged); }
        }
        public ICommand EditorGotFocusCommand { get; private set; }
        public ICommand DemoModuleLoadedCommand { get; private set; }
        public TimeSpanMaskViewModel() {
            EditorGotFocusCommand = new DelegateCommand<RoutedEventArgs>(OnEditorGotFocus);
            DemoModuleLoadedCommand = new DelegateCommand<RoutedEventArgs>(OnDemoModuleLoaded);
        }
        void OnDemoModuleLoaded(RoutedEventArgs obj) {
            LayoutHelper.FindElementByType<TextEdit>(obj.Source as FrameworkElement).Focus();
        }
        void OnEditorGotFocus(RoutedEventArgs obj) {
            FocusedEditor = obj.Source as TextEdit;
        }
        void OnFocusedEditorChanged() {
            if (FocusedEditor == null) return;
            Mask = FocusedEditor.Mask;
            MaskTypeIndex = FocusedEditor.MaskType - MaskType.TimeSpan;
            DefaultPart = TimeSpanMaskOptions.GetDefaultPart(FocusedEditor);
            InputMode = TimeSpanMaskOptions.GetInputMode(FocusedEditor);
            ChangeNextPartOnCycleValueChange = TimeSpanMaskOptions.GetChangeNextPartOnCycleValueChange(FocusedEditor);
            AssignValueToEnteredLiteral = TimeSpanMaskOptions.GetAssignValueToEnteredLiteral(FocusedEditor);
        }
        void OnMaskChanged() {
            FocusedEditor.Do(x => x.Mask = Mask);
        }
        void OnMaskTypeIndexChanged() {
            FocusedEditor.Do(x => x.MaskType = MaskType.TimeSpan + MaskTypeIndex);
        }
        void OnDefaultPartChanged() {
            FocusedEditor.Do(x => TimeSpanMaskOptions.SetDefaultPart(x, DefaultPart));
        }
        void OnAllowLargeValueInputChanged() {
            FocusedEditor.Do(x => TimeSpanMaskOptions.SetInputMode(x, InputMode));
        }
        void OnChangeNextPartOnCycleValueChangeChanged() {
            FocusedEditor.Do(x => TimeSpanMaskOptions.SetChangeNextPartOnCycleValueChange(x, ChangeNextPartOnCycleValueChange));
        }
        void OnAssignValueToEnteredLiteralChanged() {
            FocusedEditor.Do(x => TimeSpanMaskOptions.SetAssignValueToEnteredLiteral(x, AssignValueToEnteredLiteral));
        }
    }
}
