Imports DevExpress.Data.Filtering
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Data
Imports System.ComponentModel
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Forms

Namespace EditorsDemo.FilterBehavior

    Public Class CollectionViewFilterBehavior
        Inherits Behavior(Of FrameworkElement)

        Public Property CollectionView As ICollectionView
            Get
                Return CType(GetValue(CollectionViewProperty), ICollectionView)
            End Get

            Set(ByVal value As ICollectionView)
                SetValue(CollectionViewProperty, value)
            End Set
        End Property

        Public Shared ReadOnly CollectionViewProperty As DependencyProperty = DependencyProperty.Register("CollectionView", GetType(ICollectionView), GetType(CollectionViewFilterBehavior), New PropertyMetadata(Nothing, Sub(o, e) CType(o, CollectionViewFilterBehavior).UpdateCollectionView()))

        Public Property FilterCriteria As CriteriaOperator
            Get
                Return CType(GetValue(FilterCriteriaProperty), CriteriaOperator)
            End Get

            Set(ByVal value As CriteriaOperator)
                SetValue(FilterCriteriaProperty, value)
            End Set
        End Property

        Public Shared ReadOnly FilterCriteriaProperty As DependencyProperty = DependencyProperty.Register("FilterCriteria", GetType(CriteriaOperator), GetType(CollectionViewFilterBehavior), New PropertyMetadata(Nothing, Sub(o, e) CType(o, CollectionViewFilterBehavior).UpdateCollectionView()))

        Private Sub UpdateCollectionView()
            If CollectionView Is Nothing Then Return
            Dim elementType = ListBindingHelper.GetListItemType(CollectionView.SourceCollection)
            Dim method = [GetType]().GetMethod("ApplyFilter", BindingFlags.Instance Or BindingFlags.NonPublic).MakeGenericMethod({elementType})
            method.Invoke(Me, Nothing)
        End Sub

        Private Sub ApplyFilter(Of T)()
            Dim converter = New GridFilterCriteriaToExpressionConverter(Of T)()
            Dim expression = converter.Convert(FilterCriteria)
            Dim filter = expression.Compile()
            CollectionView.Filter = Function(x) filter(CType(x, T))
        End Sub
    End Class
End Namespace
