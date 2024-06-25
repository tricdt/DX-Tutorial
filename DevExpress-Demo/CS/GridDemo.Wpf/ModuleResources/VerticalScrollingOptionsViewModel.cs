using DevExpress.Mvvm;

namespace GridDemo {
    public class VerticalScrollingOptionsViewModel : BindableBase {
        GridControlDefinition selectedDefinition;
        public GridControlDefinition SelectedDefinition {
            get { return selectedDefinition; }
            set { SetProperty(ref selectedDefinition, value, () => SelectedDefinition); }
        }
        GridControlDefinitionCollection controlDefinitions;
        public GridControlDefinitionCollection ControlDefinitions {
            get { return controlDefinitions; }
            set {
                controlDefinitions = value;
                if(ControlDefinitions != null && ControlDefinitions.Count > 0)
                    SelectedDefinition = ControlDefinitions[0];
            }
        }
    }
}
