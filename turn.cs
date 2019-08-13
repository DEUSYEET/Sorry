using Sorry.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Sorry
{
    class turn
    {
        public int[] turns(object sender)
        {
            var o = (FrameworkElement)sender;
            Grid g = (Grid)o.Parent;
            var b = (FrameworkElement)sender;
            var X = Grid.GetColumn(b);
            var Y = Grid.GetRow(b);


            int[] superHot = { X, Y };
            return superHot;
        }
    }
}
