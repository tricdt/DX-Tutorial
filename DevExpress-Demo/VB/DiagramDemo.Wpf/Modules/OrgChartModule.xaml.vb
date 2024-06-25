Imports DevExpress.Diagram.Demos
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Diagram
Imports System
Imports System.Linq
Imports System.Windows

Namespace DiagramDemo

    <CodeFile("ViewModels/OrgChartViewModel.(cs)")>
    <CodeFile("Resources/OrgChartModuleResources.xaml")>
    Public Partial Class OrgChartModule
        Inherits DiagramDemoModule

        Private ReadOnly ViewModel As OrgChartViewModel

        Public Sub New()
            InitializeComponent()
            ViewModel = CreateViewModel()
            DataContext = ViewModel
            AddHandler orgChartBehavior.ItemsGenerated, Sub(_o, _e) FitToItems()
        End Sub

        Private Function CreateViewModel() As OrgChartViewModel
            Dim viewModel = ViewModelSource.Create(Function() New OrgChartViewModel(FilteredEmployees.ToArray(), GetOrgChartTemplates()))
            AddHandler viewModel.SelectedTemplateChanged, Sub(_d, _e) orgChartBehavior.Refresh()
            Return viewModel
        End Function

        Private Sub FitToItems()
            Dim items = diagramControl.Items.OfType(Of DiagramContainer)().GroupBy(Function(x) x.Position.Y).OrderBy(Function(x) x.Key).Take(3).SelectMany(Function(x) x).ToArray()
            diagramControl.FitToItems(items)
        End Sub

        Private Function GetOrgChartTemplates() As EmployeeTemplate()
            Dim templateNames = orgChartBehavior.TemplateDiagram.Items.OfType(Of DiagramContainer)().[Select](Function(x) x.TemplateName).ToArray()
            Dim templateImages = templateNames.[Select](Function(x) String.Format("images/orgchart/{0}.png", x.Replace(" ", String.Empty).ToLower())).[Select](Function(x) GetResourceStream(x))
            Return templateNames.Zip(templateImages, Function(name, image) New EmployeeTemplate(name, image)).ToArray()
        End Function

        Private Sub GenerateOrgChartItem(ByVal sender As Object, ByVal e As DiagramGenerateItemEventArgs)
            If ViewModel IsNot Nothing AndAlso ViewModel.SelectedTemplate IsNot Nothing Then e.Item = e.CreateItemFromTemplate(ViewModel.SelectedTemplate.Name)
        End Sub
    End Class

    Public Class DiagramSelectionBehavior
        Inherits Behavior(Of DiagramControl)

        Public Shared ReadOnly SelectedItemProperty As DependencyProperty = DependencyProperty.Register("SelectedItem", GetType(Object), GetType(DiagramSelectionBehavior), New PropertyMetadata(Nothing, Sub(d, e) CType(d, DiagramSelectionBehavior).OnSelectedItemChanged()))

        Public Property SelectedItem As Object
            Get
                Return CObj(GetValue(SelectedItemProperty))
            End Get

            Set(ByVal value As Object)
                SetValue(SelectedItemProperty, value)
            End Set
        End Property

        Private isSelectionLocked As Boolean = False

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.SelectionChanged, AddressOf AssociatedObject_SelectionChanged
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            RemoveHandler AssociatedObject.SelectionChanged, AddressOf AssociatedObject_SelectionChanged
        End Sub

        Private Sub AssociatedObject_SelectionChanged(ByVal sender As Object, ByVal e As DiagramSelectionChangedEventArgs)
            DoLockedActionIfNotLocked(Sub()
                If AssociatedObject.PrimarySelection IsNot Nothing Then
                    SelectedItem = AssociatedObject.PrimarySelection.DataContext
                Else
                    SelectedItem = Nothing
                End If
            End Sub)
        End Sub

        Private Sub OnSelectedItemChanged()
            DoLockedActionIfNotLocked(Sub()
                If SelectedItem IsNot Nothing Then
                    Dim diagramItem = AssociatedObject.Items.FirstOrDefault(Function(x) x.DataContext Is SelectedItem)
                    If diagramItem IsNot Nothing Then
                        AssociatedObject.SelectItem(diagramItem)
                        AssociatedObject.BringItemsIntoView({diagramItem})
                    End If
                Else
                    AssociatedObject.ClearSelection()
                End If
            End Sub)
        End Sub

        Private Sub DoLockedActionIfNotLocked(ByVal action As Action)
            If isSelectionLocked Then Return
            isSelectionLocked = True
            action()
            isSelectionLocked = False
        End Sub
    End Class
End Namespace
