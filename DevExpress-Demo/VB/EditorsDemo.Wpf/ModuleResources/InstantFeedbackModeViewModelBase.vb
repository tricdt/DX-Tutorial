Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.DemoBase
Imports System
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks

Namespace GridDemo

    Public Class InstantFeedbackModeViewModelBase(Of T As Class)

        Public Class GenerateDataViewModel

            Private _StopGeneratingData As Boolean

            Public Shared Function Create() As GenerateDataViewModel
                Return ViewModelSource.Create(Function() New GenerateDataViewModel())
            End Function

            Protected Sub New()
            End Sub

            Public Overridable Property MaxValue As Integer

            Public Overridable Property Progress As Integer

            Public Overridable Property State As String

            Public Overridable Property ShowProgress As Boolean

            Friend Property StopGeneratingData As Boolean
                Get
                    Return _StopGeneratingData
                End Get

                Private Set(ByVal value As Boolean)
                    _StopGeneratingData = value
                End Set
            End Property

            Public Sub StopAndShow()
                StopGeneratingData = True
            End Sub

            Public Sub OnProgress(ByVal progress As ProgressInfo, ByVal state As String)
                If progress IsNot Nothing Then
                    MaxValue = progress.MaxValue
                    Me.Progress = progress.Value
                End If

                ShowProgress = progress IsNot Nothing
                Me.State = state
            End Sub
        End Class

        Protected Sub New(ByVal databaseInfo As DatabaseInfo, ByVal getQueryable As Func(Of IQueryable(Of T)))
            Me.databaseInfo = databaseInfo
            Me.getQueryable = getQueryable
            AddRecordCount = 300000
        End Sub

        Protected ReadOnly getQueryable As Func(Of IQueryable(Of T))

        Private ReadOnly databaseInfo As DatabaseInfo

        Private unloaded As Boolean

        Public Overridable Property GenerateViewModel As GenerateDataViewModel

        Private ReadOnly Property OnProgress As Action(Of ProgressInfo, String)
            Get
                Return Sub(progress, state) GetOrCreateGenerateViewModel().OnProgress(progress, state)
            End Get
        End Property

        Private ReadOnly Property ShouldStopAndShow As Func(Of Boolean)
            Get
                Return Function() unloaded OrElse GetOrCreateGenerateViewModel().StopGeneratingData
            End Get
        End Property

        Public ReadOnly Property MinAddRecordCount As Integer
            Get
                Return 100000
            End Get
        End Property

        Public ReadOnly Property MaxAddRecordCount As Integer
            Get
                Return 500000
            End Get
        End Property

        Public Overridable Property CurrentNumberOfRecords As Integer

        Public Overridable Property AddRecordCount As Integer

        Public Overridable Property InstantFeedbackQueryableSource As IQueryable(Of T)

        Public Overridable Property ServerModeQueryableSource As IQueryable(Of T)

        Public Sub OnLoaded()
            If File.Exists(databaseInfo.FileName) Then
                AssignSources()
            Else
                ContinueWithAssignSources(SQLiteDataBaseGenerator.GenerateData(databaseInfo, OnProgress, ShouldStopAndShow))
            End If
        End Sub

        Public Overridable Sub OnUnloaded()
            unloaded = True
        End Sub

        Public Sub AddRecords()
            AddRecordsCore(False)
        End Sub

        Public Sub RegenerateDatabase()
            AddRecordsCore(True)
        End Sub

        Private Sub AddRecordsCore(ByVal clearTable As Boolean)
            ContinueWithAssignSources(SQLiteDataBaseGenerator.AddData(databaseInfo, AddRecordCount, clearTable, OnProgress, ShouldStopAndShow))
        End Sub

        Private Sub ContinueWithAssignSources(ByVal task As Task)
            task.ContinueWith(Sub(t) AssignSources(), TaskScheduler.FromCurrentSynchronizationContext())
        End Sub

        Private Sub AssignSources()
            If unloaded Then Return
            AssignSourcesCore()
        End Sub

        Protected Overridable Sub AssignSourcesCore()
            InstantFeedbackQueryableSource = getQueryable()
            ServerModeQueryableSource = getQueryable()
            CurrentNumberOfRecords = ServerModeQueryableSource.Count()
            GenerateViewModel = Nothing
        End Sub

        Private Function GetOrCreateGenerateViewModel() As GenerateDataViewModel
            If GenerateViewModel Is Nothing Then GenerateViewModel = GenerateDataViewModel.Create()
            Return GenerateViewModel
        End Function
    End Class
End Namespace
