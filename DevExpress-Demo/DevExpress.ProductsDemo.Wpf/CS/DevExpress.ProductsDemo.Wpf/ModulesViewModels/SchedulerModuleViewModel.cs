using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.XtraScheduler.iCalendar;

namespace ProductsDemo.Modules {
    public class SchedulerModuleViewModel : ViewModelBase {
        ICommand openScheduleCommand;
        ICommand saveScheduleCommand;

        public ObservableCollection<Appointment> Appointments { get { return Model.Appointments; } }
        protected CalendarModel Model { get; set; }

        public ICommand OpenScheduleCommand {
            get {
                if (openScheduleCommand == null)
                    openScheduleCommand = new OpenScheduleCommand(this);
                return openScheduleCommand;
            }
        }
        public ICommand SaveScheduleCommand {
            get {
                if (saveScheduleCommand == null)
                    saveScheduleCommand = new SaveScheduleCommand(this);
                return saveScheduleCommand;
            }
        }
        public SchedulerModuleViewModel() {
            Model = new CalendarModel();
        }
    }
    public abstract class CustomScheduleCommandBase : ICommand {
        SchedulerModuleViewModel viewModel;
        public CustomScheduleCommandBase(SchedulerModuleViewModel viewModel) {
            this.viewModel = viewModel;
        }

        public SchedulerModuleViewModel ViewModel { get { return viewModel; } }
        #region Event CanExecuteChanged
        public event EventHandler CanExecuteChanged;
        public virtual void OnCanExecuteChanged() {
            EventHandler handler = CanExecuteChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
        #endregion

        public bool CanExecute(object parameter) {
            return true;
        }
        public void Execute(object parameter) {
            ISchedulerExchangeCreator creator = parameter as ISchedulerExchangeCreator;
            if (creator == null)
                return;
            ExecuteCore(creator);
        }
        protected abstract void ExecuteCore(ISchedulerExchangeCreator creator);
    }
    public class OpenScheduleCommand : CustomScheduleCommandBase {
        public OpenScheduleCommand(SchedulerModuleViewModel viewModel)
            : base(viewModel) {
        
        }
        protected override void ExecuteCore(ISchedulerExchangeCreator creator) {
            ViewModel.Appointments.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "iCalendar Files (.ics)|*.ics";
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                iCalendarImporter importer = creator.CreateImporter();
                using (Stream stream = openFileDialog.OpenFile()) {
                    importer.Import(stream);
                }
            }
        }
    }
    public class SaveScheduleCommand : CustomScheduleCommandBase {
        public SaveScheduleCommand(SchedulerModuleViewModel viewModel) : base(viewModel) {
        }
        protected override void ExecuteCore(ISchedulerExchangeCreator creator) {
            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Filter = "iCalendar Files (.ics)|*.ics";
            if (openFileDialog.ShowDialog() == DialogResult.OK) {

                iCalendarExporter exporter = creator.CreateExporter();
                using (Stream stream = openFileDialog.OpenFile()) {
                    exporter.Export(stream);
                }
            }
        }
    }
    public interface ISchedulerExchangeCreator {
        iCalendarImporter CreateImporter();
        iCalendarExporter CreateExporter();
    }
}
