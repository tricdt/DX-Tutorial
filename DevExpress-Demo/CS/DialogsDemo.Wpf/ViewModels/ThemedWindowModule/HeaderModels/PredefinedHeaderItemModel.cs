using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Mvvm.DataAnnotations;

namespace DialogsDemo {
    public class HelpHeaderItemModel : BaseHeaderItemModel
    {
        public string ImageSource { get; set; }
        public bool CanHelp() { return true; }
        public void Help() { System.Diagnostics.Process.Start("http://www.devexpress.com"); }
        public HelpHeaderItemModel() { ResourceKey = "HelpHeaderItem"; }
    }
    public class PinHeaderItemModel : BaseHeaderItemModel
    {
        public string ImageSource { get; set; }
        public bool CanPin() { return !BaseModel.PinWindow; }
        [Command(UseCommandManager = true)]
        public void Pin() { BaseModel.PinWindow = true; }
        public PinHeaderItemModel() { ResourceKey = "PinHeaderItem"; }
    }
}
