Imports DevExpress.Xpf.Grid

Namespace GridDemo

    Public Class ColumnDescription

        Private _PropertyName As String, _DisplayName As String

        Public Sub New(ByVal propertyName As String, ByVal Optional displayName As String = Nothing, ByVal Optional styleKey As String = Nothing)
            Me.PropertyName = propertyName
            Me.DisplayName = If(displayName, propertyName)
            Me.StyleKey = styleKey
        End Sub

        Public Property PropertyName As String
            Get
                Return _PropertyName
            End Get

            Private Set(ByVal value As String)
                _PropertyName = value
            End Set
        End Property

        Public Property DisplayName As String
            Get
                Return _DisplayName
            End Get

            Private Set(ByVal value As String)
                _DisplayName = value
            End Set
        End Property

        Public Property StyleKey As String
    End Class

    Public Class BandDescription

        Private _DisplayName As String, _Columns As GridDemo.ColumnDescription()

        Public Sub New(ByVal displayName As String, ByVal columns As ColumnDescription(), ByVal Optional _fixed As FixedStyle = FixedStyle.None, ByVal Optional overlayByChildren As Boolean = False)
            Me.DisplayName = displayName
            Me.Columns = columns
            Fixed = _fixed
            OverlayHeaderByChildren = overlayByChildren
        End Sub

        Public Property DisplayName As String
            Get
                Return _DisplayName
            End Get

            Private Set(ByVal value As String)
                _DisplayName = value
            End Set
        End Property

        Public Property Columns As ColumnDescription()
            Get
                Return _Columns
            End Get

            Private Set(ByVal value As ColumnDescription())
                _Columns = value
            End Set
        End Property

        Public Property Fixed As FixedStyle

        Public Property OverlayHeaderByChildren As Boolean
    End Class
End Namespace
