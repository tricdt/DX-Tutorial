Imports DevExpress.Mvvm.POCO

Namespace DialogsDemo

    Public MustInherit Class BaseHeaderItemModel

        Public Overridable Property IsVisible As Boolean

        Public ReadOnly Property BaseModel As ThemedWindowViewModel
            Get
                Return GetParentViewModel(Of ThemedWindowViewModel)()
            End Get
        End Property

        Public Property ResourceKey As String
    End Class
End Namespace
