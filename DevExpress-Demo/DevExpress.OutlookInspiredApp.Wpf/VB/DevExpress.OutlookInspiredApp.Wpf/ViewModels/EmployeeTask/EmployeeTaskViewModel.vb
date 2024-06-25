Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataModel
Imports DevExpress.Mvvm.ViewModel
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.DevAV.Common

Namespace DevExpress.DevAV.ViewModels

    ''' <summary>
    ''' Represents the single EmployeeTask object view model.
    ''' </summary>
    Public Partial Class EmployeeTaskViewModel
        Inherits SingleObjectViewModel(Of EmployeeTask, Long, IDevAVDbUnitOfWork)

        ''' <summary>
        ''' Creates a new instance of EmployeeTaskViewModel as a POCO view model.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Public Shared Function Create(ByVal Optional unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As EmployeeTaskViewModel
            Return ViewModelSource.Create(Function() New EmployeeTaskViewModel(unitOfWorkFactory))
        End Function

        ''' <summary>
        ''' Initializes a new instance of the EmployeeTaskViewModel class.
        ''' This constructor is declared protected to avoid undesired instantiation of the EmployeeTaskViewModel type without the POCO proxy factory.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Protected Sub New(ByVal Optional unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
            MyBase.New(If(unitOfWorkFactory, GetUnitOfWorkFactory()), Function(x) x.Tasks, Function(x) x.Subject)
        End Sub

        Protected Overrides Sub RefreshLookUpCollections(ByVal raisePropertyChanged As Boolean)
            MyBase.RefreshLookUpCollections(raisePropertyChanged)
            AssignedEmployeesDetailEntities = CreateAddRemoveDetailEntitiesViewModel(Function(x) x.Employees, Function(x) x.AssignedEmployees)
        End Sub

        ''' <summary>
        ''' The view model that contains a look-up collection of AttachedFiles for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpAttachedFiles As IEntitiesViewModel(Of TaskAttachedFile)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As EmployeeTaskViewModel) x.LookUpAttachedFiles, getRepositoryFunc:=Function(x) x.AttachedFiles)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of Employees for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpEmployees As IEntitiesViewModel(Of Employee)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As EmployeeTaskViewModel) x.LookUpEmployees, getRepositoryFunc:=Function(x) x.Employees)
            End Get
        End Property

        ''' <summary>
        ''' The view model that contains a look-up collection of CustomerEmployees for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpCustomerEmployees As IEntitiesViewModel(Of CustomerEmployee)
            Get
                Return GetLookUpEntitiesViewModel(propertyExpression:=Function(ByVal x As EmployeeTaskViewModel) x.LookUpCustomerEmployees, getRepositoryFunc:=Function(x) x.CustomerEmployees)
            End Get
        End Property

        Public Overridable Property AssignedEmployeesDetailEntities As AddRemoveDetailEntitiesViewModel(Of EmployeeTask, Long, Employee, Long, IDevAVDbUnitOfWork)

        ''' <summary>
        ''' The view model for the EmployeeTaskAttachedFiles detail collection.
        ''' </summary>
        Public ReadOnly Property EmployeeTaskAttachedFilesDetails As CollectionViewModelBase(Of TaskAttachedFile, TaskAttachedFile, Long, IDevAVDbUnitOfWork)
            Get
                Return GetDetailsCollectionViewModel(propertyExpression:=Function(ByVal x As EmployeeTaskViewModel) x.EmployeeTaskAttachedFilesDetails, getRepositoryFunc:=Function(x) x.AttachedFiles, foreignKeyExpression:=Function(x) x.EmployeeTaskId, navigationExpression:=Function(x) x.EmployeeTask)
            End Get
        End Property
    End Class
End Namespace
