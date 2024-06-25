Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataModel
Imports DevExpress.Mvvm.ViewModel
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.DevAV.Common

Namespace DevExpress.DevAV.ViewModels

    ''' <summary>
    ''' Represents the single Customer object view model.
    ''' </summary>
    Public Partial Class CustomerViewModel
        Inherits SingleObjectViewModel(Of Customer, Long, IDevAVDbUnitOfWork)

        ''' <summary>
        ''' Creates a new instance of CustomerViewModel as a POCO view model.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Public Shared Function Create(ByVal Optional unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As CustomerViewModel
            Return ViewModelSource.Create(Function() New CustomerViewModel(unitOfWorkFactory))
        End Function

        ''' <summary>
        ''' Initializes a new instance of the CustomerViewModel class.
        ''' This constructor is declared protected to avoid undesired instantiation of the CustomerViewModel type without the POCO proxy factory.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Protected Sub New(ByVal Optional unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
            MyBase.New(If(unitOfWorkFactory, GetUnitOfWorkFactory()), Function(x) x.Customers, Function(x) x.Name)
        End Sub

        ''' <summary>
        ''' The view model that contains a look-up collection of CustomerEmployees for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpCustomerEmployees As IEntitiesViewModel(Of CustomerEmployee)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As CustomerViewModel) x.LookUpCustomerEmployees, getRepositoryFunc:=Function(x) x.CustomerEmployees)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of CustomerStores for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpCustomerStores As IEntitiesViewModel(Of CustomerStore)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As CustomerViewModel) x.LookUpCustomerStores, getRepositoryFunc:=Function(x) x.CustomerStores)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of Orders for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpOrders As IEntitiesViewModel(Of Order)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As CustomerViewModel) x.LookUpOrders, getRepositoryFunc:=Function(x) x.Orders)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of Quotes for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpQuotes As IEntitiesViewModel(Of Quote)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As CustomerViewModel) x.LookUpQuotes, getRepositoryFunc:=Function(x) x.Quotes)
            End Get
        End Property

        ''' <summary>
        ''' The view model for the CustomerEmployees detail collection.
        ''' </summary>
        Public ReadOnly Property CustomerEmployeesDetails As CollectionViewModelBase(Of CustomerEmployee, CustomerEmployee, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As CustomerViewModel) x.CustomerEmployeesDetails, getRepositoryFunc:=Function(x) x.CustomerEmployees, foreignKeyExpression:=Function(x) x.CustomerId, navigationExpression:=Function(x) x.Customer)
            End Get
        End Property

        ''' <summary>
        ''' The view model for the CustomerCustomerStores detail collection.
        ''' </summary>
        Public ReadOnly Property CustomerCustomerStoresDetails As CollectionViewModelBase(Of CustomerStore, CustomerStore, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As CustomerViewModel) x.CustomerCustomerStoresDetails, getRepositoryFunc:=Function(x) x.CustomerStores, foreignKeyExpression:=Function(x) x.CustomerId, navigationExpression:=Function(x) x.Customer)
            End Get
        End Property

        ''' <summary>
        ''' The view model for the CustomerOrders detail collection.
        ''' </summary>
        Public ReadOnly Property CustomerOrdersDetails As CollectionViewModelBase(Of Order, Order, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As CustomerViewModel) x.CustomerOrdersDetails, getRepositoryFunc:=Function(x) x.Orders, foreignKeyExpression:=Function(x) x.CustomerId, navigationExpression:=Function(x) x.Customer)
            End Get
        End Property

        ''' <summary>
        ''' The view model for the CustomerQuotes detail collection.
        ''' </summary>
        Public ReadOnly Property CustomerQuotesDetails As CollectionViewModelBase(Of Quote, Quote, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As CustomerViewModel) x.CustomerQuotesDetails, getRepositoryFunc:=Function(x) x.Quotes, foreignKeyExpression:=Function(x) x.CustomerId, navigationExpression:=Function(x) x.Customer)
            End Get
        End Property
    End Class
End Namespace
