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
    class turn : Page
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
        public string t = "";
        public bool OnGoingTurn()
        {
            if (t.Equals("Y"))
            {
                return false;
            }
            else
                return true;
        }

        public bool YellowTurn(Pawn p, int[] pos)
        {
            TextBlock fieldTB = (TextBlock)this.FindName("TurnLabel");
            fieldTB.Text = "Check";
            return true;
        }
        
        public void changeTurn()
        {
            TextBlock TurnLabel = (TextBlock)this.FindName("TurnLabel");
            if (TurnLabel.Text == "Turn: Yellow") TurnLabel.Text = "Turn: Green";
            else if (TurnLabel.Text == "Turn: Green") TurnLabel.Text = "Turn: Red";
            else if (TurnLabel.Text == "Turn: Red") TurnLabel.Text = "Turn: Blue";
            else if (TurnLabel.Text == "Turn: Blue") TurnLabel.Text = "Turn: Yellow";
        }
    }
}