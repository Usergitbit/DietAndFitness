using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// Base ViewModel class that the rest derive from
/// </summary>
namespace DietAndFitness.Core
{
    [Serializable]
    public class ViewModelBase : ModelBase
    {
        public ViewModelBase()
        {
            //required for serialization
        }

    }
}
