Imports System.ComponentModel.DataAnnotations

Namespace DevExpress.DevAV.ViewModels

    Public Enum EmployeeReportType
        None
        <Display(Name:="Profile Report")>
        Profile
        <Display(Name:="Summary Report")>
        Summary
        <Display(Name:="Employees Directory Report")>
        Directory
        <Display(Name:="Task List Report")>
        TaskList
    End Enum
End Namespace
