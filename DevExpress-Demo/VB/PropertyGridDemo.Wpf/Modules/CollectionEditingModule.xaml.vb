Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Media
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.PropertyGrid
Imports DevExpress.Mvvm.Native

Namespace PropertyGridDemo

    Public Partial Class CollectionEditingModule
        Inherits PropertyGridDemoModule

        Public Sub New()
            InitializeComponent()
            Dim itemInitializer = CType(Resources("itemInitializer"), VisualizerItemInitializer)
        End Sub

        Private Sub OnVisualizerPanelLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            grid.ItemsSource = New List(Of Object) From {root}
        End Sub

        Private Sub OnVisualizerPanelChanged(ByVal sender As Object, ByVal e As EventArgs)
            If grid IsNot Nothing Then grid.RefreshData()
        End Sub

        Private Sub pGrid_CustomExpand(ByVal sender As Object, ByVal args As CustomExpandEventArgs)
            If args.IsInitializing Then
                args.Row.Children.FirstOrDefault()
            End If
        End Sub

        Private Sub pGrid_CollectionButtonsVisibility(ByVal sender As Object, ByVal e As CollectionButtonsVisibilityEventArgs)
            If e.ButtonKind = CollectionButtonKind.Add Then
                Dim collection = CType(e.Value, UIElementCollection)
                e.IsVisible = collection.Count <= 5
            End If

            If e.ButtonKind = CollectionButtonKind.Remove Then
                Dim button = TryCast(e.Value, Button)
                If button IsNot Nothing AndAlso String.Equals(TryCast(button.Content, String), "Button", StringComparison.Ordinal) Then e.IsVisible = False
            End If
        End Sub

        Private Sub pGrid_CollectionButtonClick(ByVal sender As Object, ByVal e As CollectionButtonClickEventArgs)
            If e.ButtonKind = CollectionButtonKind.Add Then
                Dim collection = CType(e.Value, UIElementCollection)
                e.DefaultAction.Invoke(New Button With {.Content = "Button " & collection.OfType(Of Button)().Count()})
                e.Handled = True
            End If
        End Sub
    End Class

    Public Class VisualItemsSelector
        Implements IChildNodesSelector

        Private ReadOnly initializer As VisualizerItemInitializer = New VisualizerItemInitializer()

        Private Function SelectChildren(ByVal item As Object) As IEnumerable Implements IChildNodesSelector.SelectChildren
            Dim list As List(Of FrameworkElement) = New List(Of FrameworkElement)()
            Dim fe As FrameworkElement = CType(item, FrameworkElement)
            If CType(initializer, IInstanceInitializer).Types.Any(Function(typeInfo) item.GetType() Is typeInfo.Type) Then Return Nothing
            For i As Integer = 0 To VisualTreeHelper.GetChildrenCount(fe) - 1
                list.Add(CType(VisualTreeHelper.GetChild(fe, i), FrameworkElement))
            Next

            Return list
        End Function
    End Class

    Public Class ItemsSourceConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return New List(Of Object) From {value}
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class GetNameConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return If(value IsNot Nothing, value.GetType().Name, String.Empty)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class VisualizerItemInitializer
        Implements IInstanceInitializer

        Public Sub New()
        End Sub

        Private Function CreateInstance(ByVal type As TypeInfo) As Object Implements IInstanceInitializer.CreateInstance
            Dim element As FrameworkElement = Nothing
            If type.Type Is GetType(Button) Then element = New Button() With {.Content = type.Name}
            If type.Type Is GetType(CheckBox) Then element = New CheckBox() With {.Content = type.Name}
            If type.Type Is GetType(TextBox) Then element = New TextBox() With {.Text = type.Name}
            element.HorizontalAlignment = HorizontalAlignment.Center
            element.Margin = New Thickness(5)
            Return element
        End Function

        Private ReadOnly Property Types As IEnumerable(Of TypeInfo) Implements IInstanceInitializer.Types
            Get
                Dim result = New List(Of TypeInfo)()
                result.Add(New TypeInfo(GetType(Button), "New Button"))
                result.Add(New TypeInfo(GetType(TextBox), "New Text Editor"))
                result.Add(New TypeInfo(GetType(CheckBox), "New CheckBox"))
                Return result
            End Get
        End Property
    End Class
End Namespace
