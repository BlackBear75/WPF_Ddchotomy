using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Polinom_Interpolation
{
   public class GraphShow
    {
        public static void ShowGraph(MainWindow mainWindow)
        {
            try
            {
                string[] coefficientsArray = mainWindow.coefficientsTextBox.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                double[] coefficients = coefficientsArray.Select(Convert.ToDouble).ToArray();
                ProcessEquation.Equation(mainWindow);

                // Очистка списків перед додаванням нових точок
                Variable.polynomialPoints.Clear();
                Variable.interpolationPoints.Clear();

                for (double x = Variable.start; x <= Variable.end; x += Variable.precision)
                {
                    Variable.polynomialPoints.Add(new DataPoint(x, new PolynomialFunction(coefficients).Evaluate(x)));
                    Variable.interpolationPoints.Add(new DataPoint(x, new LinearInterpolation(Variable.points.ToList()).Interpolate(x)));
                }

                // Відображення графіків у новому вікні
                GraphWindow graphWindow = new GraphWindow(Variable.polynomialPoints, Variable.interpolationPoints);
                graphWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                mainWindow.resultTextBlock.Text = "Помилка: " + ex.Message;
            }
        }

        public static void Graphconstructor(List<DataPoint> polynomialPoints, List<DataPoint> interpolationPoints)
        {
           

            // Додаємо лінії для полінома та інтерполяції
            LineSeries polynomialSeries = new LineSeries { Title = "Polynomial" };
            polynomialSeries.Points.AddRange(polynomialPoints);
            Variable.model.Series.Add(polynomialSeries);

            LineSeries interpolationSeries = new LineSeries { Title = "Interpolation" };
            interpolationSeries.Points.AddRange(interpolationPoints);
            Variable.model.Series.Add(interpolationSeries);

            // Додаємо легенду для кожного LineSeries
            Variable.model.IsLegendVisible = true;
        }



    }
}
