using DevExpress.Xpf.Charts;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using DevExpress.SalesDemo.Model;

namespace ProductsDemo.Modules {
	public class PivotGridViewModel : ViewModelBase {
		DateTime rangeStart, rangeEnd, selectedRangeStart, selectedRangeEnd;
		IEnumerable<SalesGroup> filteredSource, dataSource, filteredSourceByDate;
		IEnumerable<TileData> tilesData;
        IDataProvider dataProvider = DevExpress.SalesDemo.Model.DataSource.GetDataProvider();

		public PivotGridViewModel() {
			DateTimeRange range = DateTimeUtils.GetOneYearRange();
			SelectedRangeStart = RangeStart = range.Start;
			SelectedRangeEnd = RangeEnd = range.End;
			DataSource = dataProvider.GetSales(RangeStart, RangeEnd, GroupingPeriod.Day);
			UpdateTiles();
			OnSelectedRangeChanged();
		}

		private void UpdateTiles() {
			List<TileData> tiles = new List<TileData>();
			DateTimeRange ytd = DateTimeUtils.GetYtdRange();
			DateTimeRange ytdPrev = new DateTimeRange(ytd.Start.AddYears(-1), ytd.End.AddYears(-1));
			SalesGroup ytdSales = dataProvider.GetTotalSalesByRange(ytd.Start, ytd.End);
			SalesGroup ytdSalesPrev = dataProvider.GetTotalSalesByRange(ytdPrev.Start, ytdPrev.End);
			var percentsString = "N/A";
			if(ytdSalesPrev.TotalCost != 0) {
				decimal percents = (ytdSales.TotalCost - ytdSalesPrev.TotalCost) / ytdSalesPrev.TotalCost;
				percentsString = string.Format("{1}{0:P1}", Math.Abs(percents), percents < 0 ? "-" : "+");
			}
			tiles.Add(new TileData("Revenues", "YTD GROWTH", percentsString));
			tiles.Add(new TileData("Unit Sales", "YTD", ytdSales.Units.ToString("n0")));
			tiles.Add(new TileData("Direct Sales", "YTD", ytdSales.TotalCost.ToString("$#,##0,,M", CultureInfo.InvariantCulture)));
			SalesGroup sector = dataProvider.GetSalesBySector(ytd.Start, ytd.End, GroupingPeriod.All).OrderByDescending(q => q.TotalCost).FirstOrDefault();
			if(sector != null)
				tiles.Add(new TileData("Best Sector YTD", sector.GroupName.ToUpper(), sector.TotalCost.ToString("$#,##0,,M", CultureInfo.InvariantCulture)));
			TilesData = tiles;
		}

		public DateTime RangeStart {
			get { return rangeStart; }
			private set { SetProperty(ref rangeStart, value, () => RangeStart); }
		}

		public DateTime RangeEnd {
			get { return rangeEnd; }
			private set { SetProperty(ref rangeEnd, value, () => RangeEnd); }
		}
		
		public DateTime SelectedRangeStart {
			get { return selectedRangeStart; }
		    set { SetProperty(ref selectedRangeStart, value, () => SelectedRangeStart, OnSelectedRangeChanged); }
		}

		public DateTime SelectedRangeEnd {
			get { return selectedRangeEnd; }
		    set { SetProperty(ref selectedRangeEnd, value, () => SelectedRangeEnd, OnSelectedRangeChanged); }
		}

	    public IEnumerable<SalesGroup> DataSource {
			get { return dataSource; }
			private set { SetProperty(ref dataSource, value, "DataSource"); }
		}

		public IEnumerable<SalesGroup> FilteredSource {
			get { return filteredSource; }
			private set { SetProperty(ref filteredSource, value, "FilteredSource"); }
		}

    	public IEnumerable<SalesGroup> FilteredSourceByDate {
			get { return filteredSourceByDate; }
			private set { SetProperty(ref filteredSourceByDate, value, "FilteredSourceByDate"); }
		}

		public IEnumerable<TileData> TilesData {
			get { return tilesData; }
			private set { SetProperty(ref tilesData, value, "TilesData"); }
		}

		void OnSelectedRangeChanged() {
			if(SelectedRangeEnd < SelectedRangeStart)
				return;
			FilteredSourceByDate = dataProvider.GetSalesByProduct(SelectedRangeStart, SelectedRangeEnd, GroupingPeriod.Day);
			FilteredSource = dataProvider.GetSalesByProduct(SelectedRangeStart, SelectedRangeEnd, GroupingPeriod.All);
		}
	}

	public class TileData : ViewModelBase {
		string svalue, subText, mainText;

		public TileData(string mainText, string subText, string value) {
			this.mainText = mainText;
			this.subText = subText;
			this.svalue = value;
		}

		public string MainText {
			get { return mainText; }
			private set { SetProperty(ref mainText, value, "MainText"); }
		}


		public string SubText {
			get { return subText; }
			private set { SetProperty(ref subText, value, "SubText"); }
		}

		public string Value {
			get { return svalue; }
			private set { SetProperty(ref svalue, value, "Value"); }
		}
	}
}
