Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Threading
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.DemoBase.DemoTesting

Namespace ChartsDemo

    Public Class ChartsDemoModule
        Inherits DemoModule

        Public Shared Function CreateTimer() As DispatcherTimer
            Return New DispatcherTimer(If(DemoTestingHelper.IsTesting, DispatcherPriority.ApplicationIdle, DispatcherPriority.Background))
        End Function

        Public Shared ReadOnly IsModuleLoadedPropertyKey As DependencyPropertyKey = DependencyProperty.RegisterReadOnly("IsModuleLoaded", GetType(Boolean), GetType(ChartsDemoModule), New PropertyMetadata(False))

        Public Shared ReadOnly IsModuleLoadedProperty As DependencyProperty = IsModuleLoadedPropertyKey.DependencyProperty

        Private Shared Iterator Function FindVisualChildren(Of T As DependencyObject)(ByVal root As DependencyObject) As IEnumerable(Of T)
            If root IsNot Nothing Then
                For i As Integer = 0 To VisualTreeHelper.GetChildrenCount(root) - 1
                    Dim child As DependencyObject = VisualTreeHelper.GetChild(root, i)
                    If child IsNot Nothing AndAlso TypeOf child Is T Then
                        Yield CType(child, T)
                    End If

                    For Each childOfChild As T In FindVisualChildren(Of T)(child)
                        Yield childOfChild
                    Next
                Next
            End If
        End Function

        Private runChartsAnimationOnLoadField As Boolean = True

        Public Sub New()
            AddHandler ModuleLoaded, AddressOf ChartsDemoModule_ModuleLoaded
        End Sub

        Public Overridable Sub OnStartModule()
        End Sub

        Public Overridable Sub OnStopModule()
        End Sub

        Public Overridable ReadOnly Property ModuleChartControl As ChartControl
            Get
                Return Nothing
            End Get
        End Property

        Public Property IsModuleLoaded As Boolean
            Get
                Return CBool(GetValue(IsModuleLoadedProperty))
            End Get

            Private Set(ByVal value As Boolean)
                SetValue(IsModuleLoadedPropertyKey, value)
            End Set
        End Property

        Public Property RunChartsAnimationOnLoad As Boolean
            Get
                Return runChartsAnimationOnLoadField
            End Get

            Set(ByVal value As Boolean)
                runChartsAnimationOnLoadField = value
            End Set
        End Property

        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            IsModuleLoaded = True
        End Sub

        Protected Overrides Sub Show()
            MyBase.Show()
            For Each chart As ChartControl In FindVisualChildren(Of ChartControl)(Me)
                If chart.Palette IsNot Nothing Then chart.Palette.ColorCycleLength = 10
                If runChartsAnimationOnLoadField Then chart.Animate()
            Next
        End Sub
    End Class
End Namespace
