Imports DevExpress.Xpf.Core.FilteringUI
Imports DevExpress.Xpf.Data
Imports System
Imports System.Windows.Controls

Namespace EditorsDemo.FilterBehavior

    Public Partial Class FilterBehaviorViewModelData
        Inherits UserControl

        Private ReadOnly Property ViewModel As FilterBehaviorViewModelDataViewModel
            Get
                Return CType(DataContext, FilterBehaviorViewModelDataViewModel)
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub filterBehavior_ActualFilterCriteriaChanged(ByVal sender As Object, ByVal e As EventArgs)
            If ReferenceEquals(filterBehavior.ActualFilterCriteria, Nothing) Then
                ViewModel.SetFilter(Nothing)
            Else
                Dim converter = New GridFilterCriteriaToExpressionConverter(Of ProductInfo)()
                Dim expression = converter.Convert(filterBehavior.ActualFilterCriteria)
                ViewModel.SetFilter(expression)
            End If
        End Sub

        Private Sub filterBehavior_CustomUniqueValues(ByVal sender As Object, ByVal e As CustomUniqueValuesEventArgs)
            If Not Equals(e.FieldName, "CategoryName") Then Throw New InvalidOperationException()
            e.CountedValues = ViewModel.GetCategoryUniqueValues()
        End Sub
    End Class
End Namespace
