Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Threading
Imports System.Threading.Tasks
Imports ControlsDemo.Helpers
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace ControlsDemo.Wizard.WpfTour

    Public Class WizardViewModel

        Private _ConfirmPageViewModel As ConfirmPageViewModel, _CongratulationsPageViewModel As CongratulationsPageViewModel, _Items As ObservableCollection(Of Object), _NotSoFastPageViewModel As NotSoFastPageViewModel, _PlayTunePageViewModel As PlayTunePageViewModel, _RobotPageViewModel As RobotPageViewModel, _ReadEulaPageViewModel As ReadEulaPageViewModel, _TimeConsumePageViewModel As TimeConsumePageViewModel, _WelcomePageViewModel As WelcomePageViewModel

        Public Sub New()
            Me.WelcomePageViewModel = CreateViewModel(Of ControlsDemo.Wizard.WpfTour.WelcomePageViewModel)()
            Me.PlayTunePageViewModel = CreateViewModel(Of ControlsDemo.Wizard.WpfTour.PlayTunePageViewModel)()
            Me.ReadEulaPageViewModel = CreateViewModel(Of ControlsDemo.Wizard.WpfTour.ReadEulaPageViewModel)()
            Me.ConfirmPageViewModel = CreateViewModel(Of ControlsDemo.Wizard.WpfTour.ConfirmPageViewModel)()
            Me.NotSoFastPageViewModel = CreateViewModel(Of ControlsDemo.Wizard.WpfTour.NotSoFastPageViewModel)()
            Me.TimeConsumePageViewModel = CreateViewModel(Of ControlsDemo.Wizard.WpfTour.TimeConsumePageViewModel)()
            Me.RobotPageViewModel = CreateViewModel(Of ControlsDemo.Wizard.WpfTour.RobotPageViewModel)()
            Me.CongratulationsPageViewModel = CreateViewModel(Of ControlsDemo.Wizard.WpfTour.CongratulationsPageViewModel)()
            Me.ConfirmPageViewModel.TimeConsumePage = Me.TimeConsumePageViewModel
            Me.ConfirmPageViewModel.NotSoFastPage = Me.NotSoFastPageViewModel
            Me.Items = New System.Collections.ObjectModel.ObservableCollection(Of Object) From {Me.WelcomePageViewModel, Me.PlayTunePageViewModel, Me.ReadEulaPageViewModel, Me.ConfirmPageViewModel, Me.NotSoFastPageViewModel, Me.TimeConsumePageViewModel, Me.RobotPageViewModel, Me.CongratulationsPageViewModel}
        End Sub

        Public Property ConfirmPageViewModel As ConfirmPageViewModel
            Get
                Return _ConfirmPageViewModel
            End Get

            Private Set(ByVal value As ConfirmPageViewModel)
                _ConfirmPageViewModel = value
            End Set
        End Property

        Public Property CongratulationsPageViewModel As CongratulationsPageViewModel
            Get
                Return _CongratulationsPageViewModel
            End Get

            Private Set(ByVal value As CongratulationsPageViewModel)
                _CongratulationsPageViewModel = value
            End Set
        End Property

        Public Property Items As ObservableCollection(Of Object)
            Get
                Return _Items
            End Get

            Private Set(ByVal value As ObservableCollection(Of Object))
                _Items = value
            End Set
        End Property

        Public Property NotSoFastPageViewModel As NotSoFastPageViewModel
            Get
                Return _NotSoFastPageViewModel
            End Get

            Private Set(ByVal value As NotSoFastPageViewModel)
                _NotSoFastPageViewModel = value
            End Set
        End Property

        Public Property PlayTunePageViewModel As PlayTunePageViewModel
            Get
                Return _PlayTunePageViewModel
            End Get

            Private Set(ByVal value As PlayTunePageViewModel)
                _PlayTunePageViewModel = value
            End Set
        End Property

        Public Property RobotPageViewModel As RobotPageViewModel
            Get
                Return _RobotPageViewModel
            End Get

            Private Set(ByVal value As RobotPageViewModel)
                _RobotPageViewModel = value
            End Set
        End Property

        Public Property ReadEulaPageViewModel As ReadEulaPageViewModel
            Get
                Return _ReadEulaPageViewModel
            End Get

            Private Set(ByVal value As ReadEulaPageViewModel)
                _ReadEulaPageViewModel = value
            End Set
        End Property

        Public Property TimeConsumePageViewModel As TimeConsumePageViewModel
            Get
                Return _TimeConsumePageViewModel
            End Get

            Private Set(ByVal value As TimeConsumePageViewModel)
                _TimeConsumePageViewModel = value
            End Set
        End Property

        Public Property WelcomePageViewModel As WelcomePageViewModel
            Get
                Return _WelcomePageViewModel
            End Get

            Private Set(ByVal value As WelcomePageViewModel)
                _WelcomePageViewModel = value
            End Set
        End Property

        Public Overridable Property SelectedItem As WizardPageBase

        Public Shared Function Create() As WizardViewModel
            Return DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New ControlsDemo.Wizard.WpfTour.WizardViewModel())
        End Function

        Public Sub Cancel(ByVal e As System.ComponentModel.CancelEventArgs)
            If Me.SelectedItem IsNot Nothing Then Me.SelectedItem.Cancel(e)
        End Sub

        Private Function CreateViewModel(Of T As {ControlsDemo.Wizard.WpfTour.WizardPageBase, New})() As T
            Dim viewModel = DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New T())
            viewModel.SetParentViewModel(Me)
            Return viewModel
        End Function
    End Class

    Public MustInherit Class WizardPageBase

        Protected Sub New()
            Me.CanBack = True
            Me.CanNext = Me.CanBack
            Me.CanCancel = Me.CanNext
            Me.CanFinish = False
            Me.ShowCancel = True
            Me.ShowBack = Me.ShowCancel
            Me.ShowNext = Me.ShowBack
            Me.ShowFinish = False
        End Sub

        Public Overridable Property CanBack As Boolean

        Public Overridable Property CanCancel As Boolean

        Public Overridable Property CanFinish As Boolean

        Public Overridable Property CanNext As Boolean

        Public Overridable Property Description As String

        Public Overridable Property GoToPage As Object

        Public Overridable ReadOnly Property Header As String
            Get
                Return String.Empty
            End Get
        End Property

        Public Overridable Property ShowBack As Boolean

        Public Overridable Property ShowCancel As Boolean

        Public Overridable Property ShowFinish As Boolean

        Public Overridable Property ShowNext As Boolean

        Public Sub Cancel(ByVal e As System.ComponentModel.CancelEventArgs)
            Me.OnCancel(e)
        End Sub

        Protected Overridable Sub OnCancel(ByVal e As System.ComponentModel.CancelEventArgs)
            If DevExpress.Mvvm.POCO.POCOViewModelExtensions.GetService(Of DevExpress.Mvvm.IMessageBoxService)(Me).ShowMessage("Do you want to exit the Wizard Control Feature Tour?", "Wizard Control", DevExpress.Mvvm.MessageButton.YesNo, DevExpress.Mvvm.MessageIcon.Question) = DevExpress.Mvvm.MessageResult.No Then e.Cancel = True
        End Sub
    End Class

    Public Class WelcomePageViewModel
        Inherits ControlsDemo.Wizard.WpfTour.WizardPageBase

        Public Sub New()
            Me.CanBack = False
            Me.ShowBack = False
        End Sub

        Public Overrides ReadOnly Property Header As String
            Get
                Return String.Empty
            End Get
        End Property
    End Class

    Public Class PlayTunePageViewModel
        Inherits ControlsDemo.Wizard.WpfTour.WizardPageBase

        Public Sub New()
            Me.Description = "To make this demo more entertaining, we would like to play a tune for you. Simple choose your favorite track."
        End Sub

        Public Overrides ReadOnly Property Header As String
            Get
                Return "Step 2 - Play a tune"
            End Get
        End Property

        Public Overridable Property Song As String

        Public Function CanPlay() As Boolean
            Return Not String.IsNullOrEmpty(Me.Song)
        End Function

        Public Sub Play()
            Dim text As String = "Sorry, but we don't have that song in our library..." & System.Environment.NewLine
            text += "But we are agree with you that ""{0}"" is an excellent choice."
            text = String.Format(text, Me.Song)
            DevExpress.Mvvm.POCO.POCOViewModelExtensions.GetService(Of DevExpress.Mvvm.IMessageBoxService)(Me).ShowMessage(text, "Wizard Control", DevExpress.Mvvm.MessageButton.OK, DevExpress.Mvvm.MessageIcon.Information)
            DevExpress.Mvvm.POCO.POCOViewModelExtensions.GetService(Of DevExpress.Mvvm.IWizardService)(Me).GoForward()
            Call DevExpress.Mvvm.Messenger.[Default].Send(New ControlsDemo.Wizard.WpfTour.SongMessage() With {.Song = Me.Song})
        End Sub
    End Class

    Public Class ReadEulaPageViewModel
        Inherits ControlsDemo.Wizard.WpfTour.WizardPageBase
        Implements DevExpress.Mvvm.ISupportWizardNextCommand

        Private longTextTimer As System.Diagnostics.Stopwatch

        Public Sub New()
            Me.CanNext = False
            Me.Description = "Before proceeding, we want you to read and understand the following text, which is very long."
        End Sub

        Public ReadOnly Property Eula As String
            Get
                Return ControlsDemo.Helpers.WizardDemoHelper.VeryLongText
            End Get
        End Property

        Public Overrides ReadOnly Property Header As String
            Get
                Return "Step 3 - The Read Some Very Long Text step"
            End Get
        End Property

        Public Overridable Property IsAgreed As Boolean

        Private ReadOnly Property CanGoForward As Boolean Implements Global.DevExpress.Mvvm.ISupportWizardNextCommand.CanGoForward
            Get
                Return Me.CanNext
            End Get
        End Property

        Public Sub StartTimer()
            Me.longTextTimer = System.Diagnostics.Stopwatch.StartNew()
        End Sub

        Protected Sub OnIsAgreedChanged()
            Me.CanNext = Me.IsAgreed
        End Sub

        Private Sub OnGoForward(ByVal e As System.ComponentModel.CancelEventArgs) Implements Global.DevExpress.Mvvm.ISupportWizardNextCommand.OnGoForward
            Dim elapsed = CInt(Me.longTextTimer.Elapsed.TotalSeconds)
            If elapsed < 60 Then
                Dim result = DevExpress.Mvvm.POCO.POCOViewModelExtensions.GetService(Of DevExpress.Mvvm.IMessageBoxService)(Me).ShowMessage(String.Format("Are you sure that {0} seconds was enough time for you to read all that text?", CInt(Me.longTextTimer.Elapsed.TotalSeconds)), "Wizard Control", DevExpress.Mvvm.MessageButton.YesNo, DevExpress.Mvvm.MessageIcon.Question)
                If result = DevExpress.Mvvm.MessageResult.No Then e.Cancel = True
            End If
        End Sub
    End Class

    Public Class ConfirmPageViewModel
        Inherits ControlsDemo.Wizard.WpfTour.WizardPageBase

        Public Sub New()
            Me.ShowNoSoFastPage = True
            Me.GoToPage = If(Me.ShowNoSoFastPage, Me.NotSoFastPage, Me.TimeConsumePage)
        End Sub

        Public Overrides ReadOnly Property Header As String
            Get
                Return "Step 4 - Are You Tired Yet?"
            End Get
        End Property

        Public Overridable Property NotSoFastPage As Object

        Public Overridable Property ShowNoSoFastPage As Boolean

        Public Overridable Property TimeConsumePage As Object

        Protected Sub OnShowNoSoFastPageChanged()
            Me.GoToPage = If(Me.ShowNoSoFastPage, Me.NotSoFastPage, Me.TimeConsumePage)
        End Sub
    End Class

    Public Class NotSoFastPageViewModel
        Inherits ControlsDemo.Wizard.WpfTour.WizardPageBase

        Public Overrides ReadOnly Property Header As String
            Get
                Return "Not so Fast!"
            End Get
        End Property
    End Class

    Public Class TimeConsumePageViewModel
        Inherits ControlsDemo.Wizard.WpfTour.WizardPageBase

        Public Overrides ReadOnly Property Header As String
            Get
                Return "Step 5 - Time-consuming Operation"
            End Get
        End Property

        Public Overridable Property IsCompleted As Boolean

        Public ReadOnly Property MaximumProgress As Integer
            Get
                Return 100
            End Get
        End Property

        Public ReadOnly Property MinimumProgress As Integer
            Get
                Return 0
            End Get
        End Property

        Public Overridable Property Progress As Integer

        Public Sub Clear()
            Me.Progress = 0
            Me.CanBack = False
            Me.CanNext = Me.CanBack
            Me.IsCompleted = Me.CanNext
        End Sub

        Public Function StartProcess() As Task
            Me.Clear()
            Return System.Threading.Tasks.Task.Factory.StartNew(New Global.System.Action(AddressOf Me.Process))
        End Function

        Private Sub Process()
            Call System.Threading.Thread.Sleep(System.TimeSpan.FromSeconds(1))
            Dim command = Me.GetAsyncCommand(Function(x) x.StartProcess())
            For i As Integer = Me.MinimumProgress To Me.MaximumProgress
                If command.IsCancellationRequested Then Exit For
                Me.Progress = i
                Call System.Threading.Thread.Sleep(System.TimeSpan.FromSeconds(0.02))
            Next

            Me.CanBack = True
            Me.CanNext = Me.CanBack
            Me.IsCompleted = Me.CanNext
        End Sub
    End Class

    Public Class RobotPageViewModel
        Inherits ControlsDemo.Wizard.WpfTour.WizardPageBase

        Public Overrides ReadOnly Property Header As String
            Get
                Return "Step 6 - Are You a Robot?"
            End Get
        End Property

        Public Overridable Property Capture As String

        Protected Sub OnCaptureChanged()
            Call DevExpress.Mvvm.Messenger.[Default].Send(New ControlsDemo.Wizard.WpfTour.IsRobotMessage() With {.IsRobot = Not System.[Object].Equals(Me.Capture.ToLower(), "devexpress123")})
        End Sub
    End Class

    Public Class CongratulationsPageViewModel
        Inherits ControlsDemo.Wizard.WpfTour.WizardPageBase

        Public Sub New()
            Me.CanFinish = True
            Me.CanCancel = False
            Me.ShowFinish = True
            Me.ShowCancel = False
            Me.ShowNext = Me.ShowCancel
            Me.IsRobot = True
            Me.UpdateDescription()
            Call DevExpress.Mvvm.Messenger.[Default].Register(Of ControlsDemo.Wizard.WpfTour.IsRobotMessage)(Me, New Global.System.Action(Of Global.ControlsDemo.Wizard.WpfTour.IsRobotMessage)(AddressOf Me.OnIsRobotMessage))
            Call DevExpress.Mvvm.Messenger.[Default].Register(Of ControlsDemo.Wizard.WpfTour.SongMessage)(Me, New Global.System.Action(Of Global.ControlsDemo.Wizard.WpfTour.SongMessage)(AddressOf Me.OnSongMessage))
        End Sub

        Public Overridable Property IsRobot As Boolean

        Public Overridable Property Song As String

        Protected Overrides Sub OnCancel(ByVal e As System.ComponentModel.CancelEventArgs)
        End Sub

        Private Function GetArtistName(ByVal value As String) As String
            If String.IsNullOrEmpty(value) Then Return String.Empty
            Return value.Substring(0, value.IndexOf("-") - 1)
        End Function

        Private Sub OnIsRobotMessage(ByVal obj As ControlsDemo.Wizard.WpfTour.IsRobotMessage)
            Me.IsRobot = obj.IsRobot
            Me.UpdateDescription()
        End Sub

        Private Sub OnSongMessage(ByVal obj As ControlsDemo.Wizard.WpfTour.SongMessage)
            Me.Song = obj.Song
            Me.UpdateDescription()
        End Sub

        Private Sub UpdateDescription()
            If Me.IsRobot Then
                Me.Description = "We are very sorry, but no robots are allowed to continue this wizard. Please click Finish to exit."
            Else
                Dim artist As String = Me.GetArtistName(Me.Song)
                Me.Description = If(String.IsNullOrEmpty(artist), "Thank you for completing this Wizard Control Feature Tour! We can now conclusively state that you are very patient, a quick reader and definitely not a robot.", String.Format("Thank you for completing this Wizard Control Feature Tour! We can now conclusively state that you are very patient, definitely not a robot, a quick reader, and a fan of {0} just like we are.", artist))
            End If
        End Sub
    End Class

    Public Class IsRobotMessage

        Public Property IsRobot As Boolean
    End Class

    Public Class SongMessage

        Public Property Song As String
    End Class
End Namespace
