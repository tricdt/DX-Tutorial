Imports System
Imports System.Diagnostics
Imports System.Reflection
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Windows
Imports System.Windows.Controls

Namespace DevExpress.Internal

    Public Class ExceptionHelper

        Shared Sub New()
            IsEnabled = True
        End Sub

        Public Shared Property IsEnabled As Boolean

        Public Shared Sub Initialize()
            AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf CurrentDomain_UnhandledException
        End Sub

        Private Shared arguments As UnhandledExceptionEventArgs

        Private Shared Sub CurrentDomain_UnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
            Try
                If Debugger.IsAttached OrElse Not IsEnabled Then Return
                arguments = e
                Call ShowWindow()
            Catch __unusedException1__ As Exception
            End Try
        End Sub

        Private Shared Sub ShowWindow()
            Dim message As String = GetMessage()
            Dim window = New Window() With {.Width = 600, .Height = 400, .WindowStyle = WindowStyle.ToolWindow, .ShowActivated = True, .Title = "Unhandled exception"}
            Dim grid = New Grid() With {.Margin = New Thickness(5)}
            Dim closeButton = New Button() With {.Content = "Close", .Margin = New Thickness(3)}
            AddHandler closeButton.Click, AddressOf button_Click
            Dim copyButton = New Button() With {.Content = "Copy error", .Margin = New Thickness(3)}
            AddHandler copyButton.Click, AddressOf copyButton_Click
            Dim stackPanel = New StackPanel() With {.Orientation = Orientation.Horizontal, .HorizontalAlignment = HorizontalAlignment.Center}
            Call Grid.SetRow(stackPanel, 1)
            stackPanel.Children.Add(closeButton)
            stackPanel.Children.Add(copyButton)
            grid.RowDefinitions.Add(New RowDefinition() With {.Height = New GridLength(1, GridUnitType.Star)})
            grid.RowDefinitions.Add(New RowDefinition() With {.Height = GridLength.Auto})
            grid.Children.Add(stackPanel)
            grid.Children.Add(New TextBox() With {.Text = message, .Margin = New Thickness(5)})
            window.Content = grid
            window.ShowDialog()
            Environment.Exit(1)
        End Sub

        Private Shared Function GetMessage() As String
            Dim ex As Exception = CType(arguments.ExceptionObject, Exception)
            Dim result = New StringBuilder()
            If Assembly.GetEntryAssembly() IsNot Nothing Then
                result.Append("EntryAssembly: ")
                result.Append(Assembly.GetEntryAssembly().Location)
                result.AppendLine()
            End If

            result.AppendLine("UnhandledException:")
            PackException(ex, result)
            Return result.ToString()
        End Function

        Private Shared Sub PackException(ByVal ex As Exception, ByVal stringBuilder As StringBuilder, ByVal Optional index As Integer = 0)
            AppendLine(stringBuilder, ex.Message, index)
            If Not String.IsNullOrWhiteSpace(ex.StackTrace) Then
                AppendLine(stringBuilder, "StackTrace:", index)
                AppendLine(stringBuilder, ex.StackTrace, index)
            End If

            If ex.InnerException IsNot Nothing Then
                AppendLine(stringBuilder, "InnerException:", index)
                index += 1
                PackException(ex.InnerException, stringBuilder, index)
            End If
        End Sub

        Private Shared Sub AppendLine(ByVal stringBuilder As StringBuilder, ByVal text As String, ByVal index As Integer)
            Dim tabOffset As String = New String(Microsoft.VisualBasic.Strings.ChrW(9), index)
            stringBuilder.Append(tabOffset)
            Dim regex = New Regex(Microsoft.VisualBasic.Constants.vbCrLf & "*\s")
            stringBuilder.AppendLine(regex.Replace(text, Microsoft.VisualBasic.Constants.vbCrLf & tabOffset))
        End Sub

        Private Shared Sub copyButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call Clipboard.SetText(GetMessage())
        End Sub

        Private Shared Sub button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Environment.Exit(1)
        End Sub
    End Class
End Namespace
