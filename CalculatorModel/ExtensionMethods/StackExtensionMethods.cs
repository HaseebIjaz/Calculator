using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorModel.ExtensionMethods
{
    /// <summary>
    ///The Purpose of this class is to add
    ///some extension Methods to the Stack class
    /// </summary>
    public static class StackExtensionMethods
    {

        #region Public Static Methods

        /// <summary>
        /// Reverses the Stack , that is by reversing the stack it reverses the order of elements in the stack
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>


        public static Stack<T> Reverse<T>(this Stack<T> input)
        {
            //Declare another stack to store the values from the passed stack
            Stack<T> temp = new Stack<T>();

            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            //While the passed stack isn't empty, pop elements from the passed stack onto the temp stack
            while (input.Count != 0)
                temp.Push(input.Pop());

            return temp;
        }

        #endregion

    }
}
