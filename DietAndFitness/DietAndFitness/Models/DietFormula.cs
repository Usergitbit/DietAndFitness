using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Models
{
    [Table("DietFormulas")]
    public class DietFormula : DatabaseEntity
    {
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        public override void ResetValues()
        {
            throw new NotImplementedException();
        }
    }
}
