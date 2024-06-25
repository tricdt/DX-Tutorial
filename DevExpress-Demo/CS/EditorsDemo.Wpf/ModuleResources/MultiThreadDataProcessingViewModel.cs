using System;
using System.Collections.Generic;
using DevExpress.Mvvm;

namespace GridDemo {
    public class PLinqInstantFeedbackDemoViewModel : BindableBase {
        OrderDataListSource orderDataListSource;
        OrderInfoDataGenerator orderDataGenerator = new OrderInfoDataGenerator(0);
        bool isGeneratingOrderData;
        double generateOrderDataProgressValue;
        CountItemCollection countItems;
        CountItem selectedCountItem;
        bool isDesignTime = true;

        void orderDataGenerator_GenerateOrderDataStarted(object sender, EventArgs e) {
            IsGeneratingOrderData = true;
        }
        void orderDataGenerator_GenerateOrderDataCompleted(object sender, EventArgs e) {
            IsGeneratingOrderData = false;
        }
        void orderDataGenerator_GenerateOrderDataProgress(object sender, GenerateOrderDataProgressEventArgs e) {
            GenerateOrderDataProgressValue = e.Progress;
        }
        public PLinqInstantFeedbackDemoViewModel() {
            orderDataGenerator.GenerateOrderDataStarted += orderDataGenerator_GenerateOrderDataStarted;
            orderDataGenerator.GenerateOrderDataCompleted += orderDataGenerator_GenerateOrderDataCompleted;
            orderDataGenerator.GenerateOrderDataProgress += orderDataGenerator_GenerateOrderDataProgress;
        }
        public PLinqInstantFeedbackDemoViewModel(bool isDesignTime)
            : this() {
            this.isDesignTime = isDesignTime;
        }

        void RecreateListSource() {
            if((SelectedCountItem != null) && !isDesignTime) {
                orderDataGenerator.Count = SelectedCountItem.Count;
            } else {
                orderDataGenerator.Count = 0;
            }
            ListSource = new OrderDataListSource(orderDataGenerator);
        }
        public OrderDataListSource ListSource {
            get { return orderDataListSource; }
            set { SetProperty(ref orderDataListSource, value, () => ListSource); }
        }
        public List<CategoryData> Categories {
            get { return orderDataGenerator.GetCategories(); }
        }
        public bool IsGeneratingOrderData {
            get { return isGeneratingOrderData; }
            set { SetProperty(ref isGeneratingOrderData, value, () => IsGeneratingOrderData); }
        }
        public double GenerateOrderDataProgressValue {
            get { return generateOrderDataProgressValue; }
            set { SetProperty(ref generateOrderDataProgressValue, value, () => GenerateOrderDataProgressValue); }
        }
        public CountItemCollection CountItems {
            get { return countItems; }
            set {
                if(SetProperty(ref countItems, value, () => CountItems)) {
                    if((CountItems != null) && (CountItems.Count > 0)) {
                        SelectedCountItem = CountItems[CountItems.Count / 2];
                    } else {
                        SelectedCountItem = null;
                    }
                }
            }
        }
        public CountItem SelectedCountItem {
            get { return selectedCountItem; }
            set { SetProperty(ref selectedCountItem, value, () => SelectedCountItem, RecreateListSource); }
        }
        public void SetIsDesignTime(bool value) {
            isDesignTime = value;
            RecreateListSource();
        }
    }
}
