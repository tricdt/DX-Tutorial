using System;
using System.Collections.Generic;

namespace ChartsDemo {

    public class WebSitePerformanceIndicatorItem {
        public DateTime ReportDate { get; set; }
        public int TrafficTime { get; set; } 
        public int ResponseTime { get; set; } 
        public double AveragePageLoadTime { get; set; } 
        public int MemoryUsage { get; set; } 
        public int CPUUsage { get; set; } 
        public int ClientErrors { get; set; }
        public int ServerErrors { get; set; }
        public int NewVisitors { get; set; }
        public int ReturnVisitors { get; set; }
    }


    public class WebSitePerformanceIndicator {
        List<WebSitePerformanceIndicatorItem> data;
        public List<WebSitePerformanceIndicatorItem> Data {
            get {
                if (data == null)
                    data = GetData();
                return data;
            }
        }
        List<WebSitePerformanceIndicatorItem> GetData() {
            List<WebSitePerformanceIndicatorItem> data = new List<WebSitePerformanceIndicatorItem>();
            DateTime lastDate = DateTime.Now.AddDays(-1);
            Random random = new Random(1);
            for (int i = 0; i < 30; i++) {
                int newVisitors = random.Next(18, 77);
                data.Add(new WebSitePerformanceIndicatorItem() {
                    ReportDate = lastDate.AddDays(-i),
                    TrafficTime = random.Next(3, 12),
                    ResponseTime = random.Next(40, 110),
                    AveragePageLoadTime = random.NextDouble() * 3 + 0.5,
                    MemoryUsage = random.Next(500, 2000),
                    CPUUsage = random.Next(10, 77),
                    ClientErrors = random.Next(2, 45),
                    ServerErrors = random.Next(2, 7),
                    NewVisitors = newVisitors,
                    ReturnVisitors = random.Next(10, newVisitors)
                });
            }
            return data;
        }
    }

}
