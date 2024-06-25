Imports System.Collections.Generic
Imports System.Windows.Media

Namespace TreeListDemo.Data

    Public Class FileSystemImages

        Private Shared ReadOnly images As Dictionary(Of String, ImageSource) = New Dictionary(Of String, ImageSource)()

        Private Shared Function ImageGet(ByVal name As String) As ImageSource
            Dim image As ImageSource = Nothing
            If images.TryGetValue(name, image) Then Return image
            image = GetSvgImage(name)
            images.Add(name, image)
            Return image
        End Function

        Public Shared ReadOnly Property FileImage As ImageSource
            Get
                Return ImageGet("File")
            End Get
        End Property

        Public Shared ReadOnly Property DiskImage As ImageSource
            Get
                Return ImageGet("Local_Disk")
            End Get
        End Property

        Public Shared ReadOnly Property ClosedFolderImage As ImageSource
            Get
                Return ImageGet("Folder_Closed")
            End Get
        End Property

        Public Shared ReadOnly Property OpenedFolderImage As ImageSource
            Get
                Return ImageGet("Folder_Opened")
            End Get
        End Property
    End Class
End Namespace
