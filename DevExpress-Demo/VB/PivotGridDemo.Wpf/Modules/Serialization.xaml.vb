Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.PivotGrid
Imports DevExpress.Xpf.Editors

Namespace PivotGridDemo

    Public Partial Class Serialization
        Inherits PivotGridDemoModule

        Private currentLayoutStream As MemoryStream

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub LoadSampleButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim item = CType(layoutSamplesComboBox.SelectedItem, ComboBoxEditItem)
            Dim stream = CType(item.Tag, Stream)
            RestoreLayout(stream)
            pivotGrid.BestFit(FieldArea.RowArea, True, False)
            pivotGrid.BestFitColumn(pivotGrid.ColumnCount - 1)
        End Sub

        Private Sub SaveLayoutButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            currentLayoutStream = SaveLayout()
            restoreLayoutButton.IsEnabled = True
        End Sub

        Private Sub RestoreLayoutButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RestoreLayout(currentLayoutStream)
        End Sub

        Private Sub PivotGridDemoModule_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            AddLayoutSampleItem("Original", SaveLayout())
            AddLayoutSampleItem("Brief view", GetResourceStream("BriefView"))
            AddLayoutSampleItem("Full view", GetResourceStream("FullView"))
            layoutSamplesComboBox.SelectedIndex = 0
        End Sub

        Private Sub AddLayoutSampleItem(ByVal name As String, ByVal stream As Stream)
            layoutSamplesComboBox.Items.Add(New ComboBoxEditItem() With {.Content = name, .Tag = stream})
        End Sub

        Private Function GetResourceStream(ByVal name As String) As Stream
            Dim assembly As Assembly = GetType(Serialization).Assembly
            Dim resourcePath = DemoHelper.GetPath("PivotGridDemo.Data.LayoutSamples.", assembly) & name & ".xml"
            Return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath)
        End Function

        Private Function SaveLayout() As MemoryStream
            Dim stream As MemoryStream = New MemoryStream()
            pivotGrid.SaveLayoutToStream(stream)
            Return stream
        End Function

        Private Sub RestoreLayout(ByVal stream As Stream)
            If stream Is Nothing Then Return
            stream.Position = 0
            pivotGrid.RestoreLayoutFromStream(stream)
        End Sub
    End Class
End Namespace
