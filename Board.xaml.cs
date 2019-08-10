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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sorry
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Board : Page
    {
        static Pawn p1 = new Pawn(Color.FromArgb(255, 0, 0, 255));
        static Pawn p2 = new Pawn(Color.FromArgb(255, 255, 0, 0));
        Pawn pc = p1;

        public Board()
        {
            this.InitializeComponent();
            foreach (var g in BoardGrid.Children)
            {
                if (g.GetType().Equals(typeof(Button)))
                {
                    g.Tapped += new TappedEventHandler(Button_Click);
                }
                else if (g.GetType().Equals(typeof(Grid)))
                {
                    var cg = (Grid)g;






                    foreach (var gb in cg.Children)
                    {
                        if (gb.GetType().Equals(typeof(Button)))
                        {
                            gb.Tapped += new TappedEventHandler(MiniButton_Click);
                        }
                    }
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {



            pc.SetPosition(sender);
            if (pc.Equals(p1))
            {
                pc = p2;
            }
            else if (pc.Equals(p2))
            {
                pc = p1;
            }



            //p.SetSize(30, 30);




        }
        private void MiniButton_Click(object sender, RoutedEventArgs e)
        {

            pc.SetPosition(sender);
            //p.SetSize(20, 20);


        }
    }
}
