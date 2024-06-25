using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm.POCO;

namespace ChartsDemo {

    public class BoxPlotViewModel {
        public static BoxPlotViewModel Create() {
            return ViewModelSource.Create(() => new BoxPlotViewModel());
        }

        const int InitialResultCount = 25;
        const int MeasurementsCount = 500;
        const double SecondPointSeriesArgumentOffset = 2d;

        Random random = new Random(3);
        List<double> randomValues1;
        List<double> randomValues2;

        public virtual ObservableCollection<PointData> CurrentExperimentRandomValues1 { get; protected set; }
        public virtual ObservableCollection<PointData> CurrentExperimentRandomValues2 { get; protected set; }
        public virtual ObservableCollection<BoxPlotPoint> ExperimentResults1 { get; protected set; }
        public virtual ObservableCollection<BoxPlotPoint> ExperimentResults2 { get; protected set; }
        public virtual int ExperimentNumber { get; protected set; }
        public virtual double CurrentExperimentStripMinLimit { get; protected set; }
        public virtual double CurrentExperimentStripMaxLimit { get; protected set; }
        public virtual bool ShowMeanLine { get; set; }
        public virtual int MeanLineThickness { get; set; }

        public BoxPlotViewModel() {
            ExperimentNumber = 0;
            CurrentExperimentRandomValues1 = new ObservableCollection<PointData>();
            CurrentExperimentRandomValues2 = new ObservableCollection<PointData>();
            ExperimentResults1 = new ObservableCollection<BoxPlotPoint>();
            ExperimentResults2 = new ObservableCollection<BoxPlotPoint>();
            MeanLineThickness = 1;
            GenerateInitialResults();
        }

        void GenerateInitialResults() {
            for (int i = InitialResultCount - 1; i > 0; i--) {
                List<double> measurements1 = RandomSequenceGenerator.GenerateRandomSequence(random, MeasurementsCount);
                List<double> measurements2 = RandomSequenceGenerator.GenerateRandomSequence(random, MeasurementsCount);
                ExperimentNumber++;
                BoxPlotPoint point1 = new BoxPlotPoint(ExperimentNumber, measurements1);
                BoxPlotPoint point2 = new BoxPlotPoint(ExperimentNumber, measurements2);
                ExperimentResults1.Add(point1);
                ExperimentResults2.Add(point2);
            }
            this.randomValues1 = RandomSequenceGenerator.GenerateRandomSequence(random, MeasurementsCount);
            this.randomValues2 = RandomSequenceGenerator.GenerateRandomSequence(random, MeasurementsCount);
            for (int i = 0; i < MeasurementsCount; i++) {
                CurrentExperimentRandomValues1.Add(new PointData(this.randomValues1[i], random));
                CurrentExperimentRandomValues2.Add(new PointData(this.randomValues2[i], random, SecondPointSeriesArgumentOffset));
            }
            ExperimentNumber++;
            ExperimentResults1.Add(new BoxPlotPoint(ExperimentNumber, this.randomValues1));
            ExperimentResults2.Add(new BoxPlotPoint(ExperimentNumber, this.randomValues2));
            CurrentExperimentStripMinLimit = InitialResultCount - 0.5;
            CurrentExperimentStripMaxLimit = InitialResultCount + 0.5;
        }

        public bool CanAddNewDataSet() {
            return true;
        }
        public void AddNewDataSet() {
            CurrentExperimentRandomValues1.Clear();
            CurrentExperimentRandomValues2.Clear();
            this.randomValues1 = RandomSequenceGenerator.GenerateRandomSequence(random, MeasurementsCount);
            this.randomValues2 = RandomSequenceGenerator.GenerateRandomSequence(random, MeasurementsCount);
            for (int i = 0; i < MeasurementsCount; i++) {
                CurrentExperimentRandomValues1.Add(new PointData(this.randomValues1[i], random));
                CurrentExperimentRandomValues2.Add(new PointData(this.randomValues2[i], random, SecondPointSeriesArgumentOffset));
            }
            ExperimentNumber++;
            CurrentExperimentStripMinLimit = ExperimentNumber - 0.5;
            CurrentExperimentStripMaxLimit = ExperimentNumber + 0.5;
        }
        public void CalculateBoxPlotForLastExperiment() {
            if (ExperimentNumber != ExperimentResults1.Last().ExperimentNumber) {
                ExperimentResults1.Add(new BoxPlotPoint(ExperimentNumber, this.randomValues1));
                ExperimentResults2.Add(new BoxPlotPoint(ExperimentNumber, this.randomValues2));
            }
        }
    }
}
