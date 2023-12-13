using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Polinom_Interpolation
{
    class Load_Icon
    {
        public static void  Loadicon(Window mainWindow)
        {
            Variable.iconUri = new Uri("E:\\Kyrsova_Semestr_3\\Kyrsova\\Kyrsova_\\Kyrsova\\Icons\\Icon2.png", UriKind.RelativeOrAbsolute);
            mainWindow.Icon = BitmapFrame.Create(Variable.iconUri);
        }
    }
}
