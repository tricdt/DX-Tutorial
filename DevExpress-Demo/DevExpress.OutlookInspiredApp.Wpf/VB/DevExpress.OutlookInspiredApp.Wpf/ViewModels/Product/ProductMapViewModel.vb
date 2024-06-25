Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm
Imports DevExpress.DevAV.Common

Namespace DevExpress.DevAV.ViewModels

    Public Class ProductMapViewModel
        Inherits SingleObjectViewModel(Of Product, Long, IDevAVDbUnitOfWork)

        Private citiesMapViewModelField As CitiesMapViewModel

        Private statisticsViewModelField As ProductStatisticsViewModel

        Public Sub New()
            MyBase.New(GetUnitOfWorkFactory(), Function(x) x.Products)
        End Sub

        Protected Overrides Sub OnEntityChanged()
            MyBase.OnEntityChanged()
            RemoveHandler StatisticsViewModel.FilterTypeChanged, AddressOf StatisticsViewModel_FilterTypeChanged
            Dim addresses = New HashSet(Of Address)(GetSalesStores().[Select](Function(cs) cs.Address))
            CitiesMapViewModel.Cities = CreateCities(addresses)
            StatisticsViewModel.EntityId = PrimaryKey
            StatisticsViewModel.SelectedAddress = CitiesMapViewModel.Cities.FirstOrDefault()
            AddHandler StatisticsViewModel.FilterTypeChanged, AddressOf StatisticsViewModel_FilterTypeChanged
        End Sub

        Private Sub StatisticsViewModel_FilterTypeChanged(ByVal sender As Object, ByVal e As PeriodEventArgs)
            Dim addresses = New HashSet(Of Address)(GetSalesStores().[Select](Function(cs) cs.Address))
            CitiesMapViewModel.Cities = CreateCities(addresses, e.Period)
        End Sub

        Public Function GetSalesStores(ByVal Optional period As Period = Period.Lifetime) As IEnumerable(Of CustomerStore)
            Return QueriesHelper.GetSalesStoresForPeriod(UnitOfWork.Orders, period)
        End Function

        Private Function CreateCities(ByVal addresses As HashSet(Of Address), ByVal Optional period As Period = Period.Lifetime) As List(Of CityViewModel)
            Dim newCities = New List(Of CityViewModel)()
            For Each city In addresses.[Select](Function(a) CityViewModel.Create(a, UnitOfWork))
                city.Sales = GetSalesByCity(city.Address.City, period).GroupBy(Function(mi) mi.Customer).[Select](Function(gr) New SalesViewModel(gr.Key.Name, gr.CustomSum(Function(mi) mi.Total)))
                Dim address As String = city.Address.City
                If Not newCities.Any(Function(c) Equals(c.Address.City, address)) Then newCities.Add(city)
            Next

            Return newCities
        End Function

        Private Function GetSalesByCity(ByVal city As String, ByVal Optional period As Period = Period.Lifetime) As IEnumerable(Of MapItem)
            Return QueriesHelper.GetSaleMapItemsByCity(UnitOfWork.OrderItems, Entity.Id, city, period)
        End Function

        Public ReadOnly Property CitiesMapViewModel As CitiesMapViewModel
            Get
                If citiesMapViewModelField Is Nothing Then citiesMapViewModelField = CitiesMapViewModel.Create()
                Return citiesMapViewModelField
            End Get
        End Property

        Public ReadOnly Property StatisticsViewModel As ProductStatisticsViewModel
            Get
                If statisticsViewModelField Is Nothing Then statisticsViewModelField = ProductStatisticsViewModel.Create()
                Return statisticsViewModelField
            End Get
        End Property

        Public Overrides Sub Delete()
            Dim messageBoxService As IMessageBoxService = citiesMapViewModelField.GetRequiredService(Of IMessageBoxService)()
            messageBoxService.ShowMessage("To ensure data integrity, the Products module doesn't allow records to be deleted. Record deletion is supported by the Employees module.", "Delete Product", MessageButton.OK)
        End Sub

        Protected Overrides Function GetTitle() As String
            Return "Product Sales Map - DevAV"
        End Function
    End Class
End Namespace
