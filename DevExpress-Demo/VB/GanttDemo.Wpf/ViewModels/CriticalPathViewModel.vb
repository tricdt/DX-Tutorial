Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Gantt
Imports DevExpress.Xpf.Gantt
Imports System
Imports System.Collections.Generic

Namespace GanttDemo

    <POCOViewModel>
    Public Class CriticalPathViewModel
        Inherits BindableBase

        Private _SingleSource As List(Of DevExpress.Mvvm.Gantt.GanttTask), _MultipleSource As List(Of DevExpress.Mvvm.Gantt.GanttTask)

        Public Property SingleSource As List(Of GanttTask)
            Get
                Return _SingleSource
            End Get

            Private Set(ByVal value As List(Of GanttTask))
                _SingleSource = value
            End Set
        End Property

        Public Property MultipleSource As List(Of GanttTask)
            Get
                Return _MultipleSource
            End Get

            Private Set(ByVal value As List(Of GanttTask))
                _MultipleSource = value
            End Set
        End Property

        Public Overridable Property FirstVisibleDate As Date

        Private criticalPathHighlightModeField As CriticalPathHighlightMode = CriticalPathHighlightMode.Single

        Public Overridable Property CriticalPathHighlightMode As CriticalPathHighlightMode
            Get
                Return criticalPathHighlightModeField
            End Get

            Set(ByVal value As CriticalPathHighlightMode)
                If criticalPathHighlightModeField.Equals(value) Then Return
                Dim firstVisibleDate = Me.FirstVisibleDate
                criticalPathHighlightModeField = value
                RaisePropertyChanged(Function() CriticalPathHighlightMode)
                Me.FirstVisibleDate = firstVisibleDate
            End Set
        End Property

        Public Sub New()
            SingleSource = CreateSingleSource()
            MultipleSource = CreateMultipleSource()
            FirstVisibleDate = New DateTime(2018, 12, 3)
        End Sub

        Private Function CreateSingleSource() As List(Of GanttTask)
            Dim tasks = CreateTasksSource()
            tasks(3).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 2})
            tasks(4).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 3})
            tasks(5).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 4})
            tasks(6).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 5})
            tasks(8).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 6})
            tasks(9).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 8})
            tasks(10).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 9})
            tasks(11).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 10})
            tasks(12).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 11})
            tasks(13).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 12})
            tasks(14).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 13})
            tasks(15).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 14})
            tasks(16).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 15})
            tasks(18).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 16})
            tasks(19).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 18})
            tasks(20).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 19})
            tasks(21).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 20})
            tasks(22).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 21})
            tasks(23).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 22})
            tasks(24).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 23})
            tasks(26).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 24})
            tasks(27).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 26})
            tasks(28).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 27})
            tasks(29).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 28})
            tasks(30).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 29})
            tasks(31).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 30})
            tasks(33).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 24})
            tasks(34).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 24})
            tasks(36).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 33})
            tasks(36).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 31})
            tasks(37).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 31})
            tasks(37).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 36})
            tasks(38).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 37})
            tasks(39).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 38})
            tasks(40).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 39})
            tasks(41).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 40})
            tasks(43).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 41})
            tasks(44).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 43})
            tasks(45).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 44})
            tasks(46).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 45})
            tasks(47).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 46})
            tasks(49).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 24})
            tasks(50).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 24})
            tasks(51).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 24})
            tasks(52).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 49})
            tasks(52).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 31})
            tasks(52).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 50})
            tasks(52).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 51})
            tasks(53).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 52})
            tasks(54).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 53})
            tasks(55).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 54})
            tasks(56).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 55})
            tasks(58).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 24})
            tasks(59).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 58})
            tasks(59).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 29})
            tasks(60).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 59})
            tasks(61).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 60})
            tasks(62).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 24})
            tasks(63).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 62})
            tasks(63).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 29})
            tasks(64).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 63})
            tasks(65).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 64})
            tasks(66).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 65})
            tasks(66).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 61})
            tasks(68).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 16})
            tasks(69).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 68})
            tasks(70).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 47})
            tasks(70).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 69})
            tasks(70).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 66})
            tasks(70).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 56})
            tasks(71).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 70})
            tasks(72).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 71})
            tasks(73).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 72})
            tasks(75).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 73})
            tasks(76).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 75})
            tasks(77).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 76})
            tasks(78).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 77})
            tasks(79).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 78})
            tasks(80).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 79})
            tasks(82).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 80})
            tasks(83).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 82})
            tasks(84).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 83})
            tasks(85).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 84})
            tasks(86).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 85})
            Return tasks
        End Function

        Private Function CreateMultipleSource() As List(Of GanttTask)
            Dim tasks = CreateTasksSource()
            tasks(3).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 2})
            tasks(4).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 3})
            tasks(5).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 4})
            tasks(6).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 5})
            tasks(9).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 8})
            tasks(10).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 9})
            tasks(11).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 10})
            tasks(12).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 11})
            tasks(13).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 12})
            tasks(14).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 13})
            tasks(15).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 14})
            tasks(16).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 15})
            tasks(19).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 18})
            tasks(20).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 19})
            tasks(21).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 20})
            tasks(22).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 21})
            tasks(23).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 22})
            tasks(24).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 23})
            tasks(27).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 26})
            tasks(28).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 27})
            tasks(29).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 28})
            tasks(30).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 29})
            tasks(31).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 30})
            tasks(36).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 33})
            tasks(36).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 34})
            tasks(37).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 36})
            tasks(38).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 37})
            tasks(39).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 38})
            tasks(40).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 39})
            tasks(41).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 40})
            tasks(43).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 41})
            tasks(44).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 43})
            tasks(45).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 44})
            tasks(46).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 45})
            tasks(47).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 46})
            tasks(52).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 49})
            tasks(52).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 50})
            tasks(52).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 51})
            tasks(53).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 52})
            tasks(54).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 53})
            tasks(55).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 54})
            tasks(56).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 55})
            tasks(59).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 58})
            tasks(60).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 59})
            tasks(61).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 60})
            tasks(63).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 62})
            tasks(64).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 63})
            tasks(65).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 64})
            tasks(66).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 65})
            tasks(66).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 61})
            tasks(69).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 68})
            tasks(70).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 69})
            tasks(71).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 70})
            tasks(72).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 71})
            tasks(73).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 72})
            tasks(76).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 75})
            tasks(77).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 76})
            tasks(78).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 77})
            tasks(79).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 78})
            tasks(80).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 79})
            tasks(83).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 82})
            tasks(84).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 83})
            tasks(85).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 84})
            Return tasks
        End Function

        Private Function CreateTasksSource() As List(Of GanttTask)
            Return New List(Of GanttTask)() From {New GanttTask With {.Id = 0, .Name = "Software Development", .StartDate = New DateTime(2018, 10, 25, 8, 0, 0), .FinishDate = New DateTime(2019, 3, 7, 15, 0, 0)}, New GanttTask With {.Id = 1, .ParentId = 0, .Name = "Scope", .StartDate = New DateTime(2018, 10, 25, 8, 0, 0), .FinishDate = New DateTime(2018, 10, 30, 12, 0, 0)}, New GanttTask With {.Id = 2, .ParentId = 1, .Name = "Determine project scope", .StartDate = New DateTime(2018, 10, 25, 8, 0, 0), .FinishDate = New DateTime(2018, 10, 25, 12, 0, 0)}, New GanttTask With {.Id = 3, .ParentId = 1, .Name = "Secure project sponsorship", .StartDate = New DateTime(2018, 10, 25, 13, 0, 0), .FinishDate = New DateTime(2018, 10, 26, 12, 0, 0)}, New GanttTask With {.Id = 4, .ParentId = 1, .Name = "Define preliminary resources", .StartDate = New DateTime(2018, 10, 26, 13, 0, 0), .FinishDate = New DateTime(2018, 10, 29, 12, 0, 0)}, New GanttTask With {.Id = 5, .ParentId = 1, .Name = "Secure core resources", .StartDate = New DateTime(2018, 10, 29, 13, 0, 0), .FinishDate = New DateTime(2018, 10, 30, 12, 0, 0)}, New GanttTask With {.Id = 6, .ParentId = 1, .Name = "Scope complete", .StartDate = New DateTime(2018, 10, 30, 12, 0, 0), .FinishDate = New DateTime(2018, 10, 30, 12, 0, 0)}, New GanttTask With {.Id = 7, .ParentId = 0, .Name = "Analysis/Software Requirements", .StartDate = New DateTime(2018, 10, 30, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 19, 12, 0, 0)}, New GanttTask With {.Id = 8, .ParentId = 7, .Name = "Conduct needs analysis", .StartDate = New DateTime(2018, 10, 30, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 6, 12, 0, 0)}, New GanttTask With {.Id = 9, .ParentId = 7, .Name = "Draft preliminary software specifications", .StartDate = New DateTime(2018, 11, 6, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 9, 12, 0, 0)}, New GanttTask With {.Id = 10, .ParentId = 7, .Name = "Develop preliminary budget", .StartDate = New DateTime(2018, 11, 9, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 13, 12, 0, 0)}, New GanttTask With {.Id = 11, .ParentId = 7, .Name = "Review software specifications/budget with team", .StartDate = New DateTime(2018, 11, 13, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 13, 17, 0, 0)}, New GanttTask With {.Id = 12, .ParentId = 7, .Name = "Incorporate feedback on software specifications", .StartDate = New DateTime(2018, 11, 14, 8, 0, 0), .FinishDate = New DateTime(2018, 11, 14, 17, 0, 0)}, New GanttTask With {.Id = 13, .ParentId = 7, .Name = "Develop delivery timeline", .StartDate = New DateTime(2018, 11, 15, 8, 0, 0), .FinishDate = New DateTime(2018, 11, 15, 17, 0, 0)}, New GanttTask With {.Id = 14, .ParentId = 7, .Name = "Obtain approvals to proceed (concept, timeline, budget)", .StartDate = New DateTime(2018, 11, 16, 8, 0, 0), .FinishDate = New DateTime(2018, 11, 16, 12, 0, 0)}, New GanttTask With {.Id = 15, .ParentId = 7, .Name = "Secure required resources", .StartDate = New DateTime(2018, 11, 16, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 19, 12, 0, 0)}, New GanttTask With {.Id = 16, .ParentId = 7, .Name = "Analysis complete", .StartDate = New DateTime(2018, 11, 19, 12, 0, 0), .FinishDate = New DateTime(2018, 11, 19, 12, 0, 0)}, New GanttTask With {.Id = 17, .ParentId = 0, .Name = "Design", .StartDate = New DateTime(2018, 11, 19, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 7, 17, 0, 0)}, New GanttTask With {.Id = 18, .ParentId = 17, .Name = "Review preliminary software specifications", .StartDate = New DateTime(2018, 11, 19, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 21, 12, 0, 0)}, New GanttTask With {.Id = 19, .ParentId = 17, .Name = "Develop functional specifications", .StartDate = New DateTime(2018, 11, 21, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 28, 12, 0, 0)}, New GanttTask With {.Id = 20, .ParentId = 17, .Name = "Develop prototype based on functional specifications", .StartDate = New DateTime(2018, 11, 28, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 4, 12, 0, 0)}, New GanttTask With {.Id = 21, .ParentId = 17, .Name = "Review functional specifications", .StartDate = New DateTime(2018, 12, 4, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 6, 12, 0, 0)}, New GanttTask With {.Id = 22, .ParentId = 17, .Name = "Incorporate feedback into functional specifications", .StartDate = New DateTime(2018, 12, 6, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 7, 12, 0, 0)}, New GanttTask With {.Id = 23, .ParentId = 17, .Name = "Obtain approval to proceed", .StartDate = New DateTime(2018, 12, 7, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 7, 17, 0, 0)}, New GanttTask With {.Id = 24, .ParentId = 17, .Name = "Design complete", .StartDate = New DateTime(2018, 12, 7, 17, 0, 0), .FinishDate = New DateTime(2018, 12, 7, 17, 0, 0)}, New GanttTask With {.Id = 25, .ParentId = 0, .Name = "Development", .StartDate = New DateTime(2018, 12, 10, 8, 0, 0), .FinishDate = New DateTime(2019, 1, 8, 15, 0, 0)}, New GanttTask With {.Id = 26, .ParentId = 25, .Name = "Review functional specifications", .StartDate = New DateTime(2018, 12, 10, 8, 0, 0), .FinishDate = New DateTime(2018, 12, 10, 17, 0, 0)}, New GanttTask With {.Id = 27, .ParentId = 25, .Name = "Identify modular/tiered design parameters", .StartDate = New DateTime(2018, 12, 11, 8, 0, 0), .FinishDate = New DateTime(2018, 12, 11, 17, 0, 0)}, New GanttTask With {.Id = 28, .ParentId = 25, .Name = "Assign development staff", .StartDate = New DateTime(2018, 12, 12, 8, 0, 0), .FinishDate = New DateTime(2018, 12, 12, 17, 0, 0)}, New GanttTask With {.Id = 29, .ParentId = 25, .Name = "Develop code", .StartDate = New DateTime(2018, 12, 13, 8, 0, 0), .FinishDate = New DateTime(2019, 1, 2, 17, 0, 0)}, New GanttTask With {.Id = 30, .ParentId = 25, .Name = "Developer testing (primary debugging)", .StartDate = New DateTime(2018, 12, 18, 15, 0, 0), .FinishDate = New DateTime(2019, 1, 8, 15, 0, 0)}, New GanttTask With {.Id = 31, .ParentId = 25, .Name = "Development complete", .StartDate = New DateTime(2019, 1, 8, 15, 0, 0), .FinishDate = New DateTime(2019, 1, 8, 15, 0, 0)}, New GanttTask With {.Id = 32, .ParentId = 0, .Name = "Testing", .StartDate = New DateTime(2018, 12, 10, 8, 0, 0), .FinishDate = New DateTime(2019, 2, 14, 15, 0, 0)}, New GanttTask With {.Id = 33, .ParentId = 32, .Name = "Develop unit test plans using product specifications", .StartDate = New DateTime(2018, 12, 10, 8, 0, 0), .FinishDate = New DateTime(2018, 12, 13, 17, 0, 0)}, New GanttTask With {.Id = 34, .ParentId = 32, .Name = "Develop integration test plans using product specifications", .StartDate = New DateTime(2018, 12, 10, 8, 0, 0), .FinishDate = New DateTime(2018, 12, 13, 17, 0, 0)}, New GanttTask With {.Id = 35, .ParentId = 32, .Name = "Unit Testing", .StartDate = New DateTime(2019, 1, 8, 15, 0, 0), .FinishDate = New DateTime(2019, 1, 29, 15, 0, 0)}, New GanttTask With {.Id = 36, .ParentId = 35, .Name = "Review modular code", .StartDate = New DateTime(2019, 1, 8, 15, 0, 0), .FinishDate = New DateTime(2019, 1, 15, 15, 0, 0)}, New GanttTask With {.Id = 37, .ParentId = 35, .Name = "Test component modules to product specifications", .StartDate = New DateTime(2019, 1, 15, 15, 0, 0), .FinishDate = New DateTime(2019, 1, 17, 15, 0, 0)}, New GanttTask With {.Id = 38, .ParentId = 35, .Name = "Identify anomalies to product specifications", .StartDate = New DateTime(2019, 1, 17, 15, 0, 0), .FinishDate = New DateTime(2019, 1, 22, 15, 0, 0)}, New GanttTask With {.Id = 39, .ParentId = 35, .Name = "Modify code", .StartDate = New DateTime(2019, 1, 22, 15, 0, 0), .FinishDate = New DateTime(2019, 1, 25, 15, 0, 0)}, New GanttTask With {.Id = 40, .ParentId = 35, .Name = "Re-test modified code", .StartDate = New DateTime(2019, 1, 25, 15, 0, 0), .FinishDate = New DateTime(2019, 1, 29, 15, 0, 0)}, New GanttTask With {.Id = 41, .ParentId = 35, .Name = "Unit testing complete", .StartDate = New DateTime(2019, 1, 29, 15, 0, 0), .FinishDate = New DateTime(2019, 1, 29, 15, 0, 0)}, New GanttTask With {.Id = 42, .ParentId = 32, .Name = "Integration Testing", .StartDate = New DateTime(2019, 1, 29, 15, 0, 0), .FinishDate = New DateTime(2019, 2, 14, 15, 0, 0)}, New GanttTask With {.Id = 43, .ParentId = 42, .Name = "Test module integration", .StartDate = New DateTime(2019, 1, 29, 15, 0, 0), .FinishDate = New DateTime(2019, 2, 5, 15, 0, 0)}, New GanttTask With {.Id = 44, .ParentId = 42, .Name = "Identify anomalies to specifications", .StartDate = New DateTime(2019, 2, 5, 15, 0, 0), .FinishDate = New DateTime(2019, 2, 7, 15, 0, 0)}, New GanttTask With {.Id = 45, .ParentId = 42, .Name = "Modify code", .StartDate = New DateTime(2019, 2, 7, 15, 0, 0), .FinishDate = New DateTime(2019, 2, 12, 15, 0, 0)}, New GanttTask With {.Id = 46, .ParentId = 42, .Name = "Re-test modified code", .StartDate = New DateTime(2019, 2, 12, 15, 0, 0), .FinishDate = New DateTime(2019, 2, 14, 15, 0, 0)}, New GanttTask With {.Id = 47, .ParentId = 42, .Name = "Integration testing complete", .StartDate = New DateTime(2019, 2, 14, 15, 0, 0), .FinishDate = New DateTime(2019, 2, 14, 15, 0, 0)}, New GanttTask With {.Id = 48, .ParentId = 0, .Name = "Training", .StartDate = New DateTime(2018, 12, 10, 8, 0, 0), .FinishDate = New DateTime(2019, 2, 11, 15, 0, 0)}, New GanttTask With {.Id = 49, .ParentId = 48, .Name = "Develop training specifications for end users", .StartDate = New DateTime(2018, 12, 10, 8, 0, 0), .FinishDate = New DateTime(2018, 12, 12, 17, 0, 0)}, New GanttTask With {.Id = 50, .ParentId = 48, .Name = "Develop training specifications for helpdesk support staff", .StartDate = New DateTime(2018, 12, 10, 8, 0, 0), .FinishDate = New DateTime(2018, 12, 12, 17, 0, 0)}, New GanttTask With {.Id = 51, .ParentId = 48, .Name = "Identify training delivery methodology (computer based training, classroom, etc.)", .StartDate = New DateTime(2018, 12, 10, 8, 0, 0), .FinishDate = New DateTime(2018, 12, 11, 17, 0, 0)}, New GanttTask With {.Id = 52, .ParentId = 48, .Name = "Develop training materials", .StartDate = New DateTime(2019, 1, 8, 15, 0, 0), .FinishDate = New DateTime(2019, 1, 29, 15, 0, 0)}, New GanttTask With {.Id = 53, .ParentId = 48, .Name = "Conduct training usability study", .StartDate = New DateTime(2019, 1, 29, 15, 0, 0), .FinishDate = New DateTime(2019, 2, 4, 15, 0, 0)}, New GanttTask With {.Id = 54, .ParentId = 48, .Name = "Finalize training materials", .StartDate = New DateTime(2019, 2, 4, 15, 0, 0), .FinishDate = New DateTime(2019, 2, 7, 15, 0, 0)}, New GanttTask With {.Id = 55, .ParentId = 48, .Name = "Develop training delivery mechanism", .StartDate = New DateTime(2019, 2, 7, 15, 0, 0), .FinishDate = New DateTime(2019, 2, 11, 15, 0, 0)}, New GanttTask With {.Id = 56, .ParentId = 48, .Name = "Training materials complete", .StartDate = New DateTime(2019, 2, 11, 15, 0, 0), .FinishDate = New DateTime(2019, 2, 11, 15, 0, 0)}, New GanttTask With {.Id = 57, .ParentId = 0, .Name = "Documentation", .StartDate = New DateTime(2018, 12, 10, 8, 0, 0), .FinishDate = New DateTime(2019, 1, 21, 12, 0, 0)}, New GanttTask With {.Id = 58, .ParentId = 57, .Name = "Develop Help specification", .StartDate = New DateTime(2018, 12, 10, 8, 0, 0), .FinishDate = New DateTime(2018, 12, 10, 17, 0, 0)}, New GanttTask With {.Id = 59, .ParentId = 57, .Name = "Develop Help system", .StartDate = New DateTime(2018, 12, 24, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 14, 12, 0, 0)}, New GanttTask With {.Id = 60, .ParentId = 57, .Name = "Review Help documentation", .StartDate = New DateTime(2019, 1, 14, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 17, 12, 0, 0)}, New GanttTask With {.Id = 61, .ParentId = 57, .Name = "Incorporate Help documentation feedback", .StartDate = New DateTime(2019, 1, 17, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 21, 12, 0, 0)}, New GanttTask With {.Id = 62, .ParentId = 57, .Name = "Develop user manuals specifications", .StartDate = New DateTime(2018, 12, 10, 8, 0, 0), .FinishDate = New DateTime(2018, 12, 11, 17, 0, 0)}, New GanttTask With {.Id = 63, .ParentId = 57, .Name = "Develop user manuals", .StartDate = New DateTime(2018, 12, 24, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 14, 12, 0, 0)}, New GanttTask With {.Id = 64, .ParentId = 57, .Name = "Review all user documentation", .StartDate = New DateTime(2019, 1, 14, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 16, 12, 0, 0)}, New GanttTask With {.Id = 65, .ParentId = 57, .Name = "Incorporate user documentation feedback", .StartDate = New DateTime(2019, 1, 16, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 18, 12, 0, 0)}, New GanttTask With {.Id = 66, .ParentId = 57, .Name = "Documentation complete", .StartDate = New DateTime(2019, 1, 21, 12, 0, 0), .FinishDate = New DateTime(2019, 1, 21, 12, 0, 0)}, New GanttTask With {.Id = 67, .ParentId = 0, .Name = "Pilot", .StartDate = New DateTime(2018, 11, 19, 13, 0, 0), .FinishDate = New DateTime(2019, 2, 25, 15, 0, 0)}, New GanttTask With {.Id = 68, .ParentId = 67, .Name = "Identify test group", .StartDate = New DateTime(2018, 11, 19, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 20, 12, 0, 0)}, New GanttTask With {.Id = 69, .ParentId = 67, .Name = "Develop software delivery mechanism", .StartDate = New DateTime(2018, 11, 20, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 21, 12, 0, 0)}, New GanttTask With {.Id = 70, .ParentId = 67, .Name = "Install/deploy software", .StartDate = New DateTime(2019, 2, 14, 15, 0, 0), .FinishDate = New DateTime(2019, 2, 15, 15, 0, 0)}, New GanttTask With {.Id = 71, .ParentId = 67, .Name = "Obtain user feedback", .StartDate = New DateTime(2019, 2, 15, 15, 0, 0), .FinishDate = New DateTime(2019, 2, 22, 15, 0, 0)}, New GanttTask With {.Id = 72, .ParentId = 67, .Name = "Evaluate testing information", .StartDate = New DateTime(2019, 2, 22, 15, 0, 0), .FinishDate = New DateTime(2019, 2, 25, 15, 0, 0)}, New GanttTask With {.Id = 73, .ParentId = 67, .Name = "Pilot complete", .StartDate = New DateTime(2019, 2, 25, 15, 0, 0), .FinishDate = New DateTime(2019, 2, 25, 15, 0, 0)}, New GanttTask With {.Id = 74, .ParentId = 0, .Name = "Deployment", .StartDate = New DateTime(2019, 2, 25, 15, 0, 0), .FinishDate = New DateTime(2019, 3, 4, 15, 0, 0)}, New GanttTask With {.Id = 75, .ParentId = 74, .Name = "Determine final deployment strategy", .StartDate = New DateTime(2019, 2, 25, 15, 0, 0), .FinishDate = New DateTime(2019, 2, 26, 15, 0, 0)}, New GanttTask With {.Id = 76, .ParentId = 74, .Name = "Develop deployment methodology", .StartDate = New DateTime(2019, 2, 26, 15, 0, 0), .FinishDate = New DateTime(2019, 2, 27, 15, 0, 0)}, New GanttTask With {.Id = 77, .ParentId = 74, .Name = "Secure deployment resources", .StartDate = New DateTime(2019, 2, 27, 15, 0, 0), .FinishDate = New DateTime(2019, 2, 28, 15, 0, 0)}, New GanttTask With {.Id = 78, .ParentId = 74, .Name = "Train support staff", .StartDate = New DateTime(2019, 2, 28, 15, 0, 0), .FinishDate = New DateTime(2019, 3, 1, 15, 0, 0)}, New GanttTask With {.Id = 79, .ParentId = 74, .Name = "Deploy software", .StartDate = New DateTime(2019, 3, 1, 15, 0, 0), .FinishDate = New DateTime(2019, 3, 4, 15, 0, 0)}, New GanttTask With {.Id = 80, .ParentId = 74, .Name = "Deployment complete", .StartDate = New DateTime(2019, 3, 4, 15, 0, 0), .FinishDate = New DateTime(2019, 3, 4, 15, 0, 0)}, New GanttTask With {.Id = 81, .ParentId = 0, .Name = "Post Implementation Review", .StartDate = New DateTime(2019, 3, 4, 15, 0, 0), .FinishDate = New DateTime(2019, 3, 7, 15, 0, 0)}, New GanttTask With {.Id = 82, .ParentId = 81, .Name = "Document lessons learned", .StartDate = New DateTime(2019, 3, 4, 15, 0, 0), .FinishDate = New DateTime(2019, 3, 5, 15, 0, 0)}, New GanttTask With {.Id = 83, .ParentId = 81, .Name = "Distribute to team members", .StartDate = New DateTime(2019, 3, 5, 15, 0, 0), .FinishDate = New DateTime(2019, 3, 6, 15, 0, 0)}, New GanttTask With {.Id = 84, .ParentId = 81, .Name = "Create software maintenance team", .StartDate = New DateTime(2019, 3, 6, 15, 0, 0), .FinishDate = New DateTime(2019, 3, 7, 15, 0, 0)}, New GanttTask With {.Id = 85, .ParentId = 81, .Name = "Post implementation review complete", .StartDate = New DateTime(2019, 3, 7, 15, 0, 0), .FinishDate = New DateTime(2019, 3, 7, 15, 0, 0)}, New GanttTask With {.Id = 86, .ParentId = 0, .Name = "Software development template complete", .StartDate = New DateTime(2019, 3, 7, 15, 0, 0), .FinishDate = New DateTime(2019, 3, 7, 15, 0, 0)}}
        End Function
    End Class
End Namespace
