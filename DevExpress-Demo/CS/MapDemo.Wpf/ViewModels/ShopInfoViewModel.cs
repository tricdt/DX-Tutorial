using System.Collections.Generic;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Map;

namespace MapDemo {
    [POCOViewModel]
    public class ShopInfoViewModel {
        public virtual string Name { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Address { get; set; }
        public virtual double Sales { get; set; }
        public virtual GeoPoint ShopLocation { get; set; }

        static string ConvertShopNameToFilePath(string ShopName) {
            string result = ShopName.Replace(" ", "");
            result = "../Images/Shops/" + result.Replace("-", "") + ".png";
            return result;
        }

        Dictionary<string, double> statistics = new Dictionary<string, double>();
        readonly string imagePath;

        public ShopInfoViewModel(string Name, string Address, string Phone, string Fax) {
            this.Name = Name;
            this.Address = Address;
            this.Phone = Phone;
            this.Fax = Fax;
            this.imagePath = ConvertShopNameToFilePath(Name);
        }

        public string ImagePath { get { return imagePath; } }

        public void AddProductGroup(string groupName, double sales) {
            if(statistics.ContainsKey(groupName))
                statistics[groupName] = sales;
            else
                statistics.Add(groupName, sales);
            Sales += sales;
        }
        public double GetSalesByProductGroup(string groupName) {
            return statistics.ContainsKey(groupName) ? statistics[groupName] : 0.0;
        }
    }
}
