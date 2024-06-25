Imports DevExpress.Data.Filtering
Imports DevExpress.Diagram.Core
Imports DevExpress.Xpf.Diagram
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports DevExpress.Diagram.Demos

Namespace DiagramDemo

    Public Partial Class ProductFlowDiagramModule
        Inherits DiagramDemoModule

        Private ReadOnly styles As DiagramItemStyleId() = DiagramShapeStyleId.Styles.ToArray()

        Private ReadOnly info As ProductFlowInfo

        Public Sub New()
            InitializeComponent()
            diagramControl.Commands.RegisterHotKeys(New Action(Of IHotKeysRegistrator)(AddressOf ClearHotKeys))
            info = GenerateProductFlowInfo()
            DataContext = info
        End Sub

        Private Sub ClearHotKeys(ByVal registrator As IHotKeysRegistrator)
            registrator.ClearHotKeys(DiagramCommandsBase.SaveFileAsCommand)
            registrator.ClearHotKeys(DiagramCommandsBase.SaveFileCommand)
        End Sub

        Private Sub SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim selectedDiagramItem = diagramControl.PrimarySelection
            gridControl.ClearGrouping()
            gridControl.FilterCriteria = Nothing
            If Not diagramControl.SelectedItems.Any() Then Return
            Dim customers = GetDataItems(Of CustomerData)()
            Dim categories = GetDataItems(Of CategoryData)()
            Dim connectors = GetDataItems(Of ProductFlowData)()
            If connectors.Any() Then
                Dim connectorsCriteria = connectors.[Select](Function(c) New GroupOperator(GroupOperatorType.And, GetCategoryOperator(c.Category.Name), GetCustomerOperator(c.Customer.Name)))
                gridControl.FilterCriteria = New GroupOperator(GroupOperatorType.Or, connectorsCriteria)
                GroupGridControl("Category.Name")
                Return
            End If

            If customers.Any() AndAlso Not categories.Any() Then
                Dim customersCriteria = customers.[Select](Function(c) GetCustomerOperator(c.Name))
                gridControl.FilterCriteria = New GroupOperator(GroupOperatorType.Or, customersCriteria)
                GroupGridControl("Category.Name")
            End If

            If categories.Any() Then
                Dim productCriteria = New GroupOperator(GroupOperatorType.Or, categories.[Select](Function(c) GetCategoryOperator(c.Name)))
                Dim customersCriteria As GroupOperator = Nothing
                If customers.Any() Then customersCriteria = New GroupOperator(GroupOperatorType.Or, customers.[Select](Function(c) GetCustomerOperator(c.Name)))
                gridControl.FilterCriteria = If(customers.Any(), New GroupOperator(GroupOperatorType.And, customersCriteria, productCriteria), productCriteria)
                GroupGridControl("Customer.Name")
            End If
        End Sub

        Private Sub GroupGridControl(ByVal fieldName As String)
            gridControl.GroupBy(fieldName)
            gridControl.ExpandAllGroups()
        End Sub

        Private Sub dataBindingBehavior_GenerateItem(ByVal sender As Object, ByVal e As DiagramGenerateItemEventArgs)
            Dim templateName = If((TypeOf e.DataObject Is CustomerData), "CustomerTemplate", "CategoryTemplate")
            e.Item = e.CreateItemFromTemplate(templateName)
        End Sub

        Private Sub dataBindingBehavior_CustomLayoutItems(ByVal sender As Object, ByVal e As DiagramCustomLayoutItemsEventArgs)
            Call ArrangeItemsInLine(Of CategoryData)(e.Items, New Point(600, 50), New Size(150, 105), 20)
            Call ArrangeItemsInLine(Of CustomerData)(e.Items, New Point(50, 100), New Size(150, 105), 20)
            For Each item In e.Items
                Dim customer = TryCast(item.DataContext, CustomerData)
                If customer IsNot Nothing Then
                    item.ThemeStyleId = styles(Array.IndexOf(info.Customers, customer))
                End If
            Next

            For Each connector In e.DiagramConnectors
                Dim connectorData = CType(connector.DataContext, ProductFlowData)
                connector.ThemeStyleId = styles(Array.IndexOf(info.Customers, connectorData.Customer))
            Next

            e.Handled = True
        End Sub

        Private Sub ArrangeItemsInLine(Of TDataContext)(ByVal items As IEnumerable(Of DiagramItem), ByVal startPosition As Point, ByVal itemSize As Size, ByVal margin As Integer)
            Dim position As Point = startPosition
            For Each diagramItem In items.Where(Function(x) TypeOf x.DataContext Is TDataContext)
                diagramItem.Position = position
                position.Offset(0, itemSize.Height + margin)
            Next
        End Sub

        Private Sub dataBindingBehavior_UpdateConnector(ByVal sender As Object, ByVal e As DiagramUpdateConnectorEventArgs)
            Dim connectorData = CType(e.DataObject, ProductFlowData)
            e.Connector.StrokeThickness = connectorData.Weight
        End Sub

        Private Sub DiagramControl_ItemsChanged(ByVal sender As Object, ByVal e As DiagramItemsChangedEventArgs)
            If diagramControl.Items.Count = 1 Then diagramControl.SelectItem(diagramControl.Items.First())
        End Sub

        Private Sub dataBindingBehavior_ItemsGenerated(ByVal sender As Object, ByVal e As DiagramItemsGeneratedEventArgs)
            diagramControl.IsReadOnly = True
        End Sub

        Private Function GetDataItems(Of T)() As IEnumerable(Of T)
            Return diagramControl.SelectedItems.[Select](Function(x) x.DataContext).Where(Function(x) TypeOf x Is T).Cast(Of T)()
        End Function

        Private Function GetCategoryOperator(ByVal value As String) As BinaryOperator
            Return GetEqualOperator("Category.Name", value)
        End Function

        Private Function GetCustomerOperator(ByVal value As String) As BinaryOperator
            Return GetEqualOperator("Customer.Name", value)
        End Function

        Private Function GetEqualOperator(ByVal propertyName As String, ByVal value As String) As BinaryOperator
            Return New BinaryOperator(propertyName, value, BinaryOperatorType.Equal)
        End Function
    End Class
End Namespace
