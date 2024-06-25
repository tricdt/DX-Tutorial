Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Linq
Imports System.Windows.Media

Namespace GridDemo

    Public Partial Class TreeListView
        Inherits GridDemoModule

        Private maxId As Integer

        Public Sub New()
            InitializeComponent()
            maxId = EmployeesWithPhotoData.DataSource.Max(Function(x) x.Id)
        End Sub

        Private Sub OnInitNewNode(ByVal sender As Object, ByVal e As TreeList.TreeListNodeEventArgs)
            view.SetNodeValue(e.Node, "Id", Threading.Interlocked.Increment(maxId))
        End Sub
    End Class

    Public Class EmployeeCategoryImageSelector
        Inherits TreeListNodeImageSelector

        Public Overrides Function [Select](ByVal rowData As TreeList.TreeListRowData) As ImageSource
            Dim empl As Employee = TryCast(rowData.Row, Employee)
            If empl Is Nothing OrElse String.IsNullOrEmpty(empl.GroupName) Then Return Nothing
            Dim extension = New SvgImageSourceExtension() With {.Uri = New Uri(GroupNameToImageConverterExtension.GetImagePathByGroupName(empl.GroupName)), .Size = New Windows.Size(16, 16)}
            Return CType(extension.ProvideValue(Nothing), ImageSource)
        End Function
    End Class
End Namespace
