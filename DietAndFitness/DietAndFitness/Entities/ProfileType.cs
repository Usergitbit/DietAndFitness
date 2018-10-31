using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Entities
{
    [Table("ProfileTypes")]
    public class ProfileType : DatabaseEntity
    {
        public ProfileType()
        {

        }
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
