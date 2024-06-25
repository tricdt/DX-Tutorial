Imports System.Collections.Generic

Namespace DevExpress.DevAV.ViewModels

    Public Interface IFilterTreeModelPageSpecificSettings

        ReadOnly Property StaticFiltersTitle As String

        ReadOnly Property CustomFiltersTitle As String

        Property StaticFilters As FilterInfoList

        Property CustomFilters As FilterInfoList

        ReadOnly Property HiddenFilterProperties As IEnumerable(Of String)

        ReadOnly Property AdditionalFilterProperties As IEnumerable(Of String)

        Sub SaveSettings()

    End Interface
End Namespace
