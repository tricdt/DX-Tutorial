Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Windows.Media
Imports DevExpress.Internal
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.POCO
Imports NavigationDemo.Utils

Namespace NavigationDemo

    Public Class PhotoStudioViewModel

        Private universalFilter As UniversalFilter

        Private contrastFilter As ContrastFilter

        Private allowUpdateColors As Boolean = True

        Private allowUpdateBrightness As Boolean = True

        Private allowUpdateContrast As Boolean = True

        Public Sub New()
            CreatePhotos()
            universalFilter = New UniversalFilter()
            contrastFilter = New ContrastFilter()
        End Sub

        Public Overridable Sub OnLoaded()
            SelectedPhoto = Photos.First()
            FilteredImage = SelectedPhoto.Image
            ClearAllFilters()
        End Sub

        Public Sub OnUnloaded()
        End Sub

        Public Overridable Property Photos As IEnumerable(Of PhotoInfo)

        Public Overridable Property SelectedPhoto As PhotoInfo

        Public Overridable Property FilteredImage As ImageSource

        Public Overridable Property Filters As IEnumerable(Of FilterViewModel)

        Public Overridable Property SelectedFilter As FilterViewModel

        Public Overridable Property Brightness As Integer

        Public Overridable Property Contrast As Integer

        <BindableProperty(OnPropertyChangedMethodName:="OnColorChanged")>
        Public Overridable Property RColor As Integer

        <BindableProperty(OnPropertyChangedMethodName:="OnColorChanged")>
        Public Overridable Property GColor As Integer

        <BindableProperty(OnPropertyChangedMethodName:="OnColorChanged")>
        Public Overridable Property BColor As Integer

        Protected Sub OnSelectedFilterChanged()
            UpdateFilteredImage()
        End Sub

        Protected Sub OnSelectedPhotoChanged()
            If SelectedPhoto Is Nothing Then Return
            UpdateFilters()
            ClearAllFilters()
            UpdateFilteredImage()
        End Sub

        Private Sub UpdateFilteredImage()
            If SelectedFilter Is Nothing Then
                FilteredImage = SelectedPhoto.Image
                Return
            End If

            ClearContrast()
            ClearColors()
            ClearBrightness()
            FilteredImage = SelectedFilter.Filter.ApplyFilter(SelectedPhoto.Uri)
            Return
        End Sub

        Protected Sub OnBrightnessChanged()
            If Not allowUpdateBrightness Then Return
            ClearContrast()
            ClearColors()
            SelectedFilter = Nothing
            FilteredImage = universalFilter.ApplyFilter(SelectedPhoto.Uri, Brightness, Brightness, Brightness)
        End Sub

        Protected Sub OnContrastChanged()
            If Not allowUpdateContrast Then Return
            ClearBrightness()
            ClearColors()
            SelectedFilter = Nothing
            FilteredImage = contrastFilter.ApplyFilter(SelectedPhoto.Uri, Contrast)
        End Sub

        Protected Sub OnColorChanged()
            If Not allowUpdateColors Then Return
            ClearContrast()
            ClearBrightness()
            SelectedFilter = Nothing
            FilteredImage = universalFilter.ApplyFilter(SelectedPhoto.Uri, RColor, GColor, BColor)
        End Sub

        Private Sub ClearAllFilters()
            ClearBrightness()
            ClearContrast()
            ClearColors()
            SelectedFilter = Nothing
        End Sub

        Private Sub ClearBrightness()
            allowUpdateBrightness = False
            Brightness = 1
            allowUpdateBrightness = True
        End Sub

        Private Sub ClearContrast()
            allowUpdateContrast = False
            Contrast = 1
            allowUpdateContrast = True
        End Sub

        Private Sub ClearColors()
            allowUpdateColors = False
            RColor = 1
            GColor = 1
            BColor = 1
            allowUpdateColors = True
        End Sub

        Private Sub UpdateFilters()
            Dim uri = SelectedPhoto.Uri
            If Filters IsNot Nothing Then
                Filters.ForEach(Sub(x) x.Uri = uri)
                Return
            End If

            Dim imageFactory = ViewModelSource.Factory(Function(ByVal uriString As String, ByVal filter As FilterBase) New FilterViewModel(uriString, filter))
            Filters = New FilterViewModel() {imageFactory(uri, New PolaroidFilter()), imageFactory(uri, New GBRFilter()), imageFactory(uri, New GrayScaleFilter()), imageFactory(uri, New BGRFilter()), imageFactory(uri, New SepiaFilter()), imageFactory(uri, New NegativeFilter())}
        End Sub

        Private Sub CreatePhotos()
            Dim random = New Random()
            Dim getDirectoryPath As Func(Of String, String) = Function(photosFolderName) DataDirectoryHelper.GetFile("Photos\" & photosFolderName, DataDirectoryHelper.DataFolderName)
            Dim getPhotoPaths As Func(Of String, String()) = Function(directoryPath) Directory.GetFiles(directoryPath, "*.jpg")
            Dim paths = getPhotoPaths(getDirectoryPath("Photos")).Concat(getPhotoPaths(getDirectoryPath("AdditionalPhotos")))
            Dim photoFactory = ViewModelSource.Factory(Function(ByVal uriString As String, ByVal rating As Integer) New PhotoInfo(uriString, rating))
            Photos = paths.[Select](Function(path) photoFactory(path, random.Next(5)))
        End Sub
    End Class
End Namespace
