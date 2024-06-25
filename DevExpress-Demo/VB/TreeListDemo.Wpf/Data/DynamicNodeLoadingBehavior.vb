Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Grid.TreeList
Imports System
Imports System.Collections
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Media

Namespace TreeListDemo.Data

    Public Class FileSystemChildBehavior
        Inherits Behavior(Of TreeListView)

        Public Property DataProvider As IDataProvider
            Get
                Return CType(GetValue(DataProviderProperty), IDataProvider)
            End Get

            Set(ByVal value As IDataProvider)
                SetValue(DataProviderProperty, value)
            End Set
        End Property

        Public Shared ReadOnly DataProviderProperty As DependencyProperty = DependencyProperty.Register("DataProvider", GetType(IDataProvider), GetType(FileSystemChildBehavior), New PropertyMetadata(Nothing, Sub(s, e) CType(s, FileSystemChildBehavior).OnDataProviderChanged(e)))

        Private Sub OnDataProviderChanged(ByVal e As DependencyPropertyChangedEventArgs)
            If e.NewValue Is Nothing Then Return
            AssociatedObject.ChildNodesSelector = New CustomAsyncChildNodeSelector(DataProvider)
        End Sub
    End Class

    Public Class CustomAsyncChildNodeSelector
        Implements IAsyncChildNodesSelector

        Private ReadOnly dataProvider As IDataProvider

        Public Sub New(ByVal dataProvider As IDataProvider)
            Me.dataProvider = dataProvider
        End Sub

        Public Function HasChildNode(ByVal item As Object, ByVal token As CancellationToken) As Task(Of Boolean) Implements IAsyncChildNodesSelector.HasChildNode
            Return Tasks.Task.Run(Async Function()
                For i As Integer = 0 To 10 - 1
                    token.ThrowIfCancellationRequested()
                    Await Tasks.Task.Delay(25)
                Next

                Return dataProvider.HasChildren(item)
            End Function)
        End Function

        Public Function SelectChildren(ByVal item As Object) As IEnumerable Implements IChildNodesSelector.SelectChildren
            Throw New NotImplementedException()
        End Function

        Public Function SelectChildrenAsync(ByVal item As Object, ByVal token As CancellationToken) As Task(Of IEnumerable) Implements IAsyncChildNodesSelector.SelectChildrenAsync
            Return Tasks.Task.Run(Async Function()
                For i As Integer = 0 To 20 - 1
                    token.ThrowIfCancellationRequested()
                    Await Tasks.Task.Delay(100)
                Next

                Return dataProvider.GetChildren(item)
            End Function)
        End Function
    End Class

    Public Class FileSystemNodeImageSelector
        Inherits TreeListNodeImageSelector

        Public Overrides Function [Select](ByVal rowData As TreeListRowData) As ImageSource
            Dim fsItem = TryCast(rowData.Row, FileSystemItem)
            If fsItem Is Nothing Then Return Nothing
            If Equals(fsItem.ItemType, "Drive") Then
                Return FileSystemImages.DiskImage
            ElseIf Equals(fsItem.ItemType, "Folder") Then
                Return If(rowData.IsExpanded, FileSystemImages.OpenedFolderImage, FileSystemImages.ClosedFolderImage)
            End If

            Return FileSystemImages.FileImage
        End Function
    End Class
End Namespace
