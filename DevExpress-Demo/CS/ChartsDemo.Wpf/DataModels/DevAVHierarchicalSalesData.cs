using System;
using System.Collections.Generic;

namespace ChartsDemo {

    public abstract class DevAVSalesModelBase {
        public string Name {
            get;
            protected set;
        }
        public double TotalIncome {
            get;
            protected set;
        }

        public DevAVSalesModelBase(string name) {
            Name = name;
        }

        public override string ToString() {
            return Name;
        }
    }


    public sealed class DevAVBranch : DevAVSalesModelBase {
        public List<DevAVProductCategory> Categories {
            get;
            private set;
        }

        public DevAVBranch(string name, List<DevAVProductCategory> categories)
            : base(name) {
            Categories = categories;
            double totalIncome = 0d;
            foreach (DevAVProductCategory category in categories)
                totalIncome += category.TotalIncome;
            TotalIncome = totalIncome;
        }
    }


    public sealed class DevAVProductCategory : DevAVSalesModelBase {
        public List<DevAVProduct> Products {
            get;
            private set;
        }

        public DevAVProductCategory(string name, List<DevAVProduct> products)
            : base(name) {
            Products = products;
            double totalIncome = 0d;
            foreach (DevAVProduct product in products)
                totalIncome += product.TotalIncome;
            TotalIncome = totalIncome;
        }
    }


    public sealed class DevAVProduct : DevAVSalesModelBase {
        public List<DevAVMonthlyIncome> Sales {
            get;
            private set;
        }

        public DevAVProduct(string name, List<DevAVMonthlyIncome> sales)
            : base(name) {
            Sales = sales;
            double totalIncome = 0d;
            foreach (DevAVMonthlyIncome monthlyIncome in sales)
                totalIncome += monthlyIncome.Income;
            TotalIncome = totalIncome;
        }
    }


    public sealed class DevAVMonthlyIncome {
        public DateTime Month { get; private set; }
        public double Income { get; private set; }

        public DevAVMonthlyIncome(DateTime month, double income) {
            Month = month;
            Income = income;
        }
    }

}
