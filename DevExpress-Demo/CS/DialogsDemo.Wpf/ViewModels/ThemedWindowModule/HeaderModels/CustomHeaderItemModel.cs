using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace DialogsDemo {
    public class CustomHeaderItemModel : BaseHeaderItemModel {
        protected virtual IMessageBoxService MessageBoxService { get { return null; } }
        public string Content { get; set; }

        public void Click() { MessageBoxService.ShowMessage(String.Format("{0} command executed!", Content), "Demo", MessageButton.OK, MessageIcon.Information, MessageResult.OK); }
        public CustomHeaderItemModel(int index) {
            ResourceKey = "CustomHeaderItem";
            Content = "Item " + index;
        }
    }
}
