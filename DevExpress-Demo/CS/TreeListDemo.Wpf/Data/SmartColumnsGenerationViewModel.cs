using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System.Collections.Generic;

namespace TreeListDemo {
    public class SmartColumnsGenerationViewModel {
        public SmartColumnsGenerationViewModel() {
            Items = new List<List<object>>();
            Items.Add(DataGenerator.GetObjects(typeof(DataAnnotationsElement1), 100));
            Items.Add(DataGenerator.GetObjects(typeof(DataAnnotationsElement1_FluentAPI), 100));
            Items.Add(DataGenerator.GetObjects(typeof(DataAnnotationsElement2), 100));
            Items.Add(DataGenerator.GetObjects(typeof(DataAnnotationsElement2_FluentAPI), 100));
            Items.Add(DataGenerator.GetObjects(typeof(DataAnnotationsElement3), 100));
            Items.Add(DataGenerator.GetObjects(typeof(DataAnnotationsElement3_FluentAPI), 100));
            Items.Add(DataGenerator.GetObjects(typeof(DataAnnotationsElement4), 100));
            Items.Add(DataGenerator.GetObjects(typeof(DataAnnotationsElement4_FluentAPI), 100));

            CurrentItemsSource = Items[0];
        }

        public List<List<object>> Items { get; private set; }
        [BindableProperty(true, OnPropertyChangedMethodName="CheckIsSmallObject")]
        public virtual List<object> CurrentItemsSource { get; set; }
        public virtual bool IsSmallObject { get; set; }
        protected void CheckIsSmallObject() {
            IsSmallObject = CurrentItemsSource[0].GetType().GetProperties().Length <= 10;
        }
    }
    public enum Gender { Male, Female }
}
