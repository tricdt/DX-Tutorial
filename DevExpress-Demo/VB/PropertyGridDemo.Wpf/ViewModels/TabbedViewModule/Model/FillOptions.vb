Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.POCO
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Runtime.CompilerServices

Namespace PropertyGridDemo

    Public Enum FillOptionsType
        NoFill
        SolidFill
        PictureFill
    End Enum

    <System.ComponentModel.DataAnnotations.MetadataTypeAttribute(GetType(PropertyGridDemo.Metadata.FillOptionsMetadata))>
    Public Class FillOptions
        Inherits PropertyGridDemo.BaseOptions

        Public Sub New()
        End Sub

        Public Sub New(ByVal root As PropertyGridDemo.SeriesOptions)
            MyBase.New(root)
        End Sub

        Public Property FillType As FillOptionsType

        Public Overridable Property Result As Brush
    End Class

    <System.ComponentModel.DataAnnotations.MetadataTypeAttribute(GetType(PropertyGridDemo.Metadata.SolidFillOptionsMetadata))>
    Public Class SolidFillOptions
        Inherits PropertyGridDemo.FillOptions

        Public Sub New()
        End Sub

        Public Sub New(ByVal root As PropertyGridDemo.SeriesOptions)
            MyBase.New(root)
        End Sub

        Public Overridable Property Color As Color

        Public Overridable Property Opacity As [Double]

        Protected Sub OnColorChanged()
            Me.Opacity = CDbl(Me.Color.A) / 255
            Me.UpdateBrush()
        End Sub

        Protected Sub OnOpacityChanged()
            Me.Color = New System.Windows.Media.Color() With {.A = CByte((Me.Opacity * 255)), .R = Me.Color.R, .G = Me.Color.G, .B = Me.Color.B}
            Me.UpdateBrush()
        End Sub

        Private Sub UpdateBrush()
            Me.Result = New System.Windows.Media.SolidColorBrush(Me.Color)
            Me.Result.Freeze()
        End Sub
    End Class

    <System.ComponentModel.DataAnnotations.MetadataTypeAttribute(GetType(PropertyGridDemo.Metadata.PictureFillOptionsMetadata))>
    Public Class PictureFillOptions
        Inherits PropertyGridDemo.FillOptions

        Public Sub New()
            Me.New(Nothing)
        End Sub

        Public Sub New(ByVal root As PropertyGridDemo.SeriesOptions)
            MyBase.New(root)
            Me.Opacity = 1
        End Sub

        Public Overridable Property Picture As BitmapImage

        Public Overridable Property Opacity As [Double]

        Protected Overridable ReadOnly Property OpenFileDialogService As IOpenFileDialogService
            Get
                Return Nothing
            End Get
        End Property

        Public Sub SelectPicture()
            If Me.OpenFileDialogService.ShowDialog() Then
                Me.Picture = New System.Windows.Media.Imaging.BitmapImage(New System.Uri(Me.OpenFileDialogService.GetFullFileName()))
            End If
        End Sub

        Public Function CanClearPicture() As Boolean
            Return Me.Picture IsNot Nothing
        End Function

        Public Sub ClearPicture()
            Me.Picture = Nothing
        End Sub

        Protected Sub OnPictureChanged()
            Me.UpdateBrush()
        End Sub

        Protected Sub OnOpacityChanged()
            Me.UpdateBrush()
        End Sub

        Private Sub UpdateBrush()
            Me.Result = Me.Picture.[With](Function(x) New System.Windows.Media.ImageBrush(x) With {.Opacity = Me.Opacity})
        End Sub
    End Class

    Public Module FillOptionsExtensions

        <Extension()>
        Public Function CreateFillOptions(ByVal type As PropertyGridDemo.FillOptionsType, ByVal options As PropertyGridDemo.SeriesOptions) As FillOptions
            Select Case type
                Case PropertyGridDemo.FillOptionsType.SolidFill
                    Return DevExpress.Mvvm.POCO.ViewModelSource.Factory(Of PropertyGridDemo.SeriesOptions, PropertyGridDemo.SolidFillOptions)(Function(x) New PropertyGridDemo.SolidFillOptions(x))(options)
                Case PropertyGridDemo.FillOptionsType.PictureFill
                    Return DevExpress.Mvvm.POCO.ViewModelSource.Factory(Of PropertyGridDemo.SeriesOptions, PropertyGridDemo.PictureFillOptions)(Function(x) New PropertyGridDemo.PictureFillOptions(x))(options)
            End Select

            Return DevExpress.Mvvm.POCO.ViewModelSource.Factory(Of PropertyGridDemo.SeriesOptions, PropertyGridDemo.FillOptions)(Function(x) New PropertyGridDemo.FillOptions(x))(options)
        End Function
    End Module
End Namespace
