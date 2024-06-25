Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Editors

Namespace EditorsDemo

    Public Class InputValidationViewModel
        Inherits ValidationViewModelBase

#Region "Validation Commands"
        <Command>
        Public Sub ValidateLogin(ByVal args As ValidationArgs)
            Dim login As String = TryCast(args.Value, String)
            Dim isValid As Boolean = Not Equals(login, "TestUser")
            args.SetError(isValid, "A user with this name is already registered")
        End Sub

        <Command>
        Public Sub ValidateMail(ByVal args As ValidationArgs)
            Dim confirmMail As String = TryCast(args.Value, String)
            Dim isValid As Boolean = Equals(Mail, confirmMail)
            args.SetError(isValid, "Two specified e-mail addresses are different")
        End Sub

        <Command>
        Public Sub ValidateName(ByVal args As ValidationArgs)
            Dim name As String = TryCast(args.Value, String)
            Dim isValid As Boolean = Not String.IsNullOrEmpty(name)
            args.SetError(isValid, "Name can't be empty")
        End Sub

        <Command>
        Public Sub ValidateAge(ByVal args As ValidationArgs)
            Dim age As Decimal = CDec(args.Value)
            If age < 21 Then args.SetError(False, "Sorry, you're too young to visit our site!")
            If age > 200 Then args.SetError(False, "Congratulations! You're the oldest man on Earth!")
        End Sub

        <Command>
        Public Sub ValidateCardType(ByVal args As ValidationArgs)
            Dim type As String = TryCast(args.Value, String)
            Dim isValid As Boolean = Equals(type, "American Express") OrElse Equals(type, "VISA")
            args.SetError(isValid, "Sorry, cards of this type are not accepted")
        End Sub

        <Command>
        Public Sub ValidateCardNumber(ByVal args As ValidationArgs)
            Dim number As String = TryCast(args.Value, String)
            Dim isValid As Boolean
            Select Case CardType
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

            args.SetError(isValid, "Invalid number")
        End Sub

        <Command>
        Public Sub ValidateCardExpDate(ByVal args As ValidationArgs)
            Dim expDate As Date = CDate(args.Value)
            Dim isValid As Boolean = expDate.CompareTo(Date.Today) > 0
            args.SetError(isValid, "Sorry, your card has expired")
        End Sub

#End Region
#Region "Show Message"
        Private ReadOnly Property MessageBoxService As IMessageBoxService
            Get
                Return ServiceContainer.GetService(Of IMessageBoxService)()
            End Get
        End Property

        <Command>
        Public Sub ShowMessage()
            MessageBoxService.ShowMessage("Thank you!", "Joined", MessageButton.OK, MessageIcon.None)
        End Sub
#End Region
    End Class

    Public Class ValidationArgs

        Private _ErrorContent As String, _Value As Object

        Public Property ErrorContent As String
            Get
                Return _ErrorContent
            End Get

            Private Set(ByVal value As String)
                _ErrorContent = value
            End Set
        End Property

        Public Property Value As Object
            Get
                Return _Value
            End Get

            Private Set(ByVal value As Object)
                _Value = value
            End Set
        End Property

        Public Sub New(ByVal value As Object)
            Me.Value = value
        End Sub

        Public Sub SetError(ByVal isValid As Boolean, ByVal errorContent As String)
            Me.ErrorContent = If(isValid, Nothing, errorContent)
        End Sub
    End Class

    Public Class ValidationEventArgsConverter
        Inherits EventArgsConverterBase(Of ValidationEventArgs)

        Protected Overrides Function Convert(ByVal sender As Object, ByVal e As ValidationEventArgs) As Object
            Return New ValidationArgs(e.Value)
        End Function

        Protected Overrides Sub ConvertBack(ByVal sender As Object, ByVal e As ValidationEventArgs, ByVal parameter As Object)
            Dim args = CType(parameter, ValidationArgs)
            e.IsValid = Equals(args.ErrorContent, Nothing)
            e.ErrorContent = args.ErrorContent
        End Sub
    End Class
End Namespace
