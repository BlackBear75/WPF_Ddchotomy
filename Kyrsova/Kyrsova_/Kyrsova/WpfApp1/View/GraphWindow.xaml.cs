using System.Collections.Generic;
using System.Windows;
using OxyPlot;
using OxyPlot.Series;

namespace Polinom_Interpolation
{
    public partial class GraphWindow : Window
    {
        public GraphWindow(List<DataPoint> polynomialPoints, List<DataPoint> interpolationPoints)
        {
            InitializeComponent();

           GraphShow.Graphconstructor(polynomialPoints, interpolationPoints);

            plotView.Model = Variable.model;
            Load_Icon.Loadicon(this);
        }
    
      }
}