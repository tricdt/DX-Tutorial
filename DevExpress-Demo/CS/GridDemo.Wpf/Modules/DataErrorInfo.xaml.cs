using System.Collections.Generic;

namespace GridDemo {
    
    
    
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/DataErrorInfoClasses.(cs)")]
    public partial class DataErrorInfo : GridDemoModule {
        public DataErrorInfo() {
            InitializeComponent();
            List<PersonInfo> persons = new List<PersonInfo>();
            persons.Add(new PersonInfo("John", "", "123 Home Lane, Homesville", "(555)956-15-47", "none"));
            persons.Add(new PersonInfo("Henry", "McAllister", "436 1st Ave, Cleveland", "(555)941-24-32", "@hotbox.com"));
            persons.Add(new PersonInfo("Frank", "Frankson", "349 Graphic Design L, Newman", "(555)155-05-02", "none"));
            persons.Add(new PersonInfo("Freddy", "Krueger", "Elm Street", "", "none"));
            persons.Add(new PersonInfo("Leticia", "Ford", "93900 Carter Lane, Cartersville", "(555)776-15-66", "none"));
            persons.Add(new PersonInfo("Karen", "Holmes", "", "(555)342-25-74", "none"));
            persons.Add(new PersonInfo("Roger", "Michelson", "3920 Michelson Dr., Bridgeford", "(555)954-51-88", "none"));
            grid.ItemsSource = persons;
        }
    }    
}
