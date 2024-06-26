﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.DXBindingDemo
{
    public class RegistrationHelper
    {
        public static bool CanRegister(string userName, bool acceptTerms)
        {
            return !string.IsNullOrEmpty(userName) && acceptTerms;
        }
    }
}