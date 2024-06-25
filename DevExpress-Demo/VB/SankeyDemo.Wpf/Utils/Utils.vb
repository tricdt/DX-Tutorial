Imports System
Imports System.Data
Imports System.IO
Imports System.Windows
Imports System.Xml
Imports System.Xml.Linq

Namespace SankeyDemo

    Public Module Utils

        Public Function GetRelativePath(ByVal name As String) As String
            name = "Data\" & name
            Dim dir As DirectoryInfo = New DirectoryInfo(DevExpress.DemoData.Helpers.DataFilesHelper.DataDirectory)
            For i As Integer = 0 To 10
                Dim filePath As String = Path.Combine(dir.FullName, name)
                If File.Exists(filePath) Then Return filePath
                dir = Directory.GetParent(dir.FullName)
            Next

            Return String.Empty
        End Function

        Public Function CreateDataSet(ByVal xmlFileName As String) As DataTable
            Dim filePath As String = GetRelativePath(xmlFileName)
            If Not String.IsNullOrWhiteSpace(filePath) Then
                Dim dataSet As DataSet = New DataSet()
                dataSet.ReadXml(filePath)
                If dataSet.Tables.Count > 0 Then Return dataSet.Tables(0)
            End If

            Return Nothing
        End Function

        Public Function GetFileUri(ByVal fileName As String) As Uri
            Return New Uri("/SankeyDemo;component/Data/" & fileName, UriKind.RelativeOrAbsolute)
        End Function

        Public Sub SetDatabasePath()
            Const dbName As String = "nwind.mdb"
            Const pathToDbTag As String = "|pathToDb|"
            Dim path As String = GetRelativePath(dbName)
            If String.IsNullOrEmpty(path) Then Return
            Dim connectionString As String = TryCast(Properties.Settings.Default("nwindConnectionString"), String)
            If String.IsNullOrEmpty(connectionString) Then Return
            connectionString = connectionString.Replace(pathToDbTag, path)
            Properties.Settings.Default("nwindConnectionString") = connectionString
        End Sub
    End Module

    Public Module DataLoader

        Private Function GetStream(ByVal fileName As String) As Stream
            Dim uri As Uri = GetResourceUri(fileName)
            Return Application.GetResourceStream(uri).Stream
        End Function

        Public Function GetResourceUri(ByVal fileName As String) As Uri
            fileName = "/SankeyDemo;component" & fileName
            Return New Uri(fileName, UriKind.RelativeOrAbsolute)
        End Function

        Public Function LoadXDocumentFromResources(ByVal fileName As String) As XDocument
            Try
                Return XDocument.Load(GetStream(fileName))
            Catch
                Return Nothing
            End Try
        End Function

        Public Function LoadXmlDocumentFromResources(ByVal fileName As String) As XmlDocument
            Dim document As XmlDocument = New XmlDocument()
            Try
                document.Load(GetStream(fileName))
                Return document
            Catch
                Return Nothing
            End Try
        End Function
    End Module

    Public Class Export

        Public Property Exporter As String

        Public Property Importer As String

        Public Property Sum As Double

        Public Sub New(ByVal from As String, ByVal [to] As String, ByVal weight As Double)
            Exporter = from
            Importer = [to]
            Sum = weight
        End Sub
    End Class
End Namespace
