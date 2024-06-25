using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace ChartsDemo {
    public class DraggableItem : INotifyPropertyChanged {
        double income;
        double cost;
        double supply;
        double demand;
        double stock;
        bool isHighlighted;

        public double Income {
            get { return income; }
            set {
                income = value;
                NotifyPropertyChanged("Income");
            }
        }
        public double Cost {
            get { return cost; }
            set {
                cost = value;
                NotifyPropertyChanged("Cost");
            }
        }
        public double Production {
            get { return supply; }
            set {
                supply = value;
                NotifyPropertyChanged("Production");
            }
        }
        public double Demand {
            get { return demand; }
            set {
                demand = value;
                NotifyPropertyChanged("Demand");
            }
        }
        public double Stock {
            get { return stock; }
            set {
                stock = value;
                NotifyPropertyChanged("Stock");
            }
        }
        public string Month { get; set; }
        public bool IsHighlighted {
            get {
                return isHighlighted;
            }
            set {
                isHighlighted = value;
                NotifyPropertyChanged("IsHighlighted");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DraggableItem(double income, double cost, double production, double demand, double stock, string month) {
            this.Income = income;
            this.Cost = cost;
            this.Production = production;
            this.Demand = demand;
            this.Stock = stock;
            this.Month = month;
            this.isHighlighted = false;
        }
        void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void UpdateCost(double newValue) {
            Cost = newValue;
        }
        public bool UpdateProduction(double newValue) {
            if (newValue < 0)
                return false;
            Production = newValue;
            return true;
        }
        public void UpdateDemand(double newValue) {
            Demand = newValue;
        }
    }
    public class DraggableDataModel : BindingList<DraggableItem> {
        public static DraggableDataModel CreateModel(int itemProductionCost = 50) {
            Random rnd = new Random();
            DateTime now = DateTime.Now;
            DateTime startDate = new DateTime(now.Year, now.Month, 1);
            DraggableDataModel data = new DraggableDataModel(itemProductionCost);

            data.Add(new DraggableItem(0, 120, 2000, 1000, 0, startDate.ToString("MMMM", CultureInfo.InvariantCulture)));
            startDate = startDate.AddMonths(1);
            for (int i = 1; i < 12; i++) {
                string month = startDate.ToString("MMMM", CultureInfo.InvariantCulture);
                double cost;
                double demand;
                if (i < 4 || i > 7) {
                    cost = data[i - 1].Cost + rnd.Next(3, 6);
                    if (i < 4)
                        demand = data[i - 1].Demand + (rnd.NextDouble() * (5 - 3) + 3) * 1000;
                    else
                        demand = data[i - 1].Demand + (rnd.NextDouble() * (8 - 6) + 6) * 1000;
                }
                else {
                    cost = data[i - 1].Cost + rnd.Next(10, 20);
                    demand = data[i - 1].Demand + (rnd.NextDouble() * (13 - 8) + 8) * 1000;
                }
                double production = data[i - 1].Production + (rnd.NextDouble() * (8 - 5) + 5) * 1000;

                data.Add(new DraggableItem(0, cost, production, demand, 0, month));
                startDate = startDate.AddMonths(1);
            }
            data.InitStock();
            data.CalcIncome(0);
            return data;
        }

        public readonly double ItemProductionCost;
        public double TotalIncome { get { return this.Select(x => x.Income).Sum(); } }
        public DraggableDataModel(double unitProductionCost) {
            this.ItemProductionCost = unitProductionCost;
        }

        void CalcFutureStock(int startingIndex, double reminderDiff) {
            for (int i = startingIndex; i < Count; i++) {
                double oldStock = this[i].Stock;
                this[i].Stock = Math.Max(this[i].Production, this[i].Stock + reminderDiff);
                if (oldStock < this[i].Demand && this[i].Stock < this[i].Demand)
                    break;
                if (oldStock < this[i].Demand && this[i].Stock > this[i].Demand)
                    reminderDiff = this[i].Stock - this[i].Demand;
                else if (oldStock > this[i].Demand && this[i].Stock < this[i].Demand)
                    reminderDiff = this[i].Demand - oldStock;
            }
        }
        double CalcStockDiff(double oldDemand, double newDemand, double stock) {
            if (oldDemand <= stock && newDemand <= stock)
                return -(newDemand - oldDemand);
            else if (oldDemand <= stock && newDemand > stock)
                return oldDemand - stock;
            else if (oldDemand > stock && newDemand < stock)
                return stock - newDemand;
            else
                return 0;
        }
        public void InitStock() {
            Random rnd = new Random();
            double initialStock = rnd.Next(20, 40) * 1000;
            for (int i = 0; i < Count; i++) {
                this[i].Stock = initialStock + this[i].Production;
                initialStock = Math.Max(0, this[i].Stock - this[i].Demand);
            }
        }
        public void UpdateCost(DraggableItem item, double newValue) {
            item.UpdateCost(newValue);
            int index = IndexOf(item);
            CalcIncome(index);
        }
        public void UpdateProduction(DraggableItem item, double newValue) {
            double stockDiff = newValue - item.Production;
            if (stockDiff < 0 && item.Stock <= 0)
                return;
            if (!item.UpdateProduction(newValue))
                return;

            int index = IndexOf(item);
            CalcFutureStock(index, stockDiff);
            CalcIncome(index);
        }
        public void UpdateDemand(DraggableItem item, double newValue) {
            double oldDemand = item.Demand;
            item.UpdateDemand(newValue);

            int index = IndexOf(item);
            double stockDiff = CalcStockDiff(oldDemand, newValue, item.Stock);
            if (stockDiff != 0)
                CalcFutureStock(index + 1, stockDiff);

            CalcIncome(index);
        }
        public void CalcIncome(int startingIndex) {
            for (int i = startingIndex; i < Count; i++) {
                double unitsSold = Math.Min(this[i].Demand, this[i].Stock);
                this[i].Income = (unitsSold * this[i].Cost - this[i].Production * ItemProductionCost) / 1000;
            }
        }
    }
}
