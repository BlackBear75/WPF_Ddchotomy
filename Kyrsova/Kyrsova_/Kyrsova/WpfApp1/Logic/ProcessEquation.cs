using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polinom_Interpolation
{
    public static class ProcessEquation
    {
        static public void Equation(MainWindow mainWindow)
        {
            // Отримання коефіцієнтів полінома з текстового поля
            string[] coefficientsArray = mainWindow.coefficientsTextBox.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            double[] coefficients = coefficientsArray.Select(Convert.ToDouble).ToArray();

            // Отримання кількості точок та самих точок з текстових полей
            int numberOfPoints = Convert.ToInt32(mainWindow.numberOfPointsTextBox.Text);
            string[] pointsArray = mainWindow.pointsTextBox.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Variable.points = new List<Point>();

            if (pointsArray.Length % 2 == 0 && pointsArray.Length / 2 == numberOfPoints)
            {
                for (int i = 0; i < pointsArray.Length; i += 2)
                {
                    double x = Convert.ToDouble(pointsArray[i]);
                    double y = Convert.ToDouble(pointsArray[i + 1]);
                    Variable.points.Add(new Point(x, y));
                }

                // Отримання інтервалу
                string[] intervalArray = mainWindow.intervalTextBox.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Variable.start = Convert.ToDouble(intervalArray[0]);
                Variable.end = Convert.ToDouble(intervalArray[1]);

                // Отримання точності
                Variable.precision = Convert.ToDouble(mainWindow.precisionTextBox.Text);

                // Виклик функції для розв'язання рівняння
                Variable.polinom = new PolynomialFunction(coefficients);
                Variable.roots = FindRoots_Class.FindRoots(Variable.polinom, new LinearInterpolation(Variable.points), Variable.start, Variable.end, Variable.precision);
                if (Variable.roots.Count > 0)
                {
                    mainWindow.resultTextBlock.Text = "Корені: " + string.Join(", ", Variable.roots);
                }
                else
                {
                    mainWindow.resultTextBlock.Text = "Корені відсутні на даному інтервалі.";
                }
            }
            else
            {
                mainWindow.resultTextBlock.Text = "Невірно уведенні дані для точок";
            }
        }
    }
  }
