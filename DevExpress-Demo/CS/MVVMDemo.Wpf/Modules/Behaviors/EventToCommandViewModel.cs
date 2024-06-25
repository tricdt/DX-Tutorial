using System.Windows;

namespace MVVMDemo.Behaviors {
    public class EventToCommandViewModel {
        public PersonInfo[] Persons { get; private set; }

        protected EventToCommandViewModel() {
            Persons = PersonInfo.Persons;
        }

        public void ShowPersonDetail(PersonInfo person) {
            if(person != null)
                MessageBox.Show(person.FullName, "Detail info");
        }
    }
}
