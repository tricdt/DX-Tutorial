using System;
using System.Collections.Generic;
using System.Windows;
namespace RibbonDemo {
    public abstract class PanelViewModel {
        protected RibbonMergingViewModel ParentViewModel { get; set; }
        public virtual string Caption { get; set; }
        public virtual Point Location { get; set; }
        public virtual bool IsActive { get; set; }
        public IEnumerable<double?> FontSizes { get; protected set; }
        public virtual void Close(object param) {
            ParentViewModel.Panels.Remove(this);
            if(ParentViewModel.Panels.Count != 0)
                ParentViewModel.Panels[0].IsActive = true;
        }
        public PanelViewModel() {
            FontSizes = new Nullable<double>[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 22, 24, 26, 28, 30,
                    32, 34, 36, 38, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 88, 96, 104, 112, 120, 128, 136, 144
                };
        }
    }
}
