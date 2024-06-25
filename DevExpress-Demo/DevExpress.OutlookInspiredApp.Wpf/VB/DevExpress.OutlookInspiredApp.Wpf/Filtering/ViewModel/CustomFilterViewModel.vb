Imports System
Imports DevExpress.Mvvm.POCO
Imports System.Collections.Generic
Imports DevExpress.Data.Filtering

Namespace DevExpress.DevAV

    Public Class CustomFilterViewModel

        Private _EntityType As Type, _HiddenProperties As IEnumerable(Of String), _AdditionalProperties As IEnumerable(Of String)

        Public Shared Function Create(ByVal entityType As Type, ByVal hiddenProperties As IEnumerable(Of String), ByVal additionalProperties As IEnumerable(Of String)) As CustomFilterViewModel
            Return ViewModelSource.Create(Function() New CustomFilterViewModel(entityType, hiddenProperties, additionalProperties))
        End Function

        Protected Sub New(ByVal entityType As Type, ByVal hiddenProperties As IEnumerable(Of String), ByVal additionalProperties As IEnumerable(Of String))
            Me.EntityType = entityType
            Me.HiddenProperties = hiddenProperties
            Me.AdditionalProperties = additionalProperties
        End Sub

        Public Property EntityType As Type
            Get
                Return _EntityType
            End Get

            Private Set(ByVal value As Type)
                _EntityType = value
            End Set
        End Property

        Public Property HiddenProperties As IEnumerable(Of String)
            Get
                Return _HiddenProperties
            End Get

            Private Set(ByVal value As IEnumerable(Of String))
                _HiddenProperties = value
            End Set
        End Property

        Public Property AdditionalProperties As IEnumerable(Of String)
            Get
                Return _AdditionalProperties
            End Get

            Private Set(ByVal value As IEnumerable(Of String))
                _AdditionalProperties = value
            End Set
        End Property

        Public Overridable Property Save As Boolean

        Public Overridable Property FilterCriteria As CriteriaOperator

        Public Overridable Property FilterName As String
    End Class
End Namespace
