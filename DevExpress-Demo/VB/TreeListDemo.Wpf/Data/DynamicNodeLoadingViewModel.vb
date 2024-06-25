Imports DevExpress.Xpf.DemoBase.Helpers
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq

Namespace TreeListDemo.Data

    Public Class DynamicNodeLoadingViewModel

        Private _DataProvider As FileSystemHelper

        Public Property Items As IEnumerable(Of FileSystemItem)

        Public Property DataProvider As FileSystemHelper
            Get
                Return _DataProvider
            End Get

            Private Set(ByVal value As FileSystemHelper)
                _DataProvider = value
            End Set
        End Property

        Public Sub New()
            DataProvider = New FileSystemHelper()
            Items = DataProvider.GetLogicalDrives().[Select](Function(x) New FileSystemItem(x, "Drive", "<Drive>", x)).ToArray()
        End Sub
    End Class

    Public Interface IDataProvider

        Function HasChildren(ByVal item As Object) As Boolean

        Function GetChildren(ByVal item As Object) As IEnumerable

    End Interface
End Namespace
