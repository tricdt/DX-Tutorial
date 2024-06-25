Imports System
Imports DevExpress.DevAV.Properties
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.XtraEditors.DXErrorProvider

Namespace ProductsDemo.Modules

    Public Enum TaskStatus
        NotStarted
        InProgress
        Completed
        WaitingOnSomeoneElse
        Deferred
    End Enum

    Public Enum TaskCategory
        <Image("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Category_HouseChores.svg")>
        HouseChores
        <Image("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Category_Shopping.svg")>
        Shopping
        <Image("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Category_Work.svg")>
        Work
    End Enum

    Public Enum FlagStatus
        Today
        Tomorrow
        ThisWeek
        NextWeek
        NoDate
        Custom
        Completed
    End Enum

    Public Enum TaskPriority
        <Image("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Priority_High.svg")>
        Low = 0
        Medium = 1
        <Image("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Priority_Low.svg")>
        High = 2
    End Enum

    Public Class Task
        Inherits BindableBase
        Implements IDXDataErrorInfo

        Private priorityField As TaskPriority = TaskPriority.Medium

        Private percentCompleteField As Integer = 0

        Private createdDateField As Date

        Private startDateField As Date? = Nothing, dueDateField As Date? = Nothing, completedDateField As Date? = Nothing

        Private subjectField, descriptionField As String

        Private statusField As TaskStatus = TaskStatus.NotStarted

        Private categoryField As TaskCategory

        Private assignToField As Contact = Nothing

        Private changeCompleteCommandField As DelegateCommand

        Public Sub New(ByVal subject As String, ByVal category As TaskCategory)
            Me.New(subject, category, Date.Now)
        End Sub

        Friend Sub New(ByVal subject As String, ByVal category As TaskCategory, ByVal [date] As Date)
            subjectField = subject
            categoryField = category
            createdDateField = [date]
        End Sub

        Public Property Priority As TaskPriority
            Get
                Return priorityField
            End Get

            Set(ByVal value As TaskPriority)
                SetProperty(priorityField, value, "Priority")
            End Set
        End Property

        Public Property PercentComplete As Integer
            Get
                Return percentCompleteField
            End Get

            Set(ByVal value As Integer)
                SetProperty(percentCompleteField, If(value < 0, 0, If(value > 100, 100, value)), "PercentComplete", New Action(AddressOf OnPercentCompleteChanged))
            End Set
        End Property

        Private Sub OnPercentCompleteChanged()
            If percentCompleteField = 100 Then
                Status = TaskStatus.Completed
            ElseIf percentCompleteField > 0 Then
                Status = TaskStatus.InProgress
            End If
        End Sub

        Public ReadOnly Property CreatedDate As Date
            Get
                Return createdDateField
            End Get
        End Property

        Public Property StartDate As Date?
            Get
                Return startDateField
            End Get

            Set(ByVal value As Date?)
                SetProperty(startDateField, value, "StartDate")
                RaisePropertyChanged("Overdue")
            End Set
        End Property

        Public Property DueDate As Date?
            Get
                Return dueDateField
            End Get

            Set(ByVal value As Date?)
                SetProperty(dueDateField, value, "DueDate")
                RaisePropertyChanged("Overdue")
            End Set
        End Property

        Public Property CompletedDate As Date?
            Get
                Return completedDateField
            End Get

            Set(ByVal value As Date?)
                SetProperty(completedDateField, value, "CompletedDate")
            End Set
        End Property

        Public Property Subject As String
            Get
                Return subjectField
            End Get

            Set(ByVal value As String)
                SetProperty(subjectField, value, "Subject")
            End Set
        End Property

        Public Property Description As String
            Get
                Return descriptionField
            End Get

            Set(ByVal value As String)
                SetProperty(descriptionField, value, "Description")
            End Set
        End Property

        Public Property Category As TaskCategory
            Get
                Return categoryField
            End Get

            Set(ByVal value As TaskCategory)
                SetProperty(categoryField, value, "Category")
            End Set
        End Property

        Public Property Status As TaskStatus
            Get
                Return statusField
            End Get

            Set(ByVal value As TaskStatus)
                SetProperty(statusField, value, "Status", New Action(AddressOf OnStatusChanged))
            End Set
        End Property

        Private Sub OnStatusChanged()
            If Status = TaskStatus.Completed Then
                PercentComplete = 100
                CompletedDate = Date.Now
            Else
                CompletedDate = Nothing
            End If

            If Status = TaskStatus.NotStarted Then PercentComplete = 0
            If Status = TaskStatus.InProgress AndAlso PercentComplete = 100 Then PercentComplete = 75
            If Status = TaskStatus.Deferred OrElse Status = TaskStatus.WaitingOnSomeoneElse Then DueDate = Nothing
            RaisePropertyChanged("Complete")
        End Sub

        Public Property AssignTo As Contact
            Get
                Return assignToField
            End Get

            Set(ByVal value As Contact)
                assignToField = value
            End Set
        End Property

        Friend ReadOnly Property TimeDiff As TimeSpan
            Get
                Return Date.Now - CreatedDate
            End Get
        End Property

        Public ReadOnly Property Overdue As Boolean
            Get
                If Status = TaskStatus.Completed OrElse Not DueDate.HasValue Then Return False
                Dim dDate As Date = DueDate.Value.Date.AddDays(1)
                If Date.Now >= dDate Then Return True
                Return False
            End Get
        End Property

        Public Property Complete As Boolean
            Get
                Return Status = TaskStatus.Completed
            End Get

            Set(ByVal value As Boolean)
                Status = If(value, TaskStatus.Completed, TaskStatus.NotStarted)
                RaisePropertiesChanged({"Complete", "FlagStatus"})
            End Set
        End Property

        Public ReadOnly Property ChangeCompleteCommand As DelegateCommand
            Get
                If changeCompleteCommandField Is Nothing Then changeCompleteCommandField = New DelegateCommand(AddressOf ChangeComplete)
                Return changeCompleteCommandField
            End Get
        End Property

        Private Sub ChangeComplete()
            Complete = Not Complete
        End Sub

        Public ReadOnly Property Icon As Integer
            Get
                Return If(Complete, 0, 1)
            End Get
        End Property

        Public ReadOnly Property FlagStatus As FlagStatus
            Get
                Dim today As Date = Date.Today
                If Complete Then Return FlagStatus.Completed
                If Not DueDate.HasValue Then Return FlagStatus.NoDate
                If DueDate.Value.Date.Equals(today) Then Return FlagStatus.Today
                If DueDate.Value.Date.Equals(today.AddDays(1)) Then Return FlagStatus.Tomorrow
                Dim thisWeekStart As Date = DevExpress.Data.Filtering.Helpers.EvalHelpers.GetWeekStart(today)
                If DueDate.Value.Date >= thisWeekStart AndAlso DueDate.Value.Date < thisWeekStart.AddDays(7) Then Return FlagStatus.ThisWeek
                If DueDate.Value.Date >= thisWeekStart.AddDays(7) AndAlso DueDate.Value.Date < thisWeekStart.AddDays(14) Then Return FlagStatus.NextWeek
                Return FlagStatus.Custom
            End Get
        End Property

        Public Sub Assign(ByVal task As Task)
            subjectField = task.Subject
            priorityField = task.Priority
            percentCompleteField = task.PercentComplete
            createdDateField = task.CreatedDate
            startDateField = task.StartDate
            dueDateField = task.DueDate
            completedDateField = task.CompletedDate
            descriptionField = task.Description
            categoryField = task.Category
            statusField = task.Status
            assignToField = task.AssignTo
            RaisePropertiesChanged(String.Empty)
        End Sub

        Public Function Clone() As Task
            Dim task As Task = New Task(Subject, Category)
            task.Assign(Me)
            Return task
        End Function

#Region "IDXDataErrorInfo Members"
        Private Sub GetError(ByVal info As ErrorInfo) Implements IDXDataErrorInfo.GetError
        End Sub

        Private Sub GetPropertyError(ByVal propertyName As String, ByVal info As ErrorInfo) Implements IDXDataErrorInfo.GetPropertyError
            If Equals(propertyName, "DueDate") Then
                If DueDate.HasValue AndAlso StartDate.HasValue AndAlso DueDate < StartDate Then SetErrorInfo(info, Resources.DueDateError, ErrorType.Critical)
                If Not DueDate.HasValue AndAlso Status = TaskStatus.InProgress Then SetErrorInfo(info, Resources.DueDateWarning, ErrorType.Warning)
            End If
        End Sub

        Private Sub SetErrorInfo(ByVal info As ErrorInfo, ByVal errorText As String, ByVal errorType As ErrorType)
            info.ErrorText = errorText
            info.ErrorType = errorType
        End Sub
#End Region
    End Class
End Namespace
