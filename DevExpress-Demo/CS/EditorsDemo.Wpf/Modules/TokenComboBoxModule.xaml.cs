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
using DevExpress.DemoData.Models;
using DevExpress.Mvvm;
using DevExpress.Xpf.DemoBase.DataClasses;

namespace EditorsDemo {
    public partial class TokenComboBoxModule : EditorsDemoModule {
        public TokenComboBoxModule() {
            InitializeComponent();
        }
    }
    public class TokenComboBoxViewModel {

        public TokenComboBoxViewModel() {
            Countries = NWindContext.Create().CountriesArray;
            SelectedCountries = CreateSelectedCountries();
            MultiLineSelectedCountries = CreateMultiLineSelectedCountries();
        }
        
        
        public string[] Countries { get; private set; }
        public object SelectedCountries { get; private set; }
        public object MultiLineSelectedCountries{ get; private set; }

        List<object> CreateMultiLineSelectedCountries() {
            return new List<object>() {
                Countries[0],
                Countries[1],
                Countries[12],
                Countries[5],
                Countries[7],
                Countries[3],
                Countries[10],
                Countries[15],
                Countries[21],
                Countries[25],
                Countries[29],
                Countries[30],
                Countries[90],
                Countries[40],
                Countries[22],
                Countries[54],
                Countries[20],
                Countries[31],
                Countries[37],
                Countries[43],
                Countries[49],
                Countries[63],
                Countries[4],
                Countries[6],
                Countries[60],
                Countries[61],
                Countries[65],
                Countries[70],
                Countries[74],
                Countries[76],
                Countries[71],
                Countries[73],
            };
        }
        
        List<object> CreateSelectedCountries() {
            return new List<object>() {
                Countries[7],
                Countries[3],
                Countries[10]
            };
        }
    }
}
