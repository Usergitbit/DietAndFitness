using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Models
{
    interface IValidation
    {
        bool IsValid();
        void ResetValues();
    }
}
