using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI.ModuleInjection;
using DevExpress.WindowsMailClient.Wpf.ViewModel;
using DevExpress.Xpf.WindowsUI;

namespace DevExpress.WindowsMailClient.Wpf.Internal {
    public class HamburgerMenuWrapper : ISelectorWrapper<HamburgerMenu> {
        readonly DXObservableCollection<object> bottomItems = new DXObservableCollection<object>();
        IDisposable collectionBinder;
        HamburgerMenu target;

        public HamburgerMenuWrapper() {
            bottomItems.CollectionChanged += BottomItems_CollectionChanged;
            bottomItems.BeforeClear += BottomItems_BeforeClear;
        }

        public event EventHandler SelectionChanged;

        void BottomItems_BeforeClear(object sender, EventArgs e) {
            Unsubscribe(sender as IList);
        }
        void BottomItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            switch (e.Action) {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    Subscribe(e.NewItems);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    Unsubscribe(e.OldItems);
                    break;
            }
        }
        void OnRadioButtonChecked(object sender, RoutedEventArgs e) {
            if (sender is HamburgerMenuBottomBarRadioButton) {
                object dataContext = ((HamburgerMenuBottomBarRadioButton)sender).DataContext;
                var mailHamburgerMenuViewModel = dataContext as MailHamburgerMenuViewModel;
                if(mailHamburgerMenuViewModel != null) {
                    Target.ItemsSource = mailHamburgerMenuViewModel.ItemsSource;
                    Target.DataContext = mailHamburgerMenuViewModel.ContentViewModel;
                }
                else {
                    Target.ClearValue(HamburgerMenu.ItemsSourceProperty);
                    Target.ClearValue(FrameworkElement.DataContextProperty);
                }
            }
            if (SelectionChanged != null)
                SelectionChanged(this, EventArgs.Empty);
        }

        void Subscribe(IList items) {
            if (items == null) return;
            foreach (var item in items) {
                var radioButton = item as HamburgerMenuBottomBarRadioButton;
                if (radioButton != null) radioButton.Checked += OnRadioButtonChecked;
            }
        }
        void Unsubscribe(IList items) {
            if (items == null) return;
            foreach (var item in items) {
                var radioButton = item as HamburgerMenuBottomBarRadioButton;
                if (radioButton != null) radioButton.Checked -= OnRadioButtonChecked;
            }
        }

        public object ItemsSource { get { return Target.BottomBarItemsSource; } set { Target.BottomBarItemsSource = value; } }
        public virtual DataTemplate ItemTemplate { get { return Target.ItemTemplate; } set { } }
        public virtual DataTemplateSelector ItemTemplateSelector { get { return Target.ItemTemplateSelector; } set { } }
        public object SelectedItem {
            get {
                var selectedContainer = Target.BottomBarItems.OfType<HamburgerMenuBottomBarRadioButton>().FirstOrDefault(x => x.IsChecked == true);
                return selectedContainer != null ? selectedContainer.DataContext : null;
            }
            set {
                var selectedContainer = Target.ContainerFromItem(value) as HamburgerMenuBottomBarRadioButton;
                if (selectedContainer != null) selectedContainer.IsChecked = true;
            }
        }
        public HamburgerMenu Target {
            get { return target; }
            set {
                if (target == value) return;
                if (target != null) {
                    if (collectionBinder != null)
                        collectionBinder.Dispose();
                }
                target = value;
                if (target != null)
                    collectionBinder = CollectionBinder.BindOneWay(Target.BottomBarItems, bottomItems, x => x);
            }
        }
    }
}
