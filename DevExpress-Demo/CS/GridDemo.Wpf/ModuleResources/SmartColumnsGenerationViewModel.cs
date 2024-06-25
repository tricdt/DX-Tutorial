using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GridDemo {
    [POCOViewModel]
    public class SmartColumnsGenerationViewModel {
        const int RowCount = 100;
        Random rnd = new Random();
        protected SmartColumnsGenerationViewModel() {
            Items = new List<CollectionInfo>();
            Items.Add(new CollectionInfo(DataGenerator.GetObjects(typeof(DataAnnotationsElement1), RowCount), "Supported data types"));
            Items.Add(new CollectionInfo(DataGenerator.GetObjects(typeof(DataAnnotationsElement1_FluentAPI), RowCount), "Supported data types (Fluent API)"));
            Items.Add(new CollectionInfo(DataGenerator.GetObjects(typeof(DataAnnotationsElement2), RowCount, "Department"), "Attribute support"));
            Items.Add(new CollectionInfo(DataGenerator.GetObjects(typeof(DataAnnotationsElement2_FluentAPI), RowCount, "Department"), "Attribute support (Fluent API)"));
            Items.Add(new CollectionInfo(GetMaskedInputData(), "Masked input"));
            Items.Add(new CollectionInfo(GetMaskedInputData_FluentAPI(), "Masked input (Fluent API)"));
            Items.Add(new CollectionInfo(DataGenerator.GetObjects(typeof(DataAnnotationsElement3), RowCount), "Bands Layout"));
            Items.Add(new CollectionInfo(DataGenerator.GetObjects(typeof(DataAnnotationsElement3_FluentAPI), RowCount), "Bands Layout (Fluent API)"));
            Items.Add(new CollectionInfo(DataGenerator.GetObjects(typeof(DataAnnotationsElement4), RowCount), "Nested Bands Layout"));
            Items.Add(new CollectionInfo(DataGenerator.GetObjects(typeof(DataAnnotationsElement4_FluentAPI), RowCount), "Nested Bands Layout (Fluent API)"));

            SelectedCollectionInfo = Items[0];
        }

        IEnumerable<MaskedInputData> GetMaskedInputData() {
            List<MaskedInputData> result = new List<MaskedInputData>();
            for(int i = 0; i < RowCount; i++) {
                var maskedInput = new MaskedInputData {
                    PercentProperty = rnd.NextDouble(), CurrencyProperty = rnd.Next(1, 20) * 100 - 0.1m,
                    NumberProperty = rnd.Next(10000000) / 100d, FixedPointProperty = rnd.Next(10000000) / 100d, Decimal4DigitsProperty = rnd.Next(10000),
                    CustomNumericMaskPropery1 = rnd.Next(100000) / 10d, CustomNumericMaskPropery2 = rnd.Next(100000) / 10d,
                    PhoneProperty = rnd.Next(100, 1000).ToString() + "-" + rnd.Next(10, 100).ToString() + "-" + rnd.Next(10, 100).ToString(), 
                    ShortZipCodeProperty = rnd.Next(10000, 100000).ToString(), 
                    LongZipCodeProperty = rnd.Next(10000, 100000).ToString() + "-" + rnd.Next(1000, 10000).ToString(),
                    SocialSequrityProperty = rnd.Next(100, 1000).ToString() + "-" + rnd.Next(10, 100).ToString() + "-" + rnd.Next(1000, 10000).ToString(),
                };
                AssignDateTimeProperties(maskedInput);
                result.Add(maskedInput);
            }
            return result;
        }
        IEnumerable<MaskedInputData_FluentAPI> GetMaskedInputData_FluentAPI() {
            List<MaskedInputData_FluentAPI> result = new List<MaskedInputData_FluentAPI>();
            for(int i = 0; i < RowCount; i++) {
                var maskedInput = new MaskedInputData_FluentAPI {
                    PercentProperty = rnd.NextDouble(), CurrencyProperty = rnd.Next(1, 20) * 100 - 0.1m,
                    NumberProperty = rnd.Next(10000000) / 100d, FixedPointProperty = rnd.Next(10000000) / 100d, Decimal4DigitsProperty = rnd.Next(10000),
                    CustomNumericMaskPropery1 = rnd.Next(100000) / 10d, CustomNumericMaskPropery2 = rnd.Next(100000) / 10d,
                    PhoneProperty = rnd.Next(100, 1000).ToString() + "-" + rnd.Next(10, 100).ToString() + "-" + rnd.Next(10, 100).ToString(),
                    ShortZipCodeProperty = rnd.Next(10000, 100000).ToString(),
                    LongZipCodeProperty = rnd.Next(10000, 100000).ToString() + "-" + rnd.Next(1000, 10000).ToString(),
                    SocialSequrityProperty = rnd.Next(100, 1000).ToString() + "-" + rnd.Next(10, 100).ToString() + "-" + rnd.Next(1000, 10000).ToString(),
                };
                AssignDateTimeProperties(maskedInput);
                result.Add(maskedInput);
            }
            return result;
        }
        void AssignDateTimeProperties(object obj) {
            foreach(var property in obj.GetType().GetProperties().Where(x => x.PropertyType == typeof(DateTime))) {
                property.SetValue(obj, DateTime.Now.AddDays(rnd.Next(-1000, 1000)), null);
            }
        }
        public List<CollectionInfo> Items { get; private set; }

        public virtual CollectionInfo SelectedCollectionInfo { get; set; }

        protected void OnSelectedCollectionInfoChanged() {
            IsSmallObject = SelectedCollectionInfo.Collection.Cast<object>().First().GetType().GetProperties().Length <= 10;
        }

        public virtual bool IsSmallObject { get; set; }
    }
    public enum Gender { Male, Female }
}
