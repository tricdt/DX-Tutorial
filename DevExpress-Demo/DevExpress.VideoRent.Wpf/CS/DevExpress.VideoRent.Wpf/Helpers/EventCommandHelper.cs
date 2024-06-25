using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using DevExpress.VideoRent.Wpf.ModulesBase;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using DevExpress.VideoRent.ViewModel.Helpers;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public delegate bool IsEventArgsCorrectDelegate(object sender, EventArgs e);
    public class EventCommand : DependencyObject {
        #region Dependency Properies
        public static readonly DependencyProperty EventProperty;
        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty ParameterProperty;
        static EventCommand() {
            Type ownerType = typeof(EventCommand);
            EventProperty = DependencyProperty.Register("Event", typeof(string), ownerType, new PropertyMetadata(null));
            CommandProperty = DependencyProperty.Register("Command", typeof(object), ownerType, new PropertyMetadata(null));
            ParameterProperty = DependencyProperty.Register("Parameter", typeof(object), ownerType, new PropertyMetadata(null));
        }
        #endregion
        public string Event { get { return (string)GetValue(EventProperty); } set { SetValue(EventProperty, value); } }
        public object Command { get { return GetValue(CommandProperty); } set { SetValue(CommandProperty, value); } }
        public object Parameter { get { return GetValue(ParameterProperty); } set { SetValue(ParameterProperty, value); } }
        public IsEventArgsCorrectDelegate IsEventArgsCorrect { get; set; }
        public bool UseEventArgsInsteadOfParameter { get; set; }
        public void Execute(object sender, EventArgs e) {
            if(IsEventArgsCorrect != null && !IsEventArgsCorrect(sender, e)) return;
            object parameter = UseEventArgsInsteadOfParameter ? e : Parameter;
            Action<object> action = Command as Action<object>;
            if(action != null) {
                action(parameter);
                return;
            }
            ICommand iCommand = Command as ICommand;
            if(iCommand != null) {
                iCommand.Execute(parameter);
                return;
            }
        }
    }
    public class EventCommandHelper : DependencyObject {
        #region Dependency Properties
        public static readonly DependencyProperty EventCommandsProperty;
        static EventCommandHelper() {
            Type ownerType = typeof(EventCommandHelper);
            EventCommandsProperty = DependencyProperty.RegisterAttached("EventCommands", typeof(ObservableCollection<EventCommand>), ownerType, new PropertyMetadata(null, RaiseEventCommandsChanged));
        }
        #endregion
        public static ObservableCollection<EventCommand> GetEventCommands(DependencyObject d) {
            ObservableCollection<EventCommand> eventCommands = (ObservableCollection<EventCommand>)d.GetValue(EventCommandsProperty);
            if(eventCommands == null) {
                eventCommands = new ObservableCollection<EventCommand>();
                SetEventCommands(d, eventCommands);
            }
            return eventCommands;
        }
        public static void SetEventCommands(DependencyObject d, ObservableCollection<EventCommand> value) { d.SetValue(EventCommandsProperty, value); }
        static void RaiseEventCommandsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ObservableCollection<EventCommand> oldValue = (ObservableCollection<EventCommand>)e.OldValue;
            ObservableCollection<EventCommand> newValue = (ObservableCollection<EventCommand>)e.NewValue;
            if(oldValue != null)
                oldValue.CollectionChanged -= (sender, ea) => OnEventCommandsChanged(d);
            if(newValue != null) {
                newValue.CollectionChanged += (sender, ea) => OnEventCommandsChanged(d);
            }
        }
        static void OnEventCommandsChanged(DependencyObject d) {
            foreach(EventCommand eventCommand in GetEventCommands(d)) {
                SubscribeToEvent(d, eventCommand);
            }
        }
        static void SubscribeToEvent(DependencyObject d, EventCommand eventCommand) {
            EventInfo eventInfo = d.GetType().GetEvent(eventCommand.Event);
            if(eventInfo == null) return;
            MethodInfo methodInfo = typeof(EventCommand).GetMethod("Execute", new Type[] { typeof(object), typeof(EventArgs) });
            Delegate delegateObject = Delegate.CreateDelegate(eventInfo.EventHandlerType, eventCommand, methodInfo);
            if(eventInfo.EventHandlerType == typeof(CommandEventHandler)) {
                eventCommand.UseEventArgsInsteadOfParameter = true;
            }
            if(eventInfo.EventHandlerType == typeof(MouseButtonEventHandler) && eventInfo.Name == "MouseDoubleClick" && d is GridViewBase) {
                eventCommand.IsEventArgsCorrect = CheckRowAndExecute;
            }
            if(eventInfo.EventHandlerType == typeof(KeyEventHandler) && eventInfo.Name == "KeyDown" && d is GridViewBase) {
                eventCommand.IsEventArgsCorrect = CheckKeyAndExecute;
            }
            eventInfo.RemoveEventHandler(d, delegateObject);
            eventInfo.AddEventHandler(d, delegateObject);
        }
        static bool CheckRowAndExecute(object owner, EventArgs e) {
            GridViewBase view = owner as GridViewBase;
            MouseEventArgs mea = e as MouseEventArgs;
            if(view == null || mea == null) return false;
            int rowHandle = view.GetRowHandleByMouseEventArgs(mea);
            return !(rowHandle == GridControl.InvalidRowHandle || rowHandle == GridControl.NewItemRowHandle || rowHandle == GridControl.AutoFilterRowHandle || view.Grid != null && view.Grid.IsGroupRowHandle(rowHandle));
        }
        static bool CheckKeyAndExecute(object owner, EventArgs e) {
            GridViewBase view = owner as GridViewBase;
            KeyEventArgs kea = e as KeyEventArgs;
            return view != null && kea != null && kea.Key == Key.Return;
        }
    }
    public class CustomEventCommandHelper : EventCommandHelper {
        public static readonly DependencyProperty ClickProperty;
        public static readonly DependencyProperty ItemClickProperty;
        public static readonly DependencyProperty ItemClickParameterProperty;
        public static readonly DependencyProperty ItemDoubleClickProperty;
        public static readonly DependencyProperty AddItemClickProperty;
        public static readonly DependencyProperty DeleteItemClickProperty;
        public static readonly DependencyProperty DefaultButtonClickProperty;
        public static readonly DependencyProperty ClosingProperty;
        public static readonly DependencyProperty SpinUpClickProperty;
        public static readonly DependencyProperty SpinDownClickProperty;
        public static readonly DependencyProperty MouseDoubleClickProperty;
        public static readonly DependencyProperty MouseDoubleClickParameterProperty;
        public static readonly DependencyProperty ValueReadingProperty;
        public static readonly DependencyProperty ValueWritingProperty;
        public static readonly DependencyProperty EnterKeyDownProperty;
        public static readonly DependencyProperty EnterKeyDownParameterProperty;
        static CustomEventCommandHelper() {
            Type ownerType = typeof(CustomEventCommandHelper);
            ClickProperty = DependencyProperty.RegisterAttached("Click", typeof(object), ownerType, new PropertyMetadata(null, RaiseClickChanged));
            ItemClickProperty = DependencyProperty.RegisterAttached("ItemClick", typeof(object), ownerType, new PropertyMetadata(null, RaiseItemClickChanged));
            ItemClickParameterProperty = DependencyProperty.RegisterAttached("ItemClickParameter", typeof(object), ownerType, new PropertyMetadata(null));
            ItemDoubleClickProperty = DependencyProperty.RegisterAttached("ItemDoubleClick", typeof(object), ownerType, new PropertyMetadata(null, RaiseItemDoubleClickChanged));
            AddItemClickProperty = DependencyProperty.RegisterAttached("AddItemClick", typeof(object), ownerType, new PropertyMetadata(null, RaiseAddItemClickChanged));
            DeleteItemClickProperty = DependencyProperty.RegisterAttached("DeleteItemClick", typeof(object), ownerType, new PropertyMetadata(null, RaiseDeleteItemClickChanged));
            DefaultButtonClickProperty = DependencyProperty.RegisterAttached("DefaultButtonClick", typeof(object), ownerType, new PropertyMetadata(null, RaiseDefaultButtonClickChanged));
            ClosingProperty = DependencyProperty.RegisterAttached("Closing", typeof(object), ownerType, new PropertyMetadata(null, RaiseClosingChanged));
            SpinUpClickProperty = DependencyProperty.RegisterAttached("SpinUpClick", typeof(object), ownerType, new PropertyMetadata(null, RaiseSpinUpClickChanged));
            SpinDownClickProperty = DependencyProperty.RegisterAttached("SpinDownClick", typeof(object), ownerType, new PropertyMetadata(null, RaiseSpinDownClickChanged));
            MouseDoubleClickProperty = DependencyProperty.RegisterAttached("MouseDoubleClick", typeof(object), ownerType, new PropertyMetadata(null, RaiseMouseDoubleClickChanged));
            MouseDoubleClickParameterProperty = DependencyProperty.RegisterAttached("MouseDoubleClickParameter", typeof(object), ownerType, new PropertyMetadata(null));
            ValueWritingProperty = DependencyProperty.RegisterAttached("ValueWriting", typeof(object), ownerType, new PropertyMetadata(null, RaiseValueWritingChanged));
            ValueReadingProperty = DependencyProperty.RegisterAttached("ValueReading", typeof(object), ownerType, new PropertyMetadata(null, RaiseValueReadingChanged));
            EnterKeyDownProperty = DependencyProperty.RegisterAttached("EnterKeyDown", typeof(object), ownerType, new PropertyMetadata(null, RaiseEnterKeyDownChanged));
            EnterKeyDownParameterProperty = DependencyProperty.RegisterAttached("EnterKeyDownParameter", typeof(object), ownerType, new PropertyMetadata(null));
        }
        public static object GetClick(DependencyObject d) { return d.GetValue(ClickProperty); }
        public static void SetClick(DependencyObject d, object value) { d.SetValue(ClickProperty, value); }
        public static object GetItemClick(DependencyObject d) { return d.GetValue(ItemClickProperty); }
        public static void SetItemClick(DependencyObject d, object value) { d.SetValue(ItemClickProperty, value); }
        public static object GetItemClickParameter(DependencyObject d) { return d.GetValue(ItemClickParameterProperty); }
        public static void SetItemClickParameter(DependencyObject d, object value) { d.SetValue(ItemClickParameterProperty, value); }
        public static object GetItemDoubleClick(DependencyObject d) { return d.GetValue(ItemDoubleClickProperty); }
        public static void SetItemDoubleClick(DependencyObject d, object value) { d.SetValue(ItemDoubleClickProperty, value); }
        public static object GetAddItemClick(DependencyObject d) { return d.GetValue(AddItemClickProperty); }
        public static void SetAddItemClick(DependencyObject d, object value) { d.SetValue(AddItemClickProperty, value); }
        public static object GetDeleteItemClick(DependencyObject d) { return d.GetValue(DeleteItemClickProperty); }
        public static void SetDeleteItemClick(DependencyObject d, object value) { d.SetValue(DeleteItemClickProperty, value); }
        public static object GetDefaultButtonClick(DependencyObject d) { return d.GetValue(DefaultButtonClickProperty); }
        public static void SetDefaultButtonClick(DependencyObject d, object value) { d.SetValue(DefaultButtonClickProperty, value); }
        public static object GetClosing(DependencyObject d) { return d.GetValue(ClosingProperty); }
        public static void SetClosing(DependencyObject d, object value) { d.SetValue(ClosingProperty, value); }
        public static object GetSpinUpClick(DependencyObject d) { return d.GetValue(SpinUpClickProperty); }
        public static void SetSpinUpClick(DependencyObject d, object value) { d.SetValue(SpinUpClickProperty, value); }
        public static object GetSpinDownClick(DependencyObject d) { return d.GetValue(SpinDownClickProperty); }
        public static void SetSpinDownClick(DependencyObject d, object value) { d.SetValue(SpinDownClickProperty, value); }
        public static object GetMouseDoubleClick(DependencyObject d) { return d.GetValue(MouseDoubleClickProperty); }
        public static void SetMouseDoubleClick(DependencyObject d, object value) { d.SetValue(MouseDoubleClickProperty, value); }
        public static object GetMouseDoubleClickParameter(DependencyObject d) { return d.GetValue(MouseDoubleClickParameterProperty); }
        public static void SetMouseDoubleClickParameter(DependencyObject d, object value) { d.SetValue(MouseDoubleClickParameterProperty, value); }
        public static object GetValueWriting(DependencyObject d) { return d.GetValue(ValueWritingProperty); }
        public static void SetValueWriting(DependencyObject d, object value) { d.SetValue(ValueWritingProperty, value); }
        public static object GetValueReading(DependencyObject d) { return d.GetValue(ValueReadingProperty); }
        public static void SetValueReading(DependencyObject d, object value) { d.SetValue(ValueReadingProperty, value); }
        public static object GetEnterKeyDown(DependencyObject d) { return d.GetValue(EnterKeyDownProperty); }
        public static void SetEnterKeyDown(DependencyObject d, object value) { d.SetValue(EnterKeyDownProperty, value); }
        public static object GetEnterKeyDownParameter(DependencyObject d) { return d.GetValue(EnterKeyDownParameterProperty); }
        public static void SetEnterKeyDownParameter(DependencyObject d, object value) { d.SetValue(EnterKeyDownParameterProperty, value); }
        protected static void BindCommand(DependencyObject d, string eventName, DependencyProperty commandProperty) {
            BindCommand(d, eventName, commandProperty, null);
        }
        protected static void BindCommand(DependencyObject d, string eventName, DependencyProperty commandProperty, DependencyProperty parameterProperty) {
            ObservableCollection<EventCommand> eventCommands = GetEventCommands(d);
            EventCommand eventCommand = new EventCommand() { Event = eventName };
            BindingOperations.SetBinding(eventCommand, EventCommand.CommandProperty, new Binding() { Source = d, Mode = BindingMode.OneWay, Path = new PropertyPath(commandProperty) });
            if(parameterProperty != null)
                BindingOperations.SetBinding(eventCommand, EventCommand.ParameterProperty, new Binding() { Source = d, Mode = BindingMode.OneWay, Path = new PropertyPath(parameterProperty) });
            eventCommands.Add(eventCommand);
        }
        static void RaiseClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BindCommand(d, "Click", ClickProperty);
        }
        static void RaiseItemClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BindCommand(d, "ItemClick", ItemClickProperty, ItemClickParameterProperty);
        }
        static void RaiseItemDoubleClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BindCommand(d, "ItemDoubleClick", ItemDoubleClickProperty);
        }
        static void RaiseDeleteItemClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BindCommand(d, "DeleteItemClick", DeleteItemClickProperty);
        }
        static void RaiseAddItemClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BindCommand(d, "AddItemClick", AddItemClickProperty);
        }
        static void RaiseDefaultButtonClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BindCommand(d, "DefaultButtonClick", DefaultButtonClickProperty);
        }
        static void RaiseClosingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BindCommand(d, "Closing", ClosingProperty);
        }
        static void RaiseSpinUpClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BindCommand(d, "SpinUpClick", SpinUpClickProperty);
        }
        static void RaiseSpinDownClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BindCommand(d, "SpinDownClick", SpinDownClickProperty);
        }
        static void RaiseMouseDoubleClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BindCommand(d, "MouseDoubleClick", MouseDoubleClickProperty, MouseDoubleClickParameterProperty);
        }
        static void RaiseValueWritingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BindCommand(d, "ValueWriting", ValueWritingProperty);
        }
        static void RaiseValueReadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BindCommand(d, "ValueReading", ValueReadingProperty);
        }
        static void RaiseEnterKeyDownChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BindCommand(d, "KeyDown", EnterKeyDownProperty,EnterKeyDownParameterProperty);
        }
    }
}
