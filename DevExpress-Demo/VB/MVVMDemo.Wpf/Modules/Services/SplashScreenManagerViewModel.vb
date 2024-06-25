Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System
Imports System.Threading.Tasks

Namespace MVVMDemo.Services

    Public Class SplashScreenManagerViewModel

#Region "!"
        Public Async Sub ShowSplashScreen(ByVal serviceName As String)
            Dim splashScreenService As ISplashScreenManagerService = GetRequiredService(Of ISplashScreenManagerService)(serviceName)
            splashScreenService.ViewModel = CreateViewModel()
            splashScreenService.Show()
            Await Task.Delay(3000)
            splashScreenService.Close()
        End Sub

#End Region
        Private Function CreateViewModel() As DXSplashScreenViewModel
            Return New DXSplashScreenViewModel() With {.Logo = New Uri("pack://application:,,,/DevExpress.Xpf.DemoBase.v20.1;component/DemoLauncher/Images/Logo.svg"), .Status = "Initializing...", .Subtitle = "Powered by DevExpress", .Copyright = GetCopyright(), .IsIndeterminate = True}
        End Function

        Private Function GetCopyright() As String
            Return AssemblyInfo.AssemblyCopyright & If(AssemblyInfo.AssemblyCopyright.Contains("All rights reserved"), "", Microsoft.VisualBasic.Constants.vbLf & "All rights reserved")
        End Function
    End Class
End Namespace
