using DevExpress.Xpf.Charts.Sankey;
using System;
using System.Collections.Generic;

namespace SankeyDemo {
    public abstract class AscendingOrDescendingNodeComparer : IComparer<SankeyNode> {
        public bool Ascending { get; set; }

        public abstract int Compare(SankeyNode x, SankeyNode y);
    }
    public class TotalWeightComparer : AscendingOrDescendingNodeComparer {
        public override int Compare(SankeyNode x, SankeyNode y) {
            return (Ascending ? 1 : -1) * Math.Sign(x.TotalWeight - y.TotalWeight);
        }

        public override string ToString() {
            return "Total Weight";
        }
    }

    public class OutputLinkCountComparer : AscendingOrDescendingNodeComparer, IComparer<SankeyNode> {
        public override int Compare(SankeyNode x, SankeyNode y) {
            if(x.OutputLinks.Count != 0)
                return (Ascending ? 1 : -1) * Math.Sign(x.OutputLinks.Count - y.OutputLinks.Count);
            else
                return (Ascending ? 1 : -1) * Math.Sign(x.InputLinks.Count - y.InputLinks.Count);
        }

        public override string ToString() {
            return "Output Link Count";
        }
    }

    public class NodeNameComparer : AscendingOrDescendingNodeComparer, IComparer<SankeyNode> {
        public override int Compare(SankeyNode x, SankeyNode y) {
            return (Ascending ? 1 : -1) * x.Tag.ToString().CompareTo(y.Tag.ToString());
        }

        public override string ToString() {
            return "Node Name";
        }
    }
}
