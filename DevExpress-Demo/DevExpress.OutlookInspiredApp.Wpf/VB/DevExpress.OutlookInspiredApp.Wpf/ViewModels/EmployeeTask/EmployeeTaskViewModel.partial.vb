Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Linq
Imports System.Windows
Imports System.Windows.Media
Imports DevExpress.DevAV.Common.Utils
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.Localization
Imports DevExpress.Mvvm.DataModel
Imports System.Windows.Threading
Imports DevExpress.Mvvm.DataAnnotations

Namespace DevExpress.DevAV.ViewModels

    Partial Class EmployeeTaskViewModel

        Private StartTimeOfWorkDay As Date = New DateTime(1, 1, 1, 9, 0, 0)

        Private EndTimeOfWorkDay As Date = New DateTime(1, 1, 1, 19, 0, 0)

        Private ReadOnly Property PrintService As IReportService
            Get
                Return GetRequiredService(Of IReportService)("PrintService")
            End Get
        End Property

        Private ReadOnly Property ExportService As IReportService
            Get
                Return GetRequiredService(Of IReportService)("ExportService")
            End Get
        End Property

        Protected Overridable ReadOnly Property OpenFileDialogService As IOpenFileDialogService
            Get
                Return Nothing
            End Get
        End Property

        Public Overridable Property FilesInfo As ObservableCollection(Of AttachedFileInfo)

        Public Overridable Property EntityContextLookUpEmployees As IEnumerable(Of Employee)

        Public Overridable Property AssignedEmployees As IEnumerable(Of Employee)

        Public Overridable Property RtfTextDescription As String

        Public Overridable Property TextDescription As String

        Public Property SelectedFile As AttachedFileInfo

        Private allowEntityChange As Boolean

        Private disableAttachedFilesReload As Boolean

        Private allowParameterChanged As Boolean

        Private linksViewModelField As LinksViewModel

        Private createdFilePaths As List(Of String)

        Public actualRtfTextDescription As String

        Protected Overrides Function CreateEntity() As EmployeeTask
            Dim entity As EmployeeTask = MyBase.CreateEntity()
            entity.StartDate = Date.Now
            entity.DueDate = Date.Now + New TimeSpan(48, 0, 0)
            entity.Category = Colors.Transparent.ToString()
#If Not DXCORE3
            Dim id = EmployeeTaskDetailsCollectionViewModel.LastSelectedEmployeeId
            If id IsNot Nothing Then
                Dim employee = UnitOfWork.Employees.SingleOrDefault(Function(x) x.Id = id.Value)
                If employee IsNot Nothing Then entity.AssignedEmployees.Add(employee)
                EmployeeTaskDetailsCollectionViewModel.LastSelectedEmployeeId = Nothing
            End If

#End If
            Return entity
        End Function

        Protected Overrides Sub OnEntityChanged()
            MyBase.OnEntityChanged()
            allowEntityChange = False
            If Not disableAttachedFilesReload Then
                FilesInfo = New ObservableCollection(Of AttachedFileInfo)()
                If Entity IsNot Nothing AndAlso Entity.AttachedFiles IsNot Nothing Then
                    For Each file In Entity.AttachedFiles
                        FilesInfo.Add(GetAttachedFileInfo(file.Name, "", file.Id))
                    Next
                End If
            End If

            RaisePropertyChanged(Function(vm) vm.ReminderTime)
            RaisePropertyChanged(Function(vm) vm.Status)
            RaisePropertyChanged(Function(vm) vm.Completion)
            EntityContextLookUpEmployees = UnitOfWork.Employees.AsEnumerable()
            If Entity IsNot Nothing Then
                allowParameterChanged = False
                AssignedEmployees = Entity.AssignedEmployees
                RtfTextDescription = Entity.RtfTextDescription
                Dim rtfAction As Action = Sub()
                    actualRtfTextDescription = RtfTextDescription
                    allowParameterChanged = True
                End Sub
                Dispatcher.CurrentDispatcher.BeginInvoke(rtfAction)
                Log(String.Format("OutlookInspiredApp: Edit Task: {0}", If(String.IsNullOrEmpty(Entity.Subject), "<New>", Entity.Subject)))
            End If

            allowEntityChange = True
        End Sub

        Public Overridable Sub OnAssignedEmployeesChanged()
            If Not allowParameterChanged OrElse AssignedEmployeesAreEqual() Then Return
            If allowEntityChange AndAlso Entity IsNot Nothing Then
                Entity.AssignedEmployees = AssignedEmployees.ToList()
                SetCollectionChange()
            End If
        End Sub

        Public Overridable Sub OnRtfTextDescriptionChanged()
            If Not allowParameterChanged OrElse Entity Is Nothing OrElse Not allowEntityChange Then Return
            If Not Equals(actualRtfTextDescription, RtfTextDescription) Then
                Entity.RtfTextDescription = RtfTextDescription
                actualRtfTextDescription = RtfTextDescription
                Update()
            End If
        End Sub

        Public Overridable Sub OnTextDescriptionChanged()
            If Entity IsNot Nothing AndAlso Not Equals(TextDescription, Entity.Description) AndAlso allowParameterChanged Then Entity.Description = TextDescription
        End Sub

        Public Overridable Sub Unloaded()
            DeleteTempFiles(createdFilePaths)
        End Sub

        Public Property ReminderTime As Date?
            Get
                If Entity Is Nothing OrElse Entity.ReminderDateTime Is Nothing Then Return Nothing
                Return Entity.ReminderDateTime.Value
            End Get

            Set(ByVal value As Date?)
                If Entity Is Nothing OrElse value Is Nothing OrElse Entity.ReminderDateTime Is Nothing Then Return
                Dim reminderDateTime As Date = CDate(Entity.ReminderDateTime)
                Entity.ReminderDateTime = New DateTime(reminderDateTime.Year, reminderDateTime.Month, reminderDateTime.Day, (CDate(value)).Hour, (CDate(value)).Minute, (CDate(value)).Second)
            End Set
        End Property

        Public Property Status As EmployeeTaskStatus
            Get
                Return If(Entity Is Nothing, EmployeeTaskStatus.NotStarted, Entity.Status)
            End Get

            Set(ByVal value As EmployeeTaskStatus)
                If Entity Is Nothing Then Return
                Dim oldStatus As String = Entity.Status.ToString()
                Entity.Status = value
                If Equals(oldStatus, EmployeeTaskStatus.Completed.ToString()) AndAlso Entity.Status <> EmployeeTaskStatus.Completed Then Entity.Completion = 50
                If Entity.Status = EmployeeTaskStatus.Completed AndAlso Entity.Completion <> 100 Then Entity.Completion = 100
                RaisePropertyChanged(Function(vm) vm.Entity)
                RaisePropertyChanged(Function(vm) vm.Entity.Status)
                RaisePropertyChanged(Function(vm) vm.Entity.Completion)
            End Set
        End Property

        Public Property Completion As Integer
            Get
                Return If(Entity IsNot Nothing, Entity.Completion, 0)
            End Get

            Set(ByVal value As Integer)
                If Entity Is Nothing Then Return
                Entity.Completion = value
                If Entity.Completion = 100 Then Entity.Status = EmployeeTaskStatus.Completed
                If Entity.Completion <> 100 AndAlso Entity.Status = EmployeeTaskStatus.Completed Then Entity.Status = EmployeeTaskStatus.InProgress
                RaisePropertyChanged(Function(vm) vm.Entity.Status)
                RaisePropertyChanged(Function(vm) vm.Entity)
            End Set
        End Property

        Public Overridable Sub FollowUp(ByVal name As String)
            Select Case name
                Case "Today"
                    SetFollowUpDateTime(Date.Now, Date.Now, True, Date.Now, EndTimeOfWorkDay.AddHours(-1))
                Case "Tomorrow"
                    SetFollowUpDateTime(Date.Now.AddDays(1), Date.Now.AddDays(1), True, NextWorkDay(Date.Now), StartTimeOfWorkDay)
                Case "ThisWeek"
                    SetFollowUpDateTime(GetThisWeekStartDate(), GetBorderWorkDay(False), True, GetThisWeekStartDate(), StartTimeOfWorkDay)
                Case "NextWeek"
                    SetFollowUpDateTime(GetBorderWorkDay(True, True), GetBorderWorkDay(False, True), True, GetBorderWorkDay(True, True), StartTimeOfWorkDay)
                Case "NoDate"
                    SetFollowUpDateTime(Nothing, Nothing, False, Nothing, Nothing)
                Case "Custom"
                    SetFollowUpDateTime(Date.Now, Date.Now, True, Date.Now, Date.Now)
            End Select

            RaisePropertyChanged(Function(vm) vm.Entity)
        End Sub

        Public Overridable Sub MarkComplete()
            Status = EmployeeTaskStatus.Completed
            RaisePropertyChanged(Function(vm) vm.Status)
            RaisePropertyChanged(Function(vm) vm.Completion)
        End Sub

        Public Overridable Sub AttachFiles(ByVal e As DragEventArgs)
            Dim filesName As String() = New String() {}
            If e IsNot Nothing AndAlso e.Data.GetDataPresent(DataFormats.FileDrop) Then
                filesName = CType(e.Data.GetData(DataFormats.FileDrop), String())
            Else
                Dim dialogResult As Boolean = OpenFileDialogService.ShowDialog()
                If dialogResult Then filesName = OpenFileDialogService.Files.[Select](Function(x) Path.Combine(x.DirectoryName, x.Name)).ToArray()
            End If

            AttachedFilesCore(filesName)
        End Sub

        Public Overridable Sub PrintTaskDetail()
            ShowReport(TaskDetailReport(Entity), "TaskDetail")
        End Sub

        Private Sub AttachedFilesCore(ByVal names As String())
            Dim fileLengthExceed As Boolean = False
            Dim info As FileInfo
            For Each name As String In names
                info = New FileInfo(name)
                If info.Length > MaxAttachedFileSize * 1050578 Then
                    fileLengthExceed = True
                Else
                    FilesInfo.Add(GetAttachedFileInfo(info.Name, info.DirectoryName))
                End If
            Next

            If fileLengthExceed Then MessageBoxService.ShowMessage(String.Format("The size of one of the files exceeds {0} MB.", MaxAttachedFileSize), "Error attaching files")
            SetCollectionChange()
            Update()
        End Sub

        Public Overridable Sub OpenFile()
            If SelectedFile.Id = -1 Then
                OpenFileFromDisc(SelectedFile.FullPath)
                Return
            End If

            If createdFilePaths Is Nothing Then createdFilePaths = New List(Of String)()
            createdFilePaths.Add(OpenFileFromDb(SelectedFile.Name, Enumerable.Single(Entity.AttachedFiles, Function(x) x.Id = SelectedFile.Id).Content))
        End Sub

        Public Overridable Sub DeleteFile()
            FilesInfo.Remove(SelectedFile)
            SetCollectionChange()
            Update()
        End Sub

        Public Overrides Sub Delete()
            If MessageBoxService.ShowMessage(String.Format(ScaffoldingLocalizer.GetString(ScaffoldingStringId.Confirmation_Delete), EntityDisplayName), GetConfirmationMessageTitle(), MessageButton.YesNo) <> MessageResult.Yes Then Return
            Try
                If Entity.AttachedFiles IsNot Nothing Then
                    Dim deletedFileKeys As Long() = Entity.AttachedFiles.[Select](Function(x) x.Id).ToArray()
                    For Each key In deletedFileKeys
                        Dim file = UnitOfWork.AttachedFiles.Find(key)
                        UnitOfWork.AttachedFiles.Remove(file)
                    Next
                End If

                OnBeforeEntityDeleted(PrimaryKey, Entity)
                Repository.Remove(Entity)
                UnitOfWork.SaveChanges()
                Dim primaryKeyForMessage As Long = PrimaryKey
                Dim entityForMessage As EmployeeTask = Entity
                Entity = Nothing
                OnEntityDeleted(primaryKeyForMessage, entityForMessage)
                Close()
            Catch e As DbException
                MessageBoxService.ShowMessage(e.ErrorMessage, e.ErrorCaption, MessageButton.OK, MessageIcon.Error)
            End Try
        End Sub

        Private Sub SetCollectionChange()
            Entity.AttachedCollectionsChanged = Not Entity.AttachedCollectionsChanged
        End Sub

        Protected Overrides Function SaveCore() As Boolean
            disableAttachedFilesReload = True
            Dim saveCoreResult As Boolean = MyBase.SaveCore()
            If Not saveCoreResult Then Return False
            Dim hasFilesOperations As Boolean = False
            disableAttachedFilesReload = False
            If Entity.AttachedFiles IsNot Nothing Then
                Dim deletedItems As List(Of Long) = New List(Of Long)()
                For Each file In Entity.AttachedFiles
                    Dim id As Long = file.Id
                    If FilesInfo.FirstOrDefault(Function(x) id = x.Id) Is Nothing Then deletedItems.Add(file.Id)
                Next

                For Each item As Long In deletedItems
                    Dim file = UnitOfWork.AttachedFiles.Find(item)
                    UnitOfWork.AttachedFiles.Remove(file)
                    hasFilesOperations = True
                Next
            End If

            For Each fileInfo In FilesInfo
                If fileInfo.Id = -1 Then
                    Try
                        Using stream As FileStream = New FileStream(fileInfo.FullPath, FileMode.Open, FileAccess.Read)
                            Dim attachedFile As TaskAttachedFile = UnitOfWork.AttachedFiles.Create()
                            attachedFile.EmployeeTaskId = Entity.Id
                            attachedFile.Name = fileInfo.Name
                            attachedFile.Content = New Byte(CInt(stream.Length) - 1) {}
                            stream.Read(attachedFile.Content, 0, CInt(stream.Length))
                            hasFilesOperations = True
                        End Using
                    Catch __unusedException1__ As Exception
                        MessageBoxService.ShowMessage("Error attaching file!", "Error")
                        Return False
                    End Try
                End If
            Next

            If hasFilesOperations Then UnitOfWork.SaveChanges()
            Return True
        End Function

        Private Function AssignedEmployeesAreEqual() As Boolean
            If Entity Is Nothing OrElse Entity.AssignedEmployees Is Nothing OrElse AssignedEmployees Is Nothing Then Return True
            Dim hasItems1 = Entity.AssignedEmployees.Except(AssignedEmployees).Any()
            Dim hasItems2 = AssignedEmployees.Except(Entity.AssignedEmployees).Any()
            Return Not(hasItems1 OrElse hasItems2)
        End Function

        Public Overridable Function CanMarkComplete() As Boolean
            Return Entity IsNot Nothing AndAlso Entity.Status <> EmployeeTaskStatus.Completed
        End Function

        Public Overridable Function CanOpenFile() As Boolean
            Return SelectedFile IsNot Nothing
        End Function

        Public Overridable Function CanDeleteFile() As Boolean
            Return SelectedFile IsNot Nothing
        End Function

        Protected Overrides Function GetTitle() As String
#If DXCORE3
            return Entity.Owner != null ? Entity.Owner.FullName : (Entity.Subject ?? string.Empty);
#Else
            Return If(Entity.Subject, String.Empty)
#End If
        End Function

        Protected Overrides Function GetConfirmationMessageTitle() As String
            Dim baseTitle = MyBase.GetConfirmationMessageTitle()
            If String.IsNullOrEmpty(baseTitle) Then Return baseTitle
            Return If(baseTitle.Length <= 30, baseTitle, baseTitle.Substring(0, 30) & "...")
        End Function

        Public ReadOnly Property LinksViewModel As LinksViewModel
            Get
                If linksViewModelField Is Nothing Then linksViewModelField = LinksViewModel.Create()
                Return linksViewModelField
            End Get
        End Property

        Private Sub SetFollowUpDateTime(ByVal startDate As Date?, ByVal dueDate As Date?, ByVal reminder As Boolean, ByVal reminderDateTime As Date?, ByVal reminderTime As Date?)
            Entity.StartDate = startDate
            Entity.DueDate = dueDate
            Entity.Reminder = reminder
            Entity.ReminderDateTime = reminderDateTime
            Me.ReminderTime = reminderTime
            RaisePropertyChanged(Function(vm) vm.ReminderTime)
        End Sub

        Private Function NextWorkDay(ByVal [date] As Date) As Date
            Do
                [date] = [date].AddDays(1)
            Loop While IsHoliday([date])

            Return [date]
        End Function

        Private Function IsHoliday(ByVal dateTime As Date) As Boolean
            Return dateTime.DayOfWeek = DayOfWeek.Saturday OrElse dateTime.DayOfWeek = DayOfWeek.Sunday
        End Function

        Private Function GetBorderWorkDay(ByVal Optional firstWorkDay As Boolean = True, ByVal Optional nextWeek As Boolean = False) As Date
            Dim dateTime As Date = Date.Now
            While IsHoliday(dateTime)
                dateTime = dateTime.AddDays(1)
            End While

            If nextWeek Then dateTime = dateTime.AddDays(7)
            While Not IsHoliday(dateTime)
                dateTime = dateTime.AddDays(If(firstWorkDay, -1, 1))
            End While

            dateTime = dateTime.AddDays(If(firstWorkDay, 1, -1))
            Return dateTime
        End Function

        Private Function GetThisWeekStartDate() As Date
            Dim dateTime As Date = Date.Now
            Dim realCounter As Integer = 0
            While IsHoliday(dateTime)
                dateTime = dateTime.AddDays(1)
            End While

            Do
                dateTime = dateTime.AddDays(1)
                If Not IsHoliday(dateTime) Then realCounter += 1
            Loop While Not IsHoliday(dateTime) AndAlso realCounter < 2

            If realCounter < 2 Then dateTime = dateTime.AddDays(-1)
            Return dateTime
        End Function

        Private Sub ShowReport(ByVal reportInfo As IReportInfo, ByVal reportId As String)
            ExportService.ShowReport(reportInfo)
            PrintService.ShowReport(reportInfo)
            Log(String.Format("OutlookInspiredApp: Create Report : Task: {0}", reportId))
        End Sub
    End Class

    Public Class EmployeeTaskStatusWithExternalMetadata

        Public Shared Sub BuildMetadata(ByVal builder As EnumMetadataBuilder(Of EmployeeTaskStatus))
            builder.Member(EmployeeTaskStatus.NotStarted).ImageUri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Tasks/NotStarted.svg").EndMember().Member(EmployeeTaskStatus.NeedAssistance).ImageUri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Tasks/NeedAssistance.svg").EndMember().Member(EmployeeTaskStatus.InProgress).ImageUri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Tasks/InProgress.svg").EndMember().Member(EmployeeTaskStatus.Deferred).ImageUri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Tasks/Deferred.svg").EndMember().Member(EmployeeTaskStatus.Completed).ImageUri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Tasks/Completed.svg").EndMember()
        End Sub
    End Class

    Public Class EmployeeTaskPriorityWithExternalMetadata

        Public Shared Sub BuildMetadata(ByVal builder As EnumMetadataBuilder(Of EmployeeTaskPriority))
            builder.Member(EmployeeTaskPriority.High).ImageUri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Priority/MediumPriority.svg").EndMember().Member(EmployeeTaskPriority.Low).ImageUri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Priority/LowPriority.svg").EndMember().Member(EmployeeTaskPriority.Normal).ImageUri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Priority/NormalPriority.svg").EndMember().Member(EmployeeTaskPriority.Urgent).ImageUri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Priority/HighPriority.svg").EndMember()
        End Sub
    End Class
End Namespace
