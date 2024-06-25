using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Resources;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.DemoBase.Helpers.Internal;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace TreeListDemo {
    public class CodeViewer : CodeViewPresenter {
        public static readonly DependencyProperty CurrentItemsCollectionProperty = DependencyProperty.Register("CurrentItemsCollection", typeof(IEnumerable), typeof(CodeViewer), new PropertyMetadata(new PropertyChangedCallback(OnCurrentItemsCollectionChanged)));

        public static void OnCurrentItemsCollectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            ((CodeViewer)o).OnCurrentItemsCollectionChanged();
        }

        public CodeViewer() {
            FontFamily = new FontFamily("Consolas");
            FontSize = 13;
        }
        public IEnumerable CurrentItemsCollection {
            get { return (IEnumerable)GetValue(CurrentItemsCollectionProperty); }
            set { SetValue(CurrentItemsCollectionProperty, value); }
        }
        string LoadSourceCode(Type type) {
            string resourcePath =
                string.Format("/{0};component/Data/DataAnnotations/{1}{2}",
                    type.Assembly.FullName.Split(',')[0],
                    type.Name,
                    DemoHelper.GetCodeSuffix(type.Assembly));
            StreamResourceInfo resource = Application.GetResourceStream(new Uri(resourcePath, UriKind.Relative));
            return resource != null ? new StreamReader(resource.Stream).ReadToEnd() : null;
        }
        void OnCurrentItemsCollectionChanged() {
            if(CurrentItemsCollection != null) {
                Type elementType = ((IValueConverter)new IEnumerableToFirstItemConverter()).Convert(CurrentItemsCollection, null, null, CultureInfo.CurrentCulture).GetType();
                CodeText = new CodeLanguageText(DemoHelper.GetDemoLanguage(elementType.Assembly), LoadSourceCode(elementType));
            }
            else
                CodeText = null;
        }
    }

}
