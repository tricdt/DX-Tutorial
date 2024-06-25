using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.DemoBase.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WindowsUIDemo {
    public class NotificationsViewModel {
        public virtual bool ShowImage { get; set; }
        public virtual NotificationTemplate Template { get; set; }
        public virtual INotificationService NotificationService { get { return null; } }
        public virtual ILogService LogService { get { return null; } }

        public string AppModelID { get; private set; }
        public int PredefinedNotificationId { get; set; }

        protected NotificationsViewModel() {
            ShowImage = true;
            CustomNotificationWidth = 380;
            CustomNotificationHeight = 100;
            CustomNotificationBackground = Color.FromRgb(0xf7, 0x94, 0x1e);
            PredefinedNotificationId = 100;
        }

        public void ShowCustom() {
            CustomToastViewModel viewModel = ViewModelSource.Create(() => new CustomToastViewModel {
                Width = CustomNotificationWidth,
                Height = CustomNotificationHeight,
                Text = CustomNotificationText,
                Background = CustomNotificationBackgroundBrush
            });
            INotification notification = NotificationService.CreateCustomNotification(viewModel);
            Show(notification);
        }

        public void ShowPredefined() {
            ImageSource image = ShowImage ? new BitmapImage(new Uri("pack://application:,,,/WindowsUIDemo;component/Images/sun.png", UriKind.Absolute)) : null;
            string text1 = "Lorem ipsum dolor sit amet integer fringilla, dui eget ultrices cursus, justo tellus.";
            string text2 = "In ornare ante magna, eget volutpat mi bibendum a. Nam ut ullamcorper libero. Pellentesque habitant.";
            string text3 = "Quisque sapien odio, mollis tincidunt";
            INotification notification = NotificationService.CreatePredefinedNotification(text1, text2, text3, image, PredefinedNotificationId.ToString());
            PredefinedNotificationId++;
            Show(notification);
        }

        void Show(INotification notification) {
            LogService.LogLine("Showing...");
            notification.ShowAsync().ContinueWith(OnNotificationShown, TaskScheduler.FromCurrentSynchronizationContext());
        }

        void OnNotificationShown(Task<NotificationResult> task) {
            try {
                switch(task.Result) {
                    case NotificationResult.Activated:
                        LogService.LogLine("Activated");
                        break;
                    case NotificationResult.TimedOut:
                        LogService.LogLine("Timed out");
                        break;
                    case NotificationResult.UserCanceled:
                        LogService.LogLine("Canceled by user");
                        break;
                    case NotificationResult.Dropped:
                        LogService.LogLine("Dropped (the queue is full)");
                        break;
                }
            } catch(AggregateException e) {
                var actualException = e.InnerException.InnerException;
                var exceptionMessage = actualException != null ? actualException.Message : e.InnerException.Message;
                LogService.LogLine(exceptionMessage.ToUpper().Contains("803E0114")
                    ? "System settings prevent native notifications from being delivered. You can enable notifications in the Settings app: Settings | System | Notifications & actions."
                    : ("Error: " + exceptionMessage));
            }
            LogService.LogLine("");
        }

        protected void OnCustomNotificationBackgroundChanged() {
            CustomNotificationBackgroundBrush = new SolidColorBrush { Color = CustomNotificationBackground };
        }

        public virtual string CustomNotificationText { get; set; }
        public virtual double CustomNotificationWidth { get; set; }
        public virtual double CustomNotificationHeight { get; set; }
        public virtual Color CustomNotificationBackground { get; set; }
        public virtual SolidColorBrush CustomNotificationBackgroundBrush { get; set; }
    }

    [POCOViewModel]
    public class CustomToastViewModel {
        public virtual string Text { get; set; }
        public virtual double Height { get; set; }
        public virtual double Width { get; set; }
        public virtual SolidColorBrush Background { get; set; }
    }
    public class NotificationInfo {
        public NotificationTemplate Template { get; set; }
        public string Description { get; set; }
        public string Win10Description { get; set; }
    }
}
