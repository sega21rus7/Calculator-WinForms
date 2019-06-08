using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); // дизайнер
            SpecialButtonsClickHandlers();
        }

        void SpecialButtonsClickHandlers()
        {
            CButton.Click += (sender, args) =>
                {
                    EnterTextBox.Text = string.Empty;
                    ExpressionTextBox.Text = string.Empty;
                };
            CEButton.Click += (sender, args) => EnterTextBox.Text = string.Empty;
            DeleteButton.Click += (sender, args) =>
                {
                    if (EnterTextBox.Text != string.Empty)
                        EnterTextBox.Text =
                            EnterTextBox.Text.Remove(EnterTextBox.Text.Length - 1);
                };
            ForDoubleButton.Click += (sender, args) =>
                {
                    if (EnterTextBox.Text.IndexOf(',') != EnterTextBox.Text.Length - 1)
                        EnterTextBox.Paste(",");
                };
            PIButton.Click += (sender, args) => EnterTextBox.Paste(Math.PI.ToString());
            EButton.Click += (sender, args) => EnterTextBox.Paste(Math.Exp(1).ToString());
            X2Button.Click += (sender, args) =>
                {
                    if (EnterTextBox.Text != string.Empty) EnterTextBox.Paste("^2");
                };
            YXButton.Click += (sender, args) =>
                {
                    if (EnterTextBox.Text != string.Empty) EnterTextBox.Paste("^");
                };
        }

        void Buttons_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            EnterTextBox.Paste(button.Text);
        }

        void FunctionButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var line = EnterTextBox.Text;
            try
            {
                var calc = new MyCalc();
                var result = calc.ComputeOfOneValue(line, button.Text);
                EnterTextBox.Clear();
                EnterTextBox.Paste(result.ToString());
            }
            catch (Exception exception)
            {
                ErrorMessage(exception.Message);
            }
        }

        void ResultButton_Click(object sender, EventArgs e)
        {
            var line = EnterTextBox.Text;
            try
            {
                var calc = new MyCalc();
                var result = calc.ComputeOfTwoValue(line);
                ExpressionTextBox.Text = EnterTextBox.Text;
                EnterTextBox.Text = result.ToString();
            }
            catch (Exception exception)
            {
                ErrorMessage(exception.Message);
            }
        }

        static void ErrorMessage(string message)
        {
            MessageBox.Show(
                        message,
                        "Сообщение об ошибке",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
