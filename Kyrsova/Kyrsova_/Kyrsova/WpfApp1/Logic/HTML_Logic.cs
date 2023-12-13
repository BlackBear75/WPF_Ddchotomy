using Microsoft.Win32;
using OxyPlot.Series;
using OxyPlot.Wpf;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Polinom_Interpolation
{
   public class HTML_Logic
    {
        public static void Report_HTML()
        {
            try
            {
                // Створення об'єкту ProcessStartInfo
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Створення процесу і вказівка команди для відкриття HTML-файлу в браузері
                System.Diagnostics.Process process = new System.Diagnostics.Process
                {
                    StartInfo = startInfo
                };
                process.Start();

                using (StreamWriter sw = process.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        // Вказати команду відкриття HTML-файлу в браузері
                        sw.WriteLine($"start {Variable.fileName}");
                    }
                }

                // Закрити процес
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при відкритті HTML-звіту: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void GenerateHtmlReport(MainWindow mainWindow)
        {
            try
            {
                
                List<Dataset> datasets = GetDatasets(mainWindow);
                string graghImage = GetBase64GraphImage(Variable.polynomialPoints, Variable.interpolationPoints);
                Variable.fileName = GetSaveHtmlFileName();
                if (Variable.fileName != null)
                {

                    Report.GenerateHtmlReport(datasets, Variable.roots, Variable.polinom, new LinearInterpolation(Variable.points), graghImage, Variable.fileName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private static List<Dataset> GetDatasets(MainWindow mainWindow)
        {
            List<Dataset> datasets = new List<Dataset>();



            Dataset dataset1 = new Dataset
            {
                Coefficients = mainWindow.coefficientsTextBox.Text, 
                NumberOfPoints = mainWindow.numberOfPointsTextBox.Text,
                Points = mainWindow.pointsTextBox.Text,
                Interval = mainWindow.intervalTextBox.Text,
                Precision = mainWindow.precisionTextBox.Text
            };

            datasets.Add(dataset1);

            return datasets;
        }

        public static string GetBase64GraphImage(List<DataPoint> polynomialPoints, List<DataPoint> interpolationPoints)
        {
            var plotModel = new PlotModel();

            // Додаємо серії для полінома та інтерполяції
            var polynomialSeries = new LineSeries { Title = "Polynomial" };
            polynomialSeries.Points.AddRange(polynomialPoints);
            plotModel.Series.Add(polynomialSeries);

            var interpolationSeries = new LineSeries { Title = "Interpolation" };
            interpolationSeries.Points.AddRange(interpolationPoints);
            plotModel.Series.Add(interpolationSeries);



            // Перетворюємо графік у формат PNG та його конвертація у Base64
            using (var stream = new MemoryStream())
            {
                var pngExporter = new PngExporter { Width = 600, Height = 400 };
                pngExporter.Export(plotModel, stream);
                byte[] imageBytes = stream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }
        public static string GetSaveHtmlFileName()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "HTML files (*.html)|*.html|All files (*.*)|*.*";
            saveFileDialog.Title = "Save HTML Report";

            if (saveFileDialog.ShowDialog() == true)
            {
                return saveFileDialog.FileName;
            }

            return null;
        }

    }
}
