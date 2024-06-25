Imports DevExpress.Mvvm
Imports System.Collections.Generic

Namespace EditorsDemo

    Public Class ValidationViewModelBase
        Inherits ViewModelBase

        Public Class CardInfo

            Public Property Name As String

            Public Property DisplayName As String
        End Class

        Public Shared ReadOnly Cards As List(Of CardInfo) = New List(Of CardInfo)()

        Shared Sub New()
            Call Cards.Add(New CardInfo() With {.Name = "VISA", .DisplayName = "VISA (13 digits)"})
            Call Cards.Add(New CardInfo() With {.Name = "Master Card", .DisplayName = "Master Card (16 digits)"})
            Call Cards.Add(New CardInfo() With {.Name = "American Express", .DisplayName = "American Express (15 digits)"})
            Call Cards.Add(New CardInfo() With {.Name = "Diners Club", .DisplayName = "Diners Club (13 digits)"})
        End Sub

        Public Property Login As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Property Mail As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Property ConfirmMail As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Property FirstName As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Property LastName As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Property Age As Decimal
            Get
                Return GetValue(Of Decimal)()
            End Get

            Set(ByVal value As Decimal)
                SetValue(value)
            End Set
        End Property

        Public Property CardType As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Property CardNumber As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Property CardExpDate As Date
            Get
                Return GetValue(Of Date)()
            End Get

            Set(ByVal value As Date)
                SetValue(value)
            End Set
        End Property

        Public Sub New()
            Login = "TestUser"
            Mail = "testmail@devexpress.com"
            FirstName = "John"
            LastName = "Joe"
            CardType = "VISA"
            CardExpDate = Date.Today.AddDays(-1)
        End Sub
    End Class
End Namespace
