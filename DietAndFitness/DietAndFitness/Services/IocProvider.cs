using DietAndFitness.Interfaces;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Services
{
    public class IocProvider : IIocProvider
    {
        public SimpleIoc Ioc => App.Ioc;
    }
}
