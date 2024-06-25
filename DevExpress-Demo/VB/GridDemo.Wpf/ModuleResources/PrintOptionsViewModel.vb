Namespace GridDemo

    Public Class PrintOptionsViewModel
        Inherits PrintViewModelBase

        Public Property UseCustomPrintStyles As Boolean
            Get
                Return GetProperty(Function() Me.UseCustomPrintStyles)
            End Get

            Set(ByVal value As Boolean)
                SetProperty(Function() UseCustomPrintStyles, value)
            End Set
        End Property

        Public Overrides Sub ReOpenPreviewTabs()
            MyBase.ReOpenPreviewTabs()
            UseCustomPrintStyles = True
            ShowPreviewInNewTab()
            UseCustomPrintStyles = False
        End Sub

        Protected Overrides Function GetTitle() As String
            Return If(UseCustomPrintStyles, "Custom", "Default") & " Style Print Preview"
        End Function
    End Class
End Namespace
