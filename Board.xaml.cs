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

//The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

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
        Pawn pc = yp1;
        Pawn selectedP;

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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

//Yellow, Green, Red, Blue

            //Identify turn
            //Get selected pawn
                //set pc = to selected pawn of appropriate color
            //Alternate through selected pawns
            //Move to allowed space.
            //End Turn

            //if (TurnLabel.Text.Equals("Turn: Yellow"))
            //{
                turn t = new turn();
                Pawn p = new Pawn();
                int[] checkTurn = t.turns(sender);
                int[] pawnpos = yp1.position;
            TurnLabel.Text = "Pawn Pos" + pawnpos[0] + "||" + pawnpos[1] + "\nClickPos" + checkTurn[0] + "||" + checkTurn[1];
            // p.position = ;
            pc = yp1;
           pc.SetPosition(sender, checkTurn);


            //}
        }
        private void MiniButton_Click(object sender, RoutedEventArgs e)
        { //Don't think we need miniButton click?
//  
 //           pc.SetPosition(sender);
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            int[] pos=null;
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
