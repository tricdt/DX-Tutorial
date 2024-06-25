Imports System
Imports DevExpress.XtraRichEdit.API.Native
Imports DevExpress.XtraRichEdit
Imports DevExpress.Xpf.Bars

Namespace RichEditDemo

    Public Partial Class DocumentProperties
        Inherits RichEditDemoModule

        Public Shared ReadOnly Properties As DocumentPropertyInfo() = New DocumentPropertyInfo() {New DocumentPropertyInfo("Category", "DOCPROPERTY Category"), New DocumentPropertyInfo("Created", "CREATEDATE"), New DocumentPropertyInfo("Creator", "AUTHOR"), New DocumentPropertyInfo("Description", "COMMENTS"), New DocumentPropertyInfo("Keywords"), New DocumentPropertyInfo("LastModifiedBy", "LASTSAVEDBY"), New DocumentPropertyInfo("LastPrinted", "PRINTDATE"), New DocumentPropertyInfo("Modified", "SAVEDATE"), New DocumentPropertyInfo("Revision", "REVNUM"), New DocumentPropertyInfo("Subject"), New DocumentPropertyInfo("Title")}

        Public Sub New()
            InitializeComponent()
            Document.Fields.Update()
        End Sub

        Private ReadOnly Property Document As Document
            Get
                Return richEdit.Document
            End Get
        End Property

        Private Sub OnDocumentPropertiesChanged(ByVal sender As Object, ByVal e As EventArgs)
            Document.Fields.Update()
        End Sub

        Private Sub OnCalculateDocumentVariable(ByVal sender As Object, ByVal e As CalculateDocumentVariableEventArgs)
            If e.Arguments.Count = 0 OrElse Not Equals(e.VariableName, "CustomProperty") Then Return
            Dim name As String = e.Arguments(0).Value
            Dim customProperty As Object = Document.CustomProperties(name)
            If customProperty IsNot Nothing Then e.Value = customProperty.ToString()
            e.Handled = True
        End Sub

        Private Sub OnPropertyItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            Dim propertyInfo = CType(e.Item.DataContext, DocumentPropertyInfo)
            Document.BeginUpdate()
            Dim field As Field = Document.Fields.Create(richEdit.Document.CaretPosition, propertyInfo.Name)
            field.Update()
            Document.EndUpdate()
        End Sub
    End Class
End Namespace
