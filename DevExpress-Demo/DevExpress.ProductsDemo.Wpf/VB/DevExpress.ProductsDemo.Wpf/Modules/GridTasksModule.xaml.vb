Imports DevExpress.Data
Imports DevExpress.Xpf.Grid
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Printing
Imports System
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls

Namespace ProductsDemo.Modules

    Public Partial Class GridTasksModule
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub TableView_CellValueChanging(ByVal sender As Object, ByVal e As CellValueChangedEventArgs)
            e.Source.PostEditor()
        End Sub
    End Class

    Public MustInherit Class GridControlBehaviorBase
        Inherits Behavior(Of GridControl)

        Protected ViewModel As GridViewModelBaseType

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            ViewModel = TryCast(AssociatedObject.DataContext, GridViewModelBaseType)
        End Sub
    End Class

    Public Class GridControlColumnsUpdateLocker
        Inherits GridControlBehaviorBase

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler ViewModel.IsLoadingChanged, AddressOf viewModel_IsLoadingChanged
        End Sub

        Private Sub viewModel_IsLoadingChanged(ByVal sender As Object, ByVal e As IsLoadingEventArgs)
            If e.IsLoading Then
                AssociatedObject.Columns.BeginUpdate()
                AssociatedObject.SortInfo.BeginUpdate()
            Else
                AssociatedObject.SortInfo.EndUpdate()
                AssociatedObject.Columns.EndUpdate()
            End If
        End Sub

        Protected Overrides Sub OnDetaching()
            RemoveHandler ViewModel.IsLoadingChanged, AddressOf viewModel_IsLoadingChanged
            MyBase.OnDetaching()
        End Sub
    End Class

    Public Class GridControlPrint
        Inherits GridControlBehaviorBase

        Private printGridControl As GridControl

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            PrintableControlLink = GetPrintableControlLink()
            RemoveHandler ViewModel.Print, AddressOf ViewModel_Print
            AddHandler ViewModel.Print, AddressOf ViewModel_Print
        End Sub

        Protected Overrides Sub OnDetaching()
            PrintableControlLink = Nothing
            RemoveHandler ViewModel.Print, AddressOf ViewModel_Print
            MyBase.OnDetaching()
        End Sub

        Private Function GetPrintableControlLink() As PrintableControlLink
            Return New PrintableControlLink(GetPrintGridControl().View)
        End Function

        Private Sub ViewModel_Print(ByVal sender As Object, ByVal e As EventArgs)
            Dim link = GetPrintableControlLink()
            Dim preview = New DocumentPreviewControl() With {.DocumentSource = link}
            link.CreateDocument(True)
            Dim previewWindow = New Window() With {.Content = preview, .Title = "Print Preview"}
            previewWindow.ShowDialog()
        End Sub

        Private Function GetPrintGridControl() As GridControl
            If TypeOf AssociatedObject.View Is TableView Then Return AssociatedObject
            If printGridControl Is Nothing Then
                printGridControl = TryCast(AssociatedObject.TryFindResource("printGridControl"), GridControl)
                printGridControl.DataContext = AssociatedObject.DataContext
                printGridControl.Style = TryCast(AssociatedObject.TryFindResource("gridControlMVVMStyle"), Style)
                Call Interaction.GetBehaviors(printGridControl).Add(New GridControlColumnsUpdateLocker())
            End If

            Return printGridControl
        End Function
    End Class

    Public Class StatusBarSummaryUpdate
        Inherits GridControlBehaviorBase

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.CustomSummary, AddressOf AssociatedObject_CustomSummary
        End Sub

        Public Shared ReadOnly CountProperty As DependencyProperty = DependencyProperty.Register("Count", GetType(Integer), GetType(StatusBarSummaryUpdate), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property Count As Integer
            Get
                Return CInt(GetValue(CountProperty))
            End Get

            Set(ByVal value As Integer)
                SetValue(CountProperty, value)
            End Set
        End Property

        Private Sub AssociatedObject_CustomSummary(ByVal sender As Object, ByVal e As CustomSummaryEventArgs)
            Select Case e.SummaryProcess
                Case CustomSummaryProcess.Start
                    Count = 0
                Case CustomSummaryProcess.Calculate
                Case CustomSummaryProcess.Finalize
                    Count = CInt(AssociatedObject.GetTotalSummaryValue(AssociatedObject.TotalSummary(0)))
            End Select
        End Sub

        Protected Overrides Sub OnDetaching()
            RemoveHandler AssociatedObject.CustomSummary, AddressOf AssociatedObject_CustomSummary
            MyBase.OnDetaching()
        End Sub
    End Class
End Namespace
