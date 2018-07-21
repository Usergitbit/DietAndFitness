using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DietAndFitness.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Method that returns a property name for an instance of an object
        /// </summary>
        /// <typeparam name="T">The object's type</typeparam>
        /// <typeparam name="TReturn">The property's type</typeparam>
        /// <param name="obj">The object</param>
        /// <param name="property">The property</param>
        /// <returns></returns>
        public static string GetPropertyName<T, TReturn>(this T obj, Expression<Func<T,TReturn>> property) where T : class
        {
            MemberExpression body = (MemberExpression)property.Body;
            return body.Member.Name;
        }
    }
}
