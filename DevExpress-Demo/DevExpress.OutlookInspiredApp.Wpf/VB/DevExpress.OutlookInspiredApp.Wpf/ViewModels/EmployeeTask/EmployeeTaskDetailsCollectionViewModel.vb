Imports System.Linq
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Utils
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.DevAV.Common
Imports DevExpress.Mvvm.DataModel
Imports DevExpress.Mvvm.ViewModel

Namespace DevExpress.DevAV.ViewModels

    Public Class EmployeeTaskDetailsCollectionViewModel
        Inherits CollectionViewModel(Of EmployeeTask, Long, IDevAVDbUnitOfWork)

        Public Shared Function Create(ByVal Optional unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As EmployeeTaskDetailsCollectionViewModel
            Return ViewModelSource.Create(Function() New EmployeeTaskDetailsCollectionViewModel(unitOfWorkFactory))
        End Function

        Protected Sub New(ByVal Optional unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
            MyBase.New(If(unitOfWorkFactory, GetUnitOfWorkFactory()), Function(x) x.Tasks, Function(query) query.OrderByDescending(Function(x) x.DueDate))
        End Sub

        Public ReadOnly Property AssignTaskService As IDocumentManagerService
            Get
                Return GetService(Of IDocumentManagerService)("EmployeeAssignService")
            End Get
        End Property

        Public Overrides Sub Edit(ByVal projectionEntity As EmployeeTask)
            MyBase.Edit(projectionEntity)
            UpdateFilter()
        End Sub

        Public Overrides Sub Delete(ByVal projectionEntity As EmployeeTask)
            MyBase.Delete(projectionEntity)
            UpdateFilter()
        End Sub

        Public Shared Property LastSelectedEmployeeId As Long?

        Public Overrides Sub [New]()
            LastSelectedEmployeeId = GetParentViewModel(Of EmployeeViewModel)().Entity.Id
            DocumentManagerService.ShowNewEntityDocument(Of EmployeeTask)(Me)
            LastSelectedEmployeeId = Nothing
            UpdateFilter()
        End Sub

        Public Overridable Sub AssignTask(ByVal task As EmployeeTask)
            Dim document As IDocument = AssignTaskService.CreateDocument("EmployeeAssignView", task.Id, Me)
            If document IsNot Nothing Then document.Show()
            UpdateFilter()
        End Sub

        Public Overridable Function CanAssignTask(ByVal task As EmployeeTask) As Boolean
            Return Not IsLoading AndAlso task IsNot Nothing
        End Function

        Public Sub UpdateFilter()
            Dim viewModel = GetParentViewModel(Of EmployeeViewModel)()
            If viewModel.Entity Is Nothing Then Return
            Dim unitOfWork = UnitOfWorkFactory.CreateUnitOfWork()
            Dim actualEntity = unitOfWork.Employees.FirstOrDefault(Function(x) x.Id = viewModel.Entity.Id)
            FilterExpression = CriteriaOperatorToExpressionConverter.GetGenericWhere(Of EmployeeTask)(CriteriaOperator.Parse(EmployeeTaskCollectionViewModel.TasksToFilterCriteriaConverter(If(actualEntity Is Nothing, Nothing, actualEntity.AssignedEmployeeTasks))))
        End Sub
    End Class
End Namespace
