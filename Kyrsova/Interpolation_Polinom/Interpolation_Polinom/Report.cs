using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polinom_Interpolation
{
    public class ReportData
    {
        public List<double> PolynomialResults { get; set; }
        public List<double> InterpolationResults { get; set; }

        public ReportData()
        {
            PolynomialResults = new List<double>();
            InterpolationResults = new List<double>();
        }
    }
    public class Report
    {
        public static void GenerateHtmlReport(List<Dataset> datasets, List<double> roots, PolynomialFunction fx, LinearInterpolation gx, string GetBase64GraphImage, string filename)
        {
            string htmlContent = GenerateHtmlContent(datasets, roots, fx, gx, GetBase64GraphImage);
            Console.WriteLine();
            SaveHtmlToFile(htmlContent, filename);

            Console.WriteLine($"HTML report saved ");
        }

        private static string GenerateHtmlContent(List<Dataset> datasets, List<double> roots, PolynomialFunction fx, LinearInterpolation gx, string GetBase64GraphImage)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder.AppendLine("<html>");
            htmlBuilder.AppendLine("<head><title>Report</title></head>");
            htmlBuilder.AppendLine("<body>");

            // Displaying Polynomial
            htmlBuilder.AppendLine($"<p>Polynomial: {fx.PrintPolynomial()}</p>");

            // Displaying Datasets
            htmlBuilder.AppendLine("<p>Datasets:</p>");
            htmlBuilder.AppendLine("<ul>");
            foreach (var dataset in datasets)
            {
                htmlBuilder.AppendLine($"<li>Coefficients: {dataset.Coefficients}, Number of Points: {dataset.NumberOfPoints}, Points: {dataset.Points}, Interval: {dataset.Interval}, Precision: {dataset.Precision}</li>");
            }
            htmlBuilder.AppendLine("</ul>");

            // Displaying Interpolation Points
            htmlBuilder.AppendLine("<p>Interpolation Points:</p>");
            htmlBuilder.AppendLine("<ul>");
            foreach (var point in gx.GetPoints())
            {
                htmlBuilder.AppendLine($"<li>x = {point.X}, y = {point.Y}</li>");
            }
            htmlBuilder.AppendLine("</ul>");

            // Displaying Found Roots
            htmlBuilder.AppendLine("<p>Found Roots:</p>");
            if (roots.Count == 0)
            {
                htmlBuilder.AppendLine("<p>No roots found in the specified interval.</p>");
            }
            else
            {
                htmlBuilder.AppendLine("<ul>");
                foreach (var root in roots)
                {
                    htmlBuilder.AppendLine($"<li>Root: {root}</li>");
                }
                htmlBuilder.AppendLine("</ul>");
            }
            string graphImageBase64 = GetBase64GraphImage;
            htmlBuilder.AppendLine($"<img src=\"data:image/png;base64,{graphImageBase64}\" alt=\"Graph\"/>");
            htmlBuilder.AppendLine("</body>");
            htmlBuilder.AppendLine("</html>");

            return htmlBuilder.ToString();
        }


        private static void SaveHtmlToFile(string htmlContent, string fileName)
        {
            try
            {
                File.WriteAllText(fileName, htmlContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving HTML report: {ex.Message}");
            }
        }

    }
}
