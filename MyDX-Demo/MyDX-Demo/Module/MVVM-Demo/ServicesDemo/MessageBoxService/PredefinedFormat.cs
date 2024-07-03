using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ServicesDemo.MessageBoxService
{
    public class PredefinedFormat
    {
        public PredefinedFormat(string example, string format)
        {
            Example = example;
            Format = format;
        }
        public string Example { get; }
        public string Format { get; }

        override public string ToString()
        {
            return $"{Example} [{Format}]";
        }
    }
}
