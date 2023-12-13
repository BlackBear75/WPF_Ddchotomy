
using System.Xml.Serialization;

using System.Collections;
using System.Text;

namespace Polinom_Interpolation
{
    #region патерн Strategy якщо ми захочимо добавити ще рівняня для інтерполяції
    public interface IInterpolationStrategy
    {
        double Interpolate(double x);
    }
    #endregion
    
  
    #region Клас для Полінома
    public class PolynomialFunction
    {
        private double[] coefficients;

        public PolynomialFunction(params double[] coefficients)
        {
            this.coefficients = coefficients.Reverse().ToArray();
        }

        public double Evaluate(double x)
        {
            double result = 0.0;
            for (int i = 0; i < coefficients.Length; i++)
            {
                result += coefficients[i] * Math.Pow(x, i);
            }
            return result;
        }

        public string PrintPolynomial()
        {
            string polinom = "Поліном : ";

            for (int i = 0; i < coefficients.Length; i++)
            {
                if (i == 0)
                {
                 
                    polinom += coefficients[i];
                }
                else
                {
                    if (coefficients[i] >= 0)
                    {
                        
                        polinom += $" + {coefficients[i]}x^{i}";
                    }
                    else
                    {
                       
                        polinom += $" - {Math.Abs(coefficients[i])}x^{i}";
                    }
                }
            }
            return polinom;
        }
    }
    #endregion

    #region клас для лінійної інтерполяції
    public class LinearInterpolation : IInterpolationStrategy
    {
        private List<Point> points;

        public LinearInterpolation(List<Point> interpolationPoints)
        {
            points = interpolationPoints;
        } 
        public List<Point> GetPoints()
        {
            return points;
        }
        public double Interpolate(double x)
        {
            if (points.Count < 2)
            {
                throw new InvalidOperationException("Для лінійної інтерполяції потрібно щонайменше дві точки.");
            }

            points.Sort((p1, p2) => p1.X.CompareTo(p2.X));

            double minDistance1 = double.MaxValue;
            double minDistance2 = double.MaxValue;

            Point nearestPoint1 = null;
            Point nearestPoint2 = null;

            foreach (var point in points)
            {
                double distance = Math.Abs(point.X - x);

                if (distance < minDistance1)
                {
                    minDistance2 = minDistance1;
                    nearestPoint2 = nearestPoint1;

                    minDistance1 = distance;
                    nearestPoint1 = point;
                }
                else if (distance < minDistance2)
                {
                    minDistance2 = distance;
                    nearestPoint2 = point;
                }
            }

            if (nearestPoint1 == null || nearestPoint2 == null)
            {
                throw new InvalidOperationException("Не вдалося знайти дві найближчі точки для інтерполяції.");
            }

            double slope = (nearestPoint2.Y - nearestPoint1.Y) / (nearestPoint2.X - nearestPoint1.X);
            double interpolatedValue = nearestPoint1.Y + slope * (x - nearestPoint1.X);

            return interpolatedValue;
        }

       
    }
    #endregion

    #region Клас з методом вирішення
    public class FindRoots_Class
    {

       public static List<double> FindRoots(PolynomialFunction fx, LinearInterpolation gx, double start, double end, double precision)
        {
            List<double> roots = new List<double>();
            List<double> intermediateRoots = new List<double>();

            double x = start;
            while (x <= end)
            {
                double fxValue = fx.Evaluate(x);
                double gxValue = gx.Interpolate(x);



                if (Math.Abs(fxValue - gxValue) < precision)
                {
                    roots.Add(x);
                }
                else
                {
                    intermediateRoots.Add(x);
                }

                x += precision;
            }


            return roots;
        }
    }
    #endregion
    
}