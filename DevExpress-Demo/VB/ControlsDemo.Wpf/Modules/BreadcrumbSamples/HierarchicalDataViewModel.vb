Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Mvvm

Namespace ControlsDemo.BreadcrumbSamples

    Public Class HierarchicalDataViewModel
        Inherits ViewModelBase

        Public Property Items As List(Of DataItem)

        Public Sub New()
            Items = New List(Of DataItem)() From {New DataItem("Root Item 1", {New DataItem("Item 1.1", {New DataItem("Item 1.1.1"), New DataItem("Item 1.1.2")}), New DataItem("Item 1.2")}), New DataItem("Root Item 2", {New DataItem("Item 2.1")})}
        End Sub
    End Class

    Public Class DataItem

        Public Sub New(ByVal name As String)
            Me.Name = name
        End Sub

        Public Sub New(ByVal name As String, ByVal children As DataItem())
            Me.New(name)
            Me.Children = children.ToList()
        End Sub

        Public Property Children As List(Of DataItem)

        Public Property Name As String

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
End Namespace
