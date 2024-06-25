Imports System.Windows
Imports System.Windows.Controls

Namespace DevExpress.SalesDemo.Wpf.View.Common

    Public Partial Class PerformanceButtonsView
        Inherits UserControl

        Public Shared ReadOnly LastTimePeriodButtonVisibilityProperty As DependencyProperty = DependencyProperty.Register("LastTimePeriodButtonVisibility", GetType(Visibility), GetType(PerformanceButtonsView), New PropertyMetadata(Visibility.Visible))

        Public Shared ReadOnly CurrentTimePeriodButtonVisibilityProperty As DependencyProperty = DependencyProperty.Register("CurrentTimePeriodButtonVisibility", GetType(Visibility), GetType(PerformanceButtonsView), New PropertyMetadata(Visibility.Visible))

        Public Property LastTimePeriodButtonVisibility As Visibility
            Get
                Return CType(GetValue(LastTimePeriodButtonVisibilityProperty), Visibility)
            End Get

            Set(ByVal value As Visibility)
                SetValue(LastTimePeriodButtonVisibilityProperty, value)
            End Set
        End Property

        Public Property CurrentTimePeriodButtonVisibility As Visibility
            Get
                Return CType(GetValue(CurrentTimePeriodButtonVisibilityProperty), Visibility)
            End Get

            Set(ByVal value As Visibility)
                SetValue(CurrentTimePeriodButtonVisibilityProperty, value)
            End Set
        End Property

        Public Sub New()
            Me.InitializeComponent()
        End Sub
    End Class
End Namespace
