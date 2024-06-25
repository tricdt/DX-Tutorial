using DevExpress.Xpf.Grid;

namespace GridDemo {
    public class ColumnDescription {
        public ColumnDescription(string propertyName, string displayName = null, string styleKey = null) {
            PropertyName = propertyName;
            DisplayName = displayName ?? propertyName;
            StyleKey = styleKey;
        }
        public string PropertyName { get; private set; }
        public string DisplayName { get; private set; }
        public string StyleKey { get; set; }
    }
    public class BandDescription {
        public BandDescription(string displayName, ColumnDescription[] columns, FixedStyle _fixed = FixedStyle.None, bool overlayByChildren = false) {
            DisplayName = displayName;
            Columns = columns;
            Fixed = _fixed;
            OverlayHeaderByChildren = overlayByChildren;
        }
        public string DisplayName { get; private set; }
        public ColumnDescription[] Columns { get; private set; }

        public FixedStyle Fixed { get; set; }

        public bool OverlayHeaderByChildren { get; set; }
    }
}
