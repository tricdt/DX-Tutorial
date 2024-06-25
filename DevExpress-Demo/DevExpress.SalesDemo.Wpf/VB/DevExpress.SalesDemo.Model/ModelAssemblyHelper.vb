Imports System.IO
Imports System.Reflection

Namespace DevExpress.SalesDemo.Model

    Public Module ModelAssemblyHelper

        Private modelAssemblyField As Assembly = Nothing

        Public ReadOnly Property ModelAssembly As Assembly
            Get
                If modelAssemblyField Is Nothing Then modelAssemblyField = GetType(ModelAssemblyHelper).Assembly
                Return modelAssemblyField
            End Get
        End Property

        Private resourceNames As String() = Nothing

        Public Function GetResourcePath(ByVal resourceName As String) As String
            If resourceNames Is Nothing Then resourceNames = ModelAssembly.GetManifestResourceNames()
            For Each name As String In resourceNames
                If name.EndsWith(resourceName) Then Return name
            Next

            Return Nothing
        End Function

        Public Function GetResourceStream(ByVal resourceName As String) As Stream
            Return ModelAssembly.GetManifestResourceStream(GetResourcePath(resourceName))
        End Function
    End Module
End Namespace
