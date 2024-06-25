Imports System
Imports System.Windows
Imports System.Windows.Threading

Namespace GridDemo

    Public Partial Class ColumnChooser
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            UpdateToggleButtonContent()
            OptionsDataContext = Me
        End Sub

        Protected Overrides Sub ShowPopupContent()
            MyBase.ShowPopupContent()
            Dispatcher.BeginInvoke(New Action(AddressOf gridView.ShowColumnChooser), DispatcherPriority.Render, Nothing)
        End Sub

        Private Sub showHideButton_Toggle(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateToggleButtonContent()
        End Sub

        Private Sub UpdateToggleButtonContent()
            showHideButton.Content = If(showHideButton.IsChecked.Value, "Hide Column Chooser", "Show Column Chooser")
        End Sub
    End Class
End Namespace
