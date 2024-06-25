Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.Linq
Imports System.Windows.Data
Imports System.Windows.Threading
Imports DevExpress.Data
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Core

Namespace ProductsDemo.Modules

    Public Class ItemTypeToBooleanConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim itemType As String = TryCast(parameter, String)
            Dim newValue As String = TryCast(value, String)
            If Equals(itemType, Nothing) OrElse value Is Nothing Then Return False
            Return Equals(itemType, newValue)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            If CBool(value) Then Return parameter
            Return Binding.DoNothing
        End Function
    End Class

    Public Class ItemType

        Public Const None As String = "None"

        Public Const List As String = "List"

        Public Const ToDoListItem As String = "ToDoListItem"

        Public Const CompletedItem As String = "CompletedItem"

        Public Const TodayItem As String = "TodayItem"

        Public Const PrioritizedItem As String = "PrioritizedItem"

        Public Const OverdueItem As String = "OverdueItem"

        Public Const SimpleListItem As String = "SimpleListItem"

        Public Const DeferredItem As String = "DeferredItem"

        Public Const Print As String = "Print"

        Public Const Card As String = "Card"

        Public Const State As String = "State"

        Public Const Alphabet As String = "Alphabet"
    End Class

    Public MustInherit Class GridViewModelBase(Of T As Class)
        Inherits GridViewModelBaseType
        Implements ISupportNavigation

        Private _NavigationParameter As GridModuleNavigationParameter

        Public Overridable Property ItemsSource As ObservableCollection(Of T)

        Protected Overridable Sub OnItemsSourceChanging(ByVal newValue As ObservableCollection(Of T))
            SelectedItem = Nothing
            If ItemsSource IsNot Nothing Then RemoveHandler ItemsSource.CollectionChanged, AddressOf OnItemsSourceCollectionChanged
        End Sub

        Protected Overridable Sub OnItemsSourceChanged(ByVal oldValue As ObservableCollection(Of T))
            If ItemsSource IsNot Nothing Then AddHandler ItemsSource.CollectionChanged, AddressOf OnItemsSourceCollectionChanged
        End Sub

        Protected Overridable Sub OnItemsSourceCollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
        End Sub

        Protected NotOverridable Overrides Sub InitializeItemsSource()
            ItemsSource = New ObservableCollection(Of T)(GetItemsSource())
        End Sub

        Public Overridable Property SelectedItem As T

        Protected Overridable Sub OnSelectedItemChanged(ByVal oldValue As T)
            RaiseEvent SelectedItemChanged(Me, New ValueChangedEventArgs(Of T)(oldValue, SelectedItem))
        End Sub

        Protected MustOverride Function GetItemsSource() As List(Of T)

        Public Overridable Property CheckedItemType As String

        Protected Property NavigationParameter As GridModuleNavigationParameter
            Get
                Return _NavigationParameter
            End Get

            Private Set(ByVal value As GridModuleNavigationParameter)
                _NavigationParameter = value
            End Set
        End Property

        Public Event SelectedItemChanged As EventHandler(Of ValueChangedEventArgs(Of T))

        Protected Overridable Sub OnNavigatedFrom()
        End Sub

        Protected Overridable Sub OnNavigatedTo()
            If NavigationParameter = GridModuleNavigationParameter.NewItem Then Call Dispatcher.CurrentDispatcher.BeginInvoke(New Action(AddressOf ShowNewItemWindow))
        End Sub

        Public MustOverride Sub ShowNewItemWindow()

#Region "ISupportNavigation"
        Private Sub ISupportNavigation_OnNavigatedFrom() Implements ISupportNavigation.OnNavigatedFrom
            OnNavigatedFrom()
        End Sub

        Private Sub ISupportNavigation_OnNavigatedTo() Implements ISupportNavigation.OnNavigatedTo
            OnNavigatedTo()
        End Sub

        Private Property Parameter As Object Implements ISupportParameter.Parameter
            Get
                Return NavigationParameter
            End Get

            Set(ByVal value As Object)
                NavigationParameter = If(value Is Nothing, GridModuleNavigationParameter.Default, CType(value, GridModuleNavigationParameter))
            End Set
        End Property
#End Region
    End Class

    Public Enum GridModuleNavigationParameter
        [Default]
        NewItem
    End Enum

    <POCOViewModel>
    Public MustInherit Class GridViewModelBaseType

        Private _Columns As List(Of ProductsDemo.Modules.GridColumnModel)

        Public Sub New()
            InitializeData()
            InitializeItemsSource()
            InitializeColumns()
            InitializeDefaultView()
        End Sub

        Protected MustOverride Function GetColumns() As List(Of GridColumnModel)

        Protected MustOverride Sub InitializeItemsSource()

        Protected Overridable Sub InitializeData()
        End Sub

        Private Sub InitializeColumns()
            Columns = GetColumns()
        End Sub

        Protected Overridable Sub GroupBy(ByVal fieldName As String)
            Enumerable.First(Columns, Function(c) Equals(c.Name, fieldName)).IsGrouped = True
        End Sub

        Protected Sub SortBy(ByVal fieldName As String, ByVal order As ColumnSortOrder)
            Enumerable.First(Columns, Function(c) Equals(c.Name, fieldName)).SortOrder = order
        End Sub

        Protected Sub ClearModel()
            Ungroup()
            ClearSorting()
            FilterString = Nothing
            BeginUpdate()
            AssignIndexes()
            EndUpdate()
        End Sub

        Private Sub AssignIndexes()
            For i As Integer = 0 To Columns.Count - 1
                Columns(i).Index = i
            Next
        End Sub

        Private Sub Ungroup()
            For Each column In Columns
                column.IsGrouped = False
            Next
        End Sub

        Private Sub ClearSorting()
            For Each column In Columns
                column.SortOrder = ColumnSortOrder.None
            Next
        End Sub

        Protected Overridable Sub InitializeDefaultView()
        End Sub

        Public Property Columns As List(Of GridColumnModel)
            Get
                Return _Columns
            End Get

            Private Set(ByVal value As List(Of GridColumnModel))
                _Columns = value
            End Set
        End Property

        Public Overridable Property FilterString As String

        <Command(Name:="PrintCommand")>
        Public Sub DoPrint()
            RaiseEvent Print(Me, EventArgs.Empty)
        End Sub

        Protected Sub BeginUpdate()
            RaiseEvent IsLoadingChanged(Me, New IsLoadingEventArgs() With {.IsLoading = True})
        End Sub

        Protected Sub EndUpdate()
            RaiseEvent IsLoadingChanged(Me, New IsLoadingEventArgs() With {.IsLoading = False})
        End Sub

        Public Event IsLoadingChanged As EventHandler(Of IsLoadingEventArgs)

        Public Event Print As EventHandler
    End Class

    Public Class IsLoadingEventArgs
        Inherits EventArgs

        Public Property IsLoading As Boolean
    End Class
End Namespace
