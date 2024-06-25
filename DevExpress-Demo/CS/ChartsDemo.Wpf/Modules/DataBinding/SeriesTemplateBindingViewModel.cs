using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {

    public class SeriesTemplateBindingViewModel {
        public static SeriesTemplateBindingViewModel Create() {
            return ViewModelSource.Create(() => new SeriesTemplateBindingViewModel());
        }

        public IChartAnimationService AnimationService { get { return this.GetService<IChartAnimationService>(); } }
        public virtual ObservableCollection<string> SeriesDataMembers { get; protected set; }
        public virtual ObservableCollection<string> ArgumentDataMembers { get; protected set; }
        public virtual ObservableCollection<string> ValueDataMembers { get; protected set; }
        public virtual string SelectedSeriesDataMember { get; set; }
        public virtual string SelectedArgumentDataMember { get; set; }
        public virtual string SelectedValueDataMember { get; set; }
        public virtual SummaryFunction SelectedSummaryFunction { get; set; }
        public virtual string AxisYTitle { get; protected set; }
        public virtual string ChartTitle { get; protected set; }
        public virtual SummaryFunctionBase ActualSummaryFunction { get; protected set; }
        public List<DevAVSaleItem> DevAVSalesData {
            get { return new DevAVSales().GetProductsByMonths(); }
        }

        protected SeriesTemplateBindingViewModel() {
            SeriesDataMembers = new ObservableCollection<string> {
                "Company",
                "Product",
                "Month"
            };
            SelectedSeriesDataMember = "Company";
            ArgumentDataMembers = new ObservableCollection<string> {
                "Product",
                "Month"
            };
            SelectedArgumentDataMember = "Product";
            ValueDataMembers = new ObservableCollection<string> {
                "Income",
                "Revenue"
            };
            SelectedValueDataMember = "Revenue";
            SelectedSummaryFunction = SummaryFunction.Sum;
        }

        protected void OnSelectedSeriesDataMemberChanged(string oldValue) {
            if (ArgumentDataMembers == null)
                return;
            ArgumentDataMembers.Remove(SelectedSeriesDataMember);
            ArgumentDataMembers.Add(oldValue);
            if (AnimationService != null)
                AnimationService.Animate();
        }
        protected void OnSelectedArgumentDataMemberChanged(string oldValue) {
            if (SeriesDataMembers == null)
                return;
            SeriesDataMembers.Remove(SelectedArgumentDataMember);
            SeriesDataMembers.Add(oldValue);
            if (AnimationService != null)
                AnimationService.Animate();
        }
        protected void OnSelectedSummaryFunctionChanged() {
            UpdateSummaryFunction();
        }
        void UpdateSummaryFunction() {
            switch (SelectedSummaryFunction) {
                case SummaryFunction.Average:
                    ChartTitle = "Average Order Amount";
                    AxisYTitle = "Amount (USD)";
                    ActualSummaryFunction = new AverageSummaryFunction() { ValueDataMember = SelectedValueDataMember };
                    break;
                case SummaryFunction.Custom_StdDev:
                    ChartTitle = "Standard Deviation from Average Order Amount";
                    AxisYTitle = "Deviation (USD)";
                    ActualSummaryFunction = new StdDevSummaryFunction() { ValueDataMember = SelectedValueDataMember };
                    break;
                case SummaryFunction.Maximum:
                    ChartTitle = "Maximal Order Amount";
                    AxisYTitle = "Amount (USD)";
                    ActualSummaryFunction = new MaxSummaryFunction() { ValueDataMember = SelectedValueDataMember };
                    break;
                case SummaryFunction.Minimum:
                    ChartTitle = "Minimal Order Amount";
                    AxisYTitle = "Amount (USD)";
                    ActualSummaryFunction = new MinSummaryFunction() { ValueDataMember = SelectedValueDataMember };
                    break;
                case SummaryFunction.Sum:
                    ChartTitle = "Sales Volume";
                    AxisYTitle = "Volume (USD)";
                    ActualSummaryFunction = new SumSummaryFunction() { ValueDataMember = SelectedValueDataMember };
                    break;
                default:
                    throw new NotSupportedException("Only the Average, Custom, Max, Min and Sum Summary Functions are supported.");
            }
            if (AnimationService != null)
                AnimationService.Animate();
        }

        protected void OnSelectedValueDataMemberChanged() {
            UpdateSummaryFunction();
        }
    }

    public enum SummaryFunction {
        Average,
        Maximum,
        Minimum,
        Sum,
        Custom_StdDev
    }

    public class StdDevSummaryFunction : DataMemberSummaryFunction {
        public override string Name { get { return "STDDEV"; } }

        protected override SeriesPoint[] CreateSeriesPoints(Series series, object argument, string[] functionArguments, DataSourceValues[] values, object[] colors) {
            double[] amount = new double[values.Length];
            double sum = 0.0;
            for (int i = 0; i < values.Length; i++) {
                amount[i] = Convert.ToDouble(values[i][functionArguments[0]]);
                sum += amount[i];
            }
            double averageAmount = sum / values.Length;
            double standardDeviationSquareSum = 0.0;
            for (int i = 0; i < values.Length; i++) {
                double deviation = amount[i] - averageAmount;
                standardDeviationSquareSum += deviation * deviation;
            }
            SeriesPoint seriesPoint = new SeriesPoint() { Argument = argument.ToString(), Value = Math.Round(Math.Sqrt(standardDeviationSquareSum / values.Length), 2) };
            return new SeriesPoint[] { seriesPoint };
        }
    }

    public class BindingProxy : Freezable {
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));

        public object Data {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        protected override Freezable CreateInstanceCore() {
            return new BindingProxy();
        }
    }
}
