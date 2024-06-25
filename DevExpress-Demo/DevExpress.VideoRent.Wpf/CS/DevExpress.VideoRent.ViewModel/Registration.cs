using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class Registration {
        public static void Register() {
            ModulesManager.Current.RegisterModuleObjectDetailType(typeof(ArtistDetailObject), typeof(ArtistDetail));
            ModulesManager.Current.RegisterModuleObjectDetailType(typeof(ArtistsListObject), typeof(ArtistsList));
            ModulesManager.Current.RegisterModuleObjectDetailType(typeof(CompaniesListObject), typeof(CompaniesList));
            ModulesManager.Current.RegisterModuleObjectDetailType(typeof(CompanyDetailObject), typeof(CompanyDetail));
            ModulesManager.Current.RegisterModuleObjectDetailType(typeof(CustomersListObject), typeof(CustomersList));
            ModulesManager.Current.RegisterModuleObjectDetailType(typeof(CustomerDetailObject), typeof(CustomerDetail));
            ModulesManager.Current.RegisterModuleObjectDetailType(typeof(CurrentCustomerRentsDetailObject), typeof(CurrentCustomerRentsDetail));
            ModulesManager.Current.RegisterModuleObjectDetailType(typeof(FindCustomerDetailObject), typeof(FindCustomerDetail));
            ModulesManager.Current.RegisterModuleObjectDetailType(typeof(MovieDetailObject), typeof(MovieDetail));
            ModulesManager.Current.RegisterModuleObjectDetailType(typeof(MoviesListObject), typeof(MoviesList));
            ModulesManager.Current.RegisterModuleObjectDetailType(typeof(MovieCategoryDetailObject), typeof(MovieCategoryDetail));
            ModulesManager.Current.RegisterModuleObjectDetailType(typeof(MovieCategoriesListObject), typeof(MovieCategoriesList));
        }
    }
}
