Imports System.ComponentModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Printing

Namespace DevExpress.DevAV.ViewModels

    Public Class PrintableControlPreviewViewModel
        Implements IDocumentContent

        Private _DocumentOwnerProp As IDocumentOwner

        Public Shared Function Create(ByVal link As PrintableControlLink) As PrintableControlPreviewViewModel
            Return ViewModelSource.Create(Function() New PrintableControlPreviewViewModel(link))
        End Function

        Protected Sub New(ByVal link As PrintableControlLink)
            Me.Link = link
        End Sub

        Public Overridable Property Link As PrintableControlLink

        Public Sub Close()
            If DocumentOwnerProp IsNot Nothing Then DocumentOwnerProp.Close(Me)
        End Sub

        Protected Property DocumentOwnerProp As IDocumentOwner
            Get
                Return _DocumentOwnerProp
            End Get

            Private Set(ByVal value As IDocumentOwner)
                _DocumentOwnerProp = value
            End Set
        End Property

#Region "IDocumentContent"
        Private Sub OnClose(ByVal e As CancelEventArgs) Implements IDocumentContent.OnClose
        End Sub

        Private Sub OnDestroy() Implements IDocumentContent.OnDestroy
        End Sub

        Private Property DocumentOwner As IDocumentOwner Implements IDocumentContent.DocumentOwner
            Get
                Return DocumentOwnerProp
            End Get

            Set(ByVal value As IDocumentOwner)
                DocumentOwnerProp = value
            End Set
        End Property

        Private ReadOnly Property Title As Object Implements IDocumentContent.Title
            Get
                Return Nothing
            End Get
        End Property
#End Region
    End Class
End Namespace
