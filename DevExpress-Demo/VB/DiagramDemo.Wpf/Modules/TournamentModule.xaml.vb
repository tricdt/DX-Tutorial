Imports System.Windows
Imports DevExpress.Diagram.Core
Imports DevExpress.Diagram.Core.Routing
Imports DevExpress.Diagram.Demos
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo

    <CodeFile("Data/TournamentData.(cs)")>
    Public Partial Class TournamentModule
        Inherits DiagramDemoModule

        Public Sub New()
            InitializeComponent()
            diagramControl.Controller.RegisterRoutingStrategy(ConnectorType.RightAngle, New RightAngleRoutingStrategy() With {.ItemMargin = CInt(diagramControl.TreeLayoutHorizontalSpacing) \ 2})
            DataContext = New TournamentViewModel()
        End Sub

        Private Sub diagramControl_MouseDoubleClick(ByVal sender As Object, ByVal e As Input.MouseButtonEventArgs)
            Dim item = diagramControl.CalcHitItem(Function(el) e.GetPosition(el))
            Dim clickedItem = TryCast(item, DiagramItem)
            If clickedItem IsNot Nothing AndAlso Not(TypeOf clickedItem Is DiagramContainer) Then
                Dim commandContainer = TryCast(clickedItem.ParentItem, DiagramContainer)
                If commandContainer IsNot Nothing Then
                    Dim resultShape = TryCast(commandContainer.Items(2), DiagramShape)
                    diagramControl.SelectItem(resultShape)
                    diagramControl.Commands.Execute(DiagramCommandsBase.EditCommand)
                End If
            End If
        End Sub

        Private Sub diagramControl_ItemContentChanged(ByVal sender As Object, ByVal e As DiagramItemContentChangedEventArgs)
            Dim newValue As Integer = 0
            If Not Integer.TryParse(e.NewValue, newValue) Then
                MessageBox.Show("The value should be a number.", "Invalid value")
                Dim shape = TryCast(e.Item, DiagramShape)
                If shape IsNot Nothing Then shape.Content = e.OldValue
            End If
        End Sub
    End Class
End Namespace
