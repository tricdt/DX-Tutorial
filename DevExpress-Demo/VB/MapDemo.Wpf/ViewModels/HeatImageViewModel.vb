Imports System.Collections.Generic
Imports System.Windows.Media
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Map

Namespace MapDemo

    <POCOViewModel>
    Public Class HeatImageViewModel

        Public Property PaletteItems As List(Of PaletteItem)

        Public Overridable Property ActualPaletteItem As PaletteItem

        Public Sub New()
            CreatePaletteItems()
            ActualPaletteItem = PaletteItems(1)
        End Sub

        Private Sub CreatePaletteItems()
            Dim paletteItems As List(Of PaletteItem) = New List(Of PaletteItem)()
            paletteItems.Add(New PaletteItem("Default", Nothing))
            paletteItems.Add(New PaletteItem("Hot", CreateHotColorizer()))
            paletteItems.Add(New PaletteItem("Cold", CreateColdColorizer()))
            Me.PaletteItems = paletteItems
        End Sub

        Private Function CreateHotColorizer() As ChoroplethColorizer
            Dim colorizer As ChoroplethColorizer = New ChoroplethColorizer()
            colorizer.RangeStops = New DoubleCollection() From {0.1, 0.2, 0.7, 1.0}
            colorizer.Colors.Add(Color.FromArgb(50, 128, 255, 0))
            colorizer.Colors.Add(Color.FromArgb(255, 255, 255, 0))
            colorizer.Colors.Add(Color.FromArgb(255, 234, 72, 58))
            colorizer.Colors.Add(Color.FromArgb(255, 162, 36, 25))
            colorizer.ApproximateColors = True
            Return colorizer
        End Function

        Private Function CreateColdColorizer() As ChoroplethColorizer
            Dim colorizer As ChoroplethColorizer = New ChoroplethColorizer()
            colorizer.RangeStops = New DoubleCollection() From {0.0, 0.2, 0.4, 0.6, 0.8, 1.0}
            colorizer.Colors.Add(Color.FromArgb(50, 33, 102, 172))
            colorizer.Colors.Add(Color.FromArgb(255, 103, 169, 207))
            colorizer.Colors.Add(Color.FromArgb(255, 209, 229, 240))
            colorizer.Colors.Add(Color.FromArgb(255, 253, 219, 199))
            colorizer.Colors.Add(Color.FromArgb(255, 239, 138, 98))
            colorizer.Colors.Add(Color.FromArgb(255, 178, 24, 43))
            colorizer.ApproximateColors = True
            Return colorizer
        End Function

        Public Sub OnLegendItemCreating(ByVal e As LegendItemCreatingEventArgs)
            Dim endIndex As Integer = If(ActualPaletteItem.Colorizer IsNot Nothing, ActualPaletteItem.Colorizer.Colors.Count - 1, 3)
            If e.Index = 0 Then
                e.Item.Text = "low"
            ElseIf e.Index = endIndex Then
                e.Item.Text = "high"
            Else
                e.Item.Text = " "
            End If
        End Sub
    End Class

    Public Class PaletteItem

        Private _Name As String, _Colorizer As ChoroplethColorizer

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property Colorizer As ChoroplethColorizer
            Get
                Return _Colorizer
            End Get

            Private Set(ByVal value As ChoroplethColorizer)
                _Colorizer = value
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal colorizer As ChoroplethColorizer)
            Me.Name = name
            Me.Colorizer = colorizer
        End Sub
    End Class
End Namespace
