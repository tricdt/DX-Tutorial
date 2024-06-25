Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Utils
Imports System.Windows
Imports System.Windows.Documents

Namespace BarsDemo

    Public Partial Class ContextMenu
        Inherits BarsDemoModule

        Public Property MenuButton As ButtonSwitcher
            Get
                Return CType(GetValue(MenuButtonProperty), ButtonSwitcher)
            End Get

            Set(ByVal value As ButtonSwitcher)
                SetValue(MenuButtonProperty, value)
            End Set
        End Property

        Public Shared ReadOnly MenuButtonProperty As DependencyProperty = DependencyPropertyManager.Register("MenuButton", GetType(ButtonSwitcher), GetType(ContextMenu), New FrameworkPropertyMetadata())

        Public Sub New()
            InitializeComponent()
            AddHandler ModuleLoaded, AddressOf OnModuleLoaded
            CheckMouseSwitcher()
            edit.ContextMenu = Nothing
        End Sub

        Private Sub OnModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            edit.SetFocus()
        End Sub

        Private Sub OnRadioButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            CheckMouseSwitcher()
        End Sub

        Private Sub CheckMouseSwitcher()
            If CBool(Left.IsChecked) Then MenuButton = ButtonSwitcher.LeftButton
            If CBool(LeftRight.IsChecked) Then MenuButton = ButtonSwitcher.LeftRightButton
            If CBool(Right.IsChecked) Then MenuButton = ButtonSwitcher.RightButton
            UpdateText(MenuButton)
        End Sub

        Private Sub UpdateText(ByVal MenuButton As ButtonSwitcher)
            Dim text As String = String.Empty
            Select Case MenuButton
                Case ButtonSwitcher.LeftButton
                    text = "Left click here to show a context menu"
                Case ButtonSwitcher.LeftRightButton
                    text = "Left or right click here to show a context menu"
                Case ButtonSwitcher.RightButton
                    text = "Right click here to show a context menu"
            End Select

            edit.Clear()
            edit.Document.Blocks.Add(New Paragraph(New Run(text)))
        End Sub
    End Class
End Namespace
