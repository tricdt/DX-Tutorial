Imports CommonDemo.Helpers
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Core
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Windows.Media

Namespace ControlsDemo.TabControl.PinnedTabs

    Public Class MainViewModel

        Public Shared Function Create() As MainViewModel
            Return ViewModelSource.Create(Function() New MainViewModel())
        End Function

        Protected Sub New()
            Photos = New ObservableCollection(Of NaturePhoto)()
            For i As Integer = 0 To 6 - 1
                Dim photo = ImagesHelper.NaturePhotos.ElementAt(i)
                Photos.Add(NaturePhoto.Create("Nature Photo #" & i.ToString(), photo))
            Next

            Photos(5).PinMode = TabPinMode.Right
        End Sub

        Public Overridable Property Photos As ObservableCollection(Of NaturePhoto)

        Public Sub AddNewTab(ByVal e As TabControlTabAddingEventArgs)
            e.Item = NaturePhoto.Create("New Las Vegas Photo", ImagesHelper.GetRandomLasVegasPhoto())
        End Sub
    End Class

    Public Class NaturePhoto

        Private _Title As String, _Photo As ImageSource

        Public Shared Function Create(ByVal title As String, ByVal photo As ImageSource) As NaturePhoto
            Return ViewModelSource.Create(Function() New NaturePhoto(title, photo))
        End Function

        Protected Sub New(ByVal title As String, ByVal photo As ImageSource)
            Me.Title = title
            Me.Photo = photo
        End Sub

        Public Property Title As String
            Get
                Return _Title
            End Get

            Private Set(ByVal value As String)
                _Title = value
            End Set
        End Property

        Public Property Photo As ImageSource
            Get
                Return _Photo
            End Get

            Private Set(ByVal value As ImageSource)
                _Photo = value
            End Set
        End Property

        Public Overridable Property PinMode As TabPinMode

        Public Sub Pin(ByVal mode As TabPinMode)
            PinMode = mode
        End Sub

        Public Function CanPin(ByVal mode As TabPinMode) As Boolean
            Return Not PinMode.Equals(mode)
        End Function
    End Class
End Namespace
