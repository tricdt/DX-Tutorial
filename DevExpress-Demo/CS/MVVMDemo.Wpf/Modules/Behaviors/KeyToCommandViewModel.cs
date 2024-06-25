using System.Windows;

namespace MVVMDemo.Behaviors {
    public class KeyToCommandViewModel {
        public PersonInfo[] Persons { get; private set; }

        protected KeyToCommandViewModel() {
            Persons = PersonInfo.Persons;
        }

        public void ShowPersonDetail(PersonInfo person) {
            if(person != null)
                MessageBox.Show(person.FullName, "Detail info");
        }
    }
}
