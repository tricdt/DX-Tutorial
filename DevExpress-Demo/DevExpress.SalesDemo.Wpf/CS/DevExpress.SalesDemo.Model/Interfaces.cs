using System;
using System.Collections.Generic;

namespace DevExpress.SalesDemo.Model {
    public interface IDataProvider {
        SalesGroup GetTotalSalesByRange(DateTime start, DateTime end);
        IEnumerable<SalesGroup> GetSales(DateTime start, DateTime end, GroupingPeriod period);
        IEnumerable<SalesGroup> GetSalesByChannel(DateTime start, DateTime end, GroupingPeriod period);
        IEnumerable<SalesGroup> GetSalesByProduct(DateTime start, DateTime end, GroupingPeriod period);
        IEnumerable<SalesGroup> GetSalesByRegion(DateTime start, DateTime end, GroupingPeriod period);
        IEnumerable<SalesGroup> GetSalesBySector(DateTime start, DateTime end, GroupingPeriod period);
    }
    public static class DataProviderExtensions {
        public static SalesGroup GetTotalSalesByRange(this IDataProvider dataProvider, DateTimeRange range) {
            Verify(dataProvider);
            return dataProvider.GetTotalSalesByRange(range.Start, range.End);
        }
        public static IEnumerable<SalesGroup> GetSales(this IDataProvider dataProvider, DateTimeRange range, GroupingPeriod period) {
            Verify(dataProvider);
            return dataProvider.GetSales(range.Start, range.End, period);
        }
        public static IEnumerable<SalesGroup> GetSalesByChannel(this IDataProvider dataProvider, DateTimeRange range, GroupingPeriod period) {
            Verify(dataProvider);
            return dataProvider.GetSalesByChannel(range.Start, range.End, period);
        }
        public static IEnumerable<SalesGroup> GetSalesByProduct(this IDataProvider dataProvider, DateTimeRange range, GroupingPeriod period) {
            Verify(dataProvider);
            return dataProvider.GetSalesByProduct(range.Start, range.End, period);
        }
        public static IEnumerable<SalesGroup> GetSalesByRegion(this IDataProvider dataProvider, DateTimeRange range, GroupingPeriod period) {
            Verify(dataProvider);
            return dataProvider.GetSalesByRegion(range.Start, range.End, period);
        }
        public static IEnumerable<SalesGroup> GetSalesBySector(this IDataProvider dataProvider, DateTimeRange range, GroupingPeriod period) {
            Verify(dataProvider);
            return dataProvider.GetSalesBySector(range.Start, range.End, period);
        }

        static void Verify(IDataProvider dataProvider) {
            if(dataProvider == null)
                throw new ArgumentNullException();
        }
    }
    //
    public interface IDataGenerator {
        event ProgressEventHandler GenerationStart;
        event ProgressEventHandler GenerationProgress;
        event ProgressEventHandler GenerationComplete;
    }
    public delegate void ProgressEventHandler(
            object sender, ProgressEventArgs e
        );
    public class ProgressEventArgs {
        public ProgressEventArgs(int progress) {
            Progress = progress;
        }
        public int Progress { get; private set; }
    }
}
