using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Entities
{
    interface IValidation
    {
        bool IsValid();
        void ResetValues();
    }
}
