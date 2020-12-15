using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Interfaces
{
    public interface IRepository<TEntity, TKey> : ICrud<TEntity, TKey>
    {

    }
}
