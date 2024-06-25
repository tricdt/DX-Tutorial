Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Grid

Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("Controls/SalesByEmployeeData.cs")>
    <DevExpress.Xpf.DemoBase.CodeFile("Controls/ColumnDescription.cs")>
    Public Partial Class FixedBands
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class ColumnTemplateSelector
        Inherits DataTemplateSelector

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim column As ColumnDescription = CType(item, ColumnDescription)
            Dim grid = CType(container, GridControlBand).View.DataControl
            If String.IsNullOrEmpty(column.StyleKey) Then Return CType(grid.FindResource("columnTemplate"), DataTemplate)
            Return CType(grid.FindResource(column.StyleKey), DataTemplate)
        End Function
    End Class
End Namespace
