using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;

namespace DockingDemo.ViewModels {
    public class RibbonMergingViewModel {
        public RibbonMergingViewModel() {
            Panels = new ObservableCollection<PanelViewModel>();
            Panels.Add(TextPanelViewModel.Create("Simple Pad", new Point(0.0, 0.0), this));
            Panels.Add(PaintPanelViewModel.Create("Simple Paint", new Point(300.0, 50.0), this));
        }
        public virtual IList<PanelViewModel> Panels { get; set; }
        public void CreateNewTextPanel() {
            Panels.Add(TextPanelViewModel.Create("Simple Pad", new Point(50, 50), this));
            Panels[Panels.Count - 1].IsActive = true;
        }
        public void CreateNewPaintPanel() {
            Panels.Add(PaintPanelViewModel.Create("Simple Paint", new Point(70, 60), this));
            Panels[Panels.Count - 1].IsActive = true;
        }
    }
    [POCOViewModel]
    public class TextPanelViewModel : PanelViewModel {
        protected TextPanelViewModel() { }
        public static TextPanelViewModel Create(string caption, Point location, RibbonMergingViewModel parentViewModel) {
            TextPanelViewModel instance = ViewModelSource.Create(() => new TextPanelViewModel());
            instance.ParentViewModel = parentViewModel;
            instance.Caption = caption;
            instance.Location = location;
            return instance;
        }
    }
    [POCOViewModel]
    public class PaintPanelViewModel : PanelViewModel {
        protected PaintPanelViewModel() { }
        public static PaintPanelViewModel Create(string caption, Point location, RibbonMergingViewModel parentViewModel) {
            PaintPanelViewModel instance = ViewModelSource.Create(() => new PaintPanelViewModel());
            instance.Caption = caption;
            instance.Location = location;
            instance.ParentViewModel = parentViewModel;
            return instance;
        }
    }
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
