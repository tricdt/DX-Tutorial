Imports System.Collections.Generic
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Map

Namespace MapDemo

    <POCOViewModel>
    Public Class ShopInfoViewModel

        Public Overridable Property Name As String

        Public Overridable Property Phone As String

        Public Overridable Property Fax As String

        Public Overridable Property Address As String

        Public Overridable Property Sales As Double

        Public Overridable Property ShopLocation As GeoPoint

        Private Shared Function ConvertShopNameToFilePath(ByVal ShopName As String) As String
            Dim result As String = ShopName.Replace(" ", "")
            result = "../Images/Shops/" & result.Replace("-", "") & ".png"
            Return result
        End Function

        Private statistics As Dictionary(Of String, Double) = New Dictionary(Of String, Double)()

        Private ReadOnly imagePathField As String

        Public Sub New(ByVal Name As String, ByVal Address As String, ByVal Phone As String, ByVal Fax As String)
            Me.Name = Name
            Me.Address = Address
            Me.Phone = Phone
            Me.Fax = Fax
            imagePathField = ConvertShopNameToFilePath(Name)
        End Sub

        Public ReadOnly Property ImagePath As String
            Get
                Return imagePathField
            End Get
        End Property

        Public Sub AddProductGroup(ByVal groupName As String, ByVal sales As Double)
            If statistics.ContainsKey(groupName) Then
                statistics(groupName) = sales
            Else
                statistics.Add(groupName, sales)
            End If

            Me.Sales += sales
        End Sub

        Public Function GetSalesByProductGroup(ByVal groupName As String) As Double
            Return If(statistics.ContainsKey(groupName), statistics(groupName), 0.0)
        End Function
    End Class
End Namespace
