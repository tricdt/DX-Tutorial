using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    class CustomBar3DModelNamed : CustomBar3DModel {
        public override string ModelName { get { return CustomName ?? "Custom"; } }
        public string CustomName { get; set; }
    }
}
