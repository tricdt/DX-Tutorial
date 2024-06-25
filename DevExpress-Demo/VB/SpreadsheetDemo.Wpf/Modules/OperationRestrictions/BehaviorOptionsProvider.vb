Imports DevExpress.Xpf.Spreadsheet
Imports System.ComponentModel
Imports DocumentCapability = DevExpress.XtraSpreadsheet.DocumentCapability
Imports MoveActiveCellModeOnEnterPress = DevExpress.XtraSpreadsheet.MoveActiveCellModeOnEnterPress

Namespace DevExpress.Spreadsheet.Demos

    Public Class BehaviorOptionsProvider

        Private ReadOnly options As SpreadsheetBehaviorOptions

        Private ReadOnly clipboardOptionsProvider As SpreadsheetBehaviorClipboardOptionsProvider

        Private ReadOnly commonOptionsProvider As SpreadsheetBehaviorCommonOptionsProvider

        Private ReadOnly zoomOptionsProvider As SpreadsheetBehaviorZoomOptionsProvider

        Private ReadOnly worksheetOptionsProvider As SpreadsheetBehaviorWorksheetOptionsProvider

        Private ReadOnly columnOptionsProvider As SpreadsheetBehaviorColumnOptionsProvider

        Private ReadOnly rowOptionsProvider As SpreadsheetBehaviorRowOptionsProvider

        Private ReadOnly commentOptionsProvider As SpreadsheetBehaviorCommentOptionsProvider

        Private ReadOnly groupOptionsProvider As SpreadsheetBehaviorGroupOptionsProvider

        Private ReadOnly selectionOptionsProvider As SpreadsheetBehaviorSelectionOptionsProvider

        Private ReadOnly drawingOptionsProvider As SpreadsheetBehaviorDrawingOptionsProvider

        Private ReadOnly miscellaneousOptionsProvider As SpreadsheetBehaviorMiscellaneousOptionsProvider

        Public Sub New(ByVal options As SpreadsheetBehaviorOptions)
            Me.options = options
            clipboardOptionsProvider = New SpreadsheetBehaviorClipboardOptionsProvider(options)
            commonOptionsProvider = New SpreadsheetBehaviorCommonOptionsProvider(options)
            zoomOptionsProvider = New SpreadsheetBehaviorZoomOptionsProvider(options)
            worksheetOptionsProvider = New SpreadsheetBehaviorWorksheetOptionsProvider(options.Worksheet)
            columnOptionsProvider = New SpreadsheetBehaviorColumnOptionsProvider(options.Column)
            rowOptionsProvider = New SpreadsheetBehaviorRowOptionsProvider(options.Row)
            commentOptionsProvider = New SpreadsheetBehaviorCommentOptionsProvider(options.Comment)
            groupOptionsProvider = New SpreadsheetBehaviorGroupOptionsProvider(options.Group)
            selectionOptionsProvider = New SpreadsheetBehaviorSelectionOptionsProvider(options.Selection)
            drawingOptionsProvider = New SpreadsheetBehaviorDrawingOptionsProvider(options.Drawing)
            miscellaneousOptionsProvider = New SpreadsheetBehaviorMiscellaneousOptionsProvider(options)
        End Sub

        Public ReadOnly Property Clipboard As SpreadsheetBehaviorClipboardOptionsProvider
            Get
                Return clipboardOptionsProvider
            End Get
        End Property

        Public ReadOnly Property Common As SpreadsheetBehaviorCommonOptionsProvider
            Get
                Return commonOptionsProvider
            End Get
        End Property

        Public ReadOnly Property Zoom As SpreadsheetBehaviorZoomOptionsProvider
            Get
                Return zoomOptionsProvider
            End Get
        End Property

        Public ReadOnly Property Worksheet As SpreadsheetBehaviorWorksheetOptionsProvider
            Get
                Return worksheetOptionsProvider
            End Get
        End Property

        Public ReadOnly Property Column As SpreadsheetBehaviorColumnOptionsProvider
            Get
                Return columnOptionsProvider
            End Get
        End Property

        Public ReadOnly Property Row As SpreadsheetBehaviorRowOptionsProvider
            Get
                Return rowOptionsProvider
            End Get
        End Property

        Public ReadOnly Property Comment As SpreadsheetBehaviorCommentOptionsProvider
            Get
                Return commentOptionsProvider
            End Get
        End Property

        Public ReadOnly Property Group As SpreadsheetBehaviorGroupOptionsProvider
            Get
                Return groupOptionsProvider
            End Get
        End Property

        Public ReadOnly Property Selection As SpreadsheetBehaviorSelectionOptionsProvider
            Get
                Return selectionOptionsProvider
            End Get
        End Property

        Public ReadOnly Property Drawing As SpreadsheetBehaviorDrawingOptionsProvider
            Get
                Return drawingOptionsProvider
            End Get
        End Property

        Public ReadOnly Property Miscellaneous As SpreadsheetBehaviorMiscellaneousOptionsProvider
            Get
                Return miscellaneousOptionsProvider
            End Get
        End Property
    End Class

    <TypeConverter(GetType(ExpandableObjectConverter))>
    Public Class SpreadsheetBehaviorClipboardOptionsProvider

        Private ReadOnly options As SpreadsheetBehaviorOptions

        Public Sub New(ByVal options As SpreadsheetBehaviorOptions)
            Me.options = options
        End Sub

        <DefaultValue(DocumentCapability.Default)>
        Public Property Cut As DocumentCapability
            Get
                Return options.Cut
            End Get

            Set(ByVal value As DocumentCapability)
                options.Cut = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Copy As DocumentCapability
            Get
                Return options.Copy
            End Get

            Set(ByVal value As DocumentCapability)
                options.Copy = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Paste As DocumentCapability
            Get
                Return options.Paste
            End Get

            Set(ByVal value As DocumentCapability)
                options.Paste = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Empty
        End Function
    End Class

    <TypeConverter(GetType(ExpandableObjectConverter))>
    Public Class SpreadsheetBehaviorCommonOptionsProvider

        Private ReadOnly options As SpreadsheetBehaviorOptions

        Public Sub New(ByVal options As SpreadsheetBehaviorOptions)
            Me.options = options
        End Sub

        <DefaultValue(DocumentCapability.Default)>
        Public Property Open As DocumentCapability
            Get
                Return options.Open
            End Get

            Set(ByVal value As DocumentCapability)
                options.Open = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Save As DocumentCapability
            Get
                Return options.Save
            End Get

            Set(ByVal value As DocumentCapability)
                options.Save = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property SaveAs As DocumentCapability
            Get
                Return options.SaveAs
            End Get

            Set(ByVal value As DocumentCapability)
                options.SaveAs = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Print As DocumentCapability
            Get
                Return options.Print
            End Get

            Set(ByVal value As DocumentCapability)
                options.Print = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property CreateNew As DocumentCapability
            Get
                Return options.CreateNew
            End Get

            Set(ByVal value As DocumentCapability)
                options.CreateNew = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Empty
        End Function
    End Class

    <TypeConverter(GetType(ExpandableObjectConverter))>
    Public Class SpreadsheetBehaviorZoomOptionsProvider

        Public Const MinZoomFactorDefault As Single = 0.5F

        Public Const MaxZoomFactorDefault As Single = 3

        Private ReadOnly options As SpreadsheetBehaviorOptions

        Public Sub New(ByVal options As SpreadsheetBehaviorOptions)
            Me.options = options
            MinZoomFactor = MinZoomFactorDefault
            MaxZoomFactor = MaxZoomFactorDefault
        End Sub

        <DefaultValue(DocumentCapability.Default)>
        Public Property Zoom As DocumentCapability
            Get
                Return options.Zoom
            End Get

            Set(ByVal value As DocumentCapability)
                options.Zoom = value
            End Set
        End Property

        <DefaultValue(MinZoomFactorDefault)>
        Public Property MinZoomFactor As Single
            Get
                Return options.MinZoomFactor
            End Get

            Set(ByVal value As Single)
                options.MinZoomFactor = value
            End Set
        End Property

        <DefaultValue(MaxZoomFactorDefault)>
        Public Property MaxZoomFactor As Single
            Get
                Return options.MaxZoomFactor
            End Get

            Set(ByVal value As Single)
                options.MaxZoomFactor = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Empty
        End Function
    End Class

    <TypeConverter(GetType(ExpandableObjectConverter))>
    Public Class SpreadsheetBehaviorWorksheetOptionsProvider

        Private ReadOnly options As SpreadsheetWorksheetBehaviorOptions

        Public Sub New(ByVal options As SpreadsheetWorksheetBehaviorOptions)
            Me.options = options
        End Sub

        <DefaultValue(DocumentCapability.Default)>
        Public Property Delete As DocumentCapability
            Get
                Return options.Delete
            End Get

            Set(ByVal value As DocumentCapability)
                options.Delete = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Hide As DocumentCapability
            Get
                Return options.Hide
            End Get

            Set(ByVal value As DocumentCapability)
                options.Hide = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Insert As DocumentCapability
            Get
                Return options.Insert
            End Get

            Set(ByVal value As DocumentCapability)
                options.Insert = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Rename As DocumentCapability
            Get
                Return options.Rename
            End Get

            Set(ByVal value As DocumentCapability)
                options.Rename = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property TabColor As DocumentCapability
            Get
                Return options.TabColor
            End Get

            Set(ByVal value As DocumentCapability)
                options.TabColor = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Unhide As DocumentCapability
            Get
                Return options.Unhide
            End Get

            Set(ByVal value As DocumentCapability)
                options.Unhide = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Empty
        End Function
    End Class

    <TypeConverter(GetType(ExpandableObjectConverter))>
    Public Class SpreadsheetBehaviorColumnOptionsProvider

        Private ReadOnly options As SpreadsheetColumnBehaviorOptions

        Public Sub New(ByVal options As SpreadsheetColumnBehaviorOptions)
            Me.options = options
        End Sub

        <DefaultValue(DocumentCapability.Default)>
        Public Property AutoFit As DocumentCapability
            Get
                Return options.AutoFit
            End Get

            Set(ByVal value As DocumentCapability)
                options.AutoFit = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Delete As DocumentCapability
            Get
                Return options.Delete
            End Get

            Set(ByVal value As DocumentCapability)
                options.Delete = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Hide As DocumentCapability
            Get
                Return options.Hide
            End Get

            Set(ByVal value As DocumentCapability)
                options.Hide = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Insert As DocumentCapability
            Get
                Return options.Insert
            End Get

            Set(ByVal value As DocumentCapability)
                options.Insert = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Resize As DocumentCapability
            Get
                Return options.Resize
            End Get

            Set(ByVal value As DocumentCapability)
                options.Resize = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Unhide As DocumentCapability
            Get
                Return options.Unhide
            End Get

            Set(ByVal value As DocumentCapability)
                options.Unhide = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Empty
        End Function
    End Class

    <TypeConverter(GetType(ExpandableObjectConverter))>
    Public Class SpreadsheetBehaviorRowOptionsProvider

        Private ReadOnly options As SpreadsheetRowBehaviorOptions

        Public Sub New(ByVal options As SpreadsheetRowBehaviorOptions)
            Me.options = options
        End Sub

        <DefaultValue(DocumentCapability.Default)>
        Public Property AutoFit As DocumentCapability
            Get
                Return options.AutoFit
            End Get

            Set(ByVal value As DocumentCapability)
                options.AutoFit = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Delete As DocumentCapability
            Get
                Return options.Delete
            End Get

            Set(ByVal value As DocumentCapability)
                options.Delete = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Hide As DocumentCapability
            Get
                Return options.Hide
            End Get

            Set(ByVal value As DocumentCapability)
                options.Hide = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Insert As DocumentCapability
            Get
                Return options.Insert
            End Get

            Set(ByVal value As DocumentCapability)
                options.Insert = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Resize As DocumentCapability
            Get
                Return options.Resize
            End Get

            Set(ByVal value As DocumentCapability)
                options.Resize = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Unhide As DocumentCapability
            Get
                Return options.Unhide
            End Get

            Set(ByVal value As DocumentCapability)
                options.Unhide = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Empty
        End Function
    End Class

    <TypeConverter(GetType(ExpandableObjectConverter))>
    Public Class SpreadsheetBehaviorCommentOptionsProvider

        Private ReadOnly options As SpreadsheetCommentBehaviorOptions

        Public Sub New(ByVal options As SpreadsheetCommentBehaviorOptions)
            Me.options = options
        End Sub

        <DefaultValue(DocumentCapability.Default)>
        Public Property Delete As DocumentCapability
            Get
                Return options.Delete
            End Get

            Set(ByVal value As DocumentCapability)
                options.Delete = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Edit As DocumentCapability
            Get
                Return options.Edit
            End Get

            Set(ByVal value As DocumentCapability)
                options.Edit = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Insert As DocumentCapability
            Get
                Return options.Insert
            End Get

            Set(ByVal value As DocumentCapability)
                options.Insert = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Move As DocumentCapability
            Get
                Return options.Move
            End Get

            Set(ByVal value As DocumentCapability)
                options.Move = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Resize As DocumentCapability
            Get
                Return options.Resize
            End Get

            Set(ByVal value As DocumentCapability)
                options.Resize = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property ShowHide As DocumentCapability
            Get
                Return options.ShowHide
            End Get

            Set(ByVal value As DocumentCapability)
                options.ShowHide = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Empty
        End Function
    End Class

    <TypeConverter(GetType(ExpandableObjectConverter))>
    Public Class SpreadsheetBehaviorGroupOptionsProvider

        Private ReadOnly options As SpreadsheetGroupBehaviorOptions

        Public Sub New(ByVal options As SpreadsheetGroupBehaviorOptions)
            Me.options = options
        End Sub

        <DefaultValue(DocumentCapability.Default)>
        Public Property ChangeSettings As DocumentCapability
            Get
                Return options.ChangeSettings
            End Get

            Set(ByVal value As DocumentCapability)
                options.ChangeSettings = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Collapse As DocumentCapability
            Get
                Return options.Collapse
            End Get

            Set(ByVal value As DocumentCapability)
                options.Collapse = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Expand As DocumentCapability
            Get
                Return options.Expand
            End Get

            Set(ByVal value As DocumentCapability)
                options.Expand = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Group As DocumentCapability
            Get
                Return options.Group
            End Get

            Set(ByVal value As DocumentCapability)
                options.Group = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Ungroup As DocumentCapability
            Get
                Return options.Ungroup
            End Get

            Set(ByVal value As DocumentCapability)
                options.Ungroup = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Empty
        End Function
    End Class

    <TypeConverter(GetType(ExpandableObjectConverter))>
    Public Class SpreadsheetBehaviorSelectionOptionsProvider

        Private ReadOnly options As SpreadsheetSelectionBehaviorOptions

        Public Sub New(ByVal options As SpreadsheetSelectionBehaviorOptions)
            Me.options = options
        End Sub

        <DefaultValue(True)>
        Public Property AllowMultiSelection As Boolean
            Get
                Return options.AllowMultiSelection
            End Get

            Set(ByVal value As Boolean)
                options.AllowMultiSelection = value
            End Set
        End Property

        <DefaultValue(MoveActiveCellModeOnEnterPress.Down)>
        Public Property MoveActiveCellMode As MoveActiveCellModeOnEnterPress
            Get
                Return options.MoveActiveCellMode
            End Get

            Set(ByVal value As MoveActiveCellModeOnEnterPress)
                options.MoveActiveCellMode = value
            End Set
        End Property

        <DefaultValue(XtraSpreadsheet.ShowSelectionMode.Always)>
        Public Property ShowSelectionMode As XtraSpreadsheet.ShowSelectionMode
            Get
                Return options.ShowSelectionMode
            End Get

            Set(ByVal value As XtraSpreadsheet.ShowSelectionMode)
                options.ShowSelectionMode = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Empty
        End Function
    End Class

    <TypeConverter(GetType(ExpandableObjectConverter))>
    Public Class SpreadsheetBehaviorDrawingOptionsProvider

        Private ReadOnly options As SpreadsheetDrawingBehaviorOptions

        Public Sub New(ByVal options As SpreadsheetDrawingBehaviorOptions)
            Me.options = options
        End Sub

        <DefaultValue(DocumentCapability.Default)>
        Public Property ChangeZOrder As DocumentCapability
            Get
                Return options.ChangeZOrder
            End Get

            Set(ByVal value As DocumentCapability)
                options.ChangeZOrder = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Move As DocumentCapability
            Get
                Return options.Move
            End Get

            Set(ByVal value As DocumentCapability)
                options.Move = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Resize As DocumentCapability
            Get
                Return options.Resize
            End Get

            Set(ByVal value As DocumentCapability)
                options.Resize = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Rotate As DocumentCapability
            Get
                Return options.Rotate
            End Get

            Set(ByVal value As DocumentCapability)
                options.Rotate = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Empty
        End Function
    End Class

    <TypeConverter(GetType(ExpandableObjectConverter))>
    Public Class SpreadsheetBehaviorMiscellaneousOptionsProvider

        Private ReadOnly options As SpreadsheetBehaviorOptions

        Public Sub New(ByVal options As SpreadsheetBehaviorOptions)
            Me.options = options
        End Sub

        <DefaultValue(XtraSpreadsheet.CellEditorCommitMode.Auto)>
        Public Property CellEditorCommitMode As XtraSpreadsheet.CellEditorCommitMode
            Get
                Return options.CellEditorCommitMode
            End Get

            Set(ByVal value As XtraSpreadsheet.CellEditorCommitMode)
                options.CellEditorCommitMode = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property FreezePanes As DocumentCapability
            Get
                Return options.FreezePanes
            End Get

            Set(ByVal value As DocumentCapability)
                options.FreezePanes = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property ShowPopupMenu As DocumentCapability
            Get
                Return options.ShowPopupMenu
            End Get

            Set(ByVal value As DocumentCapability)
                options.ShowPopupMenu = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Touch As DocumentCapability
            Get
                Return options.Touch
            End Get

            Set(ByVal value As DocumentCapability)
                options.Touch = value
            End Set
        End Property

        <DefaultValue(XtraSpreadsheet.FunctionNameCulture.Auto)>
        Public Property FunctionNameCulture As XtraSpreadsheet.FunctionNameCulture
            Get
                Return options.FunctionNameCulture
            End Get

            Set(ByVal value As XtraSpreadsheet.FunctionNameCulture)
                options.FunctionNameCulture = value
            End Set
        End Property

        <DefaultValue(True)>
        Public Property FillHandleEnabled As Boolean
            Get
                Return options.FillHandleEnabled
            End Get

            Set(ByVal value As Boolean)
                options.FillHandleEnabled = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Drag As DocumentCapability
            Get
                Return options.Drag
            End Get

            Set(ByVal value As DocumentCapability)
                options.Drag = value
            End Set
        End Property

        <DefaultValue(DocumentCapability.Default)>
        Public Property Drop As DocumentCapability
            Get
                Return options.Drop
            End Get

            Set(ByVal value As DocumentCapability)
                options.Drop = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Empty
        End Function
    End Class
End Namespace
