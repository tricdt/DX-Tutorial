using System.Collections.Generic;
using System.Linq;
using DevExpress.DemoData.Models.Vehicles;

namespace LayoutControlDemo {
    public class Brand {
        public string Name { get; set; }
        public byte[] Image { get; set; }
    }

    public class Car {
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public decimal Price { get; set; }
        public bool IsFirstInBrand { get; set; }
        public bool IsLastInBrand { get; set; }
    }

    public class CarsViewModel {
        public List<object> Items { get; private set; }

        public CarsViewModel() {
            Items = new List<object>();
            using(var context = new VehiclesContext()) {
                var brands = context.Trademarks.OrderBy(x => x.Name);
                foreach(var brand in brands) {
                    var cars = context.Models.Where(x => x.TrademarkID == brand.ID).OrderBy(x => x.Name);
                    if(cars.Count() > 0) {
                        Items.Add(new Brand() { Name = brand.Name, Image = brand.Logo });
                        bool isFirstInBrand = true;
                        foreach(var car in cars) {
                            Items.Add(new Car() {
                                Image = car.Image,
                                Name = brand.Name + " " + car.Name,
                                Price = car.Price,
                                IsFirstInBrand = isFirstInBrand
                            });
                            isFirstInBrand = false;
                        }
                        (Items.Last() as Car).IsLastInBrand = true;
                    }
                }
            }
        }
    }
}
