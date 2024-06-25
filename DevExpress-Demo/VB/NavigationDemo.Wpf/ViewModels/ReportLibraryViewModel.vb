Imports System
Imports System.Windows
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Linq
Imports DevExpress.DemoData.Helpers
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Core

Namespace NavigationDemo

    Public Class ReportLibraryViewModel

        Private _Nodes As IEnumerable(Of NavigationDemo.ReportLibraryNode)

        Protected Overridable ReadOnly Property FolderBrowserDialogService As IFolderBrowserDialogService
            Get
                Return GetService(Of IFolderBrowserDialogService)()
            End Get
        End Property

        Public Property Nodes As IEnumerable(Of ReportLibraryNode)
            Get
                Return _Nodes
            End Get

            Private Set(ByVal value As IEnumerable(Of ReportLibraryNode))
                _Nodes = value
            End Set
        End Property

        Public Overridable Property SelectedNode As ReportLibraryNode

        Public Property CheckedReportNodes As ObservableCollection(Of ReportLibraryNode)

        Public Sub New()
            Dim reportsFolder = DataFilesHelper.FindFile("Reports", DataFilesHelper.DataPath)
            Nodes = New ObservableCollection(Of ReportLibraryNode)(GetFolderChildren(reportsFolder))
            CheckedReportNodes = New ObservableCollection(Of ReportLibraryNode)()
            SelectedNode = Enumerable.First(Nodes).Children.FirstOrDefault(Function(x) Not Equals(x.Document, Nothing))
        End Sub

        Public Sub OnNodePositionChanging(ByVal e As DragRecordOverEventArgs)
            Dim isTargetFolder = CType(e.TargetRecord, ReportLibraryNode).IsFolder
            Dim isInside = e.DropPosition = DropPosition.Inside
            If Not isTargetFolder AndAlso isInside OrElse isTargetFolder AndAlso Not isInside AndAlso Not SelectedNode.IsFolder Then
                e.Effects = DragDropEffects.None
            Else
                e.Effects = DragDropEffects.All
            End If
        End Sub

        Public Sub OnCheckStateChanged(ByVal node As ReportLibraryNode, ByVal isChecked As Object)
            If CType(isChecked, Boolean?) = True AndAlso Not node.IsFolder AndAlso Not CheckedReportNodes.Contains(node) Then
                CheckedReportNodes.Add(node)
            Else
                CheckedReportNodes.Remove(node)
            End If
        End Sub

        Public Overridable Sub SaveReports()
            If FolderBrowserDialogService.ShowDialog() Then SaveReportsCore(FolderBrowserDialogService.ResultPath)
        End Sub

        Public Overridable Function CanSaveReports() As Boolean
            Return CheckedReportNodes.Any()
        End Function

        Private Sub SaveReportsCore(ByVal targetFolder As String)
            For Each node In CheckedReportNodes
                Dim fileName = Path.GetFileName(node.Document)
                Dim targetPath = Path.Combine(targetFolder, fileName)
                Try
                    File.Copy(node.Document, targetPath, overwrite:=True)
                Catch
                End Try
            Next
        End Sub

        Private Shared Function CreateFolderNode(ByVal path As String) As ReportLibraryNode
            Dim node = ReportLibraryNode.Create(IO.Path.GetFileName(path))
            For Each child In GetFolderChildren(path)
                node.Children.Add(child)
            Next

            Return node
        End Function

        Private Shared Function GetFolderChildren(ByVal path As String) As IEnumerable(Of ReportLibraryNode)
            Return Directory.GetDirectories(path).Select(Function(x) CreateFolderNode(IO.Path.Combine(path, x))).Concat(Directory.GetFiles(path).Select(Function(x) ReportLibraryNode.Create(IO.Path.GetFileNameWithoutExtension(x), x)))
        End Function
    End Class

    Public Class ReportLibraryNode

        Public Shared Function Create(ByVal name As String, ByVal Optional document As String = Nothing) As ReportLibraryNode
            Dim node = ViewModelSource.Create(Function() New ReportLibraryNode())
            node.Name = name
            node.Document = document
            Return node
        End Function

        Public Overridable Property Name As String

        Public Overridable Property Document As String

        Private ReadOnly childrenField As ObservableCollection(Of ReportLibraryNode)

        Public ReadOnly Property Children As ObservableCollection(Of ReportLibraryNode)
            Get
                Return childrenField
            End Get
        End Property

        Public Overridable ReadOnly Property IsFolder As Boolean
            Get
                Return String.IsNullOrEmpty(Document)
            End Get
        End Property

        Protected Sub New()
            childrenField = New ObservableCollection(Of ReportLibraryNode)()
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
End Namespace
