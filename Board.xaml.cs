﻿using Sorry.Assets;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

//The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sorry
{
    public sealed partial class Board : Page
    {
        static Pawn yp1 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/YellowPawn.png")), color.yellow);
        static Pawn yp2 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/YellowPawn.png")), color.yellow);
        static Pawn yp3 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/YellowPawn.png")), color.yellow);
        static Pawn yp4 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/YellowPawn.png")), color.yellow);

        static Pawn gp1 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/GreenPawn.png")), color.green);
        static Pawn gp2 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/GreenPawn.png")), color.green);
        static Pawn gp3 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/GreenPawn.png")), color.green);
        static Pawn gp4 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/GreenPawn.png")), color.green);

        static Pawn rp1 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/RedPawn.png")), color.red);
        static Pawn rp2 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/RedPawn.png")), color.red);
        static Pawn rp3 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/RedPawn.png")), color.red);
        static Pawn rp4 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/RedPawn.png")), color.red);

        static Pawn bp1 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/BluePawn.png")), color.blue);
        static Pawn bp2 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/BluePawn.png")), color.blue);
        static Pawn bp3 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/BluePawn.png")), color.blue);
        static Pawn bp4 = new Pawn(new BitmapImage(new Uri("ms-appx:///Images/BluePawn.png")), color.blue);
        int[] ypc1 = yp1.position; int[] ypc2 = yp2.position; int[] ypc3 = yp3.position; int[] ypc4 = yp4.position; int[] gpc1 = gp1.position; int[] gpc2 = gp2.position; int[] gpc3 = gp3.position; int[] gpc4 = gp4.position; int[] rpc1 = rp1.position; int[] rpc2 = rp2.position; int[] rpc3 = rp3.position; int[] rpc4 = rp4.position; int[] bpc1 = yp1.position; int[] bpc2 = yp2.position; int[] bpc3 = yp3.position; int[] bpc4 = yp4.position;
        Pawn pc = yp1;

        List<Pawn> rPawns = new List<Pawn>();
        List<Pawn> gPawns = new List<Pawn>();
        List<Pawn> yPawns = new List<Pawn>();
        List<Pawn> bPawns = new List<Pawn>();

        List<List<Pawn>> allPawns = new List<List<Pawn>>();
        List<Pawn> everyPawn = new List<Pawn>();

        String carcheck = " ";
        static CardDeck cardDeck = new CardDeck();
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

            everyPawn.AddRange(yPawns);
            everyPawn.AddRange(gPawns);
            everyPawn.AddRange(rPawns);
            everyPawn.AddRange(bPawns);
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


            FrameworkElement button = (Button)sender;
            if (button.Name.Contains("Slider") && button.Name.Contains("Start"))
            {
                Slider(button);
                //pc.SetPosition(1, 1);
            }

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
            var o = (FrameworkElement)sender;
            Grid g = (Grid)o.Parent;
            var b = (FrameworkElement)sender;
            var X = Grid.GetColumn(b);
            var Y = Grid.GetRow(b);

            int[] helper = { X, Y };

            int[] clickedPos = t.turns(sender);
            if (TurnLabel.Text.Contains("Turn: Yellow"))
            {
                if (carcheck.Contains("One") || carcheck.Contains("Two"))
                {
                    if (sender == YellowStart1 || sender == YellowStart2 || sender == YellowStart3 || sender == YellowStart4)
                    {
                        if (yp1.position[0] == helper[0] && yp1.position[1] == helper[1]) pc = yp1; if (yp2.position[0] == helper[0] && yp2.position[1] == helper[1]) pc = yp2; if (yp3.position[0] == helper[0] && yp3.position[1] == helper[1]) pc = yp3; if (yp4.position[0] == helper[0] && yp4.position[1] == helper[1]) pc = yp4;
                        if (carcheck.Contains("One"))
                        {
                            TurnLabel.Text = "Turn: Green";
                            pc.SetPosition(YellowSlider1End, null);
                        }
                        else if (carcheck.Contains("Two"))
                            pc.SetPosition(BlankYellowSide1, null);
                        if (clickedPos == ypc2) ypc2 = pc.position; else if (clickedPos == ypc3) ypc3 = pc.position; else if (clickedPos == ypc4) ypc4 = pc.position; else if (clickedPos == ypc1) ypc1 = pc.position;
                    }
                }
                if (TurnLabel.Text.Contains("Turn: Green"))
                {
                    if (carcheck.Contains("One") || carcheck.Contains("Two"))
                    {
                        if (sender == GreenStart1 || sender == GreenStart2 || sender == GreenStart3 || sender == GreenStart4)
                        {
                            if (gp1.position[0] == helper[0] && gp1.position[1] == helper[1]) pc = gp1; if (gp2.position[0] == helper[0] && gp2.position[1] == helper[1]) pc = gp2; if (gp3.position[0] == helper[0] && gp3.position[1] == helper[1]) pc = gp3; if (gp4.position[0] == helper[0] && gp4.position[1] == helper[1]) pc = gp4;
                            if (carcheck.Contains("One"))
                            {
                                TurnLabel.Text = "Turn: Red";
                                pc.SetPosition(GreenSlider1End, null);
                            }
                            else if (carcheck.Contains("Two"))
                                pc.SetPosition(BlankGreenSide1, null);
                            if (clickedPos == gpc2) gpc2 = pc.position; else if (clickedPos == gpc3) gpc3 = pc.position; else if (clickedPos == gpc4) gpc4 = pc.position; else if (clickedPos == gpc1) gpc1 = pc.position;
                        }
                    }
                }
                if (TurnLabel.Text.Contains("Turn: Red"))
                {
                    if (carcheck.Contains("One") || carcheck.Contains("Two"))
                    {
                        if (sender == RedStart1 || sender == RedStart2 || sender == RedStart3 || sender == RedStart4)
                        {
                            if (rp1.position[0] == helper[0] && rp1.position[1] == helper[1]) pc = rp1; if (rp2.position[0] == helper[0] && rp2.position[1] == helper[1]) pc = rp2; if (rp3.position[0] == helper[0] && rp3.position[1] == helper[1]) pc = rp3; if (rp4.position[0] == helper[0] && rp4.position[1] == helper[1]) pc = rp4;
                            if (carcheck.Contains("One"))
                            {
                                TurnLabel.Text = "Turn: Blue";
                                pc.SetPosition(RedSlider1End, null);
                            }
                            else if (carcheck.Contains("Two"))
                                pc.SetPosition(BlankRedSide1, null);
                            if (clickedPos == rpc2) rpc2 = pc.position; else if (clickedPos == rpc3) rpc3 = pc.position; else if (clickedPos == rpc4) rpc4 = pc.position; else if (clickedPos == rpc1) rpc1 = pc.position;
                        }
                    }
                }
                if (TurnLabel.Text.Contains("Turn: Blue"))
                {
                    if (carcheck.Contains("One") || carcheck.Contains("Two"))
                    {
                        if (sender == BlueStart1 || sender == BlueStart2 || sender == BlueStart3 || sender == BlueStart4)
                        {
                            if (bp1.position[0] == helper[0] && bp1.position[1] == helper[1]) pc = bp1; if (bp2.position[0] == helper[0] && bp2.position[1] == helper[1]) pc = bp2; if (bp3.position[0] == helper[0] && bp3.position[1] == helper[1]) pc = bp3; if (bp4.position[0] == helper[0] && bp4.position[1] == helper[1]) pc = bp4;
                            if (carcheck.Contains("One"))
                            {
                                TurnLabel.Text = "Turn: Yellow";
                                pc.SetPosition(BlueSlider1End, null);
                            }
                            else if (carcheck.Contains("Two"))
                                pc.SetPosition(BlankBlueSide1, null);
                            if (clickedPos == bpc2) bpc2 = pc.position; else if (clickedPos == bpc3) bpc3 = pc.position; else if (clickedPos == bpc4) bpc4 = pc.position; else if (clickedPos == bpc1) bpc1 = pc.position;
                        }
                    }
                }
            }

            pc.pawnColor = yp1.pawnColor;
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
            //FaceUpCard.Content = card.ToString();
            carcheck = card.ToString();
            discardnum++;
            if (discardnum >= 45)
            {
                discardnum = 0;
            }
            //DiscardPile.Text = discardnum.ToString();
        }

        private void FaceDownCard_Click(object sender, RoutedEventArgs e)
        {
            Card card;
            card = (Card)cardDeck.drawCard();
            string cardLink = "Images/" + card.ToString() + "Card.png";
            FaceUpCard.Source = (new BitmapImage(new Uri("ms-appx:///" + cardLink)));
            carcheck = card.ToString();
            discardnum++;
            if (discardnum >= 45)
            {
                discardnum = 0;
            }
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
                        switch (p.pawnColor)
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

                            if (spotName.Contains(homeColor) && spotName.Contains("Home"))
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

        //private void GreenSlider1Start_Click(object sender, RoutedEventArgs e)
        //{

        //    var send = (FrameworkElement)sender;
        //    Slider(color.green, send);
        //}

        private void Slider(FrameworkElement space)
        {
            int longSlide = 4;
            int shortSlide = 3;
            if (!checkColorMatch(space))
            {


                if (space.Name.Contains("Green"))
                {
                    if (space.Name.Contains("2"))
                    {
                        //pc.SetPosition(pc.position[0], pc.position[1] - longSlide);

                    }
                    else
                    {
                        pc.SetPosition(GreenSlider1End, null);

                    }
                }
                else if (space.Name.Contains("Blue"))
                {
                    if (space.Name.Contains("2"))
                    {
                       // pc.SetPosition(pc.position[0], pc.position[1] + longSlide);

                    }
                    else
                    {
                        //pc.SetPosition(pc.position[0], pc.position[1] + shortSlide);

                    }
                }
                else if (space.Name.Contains("Yellow"))
                {
                    if (space.Name.Contains("2"))
                    {
                      //  pc.SetPosition(pc.position[0] - longSlide, pc.position[1]);

                    }
                    else
                    {
                      //  pc.SetPosition(pc.position[0] - shortSlide, pc.position[1]);

                    }
                }
                else if (space.Name.Contains("Red"))
                {
                    if (space.Name.Contains("2"))
                    {
                      //  pc.SetPosition(pc.position[0] + longSlide, pc.position[1]);

                    }
                    else
                    {
                      //  pc.SetPosition(pc.position[0] + shortSlide, pc.position[1]);

                    }
                }



            }


        }


        private bool checkColorMatch(FrameworkElement space)
        {
            color spaceColor = 0;

            if (space.Name.Contains("Green"))
            {
                spaceColor = color.green;
            }
            else if (space.Name.Contains("Blue"))
            {
                spaceColor = color.blue;
                  
            }
            else if (space.Name.Contains("Yellow"))
            {
                spaceColor = color.yellow;

            }
            else if (space.Name.Contains("Red"))
            {
                spaceColor = color.red;

            }
            bool colorMatch = pc.pawnColor.Equals(spaceColor);

            return colorMatch;
        }

        private string checkColor(Pawn pawn)
        {
            string c = "";
            switch (pawn.pawnColor)
            {
                case color.blue:
                    c = "Blue";
                    break;
                case color.red:
                    c = "Red";
                    break;
                case color.green:
                    c = "Green";
                    break;
                case color.yellow:
                    c = "Yellow";
                    break;
            }

            return c;

        }
    }
}
