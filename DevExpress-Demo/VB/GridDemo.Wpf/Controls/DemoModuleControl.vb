Imports System.Linq
Imports System.Windows
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Grid

Namespace GridDemo

    Public Class GridDemoModule
        Inherits DemoModule

        Private Shared ReadOnly GridControlPropertyKey As DependencyPropertyKey

        Public Shared ReadOnly GridControlProperty As DependencyProperty

        Shared Sub New()
            GridControlPropertyKey = DependencyProperty.RegisterReadOnly("GridControl", GetType(GridControl), GetType(GridDemoModule), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))
            GridControlProperty = GridControlPropertyKey.DependencyProperty
        End Sub

        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            GridControl = If(TryCast(Content, GridControl), LayoutTreeHelper.GetVisualChildren(CType(Content, DependencyObject)).OfType(Of GridControl)().FirstOrDefault())
            If OptionsDataContext Is Nothing Then OptionsDataContext = GridControl
        End Sub

        Public Property GridControl As GridControl
            Get
                Return CType(GetValue(GridControlProperty), GridControl)
            End Get

            Private Set(ByVal value As GridControl)
                SetValue(GridControlPropertyKey, value)
            End Set
        End Property

        Protected Overrides Sub HidePopupContent()
            If GridControl IsNot Nothing Then GridControl.View.HideColumnChooser()
            MyBase.HidePopupContent()
        End Sub

        Protected Overrides Sub Hide()
            If GridControl IsNot Nothing Then
                GridControl.View.CommitEditing()
            End If

            MyBase.Hide()
        End Sub
    End Class
End Namespace

Namespace CommonDemo

    Public Class CommonDemoModule
        Inherits GridDemo.GridDemoModule

    End Class
End Namespace

Namespace CommonChartsDemo

    Public Class CommonChartsDemoModule
        Inherits GridDemo.GridDemoModule

    End Class
End Namespace
