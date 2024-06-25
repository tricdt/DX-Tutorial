Imports System
Imports System.Linq
Imports System.ComponentModel
Imports DevExpress.Mvvm.ViewModel
Imports DevExpress.Mvvm.DataModel

Namespace DevExpress.DevAV.Common

    ''' <summary>
    ''' The base class for POCO view models exposing a collection of entities of the given type.
    ''' This is a partial class that provides an extension point to add custom properties, commands and override methods without modifying the auto-generated code.
    ''' </summary>
    ''' <typeparam name="TEntity">A repository entity type.</typeparam>
    ''' <typeparam name="TProjection">A projection entity type.</typeparam>
    ''' <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    Public MustInherit Partial Class EntitiesViewModel(Of TEntity As Class, TProjection As Class, TUnitOfWork As IUnitOfWork)
        Inherits EntitiesViewModelBase(Of TEntity, TProjection, TUnitOfWork)

        ''' <summary>
        ''' Initializes a new instance of the EntitiesViewModel class.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        ''' <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        ''' <param name="projection">A LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data and/or for projecting data to a custom type that does not match the repository entity type.</param>
        Protected Sub New(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IReadOnlyRepository(Of TEntity)), ByVal projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TProjection)), ByVal unitOfWorkPolicy As UnitOfWorkPolicy)
            MyBase.New(unitOfWorkFactory, getRepositoryFunc, projection, unitOfWorkPolicy)
        End Sub
    End Class
End Namespace
