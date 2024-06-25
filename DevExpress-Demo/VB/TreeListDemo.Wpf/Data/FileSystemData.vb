Imports System
Imports System.IO
Imports System.Linq
Imports System.Collections
Imports System.Collections.Generic

Namespace TreeListDemo.Data

    Public Class FileSystemItem

        Public Sub New(ByVal name As String, ByVal type As String, ByVal size As String, ByVal fullName As String)
            Me.Name = name
            ItemType = type
            Me.Size = size
            Me.FullName = fullName
        End Sub

        Public Property Name As String

        Public Property ItemType As String

        Public Property Size As String

        Public Property FullName As String
    End Class

    Public MustInherit Class FileSystemDataProviderBase

        Public MustOverride Function GetLogicalDrives() As String()

        Public MustOverride Function GetDirectories(ByVal path As String) As String()

        Public MustOverride Function GetFiles(ByVal path As String) As String()

        Public MustOverride Function GetDirectoryName(ByVal path As String) As String

        Public MustOverride Function GetFileName(ByVal path As String) As String

        Public MustOverride Function GetFileSize(ByVal path As String) As String

        Friend Function GetFileSize(ByVal size As Long) As String
            If size >= 1024 Then Return String.Format("{0:### ### ###} KB", size / 1024)
            Return String.Format("{0} Bytes", size)
        End Function
    End Class

    Public Class FileSystemHelper
        Inherits FileSystemDataProviderBase
        Implements IDataProvider

        Public Overrides Function GetLogicalDrives() As String()
            Return Directory.GetLogicalDrives()
        End Function

        Public Overrides Function GetDirectories(ByVal path As String) As String()
            Try
                Return Directory.EnumerateDirectories(path).ToArray()
            Catch __unusedUnauthorizedAccessException1__ As UnauthorizedAccessException
                Return New String() {}
            End Try
        End Function

        Public Overrides Function GetFiles(ByVal path As String) As String()
            Try
                Return Directory.EnumerateFiles(path).ToArray()
            Catch __unusedUnauthorizedAccessException1__ As UnauthorizedAccessException
                Return New String() {}
            End Try
        End Function

        Public Overrides Function GetDirectoryName(ByVal path As String) As String
            Return New DirectoryInfo(path).Name
        End Function

        Public Overrides Function GetFileName(ByVal path As String) As String
            Return New FileInfo(path).Name
        End Function

        Public Overrides Function GetFileSize(ByVal path As String) As String
            Dim size As Long = New FileInfo(path).Length
            Return GetFileSize(size)
        End Function

#Region "IDataProvider Members"
        Public Function HasChildren(ByVal item As Object) As Boolean Implements IDataProvider.HasChildren
            Dim fsItem = CType(item, FileSystemItem)
            If Not Directory.Exists(fsItem.FullName) Then Return False
            Try
                Return GetFiles(fsItem.FullName).Length > 0 OrElse GetDirectories(fsItem.FullName).Length > 0
            Catch __unusedUnauthorizedAccessException1__ As UnauthorizedAccessException
                Return False
            End Try
        End Function

        Public Function GetChildren(ByVal item As Object) As IEnumerable Implements IDataProvider.GetChildren
            Dim fsItem = CType(item, FileSystemItem)
            Dim items = New List(Of FileSystemItem)()
            If Not Directory.Exists(fsItem.FullName) Then Return items
            For Each s In GetDirectories(fsItem.FullName)
                Try
                    items.Add(New FileSystemItem(GetDirectoryName(s), "Folder", "<Folder>", s))
                Catch
                End Try
            Next

            For Each s In GetFiles(fsItem.FullName)
                Try
                    items.Add(New FileSystemItem(GetFileName(s), "File", GetFileSize(s).ToString(), s))
                Catch
                End Try
            Next

            Return items
        End Function
#End Region
    End Class
End Namespace
