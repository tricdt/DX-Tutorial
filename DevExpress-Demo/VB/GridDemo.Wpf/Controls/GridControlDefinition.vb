Imports DevExpress.Xpf.Editors.Settings
Imports DevExpress.Xpf.Grid
Imports System.Collections.Generic
Imports System.Windows

Namespace GridDemo

    Public Class GridControlDefinitionCollection
        Inherits List(Of GridControlDefinition)

    End Class

    Public Class GridControlDefinition
        Inherits DependencyObject

        Public Shared ReadOnly DataSourceProperty As DependencyProperty = DependencyProperty.Register("DataSource", GetType(Object), GetType(GridControlDefinition), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property DataSource As Object
            Get
                Return GetValue(DataSourceProperty)
            End Get

            Set(ByVal value As Object)
                SetValue(DataSourceProperty, value)
            End Set
        End Property

        Public Property Name As String

        Private columnsField As List(Of GridColumnDefinition)

        Public ReadOnly Property Columns As List(Of GridColumnDefinition)
            Get
                Return columnsField
            End Get
        End Property

        Public Sub New()
            columnsField = New List(Of GridColumnDefinition)()
        End Sub
    End Class

    Public Class GridColumnDefinition

        Public Property FieldName As String

        Public Property CellTemplate As DataTemplate

        Public Property Width As GridColumnWidth

        Public Property FixedWidth As Boolean

        Public Property EditSettings As BaseEditSettings

        Public Property Header As Object

        Public Property [ReadOnly] As Boolean

        Public Sub New()
            Width = Double.NaN
        End Sub
    End Class
End Namespace
