Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Windows
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.DemoBase.Helpers.Internal
Imports DevExpress.Xpf.Grid

Namespace NavigationDemo

    Public Class SolutionExplorerBehavior
        Inherits Behavior(Of TreeViewControl)

        Public Property CodeView As CodeViewPresenter
            Get
                Return CType(GetValue(CodeViewProperty), CodeViewPresenter)
            End Get

            Set(ByVal value As CodeViewPresenter)
                SetValue(CodeViewProperty, value)
            End Set
        End Property

        Public Shared ReadOnly CodeViewProperty As DependencyProperty = DependencyProperty.Register("CodeView", GetType(CodeViewPresenter), GetType(SolutionExplorerBehavior), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Private ReadOnly timer As Timers.Timer = New Timers.Timer()

        Private ReadOnly stopwatch As Stopwatch = New Stopwatch()

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.CurrentItemChanged, AddressOf AssociatedObject_CurrentItemChanged
            AddHandler AssociatedObject.Loaded, AddressOf AssociatedObject_Loaded
            timer.Interval = 500
            AddHandler timer.Elapsed, AddressOf Timer_Elapsed
        End Sub

        Protected Overrides Sub OnDetaching()
            RemoveHandler AssociatedObject.Loaded, AddressOf AssociatedObject_Loaded
            RemoveHandler AssociatedObject.CurrentItemChanged, AddressOf AssociatedObject_CurrentItemChanged
            MyBase.OnDetaching()
        End Sub

        Private Sub AssociatedObject_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateCodeText()
        End Sub

        Private Sub AssociatedObject_CurrentItemChanged(ByVal sender As Object, ByVal e As CurrentItemChangedEventArgs)
            stopwatch.Restart()
            Dim old As SolutionNode = TryCast(e.OldItem, SolutionNode)
            UpdateCodeText(If(old IsNot Nothing, old.FileName, Nothing))
        End Sub

        Private Sub Timer_Elapsed(ByVal sender As Object, ByVal e As Timers.ElapsedEventArgs)
            If stopwatch.ElapsedMilliseconds > 999 Then
                timer.Stop()
                Dispatcher.Invoke(New Action(Sub()
                    Dim solutionNode = TryCast(AssociatedObject.CurrentItem, SolutionNode)
                    CodeView.UpdateTextPresenterFromCache(solutionNode.FileName)
                    CodeView.Search(solutionNode.SearchString, If(solutionNode.SearchName, solutionNode.Name))
                End Sub))
            End If
        End Sub

        Public Sub UpdateCodeText(ByVal Optional oldFileName As String = Nothing)
            Dim solutionNode = TryCast(AssociatedObject.CurrentItem, SolutionNode)
            If solutionNode IsNot Nothing AndAlso CodeView IsNot Nothing AndAlso CodeView.IsLoaded Then
                UpdateCache()
                If Not Equals(oldFileName, solutionNode.FileName) Then
                    timer.Start()
                    Return
                End If

                CodeView.Search(solutionNode.SearchString, If(solutionNode.SearchName, solutionNode.Name))
                Return
            End If
        End Sub

        Private Sub UpdateCache()
            If CodeView.CacheCount <> 0 Then Return
            For Each node In AssociatedObject.Nodes
                Dim solutionNode = TryCast(node.Content, SolutionNode)
                Dim uri = New Uri("pack://application:,,,/NavigationDemo;component/ViewModels/SolutionExplorer/CodeFiles/" & solutionNode.FileName, UriKind.Absolute)
                Dim code = Application.GetResourceStream(uri)
                Using reader As StreamReader = New StreamReader(code.Stream)
                    Dim str As StringBuilder = New StringBuilder()
                    Dim countLine As Integer = 0
                    While Not reader.EndOfStream
                        str.AppendLine(reader.ReadLine())
                        countLine += 1
                    End While

                    Dim codeText = New CodeLanguageText(DevExpress.Xpf.DemoBase.Helpers.TextColorizer.CodeLanguage.CS, str.ToString())
                    CodeView.AddCache(solutionNode.FileName, codeText)
                End Using
            Next
        End Sub
    End Class
End Namespace
