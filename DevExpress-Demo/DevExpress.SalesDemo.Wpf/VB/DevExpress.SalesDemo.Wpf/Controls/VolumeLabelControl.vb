Imports DevExpress.SalesDemo.Wpf.Converters
Imports DevExpress.Xpf.Charts
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media

Namespace DevExpress.SalesDemo.Wpf.Controls

    Public Class VolumeLabelControl
        Inherits ItemsControl

        Public Shared ReadOnly PaletteProperty As DependencyProperty = DependencyProperty.Register("Palette", GetType(CustomPalette), GetType(VolumeLabelControl), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly LabelForegroundProperty As DependencyProperty = DependencyProperty.Register("LabelForeground", GetType(Brush), GetType(VolumeLabelControl), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property Palette As CustomPalette
            Get
                Return CType(GetValue(PaletteProperty), CustomPalette)
            End Get

            Set(ByVal value As CustomPalette)
                SetValue(PaletteProperty, value)
            End Set
        End Property

        Public Property LabelForeground As Brush
            Get
                Return CType(GetValue(LabelForegroundProperty), Brush)
            End Get

            Set(ByVal value As Brush)
                SetValue(LabelForegroundProperty, value)
            End Set
        End Property

        Public Sub New()
            DefaultStyleKey = GetType(VolumeLabelControl)
        End Sub

        Public Function IndexOf(ByVal item As Object) As Integer
            If TypeOf item Is VolumeLabelItem Then
                Return ItemContainerGenerator.IndexFromContainer(CType(item, VolumeLabelItem))
            End If

            Return Items.IndexOf(item)
        End Function

        Protected Overrides Sub PrepareContainerForItemOverride(ByVal element As DependencyObject, ByVal item As Object)
            MyBase.PrepareContainerForItemOverride(element, item)
            If TypeOf element Is VolumeLabelItem Then CType(element, VolumeLabelItem).Initialize(Me)
        End Sub

        Protected Overrides Function GetContainerForItemOverride() As DependencyObject
            Return New VolumeLabelItem()
        End Function
    End Class

    Public Class VolumeLabelItem
        Inherits HeaderedContentControl

        Public Const ElementName_PART_Label As String = "PART_Label"

        Private Owner As VolumeLabelControl

        Private PaletteToBrushConverter As PaletteToBrushConverter

        Public Sub New()
            DefaultStyleKey = GetType(VolumeLabelItem)
            PaletteToBrushConverter = New PaletteToBrushConverter()
        End Sub

        Public Sub Initialize(ByVal owner As VolumeLabelControl)
            Me.Owner = owner
            PaletteToBrushConverter.Palette = Me.Owner.Palette
        End Sub

        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            If Owner Is Nothing Then Return
            Dim labelElement As TextBlock = CType(GetTemplateChild(ElementName_PART_Label), TextBlock)
            If Owner.LabelForeground IsNot Nothing Then
                labelElement.Foreground = Owner.LabelForeground
            ElseIf Owner.Palette IsNot Nothing Then
                labelElement.Foreground = CType(PaletteToBrushConverter.Convert(Owner.IndexOf(Me), GetType(Brush), Nothing, Nothing), Brush)
            Else
                labelElement.Foreground = New SolidColorBrush(Colors.Black)
            End If
        End Sub
    End Class
End Namespace
