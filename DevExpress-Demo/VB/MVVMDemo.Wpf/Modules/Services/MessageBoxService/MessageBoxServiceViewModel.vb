Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace MVVMDemo.Services

    Public Class MessageBoxServiceViewModel
        Inherits ViewModelBase

        Public Sub ShowMessage(ByVal serviceName As String)
            Dim messageBoxService As IMessageBoxService = GetRequiredService(Of IMessageBoxService)(serviceName)
            Result = messageBoxService.ShowMessage(Text, Caption, Buttons, Icon, DefaultButton)
        End Sub

#Region "properties"
        Public Property Text As String

        Public Property Caption As String

        Public Property Icon As MessageIcon

        Public Overridable Property Result As MessageResult?

        Private buttonsField As MessageButton

        Public Property Buttons As MessageButton
            Get
                Return buttonsField
            End Get

            Set(ByVal value As MessageButton)
                SetValue(buttonsField, value, New Action(AddressOf OnButtonsChanged))
            End Set
        End Property

        Private defaultButtonField As MessageResult

        Public Property DefaultButton As MessageResult
            Get
                Return defaultButtonField
            End Get

            Set(ByVal value As MessageResult)
                SetValue(defaultButtonField, value, New Action(AddressOf UpdatePredefinedFormats))
            End Set
        End Property

        Private messageResultsField As MessageResult

        Public Property MessageResults As MessageResult
            Get
                Return messageResultsField
            End Get

            Set(ByVal value As MessageResult)
                SetValue(messageResultsField, value)
            End Set
        End Property

        Private allowTextSelectionField As Boolean

        Public Property AllowTextSelection As Boolean
            Get
                Return allowTextSelectionField
            End Get

            Set(ByVal value As Boolean)
                SetValue(allowTextSelectionField, value)
            End Set
        End Property

        Private timerTimeoutField As TimeSpan?

        Public Property TimerTimeout As TimeSpan?
            Get
                Return timerTimeoutField
            End Get

            Set(ByVal value As TimeSpan?)
                SetValue(timerTimeoutField, value, New Action(AddressOf UpdatePredefinedFormats))
            End Set
        End Property

        Private timerFormatField As String

        Public Property TimerFormat As String
            Get
                Return timerFormatField
            End Get

            Set(ByVal value As String)
                SetValue(timerFormatField, value)
            End Set
        End Property

        Private predefinedFormatsField As IList(Of PredefinedFormat)

        Public Property PredefinedFormats As IList(Of PredefinedFormat)
            Get
                Return predefinedFormatsField
            End Get

            Set(ByVal value As IList(Of PredefinedFormat))
                SetValue(predefinedFormatsField, value)
            End Set
        End Property

        Public ReadOnly Property Icons As IEnumerable(Of MessageIcon)
            Get
                Return [Enum].GetValues(GetType(MessageIcon)).Cast(Of MessageIcon)().Distinct()
            End Get
        End Property

        Protected Sub New()
            Text = "Message text"
            Caption = "Caption"
            Buttons = MessageButton.OKCancel
            Icon = MessageIcon.Information
            AllowTextSelection = True
            TimerTimeout = TimeSpan.FromSeconds(5)
            TimerFormat = "{0} ({1:%s})"
        End Sub

#End Region
#Region "methods"
        Private Sub OnButtonsChanged()
            DefaultButton = Buttons.ToMessageResults().FirstOrDefault()
        End Sub

        Private Sub UpdatePredefinedFormats()
            If TimerTimeout.HasValue Then PredefinedFormats = Helpers.GeneratePredefinedFormat(DefaultButton, TimerTimeout.Value)
        End Sub
#End Region
    End Class
End Namespace
