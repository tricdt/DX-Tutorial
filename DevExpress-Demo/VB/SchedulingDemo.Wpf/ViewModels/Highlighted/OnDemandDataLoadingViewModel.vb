Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.Native
Imports DevExpress.Xpf.Scheduling
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Threading.Tasks
Imports CreateSourceObjectEventArgs = DevExpress.Xpf.Scheduling.CreateSourceObjectEventArgs
#If DXCORE3
using Microsoft.EntityFrameworkCore;
#Else
Imports System.Data.Entity

#End If
Namespace SchedulingDemo.ViewModels

    Public Class OnDemandDataLoadingViewModel

        Const millisecondsDelay As Integer = 2000

        Public Sub New()
            If ViewModelBase.IsInDesignMode Then Return
            DBInitializer.Init()
            Using dbContext = New SchedulingContext()
                Resources = dbContext.ResourceEntities.ToObservableCollection()
            End Using
        End Sub

        Public Overridable Property Resources As ObservableCollection(Of ResourceEntity)

        Public Property Delay As Boolean

        Public Sub ProcessChanges(ByVal args As AppointmentCRUDEventArgs)
            Using dbContext = New SchedulingContext()
                dbContext.AppointmentEntities.AddRange(args.AddToSource.[Select](Function(x) CType(x.SourceObject, AppointmentEntity)))
                Dim updatedAppts = args.UpdateInSource.[Select](Function(x) CType(x.SourceObject, AppointmentEntity))
                Dim updatedApptIds = updatedAppts.[Select](Function(x) x.Id).ToArray()
                Dim apptsToUpdate = dbContext.AppointmentEntities.Where(Function(x) updatedApptIds.Contains(x.Id))
                For Each appt In updatedAppts
                    AppointmentEntityHelper.CopyProperties(appt, apptsToUpdate.First(Function(x) x.Id = appt.Id))
                Next

                Dim deletedApptIds = args.DeleteFromSource.[Select](Function(x) CType(x.SourceObject, AppointmentEntity).Id).ToArray()
                Dim apptsToDelete = dbContext.AppointmentEntities.Where(Function(x) deletedApptIds.Contains(x.Id)).ToArray()
                dbContext.AppointmentEntities.RemoveRange(apptsToDelete)
                dbContext.SaveChanges()
            End Using
        End Sub

        Public Sub CreateSourceObject(ByVal args As CreateSourceObjectEventArgs)
            If args.ItemType = ItemType.AppointmentItem Then args.Instance = New AppointmentEntity()
        End Sub

        Public Sub FetchAppointments(ByVal args As FetchDataEventArgs)
            args.AsyncResult = GetAppointments(args)
        End Sub

        Private Async Function GetAppointments(ByVal args As FetchDataEventArgs) As Task(Of Object())
            If Delay Then Await Task.Delay(millisecondsDelay)
            Using dbContext = New SchedulingContext()
                Return Await dbContext.AppointmentEntities.Where(args.GetFetchExpression(Of AppointmentEntity)()).ToArrayAsync()
            End Using
        End Function
    End Class
End Namespace
