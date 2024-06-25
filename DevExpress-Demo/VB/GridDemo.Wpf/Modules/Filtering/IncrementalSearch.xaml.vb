Imports System.Windows.Controls

Namespace GridDemo.Filtering

    Public Partial Class IncrementalSearch
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
            view.IncrementalSearchStart("M")
        End Sub
    End Class
End Namespace
