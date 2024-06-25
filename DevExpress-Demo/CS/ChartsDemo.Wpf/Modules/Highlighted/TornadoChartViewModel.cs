using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public class TornadoChartViewModel : BindableBase {
        public static TornadoChartViewModel Create() {
            return ViewModelSource.Create(() => new TornadoChartViewModel());
        }
        Dictionary<string, int> firstKeys = new Dictionary<string, int> {
            {"Male", 0 },
            {"Female", 1 }
        };
        Dictionary<string, int> secondaryKeys = new Dictionary<string, int> {
            {"0-14 years", 0 },
            {"15-64 years", 1 },
            {"65 years and older", 2 }
        };
        List<PaneViewModel> panes = new List<PaneViewModel>() {
            PaneViewModel.Create()
        };
        List<LegendViewModel> legends;
        Bar2DModel selectedModel;
        bool isShowLabelsChecked;

        public virtual List<LegendViewModel> Legends {
            get { return legends; }
        }
        public virtual List<PaneViewModel> Panes {
            get { return panes; }
        }
        public virtual PaneViewModel Pane {
            get { return Panes[0]; }
        }
        public virtual Bar2DModel SelectedModel {
            get { return selectedModel; }
            set {
                if (selectedModel == value)
                    return;
                selectedModel = value;
                foreach (GenderAgeItem item in GenderAgeItems) {
                    item.Model = value;
                }
            }
        }
        public virtual bool IsShowLabelsChecked {
            get { return isShowLabelsChecked; }
            set {
                if (isShowLabelsChecked == value)
                    return;
                IsShowToolTipsChecked = !value;
                isShowLabelsChecked = value;
                foreach (GenderAgeItem item in GenderAgeItems) {
                    item.IsShowLabelsChecked = value;
                }
            }
        }
        public virtual bool IsShowToolTipsChecked {
            get { return !IsShowLabelsChecked; }
            set {
                if (IsShowToolTipsChecked == value)
                    return;
                foreach (GenderAgeItem item in GenderAgeItems) {
                    item.IsShowToolTipsChecked = value;
                }
            }
        }
        public virtual bool IsShowTotalLabelsChecked {
            get { return Pane.IsShowTotalLabelsChecked; }
            set { Pane.IsShowTotalLabelsChecked = value; }
        }
        public virtual DevExpress.Xpf.Charts.Palette Palette { get; set; }
        public virtual List<GenderAgeItem> GenderAgeItems { get; protected set; }

        protected TornadoChartViewModel() {
            InitLegends();
            GenderAgeItems = (List<GenderAgeItem>)new AgeStructure().GetGenderAgeItemsWithPopulation();
            foreach (GenderAgeItem genderAgeItem in GenderAgeItems) {
                SetSeriesLegend(genderAgeItem);
                genderAgeItem.Pane = Pane;
            }
            IsShowLabelsChecked = false;
            IsShowToolTipsChecked = true;
            Palette = new OfficePalette();
        }
        void InitLegends() {
            legends = new List<LegendViewModel>(){
                LegendViewModel.Create(Pane),
                LegendViewModel.Create(Pane)
            };
        }
        void SetSeriesLegend(GenderAgeItem genderAgeItem) {
            if (genderAgeItem.Name.StartsWith("Female"))
                genderAgeItem.Legend = Legends[0];
            else
                genderAgeItem.Legend = Legends[1];
        }
        void SetSeriesColor(GenderAgeItem genderAgeItem) {
            string name = genderAgeItem.Name;
            string[] keys = name.Split(':');
            if (keys.Length == 2) {
                keys[1] = keys[1].TrimStart();
                int index1, index2;
                if (firstKeys.TryGetValue(keys[0], out index1) && secondaryKeys.TryGetValue(keys[1], out index2)) {
                    genderAgeItem.Color = Palette[index1 + index2 * Palette.Count];
                }
                else
                    genderAgeItem.Color = new Color();
            }
        }
        protected void OnPaletteChanged() {
            if (Palette == null)
                return;
            foreach (GenderAgeItem genderAgeItem in GenderAgeItems) {
                Palette.ColorCycleLength = secondaryKeys.Count * Palette.Count;
                SetSeriesColor(genderAgeItem);
            }
        }
    }

    public class LegendViewModel : BindableBase {
        public static LegendViewModel Create(PaneViewModel Pane) {
            return ViewModelSource.Create(() => new LegendViewModel(Pane));
        }

        public virtual PaneViewModel Pane { get; set; }

        protected LegendViewModel(PaneViewModel Pane) {
            this.Pane = Pane;
        }
    }
    public class PaneViewModel : BindableBase {
        public static PaneViewModel Create() {
            return ViewModelSource.Create(() => new PaneViewModel());
        }

        public virtual bool IsShowTotalLabelsChecked { get; set; }

        protected PaneViewModel() {
            IsShowTotalLabelsChecked = true;
        }
    }

    public class NegativeValueConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string textValue = value.ToString();
            if (textValue[0] == '-')
                return textValue.Substring(1, textValue.Length - 1);
            return textValue;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    public class ColorToBrushConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return new SolidColorBrush((Color)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
