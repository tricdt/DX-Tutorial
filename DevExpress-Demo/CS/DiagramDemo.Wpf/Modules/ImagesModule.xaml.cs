using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    public partial class ImagesModule : DiagramDemoModule {
        public ImagesModule() {
            InitializeComponent();
            diagramControl.MouseDoubleClick += (o, e) => {
                if(e.ChangedButton != System.Windows.Input.MouseButton.Left)
                    return;
                var imageItem = diagramControl.CalcHitItem(e.GetPosition(diagramControl)) as DiagramImage;
                if(imageItem == null || !object.Equals(imageItem.Tag, typeof(DiagramImage).Name))
                    return;
                diagramControl.SelectItem(imageItem);
                diagramControl.LoadImage();
            };
        }
    }
}
