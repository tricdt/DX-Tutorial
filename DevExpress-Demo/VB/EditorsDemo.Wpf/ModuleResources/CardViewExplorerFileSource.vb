Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports GridDemo.ModuleResources
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO

Namespace GridDemo

    Public Class CardViewExplorerFileSource

        Private _FullPath As String, _Type As TypeElement

        Private Shared ReadOnly factory As Func(Of String, TypeElement, SizeIcon, Integer, CardViewExplorerFileSource) = ViewModelSource.Factory(Function(ByVal fullPath As String, ByVal type As TypeElement, ByVal sizeType As SizeIcon, ByVal size As Integer) New CardViewExplorerFileSource(fullPath, type, sizeType, size))

        Public Shared Function Create(ByVal fullPath As String, ByVal type As TypeElement, ByVal sizeType As SizeIcon, ByVal size As Integer) As CardViewExplorerFileSource
            Return factory(fullPath, type, sizeType, size)
        End Function

        Private Shared folder As Byte()

        Private Shared cache As Dictionary(Of String, Byte()) = New Dictionary(Of String, Byte())()

        <DependsOnProperties("FileName")>
        Public ReadOnly Property FileNameFirst As Char
            Get
                Return Char.ToUpper(FileName(0))
            End Get
        End Property

        Public Overridable Property FileName As String

        Public Overridable Property Icon As Byte()

        Public Overridable Property Size As Integer

        Public Property FullPath As String
            Get
                Return _FullPath
            End Get

            Private Set(ByVal value As String)
                _FullPath = value
            End Set
        End Property

        Public Property Type As TypeElement
            Get
                Return _Type
            End Get

            Private Set(ByVal value As TypeElement)
                _Type = value
            End Set
        End Property

        Public Enum TypeElement
            Folder
            File
            Drive
        End Enum

        Protected Sub New(ByVal fullPath As String, ByVal type As TypeElement, ByVal sizeType As SizeIcon, ByVal size As Integer)
            Me.Type = type
            Me.FullPath = fullPath
            SetIcon(sizeType, size)
        End Sub

        Public Sub Resize(ByVal sizeType As SizeIcon, ByVal sizeInt As Integer)
            SetIcon(sizeType, sizeInt)
        End Sub

        Private Sub SetIcon(ByVal sizeType As SizeIcon, ByVal sizeInt As Integer)
            Me.Size = sizeInt
            Dim size As Size = New Size(sizeInt, sizeInt)
            Select Case Type
                Case TypeElement.Folder
                    FileName = Path.GetFileName(FullPath)
                    If folder Is Nothing Then folder = GetLargeIconByte(FullPath, False)
                    Icon = folder
                Case TypeElement.File
                    FileName = Path.GetFileName(FullPath)
                    Dim ext As String = Path.GetExtension(FullPath)
                    If Equals(ext, ".exe") OrElse Equals(ext, ".lnk") Then
                        Icon = GetLargeIconByte(FullPath, True, sizeType)
                    ElseIf Not cache.ContainsKey(ext) Then
                        Dim bi As Byte() = GetLargeIconByte(FullPath, True, sizeType)
                        cache.Add(ext, bi)
                        Icon = bi
                    Else
                        Icon = cache(ext)
                    End If

                Case TypeElement.Drive
                    FileName = FullPath
                    Icon = GetLargeIconByte(FullPath, False)
                Case Else
            End Select
        End Sub

        Public Shared Sub ClearCache()
            Call cache.Clear()
            folder = Nothing
        End Sub
    End Class
End Namespace
