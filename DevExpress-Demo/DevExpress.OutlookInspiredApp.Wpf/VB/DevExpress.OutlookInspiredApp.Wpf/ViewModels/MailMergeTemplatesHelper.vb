Imports System.Collections.Generic
Imports System.IO
Imports DevExpress.Utils

Namespace DevExpress.DevAV.ViewModels

    Public Module MailMergeTemplatesHelper

        Private templateNames As String() = New String() {"Employee of the Month.rtf", "Employee Probation Notice.rtf", "Employee Service Excellence.rtf", "Employee Thank You Note.rtf", "Welcome to DevAV.rtf"}

        Public Function GetAllTemplates() As List(Of TemplateViewModel)
            Dim templates As List(Of TemplateViewModel) = New List(Of TemplateViewModel)()
            For Each name In templateNames
                Dim stream As Stream = GetTemplateStream(name)
                templates.Add(New TemplateViewModel() With {.Name = name.Replace(".rtf", "")})
            Next

            Return templates
        End Function

        Public Function GetTemplateStream(ByVal templateName As String) As Stream
            Return AssemblyHelper.GetEmbeddedResourceStream(GetType(MailMergeTemplatesHelper).Assembly, templateName, False)
        End Function
    End Module

    Public Class TemplateViewModel

        Public Property Name As String

        Public ReadOnly Property Template As Stream
            Get
                Return GetTemplateStream(Name & ".rtf")
            End Get
        End Property
    End Class
End Namespace
