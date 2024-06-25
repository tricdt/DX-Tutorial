using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DevExpress.Common;
using DevExpress.DataAnnotations;

namespace DevExpress.DevAV {
    
    public partial class Address : IDataErrorInfo {
        [Display(Name = "Address")]
        public string Line { get; set; }
        public string City { get; set; }
        
        public StateEnum State { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string CityLine {
            get { return GetCityLine(City, State, ZipCode); }
        }
        [ZipCode, Display(Name = "Zip code")]
        public string ZipCode { get; set; }

        public override string ToString() {
            return string.Format("{0}, {1}", Line, CityLine);
        }
        #region IDataErrorInfo
        string IDataErrorInfo.Error { get { return null; } }
        string IDataErrorInfo.this[string columnName] {
            get { return IDataErrorInfoHelper.GetErrorText(this, columnName); }
        }
        #endregion

        internal static string GetCityLine(string city, StateEnum state, string zipCode) {
            return string.Format("{0}, {1} {2}", city, state, zipCode);
        }
    }
}
