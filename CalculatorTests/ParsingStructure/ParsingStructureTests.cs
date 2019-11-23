using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorModel.ParsingStructures;
using Xunit;

namespace CalculatorTests.ParsingStructure
{
    /// <summary>
    /// ParsingStructure Tests
    /// </summary>
    public class ParsingStructureTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void EvaluateExpression_twoOperands()
        {
            double expected = 3;
            CalculatorModel.ParsingStructures.ParsingStructure p = new CalculatorModel.ParsingStructures.ParsingStructure();
            double result = p.EvaluateExpression("1+2");
            Assert.Equal(expected, result);
        }
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void EvaluateExpression_multipleOperands()
        {
            double expected = 1;
            CalculatorModel.ParsingStructures.ParsingStructure p = new CalculatorModel.ParsingStructures.ParsingStructure();
            double result = p.EvaluateExpression("1+2-9+2+5");
            Assert.Equal(expected, result);
        }
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void EvaluateExpression_multipleOperands_multiplicationDivision()
        {
            double expected = 18.6;
            CalculatorModel.ParsingStructures.ParsingStructure p = new CalculatorModel.ParsingStructures.ParsingStructure();
            double result = p.EvaluateExpression("1+2*9-2/5");
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void EvaluateExpression_multipleOperands_multiplicationDivision2()
        {
            double expected = 17;
            CalculatorModel.ParsingStructures.ParsingStructure p = new CalculatorModel.ParsingStructures.ParsingStructure();
            double result = p.EvaluateExpression("1+2*9-2");
            Assert.Equal(expected, result);
        }


    }
}
