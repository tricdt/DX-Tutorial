Imports System.ComponentModel
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.XtraRichEdit

Namespace RichEditDemo

    Public Class RichEditOptionsProvider

        Private _clipboardOptionsProvider As RichEditClipboardOptionsProvider

        Private _commonOptionsProvider As RichEditCommonOptionsProvider

        Private _zoomOptionsProvider As RichEditZoomOptionsProvider

        Private _miscellaneousOptionsProvider As RichEditMiscellaneousOptionsProvider

        Private _documentCapabilitiesOptionsProvider As RichEditDocumentCapabilitiesOptionsProvider

        Public Sub New(ByVal options As RichEditControlOptions)
            _clipboardOptionsProvider = New RichEditClipboardOptionsProvider(options.Behavior)
            _commonOptionsProvider = New RichEditCommonOptionsProvider(options.Behavior)
            _zoomOptionsProvider = New RichEditZoomOptionsProvider(options.Behavior)
            _miscellaneousOptionsProvider = New RichEditMiscellaneousOptionsProvider(options.Behavior)
            _documentCapabilitiesOptionsProvider = New RichEditDocumentCapabilitiesOptionsProvider(options.DocumentCapabilities)
        End Sub

        Public ReadOnly Property Clipboard As RichEditClipboardOptionsProvider
            Get
                Return _clipboardOptionsProvider
            End Get
        End Property

        Public ReadOnly Property Common As RichEditCommonOptionsProvider
            Get
                Return _commonOptionsProvider
            End Get
        End Property

        Public ReadOnly Property Zoom As RichEditZoomOptionsProvider
            Get
                Return _zoomOptionsProvider
            End Get
        End Property

        Public ReadOnly Property Miscellaneous As RichEditMiscellaneousOptionsProvider
            Get
                Return _miscellaneousOptionsProvider
            End Get
        End Property

        Public ReadOnly Property DocumentCapabilities As RichEditDocumentCapabilitiesOptionsProvider
            Get
                Return _documentCapabilitiesOptionsProvider
            End Get
        End Property
    End Class

    <TypeConverter(GetType(ExpandableObjectConverter))>
    Public Class RichEditClipboardOptionsProvider

        Private _options As RichEditBehaviorOptions

        Public Sub New(ByVal options As RichEditBehaviorOptions)
            _options = options
        End Sub

        <DefaultValue(DocumentCapability.Default)>
        Public Property Cut As DocumentCapability
            Get
                Return _options.Cut
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Cut = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Copy As DocumentCapability
            Get
                Return _options.Copy
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Copy = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Paste As DocumentCapability
            Get
                Return _options.Paste
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Paste = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Empty
        End Function
    End Class

    <TypeConverter(GetType(ExpandableObjectConverter))>
    Public Class RichEditCommonOptionsProvider

        Private _options As RichEditBehaviorOptions

        Public Sub New(ByVal options As RichEditBehaviorOptions)
            _options = options
        End Sub

        <DefaultValue(DocumentCapability.Default)>
        Public Property Save As DocumentCapability
            Get
                Return _options.Save
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Save = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property SaveAs As DocumentCapability
            Get
                Return _options.SaveAs
            End Get

            Set(ByVal value As DocumentCapability)
                _options.SaveAs = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property CreateNew As DocumentCapability
            Get
                Return _options.CreateNew
            End Get

            Set(ByVal value As DocumentCapability)
                _options.CreateNew = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Open As DocumentCapability
            Get
                Return _options.Open
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Open = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Printing As DocumentCapability
            Get
                Return _options.Printing
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Printing = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Empty
        End Function
    End Class

    <TypeConverter(GetType(ExpandableObjectConverter))>
    Public Class RichEditZoomOptionsProvider

        Public Const DefaultMinZoomFactor As Single = 0.1F

        Public Const DefaultMaxZoomFactor As Single = 5F

        Private _options As RichEditBehaviorOptions

        Public Sub New(ByVal options As RichEditBehaviorOptions)
            _options = options
        End Sub

        <DefaultValue(DocumentCapability.Default)>
        Public Property Zooming As DocumentCapability
            Get
                Return _options.Zooming
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Zooming = value
            End Set
        End Property

        <DefaultValue(DefaultMaxZoomFactor)>
        Public Property MaxZoomFactor As Single
            Get
                Return _options.MaxZoomFactor
            End Get

            Set(ByVal value As Single)
                _options.MaxZoomFactor = value
            End Set
        End Property

        <DefaultValue(DefaultMinZoomFactor)>
        Public Property MinZoomFactor As Single
            Get
                Return _options.MinZoomFactor
            End Get

            Set(ByVal value As Single)
                _options.MinZoomFactor = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Empty
        End Function
    End Class

    <TypeConverter(GetType(ExpandableObjectConverter))>
    Public Class RichEditMiscellaneousOptionsProvider

        Private _options As RichEditBehaviorOptions

        Public Sub New(ByVal options As RichEditBehaviorOptions)
            _options = options
        End Sub

        <DefaultValue(DocumentCapability.Default)>
        Public Property Drag As DocumentCapability
            Get
                Return _options.Drag
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Drag = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Drop As DocumentCapability
            Get
                Return _options.Drop
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Drop = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property ShowPopupMenu As DocumentCapability
            Get
                Return _options.ShowPopupMenu
            End Get

            Set(ByVal value As DocumentCapability)
                _options.ShowPopupMenu = value
            End Set
        End Property

        <DefaultValue(True)>
        Public Property OvertypeAllowed As Boolean
            Get
                Return _options.OvertypeAllowed
            End Get

            Set(ByVal value As Boolean)
                _options.OvertypeAllowed = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property OfficeScrolling As DocumentCapability
            Get
                Return _options.OfficeScrolling
            End Get

            Set(ByVal value As DocumentCapability)
                _options.OfficeScrolling = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Empty
        End Function
    End Class

    <TypeConverter(GetType(ExpandableObjectConverter))>
    Public Class RichEditDocumentCapabilitiesOptionsProvider

        Private _options As DocumentCapabilitiesOptions

        Private _numberingOptionsProvider As RichEditNumberingOptionsProvider

        Public Sub New(ByVal options As DocumentCapabilitiesOptions)
            _options = options
            _numberingOptionsProvider = New RichEditNumberingOptionsProvider(options.Numbering)
        End Sub

        Public ReadOnly Property Numbering As RichEditNumberingOptionsProvider
            Get
                Return _numberingOptionsProvider
            End Get
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Bookmarks As DocumentCapability
            Get
                Return _options.Bookmarks
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Bookmarks = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property CharacterFormatting As DocumentCapability
            Get
                Return _options.CharacterFormatting
            End Get

            Set(ByVal value As DocumentCapability)
                _options.CharacterFormatting = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property CharacterStyle As DocumentCapability
            Get
                Return _options.CharacterStyle
            End Get

            Set(ByVal value As DocumentCapability)
                _options.CharacterStyle = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Comments As DocumentCapability
            Get
                Return _options.Comments
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Comments = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Fields As DocumentCapability
            Get
                Return _options.Fields
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Fields = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property FloatingObjects As DocumentCapability
            Get
                Return _options.FloatingObjects
            End Get

            Set(ByVal value As DocumentCapability)
                _options.FloatingObjects = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property HeadersFooter As DocumentCapability
            Get
                Return _options.HeadersFooters
            End Get

            Set(ByVal value As DocumentCapability)
                _options.HeadersFooters = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Hyperlinks As DocumentCapability
            Get
                Return _options.Hyperlinks
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Hyperlinks = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property InlinePictures As DocumentCapability
            Get
                Return _options.InlinePictures
            End Get

            Set(ByVal value As DocumentCapability)
                _options.InlinePictures = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property InlineShapes As DocumentCapability
            Get
                Return _options.InlineShapes
            End Get

            Set(ByVal value As DocumentCapability)
                _options.InlineShapes = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property ParagraphFormatting As DocumentCapability
            Get
                Return _options.ParagraphFormatting
            End Get

            Set(ByVal value As DocumentCapability)
                _options.ParagraphFormatting = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Paragraphs As DocumentCapability
            Get
                Return _options.Paragraphs
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Paragraphs = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property ParagraphStyle As DocumentCapability
            Get
                Return _options.ParagraphStyle
            End Get

            Set(ByVal value As DocumentCapability)
                _options.ParagraphStyle = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property ParagraphTabs As DocumentCapability
            Get
                Return _options.ParagraphTabs
            End Get

            Set(ByVal value As DocumentCapability)
                _options.ParagraphTabs = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Sections As DocumentCapability
            Get
                Return _options.Sections
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Sections = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Tables As DocumentCapability
            Get
                Return _options.Tables
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Tables = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property TableStyle As DocumentCapability
            Get
                Return _options.TableStyle
            End Get

            Set(ByVal value As DocumentCapability)
                _options.TableStyle = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property TabSymbol As DocumentCapability
            Get
                Return _options.TabSymbol
            End Get

            Set(ByVal value As DocumentCapability)
                _options.TabSymbol = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Undo As DocumentCapability
            Get
                Return _options.Undo
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Undo = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property TrackChanges As DocumentCapability
            Get
                Return _options.TrackChanges
            End Get

            Set(ByVal value As DocumentCapability)
                _options.TrackChanges = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property ParagraphBorders As DocumentCapability
            Get
                Return _options.ParagraphBorders
            End Get

            Set(ByVal value As DocumentCapability)
                _options.ParagraphBorders = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Empty
        End Function
    End Class

    <TypeConverter(GetType(ExpandableObjectConverter))>
    Public Class RichEditNumberingOptionsProvider

        Private _options As NumberingOptions

        Public Sub New(ByVal options As NumberingOptions)
            _options = options
        End Sub

        <DefaultValue(DocumentCapability.Default)>
        Public Property Bulleted As DocumentCapability
            Get
                Return _options.Bulleted
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Bulleted = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property MultiLevel As DocumentCapability
            Get
                Return _options.MultiLevel
            End Get

            Set(ByVal value As DocumentCapability)
                _options.MultiLevel = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Simple As DocumentCapability
            Get
                Return _options.Simple
            End Get

            Set(ByVal value As DocumentCapability)
                _options.Simple = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Empty
        End Function
    End Class
End Namespace
