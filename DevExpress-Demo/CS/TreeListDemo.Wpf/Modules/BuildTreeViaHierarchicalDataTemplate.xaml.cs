using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.TreeList;
using System.Windows.Controls;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Mvvm;

namespace TreeListDemo {
    public partial class BuildTreeViaHierarchicalDataTemplate : TreeListDemoModule {
        public BuildTreeViaHierarchicalDataTemplate() {
            InitializeComponent();
        }
    }
    public enum Priority { Low, Normal, High }

    public class BaseObject : BindableBase {
        public BaseObject() {
            ExecutorObj = GetRandomEmployee();
            Executor = ExecutorObj.ToString();
        }
        static readonly Random Random = new Random(DateTime.Now.Second);
        public static Employee GetRandomEmployee() {
            if(EmployeesData.DataSource == null) return null;
            return EmployeesData.DataSource[Random.Next(EmployeesData.DataSource.Count)];
        }
        string nameCore;
        String executorCore;
        ProgressingObject ownerCore;

        public string Name {
            get { return nameCore; }
            set { SetProperty(ref nameCore, value, "Name"); }
        }

        public String Executor {
            get { return executorCore; }
            set { SetProperty(ref executorCore, value, "Executor"); }
        }

        public override string ToString() {
            return Name;
        }

        public ProgressingObject Owner {
            get { return ownerCore; }
            set { SetProperty(ref ownerCore, value, "Owner"); }
        }

        public Employee ExecutorObj { get; set; }
    }

    public class ProgressingObject : BaseObject {

        int progressCore;

        public virtual void UpdateProgress() { }

        public int Progress {
            get { return progressCore; }
            set {
                if(SetProperty(ref progressCore, value, "Progress")) {
                    if(Owner != null)
                        Owner.UpdateProgress();
                }
            }
        }
    }

    public class Project : ProgressingObject {
        protected static ImageSource StaticImage;
        static Project() {
            StaticImage = ImageHelper.GetSvgImage("Object_Project");
        }
        public Project() {
            Stages = new ObservableCollection<Stage>();
            Stages.CollectionChanged += (s, e) => { this.UpdateProgress(); };
        }
        public ObservableCollection<Stage> Stages { get; set; }
        public override void UpdateProgress() {
            int completed = 0, sum = 0;

            if(Stages != null)
                foreach(Stage stage in Stages) {
                    sum++;
                    if(stage.Progress >= 100)
                        completed++;
                }
            if(sum == 0)
                Progress = 100;
            else
                Progress = completed * 100 / sum;
        }

        public ImageSource Image { get { return StaticImage; } }
    }

    public class Stage : ProgressingObject {
        protected static ImageSource StaticImage;
        static Stage() {
            StaticImage = ImageHelper.GetSvgImage("Object_Stage");
        }
        public Stage() {
            Tasks = new ObservableCollection<Task>();
            Tasks.CollectionChanged += (s, e) => { this.UpdateProgress(); };
        }
        public override void UpdateProgress() {
            int completed = 0, sum = 0;

            if(Tasks != null)
                foreach(Task task in Tasks) {
                    sum++;
                    if(task.State == null || task.State.StateValue == 2)
                        completed++;
                }
            if(sum == 0)
                Progress = 100;
            else
                Progress = completed * 100 / sum;
        }
        public ObservableCollection<Task> Tasks { get; set; }

        public ImageSource Image { get { return StaticImage; } }
    }

    public class Task : BaseObject {
        protected static ImageSource StaticImage;
        static Task() {
            StaticImage = ImageHelper.GetSvgImage("Object_Task");
        }
        DateTime startDateCore;
        DateTime endDateCore;
        State stateCore;
        Priority priorityCore;
        public bool alertCore;

        public DateTime StartDate {
            get { return startDateCore; }
            set { SetProperty(ref startDateCore, value, "StartDate"); }
        }

        public DateTime EndDate {
            get { return endDateCore; }
            set {
                if(SetProperty(ref endDateCore, value, "EndDate"))
                    UpdateAlertState();
            }
        }

        public State State {
            get { return stateCore; }
            set {
                if(SetProperty(ref stateCore, value, "State")) {
                    if(Owner != null)
                        Owner.UpdateProgress();
                    UpdateAlertState();
                }
            }
        }


        public Priority Priority {
            get { return priorityCore; }
            set { SetProperty(ref priorityCore, value, "Priority"); }
        }


        public bool Alert {
            get { return alertCore; }
            set { SetProperty(ref alertCore, value, "Alert"); }
        }

        void UpdateAlertState() {
            if(stateCore != null)
                Alert = EndDate < DateTime.Now && State.StateValue != 2;
        }

        public ImageSource Image { get { return StaticImage; } }
    }

    public class HierarchicalViewModel {
        public ObservableCollection<Project> DataItems { get; set; }

        public HierarchicalViewModel() {
            DataItems = InitData();
        }

        private ObservableCollection<Project> InitData() {
            ObservableCollection<Project> projects = new ObservableCollection<Project>();
            Project betaronProject = new Project() { Name = "Project: Betaron" };
            Project stantoneProject = new Project() { Name = "Project: Stanton" };

            InitBetaronProjectData(betaronProject);
            InitStantoneProjectData(stantoneProject);

            projects.Add(betaronProject);
            projects.Add(stantoneProject);

            return projects;
        }

        void InitBetaronProjectData(Project betaronProject) {

            Stage stage21 = new Stage() { Name = "Information Gathering", Owner = betaronProject };
            stage21.Tasks.Add(new Task() { Name = "Market research", StartDate = new DateTime(2011, 8, 1), EndDate = new DateTime(2011, 8, 5), State = States.DataSource[2], Owner = stage21, Priority = Priority.Normal });
            stage21.Tasks.Add(new Task() { Name = "Making specification", StartDate = new DateTime(2011, 8, 5), EndDate = new DateTime(2011, 8, 10), State = States.DataSource[1], Owner = stage21, Priority = Priority.High });

            Stage stage22 = new Stage() { Name = "Planning", Owner = betaronProject };
            stage22.Tasks.Add(new Task() { Name = "Documentation", StartDate = new DateTime(2011, 9, 15), EndDate = new DateTime(2011, 9, 16), State = States.DataSource[0], Owner = stage22, Priority = Priority.High });

            Stage stage23 = new Stage() { Name = "Design", Owner = betaronProject };
            stage23.Tasks.Add(new Task() { Name = "Design of a web pages", StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[0], Owner = stage23, Priority = Priority.Normal });
            stage23.Tasks.Add(new Task() { Name = "Pages layout", StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[0], Owner = stage23, Priority = Priority.Low });

            Stage stage24 = new Stage() { Name = "Development", Owner = betaronProject };
            stage24.Tasks.Add(new Task() { Name = "Design", StartDate = new DateTime(2011, 10, 27), EndDate = new DateTime(2011, 10, 28), State = States.DataSource[0], Owner = stage24, Priority = Priority.Normal });
            stage24.Tasks.Add(new Task() { Name = "Coding", StartDate = new DateTime(2011, 10, 29), EndDate = new DateTime(2011, 10, 30), State = States.DataSource[0], Owner = stage24, Priority = Priority.Normal });

            Stage stage25 = new Stage() { Name = "Testing and Delivery", Owner = betaronProject };
            stage25.Tasks.Add(new Task() { Name = "Testing", StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[0], Owner = stage25, Priority = Priority.Low });
            stage25.Tasks.Add(new Task() { Name = "Content", StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[0], Owner = stage25, Priority = Priority.Normal });

            betaronProject.Stages.Add(stage21);
            betaronProject.Stages.Add(stage22);
            betaronProject.Stages.Add(stage23);
            betaronProject.Stages.Add(stage24);
            betaronProject.Stages.Add(stage25);
        }

        void InitStantoneProjectData(Project stantoneProject) {

            Stage stage11 = new Stage() { Name = "Information Gathering", Owner = stantoneProject };
            stage11.Tasks.Add(new Task() { Name = "Market research", StartDate = new DateTime(2011, 7, 1), EndDate = new DateTime(2011, 7, 5), State = States.DataSource[2], Owner = stage11, Priority = Priority.Normal });
            stage11.Tasks.Add(new Task() { Name = "Making specification", StartDate = new DateTime(2011, 7, 5), EndDate = new DateTime(2011, 7, 10), State = States.DataSource[2], Owner = stage11, Priority = Priority.High });

            Stage stage12 = new Stage() { Name = "Planning", Owner = stantoneProject };
            stage12.Tasks.Add(new Task() { Name = "Documentation", StartDate = new DateTime(2011, 8, 13), EndDate = new DateTime(2011, 8, 14), State = States.DataSource[2], Owner = stage12, Priority = Priority.Normal });

            Stage stage13 = new Stage() { Name = "Design", Owner = stantoneProject };
            stage13.Tasks.Add(new Task() { Name = "Design of a web pages", StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[1], Owner = stage13, Priority = Priority.Normal });
            stage13.Tasks.Add(new Task() { Name = "Pages layout", StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[1], Owner = stage13, Priority = Priority.Normal });

            Stage stage14 = new Stage() { Name = "Development", Owner = stantoneProject };
            stage14.Tasks.Add(new Task() { Name = "Design", StartDate = new DateTime(2011, 10, 23), EndDate = new DateTime(2011, 10, 24), State = States.DataSource[1], Owner = stage14, Priority = Priority.Low });
            stage14.Tasks.Add(new Task() { Name = "Coding", StartDate = new DateTime(2011, 10, 25), EndDate = new DateTime(2011, 10, 26), State = States.DataSource[0], Owner = stage14, Priority = Priority.Normal });

            Stage stage15 = new Stage() { Name = "Testing and Delivery", Owner = stantoneProject };
            stage15.Tasks.Add(new Task() { Name = "Testing", StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[0], Owner = stage15 });
            stage15.Tasks.Add(new Task() { Name = "Content", StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[0], Owner = stage15, Priority = Priority.High });

            stantoneProject.Stages.Add(stage11);
            stantoneProject.Stages.Add(stage12);
            stantoneProject.Stages.Add(stage13);
            stantoneProject.Stages.Add(stage14);
            stantoneProject.Stages.Add(stage15);
        }
    }

    public class ObjectTemplateSelector : DataTemplateSelector {
        public DataTemplate ProjectTemplate { get; set; }
        public DataTemplate StageTemplate { get; set; }
        public DataTemplate TaskTemplate { get; set; }
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container) {
            TreeListRowData rowData = item as TreeListRowData;
            if(rowData != null) {
                if(rowData.Row is Project)
                    return ProjectTemplate;
                if(rowData.Row is Stage)
                    return StageTemplate;
                if(rowData.Row is Task)
                    return TaskTemplate;
            }
            return null;
        }
    }
}
