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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

//The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sorry
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
        Pawn pc = yp1;
        Pawn selectedP;

        List<Pawn> rPawns = new List<Pawn>();
        List<Pawn> gPawns = new List<Pawn>();
        List<Pawn> yPawns = new List<Pawn>();
        List<Pawn> bPawns = new List<Pawn>();

        List<List<Pawn>> allPawns = new List<List<Pawn>>();
        List<Pawn> everyPawn = new List<Pawn>();




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
            //Pawn p = new Pawn();
            int[] checkTurn = t.turns(sender);
            int[] pawnpos = yp1.position;
            TurnLabel.Text = "Pawn Pos" + pawnpos[0] + "||" + pawnpos[1] + "\nClickPos" + checkTurn[0] + "||" + checkTurn[1];
            // p.position = ;
            pc = yp1;
            pc.SetPosition(sender, checkTurn);

            FrameworkElement button = (Button)sender;
            if (button.Name.Contains("Slider") && button.Name.Contains("Start"))
            {
                Slider(button);
                //pc.SetPosition(1, 1);
            }
            //}
            pc.pawnColor = yp1.pawnColor;
        }
        private void MiniButton_Click(object sender, RoutedEventArgs e)
        { //Don't think we need miniButton click?
          //  
          //           pc.SetPosition(sender);

            turn t = new turn();
            //Pawn p = new Pawn();
            int[] checkTurn = t.turns(sender);
            int[] pawnpos = yp1.position;
            TurnLabel.Text = "Pawn Pos" + pawnpos[0] + "||" + pawnpos[1] + "\nClickPos" + checkTurn[0] + "||" + checkTurn[1];
            // p.position = ;
            pc = yp1;
            pc.SetPosition(sender, checkTurn);
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
        }
        private void FaceDownCard_Click(object sender, RoutedEventArgs e)
        {
            Card card;
            card = (Card)cardDeck.drawCard();
            string cardLink = "Images/" + card.ToString() + "Card.png";
            FaceUpCard.Source = (new BitmapImage(new Uri("ms-appx:///" + cardLink)));
            discardnum++;
            if (discardnum >= 45)
            {
                discardnum = 0;
            }
        }

        private void RedPawnHomeMovement(int[] pawn)
        {
            int[] tempPosition = pawn;

            //Red Pawn Home Movement

            //when tempPosition[0] >= 2 move down towards home 
            if (tempPosition[0] > 2)
            {
                int temp = tempPosition[0];
                temp -= 2;
                if (temp > 6)
                {
                    //Player skips turn
                    // break or return pawn.position not sure yet if need to return
                }
                else
                {
                    //sudo code fro now need check for actual home position since that spot is its own mini grid
                    tempPosition[0] = 2;
                    tempPosition[1] += temp;
                }
            }
        }

        private void BluePawnHomeMovement(int[] pawn)
        {
            int[] tempPosition = pawn;

            //Red Pawn Home Movement

            //when tempPosition[0] >= 2 move down towards home 
            if (tempPosition[1] > 2)
            {
                int temp = tempPosition[0];
                temp -= 2;
                if (temp > 6)
                {
                    //Player skips turn
                    // break or return pawn.position not sure yet if need to return
                }
                else
                {
                    //sudo code fro now need check for actual home position since that spot is its own mini grid
                    tempPosition[0] = 2;
                    tempPosition[1] += temp;
                }
            }
        }

        private void YellowPawnHomeMovement(int[] pawn)
        {
            int[] tempPosition = pawn;

            if (tempPosition[0] < 13)
            {
                int temp = tempPosition[1];
                temp -= 2;
                tempPosition[0] = 13;
                if (temp > 6)
                {
                    //player Skip Turn
                    //break or return pawn.position
                }
                else
                {
                    //sudo code need check for if value is 6 because home space is its own grid
                    tempPosition[1] -= temp;
                }

            }
            else
            {
                //break or return
            }
        }

        private void GreenPawnHomeMovement(int[] pawn)
        {
            int[] tempPosition = pawn;
            int temp = tempPosition[1];
            temp -= 2;
            tempPosition[1] = 13;
            if (temp > 6)
            {
                //player Skip Turn
                //break or return pawn.position
            }
            else
            {
                //sudo code need check for if value is 6 because home space is its own grid
                tempPosition[0] += temp;
            }
        }

        private void MovePawn(Pawn pawn, int value)
        {
            int[] tempPosition = pawn.position;

            if (tempPosition[0] >= 0 && tempPosition[1] == 0)
            {
                tempPosition[0] += value;

                if (pawn.pawnColor == color.red && tempPosition[0] > 2 && pawn.position[0] <= 2)
                {
                    RedPawnHomeMovement(tempPosition);
                }
                if (tempPosition[0] > 15)
                {
                    int temp = tempPosition[0];
                    temp -= 15;
                    tempPosition[0] = 15;
                    tempPosition[1] += temp;
                    if (pawn.pawnColor == color.blue && tempPosition[1] > 2 && pawn.position[1] <= 2)
                    {
                        BluePawnHomeMovement(tempPosition);
                    }
                }
            }
            else if (tempPosition[0] == 15 && tempPosition[1] >= 0)
            {
                tempPosition[1] += value;

                // Blue Home Movement
                if (pawn.pawnColor == color.blue && tempPosition[1] > 2 && pawn.position[1] <= 2)
                {
                    BluePawnHomeMovement(tempPosition);
                }

                if (tempPosition[1] > 15)
                {
                    //sub remaing from tempPosition[0]
                    int temp = tempPosition[1];
                    temp -= 15;
                    tempPosition[1] = 15;
                    tempPosition[0] -= temp;
                    if (pawn.pawnColor == color.yellow && tempPosition[0] < 13 && tempPosition[0] >= 13)
                    {
                        YellowPawnHomeMovement(tempPosition);
                    }
                }
            }
            else if (tempPosition[0] <= 15 && tempPosition[1] == 15)
            {
                tempPosition[0] -= value;

                //Yellow Home Movement
                if (pawn.pawnColor == color.yellow && tempPosition[0] < 13 && pawn.position[0] >= 13)
                {
                    YellowPawnHomeMovement(tempPosition);
                }

                if (tempPosition[0] < 0)
                {
                    //multiple the remaining by -1 then sub remaing from tempPosition[2]
                    int temp = tempPosition[0];
                    temp = (temp * (-1));
                    tempPosition[0] = 0;
                    tempPosition[1] -= temp;
                    if (pawn.pawnColor == color.green && tempPosition[1] < 13 && pawn.position[1] >= 13)
                    {
                        GreenPawnHomeMovement(tempPosition);
                    }
                }
            }
            else if (tempPosition[0] == 0 && tempPosition[1] <= 15)
            {
                tempPosition[1] -= value;

                // Green Home Movement
                if (pawn.pawnColor == color.green && tempPosition[1] < 13 && pawn.position[1] >= 13)
                {
                    GreenPawnHomeMovement(tempPosition);
                }
                if (tempPosition[1] < 0)
                {
                    //multiple the remaining by -1 then add remaing to tempPosition[1]
                    int temp = tempPosition[1];
                    temp = (temp * (-1));
                    tempPosition[1] = 0;
                    tempPosition[0] += temp;
                    if (pawn.pawnColor == color.red && tempPosition[0] > 2 && pawn.position[0] <= 2)
                    {
                        RedPawnHomeMovement(tempPosition);
                    }
                }
            }
            //highlight to show possible position
            //click moves to that positon
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
                        pc.SetPosition(pc.position[0], pc.position[1] - longSlide);

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
                        pc.SetPosition(pc.position[0], pc.position[1] + longSlide);

                    }
                    else
                    {
                        pc.SetPosition(pc.position[0], pc.position[1] + shortSlide);

                    }
                }
                else if (space.Name.Contains("Yellow"))
                {
                    if (space.Name.Contains("2"))
                    {
                        pc.SetPosition(pc.position[0] - longSlide, pc.position[1]);

                    }
                    else
                    {
                        pc.SetPosition(pc.position[0] - shortSlide, pc.position[1]);

                    }
                }
                else if (space.Name.Contains("Red"))
                {
                    if (space.Name.Contains("2"))
                    {
                        pc.SetPosition(pc.position[0] + longSlide, pc.position[1]);

                    }
                    else
                    {
                        pc.SetPosition(pc.position[0] + shortSlide, pc.position[1]);

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