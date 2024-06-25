Imports System.Collections.Generic
Imports System.Linq

Namespace LayoutControlDemo

    Public Class RealEstateViewModel

        Public Sub New()
            Source = Listings.DataSource
            SelectedListing = Source.First()
        End Sub

        Private Property Source As IList(Of Listing)

        Public Overridable Property SelectedListing As Listing

        Public Sub NextListing()
            Dim index = SelectedIndex()
            SelectedListing = Source(System.Threading.Interlocked.Increment(index))
        End Sub

        Public Function CanNextListing() As Boolean
            Return SelectedListing IsNot Source.Last()
        End Function

        Public Sub PreviousListing()
            Dim index = SelectedIndex()
            SelectedListing = Source(System.Threading.Interlocked.Decrement(index))
        End Sub

        Public Function CanPreviousListing() As Boolean
            Return SelectedListing IsNot Source.First()
        End Function

        Private Function SelectedIndex() As Integer
            Return Source.IndexOf(SelectedListing)
        End Function
    End Class
End Namespace
