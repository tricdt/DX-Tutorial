using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public class DateTimeScaleViewModel {
        public static DateTimeScaleViewModel Create() {
            return ViewModelSource.Create(() => new DateTimeScaleViewModel());
        }

        DateTimeScaleSeriesViewModel PriceSeries { get { return Series[0]; } }
        DateTimeScaleSeriesViewModel VolumeSeries { get { return Series[1]; } }

        public virtual ScaleMode ScaleMode { get; set; }
        public virtual ArgumentMeasureUnit MeasureUnit { get; set; }
        public virtual DateTimeScaleSeriesViewModel SelectedSeries { get; set; }
        public virtual int MeasureUnitMultiplier { get; set; }
        public virtual AggregateFunction SelectedAggregateFunction { get; set; }
        public virtual bool ScaleOptionsEnabled { get; set; }
        public virtual bool AutoGrid { get; set; }
        public virtual bool WorkTimeOnly { get; set; }
        public virtual bool WorkTimeOptionsEnabled { get; protected set; }
        public virtual bool WorkdaysOptionsEnabled { get; protected set; }
        public virtual bool ExcludeWeekends { get; set; }
        public virtual bool ExcludeHolidays { get; set; }
        public virtual bool GridOptionsEnabled { get; protected set; }
        public virtual DateTimeGridAlignment GridAlignment { get; set; }
        public virtual int GridSpacing { get; set; }
        public virtual int GridOffset { get; set; }
        public virtual int MinorTickmarksCount { get; set; }
        public virtual int StartWorkHour { get; set; }
        public virtual int EndWorkHour { get; set; }
        public virtual string Title { get; protected set; }
        public virtual DateTime? VisualRangeMin { get; protected set; }
        public virtual DateTime? VisualRangeMax { get; protected set; }
        public DateTime MayFirst1 { get; private set; }
        public DateTime MayFirst2 { get; private set; }
        public DateTime MayFirst3 { get; private set; }
        public DateTime JanuaryFirst1 { get; private set; }
        public DateTime JanuaryFirst2 { get; private set; }
        public DateTime JanuaryFirst3 { get; private set; }
        public ObservableCollection<DateTimeGridAlignment> GridAlignments { get; private set; }
        public ObservableCollection<AggregateFunction> AggregateFunctions { get; private set; }
        public List<FinancialDataPoint> DataSource { get; private set; }
        public List<DateTimeScaleSeriesViewModel> Series { get; private set; }

        protected DateTimeScaleViewModel() {
            DataSource = new FinancialDataWithBreaksGenerator().Generate();
            Series = new List<DateTimeScaleSeriesViewModel> {
                DateTimeScaleSeriesViewModel.Create(SeriesView.Price),
                DateTimeScaleSeriesViewModel.Create(SeriesView.Volume)
            };
            AggregateFunctions = new ObservableCollection<AggregateFunction> {
                AggregateFunction.Financial
            };
            GridAlignments = new ObservableCollection<DateTimeGridAlignment>();
            SelectedSeries = PriceSeries;
            ScaleMode = ScaleMode.Manual;
            MeasureUnit = ArgumentMeasureUnit.Month;
            ScaleOptionsEnabled = true;
            MeasureUnitMultiplier = 1;
            GridSpacing = 1;
            GridOffset = 0;
            MinorTickmarksCount = 3;
            int firstYear = DataSource[0].DateTimeStamp.Year;
            JanuaryFirst1 = new DateTime(firstYear, 1, 1);
            JanuaryFirst2 = new DateTime(firstYear + 1, 1, 1);
            JanuaryFirst3 = new DateTime(firstYear + 2, 1, 1);
            MayFirst1 = new DateTime(firstYear, 5, 1);
            MayFirst2 = new DateTime(firstYear + 1, 5, 1);
            MayFirst3 = new DateTime(firstYear + 2, 5, 1);
            StartWorkHour = 8;
            EndWorkHour = 18;
            ExcludeHolidays = true;
            ExcludeWeekends = true;
        }

        void SetAggregateFunctions() {
            switch (SelectedSeries.SeriesView) {
                case SeriesView.Price:
                    PriceSeries.Visible = true;
                    VolumeSeries.Visible = false;
                    AggregateFunctions.Clear();
                    AggregateFunctions.Add(AggregateFunction.Financial);
                    SelectedAggregateFunction = AggregateFunction.Financial;
                    break;
                case SeriesView.Volume:
                    PriceSeries.Visible = false;
                    VolumeSeries.Visible = true;
                    AggregateFunctions.Clear();
                    AggregateFunctions.Add(AggregateFunction.Sum);
                    AggregateFunctions.Add(AggregateFunction.Average);
                    AggregateFunctions.Add(AggregateFunction.Minimum);
                    AggregateFunctions.Add(AggregateFunction.Maximum);
                    SelectedAggregateFunction = AggregateFunction.Sum;
                    break;
                default:
                    throw new InvalidEnumArgumentException(string.Format("The {0} enum value is unknown", SelectedSeries.SeriesView));
            }
        }
        void ChangeTitle() {
            if (SelectedSeries == PriceSeries)
                Title = "Price by " + MeasureUnit;
            else
                Title = "Sales Volume by " + MeasureUnit;
        }

        protected void OnSelectedSeriesChanged() {
            switch (SelectedSeries.SeriesView) {
                case SeriesView.Price:
                    PriceSeries.Visible = true;
                    VolumeSeries.Visible = false;
                    break;
                case SeriesView.Volume:
                    PriceSeries.Visible = false;
                    VolumeSeries.Visible = true;
                    break;
                default:
                    throw new InvalidEnumArgumentException(string.Format("The {0} enum value is unknown", SelectedSeries.SeriesView));
            }
            SetAggregateFunctions();
            ChangeTitle();
        }
        protected void OnScaleModeChanged() {
            switch (ScaleMode) {
                case ScaleMode.Continuous:
                    AggregateFunctions.Clear();
                    AggregateFunctions.Add(AggregateFunction.None);
                    SelectedAggregateFunction = AggregateFunction.None;
                    ScaleOptionsEnabled = false;
                    GridOptionsEnabled = true;
                    break;
                case ScaleMode.Automatic:
                    ScaleOptionsEnabled = false;
                    SetAggregateFunctions();
                    AutoGrid = true;
                    GridOptionsEnabled = false;
                    break;
                case ScaleMode.Manual:
                    ScaleOptionsEnabled = true;
                    SetAggregateFunctions();
                    GridOptionsEnabled = true;
                    break;
                default:
                    throw new InvalidEnumArgumentException(string.Format("The {0} enum value is unknown", ScaleMode));
            }
        }
        protected void OnMeasureUnitChanged() {
            GridAlignments.Clear();
            int measureUnitEnumValue = (int)MeasureUnit;
            for (int i = measureUnitEnumValue; i < 9; i++) {
                GridAlignments.Add((DateTimeGridAlignment)i);
            }
            int gridAlignmentEnumValue = measureUnitEnumValue + 1 > 8 ? 8 : measureUnitEnumValue + 1;
            GridAlignment = (DateTimeGridAlignment)gridAlignmentEnumValue;
            VisualRangeMax = null;
            VisualRangeMin = null;
            VisualRangeMax = DataSource.Last().DateTimeStamp;
            switch (MeasureUnit) {
                case ArgumentMeasureUnit.Hour:
                    WorkdaysOptionsEnabled = true;
                    WorkTimeOptionsEnabled = true;
                    break;
                case ArgumentMeasureUnit.Day:
                    WorkdaysOptionsEnabled = true;
                    WorkTimeOptionsEnabled = false;
                    break;
                case ArgumentMeasureUnit.Week:
                case ArgumentMeasureUnit.Month:
                case ArgumentMeasureUnit.Quarter:
                case ArgumentMeasureUnit.Year:
                    WorkdaysOptionsEnabled = false;
                    WorkTimeOptionsEnabled = false;
                    break;
            }
            if (!ScaleMode.Equals(ScaleMode.Automatic)) {
                switch (MeasureUnit) {
                    case ArgumentMeasureUnit.Hour:
                        VisualRangeMin = VisualRangeMax.Value.AddDays(-50);
                        break;
                    case ArgumentMeasureUnit.Day:
                        VisualRangeMin = VisualRangeMax.Value.AddDays(-50);
                        break;
                    case ArgumentMeasureUnit.Week:
                        VisualRangeMin = VisualRangeMax.Value.AddDays(-50 * 7);
                        break;
                    case ArgumentMeasureUnit.Month:
                        VisualRangeMin = VisualRangeMax.Value.AddMonths(-50);
                        break;
                    case ArgumentMeasureUnit.Quarter:
                    case ArgumentMeasureUnit.Year:
                        VisualRangeMin = VisualRangeMax.Value.AddYears(-3);
                        break;
                }
            }
        }

        public void OnScaleChanged(AxisScaleChangedEventArgs e) {
            if (!(e.Axis is AxisX2D))
                return;
            DateTimeScaleChangedEventArgs args = e as DateTimeScaleChangedEventArgs;
            if (args == null)
                return;
            if (args.MeasureUnitChange.IsChanged)
                MeasureUnit = (ArgumentMeasureUnit)args.MeasureUnitChange.NewValue;
            if (args.GridAlignmentChange.IsChanged)
                GridAlignment = args.GridAlignmentChange.NewValue;
            if (args.GridOffsetChange.IsChanged)
                GridOffset = (int)args.GridOffsetChange.NewValue;
            if (args.GridSpacingChange.IsChanged)
                GridSpacing = (int)args.GridSpacingChange.NewValue;
            ChangeTitle();
        }
    }


    public class DateTimeScaleSeriesViewModel {
        public static DateTimeScaleSeriesViewModel Create(SeriesView view) {
            return ViewModelSource.Create(() => new DateTimeScaleSeriesViewModel(view));
        }

        readonly SeriesView view;

        public SeriesView SeriesView {
            get { return this.view; }
        }
        public virtual bool Visible {
            get;
            set;
        }

        protected DateTimeScaleSeriesViewModel(SeriesView view) {
            this.view = view;
        }

        public override string ToString() {
            return this.view.ToString();
        }
    }

    public enum SeriesView {
        Price,
        Volume
    }

    public enum ArgumentMeasureUnit {
        Millisecond = 0,
        Hour = 3,
        Day = 4,
        Week = 5,
        Month = 6,
        Quarter = 7,
        Year = 8
    }

}
