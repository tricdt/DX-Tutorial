Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.Xpf.DemoBase

Namespace SpellCheckerDemo

    Public Class SpellCheckerDemoModule
        Inherits DemoModule

        Protected Overridable ReadOnly Property CheckingElements As List(Of FrameworkElement)
            Get
                Return Nothing
            End Get
        End Property

        Protected Overrides Sub ShowPopupContent()
            MyBase.ShowPopupContent()
            If CheckingElements Is Nothing Then Return
            For Each element As FrameworkElement In CheckingElements
                element.Visibility = Visibility.Visible
            Next
        End Sub

        Protected Overrides Sub HidePopupContent()
            If CheckingElements Is Nothing Then Return
            For Each element As FrameworkElement In CheckingElements
                element.Visibility = Visibility.Hidden
            Next

            MyBase.HidePopupContent()
        End Sub
    End Class
End Namespace
