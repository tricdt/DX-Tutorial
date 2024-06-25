Imports System
Imports System.Windows.Markup

Namespace PivotGridDemo

    Public Class Int32Extension
        Inherits MarkupExtension

        Private ReadOnly value As Integer

        Public Sub New(ByVal value As Integer)
            Me.value = value
        End Sub

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return value
        End Function
    End Class
End Namespace
