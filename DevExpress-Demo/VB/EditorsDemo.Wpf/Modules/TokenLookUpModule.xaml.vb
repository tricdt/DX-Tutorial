Imports System.Collections.Generic
Imports DevExpress.Xpf.DemoBase

Namespace EditorsDemo

    <CodeFile("ModuleResources/LookUpEditTemplates.xaml")>
    Public Partial Class TokenLookUpModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class TokenLookUpViewModel
        Inherits EditorsViewModelBase

        Private _SelectedEmployees As Object, _MultiLineSelectedEmployees As Object

        Public Sub New()
            MyBase.New()
            SelectedEmployees = CreateSelectedEmployees()
            MultiLineSelectedEmployees = CreateMultiLineSelectedEmployees()
        End Sub

        Public Property SelectedEmployees As Object
            Get
                Return _SelectedEmployees
            End Get

            Private Set(ByVal value As Object)
                _SelectedEmployees = value
            End Set
        End Property

        Public Property MultiLineSelectedEmployees As Object
            Get
                Return _MultiLineSelectedEmployees
            End Get

            Private Set(ByVal value As Object)
                _MultiLineSelectedEmployees = value
            End Set
        End Property

        Private Function CreateMultiLineSelectedEmployees() As List(Of Object)
            Return New List(Of Object)() From {Employees(0), Employees(1), Employees(12), Employees(5), Employees(7), Employees(3), Employees(10), Employees(15), Employees(21), Employees(25), Employees(29), Employees(30), Employees(40), Employees(22), Employees(54), Employees(20), Employees(31), Employees(37), Employees(43), Employees(49), Employees(4), Employees(6), Employees(11), Employees(33), Employees(32), Employees(19), Employees(14), Employees(23), Employees(27), Employees(38)}
        End Function

        Private Function CreateSelectedEmployees() As List(Of Object)
            Return New List(Of Object)() From {Employees(7), Employees(3), Employees(10)}
        End Function
    End Class
End Namespace
