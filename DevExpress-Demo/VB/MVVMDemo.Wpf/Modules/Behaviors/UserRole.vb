Imports DevExpress.Mvvm.DataAnnotations
Imports System.ComponentModel.DataAnnotations

Namespace MVVMDemo.Behaviors

    Public Enum UserRole
        <Image("pack://application:,,,/MVVMDemo;component/Modules/Behaviors/Images/Admin.svg")>
        <Display(Name:="Admin", Description:="High level of access")>
        Administrator
        <Image("pack://application:,,,/MVVMDemo;component/Modules/Behaviors/Images/Moderator.svg")>
        <Display(Description:="Average level of access")>
        Moderator
        <Image("pack://application:,,,/MVVMDemo;component/Modules/Behaviors/Images/User.svg")>
        <Display(Description:="Low level of access")>
        User
    End Enum
End Namespace
