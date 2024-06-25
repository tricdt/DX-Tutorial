Imports DevExpress.Mvvm.UI
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.Settings
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Threading

Namespace BarsDemo

    Public Class RadialContextMenuViewModel

#Region "properties"
        Private _FontSizes As IEnumerable(Of Double?), _FontFamilies As IEnumerable(Of String)

        Public Property FontSizes As IEnumerable(Of Double?)
            Get
                Return _FontSizes
            End Get

            Protected Set(ByVal value As IEnumerable(Of Double?))
                _FontSizes = value
            End Set
        End Property

        Public Property FontFamilies As IEnumerable(Of String)
            Get
                Return _FontFamilies
            End Get

            Protected Set(ByVal value As IEnumerable(Of String))
                _FontFamilies = value
            End Set
        End Property

#End Region
        Public Sub New()
            FontSizes = New Double?() {3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 88, 96, 104, 112, 120, 128, 136, 144}
            FontFamilies = GetSystemFonts()
        End Sub

        Private Shared Function GetSystemFonts() As IEnumerable(Of String)
            Dim result As List(Of FontFamily) = New List(Of FontFamily)()
            Dim lang = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)
            Return FontUtility.GetSystemFontFamilies().[Select](Function(t) GetFontFamilyName(t, lang)).OrderBy(Function(t) t)
        End Function

        Private Shared Function GetFontFamilyName(ByVal fontFamily As FontFamily, ByVal lang As XmlLanguage) As String
            Try
                Return If(fontFamily.FamilyNames.ContainsKey(lang), fontFamily.FamilyNames(lang), fontFamily.ToString())
            Catch
            End Try

            Return fontFamily.ToString()
        End Function
    End Class

    Public Class ClosePopupOnListEditClickBehaviour
        Inherits Behavior(Of ListBoxEdit)

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.PreviewMouseLeftButtonUp, AddressOf AssociatedObject_PreviewMouseLeftButtonUp
            AddHandler AssociatedObject.KeyDown, AddressOf AssociatedObject_KeyDown
            AddHandler AssociatedObject.Loaded, AddressOf AssociatedObject_Loaded
        End Sub

        Private Sub AssociatedObject_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            AssociatedObject.ScrollIntoView(AssociatedObject.SelectedItem)
            AssociatedObject.Focus()
        End Sub

        Private Sub AssociatedObject_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
            If e.Key = Key.Enter OrElse e.Key = Key.Space Then
                ClosePopupAsync()
                e.Handled = True
            End If
        End Sub

        Private Sub AssociatedObject_PreviewMouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            If LayoutTreeHelper.GetVisualParents(TryCast(e.OriginalSource, DependencyObject)).OfType(Of ListBoxItem)().Count() = 1 Then ClosePopupAsync()
        End Sub

        Private Sub ClosePopupAsync()
            Dispatcher.BeginInvoke(New Action(AddressOf ClosePopup), DispatcherPriority.SystemIdle, New Object() {})
        End Sub

        Private Sub ClosePopup()
            CType(Enumerable.First(LayoutTreeHelper.GetVisualParents(AssociatedObject).OfType(Of BarPopupBorderControl)()).Popup, BarPopupBase).ClosePopup()
        End Sub
    End Class
End Namespace
