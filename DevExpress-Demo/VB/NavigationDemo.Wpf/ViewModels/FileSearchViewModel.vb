Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel.DataAnnotations
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System.Runtime.InteropServices

Namespace NavigationDemo

    Public Class FileSearchViewModel

#Region "Static"
        Private Shared Function GetNestedDirectories(ByVal directories As IEnumerable(Of DirectoryInfo), ByVal predicate As Func(Of DirectoryInfo, Boolean)) As IEnumerable(Of DirectoryInfo)
            Return directories.SelectMany(Function(x)
                Dim childDirectories = New DirectoryInfo(-1) {}
                Try
                    childDirectories = x.GetDirectories().Where(predicate).ToArray()
                Catch
                End Try

                Return {x}.Concat(GetNestedDirectories(childDirectories, predicate))
            End Function)
        End Function

#End Region
        Const maximumFilesCount As Integer = 2000

        Private ReadOnly _selectedPaths As ObservableCollection(Of String)

        Private dateTimeKind As ModifiedIntervalKind = ModifiedIntervalKind.Undefined

#Region "Properties"
        Protected Overridable ReadOnly Property FolderBrowserDialogService As IFolderBrowserDialogService
            Get
                Return GetService(Of IFolderBrowserDialogService)()
            End Get
        End Property

        Public Overridable Property SearchPattern As String

        Public ReadOnly Property SelectedPaths As ObservableCollection(Of String)
            Get
                Return _selectedPaths
            End Get
        End Property

        Public Overridable Property SelectedPath As String

        Public Overridable Property DateKind As SpecifiedDateKind

        Public Overridable Property FromDate As Date

        Public Overridable Property ToDate As Date

        Public Overridable Property IsSearchSubfolders As Boolean

        Public Overridable Property IsSearchHiddenFilesAndFolders As Boolean

        Public Overridable Property IsSearchSystemFolders As Boolean

        Public Overridable Property IsReadOnlyFiles As Boolean

        Public Overridable Property Searching As Boolean

        <DataAnnotations.BindableProperty>
        Public Overridable Property SearchResult As ObservableCollection(Of FileInfo)

#End Region
        Public Sub New()
            _selectedPaths = New ObservableCollection(Of String)()
            FromDate = Date.Now
            ToDate = Date.Now
            IsSearchSystemFolders = True
            IsSearchSubfolders = True
        End Sub

#Region "Command Methods"
        Public Sub OnInitialized()
            SearchPattern = ".exe"
            SetSelectedPath(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)
            POCOViewModelExtensions.GetAsyncCommand(Me, Function(x) x.StartSearch()).Execute(Nothing)
        End Sub

        Public Sub OnUnloaded()
            If CanCancel() Then Cancel()
            SearchResult.Clear()
            _selectedPaths.Clear()
            SelectedPath = Nothing
        End Sub

        Public Overridable Sub SelectPath()
            If FolderBrowserDialogService.ShowDialog() Then
                SetSelectedPath(FolderBrowserDialogService.ResultPath)
            End If
        End Sub

        Public Sub SetSelectedPath(ByVal path As String)
            SelectedPaths.Add(path)
            SelectedPath = path
        End Sub

        Public Function StartSearch() As Task
            Searching = True
            Return Task.Factory.StartNew(Sub()
                WalkFolderTree()
                Searching = False
            End Sub)
        End Function

        Public Overridable Function CanStartSearch() As Boolean
            Return Not(String.IsNullOrEmpty(SearchPattern) OrElse String.IsNullOrEmpty(SelectedPath) OrElse Searching)
        End Function

        Public Overridable Sub Cancel()
            POCOViewModelExtensions.GetAsyncCommand(Me, Function(x) x.StartSearch()).CancelCommand.Execute(Nothing)
        End Sub

        Public Overridable Function CanCancel() As Boolean
            Return Searching
        End Function

        Public Overridable Sub SelectDateTimeKind(ByVal kind As ModifiedIntervalKind)
            dateTimeKind = kind
        End Sub

#End Region
#Region "Filtering Predicates"
        Private Function AllowFolderIterate(ByVal info As FileSystemInfo) As Boolean
            Return GetHiddenElementPredicate(info) AndAlso (IsSearchSystemFolders OrElse Not info.Attributes.HasFlag(FileAttributes.System))
        End Function

        Private Function GetHiddenElementPredicate(ByVal info As FileSystemInfo) As Boolean
            Return IsSearchHiddenFilesAndFolders OrElse Not info.Attributes.HasFlag(FileAttributes.Hidden)
        End Function

        Private Function GetDateTimePredicate(ByVal info As FileInfo) As Boolean
            Dim result = True
            Dim start As Date? = Nothing
            Dim finish As Date? = Nothing
            GetDateTimeBounds(start, finish)
            If start.HasValue AndAlso finish.HasValue Then
                If dateTimeKind <> ModifiedIntervalKind.SpecifiedDates OrElse DateKind = SpecifiedDateKind.Modified Then
                    result = DateTimeIsInRange(info.LastWriteTime, start, finish)
                Else
                    result = DateTimeIsInRange(If(DateKind = SpecifiedDateKind.Accessed, info.LastAccessTime, info.CreationTime), start, finish)
                End If
            End If

            Return result
        End Function

        Private Sub GetDateTimeBounds(<Out> ByRef start As Date?, <Out> ByRef finish As Date?)
            start = Nothing
            finish = Nothing
            Select Case dateTimeKind
                Case ModifiedIntervalKind.LastWeek
                    start = Date.Now - TimeSpan.FromDays(7)
                    finish = Date.Now
                Case ModifiedIntervalKind.PastMonth
                    Dim year As Integer = If(Date.Now.Month > 1, Date.Now.Year, Date.Now.Year - 1)
                    Dim month As Integer = If(Date.Now.Month > 1, Date.Now.Month - 1, 12)
                    start = New DateTime(year, month, 1)
                    finish = New DateTime(year, month, Date.DaysInMonth(year, month))
                Case ModifiedIntervalKind.PastYear
                    start = Date.Now - TimeSpan.FromDays(365)
                    finish = Date.Now
                Case ModifiedIntervalKind.SpecifiedDates
                    start = FromDate
                    finish = ToDate
                Case Else
            End Select
        End Sub

        Private Function DateTimeIsInRange(ByVal dateTime As Date, ByVal start As Date?, ByVal finish As Date?) As Boolean
            Return dateTime >= start.Value AndAlso dateTime <= finish.Value
        End Function

#End Region
        Private Sub WalkFolderTree()
            Dim directories = GetSearchDirectories()
            Dim searchResult = New List(Of FileInfo)()
            For Each directoryInfo In directories
                If POCOViewModelExtensions.GetAsyncCommand(Me, Function(y) y.StartSearch()).IsCancellationRequested Then Exit For
                If Not AllowFolderIterate(directoryInfo) Then Continue For
                Dim files As IEnumerable(Of FileInfo) = Nothing
                Try
                    files = directoryInfo.GetFiles(String.Format("*{0}*", SearchPattern), SearchOption.TopDirectoryOnly).Where(Function(x) GetDateTimePredicate(x) AndAlso GetHiddenElementPredicate(x) AndAlso (Not IsReadOnlyFiles OrElse x.IsReadOnly)).ToArray()
                Catch
                    Continue For
                End Try

                searchResult.AddRange(files)
                If searchResult.Count > maximumFilesCount Then Cancel()
            Next

            Me.SearchResult = New ObservableCollection(Of FileInfo)(searchResult)
        End Sub

        Private Function GetSearchDirectories() As IEnumerable(Of DirectoryInfo)
            Dim rootInfo = {New DirectoryInfo(SelectedPath)}
            If Not IsSearchSubfolders Then Return rootInfo
            Return GetNestedDirectories(rootInfo, Function(x) GetHiddenElementPredicate(x))
        End Function
    End Class

    Public Enum SpecifiedDateKind
        <Display(Name:="Modified Date")>
        Modified
        <Display(Name:="Created Date")>
        Created
        <Display(Name:="Accessed Date")>
        Accessed
    End Enum

    Public Enum ModifiedIntervalKind
        Undefined
        LastWeek
        PastMonth
        PastYear
        SpecifiedDates
    End Enum
End Namespace
