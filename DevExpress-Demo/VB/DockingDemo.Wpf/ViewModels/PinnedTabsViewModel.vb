Imports DockingDemo.Helpers
Imports DevExpress.Mvvm.POCO
Imports System.Linq
Imports System.Collections.ObjectModel
Imports DevExpress.Xpf.Layout.Core

Namespace DockingDemo.ViewModels

    Public Class PinnedTabsViewModel

        Public Shared Function Create() As PinnedTabsViewModel
            Return ViewModelSource.Create(Function() New PinnedTabsViewModel())
        End Function

        Protected Sub New()
            Photos = New ObservableCollection(Of NaturePhoto)()
            For i As Integer = 0 To 6 - 1
                Dim photo = ImagesHelper.NaturePhotos.ElementAt(i)
                Photos.Add(NaturePhoto.Create("Nature Photo #" & i.ToString(), photo))
            Next

            Photos(0).TogglePin()
            Photos(0).PinLocation = TabHeaderPinLocation.Far
        End Sub

        Public Overridable Property Photos As ObservableCollection(Of NaturePhoto)
    End Class

    Public Class NaturePhoto

        Private _Title As String, _Photo As String

        Public Shared Function Create(ByVal title As String, ByVal photo As String) As NaturePhoto
            Return ViewModelSource.Create(Function() New NaturePhoto(title, photo))
        End Function

        Protected Sub New(ByVal title As String, ByVal photo As String)
            Me.Title = title
            Me.Photo = photo
            Pinned = False
        End Sub

        Public Property Title As String
            Get
                Return _Title
            End Get

            Private Set(ByVal value As String)
                _Title = value
            End Set
        End Property

        Public Property Photo As String
            Get
                Return _Photo
            End Get

            Private Set(ByVal value As String)
                _Photo = value
            End Set
        End Property

        Public Property Pinned As Boolean

        Public Property PinLocation As TabHeaderPinLocation

        Public Sub TogglePin()
            Pinned = Not Pinned
        End Sub
    End Class
End Namespace
