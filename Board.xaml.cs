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
        static Pawn yp1 = new Pawn(Color.FromArgb(255, 255, 0, 255));
        static Pawn yp2 = new Pawn(Color.FromArgb(255, 255, 0, 255));
        static Pawn yp3 = new Pawn(Color.FromArgb(255, 255, 0, 255));
        static Pawn yp4 = new Pawn(Color.FromArgb(255, 255, 0, 255));

        static Pawn gp1 = new Pawn(Color.FromArgb(255, 0, 255, 0));
        static Pawn gp2 = new Pawn(Color.FromArgb(255, 0, 255, 0));
        static Pawn gp3 = new Pawn(Color.FromArgb(255, 0, 255, 0));
        static Pawn gp4 = new Pawn(Color.FromArgb(255, 0, 255, 0));

        static Pawn rp1 = new Pawn(Color.FromArgb(255, 255, 0, 0));
        static Pawn rp2 = new Pawn(Color.FromArgb(255, 255, 0, 0));
        static Pawn rp3 = new Pawn(Color.FromArgb(255, 255, 0, 0));
        static Pawn rp4 = new Pawn(Color.FromArgb(255, 255, 0, 0));

        static Pawn bp1 = new Pawn(Color.FromArgb(255, 0, 0, 255));
        static Pawn bp2 = new Pawn(Color.FromArgb(255, 0, 0, 255));
        static Pawn bp3 = new Pawn(Color.FromArgb(255, 0, 0, 255));
        static Pawn bp4 = new Pawn(Color.FromArgb(255, 0, 0, 255));
        static CardDeck cardDeck = new CardDeck();

        List<Pawn> rPawns = new List<Pawn>();
        List<Pawn> gPawns = new List<Pawn>();
        List<Pawn> yPawns = new List<Pawn>();
        List<Pawn> bPawns = new List<Pawn>();

        List<List<Pawn>> allPawns = new List<List<Pawn>>();


        int discardnum = 0;
        public enum Card { One, Two, Three, Four, Five, Seven, Eight, Ten, Eleven, Twelve, Sorry };

        public Board()


        {

            yPawns.Add(yp1);
            yPawns.Add(yp2);
            yPawns.Add(yp3);
            yPawns.Add(yp4);

            gPawns.Add(gp1);
            gPawns.Add(gp2);
            gPawns.Add(gp3);
            gPawns.Add(gp4);

            rPawns.Add(rp1);
            rPawns.Add(rp2);
            rPawns.Add(rp3);
            rPawns.Add(rp4);

            bPawns.Add(bp1);
            bPawns.Add(bp2);
            bPawns.Add(bp3);
            bPawns.Add(bp4);

            allPawns.Add(yPawns);
            allPawns.Add(gPawns);
            allPawns.Add(rPawns);
            allPawns.Add(bPawns);

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


                    foreach (var pl in allPawns)
                    {

                        foreach (var p in pl)
                        {
                            p.SetPosition(0, 0);
                        }
                    }



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








            //p.SetSize(30, 30);




        }
        private void MiniButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void CheckWin(Button b)
        {
            var name = b.Name;
            bool allIn = true;
            if (name.Contains("Home"))
            {
                foreach (var pl in allPawns)
                {

                    foreach (var p in pl)
                    {
                        string spotName = ((Button)p.pawnRect.Parent).Name;

                        if (!spotName.Contains("Home"))
                        {
                            allIn = false;
                        }
                    }
                }
            }

            if (allIn)
            {
                this.Frame.Navigate(typeof(WinPage));
            }
        }




        private void FaceUpCard_Click(object sender, RoutedEventArgs e)
        {
            Card card;
            card = (Card)cardDeck.drawCard();
            FaceUpCard.Content = card.ToString();
            discardnum++;
            if (discardnum >= 45)
            {
                discardnum = 0;
            }
            DiscardPile.Text = discardnum.ToString();
        }
    }
}
