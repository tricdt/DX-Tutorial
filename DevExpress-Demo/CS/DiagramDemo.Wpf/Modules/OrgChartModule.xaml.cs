using DevExpress.Diagram.Demos;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Diagram;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace DiagramDemo {
    [CodeFile("ViewModels/OrgChartViewModel.(cs)")]
    [CodeFile("Resources/OrgChartModuleResources.xaml")]
    public partial class OrgChartModule : DiagramDemoModule {
        readonly OrgChartViewModel ViewModel;

        public OrgChartModule() {
            InitializeComponent();
            DataContext = ViewModel = CreateViewModel();
            orgChartBehavior.ItemsGenerated += (_o, _e) => FitToItems();
        }

        OrgChartViewModel CreateViewModel() {
            var viewModel = ViewModelSource.Create(() => 
                new OrgChartViewModel(EmployeesData.FilteredEmployees.ToArray(), GetOrgChartTemplates()));
            viewModel.SelectedTemplateChanged += (_d, _e) => orgChartBehavior.Refresh();
            return viewModel;
        }
        void FitToItems() {
            var items = diagramControl.Items
                            .OfType<DiagramContainer>()
                            .GroupBy(x => x.Position.Y)
                            .OrderBy(x => x.Key)
                            .Take(3)
                            .SelectMany(x => x)
                            .ToArray();
            diagramControl.FitToItems(items);
        }

        EmployeeTemplate[] GetOrgChartTemplates() {
            var templateNames = orgChartBehavior.TemplateDiagram.Items
                .OfType<DiagramContainer>()
                .Select(x => x.TemplateName)
                .ToArray();

            var templateImages = templateNames
                .Select(x => string.Format("images/orgchart/{0}.png", x.Replace(" ", string.Empty).ToLower()))
                .Select(x => DiagramDemoFileHelper.GetResourceStream(x));
            return templateNames.Zip(templateImages, (name, image) => new EmployeeTemplate(name, image)).ToArray();
        }
        void GenerateOrgChartItem(object sender, DiagramGenerateItemEventArgs e) {
            if(ViewModel != null && ViewModel.SelectedTemplate != null)
                e.Item = e.CreateItemFromTemplate(ViewModel.SelectedTemplate.Name);
        }
    }
    public class DiagramSelectionBehavior : Behavior<DiagramControl> {
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(DiagramSelectionBehavior), new PropertyMetadata(null, (d, e) => ((DiagramSelectionBehavior)d).OnSelectedItemChanged()));
        public object SelectedItem {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        bool isSelectionLocked = false;

        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
        }

        void AssociatedObject_SelectionChanged(object sender, DiagramSelectionChangedEventArgs e) {
            DoLockedActionIfNotLocked(() => {
                if(AssociatedObject.PrimarySelection != null)
                    SelectedItem = AssociatedObject.PrimarySelection.DataContext;
                else
                    SelectedItem = null;
            });
        }

        void OnSelectedItemChanged() {
            DoLockedActionIfNotLocked(() => {
                if(SelectedItem != null) {
                    var diagramItem = AssociatedObject.Items.FirstOrDefault(x => x.DataContext == SelectedItem);
                    if(diagramItem != null) {
                        AssociatedObject.SelectItem(diagramItem);
                        AssociatedObject.BringItemsIntoView(new[] { diagramItem });
                    }
                }
                else {
                    AssociatedObject.ClearSelection();
                }
            });
        }
        void DoLockedActionIfNotLocked(Action action) {
            if(isSelectionLocked)
                return;
            isSelectionLocked = true;
            action();
            isSelectionLocked = false;
        }
    }
}
