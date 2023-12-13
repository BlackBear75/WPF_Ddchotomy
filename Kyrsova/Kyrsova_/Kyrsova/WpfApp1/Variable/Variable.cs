using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polinom_Interpolation
{
    public static class Variable
    {
        public static PlotModel model = new PlotModel { Title = "Polynomial and Interpolation Graphs" };
        public static  Uri iconUri;
        public static RootsSerializer rootsSerializer = new RootsSerializer();
        public static PolynomialFunction polinom;
        public static  List<Point> points;
        public static List<double> roots;
        public static double start;
        public static string fileName;
        public static double end;
        public static double precision;
        public static List<DataPoint> polynomialPoints = new List<DataPoint>();
        public static List<DataPoint> interpolationPoints = new List<DataPoint>();
    }
}
