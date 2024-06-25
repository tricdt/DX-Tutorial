using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Map;

namespace MapDemo {
    public class SalesDashboardViewModel {
        public virtual bool GaugeVisibility { get; set; }
        public virtual ObservableCollection<ProductGroupViewModel> ActualStatistics { get; set; }
        public virtual ObservableCollection<ShopInfoViewModel> Shops { get; set; }
        public virtual string SalesDescription { get; set; }
        [BindableProperty(true, OnPropertyChangedMethodName = "SelectedShopPropertyChanged")]
        public virtual ShopInfoViewModel SelectedShop { get; set; }
        public virtual double MinSalesLevel { get; set; }
        public virtual double MaxSalesLevel { get; set; }
        public virtual IChartControlService ChartControlService { get { return null; } }

        protected void SelectedShopPropertyChanged() {
            if(SelectedShop != null)
                this.UpdateStatistics(SelectedShop);
            else
                this.UpdateTotalStatistics();
        }
        public SalesDashboardViewModel() {
            Shops = new ObservableCollection<ShopInfoViewModel>();
            ActualStatistics = new ObservableCollection<ProductGroupViewModel>();
            LoadDataFromXML();
            UpdateMinMaxSales();
            UpdateTotalStatistics();
        }
        void LoadDataFromXML() {
            List<string> productGroupNames = new List<string>();
            XDocument document = DataLoader.LoadXmlFromResources("/Data/Sales.xml");
            if(document != null) {
                foreach(XElement element in document.Element("Sales").Elements()) {
                    string shopName = element.Element("ShopName").Value;
                    string shopAddress = element.Element("ShopAddr").Value;
                    string shopPhone = element.Element("ShopPhone").Value;
                    string shopFax = element.Element("ShopFax").Value;
                    var sw = new Stopwatch();
                    sw.Start();
                    ShopInfoViewModel info = ViewModelSource.Create(() => new ShopInfoViewModel(shopName, shopAddress, shopPhone, shopFax));
                    sw.Stop();
                    foreach(XElement statElement in element.Element("ShopStatistics").Elements()) {
                        string groupName = statElement.Element("ProductsGroupName").Value;
                        if(!productGroupNames.Contains(groupName))
                            productGroupNames.Add(groupName);
                        double sales = Convert.ToDouble(statElement.Element("ProductGroupSales").Value, CultureInfo.InvariantCulture);
                        info.AddProductGroup(groupName, sales);
                    }
                    GeoPoint geoPoint = new GeoPoint(Convert.ToDouble(element.Element("Latitude").Value, CultureInfo.InvariantCulture), Convert.ToDouble(element.Element("Longitude").Value, CultureInfo.InvariantCulture));
                    info.ShopLocation = geoPoint;
                    Shops.Add(info);
                }
            }
            foreach(string groupName in productGroupNames)
                ActualStatistics.Add(ViewModelSource.Create(() => new ProductGroupViewModel(0.0, groupName)));
            UpdateTotalStatistics();
        }
        void UpdateStatistics(ShopInfoViewModel info) {
            foreach(ProductGroupViewModel productGroupInfo in ActualStatistics)
                productGroupInfo.Value = info.GetSalesByProductGroup(productGroupInfo.Name);
            SalesDescription = "Last Month Sales: " + info.Name;
            if(ChartControlService != null) {
                ChartControlService.UpdateData();
                ChartControlService.Animate();
            }
            GaugeVisibility = true;
        }
        void UpdateMinMaxSales() {
            double minSales = Shops[0].Sales;
            double maxSales = Shops[0].Sales;
            foreach(ShopInfoViewModel info in Shops) {
                if(info.Sales > maxSales)
                    maxSales = info.Sales;
                if(info.Sales < minSales)
                    minSales = info.Sales;
            }
            MinSalesLevel = minSales - 10000;
            MaxSalesLevel = maxSales + 10000;
        }
        public void UpdateTotalStatistics() {
            foreach(ProductGroupViewModel info in ActualStatistics) {
                info.Value = 0.0;
                foreach(ShopInfoViewModel shopInfo in Shops)
                    info.Value += shopInfo.GetSalesByProductGroup(info.Name);
            }
            GaugeVisibility = false;
            SalesDescription = "Last Month Sales: All Shops";
        }
    }
}
