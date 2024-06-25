using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shell;
using CommonDemo.Helpers;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils;
using DevExpress.Xpf.Core.Native;

namespace ControlsDemo {
    public class TaskbarServicesViewModel : IDisposable {
        public TaskbarServicesViewModel() {
            overlayIcons = new NamedIcon[] {
                new NamedIcon () {
                    Caption = "Moon",
                    Icon = ImagesHelper.GetSvgImage("pack://application:,,,/ControlsDemo;component/Images/Moon.svg", new Size(16,16)) },
                new NamedIcon () {
                    Caption = "Sun",
                    Icon = ImagesHelper.GetSvgImage("pack://application:,,,/ControlsDemo;component/Images/Sun.svg", new Size(16,16)) }
            };
            buttonProperties = new ObservableCollection<bool> { true, true, true, false, true };
            buttonProperties.CollectionChanged += ButtonPropertyChanged;
        }

        public void Dispose() {
            TaskbarButtonService.Description = null;
            TaskbarButtonService.OverlayIcon = null;
            TaskbarButtonService.ProgressState = TaskbarItemProgressState.None;
            TaskbarButtonService.ThumbButtonInfos.Clear();
            TaskbarButtonService.ThumbnailClipMarginCallback = null;
            TaskbarButtonService.ThumbnailClipMargin = new Thickness();
            ApplicationJumpListService.Items.Clear();
            ApplicationJumpListService.Apply();
        }
        readonly IEnumerable<NamedIcon> overlayIcons;
        public IEnumerable<NamedIcon> OverlayIcons { get { return overlayIcons; } }
        readonly ObservableCollection<bool> buttonProperties;
        public IEnumerable<bool> ButtonProperties { get { return buttonProperties; } }

        public virtual double ThumbnailClipMarginMultipliyer { get; set; }
        public virtual bool ThumbButtonsCreate { get; set; }

        public virtual Thickness ThumbnailClipMargin { get; protected set; }

        [Required]
        protected virtual ITaskbarButtonService TaskbarButtonService { get { return null; } }
        [Required]
        protected virtual IApplicationJumpListService ApplicationJumpListService { get { return null; } }
        [Required]
        protected virtual IDialogService DialogService { get { return null; } }
        [Required]
        protected virtual IMessageBoxService MessageBoxService { get { return null; } }

        protected virtual void OnThumbnailClipMarginMultipliyerChanged() {
            TaskbarButtonService.UpdateThumbnailClipMargin();
        }
        protected virtual void OnThumbButtonsCreateChanged() {
            if(ThumbButtonsCreate) {
                TaskbarButtonService.ThumbButtonInfos.Add(new TaskbarThumbButtonInfo {
                    Description = "Zoom out",
                    ImageSource = ImagesHelper.GetSvgImage("pack://application:,,,/ControlsDemo;component/Images/TaskbarServices/ZoomOut.svg", new Size(32, 32)),
                    Action = () => DecreaseThumbnailClipMarginMultipliyer()
                });
                TaskbarButtonService.ThumbButtonInfos.Add(new TaskbarThumbButtonInfo {
                    Description = "Zoom in",
                    ImageSource = ImagesHelper.GetSvgImage("pack://application:,,,/ControlsDemo;component/Images/TaskbarServices/ZoomIn.svg", new Size(32, 32)),
                    Action = () => IncreaseThumbnailClipMarginMultipliyer()
                });
                SetButtonsProperties();
            } else {
                TaskbarButtonService.ThumbButtonInfos.Clear();
            }
        }
        void ButtonPropertyChanged(object sender, NotifyCollectionChangedEventArgs e) {
            SetButtonsProperties();
        }
        void SetButtonsProperties() {
            foreach(TaskbarThumbButtonInfo item in TaskbarButtonService.ThumbButtonInfos) {
                item.IsEnabled = buttonProperties[0];
                item.IsInteractive = buttonProperties[1];
                item.IsBackgroundVisible = buttonProperties[2];
                item.DismissWhenClicked = buttonProperties[3];
                item.Visibility = (buttonProperties[4]) ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        bool DecreaseThumbnailClipMarginMultipliyer() {
            ThumbnailClipMarginMultipliyer = (ThumbnailClipMarginMultipliyer >= 10) ? (ThumbnailClipMarginMultipliyer - 10) : 0;
            TaskbarButtonService.UpdateThumbnailClipMargin();
            return true;
        }
        bool IncreaseThumbnailClipMarginMultipliyer() {
            ThumbnailClipMarginMultipliyer = (ThumbnailClipMarginMultipliyer <= 90) ? (ThumbnailClipMarginMultipliyer + 10) : 100;
            TaskbarButtonService.UpdateThumbnailClipMargin();
            return true;
        }
        Thickness GetThumbnailClipMargin(Size size) {
            return ThumbnailClipMargin = new Thickness {
                Left = 3.0 * (double)ThumbnailClipMarginMultipliyer * size.Width / (5.0 * 100.0),
                Top = 2.0 * (double)ThumbnailClipMarginMultipliyer * size.Height / (5.0 * 100.0),
                Right = 2.0 * (double)ThumbnailClipMarginMultipliyer * size.Width / (5.0 * 100.0),
                Bottom = 3.0 * (double)ThumbnailClipMarginMultipliyer * size.Height / (5.0 * 100.0)
            };
        }
        void AddTask(NewJumpTaskWindowViewModel task) {
            string customCategory = string.IsNullOrEmpty(task.CustomCategory) ? null : task.CustomCategory;
            ApplicationJumpListService.Items.AddOrReplace(customCategory, task.Title, task.Icon.Icon, task.Description,
                () => MessageBoxService.ShowMessage(task.MessageText));
            IEnumerable<RejectedApplicationJumpItem> rejectedItems = ApplicationJumpListService.Apply();
            foreach(var rejectedItem in rejectedItems) {
                var rejectedTask = (ApplicationJumpTaskInfo)rejectedItem.JumpItem;
                MessageBoxService.ShowMessage(string.Format("Error: {0}", rejectedItem.Reason), rejectedTask.Title, MessageButton.OK, MessageIcon.Error);
            }
        }
        public void OnLoaded() {
            ThumbButtonsCreate = true;
            ThumbnailClipMarginMultipliyer = 20;
            TaskbarButtonService.ThumbnailClipMarginCallback = GetThumbnailClipMargin;
        }
        public void OpenTaskWindow() {
            NewJumpTaskWindowViewModel taskAddition = ViewModelSource.Create(() => new NewJumpTaskWindowViewModel());
            IDataErrorInfo errorInfo = (IDataErrorInfo)taskAddition;
            UICommand okCommand = new UICommand() {
                Caption = "OK",
                IsCancel = false,
                IsDefault = true,
                Command = new DelegateCommand<CancelEventArgs>(x => { }, x => string.IsNullOrEmpty(errorInfo["Title"]))
            };
            UICommand cancelCommand = new UICommand() {
                Caption = "Cancel",
                IsCancel = true,
                IsDefault = false,
            };
            if(DialogService.ShowDialog(new List<UICommand>() { okCommand, cancelCommand }, "Add Jump List Task", "NewJumpTaskWindow", taskAddition) == okCommand)
                AddTask(taskAddition);
        }
    }
    public class NamedIcon {
        public string Caption { get; set; }
        public ImageSource Icon { get; set; }
    };
}
