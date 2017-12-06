using System;
using System.Collections.Generic;

namespace ModernRonin.Standard
{
    /// <summary>   Defines a provider of randomity with all sorts of typical operations you
    /// 			would use a random number generator for. </summary>
    public interface IRandomity
    {
        /// <summary>   Returns a random double in the range [0;1[. </summary>
        /// <returns>   . </returns>
        double Double();
        /// <summary>   Returns a random float in the range [0;1[. </summary>
        /// <returns>   . </returns>
        float Float();
        /// <summary>   Returns an random integer in the range [0;<paramref name="exclusiveMaximum"/>[. </summary>
        /// <param name="exclusiveMaximum"> The exclusive maximum. </param>
        /// <returns>   . </returns>
        int Integer(int exclusiveMaximum);
        /// <summary>   Returns a random integer in the range [<paramref name="inclusiveMinimum"/>;<paramref name="exclusiveMaximum"/>[. </summary>
        /// <param name="inclusiveMinimum"> The inclusive minimum. </param>
        /// <param name="exclusiveMaximum"> The exclusive maximum. </param>
        /// <returns>   . </returns>
        int Integer(int inclusiveMinimum, int exclusiveMaximum);
        /// <summary>   Returns a random boolean. </summary>
        /// <returns>   . </returns>
        bool Boolean();
        /// <summary>   Returns a random element of the given list. </summary>
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="list"> The list. </param>
        /// <returns>   . </returns>
        T ElementOf<T>(IList<T> list);
        /// <summary>   Returns a random enum value. </summary>
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <returns>   . </returns>
        /// <exception cref="ArgumentException">thrown if <typeparamref name="T"/> is not an enum</exception>
        T EnumValue<T>();
        /// <summary>   Returns a random enum value. </summary>
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <returns>   . </returns>
        /// <exception cref="ArgumentException">thrown if <typeparamref name="T"/> is not an enum</exception>
        T ElementOf<T>(IEnumerable<T> enumerable);
        /// <summary>   Checks whether a random value element of [0;1[ is smaller than the supplied parameter. </summary>
        /// <param name="rhs">  The limit to check against - this ought to be [0;1[. </param>
        /// <returns>   true if smaller than, false if not. </returns>
        bool IsSmallerThan(float rhs);
        /// <summary>   Checks whether a random value element of [0;1[ is smaller than the supplied parameter. </summary>
        /// <param name="rhs">  The limit to check against - this ought to be [0;1[. </param>
        /// <returns>   true if smaller than, false if not. </returns>
        bool IsSmallerThan(double rhs);
    }
}