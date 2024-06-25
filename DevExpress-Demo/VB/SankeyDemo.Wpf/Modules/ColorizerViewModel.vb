Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System
Imports System.Collections.Generic

Namespace SankeyDemo

    Public Class ColorizerViewModel
        Inherits BindableBase

        Public Shared Function Create() As ColorizerViewModel
            Return ViewModelSource.Create(Function() New ColorizerViewModel())
        End Function

        Private isAsiaCheckedField As Boolean = True

        Private isAustraliaCheckedField As Boolean = True

        Private isEuropeCheckedField As Boolean = True

        Private isNorthAmericaCheckedField As Boolean = True

        Private isSouthAmericaCheckedField As Boolean = True

        Private continentCountriesPairsField As Dictionary(Of String, List(Of String))

        Public Overridable Property IsAsiaChecked As Boolean
            Get
                Return isAsiaCheckedField
            End Get

            Set(ByVal value As Boolean)
                isAsiaCheckedField = value
                RefreshData()
            End Set
        End Property

        Public Overridable Property IsAustraliaChecked As Boolean
            Get
                Return isAustraliaCheckedField
            End Get

            Set(ByVal value As Boolean)
                isAustraliaCheckedField = value
                RefreshData()
            End Set
        End Property

        Public Overridable Property IsEuropeChecked As Boolean
            Get
                Return isEuropeCheckedField
            End Get

            Set(ByVal value As Boolean)
                isEuropeCheckedField = value
                RefreshData()
            End Set
        End Property

        Public Overridable Property IsNorthAmericaChecked As Boolean
            Get
                Return isNorthAmericaCheckedField
            End Get

            Set(ByVal value As Boolean)
                isNorthAmericaCheckedField = value
                RefreshData()
            End Set
        End Property

        Public Overridable Property IsSouthAmericaChecked As Boolean
            Get
                Return isSouthAmericaCheckedField
            End Get

            Set(ByVal value As Boolean)
                isSouthAmericaCheckedField = value
                RefreshData()
            End Set
        End Property

        Public Overridable ReadOnly Property ContinentCountriesPairs As Dictionary(Of String, List(Of String))
            Get
                Return continentCountriesPairsField
            End Get
        End Property

        Public Overridable Property DataSource As List(Of Export)

        Protected Sub New()
            continentCountriesPairsField = GetContinentCountriesPairs_ColorizerDemo()
            RefreshData()
        End Sub

        Private Sub RefreshData()
            Dim data As List(Of Export) = GetData()
            Dim removeFunction As Func(Of Export, String, Boolean) = Function(ByVal e, ByVal country) continentCountriesPairsField(country).Contains(e.Importer) OrElse continentCountriesPairsField(country).Contains(e.Exporter)
            If Not isNorthAmericaCheckedField Then data.RemoveAll(Function(x) removeFunction(x, "North America"))
            If Not isSouthAmericaCheckedField Then data.RemoveAll(Function(x) removeFunction(x, "South America"))
            If Not isAsiaCheckedField Then data.RemoveAll(Function(x) removeFunction(x, "Asia"))
            If Not isAustraliaCheckedField Then data.RemoveAll(Function(x) removeFunction(x, "Australia"))
            If Not isEuropeCheckedField Then data.RemoveAll(Function(x) removeFunction(x, "Europe"))
            DataSource = data
        End Sub
    End Class
End Namespace
