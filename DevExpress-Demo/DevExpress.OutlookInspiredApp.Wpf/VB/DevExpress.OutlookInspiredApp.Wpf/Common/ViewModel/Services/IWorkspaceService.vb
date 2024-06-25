Imports System.Collections.Generic

Namespace DevExpress.DevAV.Common.ViewModel

    Public Class Workspace

        Private regionsField As Dictionary(Of String, String) = New Dictionary(Of String, String)()

        Public Sub AddRegion(ByVal regionId As String, ByVal regionLayout As String)
            regionsField.Add(regionId, regionLayout)
        End Sub

        Public Function FindRegionLayout(ByVal regionId As String) As String
            Dim regionLayout As String = Nothing
            Return If(regionsField.TryGetValue(regionId, regionLayout), regionLayout, Nothing)
        End Function

        Public ReadOnly Property Regions As IEnumerable(Of KeyValuePair(Of String, String))
            Get
                Return regionsField
            End Get
        End Property
    End Class

    Public Interface IWorkspaceService

        Function SaveWorkspace() As Workspace

        Sub RestoreWorkspace(ByVal workspace As Workspace)

    End Interface
End Namespace
