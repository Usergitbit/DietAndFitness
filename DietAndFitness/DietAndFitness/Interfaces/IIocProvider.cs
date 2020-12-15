using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Interfaces
{
    public interface IIocProvider
    {
        SimpleIoc Ioc { get; }
    }
}
