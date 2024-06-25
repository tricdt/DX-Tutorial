Imports System
Imports System.IO
Imports DevExpress.DemoData.Helpers
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.XtraRichEdit
Imports System.Windows.Markup

Namespace ProductsDemo

    Public Class Utils

        Public Shared Function GetRelativePath(ByVal name As String) As String
            name = "Data\" & name
            Dim dataPath As String = DataFilesHelper.DataDirectory
            Dim s As String = "\"
            For i As Integer = 0 To 10
                If File.Exists(dataPath & s & name) Then
                    Return Path.GetFullPath(dataPath & s & name)
                Else
                    s += "..\"
                End If
            Next

            Return ""
        End Function

        Public Shared Function GetDataStream(ByVal fileName As String) As Stream
            Dim path As String = GetRelativePath(fileName)
            If Not String.IsNullOrEmpty(path) Then
                Dim fileAccess As FileAccess = If((File.GetAttributes(path) And FileAttributes.ReadOnly) <> 0, FileAccess.Read, FileAccess.ReadWrite)
                Return New FileStream(path, FileMode.Open, fileAccess)
            End If

            Return Nothing
        End Function
    End Class

    Public Class OpenXmlLoadHelper

        Public Shared Sub Load(ByVal fileName As String, ByVal richEditControl As RichEditControl)
            Dim path As String = Utils.GetRelativePath(fileName)
            If Not String.IsNullOrEmpty(path) Then richEditControl.LoadDocument(path, DocumentFormat.OpenXml)
        End Sub
    End Class

    Public Class PathProvider
        Inherits MarkupExtension

        Private ReadOnly relativePath As String

        Public Sub New(ByVal fileName As String)
            relativePath = Utils.GetRelativePath(fileName)
        End Sub

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return relativePath
        End Function
    End Class
End Namespace
