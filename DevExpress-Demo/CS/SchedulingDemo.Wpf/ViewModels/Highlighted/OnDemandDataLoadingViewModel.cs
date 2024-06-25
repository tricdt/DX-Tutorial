using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Scheduling;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CreateSourceObjectEventArgs = DevExpress.Xpf.Scheduling.CreateSourceObjectEventArgs;
#if DXCORE3
using Microsoft.EntityFrameworkCore;
#else
using System.Data.Entity;
#endif

namespace SchedulingDemo.ViewModels {
    public class OnDemandDataLoadingViewModel {
        const int millisecondsDelay = 2000;
        public OnDemandDataLoadingViewModel() {
            if(ViewModelBase.IsInDesignMode)
                return;
            DBInitializer.Init();
            using(var dbContext = new SchedulingContext()) {
                Resources = dbContext.ResourceEntities.ToObservableCollection();
            }
        }
        public virtual ObservableCollection<ResourceEntity> Resources { get; set; }
        public bool Delay { get; set; }

        public void ProcessChanges(AppointmentCRUDEventArgs args) {
            using(var dbContext = new SchedulingContext()) {
                dbContext.AppointmentEntities.AddRange(args.AddToSource.Select(x => (AppointmentEntity)x.SourceObject));

                var updatedAppts = args.UpdateInSource.Select(x => (AppointmentEntity)x.SourceObject);
                var updatedApptIds = updatedAppts.Select(x => x.Id).ToArray();
                var apptsToUpdate = dbContext.AppointmentEntities.Where(x => updatedApptIds.Contains(x.Id));
                foreach(var appt in updatedAppts)
                    AppointmentEntityHelper.CopyProperties(appt, apptsToUpdate.First(x => x.Id == appt.Id));

                var deletedApptIds = args.DeleteFromSource.Select(x => ((AppointmentEntity)x.SourceObject).Id).ToArray();
                var apptsToDelete = dbContext.AppointmentEntities.Where(x => deletedApptIds.Contains(x.Id)).ToArray();
                dbContext.AppointmentEntities.RemoveRange(apptsToDelete);

                dbContext.SaveChanges();
            }
        }
        public void CreateSourceObject(CreateSourceObjectEventArgs args) {
            if(args.ItemType == ItemType.AppointmentItem)
                args.Instance = new AppointmentEntity();
        }
        public void FetchAppointments(FetchDataEventArgs args) {
            args.AsyncResult = GetAppointments(args);
        }
        async Task<object[]> GetAppointments(FetchDataEventArgs args) {
            if(Delay)
                await Task.Delay(millisecondsDelay);
            using(var dbContext = new SchedulingContext()) 
                return await dbContext
                    .AppointmentEntities
                    .Where(args.GetFetchExpression<AppointmentEntity>())
                    .ToArrayAsync();
        }
    }
}
