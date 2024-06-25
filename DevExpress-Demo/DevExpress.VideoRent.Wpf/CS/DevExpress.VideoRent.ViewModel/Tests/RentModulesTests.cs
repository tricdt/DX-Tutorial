#if DebugTest
using System;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.Resources;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
#if SL
using DevExpress.Xpf.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
using DevExpress.Xpo;
#else
using TestClassAttribute = NUnit.Framework.TestFixtureAttribute;
using TestMethodAttribute = NUnit.Framework.TestAttribute;
using TestInitializeAttribute = NUnit.Framework.SetUpAttribute;
using TestCleanupAttribute = NUnit.Framework.TearDownAttribute;
using NUnit.Framework;
#endif

namespace DevExpress.VideoRent.ViewModel.Tests {
    public class RentModulesTests : ViewModelTests {
        public override void Init() {
            base.Init();
            Assert.IsTrue(LayoutManager.Current.Login(ReferenceData.AdministratorString, string.Empty, Session));
        }
        public override void ShutDown() {
            LayoutManager.Current.Logout();
            base.ShutDown();
        }
    }
    [TestClass]
    public class CustomerEditsTests : RentModulesTests {
        [TestMethod]
        public void OpenCustomerDetail_SetCurrentCustomer() {
            using(CustomersList list = (CustomersList)ModulesManager.Current.OpenModuleObjectDetail(new CustomersListObject(Session), false)) {
                list.OpenDetail(Andrew.Oid, null);
                CustomerDetailView detailView = CustomerDetailView.LastCreatedView;
                Assert.IsNotNull(detailView);
                detailView.Module.SetAsCurrentCustomer();
                detailView.Module.Dispose();
            }
            LayoutManager.Current.Logout();
            Assert.IsTrue(LayoutManager.Current.Login(ReferenceData.AdministratorString, string.Empty, Session));
            Assert.AreEqual(Andrew.Oid, ViewModelLayoutData.GetLayoutData().CurrentCustomerOid);
        }
#if !SL //TODO
        [TestMethod]
        public void ReturnAllCurrentCustomerActiveRents() {
            TestMessageBoxView.ShowDelegate = (message, title, button, image, defaultResult) =>
            {
                return MessageBoxResult.Yes;
            };
            MovieCategory defaultCategory = MovieCategory.GetDefaultCategory(Session);
            Receipt receipt = Anton.DoRent(new RentInfo[] { new RentInfo(Avatar, MovieItemFormat.DVD, 0), new RentInfo(Cube), new RentInfo(Postal, 2) });
            receipt.Date = DateTime.Now.AddDays(-1);
            SessionHelper.CommitSession(Session, null);
            using(CurrentCustomerRentsDetail list = (CurrentCustomerRentsDetail)ModulesManager.Current.OpenModuleObjectDetail(new CurrentCustomerRentsDetailObject(Session), false)) {
                CurrentCustomerProvider.Current.CurrentCustomerOid = Anton.Oid;
                Assert.IsNotNull(list.CurrentCustomerRentsEdit.CurrentCustomerActiveRents);
                Assert.AreNotEqual(0, list.CurrentCustomerRentsEdit.CurrentCustomerActiveRents.Count);
                list.CurrentCustomerRentsEdit.CheckAllActiveRents();
                list.ReturnRents();
                Assert.AreEqual(1, list.CurrentCustomerRentsEdit.CurrentCustomerActiveRents.Count);
            }
        }
#endif
        [TestMethod]
        public void CreateCustomersList() {
            using(CustomersList list = (CustomersList)ModulesManager.Current.OpenModuleObjectDetail(new CustomersListObject(Session), false)) {
                CustomersListView customersListView = CustomersListView.LastCreatedView;
                Assert.IsNotNull(customersListView);
                Assert.IsNotNull(customersListView.Module);
                Assert.AreEqual(list, customersListView.Module);
            }
        }
        [TestMethod]
        public void OpenCustomerDetail() {
            using(CustomersList list = (CustomersList)ModulesManager.Current.OpenModuleObjectDetail(new CustomersListObject(Session), false)) {
                list.OpenDetail(Andrew.Oid, null);
                CustomerDetailView detailView = CustomerDetailView.LastCreatedView;
                Assert.IsNotNull(detailView);
                Assert.AreEqual(Andrew.Oid, detailView.Module.CustomerEdit.VRObjectEditObject.VideoRentObject.Oid);
                detailView.Module.Dispose();
            }
        }
        [TestMethod]
        public void TryOpenCustomerDetailWithNullCurrentRecord() {
            using(CustomersList list = (CustomersList)ModulesManager.Current.OpenModuleObjectDetail(new CustomersListObject(Session), false)) {
                CustomerDetailView.LastCreatedView = null;
                list.CustomersEdit.CurrentRecord = null;
                list.CommandEdit(null);
                CustomerDetailView detailView = CustomerDetailView.LastCreatedView;
                Assert.IsNull(detailView);
            }
        }
        [TestMethod]
        public void CreateCurrentCustomerRentsDetail_ChangeCurrentCustomer() {
            using(CurrentCustomerRentsDetail list = (CurrentCustomerRentsDetail)ModulesManager.Current.OpenModuleObjectDetail(new CurrentCustomerRentsDetailObject(Session), false)) {
                CurrentCustomerProvider.Current.CurrentCustomerOid = Andrew.Oid;
                CurrentCustomerRentsDetailView rentsListView = CurrentCustomerRentsDetailView.LastCreatedView;
                Assert.IsNotNull(rentsListView);
                Assert.IsNotNull(rentsListView.Module);
                Assert.AreEqual(list, rentsListView.Module);
                Assert.AreEqual(Andrew.Oid, list.CurrentCustomerRentsEdit.CurrentCustomer.Oid);
                CurrentCustomerProvider.Current.CurrentCustomerOid = Alex.Oid;
                if(list.CurrentCustomerRentsEdit.CurrentCustomerActiveRents != null && list.CurrentCustomerRentsEdit.CurrentCustomerActiveRents.Count != 0) {
                    foreach(Rent item in list.CurrentCustomerRentsEdit.CurrentCustomerActiveRents) {
                        Assert.AreEqual(CurrentCustomerProvider.Current.CurrentCustomerOid, item.Customer.Oid);
                    }
                }
            }
        }
        [TestMethod]
        public void RetrieveDefaultCustomerOid() {
            ViewModelLayoutData viewModelLayoutData = ViewModelLayoutData.GetLayoutData();
            Guid oid = viewModelLayoutData.RetrieveDefaultCustomerOid();
            Assert.AreNotEqual(oid, Guid.Empty);
        }
        [TestMethod]
        public void CreateCustomer_CheckAllowSetAsCurrent() {
            using(CustomersList list = (CustomersList)ModulesManager.Current.OpenModuleObjectDetail(new CustomersListObject(Session), false)) {
                using(CustomerDetail detail = (CustomerDetail)list.OpenDetail(null, null)) {
                    detail.CustomerEdit.VRObjectEditObject.VideoRentObject.FirstName = "x";
                    detail.CustomerEdit.VRObjectEditObject.VideoRentObject.LastName = "y";
                    Assert.IsFalse(detail.AllowSetAsCurrentCustomer);
                    Assert.IsFalse(detail.SetAsCurrentCustomer());
                    Assert.IsTrue(detail.Save());
                    Assert.IsTrue(detail.AllowSetAsCurrentCustomer);
                    Assert.IsTrue(detail.SetAsCurrentCustomer());
                }
            }
        }
        [TestMethod]
        public void CreateCustomer_SetAsCurrent_Delete_OpenCurrentCustomerDetail() {
            using(CustomersList list = (CustomersList)ModulesManager.Current.OpenModuleObjectDetail(new CustomersListObject(Session), false)) {
                using(CustomerDetail detail = (CustomerDetail)list.OpenDetail(null, null)) {
                    detail.CustomerEdit.VRObjectEditObject.VideoRentObject.FirstName = "x";
                    detail.CustomerEdit.VRObjectEditObject.VideoRentObject.LastName = "y";
                    Assert.IsTrue(detail.Save());
                    Assert.IsTrue(detail.SetAsCurrentCustomer());
                    Assert.IsTrue(detail.Close());
                }
                Assert.IsNotNull(CurrentCustomerProvider.Current.CurrentCustomer);
                Customer currentCustomer = SessionHelper.GetObject<Customer>(CurrentCustomerProvider.Current.CurrentCustomer, list.CustomersEdit.VRObjectsEditObject.VideoRentObjects.Session);
                Assert.AreEqual("x", currentCustomer.FirstName);
                Assert.AreEqual("y", currentCustomer.LastName);
                list.CustomersEdit.CurrentRecord = currentCustomer;
                Assert.IsTrue(list.CustomersEdit.DeleteCurrentRecord());
                Assert.IsNull(CurrentCustomerProvider.Current.CurrentCustomerOid);
            }
        }
        [TestMethod]
        public void OpenCurrentCustomerDetail_CheckAllowSetAsCurrentCustomer() {
            CurrentCustomerProvider.Current.CurrentCustomerOid = Alex.Oid;
            using(CustomerDetail detail = (CustomerDetail)ModulesManager.Current.OpenModuleObjectDetail(new CustomerDetailObject(Session, CurrentCustomerProvider.Current.CurrentCustomerOid), false)) {
                Assert.IsFalse(detail.AllowSetAsCurrentCustomer);
            }
        }
        [TestMethod]
        public void OpenCustomerDetail_Change_CheckAllowSetAsCurrentCustomer() {
            CurrentCustomerProvider.Current.CurrentCustomerOid = Andrew.Oid;
            using(CustomerDetail detail = (CustomerDetail)ModulesManager.Current.OpenModuleObjectDetail(new CustomerDetailObject(Session, Alex.Oid), false)) {
                Assert.IsTrue(detail.AllowSetAsCurrentCustomer);
                detail.CustomerEdit.VRObjectEditObject.VideoRentObject.Address = "changed address";
                Assert.IsTrue(detail.AllowSetAsCurrentCustomer);
            }
        }
        [TestMethod]
        public void CreateCustomerDetailsWithDifferentTags_CloseEachGroup() {
            CurrentCustomerProvider.Current.CurrentCustomerOid = Alex.Oid;
            using(CustomersList list = (CustomersList)ModulesManager.Current.OpenModuleObjectDetail(new CustomersListObject(Session), false)) {
                using(CurrentCustomerRentsDetail detail = (CurrentCustomerRentsDetail)ModulesManager.Current.OpenModuleObjectDetail(new CurrentCustomerRentsDetailObject(Session), false)) {
                    list.ListEdit.CurrentRecord = Anton;
                    list.CommandEdit("Anton");
                    CustomerDetail currentCustomerDetail = (CustomerDetail)detail.OpenDetail(Alex.Oid, "Alex");
                    list.CommandCloseDetails(null);
                    Assert.AreNotEqual(0, ModulesManager.Current.GetModulesForType(currentCustomerDetail.GetModuleTypeKey()).Count);
                }
            }
        }
        [TestMethod]
        public void CreateCustomer_Save_CheckTitle() {
            using(CustomerDetail detail = (CustomerDetail)ModulesManager.Current.OpenModuleObjectDetail(new CustomerDetailObject(Session, null), false)) {
                detail.CustomerEdit.VRObjectEditObject.VideoRentObject.FirstName = "x";
                detail.CustomerEdit.VRObjectEditObject.VideoRentObject.LastName = "y";
                Assert.AreEqual(ConstStrings.Get("NewCustomer") + " *", detail.Title);
                Assert.IsTrue(detail.Save());
                Assert.AreEqual("x y", detail.Title);
            }
        }
    }
    [TestClass]
    public class CurrentCustomerRentsEditsTests : RentModulesTests {
        public override void Init() {
            base.Init();
            CurrentCustomerProvider.Current.CurrentCustomerOid = Andrew.Oid;
        }
        [TestMethod]
        public void CreateCurrentCustomerRentsDetail() {
            using(CurrentCustomerRentsDetail detail = (CurrentCustomerRentsDetail)ModulesManager.Current.OpenModuleObjectDetail(new CurrentCustomerRentsDetailObject(Session), false)) {
                CurrentCustomerRentsDetailView view = CurrentCustomerRentsDetailView.LastCreatedView;
                Assert.IsNotNull(view);
                Assert.IsNotNull(view.Module);
                Assert.AreEqual(detail, view.Module);
            }
        }
        [TestMethod]
        public void ChangePeriod() {
            using(CurrentCustomerRentsDetail detail = (CurrentCustomerRentsDetail)ModulesManager.Current.OpenModuleObjectDetail(new CurrentCustomerRentsDetailObject(Session), false)) {
                DateTime startDate = detail.CurrentCustomerRentsEdit.StartDate;
                DateTime endDate = detail.CurrentCustomerRentsEdit.EndDate;
                int period = detail.CurrentCustomerRentsEdit.Period;
                Assert.AreEqual(endDate, startDate.AddMonths(period));
                detail.CurrentCustomerRentsEdit.EndDate = endDate.AddMonths(2);
                Assert.AreNotEqual(period + 2, detail.CurrentCustomerRentsEdit.Period);
                detail.CurrentCustomerRentsEdit.Period = period;
                startDate = detail.CurrentCustomerRentsEdit.StartDate;
                endDate = detail.CurrentCustomerRentsEdit.EndDate;
                period = detail.CurrentCustomerRentsEdit.Period;
                Assert.AreEqual(endDate, startDate.AddMonths(period));
            }
        }
    }
}
#endif
