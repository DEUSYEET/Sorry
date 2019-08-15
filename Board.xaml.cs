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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

//The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sorry
{
    public sealed partial class Board : Page
    {
        static Pawn yp1 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/YellowPawn.png")));
        static Pawn yp2 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/YellowPawn.png")));
        static Pawn yp3 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/YellowPawn.png")));
        static Pawn yp4 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/YellowPawn.png")));

        static Pawn gp1 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/GreenPawn.png")));
        static Pawn gp2 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/GreenPawn.png")));
        static Pawn gp3 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/GreenPawn.png")));
        static Pawn gp4 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/GreenPawn.png")));

        static Pawn rp1 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/RedPawn.png")));
        static Pawn rp2 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/RedPawn.png")));
        static Pawn rp3 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/RedPawn.png")));
        static Pawn rp4 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/RedPawn.png")));

        static Pawn bp1 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/BluePawn.png")));
        static Pawn bp2 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/BluePawn.png")));
        static Pawn bp3 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/BluePawn.png")));
        static Pawn bp4 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/BluePawn.png")));
        int[] ypc1 = yp1.position; int[] ypc2 = yp2.position; int[] ypc3 = yp3.position; int[] ypc4 = yp4.position; int[] gpc1 = gp1.position; int[] gpc2 = gp2.position; int[] gpc3 = gp3.position; int[] gpc4 = gp4.position; int[] rpc1 = rp1.position; int[] rpc2 = rp2.position; int[] rpc3 = rp3.position; int[] rpc4 = rp4.position; int[] bpc1 = yp1.position; int[] bpc2 = yp2.position; int[] bpc3 = yp3.position; int[] bpc4 = yp4.position;
        Pawn pc = yp1;
        String carcheck = " ";

        static CardDeck cardDeck = new CardDeck();
        int discardnum = 0;
        public enum Card { One, Two, Three, Four, Five, Seven, Eight, Ten, Eleven, Twelve, Sorry };


        public Board()
        {
            //pc.SetPosition(sender);
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
            int[] pos = { 0, 0 };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Yellow, Green, Red, Blue
            turn t = new turn();
            int[] clickedPos = t.turns(sender);

            bool turn = t.OnGoingTurn();
            if (clickedPos == ypc1)
            {
                turn = t.YellowTurn(yp1, clickedPos);
            }
            else if (clickedPos == ypc2)
            {
                turn = t.YellowTurn(yp2, clickedPos);
            }
            else if (clickedPos == ypc3)
            {
                turn = t.YellowTurn(yp3, clickedPos);
            }
            else if (clickedPos == ypc4)
            {
                turn = t.YellowTurn(yp4, clickedPos);
            }
            else if (clickedPos == gpc1)
            {
                turn = t.YellowTurn(gp1, clickedPos);
            }
            else if (clickedPos == gpc2)
            {
                turn = t.YellowTurn(gp2, clickedPos);
            }
            else if (clickedPos == gpc3)
            {
                turn = t.YellowTurn(gp3, clickedPos);
            }
            else if (clickedPos == gpc4)
            {
                turn = t.YellowTurn(gp4, clickedPos);
            }
            else if (clickedPos == ypc1)
            {
                turn = t.YellowTurn(rp1, clickedPos);
            }
            else if (clickedPos == ypc2)
            {
                turn = t.YellowTurn(rp2, clickedPos);
            }
            else if (clickedPos == ypc3)
            {
                turn = t.YellowTurn(rp3, clickedPos);
            }
            else if (clickedPos == ypc4)
            {
                turn = t.YellowTurn(rp4, clickedPos);
            }
            else if (clickedPos == bpc1)
            {
                turn = t.YellowTurn(bp1, clickedPos);
            }
            else if (clickedPos == bpc2)
            {
                turn = t.YellowTurn(bp2, clickedPos);
            }
            else if (clickedPos == bpc3)
            {
                turn = t.YellowTurn(bp3, clickedPos);
            }
            else if (clickedPos == bpc4)
            {
                turn = t.YellowTurn(bp4, clickedPos);
            }
            turn = false;// t.OnGoingTurn();
        }
        private void MiniButton_Click(object sender, RoutedEventArgs e)
        {
            turn t = new turn();
            int[] clickedPos = t.turns(sender);
            if (TurnLabel.Text.Contains("Turn: Yellow"))
            {
                if (carcheck.Contains("One") || carcheck.Contains("Two") && sender == YellowStart1 && sender == YellowStart2 && sender == YellowStart3 && sender == YellowStart4)
                {
                    if (sender == YellowStart1 && ypc1 == clickedPos) { pc = yp2; TurnLabel.Text = "yoyoyo"; }
                    if (carcheck.Contains("One"))
                    {
                        pc.SetPosition(YellowSlider1End, null);
                    }
                    else if(carcheck.Contains("Two"))
                        pc.SetPosition(BlankYellowSide1, null);
                    if (clickedPos == ypc2) ypc2 = pc.position; else if (clickedPos == ypc3) ypc3 = pc.position; else if (clickedPos == ypc4) ypc4 = pc.position; else if (clickedPos == ypc1) ypc1 = pc.position;
                }
            }
        }
        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            int[] pos = null;
            StartGameButton.Click -= StartGameButton_Click;
            StartGameButton.Visibility = Visibility.Collapsed;
            pc = yp1; pc.SetPosition(YellowStart1, pos);
            pc = yp2; pc.SetPosition(YellowStart2, pos);
            pc = yp3; pc.SetPosition(YellowStart3, pos);
            pc = yp4; pc.SetPosition(YellowStart4, pos);
            pc = gp1; pc.SetPosition(GreenStart1, pos);
            pc = gp2; pc.SetPosition(GreenStart2, pos);
            pc = gp3; pc.SetPosition(GreenStart3, pos);
            pc = gp4; pc.SetPosition(GreenStart4, pos);
            pc = rp1; pc.SetPosition(RedStart1, pos);
            pc = rp2; pc.SetPosition(RedStart2, pos);
            pc = rp3; pc.SetPosition(RedStart3, pos);
            pc = rp4; pc.SetPosition(RedStart4, pos);
            pc = bp1; pc.SetPosition(BlueStart1, pos);
            pc = bp2; pc.SetPosition(BlueStart2, pos);
            pc = bp3; pc.SetPosition(BlueStart3, pos);
            pc = bp4; pc.SetPosition(BlueStart4, pos); pc = yp1;
            ypc1 = yp1.position; ypc2 = yp2.position; ypc3 = yp3.position; ypc4 = yp4.position; gpc1 = gp1.position; gpc2 = gp2.position; gpc3 = gp3.position; gpc4 = gp4.position; rpc1 = rp1.position; rpc2 = rp2.position; rpc3 = rp3.position; rpc4 = rp4.position; bpc1 = yp1.position; bpc2 = yp2.position; bpc3 = yp3.position; int[] bpc4 = yp4.position;
        }
        private void FaceUpCard_Click(object sender, RoutedEventArgs e)
        {
            Card card;
            card = (Card)cardDeck.drawCard();
            FaceUpCard.Content = card.ToString();
            carcheck = card.ToString();
            discardnum++;
            if (discardnum >= 45)
            {
                discardnum = 0;
            }
            DiscardPile.Text = discardnum.ToString();
        }
    }
}
