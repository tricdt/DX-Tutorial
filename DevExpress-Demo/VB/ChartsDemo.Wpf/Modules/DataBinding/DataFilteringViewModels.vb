Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq

Namespace ChartsDemo

    Public Class SalesDataViewModel

        Private _Sales As List(Of ChartsDemo.DevAVDataItem), _Companies As List(Of String)

        Public Property Sales As List(Of DevAVDataItem)
            Get
                Return _Sales
            End Get

            Private Set(ByVal value As List(Of DevAVDataItem))
                _Sales = value
            End Set
        End Property

        Public Property Companies As List(Of String)
            Get
                Return _Companies
            End Get

            Private Set(ByVal value As List(Of String))
                _Companies = value
            End Set
        End Property

        Public Sub New()
            Sales = New DevAVBranchesSales().GetList()
            Companies = Sales.[Select](Function(x) x.Company).Distinct().ToList()
        End Sub
    End Class
End Namespace
