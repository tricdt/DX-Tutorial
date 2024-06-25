Imports System
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.ViewModel
Imports DevExpress.Mvvm.DataModel

Namespace DevExpress.DevAV.Common

    Public Partial Class InstantFeedbackCollectionViewModel(Of TEntity As {Class, New}, TPrimaryKey, TUnitOfWork As IUnitOfWork)
        Inherits InstantFeedbackCollectionViewModelBase(Of TEntity, TEntity, TPrimaryKey, TUnitOfWork)

        Public Shared Function CreateInstantFeedbackCollectionViewModel(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TEntity, TPrimaryKey)), ByVal Optional projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TEntity)) = Nothing, ByVal Optional canCreateNewEntity As Func(Of Boolean) = Nothing) As InstantFeedbackCollectionViewModel(Of TEntity, TPrimaryKey, TUnitOfWork)
            Return ViewModelSource.Create(Function() New InstantFeedbackCollectionViewModel(Of TEntity, TPrimaryKey, TUnitOfWork)(unitOfWorkFactory, getRepositoryFunc, projection, canCreateNewEntity))
        End Function

        Protected Sub New(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TEntity, TPrimaryKey)), ByVal Optional projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TEntity)) = Nothing, ByVal Optional canCreateNewEntity As Func(Of Boolean) = Nothing)
            MyBase.New(unitOfWorkFactory, getRepositoryFunc, projection, canCreateNewEntity)
        End Sub
    End Class
End Namespace
