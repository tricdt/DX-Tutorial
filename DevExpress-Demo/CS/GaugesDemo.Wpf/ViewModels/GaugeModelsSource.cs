using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
using DevExpress.Utils;
using DevExpress.Xpf.Gauges;

namespace GaugesDemo {
    public class GaugeModelsSource : MarkupExtension {
        public IEnumerable<PredefinedElementKind> PredefinedModels { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return PredefinedModels.Select(x => new GaugeModelItem(x)).ToList();
        }
    }

    public class GaugeModelItem : ImmutableObject {
        readonly PredefinedElementKind modelKind;

        public object Model { get { return Activator.CreateInstance(modelKind.Type); } }
        public string Name { get { return modelKind.Name; } }

        public GaugeModelItem(PredefinedElementKind modelKind) {
            this.modelKind = modelKind;
        }
        public override string ToString() {
            return modelKind.Name;
        }
    }
}
