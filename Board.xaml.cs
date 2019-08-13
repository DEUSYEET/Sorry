using Sorry.Assets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

//The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sorry
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Board : Page
    {
        static Pawn yp1 = new Pawn(Color.FromArgb(255, 255, 0, 255), color.yellow);
        static Pawn yp2 = new Pawn(Color.FromArgb(255, 255, 0, 255), color.yellow);
        static Pawn yp3 = new Pawn(Color.FromArgb(255, 255, 0, 255), color.yellow);
        static Pawn yp4 = new Pawn(Color.FromArgb(255, 255, 0, 255), color.yellow);

        static Pawn gp1 = new Pawn(Color.FromArgb(255, 0, 255, 0), color.green);
        static Pawn gp2 = new Pawn(Color.FromArgb(255, 0, 255, 0), color.green);
        static Pawn gp3 = new Pawn(Color.FromArgb(255, 0, 255, 0), color.green);
        static Pawn gp4 = new Pawn(Color.FromArgb(255, 0, 255, 0), color.green);

        static Pawn rp1 = new Pawn(Color.FromArgb(255, 255, 0, 0), color.red);
        static Pawn rp2 = new Pawn(Color.FromArgb(255, 255, 0, 0), color.red);
        static Pawn rp3 = new Pawn(Color.FromArgb(255, 255, 0, 0), color.red);
        static Pawn rp4 = new Pawn(Color.FromArgb(255, 255, 0, 0), color.red);

        static Pawn bp1 = new Pawn(Color.FromArgb(255, 0, 0, 255), color.blue);
        static Pawn bp2 = new Pawn(Color.FromArgb(255, 0, 0, 255), color.blue);
        static Pawn bp3 = new Pawn(Color.FromArgb(255, 0, 0, 255), color.blue);
        static Pawn bp4 = new Pawn(Color.FromArgb(255, 0, 0, 255), color.blue);
        List<Pawn> rPawns = new List<Pawn>();
        List<Pawn> gPawns = new List<Pawn>();
        List<Pawn> yPawns = new List<Pawn>();
        List<Pawn> bPawns = new List<Pawn>();

        List<List<Pawn>> allPawns = new List<List<Pawn>>();
        Pawn pc = yp1;

        static CardDeck cardDeck = new CardDeck();
        int discardnum = 0;
        public enum Card { One, Two, Three, Four, Five, Seven, Eight, Ten, Eleven, Twelve, Sorry };









        public Board()
        {

            //pc.SetPosition(sender);
            this.InitializeComponent();



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

            //  if (pc == sender)
            // {
            pc.SetPosition(sender);
            //if (pc.Equals(yp1) || pc.Equals(yp2) || pc.Equals(yp3) || pc.Equals(yp4))
            if (pc.Equals(yp1))
            {
                //pc = gp1;
                pc = yp2;
                TurnLabel.Text = "Turn: Green";
            }
            //else if (pc.Equals(gp1) || pc.Equals(gp2) || pc.Equals(gp3) || pc.Equals(gp4))
            else if (pc.Equals(yp2))
            {
                //pc = rp1;
                pc = yp3;
                TurnLabel.Text = "Turn: Red";
            }
            // else if (pc.Equals(rp1) || pc.Equals(rp2) || pc.Equals(rp3) || pc.Equals(rp4))
            else if (pc.Equals(yp3))
            {
                pc = yp4;
                TurnLabel.Text = "Turn: Blue";
            }
            else if (pc.Equals(yp4))
            //else if (pc.Equals(bp1) || pc.Equals(bp2) || pc.Equals(bp3) || pc.Equals(bp4))
            {
                pc = yp1;
                TurnLabel.Text = "Turn: Yellow";
            }

            // }




        }
        private void MiniButton_Click(object sender, RoutedEventArgs e)
        {


            //  if (pc == sender)
            // {
            pc.SetPosition(sender);
            //if (pc.Equals(yp1) || pc.Equals(yp2) || pc.Equals(yp3) || pc.Equals(yp4))
            if (pc.Equals(yp1))
            {
                //pc = gp1;
                pc = yp2;
                TurnLabel.Text = "Turn: Green";
            }
            //else if (pc.Equals(gp1) || pc.Equals(gp2) || pc.Equals(gp3) || pc.Equals(gp4))
            else if (pc.Equals(yp2))
            {
                //pc = rp1;
                pc = yp3;
                TurnLabel.Text = "Turn: Red";
            }
            // else if (pc.Equals(rp1) || pc.Equals(rp2) || pc.Equals(rp3) || pc.Equals(rp4))
            else if (pc.Equals(yp3))
            {
                pc = yp4;
                TurnLabel.Text = "Turn: Blue";
            }
            else if (pc.Equals(yp4))
            //else if (pc.Equals(bp1) || pc.Equals(bp2) || pc.Equals(bp3) || pc.Equals(bp4))
            {
                pc = yp1;
                TurnLabel.Text = "Turn: Yellow";
            }

            CheckWin((Button)sender);

        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            StartGameButton.Click -= StartGameButton_Click;
            StartGameButton.Visibility = Visibility.Collapsed;
            pc = yp1; pc.SetPosition(YellowStart1);
            pc = yp2; pc.SetPosition(YellowStart2);
            pc = yp3; pc.SetPosition(YellowStart3);
            pc = yp4; pc.SetPosition(YellowStart4);
            pc = gp1; pc.SetPosition(GreenStart1);
            pc = gp2; pc.SetPosition(GreenStart2);
            pc = gp3; pc.SetPosition(GreenStart3);
            pc = gp4; pc.SetPosition(GreenStart4);
            pc = rp1; pc.SetPosition(RedStart1);
            pc = rp2; pc.SetPosition(RedStart2);
            pc = rp3; pc.SetPosition(RedStart3);
            pc = rp4; pc.SetPosition(RedStart4);
            pc = bp1; pc.SetPosition(BlueStart1);
            pc = bp2; pc.SetPosition(BlueStart2);
            pc = bp3; pc.SetPosition(BlueStart3);
            pc = bp4; pc.SetPosition(BlueStart4); pc = yp1;
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


        private void CheckWin(Button b)
        {
            var name = b.Name;
            string homeColor = "";



            if (name.Contains("Home"))
            {
                foreach (var pl in allPawns)
                {
                    int homeCount = 0;
                    foreach (var p in pl)
                    {
                        switch (p.colorName)
                        {
                            case color.red:
                                homeColor = "Red";
                                break;
                            case color.blue:
                                homeColor = "Blue";
                                break;
                            case color.green:
                                homeColor = "Green";
                                break;
                            case color.yellow:
                                homeColor = "Yellow";
                                break;
                        }

                        try
                        {

                            string spotName = ((Grid)p.pawnRect.Parent).Name;

                            if (spotName.Contains(homeColor) &&spotName.Contains("Home"))
                            {
                                homeCount += 1;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                            Debug.WriteLine(homeCount);

                    if (homeCount == 4)
                    {
                        this.Frame.Navigate(typeof(WinPage));

                    }
                    else
                    {
                        homeCount = 0;
                    }

                }
            }
        }

        private void GreenSlider1Start_Click(object sender, RoutedEventArgs e)
        {
            Slider(color.green);
        }

        private void Slider(color slideColor)
        {





        }





    }
}



