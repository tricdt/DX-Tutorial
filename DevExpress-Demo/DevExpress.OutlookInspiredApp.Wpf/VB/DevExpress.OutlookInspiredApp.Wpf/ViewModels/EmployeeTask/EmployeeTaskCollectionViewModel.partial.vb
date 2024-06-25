Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Windows.Media
Imports DevExpress.Data
Imports DevExpress.Data.Filtering
Imports DevExpress.DevAV.Common.Utils
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataModel
Imports DevExpress.Mvvm.ViewModel
Imports DevExpress.Mvvm.Localization

Namespace DevExpress.DevAV.ViewModels

    Partial Class EmployeeTaskCollectionViewModel
        Implements DevExpress.DevAV.ViewModels.ISupportFiltering(Of DevExpress.DevAV.EmployeeTask), DevExpress.DevAV.ViewModels.IFilterTreeViewModelContainer(Of DevExpress.DevAV.EmployeeTask, Long)

        Protected Overridable ReadOnly Property OpenFileDialogService As IOpenFileDialogService
            Get
                Return Nothing
            End Get
        End Property

        Public Sub ShowPrintPreview()
            Dim printModel = DevExpress.Mvvm.POCO.POCOViewModelExtensions.GetRequiredService(Of DevExpress.Mvvm.IPrintableControlPreviewService)(Me).GetPreview()
        End Sub

        Private entityPanelViewModelField As DevExpress.DevAV.ViewModels.EmployeeTaskDetailViewModel

        Public ReadOnly Property EntityPanelViewModel As EmployeeTaskDetailViewModel
            Get
                If Me.entityPanelViewModelField Is Nothing Then Me.entityPanelViewModelField = DevExpress.DevAV.ViewModels.EmployeeTaskDetailViewModel.Create()
                Return Me.entityPanelViewModelField
            End Get
        End Property

        Protected Overrides Sub OnInitializeInRuntime()
            MyBase.OnInitializeInRuntime()
            Me.SelectedEntities = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.DevAV.EmployeeTask)()
            Me.FollowUpSelector = New System.Collections.ObjectModel.ObservableCollection(Of Boolean)() From {False, False, False, False, False, False}
            Me.PrioritySelector = New System.Collections.ObjectModel.ObservableCollection(Of Boolean)() From {False, False}
        End Sub

        Public Overrides Sub OnLoaded()
            If Me.FilterTreeViewModel Is Nothing Then Return
            Me.FilterTreeViewModel.AllowFiltersContextMenu = False
            Me.RefreshEmployeeFilters()
        End Sub

        Public Sub ChangeTaskView(ByVal parameter As String)
            Dim viewKind As DevExpress.DevAV.ViewModels.TasksViewKind
            System.[Enum].TryParse(parameter, viewKind)
            If viewKind = DevExpress.DevAV.ViewModels.TasksViewKind.Active Then Me.FilterTreeViewModel.SelectedItem = Me.FilterTreeViewModel.StaticFilters(2)
            If viewKind <> Me.ViewKind AndAlso Me.ViewKind = DevExpress.DevAV.ViewModels.TasksViewKind.Active AndAlso Me.FilterTreeViewModel.SelectedItem Is Me.FilterTreeViewModel.StaticFilters(2) Then Me.FilterTreeViewModel.SelectedItem = Me.FilterTreeViewModel.StaticFilters(0)
            Me.ViewKind = viewKind
            Me.EntityPanelViewModel.ZoomEditEnabled =(Me.ViewKind = DevExpress.DevAV.ViewModels.TasksViewKind.Detailed)
            Call DevExpress.DevAV.Logger.Log(String.Format("OutlookInspiredApp: Change Task View: {0}", Me.ViewKind.ToString()))
        End Sub

        Public Overrides Sub [New]()
            Me.DocumentManagerService.ShowNewEntityDocument(Of DevExpress.DevAV.EmployeeTask)(Me)
            Me.Refresh()
            Me.UpdateEntityPanelViewModel()
            Me.RefreshEmployeeFilters()
        End Sub

        Public Overrides Sub Edit(ByVal entity As DevExpress.DevAV.EmployeeTask)
            MyBase.Edit(entity)
            Me.Refresh()
            Me.UpdateEntityPanelViewModel()
            Me.RefreshEmployeeFilters()
        End Sub

        Public Overrides Function CanEdit(ByVal projectionEntity As DevExpress.DevAV.EmployeeTask) As Boolean
            Return MyBase.CanEdit(projectionEntity) AndAlso (Me.SelectedEntities.Count = 1)
        End Function

        Public Overrides Sub Delete(ByVal projectionEntity As DevExpress.DevAV.EmployeeTask)
            If Me.MessageBoxService.ShowMessage(String.Format(DevExpress.Mvvm.Localization.ScaffoldingLocalizer.GetString(DevExpress.Mvvm.Localization.ScaffoldingStringId.Confirmation_Delete), Me.EntityDisplayName), DevExpress.Mvvm.Localization.ScaffoldingLocalizer.GetString(DevExpress.Mvvm.Localization.ScaffoldingStringId.Confirmation_Caption), DevExpress.Mvvm.MessageButton.YesNo) <> DevExpress.Mvvm.MessageResult.Yes Then Return
            Try
                Do
                    Dim task As DevExpress.DevAV.EmployeeTask = Me.SelectedEntities.First()
                    Me.Entities.Remove(task)
                    Dim primaryKey As Long = Me.Repository.GetProjectionPrimaryKey(task)
                    Dim entity As DevExpress.DevAV.EmployeeTask = Me.Repository.Find(primaryKey)
                    If entity IsNot Nothing Then
                        Me.OnBeforeEntityDeleted(primaryKey, entity)
                        Me.Repository.Remove(entity)
                        Me.Repository.UnitOfWork.SaveChanges()
                        Me.OnEntityDeleted(primaryKey, entity)
                    End If
                Loop While Me.SelectedEntities.Count > 0
            Catch e As DevExpress.Mvvm.DataModel.DbException
                Me.Refresh()
                Me.MessageBoxService.ShowMessage(e.ErrorMessage, e.ErrorCaption, DevExpress.Mvvm.MessageButton.OK, DevExpress.Mvvm.MessageIcon.[Error])
            End Try

            Me.RefreshEmployeeFilters()
        End Sub

        Public Overrides Function CanDelete(ByVal projectionEntity As DevExpress.DevAV.EmployeeTask) As Boolean
            Return Not Me.IsLoading AndAlso Me.SelectedEntities IsNot Nothing AndAlso Me.SelectedEntities.Count > 0
        End Function

        Public Overridable Sub MarkComplete()
            Dim selectedIDs = Me.SelectedEntities.[Select](Function(x) x.Id).ToList()
            selectedIDs.ForEach(Sub(id)
                Dim entity = Me.Entities.[Single](Function(x) x.Id = id)
                If entity.Status <> DevExpress.DevAV.EmployeeTaskStatus.Completed Then
                    entity.Status = DevExpress.DevAV.EmployeeTaskStatus.Completed
                    entity.Completion = 100
                    Me.Save(entity)
                    If entity Is Me.SelectedEntity Then Me.UpdateSelectedEntity()
                End If
            End Sub)
        End Sub

        Public Overridable Function CanMarkComplete() As Boolean
            Return Not Me.IsLoading AndAlso Me.SelectedEntities IsNot Nothing AndAlso Me.SelectedEntities.Count > 0 AndAlso Me.SelectedEntities.Where(Function(x) x.Status <> DevExpress.DevAV.EmployeeTaskStatus.Completed).Count() > 0
        End Function

        Public Overridable Sub AssignTask(ByVal entity As DevExpress.DevAV.EmployeeTask)
            Dim primaryKey As Long = Me.Repository.GetProjectionPrimaryKey(entity)
            Dim document As DevExpress.Mvvm.IDocument = Me.AssignTaskService.CreateDocument("EmployeeAssignView", primaryKey, Me)
            If document IsNot Nothing Then document.Show()
            Me.Refresh()
            Me.RefreshEmployeeFilters()
        End Sub

        Public Overridable Sub AttachFile(ByVal entity As DevExpress.DevAV.EmployeeTask)
            Dim dialogResult As Boolean = Me.OpenFileDialogService.ShowDialog()
            If dialogResult Then
                Dim file As DevExpress.Mvvm.IFileInfo = Me.OpenFileDialogService.Files.First()
                If file.Length > DevExpress.DevAV.Common.Utils.FilesHelper.MaxAttachedFileSize * 1050578 Then
                    Me.MessageBoxService.ShowMessage(String.Format("The file size exceeds {0} MB.", DevExpress.DevAV.Common.Utils.FilesHelper.MaxAttachedFileSize), "Error attaching file")
                    Return
                End If

                Dim unitOfWork As DevExpress.DevAV.DevAVDbDataModel.IDevAVDbUnitOfWork = Me.CreateUnitOfWork()
                Try
                    Using stream As System.IO.FileStream = New System.IO.FileStream(System.IO.Path.Combine(file.DirectoryName, file.Name), System.IO.FileMode.Open, System.IO.FileAccess.Read)
                        Dim attachedFile As DevExpress.DevAV.TaskAttachedFile = unitOfWork.AttachedFiles.Create()
                        attachedFile.EmployeeTaskId = entity.Id
                        attachedFile.Name = file.Name
                        attachedFile.Content = New Byte(CInt(stream.Length) - 1) {}
                        stream.Read(attachedFile.Content, 0, CInt(stream.Length))
                        unitOfWork.SaveChanges()
                        Me.Refresh()
                    End Using
                Catch __unusedException1__ As System.Exception
                    Me.MessageBoxService.ShowMessage("Error attaching file!", "Error")
                    Return
                End Try
            End If
        End Sub

        Public Overridable Function CanAttachFile(ByVal entity As DevExpress.DevAV.EmployeeTask) As Boolean
            Return Not Me.IsLoading AndAlso Me.SelectedEntities IsNot Nothing AndAlso Me.SelectedEntities.Count = 1
        End Function

        Public Overridable Property FollowUpSelector As ObservableCollection(Of Boolean)

        Public Overridable Property PrioritySelector As ObservableCollection(Of Boolean)

        Public Overridable Property [Private] As Boolean?

        Public Overridable Property Category As String

        Public Overridable Property GridControlFilterString As String

        Private tagsFilterString As String

        Private Property CurrentFollowUp As DevExpress.DevAV.EmployeeTaskFollowUp?

        Private Property CurrentPriority As DevExpress.DevAV.EmployeeTaskPriority?

        Public Overridable Sub OnPrivateChanged()
            Me.SetFilterString()
        End Sub

        Public Overridable Sub OnCategoryChanged()
            Me.SetFilterString()
        End Sub

        Public Overridable Sub FollowUp(ByVal parameter As String)
            Me.CurrentFollowUp = If(Me.FollowUpSelector.Contains(True), CType(System.[Enum].Parse(GetType(DevExpress.DevAV.EmployeeTaskFollowUp), parameter), DevExpress.DevAV.EmployeeTaskFollowUp?), Nothing)
            Me.SetFilterString()
        End Sub

        Public Overridable Sub Priority(ByVal parameter As String)
            Me.CurrentPriority = If(Me.PrioritySelector.Contains(True), CType(System.[Enum].Parse(GetType(DevExpress.DevAV.EmployeeTaskPriority), parameter), DevExpress.DevAV.EmployeeTaskPriority?), Nothing)
            Me.SetFilterString()
        End Sub

        Private tagsFilterSetter As Boolean

        Public Overridable Sub OnGridControlFilterStringChanged()
            If Me.tagsFilterSetter Then
                Me.tagsFilterSetter = False
                Return
            End If

            For i As Integer = 0 To Me.FollowUpSelector.Count - 1
                Me.FollowUpSelector(i) = False
            Next

            For i As Integer = 0 To Me.PrioritySelector.Count - 1
                Me.PrioritySelector(i) = False
            Next

            Me.CurrentFollowUp = Nothing
            Me.CurrentPriority = Nothing
            Me.[Private] = Nothing
            Me.Category = System.Windows.Media.Colors.Transparent.ToString()
        End Sub

        Private Sub SetFilterString()
            Dim filterStrings As System.Collections.Generic.List(Of String) = New System.Collections.Generic.List(Of String)()
            If Me.CurrentFollowUp IsNot Nothing Then filterStrings.Add("[FollowUp] == '" & Me.CurrentFollowUp.ToString() & "'")
            If Me.CurrentPriority IsNot Nothing Then filterStrings.Add("[Priority] == '" & Me.CurrentPriority.ToString() & "'")
            If Me.[Private] IsNot Nothing Then filterStrings.Add("[Private] == '" & Me.[Private].ToString() & "'")
            If Not Equals(Me.Category, System.Windows.Media.Colors.Transparent.ToString()) AndAlso Not Equals(Me.Category, Nothing) Then filterStrings.Add("[Category] == '" & Me.Category.ToString() & "'")
            Me.tagsFilterString = If((filterStrings.Count = 0), String.Empty, filterStrings(0))
            For i As Integer = 1 To filterStrings.Count() - 1
                Me.tagsFilterString += " and "
                Me.tagsFilterString += filterStrings(i)
            Next

            Me.tagsFilterSetter = True
            Me.GridControlFilterString = Me.tagsFilterString
        End Sub

        Private Sub RefreshEmployeeFilters()
            Dim getEmployeesFunction As System.Func(Of DevExpress.DevAV.DevAVDbDataModel.IDevAVDbUnitOfWork, DevExpress.Mvvm.DataModel.IRepository(Of DevExpress.DevAV.Employee, Long)) = Function(x) x.Employees
            Dim unitOfWork = Me.UnitOfWorkFactory.CreateUnitOfWork()
            Dim newFilters As System.Collections.Generic.List(Of DevExpress.DevAV.ViewModels.FilterInfo) = New System.Collections.Generic.List(Of DevExpress.DevAV.ViewModels.FilterInfo)()
            For Each employee In getEmployeesFunction(unitOfWork)
                newFilters.Add(New DevExpress.DevAV.ViewModels.FilterInfo() With {.Name = employee.FullName, .FilterCriteria = DevExpress.DevAV.ViewModels.EmployeeTaskCollectionViewModel.TasksToFilterCriteriaConverter(employee.AssignedEmployeeTasks)})
            Next

            Dim oldFilters As System.Collections.Generic.IEnumerable(Of DevExpress.DevAV.ViewModels.FilterInfo) = Me.FilterTreeViewModel.CustomFilters.[Select](Function(x) New DevExpress.DevAV.ViewModels.FilterInfo() With {.Name = x.Name, .FilterCriteria = If(System.[Object].ReferenceEquals(x.FilterCriteria, Nothing), String.Empty, x.FilterCriteria.ToString())})
            If Not newFilters.SequenceEqual(oldFilters, New DevExpress.DevAV.ViewModels.FilterInfoComparer()) Then
                If newFilters.Count <> oldFilters.Count() OrElse Not newFilters.SequenceEqual(oldFilters, New DevExpress.DevAV.ViewModels.FilterInfoNameComparer()) Then
                    Me.FilterTreeViewModel.ResetToAll()
                    Me.FilterTreeViewModel.ResetCustomFilters()
                    For Each newFilter In newFilters
                        Me.FilterTreeViewModel.AddCustomFilter(newFilter.Name, DevExpress.Data.Filtering.CriteriaOperator.Parse(newFilter.FilterCriteria))
                    Next
                Else
                    Dim index As Integer = 0
                    For Each newFilter In newFilters
                        Me.FilterTreeViewModel.ModifyCustomFilterCriteria(Me.FilterTreeViewModel.CustomFilters.ElementAt(index), DevExpress.Data.Filtering.CriteriaOperator.Parse(newFilter.FilterCriteria))
                        index += 1
                    Next
                End If
            End If
        End Sub

        Public Sub PrintTasksSummary()
            Me.ShowReport(DevExpress.DevAV.ViewModels.ReportInfoFactory.TaskListReport(Me.CreateUnitOfWork().Tasks.ToList()), "Summary")
        End Sub

        Private ReadOnly Property PrintService As IReportService
            Get
                Return Me.GetRequiredService(Of DevExpress.DevAV.Common.ViewModel.IReportService)("PrintService")
            End Get
        End Property

        Private ReadOnly Property ExportService As IReportService
            Get
                Return Me.GetRequiredService(Of DevExpress.DevAV.Common.ViewModel.IReportService)("ExportService")
            End Get
        End Property

        Public ReadOnly Property AssignTaskService As IDocumentManagerService
            Get
                Return Me.GetService(Of DevExpress.Mvvm.IDocumentManagerService)("EmployeeAssignService")
            End Get
        End Property

        Public Overridable Function CanAssignTask(ByVal entity As DevExpress.DevAV.EmployeeTask) As Boolean
            Return Not Me.IsLoading AndAlso Me.SelectedEntities IsNot Nothing AndAlso Me.SelectedEntities.Count = 1
        End Function

        Public Overridable Property FilterTreeViewModel As FilterTreeViewModel(Of DevExpress.DevAV.EmployeeTask, Long) Implements Global.DevExpress.DevAV.ViewModels.IFilterTreeViewModelContainer(Of Global.DevExpress.DevAV.EmployeeTask, System.Int64).FilterTreeViewModel

#Region "ISupportFiltering"
        Private Property ISupportFiltering_FilterExpression As Expression(Of System.Func(Of DevExpress.DevAV.EmployeeTask, Boolean)) Implements Global.DevExpress.DevAV.ViewModels.ISupportFiltering(Of Global.DevExpress.DevAV.EmployeeTask).FilterExpression
            Get
                Return Me.FilterExpression
            End Get

            Set(ByVal value As Expression(Of System.Func(Of DevExpress.DevAV.EmployeeTask, Boolean)))
                Me.FilterExpression = value
            End Set
        End Property

#End Region
        Public Overridable Property ViewKind As TasksViewKind

        Public Overridable Property SelectedEntities As ObservableCollection(Of DevExpress.DevAV.EmployeeTask)

        Protected Overrides Sub OnSelectedEntityChanged()
            MyBase.OnSelectedEntityChanged()
            Me.UpdateEntityPanelViewModel()
        End Sub

        Public Overrides Sub UpdateSelectedEntity()
            MyBase.UpdateSelectedEntity()
            Me.UpdateEntityPanelViewModel()
        End Sub

        Private Sub UpdateEntityPanelViewModel()
            If Me.SelectedEntity IsNot Nothing Then
                Me.EntityPanelViewModel.Entity = Nothing
                Me.EntityPanelViewModel.Entity = Me.SelectedEntity
            End If
        End Sub

        Public Shared Function TasksToFilterCriteriaConverter(ByVal tasks As System.Collections.Generic.List(Of DevExpress.DevAV.EmployeeTask)) As String
            Dim criteria As String = "[Id] = '-1'"
            If tasks IsNot Nothing AndAlso tasks.Count > 0 Then
                criteria = String.Empty
                For Each task In tasks
                    criteria += "[Id] = '" & task.Id & "'" & (If((task IsNot tasks.ElementAt(tasks.Count - 1)), " Or ", ""))
                Next
            End If

            Return criteria
        End Function

        Private Sub ShowReport(ByVal reportInfo As DevExpress.DevAV.Common.ViewModel.IReportInfo, ByVal reportId As String)
            Me.ExportService.ShowReport(reportInfo)
            Me.PrintService.ShowReport(reportInfo)
            Call DevExpress.DevAV.Logger.Log(String.Format("OutlookInspiredApp: Create Report : Tasks: {0}", reportId))
        End Sub
    End Class

    Friend Class FilterInfoComparer
        Implements System.Collections.Generic.IEqualityComparer(Of DevExpress.DevAV.ViewModels.FilterInfo)

        Public Overloads Function Equals(ByVal info1 As DevExpress.DevAV.ViewModels.FilterInfo, ByVal info2 As DevExpress.DevAV.ViewModels.FilterInfo) As Boolean Implements Global.System.Collections.Generic.IEqualityComparer(Of Global.DevExpress.DevAV.ViewModels.FilterInfo).Equals
            Return Equals(info1.Name, info2.Name) AndAlso Equals(info1.FilterCriteria, info2.FilterCriteria)
        End Function

        Public Overloads Function GetHashCode(ByVal info As DevExpress.DevAV.ViewModels.FilterInfo) As Integer Implements Global.System.Collections.Generic.IEqualityComparer(Of Global.DevExpress.DevAV.ViewModels.FilterInfo).GetHashCode
            Return info.ToString().ToLower().GetHashCode()
        End Function
    End Class

    Friend Class FilterInfoNameComparer
        Implements System.Collections.Generic.IEqualityComparer(Of DevExpress.DevAV.ViewModels.FilterInfo)

        Public Overloads Function Equals(ByVal info1 As DevExpress.DevAV.ViewModels.FilterInfo, ByVal info2 As DevExpress.DevAV.ViewModels.FilterInfo) As Boolean Implements Global.System.Collections.Generic.IEqualityComparer(Of Global.DevExpress.DevAV.ViewModels.FilterInfo).Equals
            Return Equals(info1.Name, info2.Name)
        End Function

        Public Overloads Function GetHashCode(ByVal info As DevExpress.DevAV.ViewModels.FilterInfo) As Integer Implements Global.System.Collections.Generic.IEqualityComparer(Of Global.DevExpress.DevAV.ViewModels.FilterInfo).GetHashCode
            Return info.ToString().ToLower().GetHashCode()
        End Function
    End Class

    Public Enum TasksViewKind
        SimpleList
        Detailed
        Prioritized
        Active
    End Enum
End Namespace
