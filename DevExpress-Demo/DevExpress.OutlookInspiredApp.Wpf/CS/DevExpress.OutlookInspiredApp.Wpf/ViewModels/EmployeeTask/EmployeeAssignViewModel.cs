using System.Collections.Generic;
using System.Linq;
using DevExpress.DevAV.Common.ViewModel;
using DevExpress.DevAV.DevAVDbDataModel;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.DevAV.Common;
using DevExpress.Mvvm.DataModel;

namespace DevExpress.DevAV.ViewModels {
    public class EmployeeAssignViewModel : SingleObjectViewModel<EmployeeTask, long, IDevAVDbUnitOfWork> {
        public static EmployeeAssignViewModel Create(IUnitOfWorkFactory<IDevAVDbUnitOfWork> unitOfWorkFactory = null) {
            return ViewModelSource.Create(() => new EmployeeAssignViewModel(unitOfWorkFactory));
        }
        protected EmployeeAssignViewModel(IUnitOfWorkFactory<IDevAVDbUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Tasks, x => x.Subject) {
        }

        public virtual IEnumerable<Employee> EntityContextLookUpEmployees { get; set; }
        bool allowEntityChange;

        protected override void OnEntityChanged() {
            base.OnEntityChanged();
            allowEntityChange = false;
            EntityContextLookUpEmployees = this.UnitOfWork.Employees.AsEnumerable();
            allowEntityChange = true;
        }
        public virtual void AssignedEmployeesChanged() {
            if(allowEntityChange) {
                Entity.AttachedCollectionsChanged = true;
                Update();
                Entity.AttachedCollectionsChanged = false;
            }
        }
        protected override string GetTitle() {
            return "Assign task";
        }
        protected override bool TryClose() {
            return true;
        }
        [Command(CanExecuteMethodName = "CanSave")]
        public virtual void SaveEntity() {
            if(SaveCore())
                Close();
            if(DocumentOwner != null)
                DocumentOwner.Close(this);
        }
    }
}
