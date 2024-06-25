Imports DevExpress.Internal
Imports DevExpress.Mvvm
Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Linq
Imports DialogsDemo.Helpers
Imports DevExpress.Xpf.Core
Imports System.Collections.Generic
Imports DevExpress.Xpf.Dialogs
Imports DevExpress.Mvvm.DataAnnotations
Imports CommonDemo.Helpers
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace DialogsDemo

    Public Class FileDialogsViewModel

        Private ReadOnly Property DefaultPhotoGalleryPath As String
            Get
                Return DialogsDemo.Helpers.DialogsDemoHelper.GetPhotosPath("Photos")
            End Get
        End Property

        Public ReadOnly Property AdditionalPhotosPath As String
            Get
                Return DialogsDemo.Helpers.DialogsDemoHelper.GetPhotosPath("AdditionalPhotos")
            End Get
        End Property

        Private ReadOnly Property DefaultPhotoExtension As String
            Get
                Return ".jpg"
            End Get
        End Property

        <DevExpress.Mvvm.DataAnnotations.BindablePropertyAttribute>
        Public Overridable Property Photos As ObservableCollection(Of CommonDemo.Helpers.PhotoData)

        <DevExpress.Mvvm.DataAnnotations.BindablePropertyAttribute>
        Public Overridable Property CurrentPhoto As PhotoData

        <DevExpress.Mvvm.DataAnnotations.BindablePropertyAttribute>
        Public Overridable Property DefaultExt As String

        <DevExpress.Mvvm.DataAnnotations.BindablePropertyAttribute>
        Public Overridable Property GalleryDirectory As String

        Public ReadOnly Property Filters As Filters
            Get
                Return DialogsDemo.Helpers.FiltersHelper.PhotoFilters
            End Get
        End Property

        Public Property Extensions As String()

        Public Sub New()
            Me.UpdateExtensions()
            Me.UpdateGallery(Me.DefaultPhotoGalleryPath)
        End Sub

        Private Sub UpdateExtensions()
            Me.Extensions = New String() {"JPG", "PNG", "BMP", "JPEG"}
            Me.DefaultExt = Me.Extensions.FirstOrDefault()
        End Sub

        Private Sub UpdateGallery(ByVal path As String)
            Me.GalleryDirectory = System.IO.Path.GetFullPath(path)
            Me.Photos = New System.Collections.ObjectModel.ObservableCollection(Of CommonDemo.Helpers.PhotoData)(CommonDemo.Helpers.ImageHelper.GetJPGPhotos(Me.GalleryDirectory))
            Me.CurrentPhoto = Me.Photos.FirstOrDefault()
        End Sub

        Public Sub ChangeGalleryDirectory()
            Dim folderBrowser = Me.GetFolderBrowserDialog()
            If folderBrowser.ShowDialog().Value Then
                Me.UpdateGallery(folderBrowser.FileName)
            End If
        End Sub

        Public Sub UploadPhotos()
            Dim openDialog = Me.GetOpenDialog()
            If openDialog.ShowDialog().Value Then
                For Each file In openDialog.FileNames
                    Me.Photos.Add(CommonDemo.Helpers.ImageHelper.GetJPGPhoto(file))
                    Me.CopyPhoto(file, Me.GalleryDirectory & "\" & System.IO.Path.GetFileName(file))
                Next
            End If
        End Sub

        Public Sub DownloadPhoto()
            Dim saveDialog = Me.GetSaveDialog()
            If saveDialog.ShowDialog().Value Then
                Dim file = saveDialog.FileName
                Me.CopyPhoto(System.IO.Path.Combine(Me.GalleryDirectory, Me.CurrentPhoto.PhotoName & Me.DefaultPhotoExtension), file)
            End If
        End Sub

        Public Function CanDownloadPhoto() As Boolean
            Return Me.CurrentPhoto IsNot Nothing
        End Function

        Private Sub CopyPhoto(ByVal sourceFilePath As String, ByVal destFileName As String)
            Try
                Call System.IO.File.Copy(sourceFilePath, destFileName, True)
            Catch e As System.Exception
                Call DevExpress.Xpf.Core.ThemedMessageBox.Show("Error", e.Message)
            End Try
        End Sub

#Region "dialogs"
        Private Function GetOpenDialog() As DXOpenFileDialog
            Return New DevExpress.Xpf.Dialogs.DXOpenFileDialog() With {.InitialDirectory = Me.AdditionalPhotosPath, .Filter = Me.Filters.GetFilterString(), .Title = "DX Open Dialog", .Multiselect = True}
        End Function

        Private Function GetSaveDialog() As DXSaveFileDialog
            Return New DevExpress.Xpf.Dialogs.DXSaveFileDialog() With {.InitialDirectory = Me.AdditionalPhotosPath, .DefaultExt = Me.DefaultExt, .Title = "DX Save Dialog"}
        End Function

        Private Function GetFolderBrowserDialog() As DXOpenFileDialog
            Return New DevExpress.Xpf.Dialogs.DXOpenFileDialog() With {.InitialDirectory = DialogsDemo.Helpers.DialogsDemoHelper.GetPhotosPath(String.Empty), .Title = "DX Open folder Dialog", .OpenFileDialogMode = DevExpress.Xpf.Dialogs.OpenFileDialogMode.Folders}
        End Function
#End Region
    End Class
End Namespace
