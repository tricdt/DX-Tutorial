using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVVMDemo.Services {
    public class MessageBoxServiceViewModel :ViewModelBase {
        public void ShowMessage(string serviceName) {
            IMessageBoxService messageBoxService = this.GetRequiredService<IMessageBoxService>(serviceName);
            Result = messageBoxService.ShowMessage(Text, Caption, Buttons, Icon, DefaultButton);
        }

        #region properties
        public string Text { get; set; }
        public string Caption { get; set; }
        public MessageIcon Icon { get; set; }
        public virtual MessageResult? Result { get; protected set; }
        MessageButton buttons;
        public MessageButton Buttons {
            get { return buttons; }
            set { SetValue(ref buttons, value, OnButtonsChanged); }
        }
        MessageResult defaultButton;
        public MessageResult DefaultButton {
            get { return defaultButton; }
            set { SetValue(ref defaultButton, value, UpdatePredefinedFormats); }
        }
        MessageResult messageResults;
        public MessageResult MessageResults {
            get { return messageResults; }
            set { SetValue(ref messageResults, value); }
        }
        bool allowTextSelection;
        public bool AllowTextSelection {
            get { return allowTextSelection; }
            set { SetValue(ref allowTextSelection, value); }
        }

        TimeSpan? timerTimeout;
        public TimeSpan? TimerTimeout {
            get { return timerTimeout; }
            set { SetValue(ref timerTimeout, value, UpdatePredefinedFormats); }
        }

        string timerFormat;
        public string TimerFormat {
            get { return timerFormat; }
            set { SetValue(ref timerFormat, value); }
        }
        IList<PredefinedFormat> predefinedFormats;
        public IList<PredefinedFormat> PredefinedFormats {
            get { return predefinedFormats; }
            set { SetValue(ref predefinedFormats, value); }
        }
        public IEnumerable<MessageIcon> Icons {
            get {
                return Enum.GetValues(typeof(MessageIcon)).Cast<MessageIcon>().Distinct();
            }
        }
        protected MessageBoxServiceViewModel() {
            Text = "Message text";
            Caption = "Caption";
            Buttons = MessageButton.OKCancel;
            Icon = MessageIcon.Information;
            AllowTextSelection = true;
            TimerTimeout = TimeSpan.FromSeconds(5);
            TimerFormat = "{0} ({1:%s})";
        }

        #endregion

        #region methods
        void OnButtonsChanged() {
            DefaultButton = Buttons.ToMessageResults().FirstOrDefault();
        }
        void UpdatePredefinedFormats() {
            if (TimerTimeout.HasValue)
                PredefinedFormats = Helpers.GeneratePredefinedFormat(DefaultButton, TimerTimeout.Value);
        }
        #endregion
    }
}
