Imports System.Windows
Imports System.Collections.Generic

Namespace SpellCheckerDemo

    Public Partial Class TextBox
        Inherits SpellCheckerDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides ReadOnly Property CheckingElements As List(Of FrameworkElement)
            Get
                Return New List(Of FrameworkElement)() From {tb}
            End Get
        End Property
    End Class
End Namespace
