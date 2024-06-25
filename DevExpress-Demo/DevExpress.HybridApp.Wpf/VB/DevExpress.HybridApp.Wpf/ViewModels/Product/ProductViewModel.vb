Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataModel
Imports DevExpress.Mvvm.ViewModel
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.DevAV.Common

Namespace DevExpress.DevAV.ViewModels

    ''' <summary>
    ''' Represents the single Product object view model.
    ''' </summary>
    Public Partial Class ProductViewModel
        Inherits SingleObjectViewModel(Of Product, Long, IDevAVDbUnitOfWork)

        ''' <summary>
        ''' Creates a new instance of ProductViewModel as a POCO view model.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Public Shared Function Create(ByVal Optional unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As ProductViewModel
            Return ViewModelSource.Create(Function() New ProductViewModel(unitOfWorkFactory))
        End Function

        ''' <summary>
        ''' Initializes a new instance of the ProductViewModel class.
        ''' This constructor is declared protected to avoid undesired instantiation of the ProductViewModel type without the POCO proxy factory.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Protected Sub New(ByVal Optional unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
            MyBase.New(If(unitOfWorkFactory, GetUnitOfWorkFactory()), Function(x) x.Products, Function(x) x.Name)
        End Sub

        ''' <summary>
        ''' The view model that contains a look-up collection of OrderItems for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpOrderItems As IEntitiesViewModel(Of OrderItem)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As ProductViewModel) x.LookUpOrderItems, getRepositoryFunc:=Function(x) x.OrderItems)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of Employees for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpEmployees As IEntitiesViewModel(Of Employee)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As ProductViewModel) x.LookUpEmployees, getRepositoryFunc:=Function(x) x.Employees)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of Pictures for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpPictures As IEntitiesViewModel(Of Picture)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As ProductViewModel) x.LookUpPictures, getRepositoryFunc:=Function(x) x.Pictures)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of ProductCatalogs for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpProductCatalogs As IEntitiesViewModel(Of ProductCatalog)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As ProductViewModel) x.LookUpProductCatalogs, getRepositoryFunc:=Function(x) x.ProductCatalogs)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of ProductImages for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpProductImages As IEntitiesViewModel(Of ProductImage)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As ProductViewModel) x.LookUpProductImages, getRepositoryFunc:=Function(x) x.ProductImages)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of QuoteItems for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpQuoteItems As IEntitiesViewModel(Of QuoteItem)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As ProductViewModel) x.LookUpQuoteItems, getRepositoryFunc:=Function(x) x.QuoteItems)
            End Get
        End Property

        ''' <summary>
        ''' The view model for the ProductOrderItems detail collection.
        ''' </summary>
        Public ReadOnly Property ProductOrderItemsDetails As CollectionViewModelBase(Of OrderItem, OrderItem, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As ProductViewModel) x.ProductOrderItemsDetails, getRepositoryFunc:=Function(x) x.OrderItems, foreignKeyExpression:=Function(x) x.ProductId, navigationExpression:=Function(x) x.Product)
            End Get
        End Property

        ''' <summary>
        ''' The view model for the ProductCatalog detail collection.
        ''' </summary>
        Public ReadOnly Property ProductCatalogDetails As CollectionViewModelBase(Of ProductCatalog, ProductCatalog, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As ProductViewModel) x.ProductCatalogDetails, getRepositoryFunc:=Function(x) x.ProductCatalogs, foreignKeyExpression:=Function(x) x.ProductId, navigationExpression:=Function(x) x.Product)
            End Get
        End Property

        ''' <summary>
        ''' The view model for the ProductImages detail collection.
        ''' </summary>
        Public ReadOnly Property ProductImagesDetails As CollectionViewModelBase(Of ProductImage, ProductImage, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As ProductViewModel) x.ProductImagesDetails, getRepositoryFunc:=Function(x) x.ProductImages, foreignKeyExpression:=Function(x) x.ProductId, navigationExpression:=Function(x) x.Product)
            End Get
        End Property

        ''' <summary>
        ''' The view model for the ProductQuoteItems detail collection.
        ''' </summary>
        Public ReadOnly Property ProductQuoteItemsDetails As CollectionViewModelBase(Of QuoteItem, QuoteItem, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As ProductViewModel) x.ProductQuoteItemsDetails, getRepositoryFunc:=Function(x) x.QuoteItems, foreignKeyExpression:=Function(x) x.ProductId, navigationExpression:=Function(x) x.Product)
            End Get
        End Property
    End Class
End Namespace
