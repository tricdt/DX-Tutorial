using System.Collections.Generic;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public class PointSeriesViewViewModel {
        public static PointSeriesViewViewModel Create() {
            return ViewModelSource.Create(() => new PointSeriesViewViewModel());
        }

        readonly List<Marker3DPointModel> models = new List<Marker3DPointModel> {
            new Marker3DCubePointModel(),
            new Marker3DSpherePointModel()
        };

        public List<Marker3DPointModel> PointModels { get { return this.models; } }
        public virtual Marker3DPointModel SelectedPointModel { get; set; }
        public virtual DetalizationLevel SelectedDetalizationLevel { get; set; }
        public virtual double MarkerSize { get; set; }
        public virtual bool IsDetalizationLevelEnabled { get; set; }

        protected PointSeriesViewViewModel() {
            SelectedPointModel = this.models[1];
            SelectedDetalizationLevel = DetalizationLevel.Normal;
            MarkerSize = 30;
        }

        protected void OnSelectedDetalizationLevelChanged() {
            Marker3DSpherePointModel sphereModel = SelectedPointModel as Marker3DSpherePointModel;
            if (sphereModel != null)
                sphereModel.SphereDetalizationLevel = SelectedDetalizationLevel;
        }
        protected void OnSelectedPointModelChanged() {
            if (SelectedPointModel is Marker3DSpherePointModel)
                IsDetalizationLevelEnabled = true;
            else
                IsDetalizationLevelEnabled = false;
        }
    }
}
