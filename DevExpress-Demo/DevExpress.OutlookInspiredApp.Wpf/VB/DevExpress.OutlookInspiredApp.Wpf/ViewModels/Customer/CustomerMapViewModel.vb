Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm
Imports DevExpress.DevAV.Common

Namespace DevExpress.DevAV.ViewModels

    Public Class CustomerMapViewModel
        Inherits SingleObjectViewModel(Of Customer, Long, IDevAVDbUnitOfWork)

        Private statisticsViewModelField As CustomerStatisticsViewModel

        Private citiesMapViewModelField As CitiesMapViewModel

        Public Sub New()
            MyBase.New(GetUnitOfWorkFactory(), Function(x) x.Customers)
        End Sub

        Protected Overrides Sub OnEntityChanged()
            MyBase.OnEntityChanged()
            RemoveHandler StatisticsViewModel.FilterTypeChanged, AddressOf StatisticsViewModel_FilterTypeChanged
            Dim addresses = New HashSet(Of Address)(QueriesHelper.GetDistinctStoresForPeriod(UnitOfWork.Orders, PrimaryKey).Where(Function(x) x IsNot Nothing).[Select](Function(cs) cs.Address))
            CitiesMapViewModel.Cities = CreateCities(addresses)
            StatisticsViewModel.EntityId = PrimaryKey
            StatisticsViewModel.SelectedAddress = CitiesMapViewModel.Cities.FirstOrDefault()
            AddHandler StatisticsViewModel.FilterTypeChanged, AddressOf StatisticsViewModel_FilterTypeChanged
        End Sub

        Private Sub StatisticsViewModel_FilterTypeChanged(ByVal sender As Object, ByVal e As PeriodEventArgs)
            Dim addresses = New HashSet(Of Address)(QueriesHelper.GetDistinctStoresForPeriod(UnitOfWork.Orders, PrimaryKey).Where(Function(x) x IsNot Nothing).[Select](Function(cs) cs.Address))
            CitiesMapViewModel.Cities = CreateCities(addresses, e.Period)
        End Sub

        Private Function CreateCities(ByVal addresses As HashSet(Of Address), ByVal Optional period As Period = Period.Lifetime) As List(Of CityViewModel)
            Dim newCities = New List(Of CityViewModel)()
            For Each city In addresses.[Select](Function(a) CityViewModel.Create(a, UnitOfWork))
                city.Sales = QueriesHelper.GetSaleMapItemsByCustomerAndCity(UnitOfWork.OrderItems, PrimaryKey, city.Address.City, period).GroupBy(Function(mi) mi.Product).[Select](Function(gr) New SalesViewModel(gr.Key.Name, gr.CustomSum(Function(mi) mi.Total)))
                Dim address As String = city.Address.City
                If Not newCities.Any(Function(c) Equals(c.Address.City, address)) Then newCities.Add(city)
            Next

            Return newCities
        End Function

        Public ReadOnly Property CitiesMapViewModel As CitiesMapViewModel
            Get
                If citiesMapViewModelField Is Nothing Then citiesMapViewModelField = CitiesMapViewModel.Create()
                Return citiesMapViewModelField
            End Get
        End Property

        Public ReadOnly Property StatisticsViewModel As CustomerStatisticsViewModel
            Get
                If statisticsViewModelField Is Nothing Then statisticsViewModelField = CustomerStatisticsViewModel.Create()
                Return statisticsViewModelField
            End Get
        End Property

        Public Overrides Sub Delete()
            Dim messageBoxService As IMessageBoxService = citiesMapViewModelField.GetRequiredService(Of IMessageBoxService)()
            messageBoxService.ShowMessage("To ensure data integrity, the Customers module doesn't allow records to be deleted. Record deletion is supported by the Employees module.", "Delete Customer", MessageButton.OK)
        End Sub

        Protected Overrides Function GetTitle() As String
            Return "Customer Sales Map - DevAV"
        End Function
    End Class
End Namespace
