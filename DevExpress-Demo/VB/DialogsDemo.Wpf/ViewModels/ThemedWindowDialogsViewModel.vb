Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports DevExpress.Xpf.Grid

Namespace DialogsDemo

    Public Class ThemedWindowDialogsViewModel

        Public Sub New()
            CreateContent()
        End Sub

        Protected Overridable ReadOnly Property DialogService As IDialogService
            Get
                Return Nothing
            End Get
        End Property

        Protected Overridable ReadOnly Property MessageBoxService As IMessageBoxService
            Get
                Return Nothing
            End Get
        End Property

        Public Overridable Property Content As Object

        Public Overridable Property FocusedRow As Employee

        Public Sub ShowDialogWindow(ByVal parameter As MouseButtonEventArgs)
            Dim hitObject = TryCast(parameter.Source, CardView).CalcHitInfo(TryCast(parameter.OriginalSource, DependencyObject))
            If hitObject.HitTest = CardViewHitTest.Card Then DialogService.ShowDialog(MessageButton.OK, "Show record", Me)
        End Sub

        Public Sub ShowMessage()
            MessageBoxService.ShowMessage("Would you like to send an e-mail?", "Confirm", MessageButton.OKCancel, MessageIcon.Question)
        End Sub

        Protected Function CreateChildModel(Of TChildModel)() As TChildModel
            Dim model = ViewModelSource(Of TChildModel).Create()
            model.SetParentViewModel(Me)
            Return model
        End Function

        Protected Sub CreateContent()
            Content = CreateChildModel(Of ThemedWindowDialogContentModel)()
        End Sub
    End Class

    Public Class ThemedWindowDialogContentModel

        Public ReadOnly Property RootViewModel As ThemedWindowDialogsViewModel
            Get
                Return GetParentViewModel(Of ThemedWindowDialogsViewModel)()
            End Get
        End Property
    End Class
End Namespace
