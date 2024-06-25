Imports System
Imports System.Collections.Generic

Namespace ChartsDemo

    Public MustInherit Class DevAVSalesModelBase

        Private _Name As String, _TotalIncome As Double

        Public Property Name As String
            Get
                Return _Name
            End Get

            Protected Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property TotalIncome As Double
            Get
                Return _TotalIncome
            End Get

            Protected Set(ByVal value As Double)
                _TotalIncome = value
            End Set
        End Property

        Public Sub New(ByVal name As String)
            Me.Name = name
        End Sub

        Public Overrides Function ToString() As String
            Return Me.Name
        End Function
    End Class

    Public NotInheritable Class DevAVBranch
        Inherits ChartsDemo.DevAVSalesModelBase

        Private _Categories As List(Of ChartsDemo.DevAVProductCategory)

        Public Property Categories As List(Of ChartsDemo.DevAVProductCategory)
            Get
                Return _Categories
            End Get

            Private Set(ByVal value As List(Of ChartsDemo.DevAVProductCategory))
                _Categories = value
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal categories As System.Collections.Generic.List(Of ChartsDemo.DevAVProductCategory))
            MyBase.New(name)
            Me.Categories = categories
            Dim totalIncome As Double = 0R
            For Each category As ChartsDemo.DevAVProductCategory In categories
                totalIncome += category.TotalIncome
            Next

            Me.TotalIncome = totalIncome
        End Sub
    End Class

    Public NotInheritable Class DevAVProductCategory
        Inherits ChartsDemo.DevAVSalesModelBase

        Private _Products As List(Of ChartsDemo.DevAVProduct)

        Public Property Products As List(Of ChartsDemo.DevAVProduct)
            Get
                Return _Products
            End Get

            Private Set(ByVal value As List(Of ChartsDemo.DevAVProduct))
                _Products = value
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal products As System.Collections.Generic.List(Of ChartsDemo.DevAVProduct))
            MyBase.New(name)
            Me.Products = products
            Dim totalIncome As Double = 0R
            For Each product As ChartsDemo.DevAVProduct In products
                totalIncome += product.TotalIncome
            Next

            Me.TotalIncome = totalIncome
        End Sub
    End Class

    Public NotInheritable Class DevAVProduct
        Inherits ChartsDemo.DevAVSalesModelBase

        Private _Sales As List(Of ChartsDemo.DevAVMonthlyIncome)

        Public Property Sales As List(Of ChartsDemo.DevAVMonthlyIncome)
            Get
                Return _Sales
            End Get

            Private Set(ByVal value As List(Of ChartsDemo.DevAVMonthlyIncome))
                _Sales = value
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal sales As System.Collections.Generic.List(Of ChartsDemo.DevAVMonthlyIncome))
            MyBase.New(name)
            Me.Sales = sales
            Dim totalIncome As Double = 0R
            For Each monthlyIncome As ChartsDemo.DevAVMonthlyIncome In sales
                totalIncome += monthlyIncome.Income
            Next

            Me.TotalIncome = totalIncome
        End Sub
    End Class

    Public NotInheritable Class DevAVMonthlyIncome

        Private _Month As DateTime, _Income As Double

        Public Property Month As DateTime
            Get
                Return _Month
            End Get

            Private Set(ByVal value As DateTime)
                _Month = value
            End Set
        End Property

        Public Property Income As Double
            Get
                Return _Income
            End Get

            Private Set(ByVal value As Double)
                _Income = value
            End Set
        End Property

        Public Sub New(ByVal month As System.DateTime, ByVal income As Double)
            Me.Month = month
            Me.Income = income
        End Sub
    End Class
End Namespace
