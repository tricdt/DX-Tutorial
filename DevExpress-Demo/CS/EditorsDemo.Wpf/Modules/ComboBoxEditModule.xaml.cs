using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Editors;
using System.Windows.Markup;
using System.IO;
using DevExpress.DemoData.Models;

namespace EditorsDemo {
    public partial class ComboBoxEditModule : EditorsDemoModule {
        public ComboBoxEditModule() {
            DataContext = new ComboBoxEditViewModel();
            InitializeComponent();
        }

    }
    public class ComboBoxEditViewModel : EditorsViewModelBase {
        public ComboBoxEditViewModel() : base() {
            var context = NWindContext.Create();
            Countries = context.CountriesArray;
            Categories = context.Categories.ToList();
            SelectedCategories = new Category[] { Categories[2], Categories[5], Categories[6] };
            SelectedProduct = Products[5].ProductName;
        }
        public IEnumerable<string> Countries { get; private set; }
        public IList<Category> Categories { get; private set; }
        public object SelectedCategories { get; set; }
        public string SelectedProduct { get; set; }
    }
}
