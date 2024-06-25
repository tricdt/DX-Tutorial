Imports System.Windows.Controls
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels

    Public Class ViewSettingsViewModel

        Public Shared Function Create(ByVal viewKind As CollectionViewKind, ByVal parentViewModel As Object) As ViewSettingsViewModel
            Return ViewModelSource.Create(Function() New ViewSettingsViewModel(viewKind)).SetParentViewModel(parentViewModel)
        End Function

        Private defaultCollectionViewKind As CollectionViewKind

        Protected Sub New(ByVal defaultCollectionViewKind As CollectionViewKind)
            Me.defaultCollectionViewKind = defaultCollectionViewKind
            ResetView()
        End Sub

        Public Overridable Property ViewKind As CollectionViewKind

        Public Overridable Property Orientation As Orientation

        Public Overridable Property IsDataPaneVisible As Boolean

        Public Overridable Property IsFilterPaneVisible As Boolean

        Public Sub ResetView()
            ViewKind = defaultCollectionViewKind
            Orientation = Orientation.Horizontal
            IsDataPaneVisible = True
            IsFilterPaneVisible = True
        End Sub

        Public Function CanShowList() As Boolean
            Return Not Equals(ViewKind, CollectionViewKind.ListView)
        End Function

        Public Function CanShowCard() As Boolean
            Return Not Equals(ViewKind, CollectionViewKind.CardView)
        End Function

        Public Function CanShowMasterDetail() As Boolean
            Return Not Equals(ViewKind, CollectionViewKind.MasterDetailView)
        End Function

        Public Sub ShowList()
            ViewKind = CollectionViewKind.ListView
        End Sub

        Public Sub ShowCard()
            ViewKind = CollectionViewKind.CardView
        End Sub

        Public Sub ShowMasterDetail()
            ViewKind = CollectionViewKind.MasterDetailView
        End Sub

        Public Sub DataPaneRight()
            Orientation = Orientation.Horizontal
            IsDataPaneVisible = True
        End Sub

        Public Function CanDataPaneRight() As Boolean
            Return Orientation <> Orientation.Horizontal OrElse Not IsDataPaneVisible
        End Function

        Public Sub DataPaneLeft()
            Orientation = Orientation.Vertical
            IsDataPaneVisible = True
        End Sub

        Public Function CanDataPaneLeft() As Boolean
            Return Orientation <> Orientation.Vertical OrElse Not IsDataPaneVisible
        End Function

        Public Sub DataPaneOff()
            IsDataPaneVisible = False
        End Sub

        Public Function CanDataPaneOff() As Boolean
            Return IsDataPaneVisible
        End Function
    End Class
End Namespace
