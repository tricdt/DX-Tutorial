Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Docking
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows

Namespace DockingDemo

    Friend Interface IDockLayoutManagerEventsService

        Sub ClearEventsLog()

    End Interface

    Public Class DockLayoutManagerEventsService
        Inherits ServiceBase
        Implements IDockLayoutManagerEventsService

        Public Shared ReadOnly LogProperty As DependencyProperty = DependencyProperty.Register("Log", GetType(String), GetType(DockLayoutManagerEventsService))

        Public Property Log As String
            Get
                Return CStr(GetValue(LogProperty))
            End Get

            Set(ByVal value As String)
                SetValue(LogProperty, value)
            End Set
        End Property

        Private ReadOnly Property DockLayoutManager As DockLayoutManager
            Get
                Return TryCast(AssociatedObject, DockLayoutManager)
            End Get
        End Property

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler DockLayoutManager.DockOperationStarting, AddressOf Me.DockLayoutManager_DockOperationStarting
            AddHandler DockLayoutManager.DockOperationCompleted, AddressOf Me.DockLayoutManager_DockOperationCompleted
            AddHandler DockLayoutManager.DockItemActivating, AddressOf Me.DockLayoutManager_DockItemActivating
            AddHandler DockLayoutManager.DockItemActivated, AddressOf Me.DockLayoutManager_DockItemActivated
            AddHandler DockLayoutManager.DockItemExpanded, AddressOf Me.DockLayoutManager_DockItemExpanded
            AddHandler DockLayoutManager.DockItemCollapsed, AddressOf Me.DockLayoutManager_DockItemCollapsed
            AddHandler DockLayoutManager.DockItemStartDocking, AddressOf Me.DockLayoutManager_DockItemStartDocking
            AddHandler DockLayoutManager.DockItemDragging, AddressOf Me.DockLayoutManager_DockItemDragging
            AddHandler DockLayoutManager.DockItemEndDocking, AddressOf Me.DockLayoutManager_DockItemEndDocking
        End Sub

        Protected Overrides Sub OnDetaching()
            RemoveHandler DockLayoutManager.DockOperationStarting, AddressOf Me.DockLayoutManager_DockOperationStarting
            RemoveHandler DockLayoutManager.DockOperationCompleted, AddressOf Me.DockLayoutManager_DockOperationCompleted
            RemoveHandler DockLayoutManager.DockItemActivating, AddressOf Me.DockLayoutManager_DockItemActivating
            RemoveHandler DockLayoutManager.DockItemActivated, AddressOf Me.DockLayoutManager_DockItemActivated
            RemoveHandler DockLayoutManager.DockItemExpanded, AddressOf Me.DockLayoutManager_DockItemExpanded
            RemoveHandler DockLayoutManager.DockItemCollapsed, AddressOf Me.DockLayoutManager_DockItemCollapsed
            RemoveHandler DockLayoutManager.DockItemStartDocking, AddressOf Me.DockLayoutManager_DockItemStartDocking
            RemoveHandler DockLayoutManager.DockItemDragging, AddressOf Me.DockLayoutManager_DockItemDragging
            RemoveHandler DockLayoutManager.DockItemEndDocking, AddressOf Me.DockLayoutManager_DockItemEndDocking
            MyBase.OnDetaching()
        End Sub

        Private Sub DockLayoutManager_DockItemEndDocking(ByVal sender As Object, ByVal e As Base.DockItemDockingEventArgs)
            AddLogEntry("DockItemEndDocking: " & e.Item.CustomizationCaption)
        End Sub

        Private Sub DockLayoutManager_DockItemDragging(ByVal sender As Object, ByVal e As Base.DockItemDraggingEventArgs)
            AddLogEntry("DockItemDragging: " & e.Item.CustomizationCaption, True)
        End Sub

        Private Sub DockLayoutManager_DockItemStartDocking(ByVal sender As Object, ByVal e As Base.ItemCancelEventArgs)
            AddLogEntry("DockItemStartDocking: " & e.Item.CustomizationCaption)
        End Sub

        Private Sub DockLayoutManager_DockItemCollapsed(ByVal sender As Object, ByVal e As Base.DockItemCollapsedEventArgs)
            AddLogEntry("DockItemCollapsed: " & e.Item.CustomizationCaption)
        End Sub

        Private Sub DockLayoutManager_DockItemExpanded(ByVal sender As Object, ByVal e As Base.DockItemExpandedEventArgs)
            AddLogEntry("DockItemExpanded: " & e.Item.CustomizationCaption)
        End Sub

        Private Sub DockLayoutManager_DockItemActivated(ByVal sender As Object, ByVal e As Base.DockItemActivatedEventArgs)
            If e.Item Is Nothing Then Return
            AddLogEntry("DockItemActivated: " & e.Item.CustomizationCaption)
        End Sub

        Private Sub DockLayoutManager_DockItemActivating(ByVal sender As Object, ByVal e As Base.ItemCancelEventArgs)
            AddLogEntry("DockItemActivating: " & e.Item.CustomizationCaption)
        End Sub

        Private Sub DockLayoutManager_DockOperationCompleted(ByVal sender As Object, ByVal e As Base.DockOperationCompletedEventArgs)
            AddLogEntry("DockOperationCompleted: " & e.Item.CustomizationCaption & ", Operation: " & e.DockOperation)
        End Sub

        Private Sub DockLayoutManager_DockOperationStarting(ByVal sender As Object, ByVal e As Base.DockOperationStartingEventArgs)
            AddLogEntry("DockOperationStarting: " & e.Item.CustomizationCaption & ", Operation: " & e.DockOperation)
        End Sub

        Const logEntriesCount As Integer = 100

        Private logQueue As Queue(Of LogEntry) = New Queue(Of LogEntry)()

        Private lastEntry As LogEntry

        Private Sub AddLogEntry(ByVal str As String, ByVal Optional groupEvents As Boolean = False)
            Dim entry As LogEntry = New LogEntry(str)
            If entry Is lastEntry AndAlso groupEvents Then
                lastEntry.AddRecord()
            Else
                logQueue.Enqueue(entry)
                lastEntry = entry
            End If

            If logQueue.Count > logEntriesCount Then logQueue.Dequeue()
            Dim builder As StringBuilder = New StringBuilder()
            For Each e In logQueue
                builder.Append(If(builder.Length <> 0, Environment.NewLine, String.Empty))
                builder.Append(e)
            Next

            Log = builder.ToString()
        End Sub

        Public Sub ClearEventsLog() Implements IDockLayoutManagerEventsService.ClearEventsLog
            logQueue.Clear()
            Log = String.Empty
            lastEntry = Nothing
        End Sub

        Private Class LogEntry
            Implements IEquatable(Of LogEntry)

            Private text As String

            Private count As Integer = 1

            Public Sub New(ByVal text As String)
                Me.text = text
            End Sub

            Public Sub AddRecord()
                count += 1
            End Sub

            Public Overrides Function Equals(ByVal obj As Object) As Boolean
                If TypeOf obj Is LogEntry Then Return Equals(CType(obj, LogEntry))
                Return MyBase.Equals(obj)
            End Function

            Public Shared Operator =(ByVal first As LogEntry, ByVal second As LogEntry) As Boolean
                If CObj(first) Is Nothing Then Return CObj(second) Is Nothing
                Return first.Equals(second)
            End Operator

            Public Shared Operator <>(ByVal first As LogEntry, ByVal second As LogEntry) As Boolean
                Return Not first Is second
            End Operator

            Public Overloads Function Equals(ByVal other As LogEntry) As Boolean Implements IEquatable(Of LogEntry).Equals
                If ReferenceEquals(Nothing, other) Then Return False
                If ReferenceEquals(Me, other) Then Return True
                Return Equals(text, other.text)
            End Function

            Public Overrides Function GetHashCode() As Integer
                Dim hashCode As Integer = 47
                If Not Equals(text, Nothing) Then hashCode = hashCode * 53 Xor text.GetHashCode()
                Return hashCode
            End Function

            Public Overrides Function ToString() As String
                Return text & If(count > 1, " (x" & count & ")", String.Empty)
            End Function
        End Class
    End Class
End Namespace
