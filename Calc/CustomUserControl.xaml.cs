using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calc
{
    /// <summary>
    /// Interaction logic for CustomUserControl.xaml
    /// </summary>
    public partial class CustomUserControl : UserControl
    {
        public CustomUserControl()
        {
            InitializeComponent();
            CalcFace.Visibility = Visibility.Hidden;
        }

        //private bool isShowHideVisible;
        //public bool IsShowHideVisible
        //{
        //    get { return isShowHideVisible; }
        //    set
        //    {
        //        if (isShowHideVisible != value)
        //        {
        //            isShowHideVisible = value;
        //            OnPropertyChange("IsShowHideVisible");
        //        }
        //    }
        //}
        //private void OnPropertyChange([CallerMemberName] string pPropertyName = null)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(pPropertyName));
        //    }
        //}
        private void ShowBtn_Click(object sender, RoutedEventArgs e)
        {
            

            
                if (CalcFace.Visibility != Visibility.Visible)
                {
                    CalcFace.Visibility = Visibility.Visible;
                }
                else
                {
                    CalcFace.Visibility = Visibility.Hidden;
                }
            

        }

        
        private decimal _actualResult = 0;
        private char _operation = ' ';
        private bool _isLastOperation = true;
        private bool _isLastEqual = false;
        private int _counter = 0;

        

        private void Calculations()
        {
            try
            {
                switch (_operation)
                {
                    case '+':
                        _actualResult += Convert.ToDecimal(ResultDisplay.Text); break;
                    case '-':
                        _actualResult -= Convert.ToDecimal(ResultDisplay.Text); break;
                    case '*':
                        _actualResult *= Convert.ToDecimal(ResultDisplay.Text);
                        _actualResult = Math.Round(_actualResult, 10);
                        break;
                    case '/':
                        if (Convert.ToDecimal(ResultDisplay.Text) == 0)
                        {
                            MessageBox.Show("Cannot devide by 0");
                            ComputingDisplay.Text = "";
                            _actualResult = 0;
                            ResultDisplay.Text = "";
                        }
                        else
                        {
                            _actualResult /= Convert.ToDecimal(ResultDisplay.Text);
                            _actualResult = Math.Round(_actualResult, 10);
                        }
                        break;
                    default:
                        //uhjklhkhkjhkj
                        break;
                }
            }
            catch
            {
                ResultDisplay.Text = "";
                _actualResult = 0;
                ComputingDisplay.Text = "";
                MessageBox.Show("Too many numbers");

            }
        }

        private void UpdateResultDisplay()
        {
            ResultDisplay.Text = _actualResult.ToString();
        }
        private void UpdateComputing(int c)
        {
            if (c == 1)
            {
                ComputingDisplay.Text = ResultDisplay.Text + Convert.ToString(_operation);
                _actualResult = Convert.ToDecimal(ResultDisplay.Text);
            }
            else
            {
                ComputingDisplay.Text = ComputingDisplay.Text + ResultDisplay.Text + Convert.ToString(_operation);
            }
        }

        private void ButtonOperation_Click(object sender, RoutedEventArgs e)
        {
            string _txt = sender.ToStr();
            if (_isLastOperation == false)
            {
                _counter++;
                Calculations();
                _operation = Convert.ToChar(_txt);
                UpdateComputing(_counter);

                _isLastOperation = true;
                UpdateResultDisplay();
            }
            if (_isLastEqual == true)
            {
                _operation = Convert.ToChar(_txt);
                ComputingDisplay.Text = _actualResult.ToString() + Convert.ToString(_operation);
                _isLastEqual = false;
            }
        }

        private void ButtonNumb_Click(object sender, RoutedEventArgs e)
        {
            if (_isLastOperation == true)
            {
                ResultDisplay.Text = "";
                _isLastOperation = false;
            }
            ResultDisplay.Text += sender.ToStr();
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            _actualResult = 0;
            ComputingDisplay.Text = "";
            ResultDisplay.Text = "";
            _counter = 0;
            _operation = ' ';
            _isLastOperation = true;
        }

        private void btnCE_Click(object sender, RoutedEventArgs e)
        {
            ResultDisplay.Text = "";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (ResultDisplay.Text.Length > 0 && _isLastOperation == false)
            {
                ResultDisplay.Text = ResultDisplay.Text.Remove(ResultDisplay.Text.Length - 1, 1);
            }
        }

        private void btnComma_Click(object sender, RoutedEventArgs e)
        {
            bool isAlreadyComma = ResultDisplay.Text.Contains('.');
            if (isAlreadyComma == false && ResultDisplay.Text.Length != 0)
                ResultDisplay.Text += '.';
        }

        private void btnEqual_Click(object sender, RoutedEventArgs e)
        {
            if (_isLastEqual != true)
            {
                Calculations();
                ComputingDisplay.Text = ComputingDisplay.Text + ResultDisplay.Text + "=";
                ResultDisplay.Text = _actualResult.ToString();
                _isLastEqual = true;
                _isLastOperation = true;
            }
            MainTextBox.Text = ResultDisplay.Text;
        }

        private void btPlsMns7_Click(object sender, RoutedEventArgs e)
        {
            if (ResultDisplay.Text != "")
            {
                _actualResult = Convert.ToDecimal(ResultDisplay.Text);
                _actualResult = _actualResult * (-1);
                ResultDisplay.Text = _actualResult.ToString();
            }
        }

        void IsTooLarge(TextBox obj)
        {
            if (obj.Text.Length > 20)
            {
                ResultDisplay.Text = "";
                _actualResult = 0;
                MessageBox.Show("Too many numbers");
            }
        }

        private void ResultDisplay_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsTooLarge((TextBox)sender);
        }
    }
}
