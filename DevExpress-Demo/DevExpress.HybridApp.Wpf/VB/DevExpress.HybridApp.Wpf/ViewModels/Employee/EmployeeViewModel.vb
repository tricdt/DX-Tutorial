Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataModel
Imports DevExpress.Mvvm.ViewModel
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.DevAV.Common

Namespace DevExpress.DevAV.ViewModels

    ''' <summary>
    ''' Represents the single Employee object view model.
    ''' </summary>
    Public Partial Class EmployeeViewModel
        Inherits SingleObjectViewModel(Of Employee, Long, IDevAVDbUnitOfWork)

        ''' <summary>
        ''' Creates a new instance of EmployeeViewModel as a POCO view model.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Public Shared Function Create(ByVal Optional unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As EmployeeViewModel
            Return ViewModelSource.Create(Function() New EmployeeViewModel(unitOfWorkFactory))
        End Function

        ''' <summary>
        ''' Initializes a new instance of the EmployeeViewModel class.
        ''' This constructor is declared protected to avoid undesired instantiation of the EmployeeViewModel type without the POCO proxy factory.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Protected Sub New(ByVal Optional unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
            MyBase.New(If(unitOfWorkFactory, GetUnitOfWorkFactory()), Function(x) x.Employees, Function(x) x.FullName)
        End Sub

        Protected Overrides Sub RefreshLookUpCollections(ByVal raisePropertyChanged As Boolean)
            MyBase.RefreshLookUpCollections(raisePropertyChanged)
            AssignedEmployeeTasksDetailEntities = CreateAddRemoveDetailEntitiesViewModel(Function(x) x.Tasks, Function(x) x.AssignedEmployeeTasks)
        End Sub

        ''' <summary>
        ''' The view model that contains a look-up collection of Tasks for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpTasks As IEntitiesViewModel(Of EmployeeTask)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As EmployeeViewModel) x.LookUpTasks, getRepositoryFunc:=Function(x) x.Tasks)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of Pictures for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpPictures As IEntitiesViewModel(Of Picture)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As EmployeeViewModel) x.LookUpPictures, getRepositoryFunc:=Function(x) x.Pictures)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of Probations for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpProbations As IEntitiesViewModel(Of Probation)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As EmployeeViewModel) x.LookUpProbations, getRepositoryFunc:=Function(x) x.Probations)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of Communications for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpCommunications As IEntitiesViewModel(Of CustomerCommunication)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As EmployeeViewModel) x.LookUpCommunications, getRepositoryFunc:=Function(x) x.Communications)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of Orders for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpOrders As IEntitiesViewModel(Of Order)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As EmployeeViewModel) x.LookUpOrders, getRepositoryFunc:=Function(x) x.Orders)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of Products for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpProducts As IEntitiesViewModel(Of Product)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As EmployeeViewModel) x.LookUpProducts, getRepositoryFunc:=Function(x) x.Products)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of Quotes for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpQuotes As IEntitiesViewModel(Of Quote)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As EmployeeViewModel) x.LookUpQuotes, getRepositoryFunc:=Function(x) x.Quotes)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of Evaluations for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpEvaluations As IEntitiesViewModel(Of Evaluation)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As EmployeeViewModel) x.LookUpEvaluations, getRepositoryFunc:=Function(x) x.Evaluations)
            End Get
        End Property

        Public Overridable Property AssignedEmployeeTasksDetailEntities As AddRemoveDetailEntitiesViewModel(Of Employee, Long, EmployeeTask, Long, IDevAVDbUnitOfWork)

        ''' <summary>
        ''' The view model for the EmployeeAssignedTasks detail collection.
        ''' </summary>
        Public ReadOnly Property EmployeeAssignedTasksDetails As CollectionViewModelBase(Of EmployeeTask, EmployeeTask, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As EmployeeViewModel) x.EmployeeAssignedTasksDetails, getRepositoryFunc:=Function(x) x.Tasks, foreignKeyExpression:=Function(x) x.AssignedEmployeeId, navigationExpression:=Function(x) x.AssignedEmployee)
            End Get
        End Property

        ''' <summary>
        ''' The view model for the EmployeeOwnedTasks detail collection.
        ''' </summary>
        Public ReadOnly Property EmployeeOwnedTasksDetails As CollectionViewModelBase(Of EmployeeTask, EmployeeTask, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As EmployeeViewModel) x.EmployeeOwnedTasksDetails, getRepositoryFunc:=Function(x) x.Tasks, foreignKeyExpression:=Function(x) x.OwnerId, navigationExpression:=Function(x) x.Owner)
            End Get
        End Property

        ''' <summary>
        ''' The view model for the EmployeeEmployees detail collection.
        ''' </summary>
        Public ReadOnly Property EmployeeEmployeesDetails As CollectionViewModelBase(Of CustomerCommunication, CustomerCommunication, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As EmployeeViewModel) x.EmployeeEmployeesDetails, getRepositoryFunc:=Function(x) x.Communications, foreignKeyExpression:=Function(x) x.EmployeeId, navigationExpression:=Function(x) x.Employee)
            End Get
        End Property

        ''' <summary>
        ''' The view model for the EmployeeOrders detail collection.
        ''' </summary>
        Public ReadOnly Property EmployeeOrdersDetails As CollectionViewModelBase(Of Order, Order, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As EmployeeViewModel) x.EmployeeOrdersDetails, getRepositoryFunc:=Function(x) x.Orders, foreignKeyExpression:=Function(x) x.EmployeeId, navigationExpression:=Function(x) x.Employee)
            End Get
        End Property

        ''' <summary>
        ''' The view model for the EmployeeProducts detail collection.
        ''' </summary>
        Public ReadOnly Property EmployeeProductsDetails As CollectionViewModelBase(Of Product, Product, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As EmployeeViewModel) x.EmployeeProductsDetails, getRepositoryFunc:=Function(x) x.Products, foreignKeyExpression:=Function(x) x.EngineerId, navigationExpression:=Function(x) x.Engineer)
            End Get
        End Property

        ''' <summary>
        ''' The view model for the EmployeeSupportedProducts detail collection.
        ''' </summary>
        Public ReadOnly Property EmployeeSupportedProductsDetails As CollectionViewModelBase(Of Product, Product, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As EmployeeViewModel) x.EmployeeSupportedProductsDetails, getRepositoryFunc:=Function(x) x.Products, foreignKeyExpression:=Function(x) x.SupportId, navigationExpression:=Function(x) x.Support)
            End Get
        End Property

        ''' <summary>
        ''' The view model for the EmployeeQuotes detail collection.
        ''' </summary>
        Public ReadOnly Property EmployeeQuotesDetails As CollectionViewModelBase(Of Quote, Quote, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As EmployeeViewModel) x.EmployeeQuotesDetails, getRepositoryFunc:=Function(x) x.Quotes, foreignKeyExpression:=Function(x) x.EmployeeId, navigationExpression:=Function(x) x.Employee)
            End Get
        End Property

        ''' <summary>
        ''' The view model for the EmployeeEvaluationsCreatedBy detail collection.
        ''' </summary>
        Public ReadOnly Property EmployeeEvaluationsCreatedByDetails As CollectionViewModelBase(Of Evaluation, Evaluation, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As EmployeeViewModel) x.EmployeeEvaluationsCreatedByDetails, getRepositoryFunc:=Function(x) x.Evaluations, foreignKeyExpression:=Function(x) x.CreatedById, navigationExpression:=Function(x) x.CreatedBy)
            End Get
        End Property

        ''' <summary>
        ''' The view model for the EmployeeEvaluations detail collection.
        ''' </summary>
        Public ReadOnly Property EmployeeEvaluationsDetails As CollectionViewModelBase(Of Evaluation, Evaluation, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As EmployeeViewModel) x.EmployeeEvaluationsDetails, getRepositoryFunc:=Function(x) x.Evaluations, foreignKeyExpression:=Function(x) x.EmployeeId, navigationExpression:=Function(x) x.Employee)
            End Get
        End Property
    End Class
End Namespace
