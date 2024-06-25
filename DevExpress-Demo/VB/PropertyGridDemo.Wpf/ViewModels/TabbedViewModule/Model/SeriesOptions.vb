Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Charts
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Windows.Media

Namespace PropertyGridDemo

    <MetadataType(GetType(Metadata.SeriesOptionsMetadata))>
    Public Class SeriesOptions

        Public Sub New()
            Fill = FillType.CreateFillOptions(Me)
            Border = BorderType.CreateBorderOptions(Me)
            Options = ViewModelSource.Create(Of CommonSeriesOptions)()
            Mirror = ViewModelSource.Create(Of MirrorOptions)()
            Label = ViewModelSource.Create(Of LabelOptions)()
            InitializeOptions()
        End Sub

        Private Sub InitializeOptions()
            FillType = FillOptionsType.SolidFill
            CType(Fill, SolidFillOptions).[Do](Sub(x)
                x.Color = Colors.LightGray
                x.Opacity = 0.5R
            End Sub)
            BorderType = BorderOptionsType.Solid
            CType(Border, SolidBorderOptions).[Do](Sub(x)
                x.Color = CType(ColorConverter.ConvertFromString("#FF093367"), Color)
                x.Opacity = 0.75
                x.Thickness = 7R
                x.DashStyle = DashStyles.Dash
            End Sub)
            ShowLabel = True
            Mirror.Show = True
            Options.Model = New BorderlessSimpleBar2DModel()
        End Sub

        <Category("FillnLine")>
        Public Overridable Property Fill As FillOptions

        Public Overridable Property FillType As FillOptionsType

        Public Sub OnFillTypeChanged()
            Fill = FillType.CreateFillOptions(Me)
        End Sub

        <Category("FillnLine")>
        Public Overridable Property Border As BorderOptions

        Public Overridable Property BorderType As BorderOptionsType

        Public Sub OnBorderTypeChanged()
            Border = BorderType.CreateBorderOptions(Me)
        End Sub

        <Category("Effects")>
        Public Overridable Property Label As LabelOptions

        Public Overridable Property ShowLabel As Boolean

        Protected Sub OnShowLabelChanged()
            Label = If(ShowLabel, ViewModelSource.Create(Of VisibleLabelOptions)(), ViewModelSource.Create(Of LabelOptions)())
        End Sub

        <Category("Effects")>
        Public Property Mirror As MirrorOptions

        <Category("SeriesOptions")>
        <DisplayName("Series Options")>
        Public Property Options As CommonSeriesOptions
    End Class

    <MetadataType(GetType(Metadata.CommonSeriesOptionsMetadata))>
    Public Class CommonSeriesOptions

        Public Sub New()
            Model = Bar2DModel.GetPredefinedKinds().First()
        End Sub

        Public Overridable Property Model As Object
    End Class
End Namespace
