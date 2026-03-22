using CalculatorLibrary_Artemov;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace CalculatorWPF_Artemov
{
    public partial class MainWindow : Window
    {
        private Calculator Calc;
        private string formula = "";
        private string currentInput = "";
        private bool isNewFormula = true;

        public MainWindow()
        {
            InitializeComponent();
            Calc = new Calculator();
            UpdateDisplays();
        }

        private void btnNumber_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string digit = button.Content.ToString();

            if (isNewFormula)
            {
                formula = "";
                currentInput = "";
                isNewFormula = false;
            }

            currentInput += digit;
            UpdateDisplays();
        }

        private int GetParenthesesDepth(string formula)
        {
            int depth = 0;
            foreach (char c in formula)
            {
                if (c == '(') depth++;
                else if (c == ')') depth--;
            }
            return depth;
        }

        private void btnOperation_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string operation = button.Content.ToString();

            if (operation == "-")
            {
                int parenDepth = GetParenthesesDepth(formula);
                bool isStartOfFormula = string.IsNullOrWhiteSpace(formula) && string.IsNullOrWhiteSpace(currentInput);

                string trimmedFormula = formula.TrimEnd();
                char lastChar = trimmedFormula.Length > 0 ? trimmedFormula[trimmedFormula.Length - 1] : '\0';

                bool canBeUnary = isStartOfFormula || lastChar == '(' || (parenDepth > 0 && "+-*/^".Contains(lastChar));

                if (canBeUnary && string.IsNullOrEmpty(currentInput))
                {
                    currentInput = "-";
                    isNewFormula = false;
                    UpdateDisplays();
                    return;
                }
                else if (!canBeUnary && string.IsNullOrEmpty(currentInput) && !string.IsNullOrEmpty(formula))
                {
                    MessageBoxResult result = MessageBox.Show
                        (
                        "Минус в начале числа разрешён только в скобках.\n\n" +
                        "Хотите автоматически исправить?",
                        "Синтаксическая ошибка",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning
                        );

                    if (result == MessageBoxResult.Yes)
                    {
                        formula += "(";
                        currentInput = "-";
                        isNewFormula = false;
                        UpdateDisplays();
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            if (string.IsNullOrEmpty(currentInput) && string.IsNullOrEmpty(formula) && operation != "-")
                return;

            if (!string.IsNullOrEmpty(currentInput))
            {
                formula += currentInput + "" + operation + "";
                currentInput = "";
            }

            else if (!string.IsNullOrEmpty(formula))
            {
                formula = formula.TrimEnd();
                int lastSpace = formula.LastIndexOf(' ');
                if (lastSpace > 0 && formula[lastSpace - 1] != '(')
                {
                    formula = formula.Substring(0, lastSpace) + "" + operation + "";
                }
                else
                {
                    formula += "" + operation + "";
                }
            }

            UpdateDisplays();
        }

        private void btnTochka_Click(object sender, RoutedEventArgs e)
        {
            if (isNewFormula)
            {
                formula = "";
                currentInput = "0";
                isNewFormula = false;
            }
            if (!currentInput.Contains("."))
            {
                if (string.IsNullOrEmpty(currentInput) || currentInput == "-")
                {
                    currentInput += (currentInput == "-" ? "" : "0") + ".";
                }
                else
                {
                    currentInput += ".";
                }
                UpdateDisplays();
            }
        }

        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput))
            {
                currentInput = currentInput.Length > 1 ? currentInput.Substring(0, currentInput.Length - 1) : "";
            }
            else if (!string.IsNullOrEmpty(formula))
            {
                formula = formula.Length > 1 ? formula.Substring(0, formula.Length - 1) : "";
            }
            UpdateDisplays();
        }

        private void btnSbros_Click(object sender, RoutedEventArgs e)
        {
            formula = "";
            currentInput = "";
            isNewFormula = true;
            UpdateDisplays();
        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            string fullFormula = formula + currentInput;

            if (string.IsNullOrWhiteSpace(fullFormula))
                return;

            try
            {
                string cleanFormula = fullFormula;

                List<double> numbers = new List<double>();
                List<string> operations = new List<string>();
                string currentNum = "";

                foreach (char c in cleanFormula)
                {
                    if (c == '(' || c == ')')
                    {
                        if (!string.IsNullOrEmpty(currentNum))
                        {
                            numbers.Add(double.Parse(currentNum, CultureInfo.InvariantCulture));
                            currentNum = "";
                        }
                        continue;
                    }

                    if (char.IsDigit(c) || c == '.' || (c == '-' && currentNum == ""))
                    {
                        if (c == '.' && currentNum.Contains("."))
                        {
                            throw new FormatException("Две точки в числе");
                        }
                        currentNum += c;
                    }

                    else if ("+-*/^".Contains(c))
                    {
                        if (!string.IsNullOrEmpty(currentNum))
                        {
                            numbers.Add(double.Parse(currentNum, CultureInfo.InvariantCulture));
                            currentNum = "";
                        }
                        operations.Add(c.ToString());
                    }

                    else
                    {
                        throw new FormatException("Недопустимый символ: '" + c + "'");
                    }
                }

                if (!string.IsNullOrEmpty(currentNum))
                {
                    numbers.Add(double.Parse(currentNum, CultureInfo.InvariantCulture));
                }

                if (numbers.Count == 0 || numbers.Count != operations.Count + 1)
                {
                    MessageBox.Show("Ошибка в формуле!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                double result = numbers[0];
                for (int i = 0; i < operations.Count; i++)
                {
                    string op = operations[i];
                    double nextNumber = numbers[i + 1];

                    switch (op)
                    {
                        case "+": result = Calc.Slozenie(result, nextNumber); break;
                        case "-": result = Calc.Vicitanie(result, nextNumber); break;
                        case "*": result = Calc.Umnozenie(result, nextNumber); break;
                        case "/": result = Calc.Delenie(result, nextNumber); break;
                        case "^": result = Calc.Stepen(result, nextNumber); break;
                        default: throw new Exception("Неизвестная операция: " + op);
                    }
                }

                formula = "";
                currentInput = result.ToString(CultureInfo.InvariantCulture);
                isNewFormula = true;
                UpdateDisplays();
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("На ноль делить нельзя! (Ата Та!)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                btnSbros_Click(null, null);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Ошибка числа: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                btnSbros_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                btnSbros_Click(null, null);
            }
        }
        private void UpdateDisplays()
        {
            display.Text = formula + (string.IsNullOrEmpty(currentInput) ? "" : currentInput);
        }
    }
}