Imports System.Collections.Generic
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels

    Public Class CitiesMapViewModel
        Inherits MapViewModelBase

        Private linksViewModelField As LinksViewModel

        Public Shared Function Create() As CitiesMapViewModel
            Return ViewModelSource.Create(Function() New CitiesMapViewModel())
        End Function

        Protected Sub New()
            Cities = New List(Of CityViewModel)()
        End Sub

        Public Overridable Property Cities As List(Of CityViewModel)

        Public Overridable Property Addresses As HashSet(Of Address)

        Public ReadOnly Property LinksViewModel As LinksViewModel
            Get
                If linksViewModelField Is Nothing Then linksViewModelField = LinksViewModel.Create()
                Return linksViewModelField
            End Get
        End Property
    End Class
End Namespace
