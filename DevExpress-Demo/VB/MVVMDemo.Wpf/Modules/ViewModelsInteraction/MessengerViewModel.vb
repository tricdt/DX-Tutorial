Imports DevExpress.Mvvm

Namespace MVVMDemo.ViewModelsInteraction

    Public Class MessengerViewModel

        Private messageIndex As Integer

        Public Sub SendMessage()
            Call Messenger.Default.Send(New Message("Message " & messageIndex))
            messageIndex += 1
        End Sub
    End Class

    Public Class ReceiverViewModel

        Public Overridable Property Status As String

        Protected Sub New()
            Call Messenger.Default.Register(Me, New System.Action(Of Message)(AddressOf OnMessage))
        End Sub

        Private Sub OnMessage(ByVal message As Message)
            Status = "Received: " & message.Content
        End Sub
    End Class

    Public Class Message

        Private _Content As String

        Public Sub New(ByVal content As String)
            Me.Content = content
        End Sub

        Public Property Content As String
            Get
                Return _Content
            End Get

            Private Set(ByVal value As String)
                _Content = value
            End Set
        End Property
    End Class
End Namespace
