using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Calculator.MainWindowViewModel;
using System.Windows.Input;

namespace CalculatorTests.MainWindowViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class MainWindowViewModelTests
    {
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CheckNumberButton_Five()
        {
            //Arrange
            string Expected = "5";

            //Act
            Calculator.MainWindowViewModel.MainWindowViewModel MvvmObj = new Calculator.MainWindowViewModel.MainWindowViewModel();
            ((ICommand)MvvmObj.HandleNumpadNumberButtonCommand).Execute("5");
            
            //Assert
            Assert.Equal(Expected, MvvmObj.ExpressionString);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CheckOperator_Plus()
        {
            //Arrange
            string Expected = "+";
            //Act
            Calculator.MainWindowViewModel.MainWindowViewModel MvvmObj = new Calculator.MainWindowViewModel.MainWindowViewModel();
            ((ICommand)MvvmObj.HandleOperatorPadCommand).Execute("+");
            bool result = Expected.Equals(MvvmObj.ExpressionString);
            //Assert
            Assert.True(result);
        }
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CheckEqualButton_ResultString_Empty()
        {
            //Arrange
            string Expected = "= 0";

            //Act
            Calculator.MainWindowViewModel.MainWindowViewModel MvvmObj = new Calculator.MainWindowViewModel.MainWindowViewModel();
            ((ICommand)MvvmObj.HandleNumpadEqualButtonCommand).Execute(null);
            //Assert
            Assert.Equal(Expected, MvvmObj.ResultString);
        }
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CheckBackSpace_ExpressionString_OneCharacterRemovedFromTheEnd()
        {
            //Arrange
            string Expected = "";
            //Act
            Calculator.MainWindowViewModel.MainWindowViewModel MvvmObj = new Calculator.MainWindowViewModel.MainWindowViewModel();
            MvvmObj.ExpressionString = "0";
            ((ICommand)MvvmObj.HandleBackspaceCommand).Execute(null);
            //Assert
            Assert.Equal(Expected, MvvmObj.ExpressionString);

        }

        [Fact]
        public void CheckClearButton_ResultString_OneCharacterRemovedFromTheEnd()
        {
            //Arrange
            string Expected = "";
            Calculator.MainWindowViewModel.MainWindowViewModel MvvmObj = new Calculator.MainWindowViewModel.MainWindowViewModel();
            MvvmObj.ExpressionString = "1+2";
            //Act
            ((ICommand)MvvmObj.HandleClearCommand).Execute(null);
            //Assert
            Assert.Equal(Expected,MvvmObj.ResultString);
        }

        [Fact]
        public void CheckEqualButton_ExpressionString_Empty()
        {
            //Arrange
            Calculator.MainWindowViewModel.MainWindowViewModel MvvmObj = new Calculator.MainWindowViewModel.MainWindowViewModel();
            MvvmObj.ExpressionString = "1+2";
            string Expected = "";
            //Act
            ((ICommand)MvvmObj.HandleNumpadEqualButtonCommand).Execute(null);
            //Assert
            Assert.Equal(Expected, MvvmObj.ExpressionString);
        }



        [Fact]
        public void CanHandleOperatorPadCommand_ExpressionString_OneCharacterRemovedFromTheEnd()
        {
            //Arrange
            Calculator.MainWindowViewModel.MainWindowViewModel MvvmObj = new Calculator.MainWindowViewModel.MainWindowViewModel();


            //Act
            MvvmObj.ExpressionString = "1";
            bool Result = ((ICommand)MvvmObj.HandleOperatorPadCommand).CanExecute(null);
            
            //Assert
            Assert.True(Result);
        }
        /*
        [Fact]
        public void CheckBackSpace_ExpressionString_OneCharacterRemovedFromTheEnd()
        {
            //Arrange
            //Act
            //Assert
            Assert.Equal()
        }*/
    }
}


//((ICommand)MvvmObj.HandleNumpadNumberButtonCommand).CanExecute