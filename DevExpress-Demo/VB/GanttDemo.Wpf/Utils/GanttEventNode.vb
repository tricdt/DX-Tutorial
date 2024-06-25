Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Diagnostics
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace GanttDemo

    <POCOViewModel>
    Public Class GanttEventNode

        Public Shared Function CreateGroup(ByVal title As String, ByVal isChecked As Boolean?) As GanttEventNode
            Return Create(title, String.Empty, isChecked, GanttEventNodeKind.Group)
        End Function

        Public Shared Function Create(ByVal title As String, ByVal ownerClassName As String, ByVal isChecked As Boolean?, ByVal kind As GanttEventNodeKind) As GanttEventNode
            Return ViewModelSource.Create(Function() New GanttEventNode(title, ownerClassName, isChecked, kind))
        End Function

        Protected Sub New(ByVal title As String, ByVal ownerClassName As String, ByVal isChecked As Boolean?, ByVal kind As GanttEventNodeKind)
            titleField = title
            kindField = kind
            Me.ownerClassName = ownerClassName
            Me.IsChecked = isChecked
        End Sub

        Private ReadOnly titleField As String

        Public ReadOnly Property Title As String
            Get
                Return titleField
            End Get
        End Property

        Private ReadOnly kindField As GanttEventNodeKind

        Public ReadOnly Property Kind As GanttEventNodeKind
            Get
                Return kindField
            End Get
        End Property

        Public Overridable Property IsChecked As Boolean?

        Public ReadOnly Property ActualIsChecked As Boolean
            Get
                Return IsChecked.HasValue AndAlso IsChecked.Value
            End Get
        End Property

        Public Overridable Property Parent As GanttEventNode

        Private ReadOnly childrenField As ObservableCollection(Of GanttEventNode) = New ObservableCollection(Of GanttEventNode)()

        Private ReadOnly ownerClassName As String

        Public ReadOnly Property Children As IEnumerable(Of GanttEventNode)
            Get
                Return childrenField
            End Get
        End Property

        Public Sub AddChild(ByVal child As GanttEventNode)
            child.Parent = Me
            childrenField.Add(child)
        End Sub

        Public Sub ShowHelp()
            Call Process.Start(New ProcessStartInfo With {.FileName = "https://documentation.devexpress.com/WPF/" & ownerClassName & "." & Title & ".event", .UseShellExecute = True})
        End Sub
    End Class

    Public Enum GanttEventNodeKind
        Group
        EventNode
        Parameter
    End Enum
End Namespace
