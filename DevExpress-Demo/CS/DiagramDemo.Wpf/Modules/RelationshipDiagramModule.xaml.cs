using DevExpress.Diagram.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Data;
using System.Globalization;
using DevExpress.Diagram.Demos;
using DevExpress.Mvvm.POCO;

namespace DiagramDemo {
    [CodeFile("ViewModels/RelationshipDiagramViewModel.(cs)")]
    [CodeFile("Resources/RelationshipDiagramModuleResources.xaml")]
    public partial class RelationshipDiagramModule : DiagramDemoModule {
        const double DefaultStrokeThickness = 2;
        const double SelectedStrokeThickness = 4;
        readonly RelationshipDiagramViewModel ViewModel;
        bool onStartup = true;

        public RelationshipDiagramModule() {
            ViewModel = ViewModelSource.Create(() => new RelationshipDiagramViewModel(EmployeesData.FilteredEmployees.Take(9).ToArray()));
            DataContext = ViewModel;
            InitializeComponent();
        }

        void diagramControl_SelectionChanged(object sender, EventArgs e) {
            foreach(var diagramConnector in diagramControl.Items.OfType<DiagramConnector>()) {
                bool isSelectionRelatedConnector = diagramControl.SelectedItems.Contains((DiagramItem)diagramConnector.BeginItem) || diagramControl.SelectedItems.Contains((DiagramItem)diagramConnector.EndItem);
                diagramConnector.StrokeThickness = isSelectionRelatedConnector ? SelectedStrokeThickness : DefaultStrokeThickness;
            }
            ViewModel.SelectedEmployee = diagramControl.PrimarySelection != null 
                ? (diagramControl.PrimarySelection.DataContext as Employee) 
                : null;
        }

        void dataBindingBehavior_GenerateConnector(object sender, DiagramGenerateConnectorEventArgs e) {
            var relationshipInfo = (RelationshipInfo)e.DataObject;
            e.Connector = e.CreateConnectorFromTemplate(relationshipInfo.Relationship.ToString());
        }

        void diagramControl_ItemsChanged(object sender, DiagramItemsChangedEventArgs e) {
            if(onStartup && e.Item is DiagramContainer) {
                onStartup = false;
                diagramControl.SelectItem((DiagramContainer)e.Item);
            }            
        }

        void dataBindingBehavior_ItemsGenerated(object sender, DiagramItemsGeneratedEventArgs e) {
            diagramControl.FitToDrawing();
            diagramControl.IsReadOnly = true;
        }
    }
}
