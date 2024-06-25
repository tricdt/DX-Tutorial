Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Utils

Namespace BarsDemo

    Public Class BarsDemoModule
        Inherits DemoModule

        Public Property GoDevExpressCommand As ICommand
            Get
                Return CType(GetValue(GoDevExpressCommandProperty), ICommand)
            End Get

            Set(ByVal value As ICommand)
                SetValue(GoDevExpressCommandProperty, value)
            End Set
        End Property

        Public Shared ReadOnly GoDevExpressCommandProperty As DependencyProperty = DependencyProperty.Register("GoDevExpressCommand", GetType(ICommand), GetType(BarsDemoModule), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property ShowAboutCommand As ICommand
            Get
                Return CType(GetValue(ShowAboutCommandProperty), ICommand)
            End Get

            Set(ByVal value As ICommand)
                SetValue(ShowAboutCommandProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ShowAboutCommandProperty As DependencyProperty = DependencyProperty.Register("ShowAboutCommand", GetType(ICommand), GetType(BarsDemoModule), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly BarManagerProperty As DependencyProperty = DependencyPropertyManager.Register("BarManager", GetType(BarManager), GetType(BarsDemoModule), New FrameworkPropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property Manager As BarManager
            Get
                Return CType(GetValue(BarManagerProperty), BarManager)
            End Get

            Set(ByVal value As BarManager)
                SetValue(BarManagerProperty, value)
            End Set
        End Property

        Shared Sub New()
            Call BarNameScope.IsScopeOwnerProperty.OverrideMetadata(GetType(BarsDemoModule), New FrameworkPropertyMetadata(True))
        End Sub

        Public Sub New()
            If Not IsInDesignTool() Then
                Margin = New Thickness(25)
                BorderThickness = New Thickness(1)
            End If

            AddHandler Loaded, AddressOf BarsDemoModule_Loaded
            ShowAboutCommand = New DelegateCommand(AddressOf ShowAboutExecute)
            GoDevExpressCommand = New DelegateCommand(AddressOf GoDevExpressExecute)
        End Sub

        Private Sub BarsDemoModule_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateBorder()
        End Sub

        Private Sub UpdateBorder()
            If Equals(Theme.MetropolisLightName, ThemeManager.GetThemeName(Me)) Then
                BorderBrush = New SolidColorBrush(Colors.DarkGray)
            Else
                Dim color As Color = TryCast(TextElement.GetForeground(Me), SolidColorBrush).Color
                BorderBrush = New SolidColorBrush(Color.FromArgb(50, color.R, color.G, color.B))
            End If
        End Sub

        Private Sub ShowAboutExecute()
            Dim demoBase = LayoutHelper.FindLayoutOrVisualParentObject(Of DemoBaseControl)(Me)
            demoBase.ShowAbout()
        End Sub

        Private Sub GoDevExpressExecute()
            Call Process.Start(New ProcessStartInfo With {.FileName = "http://www.devexpress.com", .UseShellExecute = True})
        End Sub
    End Class
End Namespace
