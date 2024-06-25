Imports CommonDemo.Helpers
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Core
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Windows.Media

Namespace CommonDemo.TabControl.WebBrowser

    Public Class WebBrowserMainViewModel

        Public Overridable Property Tabs As ObservableCollection(Of TabViewModel)

        Public Overridable Property SpeedDials As ObservableCollection(Of SpeedDialViewModel)

        Public Shared Function Create() As WebBrowserMainViewModel
            Return ViewModelSource.Create(Function() New WebBrowserMainViewModel())
        End Function

        Protected Sub New()
            Tabs = New ObservableCollection(Of TabViewModel)()
            Tabs.Add(TabViewModel.CreateNewTabViewModel())
            SpeedDials = New ObservableCollection(Of SpeedDialViewModel)()
            SpeedDials.Add(New SpeedDialViewModel(ImagesHelper.GetWebIcon("Microsoft.png"), New Uri("http://www.microsoft.com", UriKind.Absolute), "Microsoft"))
            SpeedDials.Add(New SpeedDialViewModel(ImagesHelper.GetWebIcon("Google.png"), New Uri("http://www.google.com", UriKind.Absolute), "Google"))
            SpeedDials.Add(New SpeedDialViewModel(ImagesHelper.GetWebIcon("Devexpress.png"), New Uri("http://www.devexpress.com", UriKind.Absolute), "DevExpress"))
            SpeedDials.Add(New SpeedDialViewModel(ImagesHelper.GetWebIcon("VisualStudio.png"), New Uri("http://www.visualstudio.com", UriKind.Absolute), "Visual Studio"))
            SpeedDials.Add(New SpeedDialViewModel(ImagesHelper.GetWebIcon("Stackoverflow.png"), New Uri("http://www.stackoverflow.com", UriKind.Absolute), "Stackoverflow"))
            SpeedDials.Add(New SpeedDialViewModel(ImagesHelper.GetWebIcon("Facebook.png"), New Uri("http://www.facebook.com", UriKind.Absolute), "Facebook"))
            SpeedDials.Add(New SpeedDialViewModel(ImagesHelper.GetWebIcon("Twitter.png"), New Uri("http://www.twitter.com", UriKind.Absolute), "Twitter"))
            SpeedDials.Add(New SpeedDialViewModel(ImagesHelper.GetWebIcon("Youtube.png"), New Uri("http://www.youtube.com", UriKind.Absolute), "Youtube"))
            SpeedDials.Add(New SpeedDialViewModel(ImagesHelper.GetWebIcon("Amazon.png"), New Uri("http://www.amazon.com", UriKind.Absolute), "Amazon"))
        End Sub

        Public Sub AddNewTab(ByVal e As TabControlTabAddingEventArgs)
            e.Item = TabViewModel.CreateNewTabViewModel()
        End Sub
    End Class

    Public Class TabViewModel

        Public Overridable Property IsNewTab As Boolean

        Public Overridable Property Title As String

        Public Overridable Property Url As Uri

        Public Shared Function CreateNewTabViewModel() As TabViewModel
            Return ViewModelSource.Create(Function() New TabViewModel())
        End Function

        Protected Sub New()
            IsNewTab = True
            Title = "Speed Dial"
        End Sub

        Public Sub LoadSpeedDial(ByVal speedDial As SpeedDialViewModel)
            IsNewTab = False
            Title = speedDial.Title
            Url = speedDial.Url
        End Sub
    End Class

    Public Class SpeedDialViewModel

        Private _Icon As ImageSource, _Url As Uri, _Title As String

        Public Property Icon As ImageSource
            Get
                Return _Icon
            End Get

            Private Set(ByVal value As ImageSource)
                _Icon = value
            End Set
        End Property

        Public Property Url As Uri
            Get
                Return _Url
            End Get

            Private Set(ByVal value As Uri)
                _Url = value
            End Set
        End Property

        Public Property Title As String
            Get
                Return _Title
            End Get

            Private Set(ByVal value As String)
                _Title = value
            End Set
        End Property

        Public Sub New(ByVal icon As ImageSource, ByVal url As Uri, ByVal title As String)
            Me.Icon = icon
            Me.Url = url
            Me.Title = title
        End Sub
    End Class
End Namespace
