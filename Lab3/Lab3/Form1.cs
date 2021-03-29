using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Form1 : Form
    {
        int numberOfTests = 10000;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics gr = pictureBox1.CreateGraphics();
            gr.Clear(Color.White);

            Random rnd = new Random();
            int[] firstRandomVariables = new int[numberOfTests];
            int[] secondRandomVariables = new int[numberOfTests];
            double firstNumber, secondNumber;

            firstNumber = Double.Parse(textBox1.Text);
            secondNumber = Double.Parse(textBox2.Text);

            if (IsNumberCorrect(firstNumber, secondNumber))
            {
                for (int i = 0; i < 10000; i++)
                {
                    firstRandomVariables[i] = rnd.Next(pictureBox1.Width) - 100;
                    secondRandomVariables[i] = rnd.Next(pictureBox1.Width) + 100;
                }

                double firstMathExpectation = CalculateMathExpectation(firstRandomVariables);
                double secondMathExpectation = CalculateMathExpectation(secondRandomVariables);

                double firstStandartDeviation = CalculateStandartDeviation(firstRandomVariables, firstMathExpectation);
                double secondStandartDeviation = CalculateStandartDeviation(secondRandomVariables, secondMathExpectation);

                double[] probabilityDensityForFirstRandomVariables = new double[numberOfTests];
                double[] probabilityDensityForSecondRandomVariables = new double[numberOfTests];

                for (int i = 0; i < numberOfTests; i++)
                {
                    probabilityDensityForFirstRandomVariables[i] = CalculateProbabilityDensity(i, firstMathExpectation, firstStandartDeviation);
                    probabilityDensityForSecondRandomVariables[i] = CalculateProbabilityDensity(i, secondMathExpectation, secondStandartDeviation);
                }

                Graphics graph;
                for (int i = 0; i < numberOfTests; i++)
                {
                    graph = pictureBox1.CreateGraphics();
                    graph.FillRectangle(Brushes.Red, i, pictureBox1.Height - (int)(probabilityDensityForFirstRandomVariables[i] * firstNumber * 150000), 3, 3);
                    graph.FillRectangle(Brushes.Green, i, pictureBox1.Height - (int)(probabilityDensityForSecondRandomVariables[i] * secondNumber * 150000), 3, 3);
                }

                textBox3.Text = Convert.ToString(CalculatePropabilitiesOfFalseAlarms(firstNumber, secondNumber, firstMathExpectation, secondMathExpectation, firstStandartDeviation, secondStandartDeviation));
                textBox4.Text = Convert.ToString(CalculatePropabilitiesOfMissingErrors(firstNumber, secondNumber, firstMathExpectation, secondMathExpectation, firstStandartDeviation, secondStandartDeviation));
                textBox5.Text = Convert.ToString(Convert.ToDouble(textBox3.Text) + Convert.ToDouble(textBox4.Text));
            }
            else
            {
                MessageBox.Show("Введённые значения не подходят под условия!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public double CalculatePropabilitiesOfFalseAlarms(double firstPropability, double secondProbability, double firstMathExpectation,
            double secondMathExpectation, double firstStandartDeviation, double secondStandartDeviation)
        {
            double eps = 0.001;
            double x = -100;
            double p1 = 1, p2 = 0;
            double probabilitiesOfFalseAlarms = 0;

            if (secondProbability != 0)
            {
                while (p2 < p1)
                {
                    p1 = firstPropability * CalculateProbabilityDensity(x, firstMathExpectation, firstStandartDeviation);
                    p2 = secondProbability * CalculateProbabilityDensity(x, secondMathExpectation, secondStandartDeviation);
                    probabilitiesOfFalseAlarms += p2 * eps;
                    x += eps;
                }
            }

            if (firstPropability == 0)
            {
                probabilitiesOfFalseAlarms = 1;
            }
            else if (secondProbability == 0)
            {
                probabilitiesOfFalseAlarms = 0;
            }
            else
            {
                probabilitiesOfFalseAlarms /= firstPropability;
            }

            return probabilitiesOfFalseAlarms;
        }

        public double CalculatePropabilitiesOfMissingErrors(double firstPropability, double secondProbability, double firstMathExpectation,
            double secondMathExpectation, double firstStandartDeviation, double secondStandartDeviation)
        {
            double eps = 0.001;
            double x = -100;
            double p1 = 1, p2 = 0;
            double probabilitiesOfMissingErrors = 0;

            if (secondProbability != 0)
            {
                while (p2 < p1)
                {
                    p1 = firstPropability * CalculateProbabilityDensity(x, firstMathExpectation, firstStandartDeviation);
                    p2 = secondProbability * CalculateProbabilityDensity(x, secondMathExpectation, secondStandartDeviation);
                    x += eps;
                }
            }

            double tmp = x;
            
            while (x < pictureBox1.Width + 100)
            {
                p1 = CalculateProbabilityDensity(x, firstMathExpectation, firstStandartDeviation);
                p2 = CalculateProbabilityDensity(x, secondMathExpectation, secondStandartDeviation);
                probabilitiesOfMissingErrors += p1 * firstPropability * eps;
                x += eps;
            }

            if ((firstPropability != 0) && (secondProbability != 0))
            {
                Graphics graph = pictureBox1.CreateGraphics();
                graph.DrawLine(new Pen(Brushes.Blue), new Point((int)tmp, 0), new Point((int)tmp, pictureBox1.Height));

            }

            if (firstPropability == 0)
            {
                probabilitiesOfMissingErrors = 0;
            }
            else if (secondProbability == 0)
            {
                probabilitiesOfMissingErrors = 0;
            }
            else
            {
                probabilitiesOfMissingErrors /= firstPropability;
            }

            return probabilitiesOfMissingErrors;
        }

        public double CalculateProbabilityDensity(double number, double mathExpectation, double standartDeviation)
        {
            double numerator = Math.Exp(-0.5 * Math.Pow((number - mathExpectation) / standartDeviation, 2));
            double denominator = standartDeviation * Math.Sqrt(2 * Math.PI);
            return numerator / denominator;
        }

        public double CalculateMathExpectation(int[] randomVariables)
        {
            int sumOfRandomVariables = 0;
            for (int i = 0; i < numberOfTests; i++)
            {
                sumOfRandomVariables += randomVariables[i];
            }

            return sumOfRandomVariables / numberOfTests;
        }

        public double CalculateStandartDeviation(int[] randomVariables, double mathExpectation)
        {
            double sum = 0;
            for (int i = 0; i < numberOfTests; i++)
            {
                sum += Math.Pow(randomVariables[i] - mathExpectation, 2);
            }

            return Math.Sqrt(sum / numberOfTests);
        }

        public bool IsNumberCorrect(double firstNumber, double secondNumber)
        {
            if (((firstNumber >= 0) && (firstNumber <= 1)) && 
                ((secondNumber >= 0) && (secondNumber <= 1)) &&
                (firstNumber + secondNumber == 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && (number != 8) && (number != 44))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && (number != 8) && (number != 44))
            {
                e.Handled = true;
            }
        }

    }
}
