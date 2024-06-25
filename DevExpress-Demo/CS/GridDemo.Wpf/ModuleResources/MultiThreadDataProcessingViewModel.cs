using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;

namespace GridDemo {
    [POCOViewModel]
    public class PLinqInstantFeedbackDemoViewModel {
        #region CallbackListSource
        class CallbackListSource : IListSource {
            readonly Func<IList> _GetList;
            public CallbackListSource(Func<IList> getList) {
                this._GetList = getList;
            }
            public bool ContainsListCollection { get { return false; } }
            public IList GetList() {
                return _GetList();
            }
        }
        #endregion

        protected PLinqInstantFeedbackDemoViewModel() { }

        public virtual IListSource ListSource { get; set; }

        public virtual int Count { get; set; }
        protected void OnCountChanged() {
            ListSource = new CallbackListSource(GetOrders);
        }

        ConcurrentDictionary<int, IList> listCache = new ConcurrentDictionary<int, IList>();
        IList GetOrders() {
            var actualCount = ViewModelBase.IsInDesignMode ? 0 : Count;
            return listCache.GetOrAdd(actualCount, count => OrderDataGenerator.GenerateOrders(count));
        }
    }
}
