Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.Editors
Imports System.IO
Imports System.Reflection
Imports System.Windows

Namespace GridDemo

    Public Partial Class Serialization
        Inherits GridDemoModule

        Private currentLayoutStream As MemoryStream

        Public Sub New()
            InitializeComponent()
            AddHandler Loaded, AddressOf OnLoaded
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            AddLayoutSampleItem("Original", SaveLayout())
            AddLayoutSampleItem("Brief view", GetResourceStream("BriefView"))
            AddLayoutSampleItem("Full view", GetResourceStream("FullView"))
            AddLayoutSampleItem("Banded layout", GetResourceStream("BandedLayout"))
            layoutSamplesComboBox.SelectedIndex = 0
        End Sub

        Private Sub AddLayoutSampleItem(ByVal name As String, ByVal stream As Stream)
            layoutSamplesComboBox.Items.Add(New ComboBoxEditItem() With {.Content = name, .Tag = stream})
        End Sub

        Private Function GetResourceStream(ByVal name As String) As Stream
            Dim assembly As Assembly = GetType(Serialization).Assembly
            Dim resourcePath = DemoHelper.GetPath("GridDemo.Data.LayoutSamples.", assembly) & name & ".xml"
            Return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath)
        End Function

        Private Function SaveLayout() As MemoryStream
            Dim stream As MemoryStream = New MemoryStream()
            grid.SaveLayoutToStream(stream)
            Return stream
        End Function

        Private Sub RestoreLayout(ByVal stream As Stream)
            If stream Is Nothing Then Return
            stream.Position = 0
            grid.RestoreLayoutFromStream(stream)
        End Sub

        Private Sub saveLayoutButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            currentLayoutStream = SaveLayout()
            restoreLayoutButton.IsEnabled = True
        End Sub

        Private Sub restoreLayoutButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RestoreLayout(currentLayoutStream)
        End Sub

        Private Sub loadSampleLayoutButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim item = CType(layoutSamplesComboBox.SelectedItem, ComboBoxEditItem)
            Dim stream = CType(item.Tag, Stream)
            RestoreLayout(stream)
        End Sub
    End Class
End Namespace
