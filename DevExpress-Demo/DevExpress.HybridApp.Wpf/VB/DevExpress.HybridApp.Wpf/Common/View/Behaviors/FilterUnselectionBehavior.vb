Imports System.Windows
Imports System.Windows.Data
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Navigation

Namespace DevExpress.DevAV

    Public Class FilterUnselectionBehavior
        Inherits Behavior(Of TileBar)

        Private selectFilterEnable As Boolean = True

        Public Shared ReadOnly SelectedFilterProperty As DependencyProperty = DependencyProperty.Register("SelectedFilter", GetType(FilterItem), GetType(FilterUnselectionBehavior), New PropertyMetadata(Nothing, Sub(d, e) CType(d, FilterUnselectionBehavior).OnSelectedFilterChanged()))

        Private Shared ReadOnly TileBarItemInternalProperty As DependencyProperty = DependencyProperty.Register("TilebarItemInternal", GetType(FilterItem), GetType(FilterUnselectionBehavior), New PropertyMetadata(Nothing, Sub(d, e) CType(d, FilterUnselectionBehavior).OnTileBarItemInternalChanged()))

        Public Property SelectedFilter As FilterItem
            Get
                Return CType(GetValue(SelectedFilterProperty), FilterItem)
            End Get

            Set(ByVal value As FilterItem)
                SetValue(SelectedFilterProperty, value)
            End Set
        End Property

        Private Property TileBarItemInternal As FilterItem
            Get
                Return CType(GetValue(TileBarItemInternalProperty), FilterItem)
            End Get

            Set(ByVal value As FilterItem)
                SetValue(TileBarItemInternalProperty, value)
            End Set
        End Property

        Private Sub OnSelectedFilterChanged()
            If AssociatedObject Is Nothing OrElse AssociatedObject.ItemsSource Is Nothing OrElse SelectedFilter Is TileBarItemInternal Then Return
            If SelectedFilter Is Nothing Then
                SelectTileBarItem(Nothing)
                Return
            End If

            For Each item In AssociatedObject.ItemsSource
                If item Is SelectedFilter Then
                    SelectTileBarItem(SelectedFilter)
                    Return
                End If
            Next

            SelectTileBarItem(Nothing)
        End Sub

        Private Sub OnTileBarItemInternalChanged()
            If selectFilterEnable Then SelectedFilter = TileBarItemInternal
        End Sub

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            Call BindingOperations.SetBinding(Me, TileBarItemInternalProperty, New Binding("SelectedItem") With {.Source = AssociatedObject, .Mode = BindingMode.OneWay})
            OnSelectedFilterChanged()
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            BindingOperations.ClearBinding(Me, TileBarItemInternalProperty)
        End Sub

        Private Sub SelectTileBarItem(ByVal item As FilterItem)
            selectFilterEnable = False
            AssociatedObject.SelectedItem = item
            selectFilterEnable = True
        End Sub
    End Class
End Namespace
