using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        private string currentInput = string.Empty;
        private double numberOne = 0;
        private double result = 0;
        private string operation = string.Empty;

        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // Copy result to clipboard when the page disappears
            Clipboard.SetTextAsync(currentInput);
        }

        private void OnAC(object sender, EventArgs e)
        {
            currentInput = string.Empty;
            result = 0;
            operation = string.Empty;
            numberOne = 0;
            UpdateDisplay();
            Calc.Text = "";
        }

        private void OnSwitch(object sender, EventArgs e)
        {
            if (sender is Button button && !string.IsNullOrEmpty(currentInput) && currentInput != ".")
            {
                numberOne = double.Parse(currentInput);
                numberOne = numberOne * -1;
                currentInput = numberOne.ToString();
                UpdateDisplay();
            }
        }

        private void OnPercent(object sender, EventArgs e)
        {
            if (sender is Button button && !string.IsNullOrEmpty(currentInput) && currentInput != ".")
            {numberOne = double.Parse(currentInput);
                numberOne = numberOne / 100;
                currentInput = numberOne.ToString();
                UpdateDisplay();
            }
        }

        private void OnNum(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                string buttonText = button.Text;
                currentInput += buttonText;
                UpdateDisplay();
            }
        }

        private void OnPoint(Object sender, EventArgs e)
        {
            if (sender is Button button && !currentInput.Contains("."))
            {
                string buttonText = button.Text;
                currentInput += buttonText;
                UpdateDisplay();
            }
        }

        private void OnOp(object sender, EventArgs e)
        {
            if (sender is Button button && !string.IsNullOrEmpty(currentInput) && !currentInput.EndsWith("."))
            {
                operation = button.Text;
                numberOne = double.Parse(currentInput);
                currentInput = string.Empty;
                UpdateDisplay();
            }
        }


        private void OnEqual(object sender, EventArgs e)
        {
            CalculateResult(numberOne);
            UpdateDisplay();
            operation = string.Empty;
            if (!string.IsNullOrEmpty(currentInput) && !currentInput.EndsWith("."))
            {
                numberOne = double.Parse(currentInput);
            }
            
        }

        private void CalculateResult(double initialNum)
        {
            if (!string.IsNullOrEmpty(currentInput) && !currentInput.EndsWith("."))
            {
                double input = double.Parse(currentInput);

                switch (operation)
                {
                    case "+":
                        result = initialNum + input;
                        break;
                    case "-":
                        result = initialNum - input;
                        break;
                    case "X":
                        result = initialNum * input;
                        break;
                    case "/":
                        if (input != 0)
                        {
                            result = initialNum / input;
                        }
                        else
                        {
                            // Handle division by zero if needed
                            DisplayAlert("Error", "Cannot divide by zero", "OK");
                        }
                        break;
                }

                currentInput = result.ToString();
            }
        }

        private void UpdateDisplay()
        {
            Calc.Text = numberOne.ToString() + " " + operation;
            Result.Text = currentInput;
        }
    }
}
