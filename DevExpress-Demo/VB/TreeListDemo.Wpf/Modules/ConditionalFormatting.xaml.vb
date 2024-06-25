Imports System.Collections.Generic
Imports System.Windows.Controls

Namespace TreeListDemo

    Public Partial Class ConditionalFormatting
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class SalesDataViewModel

        Private _SalesData As IList(Of TreeListDemo.SalesData)

        Public Sub New()
            SalesData = SalesDataGenerator.CreateData()
        End Sub

        Public Property SalesData As IList(Of SalesData)
            Get
                Return _SalesData
            End Get

            Private Set(ByVal value As IList(Of SalesData))
                _SalesData = value
            End Set
        End Property
    End Class
End Namespace
