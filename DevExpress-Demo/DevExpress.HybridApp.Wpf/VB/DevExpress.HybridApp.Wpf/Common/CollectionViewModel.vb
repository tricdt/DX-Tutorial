Imports System
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.ViewModel
Imports DevExpress.Mvvm.DataModel

Namespace DevExpress.DevAV.Common

    ''' <summary>
    ''' The base class for a POCO view models exposing a collection of entities of a given type and CRUD operations against these entities.
    ''' This is a partial class that provides extension point to add custom properties, commands and override methods without modifying the auto-generated code.
    ''' </summary>
    ''' <typeparam name="TEntity">An entity type.</typeparam>
    ''' <typeparam name="TPrimaryKey">A primary key value type.</typeparam>
    ''' <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    Public Partial Class CollectionViewModel(Of TEntity As Class, TPrimaryKey, TUnitOfWork As IUnitOfWork)
        Inherits CollectionViewModelType(Of TEntity, TEntity, TPrimaryKey, TUnitOfWork)

        ''' <summary>
        ''' Creates a new instance of CollectionViewModel as a POCO view model.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        ''' <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        ''' <param name="projection">An optional parameter that provides a LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data.</param>
        ''' <param name="newEntityInitializer">An optional parameter that provides a function to initialize a new entity. This parameter is used in the detail collection view models when creating a single object view model for a new entity.</param>
        ''' <param name="canCreateNewEntity">A function that is called before an attempt to create a new entity is made. This parameter is used together with the newEntityInitializer parameter.</param>
        ''' <param name="ignoreSelectEntityMessage">An optional parameter that used to specify that the selected entity should not be managed by PeekCollectionViewModel.</param>
        Public Shared Function CreateCollectionViewModel(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TEntity, TPrimaryKey)), ByVal Optional projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TEntity)) = Nothing, ByVal Optional newEntityInitializer As Action(Of TEntity) = Nothing, ByVal Optional canCreateNewEntity As Func(Of Boolean) = Nothing, ByVal Optional ignoreSelectEntityMessage As Boolean = False, ByVal Optional unitOfWorkPolicy As UnitOfWorkPolicy = UnitOfWorkPolicy.Individual) As CollectionViewModel(Of TEntity, TPrimaryKey, TUnitOfWork)
            Return ViewModelSource.Create(Function() New CollectionViewModel(Of TEntity, TPrimaryKey, TUnitOfWork)(unitOfWorkFactory, getRepositoryFunc, projection, newEntityInitializer, canCreateNewEntity, ignoreSelectEntityMessage, unitOfWorkPolicy))
        End Function

        ''' <summary>
        ''' Initializes a new instance of the CollectionViewModel class.
        ''' This constructor is declared protected to avoid an undesired instantiation of the CollectionViewModel type without the POCO proxy factory.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        ''' <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        ''' <param name="projection">An optional parameter that provides a LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data.</param>
        ''' <param name="newEntityInitializer">An optional parameter that provides a function to initialize a new entity. This parameter is used in the detail collection view models when creating a single object view model for a new entity.</param>
        ''' <param name="canCreateNewEntity">A function that is called before an attempt to create a new entity is made. This parameter is used together with the newEntityInitializer parameter.</param>
        ''' <param name="ignoreSelectEntityMessage">An optional parameter that used to specify that the selected entity should not be managed by PeekCollectionViewModel.</param>
        Protected Sub New(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TEntity, TPrimaryKey)), ByVal Optional projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TEntity)) = Nothing, ByVal Optional newEntityInitializer As Action(Of TEntity) = Nothing, ByVal Optional canCreateNewEntity As Func(Of Boolean) = Nothing, ByVal Optional ignoreSelectEntityMessage As Boolean = False, ByVal Optional unitOfWorkPolicy As UnitOfWorkPolicy = UnitOfWorkPolicy.Individual)
            MyBase.New(unitOfWorkFactory, getRepositoryFunc, projection, newEntityInitializer, canCreateNewEntity, ignoreSelectEntityMessage, unitOfWorkPolicy)
        End Sub
    End Class

    ''' <summary>
    ''' The base class for a POCO view models exposing a collection of entities of a given type and CRUD operations against these entities.
    ''' This is a partial class that provides extension point to add custom properties, commands and override methods without modifying the auto-generated code.
    ''' </summary>
    ''' <typeparam name="TEntity">A repository entity type.</typeparam>
    ''' <typeparam name="TProjection">A projection entity type.</typeparam>
    ''' <typeparam name="TPrimaryKey">A primary key value type.</typeparam>
    ''' <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    Public Partial Class CollectionViewModelType(Of TEntity As Class, TProjection As Class, TPrimaryKey, TUnitOfWork As IUnitOfWork)
        Inherits CollectionViewModelBase(Of TEntity, TProjection, TPrimaryKey, TUnitOfWork)

        ''' <summary>
        ''' Creates a new instance of CollectionViewModel as a POCO view model.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        ''' <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        ''' <param name="projection">A LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data and/or for projecting data to a custom type that does not match the repository entity type.</param>
        ''' <param name="newEntityInitializer">An optional parameter that provides a function to initialize a new entity. This parameter is used in the detail collection view models when creating a single object view model for a new entity.</param>
        ''' <param name="canCreateNewEntity">A function that is called before an attempt to create a new entity is made. This parameter is used together with the newEntityInitializer parameter.</param>
        ''' <param name="ignoreSelectEntityMessage">An optional parameter that used to specify that the selected entity should not be managed by PeekCollectionViewModel.</param>
        Public Shared Function CreateProjectionCollectionViewModel(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TEntity, TPrimaryKey)), ByVal projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TProjection)), ByVal Optional newEntityInitializer As Action(Of TEntity) = Nothing, ByVal Optional canCreateNewEntity As Func(Of Boolean) = Nothing, ByVal Optional ignoreSelectEntityMessage As Boolean = False, ByVal Optional unitOfWorkPolicy As UnitOfWorkPolicy = UnitOfWorkPolicy.Individual) As CollectionViewModelType(Of TEntity, TProjection, TPrimaryKey, TUnitOfWork)
            Return ViewModelSource.Create(Function() New CollectionViewModelType(Of TEntity, TProjection, TPrimaryKey, TUnitOfWork)(unitOfWorkFactory, getRepositoryFunc, projection, newEntityInitializer, canCreateNewEntity, ignoreSelectEntityMessage, unitOfWorkPolicy))
        End Function

        ''' <summary>
        ''' Initializes a new instance of the CollectionViewModel class.
        ''' This constructor is declared protected to avoid an undesired instantiation of the CollectionViewModel type without the POCO proxy factory.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        ''' <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        ''' <param name="projection">A LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data and/or for projecting data to a custom type that does not match the repository entity type.</param>
        ''' <param name="newEntityInitializer">An optional parameter that provides a function to initialize a new entity. This parameter is used in the detail collection view models when creating a single object view model for a new entity.</param>
        ''' <param name="canCreateNewEntity">A function that is called before an attempt to create a new entity is made. This parameter is used together with the newEntityInitializer parameter.</param>
        ''' <param name="ignoreSelectEntityMessage">An optional parameter that used to specify that the selected entity should not be managed by PeekCollectionViewModel.</param>
        Protected Sub New(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TEntity, TPrimaryKey)), ByVal projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TProjection)), ByVal Optional newEntityInitializer As Action(Of TEntity) = Nothing, ByVal Optional canCreateNewEntity As Func(Of Boolean) = Nothing, ByVal Optional ignoreSelectEntityMessage As Boolean = False, ByVal Optional unitOfWorkPolicy As UnitOfWorkPolicy = UnitOfWorkPolicy.Individual)
            MyBase.New(unitOfWorkFactory, getRepositoryFunc, projection, newEntityInitializer, canCreateNewEntity, ignoreSelectEntityMessage, unitOfWorkPolicy)
        End Sub

        Public Overrides Sub Edit(ByVal projectionEntity As TProjection)
            MyBase.Edit(projectionEntity)
            Enumerable.Last(DocumentManagerService.Documents).DestroyOnClose = True
        End Sub

        Public Overrides Sub [New]()
            MyBase.[New]()
            Dim lastDocument = DocumentManagerService.Documents.LastOrDefault()
            If lastDocument IsNot Nothing Then
                lastDocument.DestroyOnClose = True
            End If
        End Sub
    End Class
End Namespace
