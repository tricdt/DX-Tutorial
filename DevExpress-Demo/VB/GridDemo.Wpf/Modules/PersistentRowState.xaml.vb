Imports DevExpress.DemoData.Models.Vehicles
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Utils
Imports System
Imports System.Linq
Imports System.Windows
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Threading

Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/PersistentRowStateTemplates.xaml")>
    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/PersistentRowStateClasses.(cs)")>
    Public Partial Class PersistentRowState
        Inherits GridDemoModule

        Public Shared ReadOnly SetCursorPositionCommandProperty As DependencyProperty

        Public Shared ReadOnly CanExecuteHandlerCommandProperty As DependencyProperty

        Shared Sub New()
            Dim ownerType As Type = GetType(PersistentRowState)
            SetCursorPositionCommandProperty = DependencyPropertyManager.Register("SetCursorPositionCommand", GetType(ICommand), ownerType)
            CanExecuteHandlerCommandProperty = DependencyPropertyManager.Register("CanExecuteHandlerCommand", GetType(ICommand), ownerType)
        End Sub

        Public Sub New()
            DataContext = Me
            SetCursorPositionCommand = New DelegateCommand(Of FrameworkElement)(AddressOf SetCursorPosition)
            CanExecuteHandlerCommand = New DelegateCommand(Of CanExecuteRoutedEventArgs)(AddressOf OnDescriptionTextBoxCommandPreviewCanExecute)
            InitializeComponent()
            grid.ItemsSource = New VehiclesContext().Models.ToList()
            UpdateView()
        End Sub

        Private Sub viewListBox_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            If grid Is Nothing Then Return
            UpdateView()
        End Sub

        Private Sub UpdateView()
            If viewListBox.SelectedIndex = 0 Then grid.View = CType(FindResource("gridView"), GridViewBase)
            If viewListBox.SelectedIndex = 1 Then grid.View = CType(FindResource("cardView"), GridViewBase)
        End Sub

        Private Sub SetCursorPosition(ByVal sender As FrameworkElement)
            Me.sender = sender
            Dispatcher.BeginInvoke(DispatcherPriority.Render, New Action(AddressOf SetCursorPositionCore))
        End Sub

        Private sender As FrameworkElement

        Private Sub SetCursorPositionCore()
            Dim point As Point = sender.PointToScreen(New Point(sender.ActualWidth / 2, sender.ActualHeight / 2))
            SetCursorPos(CInt(point.X), CInt(point.Y))
        End Sub

        Private Sub OnDescriptionTextBoxCommandPreviewCanExecute(ByVal e As CanExecuteRoutedEventArgs)
            If e.Command Is EditingCommands.MoveDownByLine OrElse e.Command Is EditingCommands.MoveUpByLine OrElse e.Command Is EditingCommands.MoveUpByPage OrElse e.Command Is EditingCommands.MoveDownByPage Then
                e.ContinueRouting = True
            End If
        End Sub

        Public Property SetCursorPositionCommand As ICommand
            Get
                Return CType(GetValue(SetCursorPositionCommandProperty), ICommand)
            End Get

            Set(ByVal value As ICommand)
                SetValue(SetCursorPositionCommandProperty, value)
            End Set
        End Property

        Public Property CanExecuteHandlerCommand As ICommand
            Get
                Return CType(GetValue(CanExecuteHandlerCommandProperty), ICommand)
            End Get

            Set(ByVal value As ICommand)
                SetValue(CanExecuteHandlerCommandProperty, value)
            End Set
        End Property
    End Class
End Namespace
