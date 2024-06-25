Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DemoData
Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.Grid.LookUp

Namespace EditorsDemo

    Public Class LookUpEditorDemoViewModel
        Inherits EditorsViewModelBase

        Private _OrdersSource As List(Of DevExpress.DemoData.Models.OrderDetailsExtended)

        Public Sub New()
            MyBase.New()
            OrdersSource = New List(Of OrderDetailsExtended)(GetOrderDetails(DataLoader))
            SelectedOrders = New List(Of Object)() From {OrdersSource(0).ProductName, OrdersSource(2).ProductName, OrdersSource(10).ProductName}
        End Sub

        Public Property OrdersSource As List(Of OrderDetailsExtended)
            Get
                Return _OrdersSource
            End Get

            Private Set(ByVal value As List(Of OrderDetailsExtended))
                _OrdersSource = value
            End Set
        End Property

        Public Property SelectedOrders As IList(Of Object)

        Public Property FocusedEditor As LookUpEdit
            Get
                Return GetValue(Of LookUpEdit)()
            End Get

            Set(ByVal value As LookUpEdit)
                SetValue(value)
            End Set
        End Property

        Private Function GetOrderDetails(ByVal dataLoader As NWindDataLoader) As IEnumerable(Of OrderDetailsExtended)
            Return CType(dataLoader.OrderDetailsExtended, IEnumerable(Of OrderDetailsExtended)).Distinct(New OrderEqualityComparer())
        End Function
    End Class

    Public Class OrderEqualityComparer
        Implements IEqualityComparer(Of OrderDetailsExtended)

        Public Overloads Function Equals(ByVal x As OrderDetailsExtended, ByVal y As OrderDetailsExtended) As Boolean Implements IEqualityComparer(Of OrderDetailsExtended).Equals
            If x Is Nothing AndAlso y IsNot Nothing Then Return False
            If x IsNot Nothing AndAlso y Is Nothing Then Return False
            Return Equals(x.ProductName, y.ProductName)
        End Function

        Public Overloads Function GetHashCode(ByVal obj As OrderDetailsExtended) As Integer Implements IEqualityComparer(Of OrderDetailsExtended).GetHashCode
            Return obj.ProductName.GetHashCode()
        End Function
    End Class
End Namespace
