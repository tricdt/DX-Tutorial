Imports DevExpress.Data
Imports DevExpress.Utils
Imports DevExpress.Mvvm
Imports DevExpress.XtraGrid
Imports System
Imports DevExpress.Xpf.Grid

Namespace ProductsDemo.Modules

    Public Class GridColumnModel
        Inherits BindableBase

        Public Property Name As String

        Public Property DisplayName As String

        Private isGroupedField As Boolean

        Public Property IsGrouped As Boolean
            Get
                Return isGroupedField
            End Get

            Set(ByVal value As Boolean)
                If isGroupedField = value Then Return
                isGroupedField = value
                RaisePropertiesChanged("IsGrouped")
            End Set
        End Property

        Private widthField As GridColumnWidth = Double.NaN

        Public Property Width As GridColumnWidth
            Get
                Return widthField
            End Get

            Set(ByVal value As GridColumnWidth)
                widthField = value
                RaisePropertiesChanged("Width")
            End Set
        End Property

        Public Property HorizontalHeaderContentAlignment As Windows.HorizontalAlignment

        Private allowFilteringField As DefaultBoolean = DefaultBoolean.True

        Public Property AllowFiltering As DefaultBoolean
            Get
                Return allowFilteringField
            End Get

            Set(ByVal value As DefaultBoolean)
                allowFilteringField = value
            End Set
        End Property

        Private allowSortingField As DefaultBoolean = DefaultBoolean.True

        Public Property AllowSorting As DefaultBoolean
            Get
                Return allowSortingField
            End Get

            Set(ByVal value As DefaultBoolean)
                allowSortingField = value
            End Set
        End Property

        Public Property AllowEditing As DefaultBoolean

        Public Property GroupInterval As ColumnGroupInterval

        Public Property FixedWidth As Boolean

        Public Property Mask As String

        Private sortOrderField As ColumnSortOrder

        Public Property SortOrder As ColumnSortOrder
            Get
                Return sortOrderField
            End Get

            Set(ByVal value As ColumnSortOrder)
                sortOrderField = value
                RaisePropertiesChanged("SortOrder")
            End Set
        End Property

        Private indexField As Integer

        Public Property Index As Integer
            Get
                Return indexField
            End Get

            Set(ByVal value As Integer)
                indexField = value
                RaisePropertiesChanged("Index")
            End Set
        End Property

        Private isVisibleField As Boolean = True

        Public Property IsVisible As Boolean
            Get
                Return isVisibleField
            End Get

            Set(ByVal value As Boolean)
                isVisibleField = value
                RaisePropertiesChanged("IsVisible")
            End Set
        End Property
    End Class
End Namespace
