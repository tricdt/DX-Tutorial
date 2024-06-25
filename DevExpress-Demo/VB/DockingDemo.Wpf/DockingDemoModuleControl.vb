Imports System.Diagnostics
Imports System.Linq
Imports System.Windows
Imports DevExpress.Xpf
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Docking

Namespace DockingDemo

    Public Class DockingDemoModule
        Inherits DemoModule

        Private _IsModuleLoaded As Boolean

        Public Shared ReadOnly ShouldRestoreOnActivateProperty As DependencyProperty = DependencyProperty.RegisterAttached("ShouldRestoreOnActivate", GetType(Boolean), GetType(DockingDemoModule))

        Public Sub New()
            AddHandler Loaded, AddressOf OnLoaded
            AddHandler ModuleLoaded, AddressOf OnModuleLoaded
        End Sub

        Protected Property IsModuleLoaded As Boolean
            Get
                Return _IsModuleLoaded
            End Get

            Private Set(ByVal value As Boolean)
                _IsModuleLoaded = value
            End Set
        End Property

        Public Shared Function GetShouldRestoreOnActivate(ByVal target As DependencyObject) As Boolean
            Return CBool(target.GetValue(ShouldRestoreOnActivateProperty))
        End Function

        Public Shared Sub SetShouldRestoreOnActivate(ByVal target As DependencyObject, ByVal value As Boolean)
            target.SetValue(ShouldRestoreOnActivateProperty, value)
        End Sub

        Protected Overrides Sub HidePopupContent()
            Dim containerList = DevExpress.Mvvm.UI.LayoutTreeHelper.GetVisualChildren(Me).OfType(Of DockLayoutManager)()
            For Each container In containerList
                HideFloatGroups(container)
            Next

            MyBase.HidePopupContent()
        End Sub

        Protected Sub NavigateHomePage()
            Call Process.Start(New ProcessStartInfo With {.FileName = "http://www.devexpress.com", .UseShellExecute = True})
        End Sub

        Protected Overridable Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If IsModuleLoaded Then Return
            Dim containerList = DevExpress.Mvvm.UI.LayoutTreeHelper.GetVisualChildren(Me).OfType(Of DockLayoutManager)()
            For Each container In containerList
                HideFloatGroups(container)
            Next
        End Sub

        Protected Sub ShowAbout()
            Dim platformName As String = "WPF"
            Dim ati As AboutToolInfo = New AboutToolInfo()
            ati.LicenseState = LicenseState.Licensed
            ati.ProductEULAUri = "http://www.devexpress.com/"
            ati.ProductName = "DXDocking for " & platformName
            ati.Version = AssemblyInfo.MarketingVersion
            Dim tAbout As ToolAbout = New ToolAbout(ati)
            Dim aWindow As AboutWindow = New AboutWindow()
            aWindow.Content = tAbout
            aWindow.ShowDialog()
        End Sub

        Protected Overrides Sub ShowPopupContent()
            MyBase.ShowPopupContent()
            Dim containerList = DevExpress.Mvvm.UI.LayoutTreeHelper.GetVisualChildren(Me).OfType(Of DockLayoutManager)()
            For Each container In containerList
                ShowFloatGroups(container)
            Next
        End Sub

        Private Sub HideFloatGroups(ByVal dockLayoutManager As DockLayoutManager)
            If dockLayoutManager.IsDisposing Then Return
            For Each floatGroup As FloatGroup In dockLayoutManager.FloatGroups
                If floatGroup.IsOpen Then
                    SetShouldRestoreOnActivate(floatGroup, True)
                    floatGroup.IsOpen = False
                End If
            Next
        End Sub

        Private Sub OnModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            IsModuleLoaded = True
        End Sub

        Private Sub ShowFloatGroups(ByVal dockLayoutManager As DockLayoutManager)
            If dockLayoutManager.IsDisposing Then Return
            For Each floatGroup As FloatGroup In dockLayoutManager.FloatGroups
                If GetShouldRestoreOnActivate(floatGroup) Then
                    SetShouldRestoreOnActivate(floatGroup, False)
                    If Not dockLayoutManager.IsVisible Then
                        floatGroup.ShouldRestoreOnActivate = True
                    Else
                        floatGroup.IsOpen = True
                    End If
                End If
            Next
        End Sub
    End Class
End Namespace
