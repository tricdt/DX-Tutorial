Imports System
Imports System.Collections.Generic
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Gantt
Imports DevExpress.Mvvm.POCO

Namespace GanttDemo

    <POCOViewModel>
    Public Class StartupBusinessPlanViewModel

        Private _Items As List(Of DevExpress.Mvvm.Gantt.GanttTask), _StripLines As IEnumerable(Of GanttDemo.GanttStripLineBase), _Resources As List(Of DevExpress.Mvvm.Gantt.GanttResource)

        Public Property Items As List(Of GanttTask)
            Get
                Return _Items
            End Get

            Private Set(ByVal value As List(Of GanttTask))
                _Items = value
            End Set
        End Property

        Public Property StripLines As IEnumerable(Of GanttStripLineBase)
            Get
                Return _StripLines
            End Get

            Private Set(ByVal value As IEnumerable(Of GanttStripLineBase))
                _StripLines = value
            End Set
        End Property

        Public Property Resources As List(Of GanttResource)
            Get
                Return _Resources
            End Get

            Private Set(ByVal value As List(Of GanttResource))
                _Resources = value
            End Set
        End Property

        Private ReadOnly ProjectStart As Date = New DateTime(2018, 10, 25, 8, 0, 0)

        Private ReadOnly ProjectFinish As Date = New DateTime(2019, 4, 16, 12, 0, 0)

        Public Sub New()
            Items = CreateData()
            Resources = CreateResources()
            StripLines = CreateStripLinesData()
        End Sub

        Private Function CreateData() As List(Of GanttTask)
            Dim tasks = New List(Of GanttTask)()
#Region "Data generation"
            tasks.Add(New GanttTask With {.Id = 0, .Name = "New Business", .StartDate = New DateTime(2018, 10, 25, 8, 0, 0), .FinishDate = New DateTime(2019, 4, 16, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 10, 25, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 4, 16, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 1, .ParentId = 0, .Name = "Phase 1 - Strategic Plan", .StartDate = New DateTime(2018, 10, 25, 8, 0, 0), .FinishDate = New DateTime(2018, 11, 27, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 10, 25, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 26, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 2, .ParentId = 1, .Name = "Self-Assessment", .StartDate = New DateTime(2018, 10, 25, 8, 0, 0), .FinishDate = New DateTime(2018, 10, 30, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 10, 25, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 10, 29, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 3, .ParentId = 2, .Name = "Define business vision", .StartDate = New DateTime(2018, 10, 25, 8, 0, 0), .FinishDate = New DateTime(2018, 10, 26, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 10, 25, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 10, 25, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 4, .ParentId = 2, .Name = "Identify available skills, information and support", .StartDate = New DateTime(2018, 10, 26, 13, 0, 0), .FinishDate = New DateTime(2018, 10, 29, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 10, 26, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 10, 26, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 5, .ParentId = 2, .Name = "Decide whether to proceed", .StartDate = New DateTime(2018, 10, 29, 13, 0, 0), .FinishDate = New DateTime(2018, 10, 30, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 10, 29, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 10, 29, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 6, .ParentId = 1, .Name = "Define the Opportunity", .StartDate = New DateTime(2018, 10, 30, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 12, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 10, 30, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 12, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 7, .ParentId = 6, .Name = "Research the market and competition", .StartDate = New DateTime(2018, 10, 30, 13, 0, 0), .FinishDate = New DateTime(2018, 10, 31, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 10, 30, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 10, 30, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 8, .ParentId = 6, .Name = "Interview owners of similar businesses", .StartDate = New DateTime(2018, 10, 31, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 6, 17, 0, 0), .BaselineStartDate = New DateTime(2018, 10, 31, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 6, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 9, .ParentId = 6, .Name = "Identify needed resources", .StartDate = New DateTime(2018, 11, 7, 8, 0, 0), .FinishDate = New DateTime(2018, 11, 8, 17, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 7, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 8, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 10, .ParentId = 6, .Name = "Identify operating cost elements", .StartDate = New DateTime(2018, 11, 9, 8, 0, 0), .FinishDate = New DateTime(2018, 11, 12, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 9, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 12, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 11, .ParentId = 1, .Name = "Evaluate Business Approach", .StartDate = New DateTime(2018, 11, 12, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 16, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 13, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 16, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 12, .ParentId = 11, .Name = "Define new entity requirements", .StartDate = New DateTime(2018, 11, 12, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 13, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 13, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 13, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 13, .ParentId = 11, .Name = "Identify on-going business purchase opportunities", .StartDate = New DateTime(2018, 11, 13, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 14, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 14, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 14, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 14, .ParentId = 11, .Name = "Research franchise possibilities", .StartDate = New DateTime(2018, 11, 14, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 15, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 15, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 15, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 15, .ParentId = 11, .Name = "Summarize business approach", .StartDate = New DateTime(2018, 11, 15, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 16, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 16, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 16, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 16, .ParentId = 1, .Name = "Evaluate Potential Risks and Rewards", .StartDate = New DateTime(2018, 11, 13, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 22, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 14, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 22, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 17, .ParentId = 16, .Name = "Assess market size and stability", .StartDate = New DateTime(2018, 11, 13, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 15, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 14, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 15, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 18, .ParentId = 16, .Name = "Estimate the competition", .StartDate = New DateTime(2018, 11, 15, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 16, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 16, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 16, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 19, .ParentId = 16, .Name = "Assess needed resource availability", .StartDate = New DateTime(2018, 11, 16, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 20, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 19, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 20, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 20, .ParentId = 16, .Name = "Evaluate realistic initial market share", .StartDate = New DateTime(2018, 11, 20, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 21, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 21, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 21, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 21, .ParentId = 16, .Name = "Determine financial requirements", .StartDate = New DateTime(2018, 11, 16, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 20, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 19, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 20, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 22, .ParentId = 16, .Name = "Review personal suitability", .StartDate = New DateTime(2018, 11, 20, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 21, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 21, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 21, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 23, .ParentId = 16, .Name = "Evaluate initial profitability", .StartDate = New DateTime(2018, 11, 21, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 22, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 22, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 22, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 24, .ParentId = 1, .Name = "Review and modify the strategic plan", .StartDate = New DateTime(2018, 11, 22, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 27, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 23, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 26, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 25, .ParentId = 1, .Name = "Confirm decision to proceed", .StartDate = New DateTime(2018, 11, 27, 12, 0, 0), .FinishDate = New DateTime(2018, 11, 27, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 26, 17, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 26, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 26, .ParentId = 0, .Name = "Phase 2 - Define the Business Opportunity", .StartDate = New DateTime(2018, 11, 27, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 3, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 27, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 2, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 27, .ParentId = 26, .Name = "Define the Market", .StartDate = New DateTime(2018, 11, 27, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 14, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 27, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 12, 13, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 28, .ParentId = 27, .Name = "Access available information", .StartDate = New DateTime(2018, 11, 27, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 28, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 27, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 27, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 29, .ParentId = 27, .Name = "Create market analysis plan", .StartDate = New DateTime(2018, 11, 28, 13, 0, 0), .FinishDate = New DateTime(2018, 11, 30, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 28, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 11, 29, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 30, .ParentId = 27, .Name = "Implement market analysis plan", .StartDate = New DateTime(2018, 11, 30, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 7, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 11, 30, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 12, 6, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 31, .ParentId = 27, .Name = "Identify competition", .StartDate = New DateTime(2018, 12, 7, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 11, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 12, 7, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 12, 10, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 32, .ParentId = 27, .Name = "Summarize the market", .StartDate = New DateTime(2018, 12, 11, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 13, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 12, 11, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 12, 12, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 33, .ParentId = 27, .Name = "Identify target market niche", .StartDate = New DateTime(2018, 12, 13, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 14, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 12, 13, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 12, 13, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 34, .ParentId = 26, .Name = "Identify Needed Materials and Supplies", .StartDate = New DateTime(2018, 12, 14, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 25, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 12, 14, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 12, 24, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 35, .ParentId = 34, .Name = "Select a business approach (from ""Evaluate Business Approach"" above)", .StartDate = New DateTime(2018, 12, 14, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 18, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 12, 14, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 12, 17, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 36, .ParentId = 34, .Name = "Identify management staff resources", .StartDate = New DateTime(2018, 12, 18, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 19, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 12, 18, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 12, 18, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 37, .ParentId = 34, .Name = "Identify staffing requirements", .StartDate = New DateTime(2018, 12, 19, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 20, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 12, 19, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 12, 19, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 38, .ParentId = 34, .Name = "Identify needed raw materials", .StartDate = New DateTime(2018, 12, 20, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 21, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 12, 20, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 12, 20, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 39, .ParentId = 34, .Name = "Identify needed utilities", .StartDate = New DateTime(2018, 12, 21, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 24, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 12, 21, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 12, 21, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 40, .ParentId = 34, .Name = "Summarize operating expenses and financial projections", .StartDate = New DateTime(2018, 12, 24, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 25, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 12, 24, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 12, 24, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 41, .ParentId = 26, .Name = "Evaluate Potential Risks and Rewards", .StartDate = New DateTime(2018, 12, 25, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 2, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 12, 25, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 1, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 42, .ParentId = 41, .Name = "Assess market size and stability", .StartDate = New DateTime(2018, 12, 25, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 27, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 12, 25, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 12, 26, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 43, .ParentId = 41, .Name = "Assess needed resources availability", .StartDate = New DateTime(2018, 12, 27, 13, 0, 0), .FinishDate = New DateTime(2018, 12, 31, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 12, 27, 8, 0, 0), .BaselineFinishDate = New DateTime(2018, 12, 28, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 44, .ParentId = 41, .Name = "Forecast financial returns", .StartDate = New DateTime(2018, 12, 31, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 2, 12, 0, 0), .BaselineStartDate = New DateTime(2018, 12, 31, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 1, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 45, .ParentId = 26, .Name = "Review and modify the business opportunity", .StartDate = New DateTime(2019, 1, 2, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 3, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 2, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 2, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 46, .ParentId = 26, .Name = "Confirm decision to proceed", .StartDate = New DateTime(2019, 1, 3, 12, 0, 0), .FinishDate = New DateTime(2019, 1, 3, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 2, 17, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 2, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 47, .ParentId = 0, .Name = "Phase 3 - Plan for Action", .StartDate = New DateTime(2019, 1, 3, 13, 0, 0), .FinishDate = New DateTime(2019, 2, 4, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 3, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 31, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 48, .ParentId = 47, .Name = "Develop Detailed 5-Year Business Plan", .StartDate = New DateTime(2019, 1, 3, 13, 0, 0), .FinishDate = New DateTime(2019, 2, 4, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 3, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 31, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 49, .ParentId = 48, .Name = "Describe the vision and opportunity", .StartDate = New DateTime(2019, 1, 3, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 4, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 3, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 3, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 50, .ParentId = 48, .Name = "List assumptions", .StartDate = New DateTime(2019, 1, 4, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 7, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 4, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 4, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 51, .ParentId = 48, .Name = "Describe the market", .StartDate = New DateTime(2019, 1, 7, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 8, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 7, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 7, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 52, .ParentId = 48, .Name = "Describe the new business", .StartDate = New DateTime(2019, 1, 8, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 9, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 8, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 8, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 53, .ParentId = 48, .Name = "Describe strengths, weaknesses, assets and threats", .StartDate = New DateTime(2019, 1, 9, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 10, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 9, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 9, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 54, .ParentId = 48, .Name = "Estimate sales volume during startup period", .StartDate = New DateTime(2019, 1, 10, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 11, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 10, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 10, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 55, .ParentId = 48, .Name = "Forecast operating costs", .StartDate = New DateTime(2019, 1, 11, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 14, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 11, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 11, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 56, .ParentId = 48, .Name = "Establish pricing strategy", .StartDate = New DateTime(2019, 1, 14, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 15, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 14, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 14, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 57, .ParentId = 48, .Name = "Forecast revenue", .StartDate = New DateTime(2019, 1, 15, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 16, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 15, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 15, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 58, .ParentId = 48, .Name = "Summarize pro-forma financial statement", .StartDate = New DateTime(2019, 1, 16, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 18, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 16, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 17, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 59, .ParentId = 48, .Name = "Develop break-even analysis", .StartDate = New DateTime(2019, 1, 18, 13, 0, 0), .FinishDate = New DateTime(2019, 1, 18, 17, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 18, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 18, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 60, .ParentId = 48, .Name = "Develop cash-flow projection", .StartDate = New DateTime(2019, 1, 21, 8, 0, 0), .FinishDate = New DateTime(2019, 1, 21, 17, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 21, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 21, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 61, .ParentId = 48, .Name = "Identify licensing and permitting requirements", .StartDate = New DateTime(2019, 1, 22, 8, 0, 0), .FinishDate = New DateTime(2019, 1, 22, 17, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 22, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 22, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 62, .ParentId = 48, .Name = "Develop startup plan", .StartDate = New DateTime(2019, 1, 23, 8, 0, 0), .FinishDate = New DateTime(2019, 1, 24, 17, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 23, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 24, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 63, .ParentId = 48, .Name = "Develop sales and marketing strategy", .StartDate = New DateTime(2019, 1, 25, 8, 0, 0), .FinishDate = New DateTime(2019, 1, 25, 17, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 25, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 25, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 64, .ParentId = 48, .Name = "Develop distribution structure", .StartDate = New DateTime(2019, 1, 28, 8, 0, 0), .FinishDate = New DateTime(2019, 1, 28, 17, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 28, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 28, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 65, .ParentId = 48, .Name = "Describe risks and opportunities", .StartDate = New DateTime(2019, 1, 29, 8, 0, 0), .FinishDate = New DateTime(2019, 2, 1, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 29, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 30, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 66, .ParentId = 48, .Name = "Publish the business plan", .StartDate = New DateTime(2019, 2, 1, 13, 0, 0), .FinishDate = New DateTime(2019, 2, 4, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 31, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 31, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 67, .ParentId = 48, .Name = "Confirm decision to proceed", .StartDate = New DateTime(2019, 2, 4, 12, 0, 0), .FinishDate = New DateTime(2019, 2, 4, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 1, 31, 17, 0, 0), .BaselineFinishDate = New DateTime(2019, 1, 31, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 68, .ParentId = 0, .Name = "Phase 4 - Proceed With Startup Plan", .StartDate = New DateTime(2019, 2, 4, 13, 0, 0), .FinishDate = New DateTime(2019, 4, 16, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 1, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 4, 16, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 69, .ParentId = 68, .Name = "Choose a location", .StartDate = New DateTime(2019, 2, 4, 13, 0, 0), .FinishDate = New DateTime(2019, 2, 4, 17, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 1, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 2, 1, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 70, .ParentId = 68, .Name = "Establish Business Structure", .StartDate = New DateTime(2019, 2, 5, 8, 0, 0), .FinishDate = New DateTime(2019, 3, 7, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 4, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 3, 7, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 71, .ParentId = 70, .Name = "Choose a Name", .StartDate = New DateTime(2019, 2, 5, 8, 0, 0), .FinishDate = New DateTime(2019, 2, 6, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 4, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 2, 5, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 72, .ParentId = 71, .Name = "Identify implications", .StartDate = New DateTime(2019, 2, 5, 8, 0, 0), .FinishDate = New DateTime(2019, 2, 5, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 4, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 2, 4, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 73, .ParentId = 71, .Name = "Research name availability", .StartDate = New DateTime(2019, 2, 5, 13, 0, 0), .FinishDate = New DateTime(2019, 2, 6, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 5, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 2, 5, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 74, .ParentId = 70, .Name = "Choose a Bank", .StartDate = New DateTime(2019, 2, 6, 13, 0, 0), .FinishDate = New DateTime(2019, 2, 12, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 6, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 2, 12, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 75, .ParentId = 74, .Name = "Establish accounts", .StartDate = New DateTime(2019, 2, 6, 13, 0, 0), .FinishDate = New DateTime(2019, 2, 11, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 6, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 2, 11, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 76, .ParentId = 74, .Name = "Establish line of credit", .StartDate = New DateTime(2019, 2, 11, 13, 0, 0), .FinishDate = New DateTime(2019, 2, 12, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 12, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 2, 12, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 77, .ParentId = 70, .Name = "Choose legal representation", .StartDate = New DateTime(2019, 2, 12, 13, 0, 0), .FinishDate = New DateTime(2019, 2, 13, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 13, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 2, 13, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 78, .ParentId = 70, .Name = "Select business tax-basis category", .StartDate = New DateTime(2019, 2, 13, 13, 0, 0), .FinishDate = New DateTime(2019, 2, 15, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 14, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 2, 15, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 79, .ParentId = 70, .Name = "Choose capital funding source", .StartDate = New DateTime(2019, 2, 15, 13, 0, 0), .FinishDate = New DateTime(2019, 2, 19, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 18, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 2, 19, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 80, .ParentId = 70, .Name = "Commit capital funding", .StartDate = New DateTime(2019, 2, 19, 12, 0, 0), .FinishDate = New DateTime(2019, 2, 19, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 19, 17, 0, 0), .BaselineFinishDate = New DateTime(2019, 2, 19, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 81, .ParentId = 70, .Name = "Establish the Operating Control Base", .StartDate = New DateTime(2019, 2, 19, 13, 0, 0), .FinishDate = New DateTime(2019, 3, 7, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 20, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 3, 7, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 82, .ParentId = 81, .Name = "Choose and set up the accounting system", .StartDate = New DateTime(2019, 2, 19, 13, 0, 0), .FinishDate = New DateTime(2019, 2, 21, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 20, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 2, 21, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 83, .ParentId = 81, .Name = "Obtain required licenses and permits", .StartDate = New DateTime(2019, 2, 21, 13, 0, 0), .FinishDate = New DateTime(2019, 2, 27, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 22, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 2, 27, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 84, .ParentId = 81, .Name = "Obtain needed insurance", .StartDate = New DateTime(2019, 2, 27, 13, 0, 0), .FinishDate = New DateTime(2019, 3, 5, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 28, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 3, 5, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 85, .ParentId = 81, .Name = "Establish security plan", .StartDate = New DateTime(2019, 3, 5, 13, 0, 0), .FinishDate = New DateTime(2019, 3, 7, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 3, 6, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 3, 7, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 86, .ParentId = 70, .Name = "Develop Marketing Program", .StartDate = New DateTime(2019, 2, 5, 13, 0, 0), .FinishDate = New DateTime(2019, 2, 11, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 5, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 2, 8, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 87, .ParentId = 86, .Name = "Establish an advertising program", .StartDate = New DateTime(2019, 2, 5, 13, 0, 0), .FinishDate = New DateTime(2019, 2, 7, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 5, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 2, 6, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 88, .ParentId = 86, .Name = "Develop a logo", .StartDate = New DateTime(2019, 2, 7, 13, 0, 0), .FinishDate = New DateTime(2019, 2, 8, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 7, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 2, 7, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 89, .ParentId = 86, .Name = "Order promotional materials", .StartDate = New DateTime(2019, 2, 8, 13, 0, 0), .FinishDate = New DateTime(2019, 2, 11, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 8, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 2, 8, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 90, .ParentId = 68, .Name = "Provide Physical Facilities", .StartDate = New DateTime(2019, 3, 7, 13, 0, 0), .FinishDate = New DateTime(2019, 3, 28, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 3, 8, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 3, 28, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 91, .ParentId = 90, .Name = "Secure operation space", .StartDate = New DateTime(2019, 3, 7, 13, 0, 0), .FinishDate = New DateTime(2019, 3, 14, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 3, 8, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 3, 14, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 92, .ParentId = 90, .Name = "Select computer network hardware", .StartDate = New DateTime(2019, 3, 14, 13, 0, 0), .FinishDate = New DateTime(2019, 3, 15, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 3, 15, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 3, 15, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 93, .ParentId = 90, .Name = "Select computer software", .StartDate = New DateTime(2019, 3, 15, 13, 0, 0), .FinishDate = New DateTime(2019, 3, 18, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 3, 18, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 3, 18, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 94, .ParentId = 90, .Name = "Establish utilities", .StartDate = New DateTime(2019, 3, 18, 13, 0, 0), .FinishDate = New DateTime(2019, 3, 21, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 3, 19, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 3, 21, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 95, .ParentId = 90, .Name = "Provide furniture and equipment", .StartDate = New DateTime(2019, 3, 21, 13, 0, 0), .FinishDate = New DateTime(2019, 3, 27, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 3, 22, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 3, 27, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 96, .ParentId = 90, .Name = "Move in", .StartDate = New DateTime(2019, 3, 27, 13, 0, 0), .FinishDate = New DateTime(2019, 3, 28, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 3, 28, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 3, 28, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 97, .ParentId = 68, .Name = "Provide Staffing", .StartDate = New DateTime(2019, 2, 19, 13, 0, 0), .FinishDate = New DateTime(2019, 4, 16, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 20, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 4, 16, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 98, .ParentId = 97, .Name = "Interview and test candidates", .StartDate = New DateTime(2019, 2, 19, 13, 0, 0), .FinishDate = New DateTime(2019, 3, 11, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 2, 20, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 3, 11, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 99, .ParentId = 97, .Name = "Hire staff", .StartDate = New DateTime(2019, 3, 11, 13, 0, 0), .FinishDate = New DateTime(2019, 3, 25, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 3, 12, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 3, 25, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 100, .ParentId = 97, .Name = "Train staff", .StartDate = New DateTime(2019, 3, 25, 13, 0, 0), .FinishDate = New DateTime(2019, 4, 16, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 3, 26, 8, 0, 0), .BaselineFinishDate = New DateTime(2019, 4, 16, 17, 0, 0)})
            tasks.Add(New GanttTask With {.Id = 101, .ParentId = 68, .Name = "Start up the business", .StartDate = New DateTime(2019, 4, 16, 12, 0, 0), .FinishDate = New DateTime(2019, 4, 16, 12, 0, 0), .BaselineStartDate = New DateTime(2019, 4, 16, 17, 0, 0), .BaselineFinishDate = New DateTime(2019, 4, 16, 17, 0, 0)})
            tasks(4).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 3})
            tasks(5).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 4})
            tasks(7).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 5})
            tasks(8).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 7})
            tasks(9).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 8})
            tasks(10).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 9})
            tasks(12).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 10})
            tasks(13).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 12})
            tasks(14).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 13})
            tasks(15).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 14})
            tasks(17).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 12})
            tasks(18).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 17})
            tasks(19).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 18})
            tasks(20).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 19})
            tasks(21).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 15})
            tasks(22).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 21})
            tasks(23).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 22})
            tasks(24).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 23})
            tasks(25).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 24})
            tasks(28).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 25})
            tasks(29).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 28})
            tasks(30).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 29})
            tasks(31).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 30})
            tasks(32).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 31})
            tasks(33).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 32})
            tasks(35).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 28})
            tasks(35).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 33})
            tasks(36).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 35})
            tasks(37).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 36})
            tasks(38).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 37})
            tasks(39).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 38})
            tasks(40).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 39})
            tasks(42).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 40})
            tasks(43).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 42})
            tasks(44).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 43})
            tasks(45).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 44})
            tasks(46).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 45})
            tasks(49).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 46})
            tasks(50).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 49})
            tasks(51).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 50})
            tasks(52).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 51})
            tasks(53).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 52})
            tasks(54).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 53})
            tasks(55).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 54})
            tasks(56).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 55})
            tasks(57).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 56})
            tasks(58).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 57})
            tasks(59).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 58})
            tasks(60).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 59})
            tasks(61).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 60})
            tasks(62).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 61})
            tasks(63).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 62})
            tasks(64).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 63})
            tasks(65).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 64})
            tasks(66).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 65})
            tasks(67).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 66})
            tasks(69).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 67})
            tasks(72).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 69})
            tasks(73).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 72})
            tasks(75).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 73})
            tasks(76).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 75})
            tasks(77).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 75})
            tasks(77).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 76})
            tasks(78).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 77})
            tasks(79).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 78})
            tasks(80).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 79})
            tasks(82).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 79})
            tasks(82).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 80})
            tasks(83).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 82})
            tasks(84).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 83})
            tasks(85).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 84})
            tasks(87).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 73})
            tasks(88).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 87})
            tasks(89).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 88})
            tasks(91).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 85})
            tasks(91).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 89})
            tasks(92).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 91})
            tasks(93).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 92})
            tasks(94).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 93})
            tasks(95).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 94})
            tasks(96).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 95})
            tasks(98).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 79})
            tasks(99).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 98})
            tasks(100).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 99})
            tasks(101).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 96})
            tasks(101).PredecessorLinks.Add(New GanttPredecessorLink() With {.PredecessorTaskId = 100})
            tasks(3).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(4).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(4).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(5).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(7).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(8).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 6})
            tasks(9).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(9).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 2})
            tasks(10).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 7})
            tasks(12).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(13).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(14).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(15).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(17).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(18).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(19).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(20).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(21).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(22).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(23).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(28).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(29).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(30).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(31).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(32).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(33).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(35).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(36).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(37).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(38).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(39).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(40).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(42).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(43).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(44).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 7})
            tasks(46).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(46).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 2})
            tasks(46).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 3})
            tasks(46).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 7})
            tasks(49).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(50).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(51).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(52).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(53).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(54).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(54).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 7})
            tasks(55).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(55).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 7})
            tasks(56).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(57).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(58).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(59).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(60).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(61).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(62).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(63).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(64).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(65).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(66).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(67).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(72).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 3})
            tasks(73).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 3})
            tasks(75).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 8})
            tasks(76).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 8})
            tasks(77).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 3})
            tasks(78).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 3})
            tasks(78).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 7})
            tasks(79).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(80).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(82).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 7})
            tasks(83).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 3})
            tasks(83).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 4})
            tasks(84).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 7})
            tasks(85).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(85).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 3})
            tasks(87).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(88).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(88).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 3})
            tasks(89).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 1})
            tasks(91).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 3})
            tasks(92).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 9})
            tasks(93).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 9})
            tasks(94).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(95).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(96).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(98).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(99).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(100).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
            tasks(101).ResourceLinks.Add(New GanttResourceLink() With {.ResourceId = 5})
#End Region
            Return tasks
        End Function

        Private Function CreateResources() As List(Of GanttResource)
            Dim resources = New List(Of GanttResource)()
#Region "Data generation"
            resources.Add(New GanttResource With {.Name = "Business Advisor", .Id = 1})
            resources.Add(New GanttResource With {.Name = "Peers", .Id = 2})
            resources.Add(New GanttResource With {.Name = "Lawyer", .Id = 3})
            resources.Add(New GanttResource With {.Name = "Government Agency", .Id = 4})
            resources.Add(New GanttResource With {.Name = "Manager", .Id = 5})
            resources.Add(New GanttResource With {.Name = "Owners", .Id = 6})
            resources.Add(New GanttResource With {.Name = "Accountant", .Id = 7})
            resources.Add(New GanttResource With {.Name = "Banker", .Id = 8})
            resources.Add(New GanttResource With {.Name = "Information Services", .Id = 9})
#End Region
            Return resources
        End Function

        Private Function CreateStripLinesData() As IEnumerable(Of GanttStripLineBase)
            Return New GanttStripLineBase() {GanttStripLine.Create(ProjectStart, TimeSpan.Zero, "Project Start"), GanttStripLine.Create(ProjectFinish, TimeSpan.Zero, "Project Finish"), WeeklyStripLine.Create(DayOfWeek.Wednesday, ProjectStart, TimeSpan.FromHours(15), ProjectFinish, TimeSpan.FromMinutes(90), "Weekly meeting")}
        End Function
    End Class

    Public MustInherit Class GanttStripLineBase

        Public Overridable Property StartDate As Date

        Public Overridable Property Duration As TimeSpan

        Public Overridable Property Caption As String

        Protected Sub New(ByVal startDate As Date, ByVal duration As TimeSpan, ByVal caption As String)
            Me.StartDate = startDate
            Me.Duration = duration
            Me.Caption = caption
        End Sub
    End Class

    <POCOViewModel>
    Public Class GanttStripLine
        Inherits GanttStripLineBase

        Protected Sub New(ByVal startDate As Date, ByVal duration As TimeSpan, ByVal caption As String)
            MyBase.New(startDate, duration, caption)
        End Sub

        Public Shared Function Create(ByVal startDate As Date, ByVal duration As TimeSpan, ByVal caption As String) As GanttStripLine
            Return ViewModelSource.Create(Function() New GanttStripLine(startDate, duration, caption))
        End Function
    End Class

    <POCOViewModel>
    Public Class WeeklyStripLine
        Inherits GanttStripLineBase

        Public Overridable Property FinishDate As Date

        Public Overridable Property DayOfWeek As DayOfWeek

        Public Overridable Property StartTime As TimeSpan

        Protected Sub New(ByVal dayOfWeek As DayOfWeek, ByVal startDate As Date, ByVal startTime As TimeSpan, ByVal finishDate As Date, ByVal duration As TimeSpan, ByVal caption As String)
            MyBase.New(startDate, duration, caption)
            Me.DayOfWeek = dayOfWeek
            Me.FinishDate = finishDate
            Me.StartTime = startTime
        End Sub

        Public Shared Function Create(ByVal dayOfWeek As DayOfWeek, ByVal startDate As Date, ByVal startTime As TimeSpan, ByVal finishDate As Date, ByVal duration As TimeSpan, ByVal caption As String) As WeeklyStripLine
            Return ViewModelSource.Create(Function() New WeeklyStripLine(dayOfWeek, startDate, startTime, finishDate, duration, caption))
        End Function
    End Class
End Namespace
