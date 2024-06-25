Imports System.Collections.Generic
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Windows
Imports System.Windows.Media
Imports DevExpress.Data
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace WindowsUIDemo

    <CodeFile("ViewModels/NotificationsViewModel.(cs)")>
    Public Partial Class Notifications
        Inherits WindowsUIDemoModule

        Public Sub New()
            InitializeComponent()
#If CLICKONCE
            useWin8Notifications.IsChecked = false;
            useWin8Notifications.IsEnabled = false;
#End If
        End Sub

        Public Shared ReadOnly Property ApplicationID As String
            Get
                Return String.Format("Components_{0}_Demo_Center_{0}", AssemblyInfo.VersionShort.Replace(".", "_"))
            End Get
        End Property

        Public Shared ReadOnly Property ApplicationName As String
            Get
                Return "Notifications"
            End Get
        End Property

        Private ReadOnly Property ViewModel As NotificationsViewModel
            Get
                Return CType(DataContext, NotificationsViewModel)
            End Get
        End Property

        Private Property IsShortcutCreated As Boolean

        Private Function PatchBackground(ByVal stream As MemoryStream, ByVal color As Color) As MemoryStream
            Dim doc As String = Encoding.UTF8.GetString(stream.ToArray())
            Dim rx As Regex = New Regex("color=""#(.*?)""")
            Dim matches = rx.Matches(doc)
            If matches.Count > 0 Then
                Dim strColor As String = matches(0).Groups(1).ToString()
                doc = doc.Replace(strColor, color.ToString().Substring(3))
            End If

            Return New MemoryStream(Encoding.UTF8.GetBytes(doc))
        End Function

        Private Sub OnModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ActivatorLogger = ViewModel.LogService
            TryCreateApplicationShortcut()
        End Sub

        Private Sub OnModuleUnloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ActivatorLogger = Nothing
            TryRemoveApplicationShortcut()
        End Sub

        Private Sub TryCreateApplicationShortcut()
            Try
                IsShortcutCreated = True
                ShellHelper.TryCreateShortcut(ApplicationID, ApplicationName, Nothing, GetType(UIDemoNotificationActivator))
            Catch
                ActivatorLogger.LogLine("Cannot create application shortcut. Native" & Microsoft.VisualBasic.Constants.vbCrLf & "notifications are not available.")
                IsShortcutCreated = False
                useWin8Notifications.IsChecked = False
                useWin8Notifications.IsEnabled = False
            End Try
        End Sub

        Private Sub TryRemoveApplicationShortcut()
            If IsShortcutCreated Then ShellHelper.TryRemoveShortcut(ApplicationName)
        End Sub

        Private Shared ActivatorLogger As ILogService

        Public Shared Sub SendActivatorMessage(ByVal arguments As String)
            If ActivatorLogger IsNot Nothing Then
                ActivatorLogger.LogLine("Activator invoked! Notification id = " & arguments)
            End If
        End Sub
    End Class

    <Guid("45FD942D-1AD5-48E7-B139-4E1FB9F1F060"), ComVisible(True)>
    Public Class UIDemoNotificationActivator
        Inherits ToastNotificationActivator

        Public Overrides Sub OnActivate(ByVal arguments As String, ByVal data As Dictionary(Of String, String))
            Application.Current.Dispatcher.Invoke(Sub() Notifications.SendActivatorMessage(arguments))
        End Sub
    End Class
End Namespace
