Namespace GridDemo

    Public Class PrintTemplatesViewModel
        Inherits PrintViewModelBase

        Public Property Mode As PrintTemplatesMode
            Get
                Return GetProperty(Function() Me.Mode)
            End Get

            Set(ByVal value As PrintTemplatesMode)
                SetProperty(Function() Mode, value)
            End Set
        End Property

        Public Overrides Sub ReOpenPreviewTabs()
            Mode = PrintTemplatesMode.Default
            MyBase.ReOpenPreviewTabs()
            Mode = PrintTemplatesMode.MailMerge
            ShowPreviewInNewTab()
            Mode = PrintTemplatesMode.Detail
            ShowPreviewInNewTab()
        End Sub

        Protected Overrides Function GetTitle() As String
            Return Mode.ToString() & " Print Preview"
        End Function
    End Class

    Public Enum PrintTemplatesMode
        Detail
        MailMerge
        [Default]
    End Enum
End Namespace
