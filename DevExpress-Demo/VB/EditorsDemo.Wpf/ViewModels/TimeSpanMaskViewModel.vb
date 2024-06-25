Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Data.Mask
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.Native
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Editors

Namespace EditorsDemo

    Public Class TimeSpanMaskViewModel
        Inherits ViewModelBase

        Private _EditorGotFocusCommand As ICommand, _DemoModuleLoadedCommand As ICommand

        Public Property FocusedEditor As TextEdit
            Get
                Return GetProperty(Function() Me.FocusedEditor)
            End Get

            Set(ByVal value As TextEdit)
                SetProperty(Function() FocusedEditor, value, New System.Action(AddressOf OnFocusedEditorChanged))
            End Set
        End Property

        Public Property Mask As String
            Get
                Return GetProperty(Function() Me.Mask)
            End Get

            Set(ByVal value As String)
                SetProperty(Function() Mask, value, New System.Action(AddressOf Me.OnMaskChanged))
            End Set
        End Property

        Public Property MaskTypeIndex As Integer
            Get
                Return GetProperty(Function() Me.MaskTypeIndex)
            End Get

            Set(ByVal value As Integer)
                SetProperty(Function() MaskTypeIndex, value, New System.Action(AddressOf Me.OnMaskTypeIndexChanged))
            End Set
        End Property

        Public Property DefaultPart As TimeSpanPart?
            Get
                Return GetProperty(Function() Me.DefaultPart)
            End Get

            Set(ByVal value As TimeSpanPart?)
                SetProperty(Function() DefaultPart, value, New System.Action(AddressOf OnDefaultPartChanged))
            End Set
        End Property

        Public Property InputMode As TimeSpanInputMode
            Get
                Return GetProperty(Function() Me.InputMode)
            End Get

            Set(ByVal value As TimeSpanInputMode)
                SetProperty(Function() InputMode, value, New System.Action(AddressOf OnAllowLargeValueInputChanged))
            End Set
        End Property

        Public Property ChangeNextPartOnCycleValueChange As Boolean
            Get
                Return GetProperty(Function() Me.ChangeNextPartOnCycleValueChange)
            End Get

            Set(ByVal value As Boolean)
                SetProperty(Function() ChangeNextPartOnCycleValueChange, value, New System.Action(AddressOf OnChangeNextPartOnCycleValueChangeChanged))
            End Set
        End Property

        Public Property AssignValueToEnteredLiteral As Boolean
            Get
                Return GetProperty(Function() Me.AssignValueToEnteredLiteral)
            End Get

            Set(ByVal value As Boolean)
                SetProperty(Function() AssignValueToEnteredLiteral, value, New System.Action(AddressOf OnAssignValueToEnteredLiteralChanged))
            End Set
        End Property

        Public Property EditorGotFocusCommand As ICommand
            Get
                Return _EditorGotFocusCommand
            End Get

            Private Set(ByVal value As ICommand)
                _EditorGotFocusCommand = value
            End Set
        End Property

        Public Property DemoModuleLoadedCommand As ICommand
            Get
                Return _DemoModuleLoadedCommand
            End Get

            Private Set(ByVal value As ICommand)
                _DemoModuleLoadedCommand = value
            End Set
        End Property

        Public Sub New()
            EditorGotFocusCommand = New DelegateCommand(Of RoutedEventArgs)(AddressOf OnEditorGotFocus)
            DemoModuleLoadedCommand = New DelegateCommand(Of RoutedEventArgs)(AddressOf OnDemoModuleLoaded)
        End Sub

        Private Sub OnDemoModuleLoaded(ByVal obj As RoutedEventArgs)
            Call LayoutHelper.FindElementByType(Of TextEdit)(TryCast(obj.Source, FrameworkElement)).Focus()
        End Sub

        Private Sub OnEditorGotFocus(ByVal obj As RoutedEventArgs)
            FocusedEditor = TryCast(obj.Source, TextEdit)
        End Sub

        Private Sub OnFocusedEditorChanged()
            If FocusedEditor Is Nothing Then Return
            Mask = FocusedEditor.Mask
            MaskTypeIndex = FocusedEditor.MaskType - MaskType.TimeSpan
            DefaultPart = TimeSpanMaskOptions.GetDefaultPart(FocusedEditor)
            InputMode = TimeSpanMaskOptions.GetInputMode(FocusedEditor)
            ChangeNextPartOnCycleValueChange = TimeSpanMaskOptions.GetChangeNextPartOnCycleValueChange(FocusedEditor)
            AssignValueToEnteredLiteral = TimeSpanMaskOptions.GetAssignValueToEnteredLiteral(FocusedEditor)
        End Sub

        Private Sub OnMaskChanged()
            FocusedEditor.[Do](Sub(x) x.Mask = Mask)
        End Sub

        Private Sub OnMaskTypeIndexChanged()
            FocusedEditor.[Do](Sub(x) x.MaskType = MaskType.TimeSpan + MaskTypeIndex)
        End Sub

        Private Sub OnDefaultPartChanged()
            FocusedEditor.[Do](Sub(x) TimeSpanMaskOptions.SetDefaultPart(x, DefaultPart))
        End Sub

        Private Sub OnAllowLargeValueInputChanged()
            FocusedEditor.[Do](Sub(x) TimeSpanMaskOptions.SetInputMode(x, InputMode))
        End Sub

        Private Sub OnChangeNextPartOnCycleValueChangeChanged()
            FocusedEditor.[Do](Sub(x) TimeSpanMaskOptions.SetChangeNextPartOnCycleValueChange(x, ChangeNextPartOnCycleValueChange))
        End Sub

        Private Sub OnAssignValueToEnteredLiteralChanged()
            FocusedEditor.[Do](Sub(x) TimeSpanMaskOptions.SetAssignValueToEnteredLiteral(x, AssignValueToEnteredLiteral))
        End Sub
    End Class
End Namespace
