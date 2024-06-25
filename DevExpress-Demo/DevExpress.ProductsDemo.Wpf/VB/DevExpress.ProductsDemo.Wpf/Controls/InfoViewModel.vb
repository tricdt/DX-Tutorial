Imports System.Windows.Input
Imports DevExpress.Xpf
Imports DevExpress.Mvvm
Imports DevExpress.Utils

Namespace ProductsDemo

    Public Class InfoViewModel
        Inherits BindableBase

        Private aboutInfoField As AboutInfo

        Private showHelpCommandField As ICommand

        Private showGettingStartedCommandField As ICommand

        Private showBuyNowCommandField As ICommand

        Public ReadOnly Property AboutInfo As AboutInfo
            Get
                If aboutInfoField Is Nothing Then
                    aboutInfoField = New AboutInfo()
                    Dim data As DevExpress.Internal.UserData = DevExpress.Utils.About.Utility.GetInfoEx()
                    aboutInfoField.LicenseState = If(data.IsExpired, LicenseState.TrialExpired, If(data.IsTrial, LicenseState.Trial, LicenseState.Licensed))
                    aboutInfoField.ProductName = "WPF Controls"
                    aboutInfoField.ProductPlatform = "Build Your Own Office"
                    aboutInfoField.RegistrationCode = WpfSerialNumberProvider.GetSerial()
                    aboutInfoField.Version = AssemblyInfo.Version
                End If

                Return aboutInfoField
            End Get
        End Property

        Public Sub ShowHelp()
            Core.DocumentPresenter.OpenLink(AssemblyInfo.DXLinkGetSupport)
        End Sub

        Public Sub ShowGettingStarted()
            Core.DocumentPresenter.OpenLink("https://go.devexpress.com/Demo_GetStarted_WPF.aspx")
        End Sub

        Public Sub ShowBuyNow()
            Core.DocumentPresenter.OpenLink("https://go.devexpress.com/Demo_Subscriptions_Buy.aspx")
        End Sub

        Public ReadOnly Property ShowHelpCommand As ICommand
            Get
                If showHelpCommandField Is Nothing Then showHelpCommandField = New DelegateCommand(AddressOf ShowHelp, False)
                Return showHelpCommandField
            End Get
        End Property

        Public ReadOnly Property ShowGettingStartedCommand As ICommand
            Get
                If showGettingStartedCommandField Is Nothing Then showGettingStartedCommandField = New DelegateCommand(AddressOf ShowGettingStarted, False)
                Return showGettingStartedCommandField
            End Get
        End Property

        Public ReadOnly Property ShowBuyNowCommand As ICommand
            Get
                If showBuyNowCommandField Is Nothing Then showBuyNowCommandField = New DelegateCommand(AddressOf ShowBuyNow, False)
                Return showBuyNowCommandField
            End Get
        End Property
    End Class
End Namespace
