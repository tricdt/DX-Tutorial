Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Grid

Namespace DiagramDemo

    <CodeFile("ViewModels/DiagramEventsInfo.(cs)")>
    <CodeFile("ViewModels/DiagramEventNode.(cs)")>
    <CodeFile("ViewModels/EventsViewModel.(cs)")>
    <CodeFile("Helpers/ScrollToEndService.(cs)")>
    Public Partial Class EventsModule
        Inherits DiagramDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class EventNodeTemplateSelector
        Inherits DataTemplateSelector

        Public Property EventTemplate As DataTemplate

        Public Property OtherTemplate As DataTemplate

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim node = TryCast(CType(item, GridCellData).RowData.Row, DiagramEventNode)
            Return If(node IsNot Nothing AndAlso node.Kind = DiagramEventNodeKind.EventNode, EventTemplate, OtherTemplate)
        End Function
    End Class
End Namespace
