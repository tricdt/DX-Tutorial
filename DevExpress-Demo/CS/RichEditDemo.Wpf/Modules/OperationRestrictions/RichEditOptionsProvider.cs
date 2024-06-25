using System;
using System.ComponentModel;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit;

namespace RichEditDemo {
    public class RichEditOptionsProvider {
        RichEditClipboardOptionsProvider _clipboardOptionsProvider;
        RichEditCommonOptionsProvider _commonOptionsProvider;
        RichEditZoomOptionsProvider _zoomOptionsProvider;
        RichEditMiscellaneousOptionsProvider _miscellaneousOptionsProvider;
        RichEditDocumentCapabilitiesOptionsProvider _documentCapabilitiesOptionsProvider;

        public RichEditOptionsProvider(RichEditControlOptions options) {
            this._clipboardOptionsProvider = new RichEditClipboardOptionsProvider(options.Behavior);
            this._commonOptionsProvider = new RichEditCommonOptionsProvider(options.Behavior);
            this._zoomOptionsProvider = new RichEditZoomOptionsProvider(options.Behavior);
            this._miscellaneousOptionsProvider = new RichEditMiscellaneousOptionsProvider(options.Behavior);
            this._documentCapabilitiesOptionsProvider = new RichEditDocumentCapabilitiesOptionsProvider(options.DocumentCapabilities);
        }

        public RichEditClipboardOptionsProvider Clipboard { get { return _clipboardOptionsProvider; } }
        public RichEditCommonOptionsProvider Common { get { return _commonOptionsProvider; } }
        public RichEditZoomOptionsProvider Zoom { get { return _zoomOptionsProvider; } }
        public RichEditMiscellaneousOptionsProvider Miscellaneous { get { return _miscellaneousOptionsProvider; } }
        public RichEditDocumentCapabilitiesOptionsProvider DocumentCapabilities { get { return _documentCapabilitiesOptionsProvider; } }
    }
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RichEditClipboardOptionsProvider {
        RichEditBehaviorOptions _options;

        public RichEditClipboardOptionsProvider(RichEditBehaviorOptions options) {
            this._options = options;
        }

        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Cut { get { return _options.Cut; } set { _options.Cut = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Copy { get { return _options.Copy; } set { _options.Copy = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Paste { get { return _options.Paste; } set { _options.Paste = value; } }

        public override string ToString() {
            return String.Empty;
        }
    }
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RichEditCommonOptionsProvider {
        RichEditBehaviorOptions _options;

        public RichEditCommonOptionsProvider(RichEditBehaviorOptions options) {
            this._options = options;
        }

        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Save { get { return _options.Save; } set { _options.Save = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability SaveAs { get { return _options.SaveAs; } set { _options.SaveAs = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability CreateNew { get { return _options.CreateNew; } set { _options.CreateNew = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Open { get { return _options.Open; } set { _options.Open = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Printing { get { return _options.Printing; } set { _options.Printing = value; } }

        public override string ToString() {
            return String.Empty;
        }
    }
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RichEditZoomOptionsProvider {
        public const float DefaultMinZoomFactor = 0.1f;
        public const float DefaultMaxZoomFactor = 5f;
        RichEditBehaviorOptions _options;

        public RichEditZoomOptionsProvider(RichEditBehaviorOptions options) {
            this._options = options;
        }

        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Zooming { get { return _options.Zooming; } set { _options.Zooming = value; } }
        [DefaultValue(DefaultMaxZoomFactor)]
        public float MaxZoomFactor { get { return _options.MaxZoomFactor; } set { _options.MaxZoomFactor = value; } }
        [DefaultValue(DefaultMinZoomFactor)]
        public float MinZoomFactor { get { return _options.MinZoomFactor; } set { _options.MinZoomFactor = value; } }

        public override string ToString() {
            return String.Empty;
        }
    }
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RichEditMiscellaneousOptionsProvider {
        RichEditBehaviorOptions _options;

        public RichEditMiscellaneousOptionsProvider(RichEditBehaviorOptions options) {
            this._options = options;
        }

        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Drag { get { return _options.Drag; } set { _options.Drag = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Drop { get { return _options.Drop; } set { _options.Drop = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability ShowPopupMenu { get { return _options.ShowPopupMenu; } set { _options.ShowPopupMenu = value; } }
        [DefaultValue(true)]
        public bool OvertypeAllowed { get { return _options.OvertypeAllowed; } set { _options.OvertypeAllowed = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability OfficeScrolling { get { return _options.OfficeScrolling; } set { _options.OfficeScrolling = value; } }

        public override string ToString() {
            return String.Empty;
        }
    }
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RichEditDocumentCapabilitiesOptionsProvider {
        DocumentCapabilitiesOptions _options;
        RichEditNumberingOptionsProvider _numberingOptionsProvider;

        public RichEditDocumentCapabilitiesOptionsProvider(DocumentCapabilitiesOptions options) {
            this._options = options;
            this._numberingOptionsProvider = new RichEditNumberingOptionsProvider(options.Numbering);
        }

        public RichEditNumberingOptionsProvider Numbering { get { return _numberingOptionsProvider; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Bookmarks { get { return _options.Bookmarks; } set { _options.Bookmarks = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability CharacterFormatting { get { return _options.CharacterFormatting; } set { _options.CharacterFormatting = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability CharacterStyle { get { return _options.CharacterStyle; } set { _options.CharacterStyle = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Comments { get { return _options.Comments; } set { _options.Comments = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Fields { get { return _options.Fields; } set { _options.Fields = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability FloatingObjects { get { return _options.FloatingObjects; } set { _options.FloatingObjects = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability HeadersFooter { get { return _options.HeadersFooters; } set { _options.HeadersFooters = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Hyperlinks { get { return _options.Hyperlinks; } set { _options.Hyperlinks = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability InlinePictures { get { return _options.InlinePictures; } set { _options.InlinePictures = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability InlineShapes { get { return _options.InlineShapes; } set { _options.InlineShapes = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability ParagraphFormatting { get { return _options.ParagraphFormatting; } set { _options.ParagraphFormatting = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Paragraphs { get { return _options.Paragraphs; } set { _options.Paragraphs = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability ParagraphStyle { get { return _options.ParagraphStyle; } set { _options.ParagraphStyle = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability ParagraphTabs { get { return _options.ParagraphTabs; } set { _options.ParagraphTabs = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Sections { get { return _options.Sections; } set { _options.Sections = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Tables { get { return _options.Tables; } set { _options.Tables = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability TableStyle { get { return _options.TableStyle; } set { _options.TableStyle = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability TabSymbol { get { return _options.TabSymbol; } set { _options.TabSymbol = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Undo { get { return _options.Undo; } set { _options.Undo = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability TrackChanges { get { return _options.TrackChanges; } set { _options.TrackChanges = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability ParagraphBorders { get { return _options.ParagraphBorders; } set { _options.ParagraphBorders = value; } }

        public override string ToString() {
            return String.Empty;
        }
    }
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RichEditNumberingOptionsProvider {
        NumberingOptions _options;

        public RichEditNumberingOptionsProvider(NumberingOptions options) {
            this._options = options;
        }

        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Bulleted { get { return _options.Bulleted; } set { _options.Bulleted = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability MultiLevel { get { return _options.MultiLevel; } set { _options.MultiLevel = value; } }
        [DefaultValue(DocumentCapability.Default)]
        public DocumentCapability Simple { get { return _options.Simple; } set { _options.Simple = value; } }

        public override string ToString() {
            return String.Empty;
        }
    }
}
