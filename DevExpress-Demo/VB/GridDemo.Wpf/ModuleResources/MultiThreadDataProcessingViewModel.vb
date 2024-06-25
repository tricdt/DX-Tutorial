Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports System
Imports System.Collections
Imports System.Collections.Concurrent
Imports System.ComponentModel

Namespace GridDemo

    <POCOViewModel>
    Public Class PLinqInstantFeedbackDemoViewModel

#Region "CallbackListSource"
        Private Class CallbackListSource
            Implements IListSource

            Private ReadOnly _GetList As Func(Of IList)

            Public Sub New(ByVal getList As Func(Of IList))
                _GetList = getList
            End Sub

            Public ReadOnly Property ContainsListCollection As Boolean Implements IListSource.ContainsListCollection
                Get
                    Return False
                End Get
            End Property

            Public Function GetList() As IList Implements IListSource.GetList
                Return _GetList()
            End Function
        End Class

#End Region
        Protected Sub New()
        End Sub

        Public Overridable Property ListSource As IListSource

        Public Overridable Property Count As Integer

        Protected Sub OnCountChanged()
            ListSource = New CallbackListSource(AddressOf GetOrders)
        End Sub

        Private listCache As ConcurrentDictionary(Of Integer, IList) = New ConcurrentDictionary(Of Integer, IList)()

        Private Function GetOrders() As IList
            Dim actualCount = If(ViewModelBase.IsInDesignMode, 0, Count)
            Return listCache.GetOrAdd(actualCount, Function(count) GenerateOrders(count))
        End Function
    End Class
End Namespace
