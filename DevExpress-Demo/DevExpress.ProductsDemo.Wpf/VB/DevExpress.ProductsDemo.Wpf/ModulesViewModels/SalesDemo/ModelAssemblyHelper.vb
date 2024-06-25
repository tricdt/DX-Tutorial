Imports System.IO
Imports System.Reflection

Namespace ProductsDemo

    Public Module ModelAssemblyHelper

        Private assembly As Assembly = Nothing

        Public ReadOnly Property CurrentAssembly As Assembly
            Get
                If assembly Is Nothing Then assembly = GetType(ModelAssemblyHelper).Assembly
                Return assembly
            End Get
        End Property

        Public Function GetResourcePath(ByVal resourceName As String) As String
            Return "ProductsDemo.SalesDemoResources." & resourceName
        End Function

        Public Function GetResourceStream(ByVal resourceName As String) As Stream
            Return CurrentAssembly.GetManifestResourceStream(GetResourcePath(resourceName))
        End Function
    End Module
End Namespace
