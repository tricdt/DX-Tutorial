Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Media
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public Class TornadoChartViewModel
        Inherits BindableBase

        Public Shared Function Create() As TornadoChartViewModel
            Return ViewModelSource.Create(Function() New TornadoChartViewModel())
        End Function

        Private firstKeys As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer) From {{"Male", 0}, {"Female", 1}}

        Private secondaryKeys As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer) From {{"0-14 years", 0}, {"15-64 years", 1}, {"65 years and older", 2}}

        Private panesField As List(Of PaneViewModel) = New List(Of PaneViewModel)() From {PaneViewModel.Create()}

        Private legendsField As List(Of LegendViewModel)

        Private selectedModelField As Bar2DModel

        Private isShowLabelsCheckedField As Boolean

        Public Overridable ReadOnly Property Legends As List(Of LegendViewModel)
            Get
                Return legendsField
            End Get
        End Property

        Public Overridable ReadOnly Property Panes As List(Of PaneViewModel)
            Get
                Return panesField
            End Get
        End Property

        Public Overridable ReadOnly Property Pane As PaneViewModel
            Get
                Return Panes(0)
            End Get
        End Property

        Public Overridable Property SelectedModel As Bar2DModel
            Get
                Return selectedModelField
            End Get

            Set(ByVal value As Bar2DModel)
                If selectedModelField Is value Then Return
                selectedModelField = value
                For Each item As GenderAgeItem In GenderAgeItems
                    item.Model = value
                Next
            End Set
        End Property

        Public Overridable Property IsShowLabelsChecked As Boolean
            Get
                Return isShowLabelsCheckedField
            End Get

            Set(ByVal value As Boolean)
                If isShowLabelsCheckedField = value Then Return
                IsShowToolTipsChecked = Not value
                isShowLabelsCheckedField = value
                For Each item As GenderAgeItem In GenderAgeItems
                    item.IsShowLabelsChecked = value
                Next
            End Set
        End Property

        Public Overridable Property IsShowToolTipsChecked As Boolean
            Get
                Return Not IsShowLabelsChecked
            End Get

            Set(ByVal value As Boolean)
                If IsShowToolTipsChecked = value Then Return
                For Each item As GenderAgeItem In GenderAgeItems
                    item.IsShowToolTipsChecked = value
                Next
            End Set
        End Property

        Public Overridable Property IsShowTotalLabelsChecked As Boolean
            Get
                Return Pane.IsShowTotalLabelsChecked
            End Get

            Set(ByVal value As Boolean)
                Pane.IsShowTotalLabelsChecked = value
            End Set
        End Property

        Public Overridable Property Palette As Palette

        Public Overridable Property GenderAgeItems As List(Of GenderAgeItem)

        Protected Sub New()
            InitLegends()
            GenderAgeItems = CType(New AgeStructure().GetGenderAgeItemsWithPopulation(), List(Of GenderAgeItem))
            For Each genderAgeItem As GenderAgeItem In GenderAgeItems
                SetSeriesLegend(genderAgeItem)
                genderAgeItem.Pane = Pane
            Next

            IsShowLabelsChecked = False
            IsShowToolTipsChecked = True
            Palette = New OfficePalette()
        End Sub

        Private Sub InitLegends()
            legendsField = New List(Of LegendViewModel)() From {LegendViewModel.Create(Pane), LegendViewModel.Create(Pane)}
        End Sub

        Private Sub SetSeriesLegend(ByVal genderAgeItem As GenderAgeItem)
            If genderAgeItem.Name.StartsWith("Female") Then
                genderAgeItem.Legend = Legends(0)
            Else
                genderAgeItem.Legend = Legends(1)
            End If
        End Sub

        Private Sub SetSeriesColor(ByVal genderAgeItem As GenderAgeItem)
            Dim name As String = genderAgeItem.Name
            Dim keys As String() = name.Split(":"c)
            If keys.Length = 2 Then
                keys(1) = keys(1).TrimStart()
                Dim index1, index2 As Integer
                If firstKeys.TryGetValue(keys(0), index1) AndAlso secondaryKeys.TryGetValue(keys(1), index2) Then
                    genderAgeItem.Color = Palette(index1 + index2 * Palette.Count)
                Else
                    genderAgeItem.Color = New Color()
                End If
            End If
        End Sub

        Protected Sub OnPaletteChanged()
            If Palette Is Nothing Then Return
            For Each genderAgeItem As GenderAgeItem In GenderAgeItems
                Palette.ColorCycleLength = secondaryKeys.Count * Palette.Count
                SetSeriesColor(genderAgeItem)
            Next
        End Sub
    End Class

    Public Class LegendViewModel
        Inherits BindableBase

        Public Shared Function Create(ByVal Pane As PaneViewModel) As LegendViewModel
            Return ViewModelSource.Create(Function() New LegendViewModel(Pane))
        End Function

        Public Overridable Property Pane As PaneViewModel

        Protected Sub New(ByVal Pane As PaneViewModel)
            Me.Pane = Pane
        End Sub
    End Class

    Public Class PaneViewModel
        Inherits BindableBase

        Public Shared Function Create() As PaneViewModel
            Return ViewModelSource.Create(Function() New PaneViewModel())
        End Function

        Public Overridable Property IsShowTotalLabelsChecked As Boolean

        Protected Sub New()
            IsShowTotalLabelsChecked = True
        End Sub
    End Class

    Public Class NegativeValueConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim textValue As String = value.ToString()
            If textValue(0) = "-"c Then Return textValue.Substring(1, textValue.Length - 1)
            Return textValue
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class ColorToBrushConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return New SolidColorBrush(CType(value, Color))
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class
End Namespace
