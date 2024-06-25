Imports DevExpress.Mvvm.POCO

Namespace DialogsDemo

    Public MustInherit Class BaseThemedWindowContentModel

        Public ReadOnly Property RootViewModel As ThemedWindowViewModel
            Get
                Return GetParentViewModel(Of ThemedWindowViewModel)()
            End Get
        End Property
    End Class
End Namespace
