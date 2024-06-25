Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Core
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Media
Imports System.Runtime.CompilerServices

Namespace PropertyGridDemo

    Public Enum BorderOptionsType
        Empty
        Solid
    End Enum

    Public Module BorderOptionsExtensions

        <Extension()>
        Public Function CreateBorderOptions(ByVal type As PropertyGridDemo.BorderOptionsType, ByVal options As PropertyGridDemo.SeriesOptions) As BorderOptions
            Select Case type
                Case PropertyGridDemo.BorderOptionsType.Solid
                    Return DevExpress.Mvvm.POCO.ViewModelSource.Factory(Of PropertyGridDemo.SeriesOptions, PropertyGridDemo.SolidBorderOptions)(Function(x) New PropertyGridDemo.SolidBorderOptions(x))(options)
            End Select

            Return DevExpress.Mvvm.POCO.ViewModelSource.Factory(Of PropertyGridDemo.SeriesOptions, PropertyGridDemo.BorderOptions)(Function(x) New PropertyGridDemo.BorderOptions(x))(options)
        End Function
    End Module

    <System.ComponentModel.DataAnnotations.MetadataTypeAttribute(GetType(PropertyGridDemo.Metadata.BorderOptionsDataMetadata))>
    Public Class BorderOptionsData

        Public Sub New()
            Me.Color = System.Windows.Media.Colors.Transparent
            Me.Opacity = 0R
            Me.Thickness = 0R
            Me.DashStyle = System.Windows.Media.DashStyles.Solid
        End Sub

        Public Property Color As Color

        Public Property Opacity As Double

        Public Property Thickness As Double

        Public Overridable Property DashStyle As DashStyle
    End Class

    <System.ComponentModel.DataAnnotations.MetadataTypeAttribute(GetType(PropertyGridDemo.Metadata.BorderOptionsMetadata))>
    Public Class BorderOptions
        Inherits PropertyGridDemo.BaseOptions

        Public Sub New()
        End Sub

        Public Property BorderType As BorderOptionsType

        Public Sub New(ByVal root As PropertyGridDemo.SeriesOptions)
            MyBase.New(root)
        End Sub

        Public Overridable Property Data As BorderOptionsData
    End Class

    <System.ComponentModel.DataAnnotations.MetadataTypeAttribute(GetType(PropertyGridDemo.Metadata.SolidBorderOptionsMetadata))>
    Public Class SolidBorderOptions
        Inherits PropertyGridDemo.BorderOptions

        Public Sub New()
            Me.New(Nothing)
        End Sub

        Public Sub New(ByVal root As PropertyGridDemo.SeriesOptions)
            MyBase.New(root)
            Me.Color = System.Windows.Media.Colors.Black
            Me.Opacity = 1R
            Me.Thickness = 0R
            Me.DashStyle = System.Windows.Media.DashStyles.Solid
        End Sub

        Public Overridable Property Color As Color

        Public Overridable Property Opacity As Double

        Public Overridable Property Thickness As Double

        Public Overridable Property DashStyle As DashStyle

        Protected Sub OnColorChanged()
            Me.Opacity = CDbl(Me.Color.A) / 255
            Me.UpdateData()
        End Sub

        Protected Sub OnOpacityChanged()
            Me.Color = New System.Windows.Media.Color() With {.A = CByte((Me.Opacity * 255)), .R = Me.Color.R, .G = Me.Color.G, .B = Me.Color.B}
            Me.UpdateData()
        End Sub

        Protected Sub OnThicknessChanged()
            Me.UpdateData()
        End Sub

        Protected Sub OnDashStyleChanged()
            Me.UpdateData()
        End Sub

        Protected Sub UpdateData()
            Me.Data = New PropertyGridDemo.BorderOptionsData() With {.Color = Me.Color, .Opacity = Me.Opacity, .Thickness = Me.Thickness, .DashStyle = Me.DashStyle}
        End Sub
    End Class
End Namespace
