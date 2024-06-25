Imports DevExpress.Mvvm.UI.Interactivity
Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Windows.Controls

Namespace CommonDemo.Helpers

    Public Class WebBrowserSupressScriptErrorsBehavior
        Inherits Behavior(Of WebBrowser)

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.Navigated, AddressOf OnWebBrowserNavigated
        End Sub

        Protected Overrides Sub OnDetaching()
            RemoveHandler AssociatedObject.Navigated, AddressOf OnWebBrowserNavigated
            MyBase.OnDetaching()
        End Sub

        Private Sub OnWebBrowserNavigated(ByVal sender As Object, ByVal e As Windows.Navigation.NavigationEventArgs)
            Call SetSilent(CType(AssociatedObject, WebBrowser), True)
        End Sub

        Private Shared IID_IWebBrowserApp As Guid = New Guid("0002DF05-0000-0000-C000-000000000046")

        Private Shared IID_IWebBrowser2 As Guid = New Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E")

        Private Shared Sub SetSilent(ByVal browser As WebBrowser, ByVal silent As Boolean)
            Dim provider As IOleServiceProvider = TryCast(browser.Document, IOleServiceProvider)
            If provider Is Nothing Then Return
            Dim webBrowser As Object
            provider.QueryService(IID_IWebBrowserApp, IID_IWebBrowser2, webBrowser)
            If webBrowser Is Nothing Then Return
            Dim webBrowserType As Type = webBrowser.GetType()
            webBrowserType.InvokeMember("Silent", BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.PutDispProperty, Nothing, webBrowser, New Object() {silent})
        End Sub

        <ComImport, Guid("6D5140C1-7436-11CE-8034-00AA006009FA"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
        Friend Interface IOleServiceProvider

            <PreserveSig>
            Function QueryService(<[In]> ByRef guidService As Guid, <[In]> ByRef riid As Guid, <Out> <MarshalAs(UnmanagedType.IDispatch)> ByRef ppvObject As Object) As Integer

        End Interface
    End Class
End Namespace
