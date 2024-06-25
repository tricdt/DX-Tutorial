Imports System.Windows
Imports System.Windows.Media
Imports NavigationDemo.Utils

Namespace NavigationDemo

    Public Class FilterViewModel

        Private Shared filterPreviewImageSize As Size = New Size(200, 136)

        Public Sub New(ByVal uri As String, ByVal filter As FilterBase)
            Me.Filter = filter
            Me.Uri = uri
        End Sub

        Public Overridable Property Uri As String

        Public Overridable Property Image As ImageSource

        Public Overridable Property Filter As FilterBase

        Public Overridable ReadOnly Property Name As String
            Get
                Return If(Filter IsNot Nothing, Filter.Name, String.Empty)
            End Get
        End Property

        Public Overridable Sub OnUriChanged()
            Image = Filter.ApplyFilter(Uri, filterPreviewImageSize)
        End Sub
    End Class
End Namespace
