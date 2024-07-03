using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using MyDX_Demo.Module.MVVM_Demo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ServicesDemo.NavigationService
{
    public class CollectionViewModel
    {
        INavigationService NavigationService { get { return this.GetRequiredService<INavigationService>(); } }

        public void ShowDetail(PersonInfo person)
        {
            if (person == null)
                return;
            NavigationService.Navigate(viewType: "NavigationDetailView", param: person, parentViewModel: this);
        }
    }
}
