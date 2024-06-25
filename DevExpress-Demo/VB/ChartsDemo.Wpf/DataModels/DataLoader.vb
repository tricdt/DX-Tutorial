Imports System
Imports System.IO
Imports System.Windows
Imports System.Xml.Linq

Namespace ChartsDemo

    Friend Module DataLoader

        Public Function LoadXmlFromResources(ByVal fileName As String) As XDocument
            Try
                Return XDocument.Load(LoadFromResources(fileName))
            Catch
                Return Nothing
            End Try
        End Function

        Public Function LoadFromResources(ByVal fileName As String) As Stream
            Try
                Dim uri As Uri = New Uri("/ChartsDemo;component" & fileName, UriKind.RelativeOrAbsolute)
                Return Application.GetResourceStream(uri).Stream
            Catch
                Return Nothing
            End Try
        End Function
    End Module
End Namespace
