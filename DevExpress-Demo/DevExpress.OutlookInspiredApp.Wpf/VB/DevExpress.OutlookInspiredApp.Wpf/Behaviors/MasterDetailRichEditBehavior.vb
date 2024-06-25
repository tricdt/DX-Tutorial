Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.XtraRichEdit

Namespace DevExpress.DevAV

    Public Class MasterDetailRichEditBehavior
        Inherits Behavior(Of RichEditControl)

        Public Shared ReadOnly OrderProperty As DependencyProperty = DependencyProperty.Register("Order", GetType(Order), GetType(MasterDetailRichEditBehavior), New PropertyMetadata(Nothing, Sub(d, e) CType(d, MasterDetailRichEditBehavior).OnOrderChanged()))

        Public Property Order As Order
            Get
                Return CType(GetValue(OrderProperty), Order)
            End Get

            Set(ByVal value As Order)
                SetValue(OrderProperty, value)
            End Set
        End Property

        Private masterTemplate As IRichEditDocumentServer

        Private detailTemplate As IRichEditDocumentServer

        Private richEdit As RichEditControl

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            richEdit = AssociatedObject
            CreateTemplates()
            SubscribeEvents()
            OnOrderChanged()
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            UnsubscribeEvents()
        End Sub

        Private Sub OnCalculateDocumentVariable(ByVal sender As Object, ByVal e As CalculateDocumentVariableEventArgs)
            If Order Is Nothing OrElse Not Equals(e.VariableName, "Products") Then Return
            Using server = New RichEditDocumentServer()
                Using stream = GetTemplateStream("Sales Order Follow-Up Detail.rtf")
                    server.LoadDocument(stream, DocumentFormat.Rtf)
                    Dim options = server.CreateMailMergeOptions()
                    options.DataSource = Order.OrderItems
                    server.MailMerge(options, detailTemplate.Document)
                End Using
            End Using

            e.Value = detailTemplate
            e.KeepLastParagraph = True
            e.Handled = True
        End Sub

        Private Sub OnOrderChanged()
            If Order Is Nothing OrElse richEdit Is Nothing Then Return
            Using stream = GetTemplateStream("Sales Order Follow-Up.rtf")
                masterTemplate.LoadDocument(stream, DocumentFormat.Rtf)
                Dim options = masterTemplate.CreateMailMergeOptions()
                options.DataSource = New List(Of Order)() From {Order}
                masterTemplate.MailMerge(options, richEdit.Document)
            End Using
        End Sub

        Private Sub SubscribeEvents()
            AddHandler richEdit.CalculateDocumentVariable, AddressOf OnCalculateDocumentVariable
        End Sub

        Private Sub UnsubscribeEvents()
            RemoveHandler richEdit.CalculateDocumentVariable, AddressOf OnCalculateDocumentVariable
        End Sub

        Private Sub CreateTemplates()
            masterTemplate = richEdit.CreateDocumentServer()
            detailTemplate = richEdit.CreateDocumentServer()
        End Sub
    End Class
End Namespace
