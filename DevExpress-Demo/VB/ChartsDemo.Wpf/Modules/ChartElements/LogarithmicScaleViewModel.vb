Imports System.Windows.Media
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public Class LogarithmicScaleViewModel

        Public Shared Function Create() As LogarithmicScaleViewModel
            Return ViewModelSource.Create(Function() New LogarithmicScaleViewModel())
        End Function

        Public Overridable Property Palette As Palette

        Public Overridable Property Headphones190dBSPLBrush As SolidColorBrush

        Public Overridable Property Headphones1100dBSPLBrush As SolidColorBrush

        Public Overridable Property Headphones290dBSPLBrush As SolidColorBrush

        Public Overridable Property Headphones2100dBSPLBrush As SolidColorBrush

        Protected Sub New()
            Palette = New OfficePalette()
        End Sub

        Protected Sub OnPaletteChanged()
            If Palette Is Nothing Then Return
            Headphones190dBSPLBrush = New SolidColorBrush(Palette(0))
            Headphones1100dBSPLBrush = New SolidColorBrush(ChangeColorLuminance(Palette(0), 0.8F))
            Headphones290dBSPLBrush = New SolidColorBrush(Palette(1))
            Headphones2100dBSPLBrush = New SolidColorBrush(ChangeColorLuminance(Palette(1), 0.8F))
            Headphones190dBSPLBrush.Freeze()
            Headphones1100dBSPLBrush.Freeze()
            Headphones290dBSPLBrush.Freeze()
            Headphones2100dBSPLBrush.Freeze()
        End Sub
    End Class
End Namespace
