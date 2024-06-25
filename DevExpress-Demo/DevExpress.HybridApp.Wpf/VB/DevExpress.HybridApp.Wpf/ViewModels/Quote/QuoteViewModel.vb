Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataModel
Imports DevExpress.Mvvm.ViewModel
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.DevAV.Common

Namespace DevExpress.DevAV.ViewModels

    ''' <summary>
    ''' Represents the single Quote object view model.
    ''' </summary>
    Public Partial Class QuoteViewModel
        Inherits SingleObjectViewModel(Of Quote, Long, IDevAVDbUnitOfWork)

        ''' <summary>
        ''' Creates a new instance of QuoteViewModel as a POCO view model.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Public Shared Function Create(ByVal Optional unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As QuoteViewModel
            Return ViewModelSource.Create(Function() New QuoteViewModel(unitOfWorkFactory))
        End Function

        ''' <summary>
        ''' Initializes a new instance of the QuoteViewModel class.
        ''' This constructor is declared protected to avoid undesired instantiation of the QuoteViewModel type without the POCO proxy factory.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Protected Sub New(ByVal Optional unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
            MyBase.New(If(unitOfWorkFactory, GetUnitOfWorkFactory()), Function(x) x.Quotes, Function(x) x.Number)
        End Sub

        ''' <summary>
        ''' The view model that contains a look-up collection of QuoteItems for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpQuoteItems As IEntitiesViewModel(Of QuoteItem)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As QuoteViewModel) x.LookUpQuoteItems, getRepositoryFunc:=Function(x) x.QuoteItems)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of Customers for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpCustomers As IEntitiesViewModel(Of Customer)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As QuoteViewModel) x.LookUpCustomers, getRepositoryFunc:=Function(x) x.Customers)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of CustomerStores for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpCustomerStores As IEntitiesViewModel(Of CustomerStore)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As QuoteViewModel) x.LookUpCustomerStores, getRepositoryFunc:=Function(x) x.CustomerStores)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of Employees for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpEmployees As IEntitiesViewModel(Of Employee)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As QuoteViewModel) x.LookUpEmployees, getRepositoryFunc:=Function(x) x.Employees)
            End Get
        End Property

        ''' <summary>
        ''' The view model for the QuoteQuoteItems detail collection.
        ''' </summary>
        Public ReadOnly Property QuoteQuoteItemsDetails As CollectionViewModelBase(Of QuoteItem, QuoteItem, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As QuoteViewModel) x.QuoteQuoteItemsDetails, getRepositoryFunc:=Function(x) x.QuoteItems, foreignKeyExpression:=Function(x) x.QuoteId, navigationExpression:=Function(x) x.Quote)
            End Get
        End Property
    End Class
End Namespace
