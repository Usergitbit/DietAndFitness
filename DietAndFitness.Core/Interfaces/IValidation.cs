using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Interfaces
{
    interface IValidation
    {
        bool IsValid();
        void ResetValues();
    }
}
