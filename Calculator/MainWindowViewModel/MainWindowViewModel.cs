using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.ViewModelBase;
using Calculator.RelayCommand;
using CalculatorModel.ParsingStructures;
using System.Globalization;


namespace Calculator.MainWindowViewModel
{
    /// <summary>
    /// The MainWindowViewModel class actually exposes its public properties to the calculator
    /// </summary>
    public class MainWindowViewModel : Calculator.ViewModelBase.ViewModelBase
    {
        /// <summary>
        /// Model Class Object Parsing Structure
        /// </summary>
        private ParsingStructure p;

        #region Constructor
        /// <summary>
        /// Public Constructor
        /// </summary>
        public MainWindowViewModel()
        {
            p = new ParsingStructure();
            _ExpressionString = new StringBuilder("", 50);
            _ResultString = "";
            _HandleNumpadNumberButtonCommand = new RelayCommand<string>(OnHandleNumPadNumberButtonCommand, CanHandleNumPadNumberButtonCommand);
            _HandleOperatorPadCommand = new RelayCommand<string>(OnHandleOperatorPadCommand, CanHandleOperatorPadCommand);
            _HandleNumberPadEqualButtonCommand = new Calculator.RelayCommand.RelayCommand(OnHandleNumpadEqualButtonCommand, CanHandleNumpadEqualButtonCommand);
            _HandleClearCommand = new Calculator.RelayCommand.RelayCommand(OnHandleClearCommand, CanHandleClearCommand);
            _HandleBackspaceCommand = new Calculator.RelayCommand.RelayCommand(OnHandleBackSpaceCommand, CanHandleBackSpaceCommand);
            canAppendOperator = false;
        }

        #endregion

        #region Private Data Members and Public Properties

        private bool canAppendOperator;

        private string _ResultString;
        /// <summary>
        /// Result of calculated Expression
        /// </summary>
        public string ResultString
        {
            get { return _ResultString; }
            private set
            {
                _ResultString = value;
                OnPropertyChanged("ResultString");
            }
        }

        private StringBuilder _ExpressionString;

        /// <summary>
        /// Expression String to be bound with textbox
        /// </summary>
        public string ExpressionString
        {
            get
            {
                //    Thread.Sleep(5000);
                return _ExpressionString.ToString();
            }

             set
            {
                _ExpressionString.Append(value);
                OnPropertyChanged("ExpressionString");
                _HandleBackspaceCommand.RaiseCanExecuteChanged();
                _HandleClearCommand.RaiseCanExecuteChanged();
                if(ParsingStructure.IsNumber(_ExpressionString[0]))
                {

                }
            }
        }



        private Calculator.RelayCommand.RelayCommand _HandleClearCommand;
        /// <summary>
        /// 
        /// </summary>
        public Calculator.RelayCommand.RelayCommand HandleClearCommand
        {
            get { return _HandleClearCommand; }
        }

        private Calculator.RelayCommand.RelayCommand _HandleBackspaceCommand;
        /// <summary>
        /// 
        /// </summary>
        public Calculator.RelayCommand.RelayCommand HandleBackspaceCommand
        {
            get { return _HandleBackspaceCommand; }
        }



        /// <summary>
        /// Private Data Member corresponding to HandleNumPadNumberButton property
        /// </summary>
        private readonly RelayCommand<string> _HandleNumpadNumberButtonCommand;

        /// <summary>
        /// Command Property for responding to the Number button on Calculator
        /// </summary>
        public RelayCommand<string> HandleNumpadNumberButtonCommand
        {
            get { return _HandleNumpadNumberButtonCommand; }

        }


        /// <summary>
        /// 
        /// </summary>
        private readonly RelayCommand<string> _HandleOperatorPadCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand<string> HandleOperatorPadCommand
        {
            get { return _HandleOperatorPadCommand; }
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly Calculator.RelayCommand.RelayCommand _HandleNumberPadEqualButtonCommand;
        /// <summary>
        /// 
        /// </summary>
        public Calculator.RelayCommand.RelayCommand HandleNumpadEqualButtonCommand
        {
            get { return _HandleNumberPadEqualButtonCommand; }
        }

        #endregion

        #region  Private Methods 
        /// <summary>
        ///  Contains Logic for handling a number pressed(0,1,2,3,4,5,6,7,8,9
        /// </summary>
        /// <param name="str"></param>
        private void OnHandleNumPadNumberButtonCommand(string str)
        {
            ExpressionString = str;
            canAppendOperator = true;
            _HandleOperatorPadCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Contains Validation Logic
        /// </summary>
        /// <returns></returns>
        private bool CanHandleNumPadNumberButtonCommand()
        {
            return true;
        }

        /// <summary>
        /// Contains Logic for handling a operator pressed(+,-,*,/)
        /// </summary>
        /// <param name="str"></param>
        private void OnHandleOperatorPadCommand(string str)
        {
            ExpressionString = str;
            canAppendOperator = false;
            _HandleOperatorPadCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Contains Validation Logic
        /// </summary>
        /// <returns></returns>
        private bool CanHandleOperatorPadCommand()
        {
            if (canAppendOperator)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnHandleNumpadEqualButtonCommand()
        {
            if(String.IsNullOrEmpty(ExpressionString))
            {
                ResultString = "= 0";
                return;
            }

            ResultString = "= " + (p.EvaluateExpression(_ExpressionString.ToString())).ToString(CultureInfo.InvariantCulture);
            _ExpressionString.Clear();
            OnPropertyChanged("HandleNumpadEqualButtonCommand");
            _HandleBackspaceCommand.RaiseCanExecuteChanged();
            _HandleClearCommand.RaiseCanExecuteChanged();
            canAppendOperator = false;
            _HandleNumberPadEqualButtonCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        ///  Contains Validation Logic
        /// </summary>
        /// <returns></returns>
        private bool CanHandleNumpadEqualButtonCommand()
        {
            return true;
        }

        private void OnHandleClearCommand()
        {
            _ExpressionString.Clear();
            OnPropertyChanged("ExpressionString");
            ResultString = "";
        }
        private bool CanHandleClearCommand()

        {
            if (_ExpressionString.Length == 0 && ResultString.Length == 0)
            {
                return false;
            }
            return true;
        }

        private void OnHandleBackSpaceCommand()
        {
            _ExpressionString.Length--;
            OnPropertyChanged("ExpressionString");

        }

        private bool CanHandleBackSpaceCommand()
        {
            if (_ExpressionString.Length == 0)
            {
                return false;
            }
            return true;
        }

        #endregion

    }
}
