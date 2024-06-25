Imports System
Imports DevExpress.Mvvm

Namespace TreeListDemo

    Public Partial Class BuildTreeFromSelfReferenceData
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As Windows.RoutedEventArgs)
            treeList.View.ExpandAllNodes()
        End Sub
    End Class

    Public Class SelfReferenceDataViewModel
        Inherits BindableBase

        Private showServiceColumnsField As Boolean

        Public Property ShowServiceColumns As Boolean
            Get
                Return showServiceColumnsField
            End Get

            Set(ByVal value As Boolean)
                SetProperty(showServiceColumnsField, value, Function() ShowServiceColumns)
            End Set
        End Property

        Public ReadOnly Property KeyFieldName As String
            Get
                Return "Id"
            End Get
        End Property

        Public ReadOnly Property ParentFieldName As String
            Get
                Return "ParentId"
            End Get
        End Property
    End Class
End Namespace
