Imports System
Imports System.Windows
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Utils
Imports DevExpress.Xpf.Ribbon

Namespace RibbonDemo

    Public Class RibbonDemoModule
        Inherits DemoModule

        Public Shared ReadOnly RibbonProperty As DependencyProperty

        Shared Sub New()
            Dim ownerType As Type = GetType(RibbonDemoModule)
            RibbonProperty = DependencyPropertyManager.Register("Ribbon", GetType(RibbonControl), ownerType, New FrameworkPropertyMetadata(CType(Nothing, PropertyChangedCallback)))
        End Sub

        Public Property Ribbon As RibbonControl
            Get
                Return CType(GetValue(RibbonProperty), RibbonControl)
            End Get

            Set(ByVal value As RibbonControl)
                SetValue(RibbonProperty, value)
            End Set
        End Property

        Protected Overridable ReadOnly Property NeedChangeEditorsTheme As Boolean
            Get
                Return False
            End Get
        End Property

        Protected Overrides Sub ShowPopupContent()
            MyBase.ShowPopupContent()
            If Ribbon IsNot Nothing Then Ribbon.AllowKeyTips = True
        End Sub

        Protected Overrides Sub HidePopupContent()
            If Ribbon IsNot Nothing Then
                Ribbon.AllowKeyTips = False
                NavigationTree.ExitMenuMode(False, False)
                Ribbon.CloseApplicationMenu()
            End If

            MyBase.HidePopupContent()
        End Sub
    End Class
End Namespace
