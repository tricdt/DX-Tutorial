Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataModel
Imports DevExpress.Mvvm.ViewModel
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.DevAV.Common

Namespace DevExpress.DevAV.ViewModels

    ''' <summary>
    ''' Represents the Employees collection view model.
    ''' </summary>
    Public Partial Class EmployeeCollectionViewModel
        Inherits CollectionViewModel(Of Employee, Long, IDevAVDbUnitOfWork)

        ''' <summary>
        ''' Creates a new instance of EmployeeCollectionViewModel as a POCO view model.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Public Shared Function Create(ByVal Optional unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing, ByVal Optional unitOfWorkPolicy As UnitOfWorkPolicy = UnitOfWorkPolicy.Individual) As EmployeeCollectionViewModel
            Return ViewModelSource.Create(Function() New EmployeeCollectionViewModel(unitOfWorkFactory, unitOfWorkPolicy))
        End Function

        ''' <summary>
        ''' Initializes a new instance of the EmployeeCollectionViewModel class.
        ''' This constructor is declared protected to avoid undesired instantiation of the EmployeeCollectionViewModel type without the POCO proxy factory.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Protected Sub New(ByVal Optional unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing, ByVal Optional unitOfWorkPolicy As UnitOfWorkPolicy = UnitOfWorkPolicy.Individual)
            MyBase.New(If(unitOfWorkFactory, GetUnitOfWorkFactory()), Function(x) x.Employees, unitOfWorkPolicy:=unitOfWorkPolicy)
        End Sub
    End Class
End Namespace
