Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Ribbon
Imports DevExpress.Xpf.Accordion
Imports System.Collections
Imports DevExpress.DevAV.ViewModels

Namespace DevExpress.DevAV.Views

    Public Partial Class DevAVDbView
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
        End Sub
    End Class

    Public Class OutlookChildrenSelector
        Implements IChildrenSelector

        Private Function SelectChildren(ByVal item As Object) As IEnumerable Implements IChildrenSelector.SelectChildren
            If TypeOf item Is DevAVDbModuleDescription Then
                Return CType(item, DevAVDbModuleDescription).FilterTreeViewModel.Categories
            ElseIf TypeOf item Is FilterCategory Then
                Return CType(item, FilterCategory).FilterItems
            End If

            Return Nothing
        End Function
    End Class

    Public Class ObjectsEqualityConverter
        Implements Windows.Data.IValueConverter

        Public Property Inverse As Boolean

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements Windows.Data.IValueConverter.Convert
            If value Is Nothing Then Return value
            Dim result = Equals(value.ToString(), parameter)
            Return If(Inverse, Not result, result)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements Windows.Data.IValueConverter.ConvertBack
            Return If(CBool(value), parameter, Nothing)
        End Function
    End Class

    Public Class RibbonStyleSelectorItem
        Inherits BarSplitButtonItem

        Private Shared globalRibbonStyleField As RibbonStyle = Xpf.Ribbon.RibbonStyle.Office2010

        Public Shared Property GlobalRibbonStyle As RibbonStyle
            Get
                Return globalRibbonStyleField
            End Get

            Set(ByVal value As RibbonStyle)
                If Equals(value, globalRibbonStyleField) Then Return
                globalRibbonStyleField = value
                RaiseEvent GlobalRibbonStyleChanged(Nothing, EventArgs.Empty)
            End Set
        End Property

        Public Shared Event GlobalRibbonStyleChanged As EventHandler(Of EventArgs)

        Shared Sub New()
            Call DefaultStyleKeyProperty.OverrideMetadata(GetType(RibbonStyleSelectorItem), New FrameworkPropertyMetadata(GetType(RibbonStyleSelectorItem)))
            GlobalRibbonStyleChangedEvent = Sub(s, e)
            End Sub
        End Sub

        Private popupContentControl As ContentControl = New ContentControl()

        Public Sub New()
            ActAsDropDown = True
            ItemClickBehaviour = PopupItemClickBehaviour.CloseAllPopups
            PopupControl = New PopupControlContainer() With {.Content = popupContentControl}
        End Sub

        Protected Overrides Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MyBase.OnLoaded(sender, e)
            SelectedRibbonStyle = GlobalRibbonStyle
            AddHandler GlobalRibbonStyleChanged, AddressOf RibbonStyleSelectorItem_GlobalRibbonStyleChanged
        End Sub

        Protected Overrides Sub OnUnloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MyBase.OnUnloaded(sender, e)
            RemoveHandler GlobalRibbonStyleChanged, AddressOf RibbonStyleSelectorItem_GlobalRibbonStyleChanged
        End Sub

        Private Sub RibbonStyleSelectorItem_GlobalRibbonStyleChanged(ByVal sender As Object, ByVal e As EventArgs)
            SelectedRibbonStyle = GlobalRibbonStyle
        End Sub

        Public Property SelectedRibbonStyle As RibbonStyle
            Get
                Return CType(GetValue(SelectedRibbonStyleProperty), RibbonStyle)
            End Get

            Set(ByVal value As RibbonStyle)
                SetValue(SelectedRibbonStyleProperty, value)
            End Set
        End Property

        Public Shared ReadOnly SelectedRibbonStyleProperty As DependencyProperty = DependencyProperty.Register("SelectedRibbonStyle", GetType(RibbonStyle), GetType(RibbonStyleSelectorItem), New PropertyMetadata(Xpf.Ribbon.RibbonStyle.Office2010, AddressOf OnSelectedRibbonStyleChanged))

        Private Shared Sub OnSelectedRibbonStyleChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            GlobalRibbonStyle = CType(e.NewValue, RibbonStyle)
        End Sub

        Public Property PopupTemplate As DataTemplate
            Get
                Return CType(GetValue(PopupTemplateProperty), DataTemplate)
            End Get

            Set(ByVal value As DataTemplate)
                SetValue(PopupTemplateProperty, value)
            End Set
        End Property

        Public Shared ReadOnly PopupTemplateProperty As DependencyProperty = DependencyProperty.Register("PopupTemplate", GetType(DataTemplate), GetType(RibbonStyleSelectorItem), New PropertyMetadata(Nothing, AddressOf OnPopupTemplatePropertyChanged))

        Private Shared Sub OnPopupTemplatePropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, RibbonStyleSelectorItem).popupContentControl.ContentTemplate = CType(e.NewValue, DataTemplate)
            CType(d, RibbonStyleSelectorItem).popupContentControl.Content = New With {.Selector = d}
        End Sub
    End Class
End Namespace
