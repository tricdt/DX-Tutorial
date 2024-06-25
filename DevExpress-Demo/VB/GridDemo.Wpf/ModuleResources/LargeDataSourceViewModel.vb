Imports DevExpress.Mvvm.DataAnnotations
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq

Namespace GridDemo

    <POCOViewModel>
    Public Class LargeDataSourceViewModel

        Protected Sub New()
        End Sub

        Public Overridable Property IsLoading As Boolean

        Public Overridable Property ItemsSource As IList

        Public Sub InitializeSource(ByVal rowCount As Integer, ByVal columnCountType As String)
            If ItemsSource Is Nothing Then AssignSource(rowCount, columnCountType)
        End Sub

        Public Sub AssignSource(ByVal rowCount As Integer, ByVal columnCountType As String)
            IsLoading = True
            ItemsSource = CreateSource(rowCount, columnCountType)
            IsLoading = False
        End Sub

        Private Shared Function CreateSource(ByVal rowCount As Integer, ByVal columnCountType As String) As IList
            Select Case columnCountType
                Case "Medium"
                    Return CreateSource(rowCount, Function(i) New LargeDataSourceObjectMedium(i))
                Case "Large"
                    Return CreateSource(rowCount, Function(i) New LargeDataSourceObjectLarge(i))
                Case "Immense"
                    Return CreateSource(rowCount, Function(i) New LargeDataSourceObjectImmense(i))
                Case Else
                    Throw New InvalidOperationException()
            End Select
        End Function

        Private Shared Function CreateSource(Of T As LargeDataSourceObjectBase)(ByVal rowCount As Integer, ByVal createItem As Func(Of Integer, T)) As List(Of T)
            Return Enumerable.Range(0, rowCount).[Select](Function(x) createItem(x)).ToList()
        End Function
    End Class
End Namespace
