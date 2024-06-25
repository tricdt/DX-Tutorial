Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Diagnostics
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace DiagramDemo

    <POCOViewModel>
    Public Class DiagramEventNode

        Public Shared Function Create(ByVal title As String, ByVal isChecked As Boolean?, ByVal kind As DiagramEventNodeKind) As DiagramEventNode
            Return ViewModelSource.Create(Function() New DiagramEventNode(title, isChecked, kind))
        End Function

        Protected Sub New(ByVal title As String, ByVal isChecked As Boolean?, ByVal kind As DiagramEventNodeKind)
            titleField = title
            kindField = kind
            Me.IsChecked = isChecked
        End Sub

        Private ReadOnly titleField As String

        Public ReadOnly Property Title As String
            Get
                Return titleField
            End Get
        End Property

        Private ReadOnly kindField As DiagramEventNodeKind

        Public ReadOnly Property Kind As DiagramEventNodeKind
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

        Public Overridable Property Parent As DiagramEventNode

        Private ReadOnly childrenField As ObservableCollection(Of DiagramEventNode) = New ObservableCollection(Of DiagramEventNode)()

        Public ReadOnly Property Children As IEnumerable(Of DiagramEventNode)
            Get
                Return childrenField
            End Get
        End Property

        Public Sub AddChild(ByVal child As DiagramEventNode)
            child.Parent = Me
            childrenField.Add(child)
        End Sub

        Public Sub ShowHelp()
            Call Process.Start(New ProcessStartInfo With {.FileName = "https://documentation.devexpress.com/WPF/DevExpress.Xpf.Diagram.DiagramControl." & Title & ".event", .UseShellExecute = True})
        End Sub
    End Class

    Public Enum DiagramEventNodeKind
        Group
        EventNode
        Parameter
    End Enum
End Namespace
