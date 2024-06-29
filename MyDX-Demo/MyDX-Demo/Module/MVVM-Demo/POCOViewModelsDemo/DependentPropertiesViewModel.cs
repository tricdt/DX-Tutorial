using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.POCOViewModelsDemo
{
    public class DependentPropertiesViewModel
    {
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        [DependsOnProperties("FirstName", "LastName")]
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
