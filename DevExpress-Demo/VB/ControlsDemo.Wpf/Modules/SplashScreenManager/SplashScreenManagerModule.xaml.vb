Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase
Imports System
Imports System.Threading.Tasks
Imports System.Windows

Namespace ControlsDemo

    <CodeFile("Modules/SplashScreenManager/SplashScreenMainViewModel.(cs)")>
    <CodeFile("Modules/SplashScreenManager/SplashScreenManagerModule.xaml")>
    <CodeFile("Modules/SplashScreenManager/SplashScreenManagerModule.xaml.(cs)")>
    Public Partial Class SplashScreenManagerModule
        Inherits DemoModule

        Private _ViewModel As SplashScreenMainViewModel

        Public Property ViewModel As SplashScreenMainViewModel
            Get
                Return _ViewModel
            End Get

            Private Set(ByVal value As SplashScreenMainViewModel)
                _ViewModel = value
            End Set
        End Property

        Private cancelOperation As Boolean = False

        Public Sub New()
            InitializeComponent()
            ViewModel = CType(DataContext, SplashScreenMainViewModel)
            AddHandler ModuleUnloaded, AddressOf OnModuleUnloaded
        End Sub

        Private Sub OnModuleUnloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            cancelOperation = True
            Call SplashScreenManager.CloseAll()
        End Sub

        Private Sub OnShowHyperlinkClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ShowSplashScreen()
        End Sub

        Private Sub OnCloseHyperlinkClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call SplashScreenManager.CloseAll()
        End Sub

        Private Async Sub ShowSplashScreen()
            Dim isFluent As Boolean = Me.ViewModel.SplashScreenType.Equals(PredefinedSplashScreenType.Fluent)
            Dim viewModel = New DXSplashScreenViewModel() With {.Logo = New Uri(String.Format("pack://application:,,,/DevExpress.Xpf.DemoBase.v{0};component/DemoLauncher/Images/Logo.svg", AssemblyInfo.VersionShort)), .Status = "Starting...", .Title = If(isFluent, "Fluent Splash Screen", "Themed Splash Screen"), .Subtitle = "Powered by DevExpress", .Copyright = GetCopyright(), .IsIndeterminate = isFluent}
            Dim manager As SplashScreenManager
            If isFluent Then
                manager = SplashScreenManager.CreateFluent(viewModel)
            ElseIf Me.ViewModel.SplashScreenType.Equals(PredefinedSplashScreenType.Themed) Then
                manager = SplashScreenManager.CreateThemed(viewModel)
            Else
                manager = SplashScreenManager.CreateWaitIndicator(viewModel)
            End If

            cancelOperation = False
            manager.Show(Me.ViewModel.ShowDelay, Me.ViewModel.MinDuration, Me, Me.ViewModel.StartupLocation, Me.ViewModel.TrackOwnerPosition, Me.ViewModel.InputBlock)
            For i As Integer = 0 To 100
                If i = 50 Then
                    viewModel.Status = "Loading data..."
                ElseIf i = 80 Then
                    viewModel.Status = "Finishing..."
                End If

                viewModel.Progress = i
                If cancelOperation Then Exit For
                Await Task.Delay(60)
            Next

            manager.Close()
        End Sub

        Private Function GetCopyright() As String
            Return AssemblyInfo.AssemblyCopyright & If(AssemblyInfo.AssemblyCopyright.Contains("All rights reserved"), "", Microsoft.VisualBasic.Constants.vbLf & "All rights reserved")
        End Function
    End Class
End Namespace
