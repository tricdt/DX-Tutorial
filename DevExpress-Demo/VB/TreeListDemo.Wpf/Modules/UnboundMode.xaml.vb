Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Grid.TreeList

Namespace TreeListDemo

    Public Partial Class UnboundMode
        Inherits TreeListDemo.TreeListDemoModule

        Const StateFieldName As String = "State"

        Const StartDateFieldName As String = "StartDate"

        Const EndDateFieldName As String = "EndDate"

        Private Shared ReadOnly Random As System.Random = New System.Random(System.DateTime.Now.Second)

        Public Shared Function GetRandomEmployee() As Employee
            If DevExpress.Xpf.DemoBase.DataClasses.EmployeesData.DataSource Is Nothing Then Return Nothing
            Return DevExpress.Xpf.DemoBase.DataClasses.EmployeesData.DataSource(TreeListDemo.UnboundMode.Random.[Next](DevExpress.Xpf.DemoBase.DataClasses.EmployeesData.DataSource.Count))
        End Function

        Public Sub New()
            Me.InitializeComponent()
            Me.InitData()
            Me.view.ExpandAllNodes()
        End Sub

        Private Sub InitData()
            Dim stantoneProject As DevExpress.Xpf.Grid.TreeListNode = Me.view.Nodes(0)
            Me.InitStantoneProjectData(stantoneProject)
            Dim betaronProject As DevExpress.Xpf.Grid.TreeListNode = Me.view.Nodes(1)
            Me.InitBetaronProjectData(betaronProject)
        End Sub

        Private Sub CellValueChanging(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.TreeList.TreeListCellValueChangedEventArgs)
            If Equals(e.Column.FieldName, TreeListDemo.UnboundMode.StartDateFieldName) OrElse Equals(e.Column.FieldName, TreeListDemo.UnboundMode.EndDateFieldName) OrElse Equals(e.Column.FieldName, TreeListDemo.UnboundMode.StateFieldName) Then Me.view.CommitEditing(True)
        End Sub

        Private Sub GetColumnData(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.TreeList.TreeListUnboundColumnDataEventArgs)
            If e.IsSetData Then
                Me.SetUnboundCellData(sender, e)
            Else
                Me.GetUnboundCellData(sender, e)
            End If
        End Sub

        Private Sub GetUnboundCellData(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.TreeList.TreeListUnboundColumnDataEventArgs)
            Select Case e.Column.FieldName
                Case TreeListDemo.UnboundMode.StartDateFieldName
                    e.Value = Me.GetUnboundStartDate(e, e.Node)
                Case TreeListDemo.UnboundMode.EndDateFieldName
                    e.Value = Me.GetUnboundEndDate(e, e.Node)
                Case TreeListDemo.UnboundMode.StateFieldName
                    Me.GetUnboundState(e, e.Node)
                Case Else
            End Select
        End Sub

        Private Sub SetUnboundCellData(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.TreeList.TreeListUnboundColumnDataEventArgs)
            Dim task As TreeListDemo.TaskObject = TryCast(e.Node.Content, TreeListDemo.TaskObject)
            Dim FieldName As String = e.Column.FieldName
            If task IsNot Nothing Then
                Select Case FieldName
                    Case TreeListDemo.UnboundMode.StartDateFieldName
                        task.StartDate = CDate((If(e.Value, System.DateTime.MinValue)))
                    Case TreeListDemo.UnboundMode.EndDateFieldName
                        task.EndDate = CDate((If(e.Value, System.DateTime.MinValue)))
                    Case TreeListDemo.UnboundMode.StateFieldName
                        Dim newState As TreeListDemo.State = CType(e.Value, TreeListDemo.State)
                        If task.State IsNot newState Then
                            task.State = newState
                            Me.RecursiveNodeRefresh(e.Node.ParentNode)
                        End If

                    Case Else
                End Select
            End If
        End Sub

        Private Sub RecursiveNodeRefresh(ByVal node As DevExpress.Xpf.Grid.TreeListNode)
            If node IsNot Nothing Then
                Me.treeList.RefreshRow(node.RowHandle)
                Me.RecursiveNodeRefresh(node.ParentNode)
            End If
        End Sub

        Private Sub EditorVisibility(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.TreeList.TreeListShowingEditorEventArgs)
            Dim fieldName As System.[String] = e.Column.FieldName
            e.Cancel =(Equals(fieldName, TreeListDemo.UnboundMode.StartDateFieldName) OrElse Equals(fieldName, TreeListDemo.UnboundMode.EndDateFieldName) OrElse Equals(fieldName, TreeListDemo.UnboundMode.StateFieldName)) AndAlso Not(TypeOf e.Node.Content Is TreeListDemo.TaskObject)
        End Sub

        Private Sub CollectBoundStates(ByVal treeListNode As DevExpress.Xpf.Grid.TreeListNode, ByVal states As System.Collections.Generic.List(Of TreeListDemo.State))
            Dim iterator As DevExpress.Xpf.Grid.TreeListNodeIterator = New DevExpress.Xpf.Grid.TreeListNodeIterator(treeListNode)
            For Each node As DevExpress.Xpf.Grid.TreeListNode In iterator
                Dim task As TreeListDemo.TaskObject = TryCast(node.Content, TreeListDemo.TaskObject)
                If task IsNot Nothing Then states.Add(task.State)
            Next
        End Sub

        Private Sub GetUnboundState(ByVal e As DevExpress.Xpf.Grid.TreeList.TreeListUnboundColumnDataEventArgs, ByVal treeListNode As DevExpress.Xpf.Grid.TreeListNode)
            Dim task As TreeListDemo.TaskObject = TryCast(treeListNode.Content, TreeListDemo.TaskObject)
            If task IsNot Nothing Then
                e.Value = task.State
                Return
            End If

            Dim statesList As System.Collections.Generic.List(Of TreeListDemo.State) = New System.Collections.Generic.List(Of TreeListDemo.State)()
            Me.CollectBoundStates(e.Node, statesList)
            If statesList.Contains(TreeListDemo.States.DataSource(1)) OrElse (statesList.Contains(TreeListDemo.States.DataSource(0)) AndAlso statesList.Contains(TreeListDemo.States.DataSource(2))) Then
                e.Value = TreeListDemo.States.DataSource(1)
            ElseIf statesList.Contains(TreeListDemo.States.DataSource(0)) Then
                e.Value = TreeListDemo.States.DataSource(0)
            ElseIf statesList.Contains(TreeListDemo.States.DataSource(2)) Then
                e.Value = TreeListDemo.States.DataSource(2)
            End If
        End Sub

        Private Function GetUnboundStartDate(ByVal e As DevExpress.Xpf.Grid.TreeList.TreeListUnboundColumnDataEventArgs, ByVal treeListNode As DevExpress.Xpf.Grid.TreeListNode) As DateTime
            Dim task As TreeListDemo.TaskObject = TryCast(treeListNode.Content, TreeListDemo.TaskObject)
            Dim value As System.DateTime = System.DateTime.Now
            Dim tempValue As System.DateTime
            If task IsNot Nothing Then
                value = task.StartDate
            Else
                value = System.DateTime.MaxValue
                For Each item As DevExpress.Xpf.Grid.TreeListNode In treeListNode.Nodes
                    tempValue = Me.GetUnboundStartDate(e, item)
                    If tempValue < value Then value = tempValue
                Next
            End If

            Return value
        End Function

        Private Function GetUnboundEndDate(ByVal e As DevExpress.Xpf.Grid.TreeList.TreeListUnboundColumnDataEventArgs, ByVal treeListNode As DevExpress.Xpf.Grid.TreeListNode) As DateTime
            Dim task As TreeListDemo.TaskObject = TryCast(treeListNode.Content, TreeListDemo.TaskObject)
            Dim value As System.DateTime = System.DateTime.Now
            Dim tempValue As System.DateTime
            If task IsNot Nothing Then
                value = task.EndDate
            Else
                value = System.DateTime.MinValue
                For Each item As DevExpress.Xpf.Grid.TreeListNode In treeListNode.Nodes
                    tempValue = Me.GetUnboundEndDate(e, item)
                    If tempValue > value Then value = tempValue
                Next
            End If

            Return value
        End Function

#Region "Unbound Data Initialization"
        Private Sub InitBetaronProjectData(ByVal betaronProject As DevExpress.Xpf.Grid.TreeListNode)
            betaronProject.Image = TreeListDemo.ProjectObject.Image
            Dim stage21 As DevExpress.Xpf.Grid.TreeListNode = New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.StageObject() With {.NameValue = "Information Gathering", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee()})
            stage21.Image = TreeListDemo.StageObject.Image
            stage21.Nodes.Add(New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.TaskObject() With {.NameValue = "Market research", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee(), .StartDate = New System.DateTime(2011, 10, 1), .EndDate = New System.DateTime(2011, 10, 5), .State = TreeListDemo.States.DataSource(2)}) With {.Image = TreeListDemo.TaskObject.Image})
            stage21.Nodes.Add(New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.TaskObject() With {.NameValue = "Making specification", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee(), .StartDate = New System.DateTime(2011, 10, 5), .EndDate = New System.DateTime(2011, 10, 10), .State = TreeListDemo.States.DataSource(1)}) With {.Image = TreeListDemo.TaskObject.Image})
            Dim stage22 As DevExpress.Xpf.Grid.TreeListNode = New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.StageObject() With {.NameValue = "Planning", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee()})
            stage22.Image = TreeListDemo.StageObject.Image
            stage22.Nodes.Add(New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.TaskObject() With {.NameValue = "Documentation", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee(), .StartDate = New System.DateTime(2011, 10, 15), .EndDate = New System.DateTime(2011, 10, 16), .State = TreeListDemo.States.DataSource(0)}) With {.Image = TreeListDemo.TaskObject.Image})
            Dim stage23 As DevExpress.Xpf.Grid.TreeListNode = New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.StageObject() With {.NameValue = "Design", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee()})
            stage23.Image = TreeListDemo.StageObject.Image
            stage23.Nodes.Add(New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.TaskObject() With {.NameValue = "Design of a web pages", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee(), .StartDate = New System.DateTime(2011, 10, 13), .EndDate = New System.DateTime(2011, 10, 14), .State = TreeListDemo.States.DataSource(0)}) With {.Image = TreeListDemo.TaskObject.Image})
            stage23.Nodes.Add(New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.TaskObject() With {.NameValue = "Pages layout", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee(), .StartDate = New System.DateTime(2011, 10, 13), .EndDate = New System.DateTime(2011, 10, 14), .State = TreeListDemo.States.DataSource(0)}) With {.Image = TreeListDemo.TaskObject.Image})
            Dim stage24 As DevExpress.Xpf.Grid.TreeListNode = New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.StageObject() With {.NameValue = "Development", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee()})
            stage24.Image = TreeListDemo.StageObject.Image
            stage24.Nodes.Add(New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.TaskObject() With {.NameValue = "Design", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee(), .StartDate = New System.DateTime(2011, 10, 27), .EndDate = New System.DateTime(2011, 10, 28), .State = TreeListDemo.States.DataSource(0)}) With {.Image = TreeListDemo.TaskObject.Image})
            stage24.Nodes.Add(New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.TaskObject() With {.NameValue = "Coding", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee(), .StartDate = New System.DateTime(2011, 10, 29), .EndDate = New System.DateTime(2011, 10, 30), .State = TreeListDemo.States.DataSource(0)}) With {.Image = TreeListDemo.TaskObject.Image})
            Dim stage25 As DevExpress.Xpf.Grid.TreeListNode = New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.StageObject() With {.NameValue = "Testing and Delivery", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee()})
            stage25.Image = TreeListDemo.StageObject.Image
            stage25.Nodes.Add(New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.TaskObject() With {.NameValue = "Testing", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee(), .StartDate = New System.DateTime(2011, 10, 13), .EndDate = New System.DateTime(2011, 10, 14), .State = TreeListDemo.States.DataSource(0)}) With {.Image = TreeListDemo.TaskObject.Image})
            stage25.Nodes.Add(New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.TaskObject() With {.NameValue = "Content", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee(), .StartDate = New System.DateTime(2011, 10, 13), .EndDate = New System.DateTime(2011, 10, 14), .State = TreeListDemo.States.DataSource(0)}) With {.Image = TreeListDemo.TaskObject.Image})
            betaronProject.Nodes.Add(stage21)
            betaronProject.Nodes.Add(stage22)
            betaronProject.Nodes.Add(stage23)
            betaronProject.Nodes.Add(stage24)
            betaronProject.Nodes.Add(stage25)
        End Sub

        Private Sub InitStantoneProjectData(ByVal stantoneProject As DevExpress.Xpf.Grid.TreeListNode)
            stantoneProject.Image = TreeListDemo.ProjectObject.Image
            Dim stage11 As DevExpress.Xpf.Grid.TreeListNode = New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.StageObject() With {.NameValue = "Information Gathering", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee()})
            stage11.Image = TreeListDemo.StageObject.Image
            stage11.Nodes.Add(New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.TaskObject() With {.NameValue = "Market research", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee(), .StartDate = New System.DateTime(2011, 10, 1), .EndDate = New System.DateTime(2011, 10, 5), .State = TreeListDemo.States.DataSource(2)}) With {.Image = TreeListDemo.TaskObject.Image})
            stage11.Nodes.Add(New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.TaskObject() With {.NameValue = "Making specification", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee(), .StartDate = New System.DateTime(2011, 10, 5), .EndDate = New System.DateTime(2011, 10, 10), .State = TreeListDemo.States.DataSource(2)}) With {.Image = TreeListDemo.TaskObject.Image})
            Dim stage12 As DevExpress.Xpf.Grid.TreeListNode = New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.StageObject() With {.NameValue = "Planning", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee()})
            stage12.Image = TreeListDemo.StageObject.Image
            stage12.Nodes.Add(New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.TaskObject() With {.NameValue = "Documentation", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee(), .StartDate = New System.DateTime(2011, 10, 13), .EndDate = New System.DateTime(2011, 10, 14), .State = TreeListDemo.States.DataSource(2)}) With {.Image = TreeListDemo.TaskObject.Image})
            Dim stage13 As DevExpress.Xpf.Grid.TreeListNode = New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.StageObject() With {.NameValue = "Design", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee()})
            stage13.Image = TreeListDemo.StageObject.Image
            stage13.Nodes.Add(New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.TaskObject() With {.NameValue = "Design of a web pages", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee(), .StartDate = New System.DateTime(2011, 10, 13), .EndDate = New System.DateTime(2011, 10, 14), .State = TreeListDemo.States.DataSource(1)}) With {.Image = TreeListDemo.TaskObject.Image})
            stage13.Nodes.Add(New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.TaskObject() With {.NameValue = "Pages layout", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee(), .StartDate = New System.DateTime(2011, 10, 13), .EndDate = New System.DateTime(2011, 10, 14), .State = TreeListDemo.States.DataSource(1)}) With {.Image = TreeListDemo.TaskObject.Image})
            Dim stage14 As DevExpress.Xpf.Grid.TreeListNode = New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.StageObject() With {.NameValue = "Development", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee()})
            stage14.Image = TreeListDemo.StageObject.Image
            stage14.Nodes.Add(New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.TaskObject() With {.NameValue = "Design", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee(), .StartDate = New System.DateTime(2011, 10, 23), .EndDate = New System.DateTime(2011, 10, 24), .State = TreeListDemo.States.DataSource(1)}) With {.Image = TreeListDemo.TaskObject.Image})
            stage14.Nodes.Add(New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.TaskObject() With {.NameValue = "Coding", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee(), .StartDate = New System.DateTime(2011, 10, 25), .EndDate = New System.DateTime(2011, 10, 26), .State = TreeListDemo.States.DataSource(0)}) With {.Image = TreeListDemo.TaskObject.Image})
            Dim stage15 As DevExpress.Xpf.Grid.TreeListNode = New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.StageObject() With {.NameValue = "Testing and Delivery", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee()})
            stage15.Image = TreeListDemo.StageObject.Image
            stage15.Nodes.Add(New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.TaskObject() With {.NameValue = "Testing", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee(), .StartDate = New System.DateTime(2011, 10, 13), .EndDate = New System.DateTime(2011, 10, 14), .State = TreeListDemo.States.DataSource(0)}) With {.Image = TreeListDemo.TaskObject.Image})
            stage15.Nodes.Add(New DevExpress.Xpf.Grid.TreeListNode(New TreeListDemo.TaskObject() With {.NameValue = "Content", .Executor = TreeListDemo.UnboundMode.GetRandomEmployee(), .StartDate = New System.DateTime(2011, 10, 13), .EndDate = New System.DateTime(2011, 10, 14), .State = TreeListDemo.States.DataSource(0)}) With {.Image = TreeListDemo.TaskObject.Image})
            stantoneProject.Nodes.Add(stage11)
            stantoneProject.Nodes.Add(stage12)
            stantoneProject.Nodes.Add(stage13)
            stantoneProject.Nodes.Add(stage14)
            stantoneProject.Nodes.Add(stage15)
        End Sub
#End Region
    End Class

#Region "Classes"
    Public Class State
        Implements System.IComparable

        Public Property Image As ImageSource

        Public Property TextValue As String

        Public Property StateValue As Integer

        Public Overrides Function ToString() As String
            Return Me.TextValue
        End Function

        Public Function CompareTo(ByVal obj As Object) As Integer Implements Global.System.IComparable.CompareTo
            Return System.Collections.Generic.Comparer(Of Integer).[Default].Compare(Me.StateValue, CType(obj, TreeListDemo.State).StateValue)
        End Function
    End Class

    Public Class TaskObject

        Public Property NameValue As [String]

        Public Property StartDate As DateTime

        Public Property EndDate As DateTime

        Public Property Executor As Employee

        Public Property State As State

        Public Shared ReadOnly Property Image As ImageSource
            Get
                Return TreeListDemo.ImageHelper.GetSvgImage("Object_Task")
            End Get
        End Property
    End Class

    Public Class States
        Inherits System.Collections.Generic.List(Of TreeListDemo.State)

        Private Shared src As System.Collections.Generic.List(Of TreeListDemo.State)

        Public Shared ReadOnly Property DataSource As List(Of TreeListDemo.State)
            Get
                If TreeListDemo.States.src Is Nothing Then
                    TreeListDemo.States.src = New System.Collections.Generic.List(Of TreeListDemo.State)()
                    Call TreeListDemo.States.src.Add(New TreeListDemo.State() With {.TextValue = "Not started", .StateValue = 0, .Image = TreeListDemo.ImageHelper.GetSvgImage("State_NotStarted")})
                    Call TreeListDemo.States.src.Add(New TreeListDemo.State() With {.TextValue = "In progress", .StateValue = 1, .Image = TreeListDemo.ImageHelper.GetSvgImage("State_InProgress")})
                    Call TreeListDemo.States.src.Add(New TreeListDemo.State() With {.TextValue = "Completed", .StateValue = 2, .Image = TreeListDemo.ImageHelper.GetSvgImage("State_Completed")})
                End If

                Return TreeListDemo.States.src
            End Get
        End Property
    End Class

    Public Class ProjectObject

        Public Property NameValue As [String]

        Private executorField As DevExpress.Xpf.DemoBase.DataClasses.Employee

        Public ReadOnly Property Executor As Employee
            Get
                If Me.executorField Is Nothing Then Me.executorField = TreeListDemo.UnboundMode.GetRandomEmployee()
                Return Me.executorField
            End Get
        End Property

        Public Shared ReadOnly Property Image As ImageSource
            Get
                Return TreeListDemo.ImageHelper.GetSvgImage("Object_Project")
            End Get
        End Property
    End Class

    Public Class StageObject

        Public Property NameValue As [String]

        Public Property Executor As Employee

        Public Shared ReadOnly Property Image As ImageSource
            Get
                Return TreeListDemo.ImageHelper.GetSvgImage("Object_Stage")
            End Get
        End Property
    End Class

    Public Class GroupNameToImageConverterExtension
        Inherits System.Windows.Markup.MarkupExtension
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            Return TreeListDemo.EmployeeCategoryImageSelector.GetImageByGroupName(CStr(value))
        End Function

        Public Overridable Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotImplementedException()
        End Function

        Public NotOverridable Overrides Function ProvideValue(ByVal serviceProvider As System.IServiceProvider) As Object
            Return Me
        End Function
    End Class
#End Region
End Namespace
