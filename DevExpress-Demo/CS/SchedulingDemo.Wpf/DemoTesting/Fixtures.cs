using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpf.DemoBase.DemoTesting;

namespace SchedulingDemo.Tests {
    public class SchedulingCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool CanRunModule(Type moduleType) {
            if(moduleType == typeof(OnDemandDataLoading))
                return false;
            return base.CanRunModule(moduleType);
        }
        protected override bool CheckMemoryLeaks(Type moduleType) {
            if(moduleType == typeof(PrintingTemplatesDemoModule))
                return false;
            return true;
        }	
    }
}
