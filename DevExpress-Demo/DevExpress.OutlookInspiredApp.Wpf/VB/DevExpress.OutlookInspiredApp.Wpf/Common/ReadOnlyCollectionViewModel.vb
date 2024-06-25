Imports System
Imports System.Linq
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.ViewModel
Imports DevExpress.Mvvm.DataModel

Namespace DevExpress.DevAV.Common

    ''' <summary>
    ''' The base class for POCO view models exposing a read-only collection of entities of a given type.
    ''' This is a partial class that provides the extension point to add custom properties, commands and override methods without modifying the auto-generated code.
    ''' </summary>
    ''' <typeparam name="TEntity">An entity type.</typeparam>
    ''' <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    Public Partial Class ReadOnlyCollectionViewModel(Of TEntity As Class, TUnitOfWork As IUnitOfWork)
        Inherits ReadOnlyCollectionViewModelType(Of TEntity, TEntity, TUnitOfWork)

        ''' <summary>
        ''' Creates a new instance of ReadOnlyCollectionViewModel as a POCO view model.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        ''' <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        ''' <param name="projection">An optional parameter that provides a LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data.</param>
        Public Shared Function CreateReadOnlyCollectionViewModel(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IReadOnlyRepository(Of TEntity)), ByVal Optional projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TEntity)) = Nothing, ByVal Optional unitOfWorkPolicy As UnitOfWorkPolicy = UnitOfWorkPolicy.Individual) As ReadOnlyCollectionViewModel(Of TEntity, TUnitOfWork)
            Return ViewModelSource.Create(Function() New ReadOnlyCollectionViewModel(Of TEntity, TUnitOfWork)(unitOfWorkFactory, getRepositoryFunc, projection, unitOfWorkPolicy))
        End Function

        ''' <summary>
        ''' Initializes a new instance of the ReadOnlyCollectionViewModel class.
        ''' This constructor is declared protected to avoid an undesired instantiation of the PeekCollectionViewModel type without the POCO proxy factory.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        ''' <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        ''' <param name="projection">An optional parameter that provides a LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data.</param>
        Protected Sub New(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IReadOnlyRepository(Of TEntity)), ByVal Optional projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TEntity)) = Nothing, ByVal Optional unitOfWorkPolicy As UnitOfWorkPolicy = UnitOfWorkPolicy.Individual)
            MyBase.New(unitOfWorkFactory, getRepositoryFunc, projection, unitOfWorkPolicy)
        End Sub
    End Class

    ''' <summary>
    ''' The base class for POCO view models exposing a read-only collection of entities of a given type.
    ''' This is a partial class that provides the extension point to add custom properties, commands and override methods without modifying the auto-generated code.
    ''' </summary>
    ''' <typeparam name="TEntity">A repository entity type.</typeparam>
    ''' <typeparam name="TProjection">A projection entity type.</typeparam>
    ''' <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    Public Partial Class ReadOnlyCollectionViewModelType(Of TEntity As Class, TProjection As Class, TUnitOfWork As IUnitOfWork)
        Inherits ReadOnlyCollectionViewModelBase(Of TEntity, TProjection, TUnitOfWork)

        ''' <summary>
        ''' Creates a new instance of ReadOnlyCollectionViewModel as a POCO view model.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        ''' <param name="getRepositoryFunc">A function that returns the repository representing entities of a given type.</param>
        ''' <param name="projection">A LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data and/or for projecting data to a custom type that does not match the repository entity type.</param>
        Public Shared Function CreateReadOnlyProjectionCollectionViewModel(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IReadOnlyRepository(Of TEntity)), ByVal projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TProjection)), ByVal Optional unitOfWorkPolicy As UnitOfWorkPolicy = UnitOfWorkPolicy.Individual) As ReadOnlyCollectionViewModelType(Of TEntity, TProjection, TUnitOfWork)
            Return ViewModelSource.Create(Function() New ReadOnlyCollectionViewModelType(Of TEntity, TProjection, TUnitOfWork)(unitOfWorkFactory, getRepositoryFunc, projection, unitOfWorkPolicy))
        End Function

        ''' <summary>
        ''' Initializes a new instance of the ReadOnlyCollectionViewModel class.
        ''' This constructor is declared protected to avoid an undesired instantiation of the PeekCollectionViewModel type without the POCO proxy factory.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        ''' <param name="getRepositoryFunc">A function that returns the repository representing entities of a given type.</param>
        ''' <param name="projection">A LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data and/or for projecting data to a custom type that does not match the repository entity type.</param>
        Protected Sub New(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IReadOnlyRepository(Of TEntity)), ByVal projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TProjection)), ByVal Optional unitOfWorkPolicy As UnitOfWorkPolicy = UnitOfWorkPolicy.Individual)
            MyBase.New(unitOfWorkFactory, getRepositoryFunc, projection, unitOfWorkPolicy)
        End Sub
    End Class
End Namespace
