Imports DevExpress.Mvvm.POCO
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Windows.Media
Imports DevExpress.Mvvm.DataAnnotations

Namespace RibbonDemo

    Public Enum InlineImageBorderType
        <Display(Name:="None")>
        None
        <Display(Name:="Rectangle border")>
        Rectangle
        <Display(Name:="Circle border")>
        Circle
        <Display(Name:="Triangle border")>
        Triangle
        <Display(Name:="Star border")>
        Star
        <Display(Name:="Left arrow border")>
        LeftArrow
        <Display(Name:="Right arrow border")>
        RightArrow
        <Display(Name:="Up arrow border")>
        UpArrow
        <Display(Name:="Down arrow border")>
        DownArrow
    End Enum

    <POCOViewModel>
    Public Class InlineImageViewModel

        Public Overridable Property ImageSource As String

        Public Overridable Property Color As Color

        Public Overridable Property Scale As Double

        Public Overridable Property ShapeType As InlineImageBorderType

        Public Overridable Property BorderWeight As Double

        Public Overridable Property HasBorder As Boolean

        Protected Sub OnShapeTypeChanged()
            HasBorder = ShapeType <> InlineImageBorderType.None
        End Sub

        Protected Sub New()
        End Sub

        Public Shared Function Create(ByVal imageSource As String) As InlineImageViewModel
            Dim viewModel As InlineImageViewModel = ViewModelSource.Create(Function() New InlineImageViewModel())
            viewModel.Scale = 1
            viewModel.ShapeType = InlineImageBorderType.None
            viewModel.BorderWeight = 1
            viewModel.Color = Colors.Black
            viewModel.ImageSource = imageSource
            Return viewModel
        End Function
    End Class
End Namespace
