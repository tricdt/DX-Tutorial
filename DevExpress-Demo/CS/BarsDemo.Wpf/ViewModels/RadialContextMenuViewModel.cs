using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace BarsDemo {
    public class RadialContextMenuViewModel {
        #region properties
        public IEnumerable<double?> FontSizes { get; protected set; }
        public IEnumerable<string> FontFamilies { get; protected set; }
        #endregion
        public RadialContextMenuViewModel() {
            FontSizes = new double?[] {3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 22, 24, 26, 28, 30,
                    32, 34, 36, 38, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 88, 96, 104, 112, 120, 128, 136, 144
                };
            FontFamilies = GetSystemFonts();
        }
        static IEnumerable<string> GetSystemFonts() {
            List<FontFamily> result = new List<FontFamily>();
            var lang = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            return FontUtility.GetSystemFontFamilies().Select(t => GetFontFamilyName(t, lang)).OrderBy(t => t);
        }
        static string GetFontFamilyName(FontFamily fontFamily, XmlLanguage lang) {
            try {
                return fontFamily.FamilyNames.ContainsKey(lang) ? fontFamily.FamilyNames[lang] : fontFamily.ToString();
            } catch {
            }
            return fontFamily.ToString();
        }
    }
    public class ClosePopupOnListEditClickBehaviour : Behavior<ListBoxEdit> {
        protected override void OnAttached() {
            base.OnAttached();            
            AssociatedObject.PreviewMouseLeftButtonUp += AssociatedObject_PreviewMouseLeftButtonUp;
            AssociatedObject.KeyDown += AssociatedObject_KeyDown;
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        void AssociatedObject_Loaded(object sender, RoutedEventArgs e) {
            AssociatedObject.ScrollIntoView(AssociatedObject.SelectedItem);
            AssociatedObject.Focus();
        }

        void AssociatedObject_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            if(e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Space) {
                ClosePopupAsync();
                e.Handled = true;
            }
        }
        void AssociatedObject_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if(LayoutTreeHelper.GetVisualParents(e.OriginalSource as DependencyObject).OfType<ListBoxItem>().Count() == 1)
                ClosePopupAsync();                            
        }
        void ClosePopupAsync() {
            Dispatcher.BeginInvoke(new Action(ClosePopup), DispatcherPriority.SystemIdle, new object[] { });
        }
        void ClosePopup() {
            ((BarPopupBase)LayoutTreeHelper.GetVisualParents(AssociatedObject).OfType<BarPopupBorderControl>().First().Popup).ClosePopup();
        }
    }
}
