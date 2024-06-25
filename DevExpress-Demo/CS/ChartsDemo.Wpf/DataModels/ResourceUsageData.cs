using System;
using System.Collections.Generic;

namespace ChartsDemo {

    class ResourceUsageData : List<ResourceUsageDataItem> {
        public ResourceUsageData() {
            Add(new ResourceUsageDataItem(1, 0.21, 0.22, 0.21, 5, 5, 5));
            Add(new ResourceUsageDataItem(2, 0.31, 0.11, 0.02, 7, 7, 20));
            Add(new ResourceUsageDataItem(3, 0.11, 0.21, 0.35, 2, 12, 18));
            Add(new ResourceUsageDataItem(4, 0.13, 0.25, 0.29, 7, 25, 21));
            Add(new ResourceUsageDataItem(5, 0.02, 0.10, 0.15, 25, 25, 19));
            Add(new ResourceUsageDataItem(6, 0.05, 0.11, 0.21, 27, 20, 10));
            Add(new ResourceUsageDataItem(7, 0.11, 0.15, 0.23, 44, 17, 8));
            Add(new ResourceUsageDataItem(8, 0.15, 0.20, 0.30, 45, 24, 15));
            Add(new ResourceUsageDataItem(9, 0.18, 0.25, 0.36, 50, 29, 17));
            Add(new ResourceUsageDataItem(10, 0.23, 0.12, 0.38, 52, 25, 12));
            Add(new ResourceUsageDataItem(11, 0.21, 0.08, 0.36, 52, 28, 40));
            Add(new ResourceUsageDataItem(12, 0.16, 0.08, 0.37, 55, 29, 47));
            Add(new ResourceUsageDataItem(13, 0.22, 0.27, 0.33, 53, 25, 50));
            Add(new ResourceUsageDataItem(14, 0.25, 0.29, 0.22, 51, 28, 45));
            Add(new ResourceUsageDataItem(15, 0.22, 0.31, 0.19, 49, 30, 50));
            Add(new ResourceUsageDataItem(16, 0.23, 0.34, 0.15, 45, 42, 51));
            Add(new ResourceUsageDataItem(17, 0.25, 0.40, 0.03, 46, 45, 48));
            Add(new ResourceUsageDataItem(18, 0.32, 0.54, 0.04, 42, 40, 43));
            Add(new ResourceUsageDataItem(19, 0.30, 0.51, 0.03, 45, 20, 15));
            Add(new ResourceUsageDataItem(20, 0.31, 0.45, 0.07, 48, 21, 19));
            Add(new ResourceUsageDataItem(21, 0.25, 0.40, 0.05, 48, 35, 25));
            Add(new ResourceUsageDataItem(22, 0.10, 0.43, 0.07, 49, 33, 27));
            Add(new ResourceUsageDataItem(23, 0.05, 0.45, 0.15, 49, 35, 30));
            Add(new ResourceUsageDataItem(24, 0.03, 0.44, 0.21, 51, 37, 32));
            Add(new ResourceUsageDataItem(25, 0.01, 0.42, 0.23, 55, 40, 37));
            Add(new ResourceUsageDataItem(26, 0.01, 0.45, 0.21, 57, 43, 39));
            Add(new ResourceUsageDataItem(27, 0.01, 0.43, 0.22, 59, 50, 43));
            Add(new ResourceUsageDataItem(28, 0.01, 0.39, 0.25, 62, 51, 42));
            Add(new ResourceUsageDataItem(29, 0.03, 0.27, 0.20, 42, 31, 23));
            Add(new ResourceUsageDataItem(30, 0.07, 0.25, 0.14, 25, 20, 17));
            Add(new ResourceUsageDataItem(31, 0.05, 0.12, 0.09, 35, 25, 20));
            Add(new ResourceUsageDataItem(32, 0.03, 0.10, 0.05, 41, 29, 24));
            Add(new ResourceUsageDataItem(33, 0.05, 0.08, 0.06, 48, 32, 26));
            Add(new ResourceUsageDataItem(34, 0.02, 0.09, 0.06, 55, 37, 28));
            Add(new ResourceUsageDataItem(35, 0.05, 0.11, 0.07, 59, 38, 28));
            Add(new ResourceUsageDataItem(36, 0.03, 0.13, 0.05, 63, 39, 30));
            Add(new ResourceUsageDataItem(37, 0.02, 0.15, 0.03, 67, 43, 31));
            Add(new ResourceUsageDataItem(38, 0.05, 0.12, 0.07, 71, 50, 32));
            Add(new ResourceUsageDataItem(39, 0.07, 0.16, 0.12, 65, 43, 31));
            Add(new ResourceUsageDataItem(40, 0.09, 0.25, 0.18, 61, 39, 30));
            Add(new ResourceUsageDataItem(41, 0.09, 0.23, 0.19, 60, 38, 30));
            Add(new ResourceUsageDataItem(42, 0.10, 0.25, 0.20, 63, 37, 31));
            Add(new ResourceUsageDataItem(43, 0.11, 0.22, 0.18, 64, 35, 32));
            Add(new ResourceUsageDataItem(44, 0.13, 0.31, 0.19, 60, 36, 30));
            Add(new ResourceUsageDataItem(45, 0.17, 0.33, 0.22, 58, 35, 31));
            Add(new ResourceUsageDataItem(46, 0.23, 0.30, 0.27, 63, 32, 33));
            Add(new ResourceUsageDataItem(47, 0.20, 0.25, 0.30, 58, 29, 31));
            Add(new ResourceUsageDataItem(48, 0.17, 0.23, 0.35, 62, 28, 32));
            Add(new ResourceUsageDataItem(49, 0.15, 0.25, 0.37, 60, 26, 30));
            Add(new ResourceUsageDataItem(50, 0.12, 0.22, 0.40, 55, 23, 27));
            Add(new ResourceUsageDataItem(51, 0.11, 0.20, 0.42, 57, 21, 31));
            Add(new ResourceUsageDataItem(52, 0.09, 0.18, 0.45, 60, 20, 35));
            Add(new ResourceUsageDataItem(53, 0.08, 0.17, 0.46, 65, 19, 45));
            Add(new ResourceUsageDataItem(54, 0.05, 0.10, 0.52, 77, 17, 43));
            Add(new ResourceUsageDataItem(55, 0.03, 0.12, 0.55, 81, 18, 40));
            Add(new ResourceUsageDataItem(56, 0.05, 0.09, 0.53, 75, 17, 15));
            Add(new ResourceUsageDataItem(57, 0.07, 0.12, 0.47, 67, 18, 16));
            Add(new ResourceUsageDataItem(58, 0.03, 0.09, 0.35, 60, 19, 12));
            Add(new ResourceUsageDataItem(59, 0.05, 0.12, 0.23, 41, 10, 5));
            Add(new ResourceUsageDataItem(60, 0.03, 0.07, 0.10, 33, 5, 3));
        }
    }


    class ResourceUsageDataItem {
        readonly TimeSpan second;
        readonly double process1CpuUsage;
        readonly double process2CpuUsage;
        readonly double process3CpuUsage;
        readonly double process1Memory;
        readonly double process2Memory;
        readonly double process3Memory;

        public TimeSpan Second { get { return this.second; } }
        public double Process1CpuUsage { get { return this.process1CpuUsage; } }
        public double Process2CpuUsage { get { return this.process2CpuUsage; } }
        public double Process3CpuUsage { get { return this.process3CpuUsage; } }
        public double Process1Memory { get { return this.process1Memory; } }
        public double Process2Memory { get { return this.process2Memory; } }
        public double Process3Memory { get { return this.process3Memory; } }

        public ResourceUsageDataItem(int second, double process1CpuUsage, double process2CpuUsage, double process3CpuUsage,
                                               double process1Memory, double process2Memory, double process3Memory) {
            this.second = TimeSpan.FromSeconds(second);
            this.process1CpuUsage = process1CpuUsage;
            this.process2CpuUsage = process2CpuUsage;
            this.process3CpuUsage = process3CpuUsage;
            this.process1Memory = process1Memory;
            this.process2Memory = process2Memory;
            this.process3Memory = process3Memory;
        }
    }

}
