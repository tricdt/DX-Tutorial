Imports DevExpress.Xpf.Grid
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Media

Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/ScrollBarAnnotationsModeConverter.(cs)")>
    Public Partial Class ScrollBarAnnotations
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            listAnnotaions.SelectAll()
        End Sub

#Region "Validation"
        Private Sub view_ValidateCell(ByVal sender As Object, ByVal e As GridCellValidationEventArgs)
            If e.CellValue IsNot Nothing Then
                Select Case e.Column.FieldName
                    Case "Quantity"
                        Dim quantity As Integer = Integer.Parse(e.CellValue.ToString())
                        If quantity >= 100 Then
                            e.IsValid = False
                            e.SetError("Quantity is greater than or equals to 100")
                        End If

                    Case "Discount"
                        Dim discount As Double = Double.Parse(e.CellValue.ToString())
                        If discount > 0.21 Then
                            e.IsValid = False
                            e.SetError("Discount is greater than 21%")
                        End If

                    Case "UnitPrice"
                        Dim val As Double = Double.Parse(e.CellValue.ToString())
                        If val < 2.5 Then
                            e.IsValid = False
                            e.SetError("Unit Price is less than 2.5")
                        End If

                    Case Else
                        Return
                End Select
            End If
        End Sub

#End Region
#Region "Custom Scroll Bar Annotation implementation"
        Private changedRows As HashSet(Of Integer) = New HashSet(Of Integer)()

        Private Sub view_ScrollBarAnnotationsCreating(ByVal sender As Object, ByVal e As ScrollBarAnnotationsCreatingEventArgs)
            Dim info As ScrollBarAnnotationInfo = New ScrollBarAnnotationInfo(Brushes.Green, ScrollBarAnnotationAlignment.Left, 4, 3)
            e.CustomScrollBarAnnotations = New List(Of ScrollBarAnnotationRowInfo)(changedRows.[Select](Function(x) New ScrollBarAnnotationRowInfo(grid.GetRowHandleByListIndex(x), info)))
        End Sub

        Private Sub view_RowUpdated(ByVal sender As Object, ByVal e As RowEventArgs)
            changedRows.Add(grid.GetListIndexByRowHandle(e.RowHandle))
        End Sub
#End Region
    End Class
End Namespace
