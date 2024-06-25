Imports DevExpress.Xpf.Grid
Imports System.Windows
Imports System.Windows.Controls

Namespace GanttDemo

    Public Partial Class Events
        Inherits GanttDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class EventNodeTemplateSelector
        Inherits DataTemplateSelector

        Public Property EventTemplate As DataTemplate

        Public Property OtherTemplate As DataTemplate

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim node = TryCast(CType(item, GridCellData).RowData.Row, GanttEventNode)
            Return If(node IsNot Nothing AndAlso node.Kind = GanttEventNodeKind.EventNode, EventTemplate, OtherTemplate)
        End Function
    End Class
End Namespace
