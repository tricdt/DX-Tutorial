using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.SalesDemo.Wpf.Helpers;
using System;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm.ModuleInjection;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class NavigationItemViewModel {
        public static NavigationItemViewModel Create(string moduleType, string caption, string description, BitmapImage glyph) {
            var res = ViewModelSource.Create(() => new NavigationItemViewModel());
            res.moduleType = moduleType;
            res.Caption = caption;
            res.Description = description;
            res.Glyph = glyph;
            return res;
        }
        protected NavigationItemViewModel() {
            if(this.IsInDesignMode())
                InitializeInDesignMode();
            ModuleManager.DefaultManager.GetEvents(this).Navigated += (s, e) => IsActive = true;
            ModuleManager.DefaultManager.GetEvents(this).NavigatedAway += (s, e) => IsActive = false;
        }
        void InitializeInDesignMode() {
            Caption = "Sales";
            Description = "Revenue" + Environment.NewLine + "Snapshots";
            Glyph = ResourceImageHelper.GetResourceImage("Sales.png");
        }

        string moduleType;
        public virtual string Caption { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual BitmapImage Glyph { get; protected set; }
        public virtual bool IsActive { get; set; }

        public void Activate() {
            if(IsActive) return;
            ModuleManager.DefaultManager.Navigate(Regions.Navigation, moduleType);
        }
        protected void OnIsActiveChanged() {
            if(IsActive)
                ModuleManager.DefaultManager.Navigate(Regions.Main, moduleType);
        }
    }
}
