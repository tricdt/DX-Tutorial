Imports System.Collections.Generic

Namespace GanttDemo.Examples

    Public Class PartsProductionViewModel

        Private _Parts As IEnumerable(Of GanttDemo.Examples.PartItem), _MachineTools As IEnumerable(Of GanttDemo.Examples.MachineToolItem)

        Public Sub New()
#Region "initialization"
            Dim startDate = Date.Now.Date.AddDays(3)
            Parts = New List(Of PartItem) From {New PartItem() With {.StartProductionDate = startDate, .FinishProductionDate = startDate.AddDays(10), .Name = "Part 1", .MachineTools = New List(Of MachineToolComponentLink)() From {New MachineToolComponentLink(1)}}, New PartItem() With {.StartProductionDate = startDate, .FinishProductionDate = startDate.AddDays(11), .Name = "Part 2", .MachineTools = New List(Of MachineToolComponentLink)() From {New MachineToolComponentLink(2)}}, New PartItem() With {.StartProductionDate = startDate, .FinishProductionDate = startDate.AddDays(4), .Name = "Part 3", .MachineTools = New List(Of MachineToolComponentLink)() From {New MachineToolComponentLink(3)}}, New PartItem() With {.StartProductionDate = startDate, .FinishProductionDate = startDate.AddDays(3), .Name = "Part 4", .MachineTools = New List(Of MachineToolComponentLink)() From {New MachineToolComponentLink(4)}}, New PartItem() With {.StartProductionDate = startDate, .FinishProductionDate = startDate.AddDays(21), .Name = "Part 5", .MachineTools = New List(Of MachineToolComponentLink)() From {New MachineToolComponentLink(5)}}, New PartItem() With {.StartProductionDate = startDate, .FinishProductionDate = startDate.AddDays(12), .Name = "Part 6", .MachineTools = New List(Of MachineToolComponentLink)() From {New MachineToolComponentLink(6)}}, New PartItem() With {.StartProductionDate = startDate, .FinishProductionDate = startDate.AddDays(10), .Name = "Part 7", .MachineTools = New List(Of MachineToolComponentLink)() From {New MachineToolComponentLink(7)}}, New PartItem() With {.StartProductionDate = startDate, .FinishProductionDate = startDate.AddDays(12), .Name = "Part 8", .MachineTools = New List(Of MachineToolComponentLink)() From {New MachineToolComponentLink(8)}}, New PartItem() With {.StartProductionDate = startDate, .FinishProductionDate = startDate.AddDays(20), .Name = "Part 9", .MachineTools = New List(Of MachineToolComponentLink)() From {New MachineToolComponentLink(9)}}, New PartItem() With {.StartProductionDate = startDate, .FinishProductionDate = startDate.AddDays(4), .Name = "Part 10", .MachineTools = New List(Of MachineToolComponentLink)() From {New MachineToolComponentLink(10)}}}
            MachineTools = New List(Of MachineToolItem) From {New MachineToolItem With {.Name = "Machine tool 1", .Id = 1, .WorkshopName = "Workshop 1"}, New MachineToolItem With {.Name = "Machine tool 2", .Id = 2, .WorkshopName = "Workshop 1"}, New MachineToolItem With {.Name = "Machine tool 3", .Id = 3, .WorkshopName = "Workshop 1"}, New MachineToolItem With {.Name = "Machine tool 1", .Id = 4, .WorkshopName = "Workshop 2"}, New MachineToolItem With {.Name = "Machine tool 2", .Id = 5, .WorkshopName = "Workshop 2"}, New MachineToolItem With {.Name = "Machine tool 3", .Id = 6, .WorkshopName = "Workshop 2"}, New MachineToolItem With {.Name = "Machine tool 4", .Id = 7, .WorkshopName = "Workshop 2"}, New MachineToolItem With {.Name = "Machine tool 1", .Id = 8, .WorkshopName = "Workshop 3"}, New MachineToolItem With {.Name = "Machine tool 2", .Id = 9, .WorkshopName = "Workshop 3"}, New MachineToolItem With {.Name = "Machine tool 3", .Id = 10, .WorkshopName = "Workshop 3"}}
#End Region
        End Sub

        Public Property Parts As IEnumerable(Of PartItem)
            Get
                Return _Parts
            End Get

            Private Set(ByVal value As IEnumerable(Of PartItem))
                _Parts = value
            End Set
        End Property

        Public Property MachineTools As IEnumerable(Of MachineToolItem)
            Get
                Return _MachineTools
            End Get

            Private Set(ByVal value As IEnumerable(Of MachineToolItem))
                _MachineTools = value
            End Set
        End Property
    End Class

    Public Class MachineToolItem

        Public Property Name As String

        Public Property WorkshopName As String

        Public Property Id As Integer

        Public ReadOnly Property DisplayName As String
            Get
                Return WorkshopName & " / " & Name
            End Get
        End Property
    End Class

    Public Class MachineToolComponentLink

        Public Sub New()
            Me.New(0)
        End Sub

        Public Sub New(ByVal id As Integer)
            MachineToolId = id
        End Sub

        Public Property MachineToolId As Integer
    End Class

    Public Class PartItem

        Public Property Name As String

        Public Property StartProductionDate As Date

        Public Property FinishProductionDate As Date

        Public Property MachineTools As List(Of MachineToolComponentLink)
    End Class
End Namespace
