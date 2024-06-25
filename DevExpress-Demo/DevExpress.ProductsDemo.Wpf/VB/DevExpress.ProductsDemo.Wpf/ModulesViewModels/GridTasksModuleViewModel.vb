Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Data
Imports DevExpress.Data.Filtering.Helpers
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Utils
Imports DevExpress.XtraGrid
Imports ProductsDemo.Controls

Namespace ProductsDemo.Modules

    Public Class GridTasksModuleViewModel
        Inherits GridViewModelBase(Of Task)

        Public Property Employees As List(Of EmployeeTasks)

        Public Overridable Property SelectedEmployee As EmployeeTasks

        Public Sub ShowNewTaskWindow()
            ShowEditTaskWindowCore(New Task("New Task", TaskCategory.Work), True)
        End Sub

        Protected Overrides Sub InitializeDefaultView()
            FollowUpSelector = New ObservableCollection(Of Boolean)() From {False, False, False, False, False, False, False}
            SetListView()
            CheckedItemType = ItemType.List
        End Sub

        Protected Overrides Function GetItemsSource() As List(Of Task)
            Return SelectedEmployee.Tasks
        End Function

        Protected Overridable ReadOnly Property NotificationService As INotificationService
            Get
                Return Nothing
            End Get
        End Property

        Public Overridable Property FollowUpSelector As ObservableCollection(Of Boolean)

        Private AllTasks As ObservableCollection(Of Task)

        Private Sub InitializeEmployees()
            AllTasks = New ObservableCollection(Of Task)(DataBaseHelper.Instance.Tasks)
            Dim uniqueContacts As List(Of Contact) = New List(Of Contact)()
            For Each task As Task In AllTasks
                If task.AssignTo Is Nothing OrElse task.AssignTo.Photo Is Nothing Then Continue For
                If Not uniqueContacts.Contains(task.AssignTo) Then uniqueContacts.Add(task.AssignTo)
            Next

            Employees = New List(Of EmployeeTasks)()
            For Each contact As Contact In uniqueContacts
                Dim employeeTasks As EmployeeTasks = ViewModelSource.Create(Function() New EmployeeTasks(contact, AllTasks.Where(Function(t) Equals(t.AssignTo, contact)).ToList()))
                AddHandler employeeTasks.OnShow, Sub(s, e)
                    SelectedEmployee = employeeTasks
                    InitializeItemsSource()
                End Sub
                Employees.Add(employeeTasks)
            Next

            Dim _allTasks As EmployeeTasks = ViewModelSource.Create(Function() New EmployeeTasks(Nothing, AllTasks.ToList()))
            AddHandler _allTasks.OnShow, Sub(s, e)
                SelectedEmployee = _allTasks
                InitializeItemsSource()
            End Sub
            Employees.Add(_allTasks)
            SelectedEmployee = Employees(0)
        End Sub

        Protected Overrides Sub InitializeData()
            InitializeEmployees()
        End Sub

        Private Sub UpdateEmployees()
            For Each employeeTask As EmployeeTasks In Employees
                If employeeTask.Employee Is Nothing Then
                    employeeTask.Tasks = AllTasks.ToList()
                Else
                    employeeTask.Tasks = AllTasks.Where(Function(t) Equals(t.AssignTo, employeeTask.Employee)).ToList()
                End If
            Next

            InitializeItemsSource()
        End Sub

        Protected Overrides Function GetColumns() As List(Of GridColumnModel)
            Dim result As List(Of GridColumnModel) = New List(Of GridColumnModel)()
            result.Add(New GridColumnModel() With {.Name = "Complete", .Width = 45, .HorizontalHeaderContentAlignment = Windows.HorizontalAlignment.Center, .AllowFiltering = DefaultBoolean.False, .AllowSorting = DefaultBoolean.False})
            result.Add(New GridColumnModel() With {.Name = "Icon", .Width = 40, .HorizontalHeaderContentAlignment = Windows.HorizontalAlignment.Center, .AllowFiltering = DefaultBoolean.False, .AllowSorting = DefaultBoolean.False})
            result.Add(New GridColumnModel() With {.Name = "Priority", .Width = 40})
            result.Add(New GridColumnModel() With {.Name = "Subject", .Width = 150, .DisplayName = "Task Subject", .AllowEditing = DefaultBoolean.False})
            result.Add(New GridColumnModel() With {.Name = "Status", .Width = 100})
            result.Add(New GridColumnModel() With {.Name = "PercentComplete"})
            result.Add(New GridColumnModel() With {.Name = "CreatedDate", .Width = 90, .DisplayName = "Date Created", .GroupInterval = ColumnGroupInterval.DateRange})
            result.Add(New GridColumnModel() With {.Name = "StartDate", .Width = 90})
            result.Add(New GridColumnModel() With {.Name = "DueDate", .Width = 90, .GroupInterval = ColumnGroupInterval.DateRange})
            result.Add(New GridColumnModel() With {.Name = "CompletedDate", .Width = 90, .DisplayName = "Date Completed", .AllowEditing = DefaultBoolean.False, .GroupInterval = ColumnGroupInterval.DateRange})
            result.Add(New GridColumnModel() With {.Name = "Category", .AllowEditing = DefaultBoolean.False})
            result.Add(New GridColumnModel() With {.Name = "FlagStatus", .Width = 40})
            Return result
        End Function

        Public Overrides Sub ShowNewItemWindow()
            ShowNewTaskWindow()
        End Sub

        Private Sub model_Closed(ByVal sender As Object, ByVal e As EventArgs)
            Dim model As EditTaskViewModel = CType(sender, EditTaskViewModel)
            RemoveHandler model.Closed, AddressOf model_Closed
            If editTaskWindow IsNot Nothing Then editTaskWindow.Close()
            editTaskWindow = Nothing
            If Not model.Result Then Return
            If Not model.IsNew Then
                SelectedItem.Assign(model.Task)
                NotificationService.CreatePredefinedNotification("Task Changed", model.Task.Subject, "").ShowAsync()
            Else
                model.Task.AssignTo = SelectedEmployee.Employee
                AllTasks.Add(model.Task)
                UpdateEmployees()
                SelectedItem = model.Task
                NotificationService.CreatePredefinedNotification("New Task Created", model.Task.Subject, "").ShowAsync()
            End If
        End Sub

        Public Sub ShowEditTaskWindow()
            If SelectedItem Is Nothing Then Return
            ShowEditTaskWindowCore(SelectedItem.Clone(), False)
        End Sub

        Private editTaskWindow As EditTaskWindow

        Private Sub ShowEditTaskWindowCore(ByVal task As Task, ByVal isNew As Boolean)
            Dim model As EditTaskViewModel = ViewModelSource.Create(Function() New EditTaskViewModel(task, isNew))
            AddHandler model.Closed, AddressOf model_Closed
            editTaskWindow = New EditTaskWindow()
            editTaskWindow.DataContext = model
            editTaskWindow.ShowDialog()
        End Sub

        Public Sub DeleteTask()
            NotificationService.CreatePredefinedNotification("Task Deleted", SelectedItem.Subject, "").ShowAsync()
            AllTasks.Remove(SelectedItem)
            UpdateEmployees()
        End Sub

        Protected Overrides Sub OnSelectedItemChanged(ByVal oldValue As Task)
            MyBase.OnSelectedItemChanged(oldValue)
            UpdateSelector()
        End Sub

        Public Overridable Sub FollowUp(ByVal parameter As String)
            Dim status = CType([Enum].Parse(GetType(FlagStatus), parameter), FlagStatus)
            Select Case status
                Case FlagStatus.Today
                    SelectedItem.DueDate = Date.Today
                Case FlagStatus.Tomorrow
                    SelectedItem.DueDate = Date.Today.AddDays(1)
                Case FlagStatus.ThisWeek
                    SelectedItem.DueDate = EvalHelpers.GetWeekStart(Date.Today).AddDays(5)
                Case FlagStatus.NextWeek
                    SelectedItem.DueDate = EvalHelpers.GetWeekStart(Date.Today).AddDays(12)
                Case FlagStatus.NoDate
                    SelectedItem.DueDate = Nothing
                Case FlagStatus.Custom
                    Dim vm As CustomFlagViewModel = ViewModelSource.Create(Function() New CustomFlagViewModel(SelectedItem.StartDate, SelectedItem.DueDate))
                    Dim window As CustomFlagWindow = New CustomFlagWindow() With {.DataContext = vm}
                    Dim result As Boolean? = window.ShowDialog()
                    If result.HasValue AndAlso result.Value Then
                        SelectedItem.StartDate = vm.StartDate
                        SelectedItem.DueDate = vm.DueDate
                    End If

            End Select

            SelectedItem.Complete = False
            UpdateSelector()
        End Sub

        Private Sub UpdateSelector()
            Dim index As Integer = 0
            Dim statusCollection = [Enum].GetValues(GetType(FlagStatus)).Cast(Of FlagStatus)()
            For Each flagStatus In statusCollection
                FollowUpSelector(Math.Min(Threading.Interlocked.Increment(index), index - 1)) = IsSelectedItemValid() AndAlso Equals(SelectedItem.FlagStatus, flagStatus)
            Next
        End Sub

        Private Sub ChangeColumnCompletedDateVisibility(ByVal value As Boolean)
            BeginUpdate()
            Enumerable.First(Columns, Function(column) Equals(column.Name, "CompletedDate")).IsVisible = value
            EndUpdate()
        End Sub

        Private Function IsSelectedItemValid() As Boolean
            Return SelectedItem IsNot Nothing
        End Function

        Public Sub SetListView()
            ChangeColumnCompletedDateVisibility(True)
            ClearModel()
            BeginUpdate()
            GroupBy("CreatedDate")
            SortBy("CreatedDate", ColumnSortOrder.Descending)
            EndUpdate()
        End Sub

        Public Sub SetToDoListView()
            ChangeColumnCompletedDateVisibility(False)
            ClearModel()
            BeginUpdate()
            FilterString = "[Status] <> 'Completed' And [DueDate] Is Not Null"
            GroupBy("DueDate")
            SortBy("DueDate", ColumnSortOrder.Ascending)
            EndUpdate()
        End Sub

        Public Sub SetCompletedView()
            ChangeColumnCompletedDateVisibility(False)
            ClearModel()
            BeginUpdate()
            FilterString = "[Status] = 'Completed'"
            GroupBy("CompletedDate")
            SortBy("CompletedDate", ColumnSortOrder.Descending)
            EndUpdate()
        End Sub

        Public Sub SetTodayView()
            ChangeColumnCompletedDateVisibility(False)
            ClearModel()
            FilterString = "IsOutlookIntervalToday([DueDate])"
        End Sub

        Public Sub SetPrioritizedView()
            ChangeColumnCompletedDateVisibility(True)
            ClearModel()
            GroupBy("Category")
            GroupBy("Priority")
            SortBy("Priority", ColumnSortOrder.Descending)
            SortBy("DueDate", ColumnSortOrder.Ascending)
        End Sub

        Public Sub SetOverdueView()
            ChangeColumnCompletedDateVisibility(False)
            ClearModel()
            BeginUpdate()
            FilterString = "[Overdue] = 'True'"
            GroupBy("DueDate")
            SortBy("DueDate", ColumnSortOrder.Ascending)
            EndUpdate()
        End Sub

        Public Sub SetSimpleListView()
            ChangeColumnCompletedDateVisibility(False)
            ClearModel()
            SortBy("DueDate", ColumnSortOrder.Ascending)
        End Sub

        Public Sub SetDeferredView()
            ChangeColumnCompletedDateVisibility(True)
            ClearModel()
            BeginUpdate()
            GroupBy("CompletedDate")
            SortBy("CreatedDate", ColumnSortOrder.Ascending)
            EndUpdate()
            FilterString = "[Status] = 'Deferred' Or [Status] = 'WaitingOnSomeoneElse'"
        End Sub
    End Class

    Public Class CustomFlagViewModel

        Public Sub New(ByVal startDate As Date?, ByVal dueDate As Date?)
            Me.StartDate = startDate
            Me.DueDate = dueDate
        End Sub

        Public Overridable Property StartDate As Date?

        Public Overridable Property DueDate As Date?
    End Class

    Public Class EditTaskViewModel

        Private _IsNew As Boolean, _Task As Task

        Public Sub New(ByVal task As Task, ByVal isNew As Boolean)
            Me.Task = task
            Me.IsNew = isNew
        End Sub

        Public Property IsNew As Boolean
            Get
                Return _IsNew
            End Get

            Private Set(ByVal value As Boolean)
                _IsNew = value
            End Set
        End Property

        Public Property Task As Task
            Get
                Return _Task
            End Get

            Private Set(ByVal value As Task)
                _Task = value
            End Set
        End Property

        Public Overridable Property Result As Boolean

        Public Event Closed As EventHandler

        Public Sub Save()
            Result = True
            Close()
        End Sub

        Public Sub Close()
            RaiseEvent Closed(Me, EventArgs.Empty)
        End Sub
    End Class

    Public Class EmployeeTasks

        Private _Employee As Contact

        Public Sub New(ByVal employee As Contact, ByVal tasks As List(Of Task))
            Me.Employee = employee
            Me.Tasks = tasks
        End Sub

        Public Property Employee As Contact
            Get
                Return _Employee
            End Get

            Private Set(ByVal value As Contact)
                _Employee = value
            End Set
        End Property

        Public Overridable Property Tasks As List(Of Task)

        Public Event OnShow As EventHandler

        Public Sub Show()
            RaiseEvent OnShow(Me, EventArgs.Empty)
        End Sub
    End Class
End Namespace
