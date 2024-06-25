using System.Windows;
using System.Windows.Input;

namespace MapDemo {
    public class PlaceInfoControl : VisibleControl {
        ChangePlaceCommand changePlaceCommand;
        public static readonly DependencyProperty PlaceInfoProperty = DependencyProperty.Register("PlaceInfo",
            typeof(PlaceInfo), typeof(PlaceInfoControl),new PropertyMetadata(null));

        public PlaceInfo PlaceInfo {
            get { return (PlaceInfo)GetValue(PlaceInfoProperty); }
            set { SetValue(PlaceInfoProperty, value); }
        }
        public ChangePlaceCommand ChangePlaceCommand { get { return changePlaceCommand; } }

        public event RoutedEventHandler ShowNextPlace;
        public event RoutedEventHandler ShowPrevPlace;

        public PlaceInfoControl() {
            changePlaceCommand = new ChangePlaceCommand(this);
            DefaultStyleKey = typeof(PlaceInfoControl);
        }
        public void OnShowNextPlace() {
            if(ShowNextPlace != null)
                ShowNextPlace(this, new RoutedEventArgs());
        }
        public void OnShowPreviousPlace() {
            if(ShowPrevPlace != null)
                ShowPrevPlace(this, new RoutedEventArgs());
        }
    }

    public class ChangePlaceCommand : ICommand {
        readonly PlaceInfoControl placeInfoControl;

        public ChangePlaceCommand(PlaceInfoControl placeInfoControl) {
            this.placeInfoControl = placeInfoControl;
        }
        event System.EventHandler _canExecuteChanged;
        bool ICommand.CanExecute(object parameter) {
            if(_canExecuteChanged != null)
                return placeInfoControl != null;
            return false;
        }
        event System.EventHandler ICommand.CanExecuteChanged { 
            add { _canExecuteChanged += value; }
            remove { _canExecuteChanged -= value; }
        }
        void ICommand.Execute(object parameter) {
            if (!(parameter is NavDirection))
                return;
            NavDirection navDirection = (NavDirection)parameter;
            switch(navDirection){
                case NavDirection.Next: placeInfoControl.OnShowNextPlace();
                    break;
                case NavDirection.Previous:
                    placeInfoControl.OnShowPreviousPlace();
                    break;
            }
        }
    }

    public enum NavDirection{Next, Previous}
}
