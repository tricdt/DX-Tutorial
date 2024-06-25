using System.Collections.Generic;
using DevExpress.Xpf.DemoBase;

namespace EditorsDemo {

    [CodeFile("ModuleResources/LookUpEditTemplates.xaml")]
    public partial class TokenLookUpModule : EditorsDemoModule {
        public TokenLookUpModule() {
            InitializeComponent();            
        }
    }

    public class TokenLookUpViewModel : EditorsViewModelBase {

        public TokenLookUpViewModel() : base() {
            SelectedEmployees = CreateSelectedEmployees();
            MultiLineSelectedEmployees = CreateMultiLineSelectedEmployees();
        }
        
        public object SelectedEmployees { get; private set; }
        public object MultiLineSelectedEmployees { get; private set; }

        List<object> CreateMultiLineSelectedEmployees() {
            return new List<object>() {
                Employees[0],
                Employees[1],
                Employees[12],
                Employees[5],
                Employees[7],
                Employees[3],
                Employees[10],
                Employees[15],
                Employees[21],
                Employees[25],
                Employees[29],
                Employees[30],
                Employees[40],
                Employees[22],
                Employees[54],
                Employees[20],
                Employees[31],
                Employees[37],
                Employees[43],
                Employees[49],
                Employees[4],
                Employees[6],
                Employees[11],
                Employees[33],
                Employees[32],
                Employees[19],
                Employees[14],
                Employees[23],
                Employees[27],
                Employees[38],
            };
        }

        List<object> CreateSelectedEmployees() {
            return new List<object>() { 
                Employees[7],
                Employees[3],
                Employees[10]
            };
        }
    }
}
