Imports System.Collections.Generic

Namespace ChartsDemo

    Public Class DevAVDataItem

        Private _Year As Integer, _Sales As Decimal, _Charges As Decimal, _Company As String

        Public Property Year As Integer
            Get
                Return _Year
            End Get

            Private Set(ByVal value As Integer)
                _Year = value
            End Set
        End Property

        Public Property Sales As Decimal
            Get
                Return _Sales
            End Get

            Private Set(ByVal value As Decimal)
                _Sales = value
            End Set
        End Property

        Public Property Charges As Decimal
            Get
                Return _Charges
            End Get

            Private Set(ByVal value As Decimal)
                _Charges = value
            End Set
        End Property

        Public Property Company As String
            Get
                Return _Company
            End Get

            Private Set(ByVal value As String)
                _Company = value
            End Set
        End Property

        Public Sub New(ByVal year As Integer, ByVal company As String, ByVal sales As Decimal, ByVal charges As Decimal)
            Me.Year = year
            Me.Company = company
            Me.Sales = sales
            Me.Charges = charges
        End Sub
    End Class

    Public Class DevAVBranchesSales

        Public Function GetList() As List(Of DevAVDataItem)
            Dim lastYear As Integer = Date.Now.Year - 1
            Dim list As List(Of DevAVDataItem) = New List(Of DevAVDataItem)(46)
            list.Add(New DevAVDataItem(lastYear - 10, "DevAV North", 1.010D, 0.430D))
            list.Add(New DevAVDataItem(lastYear - 10, "DevAV Central", 3.032D, 0.412D))
            list.Add(New DevAVDataItem(lastYear - 10, "DevAV South", 1.31D, 0.312D))
            list.Add(New DevAVDataItem(lastYear - 9, "DevAV North", 1.512D, 0.351D))
            list.Add(New DevAVDataItem(lastYear - 9, "DevAV Central", 3.050D, 0.411D))
            list.Add(New DevAVDataItem(lastYear - 9, "DevAV South", 1.34D, 0.333D))
            list.Add(New DevAVDataItem(lastYear - 8, "DevAV North", 1.723D, 0.431D))
            list.Add(New DevAVDataItem(lastYear - 8, "DevAV West", 0.005D, 0.215D))
            list.Add(New DevAVDataItem(lastYear - 8, "DevAV Central", 3.054D, 0.315D))
            list.Add(New DevAVDataItem(lastYear - 8, "DevAV South", 1.30D, 0.410D))
            list.Add(New DevAVDataItem(lastYear - 7, "DevAV West", 0.31D, 0.412D))
            list.Add(New DevAVDataItem(lastYear - 7, "DevAV North", 2.001D, 0.321D))
            list.Add(New DevAVDataItem(lastYear - 7, "DevAV Central", 2.975D, 0.327D))
            list.Add(New DevAVDataItem(lastYear - 7, "DevAV South", 1.283D, 0.412D))
            list.Add(New DevAVDataItem(lastYear - 6, "DevAV West", 0.41D, 0.323D))
            list.Add(New DevAVDataItem(lastYear - 6, "DevAV North", 2.612D, 0.411D))
            list.Add(New DevAVDataItem(lastYear - 6, "DevAV Central", 2.066D, 0.442D))
            list.Add(New DevAVDataItem(lastYear - 6, "DevAV South", 0.88D, 0.398D))
            list.Add(New DevAVDataItem(lastYear - 5, "DevAV West", 0.95D, 0.398D))
            list.Add(New DevAVDataItem(lastYear - 5, "DevAV North", 2.666D, 0.389D))
            list.Add(New DevAVDataItem(lastYear - 5, "DevAV Central", 2.078D, 0.421D))
            list.Add(New DevAVDataItem(lastYear - 5, "DevAV South", 1.09D, 0.401D))
            list.Add(New DevAVDataItem(lastYear - 4, "DevAV West", 1.53D, 0.435D))
            list.Add(New DevAVDataItem(lastYear - 4, "DevAV North", 3.665D, 0.444D))
            list.Add(New DevAVDataItem(lastYear - 4, "DevAV Central", 3.888D, 0.381D))
            list.Add(New DevAVDataItem(lastYear - 4, "DevAV South", 1.01D, 0.412D))
            list.Add(New DevAVDataItem(lastYear - 3, "DevAV East", 0.003D, 0.332D))
            list.Add(New DevAVDataItem(lastYear - 3, "DevAV West", 1.75D, 0.412D))
            list.Add(New DevAVDataItem(lastYear - 3, "DevAV North", 3.555D, 0.229D))
            list.Add(New DevAVDataItem(lastYear - 3, "DevAV Central", 3.008D, 0.431D))
            list.Add(New DevAVDataItem(lastYear - 3, "DevAV South", 1.11D, 0.223D))
            list.Add(New DevAVDataItem(lastYear - 2, "DevAV East", 0.32D, 0.450D))
            list.Add(New DevAVDataItem(lastYear - 2, "DevAV West", 1.31D, 0.413D))
            list.Add(New DevAVDataItem(lastYear - 2, "DevAV North", 3.485D, 0.426D))
            list.Add(New DevAVDataItem(lastYear - 2, "DevAV Central", 3.088D, 0.385D))
            list.Add(New DevAVDataItem(lastYear - 2, "DevAV South", 1.12D, 0.338D))
            list.Add(New DevAVDataItem(lastYear - 1, "DevAV East", 0.51D, 0.325D))
            list.Add(New DevAVDataItem(lastYear - 1, "DevAV West", 1.31D, 0.421D))
            list.Add(New DevAVDataItem(lastYear - 1, "DevAV North", 3.747D, 0.324D))
            list.Add(New DevAVDataItem(lastYear - 1, "DevAV Central", 3.357D, 0.441D))
            list.Add(New DevAVDataItem(lastYear - 1, "DevAV South", 1.12D, 0.524D))
            list.Add(New DevAVDataItem(lastYear, "DevAV East", 1.71D, 0.998D))
            list.Add(New DevAVDataItem(lastYear, "DevAV West", 1.22D, 0.324D))
            list.Add(New DevAVDataItem(lastYear, "DevAV North", 4.182D, 0.325D))
            list.Add(New DevAVDataItem(lastYear, "DevAV Central", 3.725D, 0.341D))
            list.Add(New DevAVDataItem(lastYear, "DevAV South", 1.111D, 0.439D))
            Return list
        End Function
    End Class
End Namespace
