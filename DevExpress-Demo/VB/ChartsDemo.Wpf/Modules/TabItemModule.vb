Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public Class TabItemModule
        Inherits UserControl

        Public Shared ReadOnly OptionsProperty As DependencyProperty = DependencyProperty.Register("Options", GetType(StackPanel), GetType(TabItemModule), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly ActiveChartProperty As DependencyProperty = DependencyProperty.Register("ActiveChart", GetType(ChartControlBase), GetType(TabItemModule), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly IsModuleLoadedProperty As DependencyProperty = DependencyProperty.Register("IsModuleLoaded", GetType(Boolean), GetType(TabItemModule), New PropertyMetadata(False))

        Public Overridable ReadOnly Property IsAnimationCompleted As Boolean
            Get
                Return True
            End Get
        End Property

        Public Property Options As StackPanel
            Get
                Return CType(GetValue(OptionsProperty), StackPanel)
            End Get

            Set(ByVal value As StackPanel)
                SetValue(OptionsProperty, value)
            End Set
        End Property

        Public Property ActiveChart As ChartControlBase
            Get
                Return CType(GetValue(ActiveChartProperty), ChartControlBase)
            End Get

            Set(ByVal value As ChartControlBase)
                SetValue(ActiveChartProperty, value)
            End Set
        End Property

        Public Property IsModuleLoaded As Boolean
            Get
                Return CBool(GetValue(IsModuleLoadedProperty))
            End Get

            Set(ByVal value As Boolean)
                SetValue(IsModuleLoadedProperty, value)
            End Set
        End Property

        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            Dim charts As IEnumerable(Of DependencyObject) = LayoutTreeHelper.GetLogicalChildren(Me).Where(Function(c) TypeOf c Is ChartControlBase)
            If charts.Count() = 1 Then
                ActiveChart = CType(charts.First(), ChartControlBase)
            Else
                Throw New NotSupportedException("TabbedItemModule must contain single ChartControl")
            End If

            If DesignerProperties.GetIsInDesignMode(Me) Then
                Dim scrollViewer As ScrollViewer = New ScrollViewer()
                scrollViewer.Content = Options
                Dim moduleRoot As DockPanel = CType(LayoutTreeHelper.GetLogicalChildren(Me).Where(Function(c) TypeOf c Is DockPanel).FirstOrDefault(), DockPanel)
                If moduleRoot IsNot Nothing Then
                    moduleRoot.Children.Insert(0, scrollViewer)
                    Call DockPanel.SetDock(scrollViewer, Dock.Right)
                Else
                    Throw New NotSupportedException("TabbedItemModule must contain a DockPanel as its content root")
                End If
            End If
        End Sub
    End Class
End Namespace
