Imports DevExpress.Xpf.Grid
Imports System
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Animation

Namespace GridDemo

    Public Enum FocusedGrid
        First
        Second
        None
    End Enum

    Public Class RowsAnimationElement
        Inherits System.Windows.FrameworkContentElement

        Public Shared ReadOnly NewRowsProgressProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly NewRowsColorProperty As System.Windows.DependencyProperty

        Shared Sub New()
            GridDemo.RowsAnimationElement.NewRowsProgressProperty = System.Windows.DependencyProperty.Register("NewRowsProgress", GetType(Double), GetType(GridDemo.RowsAnimationElement), New System.Windows.PropertyMetadata(1R))
            GridDemo.RowsAnimationElement.NewRowsColorProperty = System.Windows.DependencyProperty.Register("NewRowsColor", GetType(System.Windows.Media.Color), GetType(GridDemo.RowsAnimationElement), New System.Windows.PropertyMetadata(System.Windows.Media.Color.FromArgb(&H00, &HDE, &HF8, &HCB)))
        End Sub

        Public Property NewRowsProgress As Double
            Get
                Return CDbl(Me.GetValue(GridDemo.RowsAnimationElement.NewRowsProgressProperty))
            End Get

            Set(ByVal value As Double)
                Me.SetValue(GridDemo.RowsAnimationElement.NewRowsProgressProperty, value)
            End Set
        End Property

        Public Property NewRowsColor As Color
            Get
                Return CType(Me.GetValue(GridDemo.RowsAnimationElement.NewRowsColorProperty), System.Windows.Media.Color)
            End Get

            Set(ByVal value As Color)
                Me.SetValue(GridDemo.RowsAnimationElement.NewRowsColorProperty, value)
            End Set
        End Property
    End Class

    <System.SerializableAttribute>
    Public Class CopyPasteOutlookData

        Public Property UniqueID As Integer

        Public Property OID As Integer?

        Public Property From As String

        Public Property Sent As System.DateTime?

        Public Property HasAttachment As Boolean?

        Public Property HoursActive As Double?

        Shared Public Function ConvertOutlookDataToCopyPasteOutlookData(ByVal outlookDataObject As GridDemo.OutlookData, ByVal owner As GridDemo.CopyPasteOperations) As CopyPasteOutlookData
            Return New GridDemo.CopyPasteOutlookData() With {.UniqueID = System.Threading.Interlocked.Increment(owner.Counter), .From = outlookDataObject.From, .HasAttachment = outlookDataObject.HasAttachment, .HoursActive = outlookDataObject.HoursActive, .OID = outlookDataObject.OID, .Sent = outlookDataObject.Sent}
        End Function
    End Class

    Friend Class PasteCompetedHelper

        Public Property Owner As CopyPasteOperations

        Public Property ColorStoryboard As Storyboard

        Public Sub ColorStoryboardCompleted(ByVal sender As Object, ByVal e As System.EventArgs)
            RemoveHandler Me.ColorStoryboard.Completed, New System.EventHandler(AddressOf Me.ColorStoryboardCompleted)
            Me.Owner.RaisePasteCompetedEvent(New System.Windows.RoutedEventArgs(GridDemo.CopyPasteOperations.PasteCompetedEvent))
        End Sub
    End Class

    Friend Class PasteHelper

        Public Property View As GridViewBase

        Public Property List As BindingList(Of GridDemo.CopyPasteOutlookData)

        Public Property PositionNewRow As Integer

        Public Property ObjectsForCopy As Object()

        Public Property Owner As CopyPasteOperations

        Public Property ColorStoryboard As Storyboard

        Public Sub ColorStoryboardCompleted(ByVal sender As Object, ByVal e As System.EventArgs)
            RemoveHandler Me.ColorStoryboard.Completed, New System.EventHandler(AddressOf Me.ColorStoryboardCompleted)
            Dim posNewRow As Integer = Me.PositionNewRow
            Me.Owner.PasteRowsWithoutAnimation(posNewRow, Me.View, Me.List, Me.ObjectsForCopy, Me.Owner.MaxAnimationRows, Me.ObjectsForCopy.Length)
            Me.Owner.EndPasteForCanCommands()
        End Sub
    End Class
End Namespace
