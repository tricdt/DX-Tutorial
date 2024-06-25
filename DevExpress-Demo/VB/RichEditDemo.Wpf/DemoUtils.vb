Imports System
Imports System.IO
Imports System.DirectoryServices
Imports System.Net.NetworkInformation
Imports DevExpress.DemoData.Helpers
Imports DevExpress.XtraRichEdit.Services

Namespace RichEditDemo

    Public Class DemoUtils

        Public Shared Function GetRelativePath(ByVal name As String) As String
            name = "Data\" & name
            Dim path As String = DataFilesHelper.DataDirectory
            Dim s As String = "\"
            For i As Integer = 0 To 10
                If File.Exists(path & s & name) Then
                    Return IO.Path.GetFullPath(path & s & name)
                Else
                    s += "..\"
                End If
            Next

            Return ""
        End Function
    End Class

    Public Class UserAccountService
        Implements IUserAccountService

        Private userName As String

        Private Function GetUserName() As String Implements IUserAccountService.GetUserName
            If Equals(userName, Nothing) Then userName = GetCurrentUserDisplayName()
            Return userName
        End Function

        Private Function GetCurrentUserDisplayName() As String
            Try
                Return GetCurrentUserDisplayNameCore()
            Catch
                Return Nothing
            End Try
        End Function

        Private Function GetCurrentDomainPath() As String
            Dim properties As IPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties()
            Return "LDAP://" & properties.DomainName
        End Function

        Private Function GetCurrentUserDisplayNameCore() As String
            Using de As DirectoryEntry = New DirectoryEntry(GetCurrentDomainPath())
                Using ds As DirectorySearcher = New DirectorySearcher(de)
                    ds.ClientTimeout = TimeSpan.FromSeconds(3)
                    ds.PropertiesToLoad.Add("displayname")
                    ds.Filter = String.Format("(&(objectClass=person)(objectCategory=user)(samaccountname={0}))", Environment.UserName)
                    Dim result = ds.FindOne()
                    If result IsNot Nothing Then Return result.Properties("displayname")(0).ToString()
                    Return Nothing
                End Using
            End Using
        End Function
    End Class
End Namespace
