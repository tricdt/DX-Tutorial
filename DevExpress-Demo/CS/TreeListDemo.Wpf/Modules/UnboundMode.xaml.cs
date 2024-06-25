using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.TreeList;

namespace TreeListDemo {
    public partial class UnboundMode : TreeListDemoModule {
        const string StateFieldName = "State";
        const string StartDateFieldName = "StartDate";
        const string EndDateFieldName = "EndDate";

        static readonly Random Random = new Random(DateTime.Now.Second);
        public static Employee GetRandomEmployee() {
            if(EmployeesData.DataSource == null) return null;
            return EmployeesData.DataSource[Random.Next(EmployeesData.DataSource.Count)];
        }

        public UnboundMode() {
            InitializeComponent();
            InitData();
            view.ExpandAllNodes();
        }
        void InitData() {
            TreeListNode stantoneProject = view.Nodes[0];
            InitStantoneProjectData(stantoneProject);
            TreeListNode betaronProject = view.Nodes[1];
            InitBetaronProjectData(betaronProject);
        }
        void CellValueChanging(object sender, TreeListCellValueChangedEventArgs e) {
            if (e.Column.FieldName == StartDateFieldName || e.Column.FieldName == EndDateFieldName || e.Column.FieldName == StateFieldName)
                view.CommitEditing(true);
        }
        void GetColumnData(object sender, TreeListUnboundColumnDataEventArgs e) {
            if(e.IsSetData)
                SetUnboundCellData(sender, e);
            else
                GetUnboundCellData(sender, e);
        }
        void GetUnboundCellData(object sender, TreeListUnboundColumnDataEventArgs e) {
            switch(e.Column.FieldName) {
                case StartDateFieldName:
                    e.Value = GetUnboundStartDate(e, e.Node);
                    break;
                case EndDateFieldName:
                    e.Value = GetUnboundEndDate(e, e.Node);
                    break;
                case StateFieldName:
                    GetUnboundState(e, e.Node);
                    break;
                default:
                    break;
            }
        }
        void SetUnboundCellData(object sender, TreeListUnboundColumnDataEventArgs e) {
            TaskObject task = e.Node.Content as TaskObject;
            string FieldName = e.Column.FieldName;
            if(task != null) {
                switch(FieldName) {
                    case StartDateFieldName:
                        task.StartDate = (DateTime)(e.Value ?? DateTime.MinValue);
                        break;
                    case EndDateFieldName:
                        task.EndDate = (DateTime)(e.Value ?? DateTime.MinValue);
                        break;
                    case StateFieldName:
                        State newState = (State)e.Value;
                        if(task.State != newState) {
                            task.State = newState;
                            RecursiveNodeRefresh(e.Node.ParentNode);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        void RecursiveNodeRefresh(TreeListNode node) {
            if(node != null) {
                treeList.RefreshRow(node.RowHandle);
                RecursiveNodeRefresh(node.ParentNode);
            }
        }
        void EditorVisibility(object sender, TreeListShowingEditorEventArgs e) {
            String fieldName = e.Column.FieldName;
            e.Cancel = (fieldName == StartDateFieldName || fieldName == EndDateFieldName || fieldName == StateFieldName) 
                && !(e.Node.Content is TaskObject);
        }
        void CollectBoundStates(TreeListNode treeListNode, List<State> states) {
            TreeListNodeIterator iterator = new TreeListNodeIterator(treeListNode);
            foreach(TreeListNode node in iterator) {
                TaskObject task = node.Content as TaskObject;
                if(task != null)
                    states.Add(task.State);
            }
        }
        void GetUnboundState(TreeListUnboundColumnDataEventArgs e, TreeListNode treeListNode) {
            TaskObject task = treeListNode.Content as TaskObject;
            if (task != null) {
                e.Value = task.State;
                return;
            }
            List<State> statesList = new List<State>();
            CollectBoundStates(e.Node, statesList);
            if (statesList.Contains(States.DataSource[1])
                || (statesList.Contains(States.DataSource[0]) && statesList.Contains(States.DataSource[2])))
                e.Value = States.DataSource[1];
            else if (statesList.Contains(States.DataSource[0]))
                e.Value = States.DataSource[0];
            else if (statesList.Contains(States.DataSource[2]))
                e.Value = States.DataSource[2];
        }
        DateTime GetUnboundStartDate(TreeListUnboundColumnDataEventArgs e, TreeListNode treeListNode) {
            TaskObject task = treeListNode.Content as TaskObject;
            DateTime value = DateTime.Now;
            DateTime tempValue;
            if (task != null) {
                value = task.StartDate;
            } else {
                value = DateTime.MaxValue;
                foreach (TreeListNode item in treeListNode.Nodes) {
                    tempValue = GetUnboundStartDate(e, item);
                    if (tempValue < value)
                        value = tempValue;
                }
            }
            return value;
        }
        DateTime GetUnboundEndDate(TreeListUnboundColumnDataEventArgs e, TreeListNode treeListNode) {
            TaskObject task = treeListNode.Content as TaskObject;
            DateTime value = DateTime.Now;
            DateTime tempValue;
            if (task != null) {
                value = task.EndDate;
            } else {
                value = DateTime.MinValue;
                foreach (TreeListNode item in treeListNode.Nodes) {
                    tempValue = GetUnboundEndDate(e, item);
                    if (tempValue > value)
                        value = tempValue;
                }
            }
            return value;
        }
        

        #region Unbound Data Initialization
        void InitBetaronProjectData(TreeListNode betaronProject) {
            betaronProject.Image = ProjectObject.Image;
            TreeListNode stage21 = new TreeListNode(new StageObject() { NameValue = "Information Gathering", Executor = GetRandomEmployee() });
            stage21.Image = StageObject.Image;
            stage21.Nodes.Add(new TreeListNode(new TaskObject() { NameValue = "Market research", Executor = GetRandomEmployee(), StartDate = new DateTime(2011, 10, 1), EndDate = new DateTime(2011, 10, 5), State = States.DataSource[2] }) { Image = TaskObject.Image });
            stage21.Nodes.Add(new TreeListNode(new TaskObject() { NameValue = "Making specification", Executor = GetRandomEmployee(), StartDate = new DateTime(2011, 10, 5), EndDate = new DateTime(2011, 10, 10), State = States.DataSource[1] }) { Image = TaskObject.Image });
            TreeListNode stage22 = new TreeListNode(new StageObject() { NameValue = "Planning", Executor = GetRandomEmployee() });
            stage22.Image = StageObject.Image;
            stage22.Nodes.Add(new TreeListNode(new TaskObject() { NameValue = "Documentation", Executor = GetRandomEmployee(), StartDate = new DateTime(2011, 10, 15), EndDate = new DateTime(2011, 10, 16), State = States.DataSource[0] }) { Image = TaskObject.Image });
            TreeListNode stage23 = new TreeListNode(new StageObject() { NameValue = "Design", Executor = GetRandomEmployee() });
            stage23.Image = StageObject.Image;
            stage23.Nodes.Add(new TreeListNode(new TaskObject() { NameValue = "Design of a web pages", Executor = GetRandomEmployee(), StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[0] }) { Image = TaskObject.Image });
            stage23.Nodes.Add(new TreeListNode(new TaskObject() { NameValue = "Pages layout", Executor = GetRandomEmployee(), StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[0] }) { Image = TaskObject.Image });
            TreeListNode stage24 = new TreeListNode(new StageObject() { NameValue = "Development", Executor = GetRandomEmployee() });
            stage24.Image = StageObject.Image;
            stage24.Nodes.Add(new TreeListNode(new TaskObject() { NameValue = "Design", Executor = GetRandomEmployee(), StartDate = new DateTime(2011, 10, 27), EndDate = new DateTime(2011, 10, 28), State = States.DataSource[0] }) { Image = TaskObject.Image });
            stage24.Nodes.Add(new TreeListNode(new TaskObject() { NameValue = "Coding", Executor = GetRandomEmployee(), StartDate = new DateTime(2011, 10, 29), EndDate = new DateTime(2011, 10, 30), State = States.DataSource[0] }) { Image = TaskObject.Image });
            TreeListNode stage25 = new TreeListNode(new StageObject() { NameValue = "Testing and Delivery", Executor = GetRandomEmployee() });
            stage25.Image = StageObject.Image;
            stage25.Nodes.Add(new TreeListNode(new TaskObject() { NameValue = "Testing", Executor = GetRandomEmployee(), StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[0] }) { Image = TaskObject.Image });
            stage25.Nodes.Add(new TreeListNode(new TaskObject() { NameValue = "Content", Executor = GetRandomEmployee(), StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[0] }) { Image = TaskObject.Image });

            betaronProject.Nodes.Add(stage21);
            betaronProject.Nodes.Add(stage22);
            betaronProject.Nodes.Add(stage23);
            betaronProject.Nodes.Add(stage24);
            betaronProject.Nodes.Add(stage25);
        }

        void InitStantoneProjectData(TreeListNode stantoneProject) {
            stantoneProject.Image = ProjectObject.Image;

            TreeListNode stage11 = new TreeListNode(new StageObject() { NameValue = "Information Gathering", Executor = GetRandomEmployee() });
            stage11.Image = StageObject.Image;

            stage11.Nodes.Add(new TreeListNode(new TaskObject() { NameValue = "Market research", Executor = GetRandomEmployee(), StartDate = new DateTime(2011, 10, 1), EndDate = new DateTime(2011, 10, 5), State = States.DataSource[2] }) { Image = TaskObject.Image });
            stage11.Nodes.Add(new TreeListNode(new TaskObject() { NameValue = "Making specification", Executor = GetRandomEmployee(), StartDate = new DateTime(2011, 10, 5), EndDate = new DateTime(2011, 10, 10), State = States.DataSource[2] }) { Image = TaskObject.Image });
            TreeListNode stage12 = new TreeListNode(new StageObject() { NameValue = "Planning", Executor = GetRandomEmployee() });
            stage12.Image = StageObject.Image;
            stage12.Nodes.Add(new TreeListNode(new TaskObject() { NameValue = "Documentation", Executor = GetRandomEmployee(), StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[2] }) { Image = TaskObject.Image });

            TreeListNode stage13 = new TreeListNode(new StageObject() { NameValue = "Design", Executor = GetRandomEmployee() });
            stage13.Image = StageObject.Image;
            stage13.Nodes.Add(new TreeListNode(new TaskObject() { NameValue = "Design of a web pages", Executor = GetRandomEmployee(), StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[1] }) { Image = TaskObject.Image });
            stage13.Nodes.Add(new TreeListNode(new TaskObject() { NameValue = "Pages layout", Executor = GetRandomEmployee(), StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[1] }) { Image = TaskObject.Image });
            TreeListNode stage14 = new TreeListNode(new StageObject() { NameValue = "Development", Executor = GetRandomEmployee() });
            stage14.Image = StageObject.Image;
            stage14.Nodes.Add(new TreeListNode(new TaskObject() { NameValue = "Design", Executor = GetRandomEmployee(), StartDate = new DateTime(2011, 10, 23), EndDate = new DateTime(2011, 10, 24), State = States.DataSource[1] }) { Image = TaskObject.Image });
            stage14.Nodes.Add(new TreeListNode(new TaskObject() { NameValue = "Coding", Executor = GetRandomEmployee(), StartDate = new DateTime(2011, 10, 25), EndDate = new DateTime(2011, 10, 26), State = States.DataSource[0] }) { Image = TaskObject.Image });
            TreeListNode stage15 = new TreeListNode(new StageObject() { NameValue = "Testing and Delivery", Executor = GetRandomEmployee() });
            stage15.Image = StageObject.Image;
            stage15.Nodes.Add(new TreeListNode(new TaskObject() { NameValue = "Testing", Executor = GetRandomEmployee(), StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[0] }) { Image = TaskObject.Image });
            stage15.Nodes.Add(new TreeListNode(new TaskObject() { NameValue = "Content", Executor = GetRandomEmployee(), StartDate = new DateTime(2011, 10, 13), EndDate = new DateTime(2011, 10, 14), State = States.DataSource[0] }) { Image = TaskObject.Image });

            stantoneProject.Nodes.Add(stage11);
            stantoneProject.Nodes.Add(stage12);
            stantoneProject.Nodes.Add(stage13);
            stantoneProject.Nodes.Add(stage14);
            stantoneProject.Nodes.Add(stage15);
        }
        #endregion
    }

    #region Classes
    public class State : IComparable {
        public ImageSource Image { get; set; }
        public string TextValue { get; set; }
        public int StateValue { get; set; }
        public override string ToString() {
            return TextValue;
        }

        public int CompareTo(object obj) {
            return Comparer<int>.Default.Compare(StateValue, ((State)obj).StateValue);
        }
    }
    public class TaskObject {
        public String NameValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Employee Executor { get; set; }
        public State State { get; set; }
        public static ImageSource Image {
            get {
                return ImageHelper.GetSvgImage("Object_Task");
            }
        }
    }
    public class States : List<State> {
        static List<State> src;
        public static List<State> DataSource {
            get {
                if (src == null) {
                    src = new List<State>();
                    src.Add(new State() { TextValue = "Not started", StateValue = 0, Image = ImageHelper.GetSvgImage("State_NotStarted") });
                    src.Add(new State() { TextValue = "In progress", StateValue = 1, Image = ImageHelper.GetSvgImage("State_InProgress") });
                    src.Add(new State() { TextValue = "Completed", StateValue = 2, Image = ImageHelper.GetSvgImage("State_Completed") });
                }
                return src;
            }
        }
    }
    public class ProjectObject {
        public String NameValue { get; set; }
        Employee executor;
        public Employee Executor {
            get {
                if(executor == null)
                    executor = UnboundMode.GetRandomEmployee();
                return executor;
            }
        }
        public static ImageSource Image {
            get {
                return ImageHelper.GetSvgImage("Object_Project");
            }
        }
    }

    public class StageObject {
        public String NameValue { get; set; }
        public Employee Executor { get; set; }
        public static ImageSource Image {
            get {
                return ImageHelper.GetSvgImage("Object_Stage");
            }
        }
    }

    public class GroupNameToImageConverterExtension : MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null)
                return null;
            return EmployeeCategoryImageSelector.GetImageByGroupName((string)value);
        }
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        public sealed override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
    #endregion
}
