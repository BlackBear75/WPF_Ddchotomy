using System.Windows;

namespace Polinom_Interpolation
{
    public partial class ReportWindow : Window
    {
        public ReportWindow(string reportText)
        {
            InitializeComponent();
            reportTextBox.Text = reportText;
            Load_Icon.Loadicon(this);
        }
    }
}