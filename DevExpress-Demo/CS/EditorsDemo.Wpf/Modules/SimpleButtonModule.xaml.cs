using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Editors;
using DevExpress.DemoData.Models;
using DevExpress.Xpf.Core;
using System.Collections.ObjectModel;
using DevExpress.Xpf.PropertyGrid;
using DevExpress.Xpf.Bars;
using DevExpress.Mvvm;
using System.Threading.Tasks;

namespace EditorsDemo {

    public partial class SimpleButtonModule : EditorsDemoModule {
        public ObservableCollection<string> EventLog { get; }
        private AsyncCommand asynCommand;
        public SimpleButtonModule() {
            InitializeComponent();
            EventLog = new ObservableCollection<string>();
            rbSimpleButton.IsChecked = true;
            simpleButtonAsync.Command = asynCommand = new AsyncCommand(OnAsyncButton);
        }

        void OnRadioButtonChecked(object sender, RoutedEventArgs e) {
            RadioButton radioButton = sender as RadioButton;
            switch (radioButton.Name) {
                case "rbSimpleButton":
                    propertyGrid.SelectedObject = simpleButton;
                    break;
                case "rbSplitButton":
                    propertyGrid.SelectedObject = splitButton;
                    break;
                case "rbDropDownButton":
                    propertyGrid.SelectedObject = dropdownButton;
                    break;
                case "rbAsyncSimpleButton":
                    propertyGrid.SelectedObject = simpleButtonAsync;
                    break;
            }
        }

        void AddStringInLog(string message) {
            EventLog.Add(message);
            if(EventLog.Count > 20)
                EventLog.RemoveAt(0);
        }

        void AddStringInLog(object button) {
            var buttonItem = button as BarButtonItem;
            if(buttonItem != null)
                AddStringInLog(string.Format("{0} - {1} '{2}' Click", DateTime.Now.ToShortTimeString(), buttonItem.GetType().Name, buttonItem.Content));
            else
                AddStringInLog(string.Format("{0} - {1} Click", DateTime.Now.ToShortTimeString(), button.GetType().Name));
        }

        void OnButtonClick(object sender, RoutedEventArgs e) {
            var button = sender as SimpleButton;
            if (button != null && button.ButtonKind == ButtonKind.Toggle)
                AddStringInLog(string.Format("{0} - {1} state is: {2}", DateTime.Now.ToShortTimeString(), button.GetType().Name, button.IsChecked));
            AddStringInLog(sender);
        }

        void OnBarButtonItemClick(object sender, ItemClickEventArgs e) {
            AddStringInLog(sender);
        }

        async Task OnAsyncButton() {
            AddStringInLog(string.Format("{0} - Async SimpleButton Action Started", DateTime.Now.ToShortTimeString()));
            var idx = 0;
            while(idx++ < 300) {
                if(asynCommand.IsCancellationRequested) {
                    AddStringInLog(string.Format("{0} - Async SimpleButton: Action Cancelled", DateTime.Now.ToShortTimeString()));
                    return;
                }
                await Task.Delay(10);
            }
            AddStringInLog(string.Format("{0} - Async SimpleButton: Action Finished", DateTime.Now.ToShortTimeString()));
        }
    }
}
