Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Editors

Namespace EditorsDemo

    Public Partial Class DateTimeMaskEdit
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            AddHandler Loaded, New RoutedEventHandler(AddressOf Me.DateTimeMaskEdit_Loaded)
        End Sub

        Private Sub DateTimeMaskEdit_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            full.Focus()
        End Sub

        Private Sub EditorGotFocus(ByVal sender As Object, ByVal e As RoutedEventArgs)
            mask.FocusedEditor = TryCast(sender, TextEdit)
        End Sub
    End Class
End Namespace
