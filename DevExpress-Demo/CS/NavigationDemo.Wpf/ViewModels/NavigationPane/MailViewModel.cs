using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;

namespace NavigationDemo {
    public class MailViewModel : ViewModelBase {
        public virtual IList<MailItem> ItemsSource { get; set; }

        public virtual int SentGroupIndex { get; set; }
        public virtual int ReceiveGroupIndex { get; set; }

        public virtual NavigationId FolderId { get; set; }

        public MailViewModel() {
            SentGroupIndex = -1;
            ReceiveGroupIndex = -1;
        }
        protected override void OnParameterChanged(object parameter) {
            if(parameter != null)
                UpdateIds((NavigationId)parameter);
        }
        void UpdateIds(NavigationId id) {
            FolderId = id;
            switch(id) {
                case NavigationId.Inbox: SentGroupIndex = -1; ReceiveGroupIndex = 0; break;
                case NavigationId.Outbox: SentGroupIndex = 0; ReceiveGroupIndex = -1; break;
                case NavigationId.SentItems: SentGroupIndex = 0; ReceiveGroupIndex = -1; break;
                case NavigationId.DeletedItems: SentGroupIndex = -1; ReceiveGroupIndex = 0; break;
                case NavigationId.Drafts: SentGroupIndex = 0; ReceiveGroupIndex = -1; break;
            }
            ItemsSource = NavigationPaneData.MailData[id];
        }
    }
}
