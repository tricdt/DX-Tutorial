using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace MVVMDemo.Services.Navigation {
    public class CollectionViewModel {
        INavigationService NavigationService { get { return this.GetRequiredService<INavigationService>(); } }

        public void ShowDetail(PersonInfo person) {
            if(person == null)
                return;
            NavigationService.Navigate(viewType: "NavigationDetailView", param: person, parentViewModel: this);
        }
    }
}
