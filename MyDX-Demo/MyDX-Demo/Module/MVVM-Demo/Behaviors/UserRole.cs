using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.Behaviors
{
    public enum UserRole
    {
        [Image("pack://application:,,,/MVVMDemo;component/Modules/Behaviors/Images/Admin.svg")]
        [Display(Name = "Admin", Description = "High level of access")]
        Administrator,

        [Image("pack://application:,,,/MVVMDemo;component/Modules/Behaviors/Images/Moderator.svg")]
        [Display(Description = "Average level of access")]
        Moderator,

        [Image("pack://application:,,,/MVVMDemo;component/Modules/Behaviors/Images/User.svg")]
        [Display(Description = "Low level of access")]
        User
    }
}
