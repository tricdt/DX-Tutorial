Imports System.Windows.Controls
Imports DevExpress.Data.PLinq.Helpers
Imports GridDemo

Namespace EditorsDemo

    Public Partial Class ComboBoxEditWithPLINQSource
        Inherits EditorsDemoModule

        Public Sub New()
            PLinqServerModeCore.DefaultForceCaseInsensitiveForAnySource = True
            Dim viewModel As PLinqInstantFeedbackDemoViewModel = New PLinqInstantFeedbackDemoViewModel(False)
            viewModel.CountItems = New CountItemCollection() From {New CountItem() With {.Count = 100000, .DisplayName = "Small"}, New CountItem() With {.Count = 1000000, .DisplayName = "Medium"}, New CountItem() With {.Count = 3000000, .DisplayName = "Large"}}
            viewModel.SelectedCountItem = viewModel.CountItems(1)
            DataContext = viewModel
            InitializeComponent()
        End Sub
    End Class
End Namespace
