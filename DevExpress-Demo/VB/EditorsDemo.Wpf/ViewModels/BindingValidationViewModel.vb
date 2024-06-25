Imports System
Imports System.ComponentModel
Imports System.Windows.Controls

Namespace EditorsDemo

    Public Class BindingValidationViewModel
        Inherits ValidationViewModelBase
        Implements IDataErrorInfo

        Private errorField As String

        Private Function SetError(ByVal isValid As Boolean, ByVal errorString As String) As Boolean
            If isValid Then
                errorField = String.Empty
            Else
                errorField = errorString
            End If

            Return isValid
        End Function

#Region "IDataErrorInfo Members"
        Private ReadOnly Property [Error] As String Implements IDataErrorInfo.[Error]
            Get
                Return errorField
            End Get
        End Property

        Private ReadOnly Property Item(ByVal columnName As String) As String Implements IDataErrorInfo.Item
            Get
                Select Case columnName
                    Case "Login"
                        Return If(ValidateLogin(Login), String.Empty, errorField)
                    Case "Mail"
                        Return If(ValidateMail(Mail, ConfirmMail), String.Empty, errorField)
                    Case "ConfirmMail"
                        Return If(ValidateMail(Mail, ConfirmMail), String.Empty, errorField)
                    Case "Age"
                        Return If(ValidateAge(Age), String.Empty, errorField)
                    Case "CardType"
                        Return If(ValidateCardType(CardType), String.Empty, errorField)
                    Case "CardExpDate"
                        Return If(ValidateCardExpDate(CardExpDate), String.Empty, errorField)
                    Case "CardNumber"
                        Return If(ValidateCardNumber(CardType, CardNumber), String.Empty, errorField)
                End Select

                Return String.Empty
            End Get
        End Property

#End Region
#Region "Validation methods"
        Public Function ValidateLogin(ByVal login As String) As Boolean
            Dim isValid As Boolean = Not Equals(login, "TestUser")
            Return SetError(isValid, "A user with this name is already registered")
        End Function

        Public Function ValidateMail(ByVal mail As String, ByVal confirmMail As String) As Boolean
            Dim isValid As Boolean = Equals(mail, confirmMail)
            Return SetError(isValid, "Two specified e-mail addresses are different")
        End Function

        Public Function ValidateAge(ByVal age As Decimal) As Boolean
            If age < 21 Then Return SetError(False, "Sorry, you're too young to visit our site!")
            If age > 200 Then Return SetError(False, "Congratulations! You're the oldest man on Earth!")
            Return True
        End Function

        Public Function ValidateCardType(ByVal type As String) As Boolean
            Dim isValid As Boolean = Equals(type, "American Express") OrElse Equals(type, "VISA")
            SetError(isValid, "Sorry, cards of this type are not accepted")
            Return isValid
        End Function

        Public Function ValidateCardNumber(ByVal type As String, ByVal number As String) As Boolean
            Dim isValid As Boolean
            Select Case type
                Case "VISA"
                    isValid = Not String.IsNullOrEmpty(number) AndAlso number.Length = 13
                Case "Master Card"
                    isValid = Not String.IsNullOrEmpty(number) AndAlso number.Length = 16
                Case "American Express"
                    isValid = Not String.IsNullOrEmpty(number) AndAlso number.Length = 15
                Case "Diners Club"
                    isValid = Not String.IsNullOrEmpty(number) AndAlso number.Length = 14
                Case Else
                    isValid = False
            End Select

            Return SetError(isValid, "Invalid number")
        End Function

        Public Function ValidateCardExpDate(ByVal expDate As Date) As Boolean
            Dim isValid As Boolean = expDate.CompareTo(Date.Today) > 0
            Return SetError(isValid, "Sorry, your card has expired")
        End Function
#End Region
    End Class

    Public Class EmptyStringValidationRule
        Inherits ValidationRule

        Public Overrides Function Validate(ByVal value As Object, ByVal cultureInfo As Globalization.CultureInfo) As ValidationResult
            Return New ValidationResult(Not String.IsNullOrEmpty(CStr(value)), "Empty")
        End Function
    End Class
End Namespace
