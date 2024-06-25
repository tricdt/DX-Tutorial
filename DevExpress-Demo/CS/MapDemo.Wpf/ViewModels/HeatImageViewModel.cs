using System;
using System.Collections.Generic;
using System.Windows.Media;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Map;

namespace MapDemo {
    [POCOViewModel]
    public class HeatImageViewModel {
        public List<PaletteItem> PaletteItems { get; set; }
        public virtual PaletteItem ActualPaletteItem { get; set; }

        public HeatImageViewModel() {
            CreatePaletteItems();
            ActualPaletteItem = PaletteItems[1];
        }

        void CreatePaletteItems() {
            List<PaletteItem> paletteItems = new List<PaletteItem>();
            paletteItems.Add(new PaletteItem("Default", null));
            paletteItems.Add(new PaletteItem("Hot", CreateHotColorizer()));
            paletteItems.Add(new PaletteItem("Cold", CreateColdColorizer()));
            PaletteItems = paletteItems;
        }
        ChoroplethColorizer CreateHotColorizer() {
            ChoroplethColorizer colorizer = new ChoroplethColorizer();
            colorizer.RangeStops = new DoubleCollection() { 0.1, 0.2, 0.7, 1.0 };
            colorizer.Colors.Add(Color.FromArgb(50, 128, 255, 0));
            colorizer.Colors.Add(Color.FromArgb(255, 255, 255, 0));
            colorizer.Colors.Add(Color.FromArgb(255, 234, 72, 58));
            colorizer.Colors.Add(Color.FromArgb(255, 162, 36, 25));
            colorizer.ApproximateColors = true;
            return colorizer;
        }
        ChoroplethColorizer CreateColdColorizer() {
            ChoroplethColorizer colorizer = new ChoroplethColorizer();
            colorizer.RangeStops = new DoubleCollection() { 0.0, 0.2, 0.4, 0.6, 0.8, 1.0 };
            colorizer.Colors.Add(Color.FromArgb(50, 33, 102, 172));
            colorizer.Colors.Add(Color.FromArgb(255, 103, 169, 207));
            colorizer.Colors.Add(Color.FromArgb(255, 209, 229, 240));
            colorizer.Colors.Add(Color.FromArgb(255, 253, 219, 199));
            colorizer.Colors.Add(Color.FromArgb(255, 239, 138, 98));
            colorizer.Colors.Add(Color.FromArgb(255, 178, 24, 43));
            colorizer.ApproximateColors = true;
            return colorizer;
        }
        public void OnLegendItemCreating(LegendItemCreatingEventArgs e) {
            int endIndex = ActualPaletteItem.Colorizer != null ? ActualPaletteItem.Colorizer.Colors.Count - 1 : 3;
            if(e.Index == 0)
                e.Item.Text = "low";
            else if(e.Index == endIndex) 
                e.Item.Text = "high";
            else
                e.Item.Text = " ";
            }
    }
    public class PaletteItem {
        public string Name { get; private set; }
        public ChoroplethColorizer Colorizer { get; private set; }

        public PaletteItem(string name, ChoroplethColorizer colorizer) {
            Name = name;
            Colorizer = colorizer;
        }
    }
}
