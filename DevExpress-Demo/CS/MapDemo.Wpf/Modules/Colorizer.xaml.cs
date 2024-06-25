using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media;
using DevExpress.Xpf.Map;
using System.ComponentModel;

namespace MapDemo {
    public partial class Colorizer : MapDemoModule, INotifyPropertyChanged {
        string toolTipPattern;
        public event PropertyChangedEventHandler PropertyChanged;
        public string ToolTipPattern {
            get { return toolTipPattern; }
            set {
                if (toolTipPattern != value) {
                    toolTipPattern = value; 
                    NotifyPropertyChanged("ToolTipPattern");
                }
            }
        }

        readonly MapColorizer gdpColorizer;
        readonly MapColorizer populationColorizer;
        readonly MapColorizer politicalColorizer;

        public Colorizer() {
            InitializeComponent();
            DataContext = this;
            gdpColorizer = Resources["gdpColorizer"] as MapColorizer;
            populationColorizer = Resources["populationColorizer"] as MapColorizer;
            politicalColorizer = Resources["politicalColorizer"] as MapColorizer;
            vectorLayer.Colorizer = gdpColorizer;
            ToolTipPattern = "{NAME} : ${GDP_MD_EST:n2}M";
        }

        void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        void lbMapType_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if (lbMapType.SelectedIndex == 0) {
                vectorLayer.Colorizer = gdpColorizer;
                ToolTipPattern = "{NAME} : ${GDP_MD_EST:n2}M";
            }
            if (lbMapType.SelectedIndex == 1) {
                vectorLayer.Colorizer = populationColorizer;
                ToolTipPattern = "{NAME} : {POP_EST:#,##0,,}M"; 
            }
            if (lbMapType.SelectedIndex == 2) {
                vectorLayer.Colorizer = politicalColorizer;
                ToolTipPattern = "{NAME}";
            }
        }
    }
}
