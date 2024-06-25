Imports System
Imports System.Collections.Generic

Namespace LayoutControlDemo

    Public Class PageDashboardViewModel

        Public ReadOnly Property Agents As List(Of Agent)
            Get
                Return LayoutControlDemo.Agents.DataSource
            End Get
        End Property

        Public ReadOnly Property Listings As IList(Of Listing)
            Get
                Return LayoutControlDemo.Listings.DataSource
            End Get
        End Property
    End Class

    Public Class Agent

        Private _PhotoUri As Uri

        Public Sub New(ByVal uri As String)
            PhotoUri = New Uri(LayoutControlDemoModule.UriPrefix & uri, UriKind.Relative)
        End Sub

        Public Property FirstName As String

        Public Property LastName As String

        Public Property Phone As String

        Public Property PhotoUri As Uri
            Get
                Return _PhotoUri
            End Get

            Private Set(ByVal value As Uri)
                _PhotoUri = value
            End Set
        End Property
    End Class

    Public Module Agents

        Public ReadOnly DataSource As List(Of Agent) = New List(Of Agent) From {New Agent("/Images/Agents/1.jpg") With {.FirstName = "Anthony", .LastName = "Peterson", .Phone = "(555) 444-0782"}, New Agent("/Images/Agents/2.jpg") With {.FirstName = "Cindy", .LastName = "Haneline", .Phone = "(555) 638-9820"}, New Agent("/Images/Agents/3.jpg") With {.FirstName = "Albert", .LastName = "Walker", .Phone = "(555) 232-2303"}, New Agent("/Images/Agents/4.jpg") With {.FirstName = "Rachel", .LastName = "Scott", .Phone = "(555) 249-1614"}, New Agent("/Images/Agents/5.jpg") With {.FirstName = "Vernon", .LastName = "Rounds", .Phone = "(555) 682-5403"}, New Agent("/Images/Agents/6.jpg") With {.FirstName = "Andrew", .LastName = "Carter", .Phone = "(555) 578-3946"}}
    End Module
End Namespace
