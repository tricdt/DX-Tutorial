Imports System.Collections.Generic
Imports System.Diagnostics
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels

    Public Class EmployeeContactsViewModel

        Public Shared Function Create() As EmployeeContactsViewModel
            Return ViewModelSource.Create(Function() New EmployeeContactsViewModel())
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property Entity As Employee

        Protected ReadOnly Property MessageBoxService As IMessageBoxService
            Get
                Return GetRequiredService(Of IMessageBoxService)()
            End Get
        End Property

        Public Overridable Property EntityTasks As List(Of EmployeeTask)

        Public Sub Message()
            MessageBoxService.ShowMessage("Send an IM to: " & Entity.Skype)
        End Sub

        Public Function CanMessage() As Boolean
            Return Entity IsNot Nothing AndAlso Not String.IsNullOrEmpty(Entity.Skype)
        End Function

        Public Sub Phone()
            MessageBoxService.ShowMessage("Phone Call: " & Entity.MobilePhone)
        End Sub

        Public Function CanPhone() As Boolean
            Return Entity IsNot Nothing AndAlso Not String.IsNullOrEmpty(Entity.MobilePhone)
        End Function

        Public Sub HomeCall()
            MessageBoxService.ShowMessage("Home Call: " & Entity.HomePhone)
        End Sub

        Public Function CanHomeCall() As Boolean
            Return Entity IsNot Nothing AndAlso Not String.IsNullOrEmpty(Entity.HomePhone)
        End Function

        Public Sub MobileCall()
            MessageBoxService.ShowMessage("Mobile Call: " & Entity.MobilePhone)
        End Sub

        Public Function CanMobileCall() As Boolean
            Return Entity IsNot Nothing AndAlso Not String.IsNullOrEmpty(Entity.MobilePhone)
        End Function

        Public Sub [Call]()
            MessageBoxService.ShowMessage("Call: " & Entity.Skype)
        End Sub

        Public Function CanCall() As Boolean
            Return Entity IsNot Nothing AndAlso Not String.IsNullOrEmpty(Entity.Skype)
        End Function

        Public Sub VideoCall()
            MessageBoxService.ShowMessage("Video Call: " & Entity.Skype)
        End Sub

        Public Function CanVideoCall() As Boolean
            Return Entity IsNot Nothing AndAlso Not String.IsNullOrEmpty(Entity.Skype)
        End Function

        Public Sub MailTo()
            ExecuteMailTo(MessageBoxService, Entity.Email)
        End Sub

        Public Function CanMailTo() As Boolean
            Return Entity IsNot Nothing AndAlso Not String.IsNullOrEmpty(Entity.Email)
        End Function

        Protected Overridable Sub OnEntityChanged()
            RaiseCanExecuteChanged(Sub(x) x.Message())
            RaiseCanExecuteChanged(Sub(x) x.Phone())
            RaiseCanExecuteChanged(Sub(x) x.MobileCall())
            RaiseCanExecuteChanged(Sub(x) x.HomeCall())
            RaiseCanExecuteChanged(Sub(x) x.Call())
            RaiseCanExecuteChanged(Sub(x) x.VideoCall())
            RaiseCanExecuteChanged(Sub(x) x.MailTo())
            If Entity Is Nothing Then
                EntityTasks = Nothing
                Return
            End If

#If DXCORE3
            Entity.AssignedTasks;
#Else
            EntityTasks = Entity.AssignedEmployeeTasks
#End If
        End Sub

        Public Shared Sub ExecuteMailTo(ByVal messageBoxService As IMessageBoxService, ByVal email As String)
            Try
                Call Process.Start("mailto://" & email)
            Catch
                If messageBoxService IsNot Nothing Then messageBoxService.ShowMessage("Mail To: " & email)
            End Try
        End Sub
    End Class
End Namespace
