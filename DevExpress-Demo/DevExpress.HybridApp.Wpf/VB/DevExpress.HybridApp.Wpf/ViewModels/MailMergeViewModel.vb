Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataModel
Imports DevExpress.Xpf.Core

Namespace DevExpress.DevAV.ViewModels

    Public Class MailMergeViewModel(Of TEntity As Class, TLinks As Class)
        Implements IDocumentContent

        Private _DocumentOwnerProp As IDocumentOwner

        Public Shared Function Create(Of TUnitOfWork As IUnitOfWork, TPrimaryKey As Structure)(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TEntity, TPrimaryKey)), ByVal key As TPrimaryKey?, ByVal Optional selectedTemplateName As String = Nothing, ByVal Optional linksViewModel As TLinks = Nothing) As MailMergeViewModel(Of TEntity, TLinks)
            Dim repository = getRepositoryFunc(unitOfWorkFactory.CreateUnitOfWork())
            Dim entities = repository.ToArray()
            Dim selectedEntity = If(key IsNot Nothing, repository.Find(key.Value), Nothing)
            Return ViewModelSource.Create(Function() New MailMergeViewModel(Of TEntity, TLinks)(entities, selectedEntity, selectedTemplateName, linksViewModel))
        End Function

        Protected Sub New(ByVal entities As IEnumerable(Of TEntity), ByVal selectedEntity As TEntity, ByVal selectedTemplateName As String, ByVal linksViewModel As TLinks)
            Templates = GetAllTemplates()
            SelectedTemplate = Templates.FirstOrDefault(Function(t) Equals(t.Name, selectedTemplateName))
            IsAdditionParametersVisible = SelectedTemplate Is Nothing
            SelectedTemplate = If(SelectedTemplate, Templates.FirstOrDefault())
            Me.LinksViewModel = linksViewModel
            Me.Entities = entities
            Me.SelectedEntity = selectedEntity
            Log(String.Format("HybridApp: View Quick Letter: {0}", selectedTemplateName))
        End Sub

        Private locker As Locker = New Locker()

        Public Overridable Property SelectedEntity As TEntity

        Public Overridable Property ActiveRecord As Integer

        Public Overridable Property Entities As IEnumerable(Of TEntity)

        Public Overridable Property Templates As List(Of TemplateViewModel)

        Public Overridable Property SelectedTemplate As TemplateViewModel

        Public Overridable Property IsAdditionParametersVisible As Boolean

        Public Overridable Property LinksViewModel As TLinks

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

        Public Sub OnSelectedEntityChanged()
            locker.DoLockedActionIfNotLocked(Sub()
                If Entities IsNot Nothing Then
                    Dim index = Entities.Cast(Of Object)().[Select](Function(x, i) New With {.item = x, .index = i}).FirstOrDefault(Function(x) x.item Is SelectedEntity)
                    ActiveRecord = If(index IsNot Nothing, index.index, -1)
                Else
                    ActiveRecord = -1
                End If
            End Sub)
        End Sub

        Public Sub OnActiveRecordChanged()
            locker.DoLockedActionIfNotLocked(Sub()
                If Entities IsNot Nothing Then
                    Dim obj = Entities.Cast(Of Object)().[Select](Function(x, i) New With {.item = x, .index = i}).FirstOrDefault(Function(x) x.index = ActiveRecord)
                    SelectedEntity = If(obj IsNot Nothing, TryCast(obj.item, TEntity), Nothing)
                Else
                    SelectedEntity = Nothing
                End If
            End Sub)
        End Sub

        Public Sub OnSelectedTemplateChanged()
            Dim activeRecord As Integer = Me.ActiveRecord
            Me.ActiveRecord = -1
            Me.ActiveRecord = activeRecord
        End Sub

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
                Return "DevAV - Mail Merge"
            End Get
        End Property
#End Region
    End Class
End Namespace
