Imports DevExpress.Mvvm
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Namespace PropertyGridDemo

    Public Class DataErrorInfoValidationData
        Inherits BindableBase
        Implements IDataErrorInfo

        Default Public ReadOnly Property Item(ByVal columnName As String) As String Implements IDataErrorInfo.Item
            Get
                Select Case columnName
                    Case "Login"
                        Return If(ValidateLogin(Login), String.Empty, [Error])
                    Case "Mail"
                        Return If(ValidateMail(Mail, ConfirmMail), String.Empty, [Error])
                    Case "ConfirmMail"
                        Return If(ValidateMail(Mail, ConfirmMail), String.Empty, [Error])
                    Case "Age"
                        Return If(ValidateAge(Age), String.Empty, [Error])
                    Case "CardType"
                        Return If(ValidateCardType(CardType), String.Empty, [Error])
                    Case "CardExpDate"
                        Return If(ValidateCardExpDate(CardExpDate), String.Empty, [Error])
                    Case "CardNumber"
                        Return If(ValidateCardNumber(CardType, CardNumber), String.Empty, [Error])
                End Select

                Return String.Empty
            End Get
        End Property

        Public Shared ReadOnly Cards As List(Of CardInfo) = New List(Of CardInfo)()

        Shared Sub New()
            Call Cards.Add(New CardInfo() With {.Name = "VISA", .DisplayName = "VISA (13 digits)"})
            Call Cards.Add(New CardInfo() With {.Name = "Master Card", .DisplayName = "Master Card (16 digits)"})
            Call Cards.Add(New CardInfo() With {.Name = "American Express", .DisplayName = "American Express (15 digits)"})
            Call Cards.Add(New CardInfo() With {.Name = "Diners Club", .DisplayName = "Diners Club (13 digits)"})
        End Sub

        Public Sub New()
            Login = "TestUser"
            Mail = "testmail@devexpress.com"
            FirstName = "John"
            LastName = "Joe"
            CardType = "VISA"
        End Sub

        Private _error As String

        Private _cardExpDate As Date = Date.Today.AddMonths(-3)

        <Display(AutoGenerateField:=False)>
        Public ReadOnly Property [Error] As String Implements IDataErrorInfo.[Error]
            Get
                Return _error
            End Get
        End Property

        Public Property Login As String
            Get
                Return GetProperty(Function() Me.Login)
            End Get

            Set(ByVal value As String)
                SetProperty(Function() Login, value)
            End Set
        End Property

        Public Property Mail As String
            Get
                Return GetProperty(Function() Me.Mail)
            End Get

            Set(ByVal value As String)
                If SetProperty(Function() Mail, value) Then
                    RaisePropertiesChanged(GetPropertyName(Function() ConfirmMail))
                End If
            End Set
        End Property

        Public Property ConfirmMail As String
            Get
                Return GetProperty(Function() Me.ConfirmMail)
            End Get

            Set(ByVal value As String)
                If SetProperty(Function() ConfirmMail, value) Then
                    RaisePropertiesChanged(GetPropertyName(Function() Mail))
                End If
            End Set
        End Property

        Public Property FirstName As String
            Get
                Return GetProperty(Function() Me.FirstName)
            End Get

            Set(ByVal value As String)
                SetProperty(Function() FirstName, value)
            End Set
        End Property

        Public Property LastName As String
            Get
                Return GetProperty(Function() Me.LastName)
            End Get

            Set(ByVal value As String)
                SetProperty(Function() LastName, value)
            End Set
        End Property

        Public Property Age As Decimal
            Get
                Return GetProperty(Function() Me.Age)
            End Get

            Set(ByVal value As Decimal)
                SetProperty(Function() Age, value)
            End Set
        End Property

        Public Property CardType As String
            Get
                Return GetProperty(Function() Me.CardType)
            End Get

            Set(ByVal value As String)
                SetProperty(Function() CardType, value)
            End Set
        End Property

        Public Property CardNumber As String
            Get
                Return GetProperty(Function() Me.CardNumber)
            End Get

            Set(ByVal value As String)
                SetProperty(Function() CardNumber, value)
            End Set
        End Property

        Public Property CardExpDate As Date
            Get
                Return GetProperty(Function() Me.CardExpDate)
            End Get

            Set(ByVal value As Date)
                SetProperty(Function() CardExpDate, value)
            End Set
        End Property

        Private Sub SetError(ByVal isValid As Boolean, ByVal errorString As String)
            If isValid Then
                _error = String.Empty
            Else
                _error = errorString
            End If
        End Sub

        Public Function ValidateName(ByVal name As String) As Boolean
            Dim isValid As Boolean = Not String.IsNullOrEmpty(name)
            SetError(isValid, "Name can't be empty")
            Return isValid
        End Function

        Public Function ValidateMail(ByVal mail As String, ByVal confirmMail As String) As Boolean
            Dim isValid As Boolean = Equals(mail, confirmMail)
            SetError(isValid, "Two specified e-mail addresses are different")
            Return isValid
        End Function

        Public Function ValidateCardNumber(ByVal type As String, ByVal number As String) As Boolean
            Dim isValid As Boolean = False
            Select Case type
                Case "VISA"
                    isValid = ValidateVISA(number)
                Case "Master Card"
                    isValid = ValidateMasterCard(number)
                Case "American Express"
                    isValid = ValidateAmericanExpress(number)
                Case "Diners Club"
                    isValid = ValidateDinersClub(number)
                Case Else
                    isValid = False
            End Select

            SetError(isValid, "Invalid number")
            Return isValid
        End Function

        Public Function ValidateLogin(ByVal login As String) As Boolean
            Dim isValid As Boolean = Not Equals(login, "TestUser")
            SetError(isValid, "A user with this name is already registered")
            Return isValid
        End Function

        Public Function ValidateAge(ByVal age As Decimal) As Boolean
            Dim isValid As Boolean = age > 21 AndAlso age < 200
            If age < 21 Then
                SetError(isValid, "Sorry, you're too young to visit our site!")
                Return False
            ElseIf age > 200 Then
                SetError(isValid, "Congratulations! You're the oldest man on Earth!")
                Return False
            End If

            Return True
        End Function

        Public Function ValidateCardType(ByVal type As String) As Boolean
            Dim isValid As Boolean = Equals(type, "American Express") OrElse Equals(type, "VISA")
            SetError(isValid, "Sorry, cards of this type are not accepted")
            Return isValid
        End Function

        Private Function ValidateVISA(ByVal num As String) As Boolean
            Return Not String.IsNullOrEmpty(num) AndAlso num.Length = 13
        End Function

        Private Function ValidateMasterCard(ByVal num As String) As Boolean
            Return Not String.IsNullOrEmpty(num) AndAlso num.Length = 16
        End Function

        Private Function ValidateAmericanExpress(ByVal num As String) As Boolean
            Return Not String.IsNullOrEmpty(num) AndAlso num.Length = 15
        End Function

        Private Function ValidateDinersClub(ByVal num As String) As Boolean
            Return Not String.IsNullOrEmpty(num) AndAlso num.Length = 14
        End Function

        Public Function ValidateCardExpDate(ByVal expDate As Date) As Boolean
            Dim isValid As Boolean = expDate.CompareTo(Date.Today) > 0
            SetError(isValid, "Sorry, your card has expired")
            Return isValid
        End Function
    End Class

    Public Class CardInfo

        Public Property Name As String

        Public Property DisplayName As String
    End Class
End Namespace
