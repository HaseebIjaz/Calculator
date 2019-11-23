using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using CalculatorModel.ExtensionMethods;


namespace CalculatorModel.ParsingStructures
{
    /// <summary>
    /// The ParsingStructure class/data structure atempts to parse the arithmetic expression , later on computes and returns the results
    /// </summary>
    public class ParsingStructure
    {

        Stack<double> valuestack;
        Stack<char> operatorStack;

        #region Constructor
        /// <summary>
        /// Public Constructor
        /// </summary>
        public ParsingStructure()
        {
            valuestack = new Stack<double>();
            operatorStack = new Stack<char>();

        }

        #endregion

        #region Private Methods
        ///
        ///Checks Wether gven character is number or operator 
        ///
        public static bool IsNumber(char c)
        {
            switch (c)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '.':
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Performs +,-,*,/ operation on num1 and numw operands
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        private static double PerformOperation(double num1, double num2, char operation)
        {
            switch (operation)
            {
                case '+':
                    return num1 + num2;
                case '-':
                    return num1 - num2;
                case '*':
                    return num1 * num2;
                case '/':
                    return (num1) / num2;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Determines wether percedence of the incoming operator is higher than the previously pushed
        /// </summary>
        /// <param name="incoming"></param>
        /// <returns></returns>
        private bool IsIncomingOperatorPercedenceHigh(char incoming)
        {
            char peek = operatorStack.Peek();
            if ((peek == '*' || peek == '/') && (incoming == '+' || incoming == '-'))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Performs Arithmetic(+,-,*,/) operation of previously pushed operator using the "incoming" number and a number previously pushed on the stack 
        /// </summary>
        /// <param name="incoming"></param>
        /// <returns></returns>
        private double StackOperation(double incoming)
        {
            double num1 = valuestack.Pop();
            double num2 = incoming;
            char operatr = operatorStack.Pop();
            return PerformOperation(num1, num2, operatr);

        }


        /// <summary>
        /// Appends newly parsed number and charachter to the stacks and calls StackOperation function if the new operator to be appended has higher percedence than the previously pushed
        /// </summary>
        /// <param name="num"></param>
        /// <param name="operation"></param>

        private void AppendStackOperation(double num, char operation)
        {


            if (valuestack.Count == 0)
            {
                valuestack.Push(num);
                operatorStack.Push(operation);
                return;
            }

            bool isTophighPercedence = IsIncomingOperatorPercedenceHigh(operation);

            if (!isTophighPercedence)
            {
                valuestack.Push(num);
                operatorStack.Push(operation);

            }
            else if (isTophighPercedence)
            {
                double result = StackOperation(num);
                AppendStackOperation(result, operation);
            }
        }

        /// <summary>
        /// Performs a single stack operation(pop 2 times valueStack , pop 1 time operatorStack and perform operation , later push the result to the valueStack) to compute 
        /// </summary>
        private void ComputingStackExpression()
        {
            double num1 = valuestack.Pop();
            double num2 = valuestack.Pop();
            char operatr = operatorStack.Pop();
            valuestack.Push(PerformOperation(num1, num2, operatr));
        }

        /// <summary>
        /// Reverse both stacks for left to right readability
        /// </summary>
        private void ReverseBothStacksForLeftToRightAssociativity()
        {
            operatorStack = operatorStack.Reverse();
            valuestack = valuestack.Reverse();
        }

        /// <summary>
        /// After the value stack and the operator stack have been populated by the "ParseExpression function" this function attempts to calculate the final number as if we are reading an expression from left to right and performing the stack operation at the same time
        /// </summary>
        private double ComputeExpression()
        {
            double to_return = valuestack.Peek();
            if (valuestack.Count == 1)
            {
                valuestack.Pop();
                return to_return;
            }
            ComputingStackExpression();

            return ComputeExpression();
        }

        /// <summary>
        /// After the stacks have been populated this function checks for the last stack append to be having an operator of higher percedence in which case the stack operation will be immediately performed
        /// </summary>
        /// <param name="num"></param>
        private void CheckPercedenceAtEnd(double num)
        {
            double to_return = num;
            if (operatorStack.Count != 0)
            {
                if (operatorStack.Peek() == '*' || operatorStack.Peek() == '/')
                {
                    to_return = StackOperation(num);
                }
            }
;
            valuestack.Push(to_return);
        }

        /// <summary>
        /// Parses the string received from the view model class and later populates stacks to compute the resulting value for expression
        /// </summary>
        /// <param name="expression"></param>
        private void ParseExpression(string expression)
        {
            StringBuilder value_var = new StringBuilder("", 9);
            char operator_var;
            char[] expressionArr = expression.ToCharArray();
            foreach (char c in expressionArr)
            {
                if (IsNumber(c))
                {
                    value_var.Append(c);
                }
                else
                {
                    operator_var = c;
                    AppendStackOperation(Double.Parse(value_var.ToString(), CultureInfo.InvariantCulture), operator_var);
                    value_var = new StringBuilder("", 9);
                }
            }
            CheckPercedenceAtEnd(Double.Parse(value_var.ToString(), CultureInfo.InvariantCulture));

        }

        #endregion

        #region Public Methods 

        /// <summary>
        /// This is the function which is meant to be called and used by the ViewModel Class to calculate the final or resulting value of the expression that comes after performing operations using the Percedence rules and Left to right associativity rules
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public double EvaluateExpression(string expression)
        {
            ParseExpression(expression);
            ReverseBothStacksForLeftToRightAssociativity();
            double result = ComputeExpression();
            return result;
        }

        #endregion

    }
}
