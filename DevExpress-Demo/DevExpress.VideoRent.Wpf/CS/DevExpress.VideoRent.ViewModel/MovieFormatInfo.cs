using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using DevExpress.VideoRent.Resources;

namespace DevExpress.VideoRent.ViewModel {
    public class MovieFormatInfo : INotifyPropertyChanged {
        public string FormatText { get { return EnumTitlesKeeper<MovieItemFormat>.GetTitle(Format); } set { } }
        public string DefaultRentalDays { get; set; }
        public string DefaultLateFee { get; set; }
        public string TotalCount { get; set; }
        public string DetailCounts { get; set; }
        public bool DetailControlsVisible { get; set; }
        public ImageSource FormatIcon { get; set; }
        public Dictionary<string, int> FormatDetailsDictionary { get; set; }
        MovieItem.CountInfo CountInfo { get; set; }
        MovieCategoryPrice CategoryPrice { get; set; }
        MovieItemFormat? Format { get; set; }

        public MovieFormatInfo(MovieItemFormat? format, MovieItem.CountInfo countInfo, MovieCategoryPrice categoryPrice, ImageSource formatIcon) {
            CategoryPrice = categoryPrice;
            CountInfo = countInfo;
            Format = format;
            FormatIcon = formatIcon;
            UpdateFields();
        }
        void UpdateFields() {
            if(CategoryPrice == null) {
                DetailControlsVisible = false;
            } else {
                DetailControlsVisible = true; ;
                DefaultRentalDays = CategoryPrice.DefaultRentDays.ToString();
                DefaultLateFee = CategoryPrice.DefaultPrice.ToString();
            }
            if(Format == null)
                TotalCount = string.Format(ConstStrings.Get("ItemsCountCaptionFlowDocument"), CountInfo.Total);
            else {
                int formatIndex = CountInfo.FormatIndex(Format.Value);
                TotalCount = string.Format(ConstStrings.Get("ItemsCountCaptionFlowDocument"), CountInfo.WithFormat[formatIndex]);
            }
            DetailCounts = GetTextDetailsForFormat();
        }
        string GetTextDetailsForFormat() {
            GenerateDetailsDictionary();
            string ret = string.Format(ConstStrings.Get("ItemsCountDetailCaptionFlowDocument"), FormatDetailsDictionary[MovieItemStatus.Rented.ToString()], FormatDetailsDictionary[MovieItemStatus.Active.ToString()], FormatDetailsDictionary["For Sell"],
                FormatDetailsDictionary[MovieItemStatus.Lost.ToString()], FormatDetailsDictionary[MovieItemStatus.Damaged.ToString()], FormatDetailsDictionary[MovieItemStatus.Sold.ToString()]);
            return ret;
        }
        void GenerateDetailsDictionary() {
            int formatIndex = Format == null ? -1 : CountInfo.FormatIndex(Format.Value);
            FormatDetailsDictionary = new Dictionary<string, int>();
            FormatDetailsDictionary.Add(MovieItemStatus.Rented.ToString(), Format == null ? CountInfo.WithStatus[CountInfo.StatusIndex(MovieItemStatus.Rented)] : CountInfo.WithFormatAndStatus[formatIndex, CountInfo.StatusIndex(MovieItemStatus.Rented)]);
            FormatDetailsDictionary.Add(MovieItemStatus.Active.ToString(), Format == null ? CountInfo.WithStatus[CountInfo.StatusIndex(MovieItemStatus.Active)] : CountInfo.WithFormatAndStatus[formatIndex, CountInfo.StatusIndex(MovieItemStatus.Active)]);
            FormatDetailsDictionary.Add("For Sell", Format == null ? CountInfo.ForSell : CountInfo.WithFormatForSell[formatIndex]);
            FormatDetailsDictionary.Add(MovieItemStatus.Lost.ToString(), Format == null ? CountInfo.WithStatus[CountInfo.StatusIndex(MovieItemStatus.Lost)] : CountInfo.WithFormatAndStatus[formatIndex, CountInfo.StatusIndex(MovieItemStatus.Lost)]);
            FormatDetailsDictionary.Add(MovieItemStatus.Damaged.ToString(), Format == null ? CountInfo.WithStatus[CountInfo.StatusIndex(MovieItemStatus.Damaged)] : CountInfo.WithFormatAndStatus[formatIndex, CountInfo.StatusIndex(MovieItemStatus.Damaged)]);
            FormatDetailsDictionary.Add(MovieItemStatus.Sold.ToString(), Format == null ? CountInfo.WithStatus[CountInfo.StatusIndex(MovieItemStatus.Sold)] : CountInfo.WithFormatAndStatus[formatIndex, CountInfo.StatusIndex(MovieItemStatus.Sold)]);
        }
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string name) {
            if(PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
