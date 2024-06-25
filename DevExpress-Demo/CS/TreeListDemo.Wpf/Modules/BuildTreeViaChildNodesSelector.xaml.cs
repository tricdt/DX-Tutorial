using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Grid;
using System.Collections;

namespace TreeListDemo {
    public partial class BuildTreeViaChildNodesSelector : TreeListDemoModule {
        public BuildTreeViaChildNodesSelector() {
            InitializeComponent();
        }
    }

    public class ChildNodesSelectorViewModel {
        public ObservableCollection<Project> DataItems { get; set; }

        public ChildNodesSelectorViewModel() {
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

            Stage stage21 = new Stage() { Name = "Information Gathering"};
            stage21.Tasks.Add(new Task() { Name = "Market research", StartDate = new DateTime(2011, 8, 1), EndDate = new DateTime(2011, 8, 5), State = States.DataSource[2] });
            stage21.Tasks.Add(new Task() { Name = "Making specification", StartDate = new DateTime(2011, 8, 5), EndDate = new DateTime(2011, 8, 10), State = States.DataSource[1] });

            Stage stage22 = new Stage() { Name = "Planning" };
            stage22.Tasks.Add(new Task() { Name = "Documentation", StartDate = new DateTime(2011, 9, 15), EndDate = new DateTime(2011, 9, 16), State = States.DataSource[0] });

            Stage stage23 = new Stage() { Name = "Design" };
            stage23.Tasks.Add(new Task() { Name = "Design of a web pages", StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[0] });
            stage23.Tasks.Add(new Task() { Name = "Pages layout", StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[0] });

            Stage stage24 = new Stage() { Name = "Development" };
            stage24.Tasks.Add(new Task() { Name = "Design", StartDate = new DateTime(2011, 10, 27), EndDate = new DateTime(2011, 10, 28), State = States.DataSource[0] });
            stage24.Tasks.Add(new Task() { Name = "Coding", StartDate = new DateTime(2011, 10, 29), EndDate = new DateTime(2011, 10, 30), State = States.DataSource[0] });

            Stage stage25 = new Stage() { Name = "Testing and Delivery" };
            stage25.Tasks.Add(new Task() { Name = "Testing", StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[0] });
            stage25.Tasks.Add(new Task() { Name = "Content", StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[0] });

            betaronProject.Stages.Add(stage21);
            betaronProject.Stages.Add(stage22);
            betaronProject.Stages.Add(stage23);
            betaronProject.Stages.Add(stage24);
            betaronProject.Stages.Add(stage25);
        }

        void InitStantoneProjectData(Project stantoneProject) {

            Stage stage11 = new Stage() { Name = "Information Gathering" };
            stage11.Tasks.Add(new Task() { Name = "Market research", StartDate = new DateTime(2011, 7, 1), EndDate = new DateTime(2011, 7, 5), State = States.DataSource[2] });
            stage11.Tasks.Add(new Task() { Name = "Making specification", StartDate = new DateTime(2011, 7, 5), EndDate = new DateTime(2011, 7, 10), State = States.DataSource[2] });

            Stage stage12 = new Stage() { Name = "Planning" };
            stage12.Tasks.Add(new Task() { Name = "Documentation", StartDate = new DateTime(2011, 8, 13), EndDate = new DateTime(2011, 8, 14), State = States.DataSource[2] });

            Stage stage13 = new Stage() { Name = "Design" };
            stage13.Tasks.Add(new Task() { Name = "Design of a web pages", StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[1] });
            stage13.Tasks.Add(new Task() { Name = "Pages layout", StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[1] });

            Stage stage14 = new Stage() { Name = "Development" };
            stage14.Tasks.Add(new Task() { Name = "Design", StartDate = new DateTime(2011, 10, 23), EndDate = new DateTime(2011, 10, 24), State = States.DataSource[1] });
            stage14.Tasks.Add(new Task() { Name = "Coding", StartDate = new DateTime(2011, 10, 25), EndDate = new DateTime(2011, 10, 26), State = States.DataSource[0] });

            Stage stage15 = new Stage() { Name = "Testing and Delivery"};
            stage15.Tasks.Add(new Task() { Name = "Testing", StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[0] });
            stage15.Tasks.Add(new Task() { Name = "Content", StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[0] });

            stantoneProject.Stages.Add(stage11);
            stantoneProject.Stages.Add(stage12);
            stantoneProject.Stages.Add(stage13);
            stantoneProject.Stages.Add(stage14);
            stantoneProject.Stages.Add(stage15);
        }
    }

    public class DemoChildSelector : IChildNodesSelector {
        IEnumerable IChildNodesSelector.SelectChildren(object item) {
            if(item is Project)
                return (item as Project).Stages;
            if(item is Stage)
                return (item as Stage).Tasks;
            return null;
        }
    }
}
