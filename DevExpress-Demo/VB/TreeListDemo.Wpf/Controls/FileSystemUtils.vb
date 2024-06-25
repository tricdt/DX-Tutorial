Imports System
Imports System.IO
Imports System.Threading
Imports TreeListDemo.Data

Namespace TreeListDemo

    Public NotInheritable Class DataHelper
        Inherits FileSystemHelper

        Private Shared ReadOnly instanceCore As DataHelper = New DataHelper()

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property Instance As DataHelper
            Get
                Return instanceCore
            End Get
        End Property

        Public Function GetFileNumSize(ByVal path As String) As Long
            Return New FileInfo(path).Length
        End Function

        Public Function GetFolderSize(ByVal fullName As String, ByVal cancellationToken As CancellationToken) As Long
            Dim info As DirectoryInfo = New DirectoryInfo(fullName)
            Return GetFolderSize(info, cancellationToken)
        End Function

        Private Function GetFolderSize(ByVal d As DirectoryInfo, ByVal cancellationToken As CancellationToken) As Long
            Dim Size As Long = 0
            Dim fis As FileInfo() = {}
            Try
                fis = d.GetFiles()
            Catch
            End Try

            If fis.Length <> 0 Then
                For Each fi As FileInfo In fis
                    If cancellationToken.IsCancellationRequested Then Exit For
                    Size += fi.Length
                Next
            End If

            Dim dis As DirectoryInfo() = {}
            Try
                dis = d.GetDirectories()
            Catch
            End Try

            If dis.Length <> 0 Then
                For Each di As DirectoryInfo In dis
                    If cancellationToken.IsCancellationRequested Then Exit For
                    Size += GetFolderSize(di, cancellationToken)
                Next
            End If

            Return Size
        End Function
    End Class

    Public Class FileSystemItemSize

        Private _DisplaySize As String, _NumSize As Long

        Const kb As Integer = 1024

        Const mb As Integer = 1048576

        Public Const Folder As String = "<Folder>"

        Public Const Drive As String = "<Drive>"

        Public Const Calculating As String = "Calculating"

        Public Property DisplaySize As String
            Get
                Return _DisplaySize
            End Get

            Private Set(ByVal value As String)
                _DisplaySize = value
            End Set
        End Property

        Public Property NumSize As Long
            Get
                Return _NumSize
            End Get

            Private Set(ByVal value As Long)
                _NumSize = value
            End Set
        End Property

        Public Event SizeChanged As EventHandler(Of ItemSizeChangedEventArgs)

        Private Sub OnSizeChanged()
            RaiseEvent SizeChanged(Me, New ItemSizeChangedEventArgs(Me))
        End Sub

        Public Sub Change(ByVal size As Long)
            NumSize = size
            DisplaySize = FileSizeToString(size)
            OnSizeChanged()
        End Sub

        Public Sub Change(ByVal displaySize As String)
            Me.DisplaySize = displaySize
            OnSizeChanged()
        End Sub

        Public Sub New(ByVal displaySize As String)
            Change(displaySize)
        End Sub

        Public Sub New(ByVal size As Long)
            Change(size)
        End Sub

        Public Function IsCalculated() As Boolean
            Return Not Equals(DisplaySize, Calculating) AndAlso Not Equals(DisplaySize, Drive) AndAlso Not Equals(DisplaySize, Folder)
        End Function

        Private Function FileSizeToString(ByVal size As Long) As String
            If size > mb Then
                Return String.Format("{0:### ### ###} MB", size / mb)
            ElseIf size > kb Then
                Return String.Format("{0:### ### ###} KB", size / kb)
            Else
                Return String.Format("{0} Bytes", size)
            End If
        End Function

        Public Overrides Function ToString() As String
            Return DisplaySize
        End Function
    End Class

    Public Class ItemSizeChangedEventArgs
        Inherits EventArgs

        Public Property Size As FileSystemItemSize

        Public Sub New(ByVal itemSize As FileSystemItemSize)
            Size = itemSize
        End Sub
    End Class
End Namespace
