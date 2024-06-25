using System;
using System.Linq;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;

namespace ControlsDemo {
    public class TileNavPaneViewModel : TileNavBaseViewModel {
        public TileNavPaneViewModel() {
            Messenger.Default.Register<NotificationMessage>(this, OnNotificationMessage);
            ShowNotificationCommand = DelegateCommandFactory.Create<string>(OnShowNotificationCommand);
        }

        public ICommand ShowNotificationCommand { get; private set; }
        IMessageBoxService MessageBoxService { get { return ServiceContainer.GetService<IMessageBoxService>(); } }

        protected virtual void OnNotificationMessage(NotificationMessage message) {
            OnShowNotificationCommand(message.Source);
        }
        protected override void OnViewUnloaded() {
            base.OnViewUnloaded();
            Messenger.Default.Unregister<NotificationMessage>(this, OnNotificationMessage);
        }
        void OnShowNotificationCommand(string message) {
            MessageBoxService.ShowMessage(message);
        }
    }
}
