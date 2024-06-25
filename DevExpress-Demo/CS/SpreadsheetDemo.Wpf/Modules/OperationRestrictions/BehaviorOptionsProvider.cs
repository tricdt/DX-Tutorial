using DevExpress.Xpf.Spreadsheet;
using System;
using System.ComponentModel;

using DocumentCapability = DevExpress.XtraSpreadsheet.DocumentCapability;
using MoveActiveCellModeOnEnterPress = DevExpress.XtraSpreadsheet.MoveActiveCellModeOnEnterPress;

namespace DevExpress.Spreadsheet.Demos {
    public class BehaviorOptionsProvider {
        readonly SpreadsheetBehaviorOptions options;
        readonly SpreadsheetBehaviorClipboardOptionsProvider clipboardOptionsProvider;
        readonly SpreadsheetBehaviorCommonOptionsProvider commonOptionsProvider;
        readonly SpreadsheetBehaviorZoomOptionsProvider zoomOptionsProvider;
        readonly SpreadsheetBehaviorWorksheetOptionsProvider worksheetOptionsProvider;
        readonly SpreadsheetBehaviorColumnOptionsProvider columnOptionsProvider;
        readonly SpreadsheetBehaviorRowOptionsProvider rowOptionsProvider;
        readonly SpreadsheetBehaviorCommentOptionsProvider commentOptionsProvider;
        readonly SpreadsheetBehaviorGroupOptionsProvider groupOptionsProvider;
        readonly SpreadsheetBehaviorSelectionOptionsProvider selectionOptionsProvider;
        readonly SpreadsheetBehaviorDrawingOptionsProvider drawingOptionsProvider;
        readonly SpreadsheetBehaviorMiscellaneousOptionsProvider miscellaneousOptionsProvider;

        public BehaviorOptionsProvider(SpreadsheetBehaviorOptions options) {
            this.options = options;
            this.clipboardOptionsProvider = new SpreadsheetBehaviorClipboardOptionsProvider(options);
            this.commonOptionsProvider = new SpreadsheetBehaviorCommonOptionsProvider(options);
            this.zoomOptionsProvider = new SpreadsheetBehaviorZoomOptionsProvider(options);
            this.worksheetOptionsProvider = new SpreadsheetBehaviorWorksheetOptionsProvider(options.Worksheet);
            this.columnOptionsProvider = new SpreadsheetBehaviorColumnOptionsProvider(options.Column);
            this.rowOptionsProvider = new SpreadsheetBehaviorRowOptionsProvider(options.Row);
            this.commentOptionsProvider = new SpreadsheetBehaviorCommentOptionsProvider(options.Comment);
            this.groupOptionsProvider = new SpreadsheetBehaviorGroupOptionsProvider(options.Group);
            this.selectionOptionsProvider = new SpreadsheetBehaviorSelectionOptionsProvider(options.Selection);
            this.drawingOptionsProvider = new SpreadsheetBehaviorDrawingOptionsProvider(options.Drawing);
            this.miscellaneousOptionsProvider = new SpreadsheetBehaviorMiscellaneousOptionsProvider(options);
        }

        public SpreadsheetBehaviorClipboardOptionsProvider Clipboard { get { return clipboardOptionsProvider; } }
        public SpreadsheetBehaviorCommonOptionsProvider Common { get { return commonOptionsProvider; } }
        public SpreadsheetBehaviorZoomOptionsProvider Zoom { get { return zoomOptionsProvider; } }
        public SpreadsheetBehaviorWorksheetOptionsProvider Worksheet { get { return worksheetOptionsProvider; } }
        public SpreadsheetBehaviorColumnOptionsProvider Column { get { return columnOptionsProvider; } }
        public SpreadsheetBehaviorRowOptionsProvider Row { get { return rowOptionsProvider; } }
        public SpreadsheetBehaviorCommentOptionsProvider Comment { get { return commentOptionsProvider; } }
        public SpreadsheetBehaviorGroupOptionsProvider Group { get { return groupOptionsProvider; } }
        public SpreadsheetBehaviorSelectionOptionsProvider Selection { get { return selectionOptionsProvider; } }
        public SpreadsheetBehaviorDrawingOptionsProvider Drawing { get { return drawingOptionsProvider; } }
        public SpreadsheetBehaviorMiscellaneousOptionsProvider Miscellaneous { get { return miscellaneousOptionsProvider; } }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SpreadsheetBehaviorClipboardOptionsProvider {
        readonly SpreadsheetBehaviorOptions options;

        public SpreadsheetBehaviorClipboardOptionsProvider(SpreadsheetBehaviorOptions options) {
            this.options = options;
        }

        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Cut { get { return options.Cut; } set { options.Cut = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Copy { get { return options.Copy; } set { options.Copy = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Paste { get { return options.Paste; } set { options.Paste = value; } }

        public override string ToString() {
            return String.Empty;
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SpreadsheetBehaviorCommonOptionsProvider {
        readonly SpreadsheetBehaviorOptions options;

        public SpreadsheetBehaviorCommonOptionsProvider(SpreadsheetBehaviorOptions options) {
            this.options = options;
        }

        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Open { get { return options.Open; } set { options.Open = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Save { get { return options.Save; } set { options.Save = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability SaveAs { get { return options.SaveAs; } set { options.SaveAs = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Print { get { return options.Print; } set { options.Print = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability CreateNew { get { return options.CreateNew; } set { options.CreateNew = value; } }

        public override string ToString() {
            return String.Empty;
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SpreadsheetBehaviorZoomOptionsProvider {
        public const float MinZoomFactorDefault = 0.5f;
        public const float MaxZoomFactorDefault = 3;
        readonly SpreadsheetBehaviorOptions options;

        public SpreadsheetBehaviorZoomOptionsProvider(SpreadsheetBehaviorOptions options) {
            this.options = options;
            MinZoomFactor = MinZoomFactorDefault;
            MaxZoomFactor = MaxZoomFactorDefault;
        }

        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Zoom { get { return options.Zoom; } set { options.Zoom = value; } }
        [DefaultValue(MinZoomFactorDefault)]
        public float MinZoomFactor { get { return options.MinZoomFactor; } set { options.MinZoomFactor = value; } }
        [DefaultValue(MaxZoomFactorDefault)]
        public float MaxZoomFactor { get { return options.MaxZoomFactor; } set { options.MaxZoomFactor = value; } }

        public override string ToString() {
            return String.Empty;
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SpreadsheetBehaviorWorksheetOptionsProvider {
        readonly SpreadsheetWorksheetBehaviorOptions options;

        public SpreadsheetBehaviorWorksheetOptionsProvider(SpreadsheetWorksheetBehaviorOptions options) {
            this.options = options;
        }

        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Delete { get { return options.Delete; } set { options.Delete = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Hide { get { return options.Hide; } set { options.Hide = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Insert { get { return options.Insert; } set { options.Insert = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Rename { get { return options.Rename; } set { options.Rename = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability TabColor { get { return options.TabColor; } set { options.TabColor = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Unhide { get { return options.Unhide; } set { options.Unhide = value; } }

        public override string ToString() {
            return String.Empty;
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SpreadsheetBehaviorColumnOptionsProvider {
        readonly SpreadsheetColumnBehaviorOptions options;

        public SpreadsheetBehaviorColumnOptionsProvider(SpreadsheetColumnBehaviorOptions options) {
            this.options = options;
        }

        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability AutoFit { get { return options.AutoFit; } set { options.AutoFit = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Delete { get { return options.Delete; } set { options.Delete = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Hide { get { return options.Hide; } set { options.Hide = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Insert { get { return options.Insert; } set { options.Insert = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Resize { get { return options.Resize; } set { options.Resize = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Unhide { get { return options.Unhide; } set { options.Unhide = value; } }

        public override string ToString() {
            return String.Empty;
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SpreadsheetBehaviorRowOptionsProvider {
        readonly SpreadsheetRowBehaviorOptions options;

        public SpreadsheetBehaviorRowOptionsProvider(SpreadsheetRowBehaviorOptions options) {
            this.options = options;
        }

        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability AutoFit { get { return options.AutoFit; } set { options.AutoFit = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Delete { get { return options.Delete; } set { options.Delete = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Hide { get { return options.Hide; } set { options.Hide = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Insert { get { return options.Insert; } set { options.Insert = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Resize { get { return options.Resize; } set { options.Resize = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Unhide { get { return options.Unhide; } set { options.Unhide = value; } }

        public override string ToString() {
            return String.Empty;
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SpreadsheetBehaviorCommentOptionsProvider {
        readonly SpreadsheetCommentBehaviorOptions options;

        public SpreadsheetBehaviorCommentOptionsProvider(SpreadsheetCommentBehaviorOptions options) {
            this.options = options;
        }

        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Delete { get { return options.Delete; } set { options.Delete = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Edit { get { return options.Edit; } set { options.Edit = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Insert { get { return options.Insert; } set { options.Insert = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Move { get { return options.Move; } set { options.Move = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Resize { get { return options.Resize; } set { options.Resize = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability ShowHide { get { return options.ShowHide; } set { options.ShowHide = value; } }

        public override string ToString() {
            return String.Empty;
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SpreadsheetBehaviorGroupOptionsProvider {
        readonly SpreadsheetGroupBehaviorOptions options;

        public SpreadsheetBehaviorGroupOptionsProvider(SpreadsheetGroupBehaviorOptions options) {
            this.options = options;
        }

        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability ChangeSettings { get { return options.ChangeSettings; } set { options.ChangeSettings = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Collapse { get { return options.Collapse; } set { options.Collapse = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Expand { get { return options.Expand; } set { options.Expand = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Group { get { return options.Group; } set { options.Group = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Ungroup { get { return options.Ungroup; } set { options.Ungroup = value; } }

        public override string ToString() {
            return String.Empty;
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SpreadsheetBehaviorSelectionOptionsProvider {
        readonly SpreadsheetSelectionBehaviorOptions options;

        public SpreadsheetBehaviorSelectionOptionsProvider(SpreadsheetSelectionBehaviorOptions options) {
            this.options = options;
        }

        [DefaultValue(true)]
        public bool AllowMultiSelection { get { return options.AllowMultiSelection; } set { options.AllowMultiSelection = value; } }
        [DefaultValue(MoveActiveCellModeOnEnterPress.Down)]
        public MoveActiveCellModeOnEnterPress MoveActiveCellMode { get { return options.MoveActiveCellMode; } set { options.MoveActiveCellMode = value; } }
        [DefaultValue(DevExpress.XtraSpreadsheet.ShowSelectionMode.Always)]
        public DevExpress.XtraSpreadsheet.ShowSelectionMode ShowSelectionMode { get { return options.ShowSelectionMode; } set { options.ShowSelectionMode = value; } }

        public override string ToString() {
            return String.Empty;
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SpreadsheetBehaviorDrawingOptionsProvider {
        readonly SpreadsheetDrawingBehaviorOptions options;

        public SpreadsheetBehaviorDrawingOptionsProvider(SpreadsheetDrawingBehaviorOptions options) {
            this.options = options;
        }

        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability ChangeZOrder { get { return options.ChangeZOrder; } set { options.ChangeZOrder = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Move { get { return options.Move; } set { options.Move = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Resize { get { return options.Resize; } set { options.Resize = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Rotate { get { return options.Rotate; } set { options.Rotate = value; } }

        public override string ToString() {
            return String.Empty;
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SpreadsheetBehaviorMiscellaneousOptionsProvider {
        readonly SpreadsheetBehaviorOptions options;

        public SpreadsheetBehaviorMiscellaneousOptionsProvider(SpreadsheetBehaviorOptions options) {
            this.options = options;
        }

        [DefaultValue(DevExpress.XtraSpreadsheet.CellEditorCommitMode.Auto)]
        public DevExpress.XtraSpreadsheet.CellEditorCommitMode CellEditorCommitMode { get { return options.CellEditorCommitMode; } set { options.CellEditorCommitMode = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability FreezePanes { get { return options.FreezePanes; } set { options.FreezePanes = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability ShowPopupMenu { get { return options.ShowPopupMenu; } set { options.ShowPopupMenu = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Touch { get { return options.Touch; } set { options.Touch = value; } }
        [DefaultValue(DevExpress.XtraSpreadsheet.FunctionNameCulture.Auto)]
        public DevExpress.XtraSpreadsheet.FunctionNameCulture FunctionNameCulture { get { return options.FunctionNameCulture; } set { options.FunctionNameCulture = value; } }
        [DefaultValue(true)]
        public bool FillHandleEnabled { get { return options.FillHandleEnabled; } set { options.FillHandleEnabled = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Drag { get { return options.Drag; } set { options.Drag = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Drop { get { return options.Drop; } set { options.Drop = value; } }

        public override string ToString() {
            return String.Empty;
        }
    }
}
