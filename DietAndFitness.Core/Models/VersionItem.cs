using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Core.Models
{
    public class VersionItem : DatabaseEntity
    {
        public int Number { get; set; }

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
