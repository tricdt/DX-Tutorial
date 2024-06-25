Imports System.Diagnostics
Imports System.Windows

Namespace PivotGridDemo

#If Not DXCORE3
    <DevExpress.Xpf.DemoBase.CodeFile("Helpers/ServerMode/ServerModeViewModel.(cs)")>
    <DevExpress.Xpf.DemoBase.CodeFile("Helpers/ServerMode/OrderDataContext.(cs)")>
    <DevExpress.Xpf.DemoBase.CodeFile("Helpers/ServerMode/ServerModeViewModelBase.(cs)")>
    Public Partial Class ServerMode
        Inherits PivotGridDemoModule

#Else
    [DevExpress.Xpf.DemoBase.CodeFile("Helpers/ServerMode/ServerModeViewModelBase.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("Helpers/ServerMode/ServerModeViewModelNetCore.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("Helpers/ServerMode/OrderDataContextNetCore.(cs)")]
    public partial class ServerMode : PivotGridDemoModule {
#End If
        Private ReadOnly timer As Stopwatch = New Stopwatch()

        Private asyncCompleted As Date = Date.Now

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub pivotGrid_AsyncOperationStarting(ByVal sender As Object, ByVal e As RoutedEventArgs)
            tbTimeTaken.Text = "..."
            If Not timer.IsRunning Then
                If(Date.Now - asyncCompleted).TotalMilliseconds < 100 Then
                    timer.Start()
                Else
                    timer.Restart()
                End If
            End If
        End Sub

        Private Sub pivotGrid_AsyncOperationCompleted(ByVal sender As Object, ByVal e As RoutedEventArgs)
            timer.Stop()
            tbTimeTaken.Text = timer.ElapsedMilliseconds.ToString()
            asyncCompleted = Date.Now
        End Sub
    End Class
End Namespace
