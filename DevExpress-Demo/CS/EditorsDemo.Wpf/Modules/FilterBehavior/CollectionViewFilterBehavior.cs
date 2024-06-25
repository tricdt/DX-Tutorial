using DevExpress.Data.Filtering;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Data;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;

namespace EditorsDemo.FilterBehavior {
    public class CollectionViewFilterBehavior : Behavior<FrameworkElement> {
        public ICollectionView CollectionView {
            get { return (ICollectionView)GetValue(CollectionViewProperty); }
            set { SetValue(CollectionViewProperty, value); }
        }
        public static readonly DependencyProperty CollectionViewProperty =
            DependencyProperty.Register("CollectionView", typeof(ICollectionView), typeof(CollectionViewFilterBehavior), 
                new PropertyMetadata(null, (o, e) => ((CollectionViewFilterBehavior)o).UpdateCollectionView()));

        public CriteriaOperator FilterCriteria {
            get { return (CriteriaOperator)GetValue(FilterCriteriaProperty); }
            set { SetValue(FilterCriteriaProperty, value); }
        }
        public static readonly DependencyProperty FilterCriteriaProperty =
            DependencyProperty.Register("FilterCriteria", typeof(CriteriaOperator), typeof(CollectionViewFilterBehavior), 
                new PropertyMetadata(null, (o, e) => ((CollectionViewFilterBehavior)o).UpdateCollectionView()));

        void UpdateCollectionView() {
            if(CollectionView == null)
                return;
            var elementType = ListBindingHelper.GetListItemType(CollectionView.SourceCollection);
            var method = GetType()
                .GetMethod("ApplyFilter", BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(new[] { elementType });
            method.Invoke(this, null);
        }
        void ApplyFilter<T>() {
            var converter = new GridFilterCriteriaToExpressionConverter<T>();
            var expression = converter.Convert(FilterCriteria);
            var filter = expression.Compile();
            CollectionView.Filter = x => filter((T)x);
        }
    }
}
