using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Media;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;

namespace DialogsDemo {
    public class ThemedWindowViewModel {
        public ThemedWindowViewModel() {
            PinWindow = true;
            ShowTitle = true;             
            InitializeHeaderItems();
            OnWindowKindChanged();
        }

        void InitializeHeaderItems() {
            HeaderItems = new ObservableCollection<BaseHeaderItemModel>();
            HeaderItems.Add(CreateChildModel<WindowTitleEditorHeaderItemModel>());
            HeaderItems.Add(CreateChildModel<HelpHeaderItemModel>());
            HeaderItems.Add(CreateChildModel<PinHeaderItemModel>());
            ShowPredefinedItems = true;
        }

        protected virtual IWindowService WindowService { get { return null; } }        

        public virtual bool PinWindow { get; set; }
        public virtual bool ShowTitle { get; set; }
        
        public virtual bool UseGlowColors { get; set; }

        public virtual WindowTitleAlignment TitleAlignment { get; set; }
        public virtual WindowKind WindowKind { get; set; }
        public virtual string Title { get; set; }
        public virtual ImageSource Icon { get; set; }
        public virtual SolidColorBrush ActiveGlowColor { get; set; }
        public virtual SolidColorBrush InactiveGlowColor { get; set; }
        public virtual bool ShowPredefinedItems { get; set; }

        public virtual BaseThemedWindowContentModel Content { get; set; }
        
        public ObservableCollection<BaseHeaderItemModel> HeaderItems { get; private set; }

        public void TogglePredefinedItemsVisibility() {
            ShowPredefinedItems = !ShowPredefinedItems;
        }

        
        public void ShowWindow() { WindowService.Show(this); }

        public bool CanAddHeaderItem() { return true; }

        public void AddHeaderItem() {
            var model = ViewModelSource<CustomHeaderItemModel>.Create(HeaderItems.Count-3);
            model.SetParentViewModel(this);
            HeaderItems.Insert(0, model);
        }

        public bool CanRemoveHeaderItem() { return HeaderItems.Count > 3; }        
        [Command(UseCommandManager = true)]
        public void RemoveHeaderItem() { HeaderItems.RemoveAt(0); }

        protected void OnShowPredefinedItemsChanged() {
            for (int i = 1; i <= 3; i++)
                HeaderItems[HeaderItems.Count-i].IsVisible = ShowPredefinedItems;
        }
        protected TChildModel CreateChildModel<TChildModel>() {
            var model = ViewModelSource<TChildModel>.Create();
            model.SetParentViewModel(this);
            return model;
        }

        protected void OnWindowKindChanged() {
            switch (WindowKind) {
                case WindowKind.Normal:
                case WindowKind.Auto:
                    Content = CreateChildModel<SimpleThemedWindowContentModel>();
                    break;
                case WindowKind.Tabbed:
                    Content = CreateChildModel<TabbedThemedWindowContentModel>();
                    break;
                case WindowKind.Ribbon:
                    Content = CreateChildModel<RibbonThemedWindowContentModel>();
                    break;
            }
        }
    }
}
