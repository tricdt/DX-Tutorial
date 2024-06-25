using System;

namespace GridDemo {
    public partial class WCFInstantFeedback : GridDemoModule {
        public static readonly Uri ServiceUri = new Uri("http://demos.devexpress.com/Services/WcfLinqSC/WcfSCService.svc/");
        public static WcfSCService.SCEntities DataServiceContext { get { return new WcfSCService.SCEntities(ServiceUri); } }
        public WCFInstantFeedback() {
            InitializeComponent();
            ModuleUnloaded += (s, e) => {
                grid.ItemsSource = null;
                wcfInstantSource.Dispose();
            };
        }
    }
}
