Imports System.Collections.Generic
Imports System.Windows.Controls
Imports DevExpress.DemoData.Models

Namespace EditorsDemo

    Public Partial Class TokenComboBoxModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class TokenComboBoxViewModel

        Private _Countries As String(), _SelectedCountries As Object, _MultiLineSelectedCountries As Object

        Public Sub New()
            Countries = NWindContext.Create().CountriesArray
            SelectedCountries = CreateSelectedCountries()
            MultiLineSelectedCountries = CreateMultiLineSelectedCountries()
        End Sub

        Public Property Countries As String()
            Get
                Return _Countries
            End Get

            Private Set(ByVal value As String())
                _Countries = value
            End Set
        End Property

        Public Property SelectedCountries As Object
            Get
                Return _SelectedCountries
            End Get

            Private Set(ByVal value As Object)
                _SelectedCountries = value
            End Set
        End Property

        Public Property MultiLineSelectedCountries As Object
            Get
                Return _MultiLineSelectedCountries
            End Get

            Private Set(ByVal value As Object)
                _MultiLineSelectedCountries = value
            End Set
        End Property

        Private Function CreateMultiLineSelectedCountries() As List(Of Object)
            Return New List(Of Object)() From {Countries(0), Countries(1), Countries(12), Countries(5), Countries(7), Countries(3), Countries(10), Countries(15), Countries(21), Countries(25), Countries(29), Countries(30), Countries(90), Countries(40), Countries(22), Countries(54), Countries(20), Countries(31), Countries(37), Countries(43), Countries(49), Countries(63), Countries(4), Countries(6), Countries(60), Countries(61), Countries(65), Countries(70), Countries(74), Countries(76), Countries(71), Countries(73)}
        End Function

        Private Function CreateSelectedCountries() As List(Of Object)
            Return New List(Of Object)() From {Countries(7), Countries(3), Countries(10)}
        End Function
    End Class
End Namespace
