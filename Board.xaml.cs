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

        List<object> availableSpots = new List<object>();
        bool onGoingTurn = false;
        Pawn selectedP;


        Pawn sorryPawn;

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

        //Yellow, Green, Red, Blue
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button sendButton = (Button)sender;
            var o = (FrameworkElement)sender;
            Grid g = (Grid)o.Parent;
            var b = (FrameworkElement)sender;
            var X = Grid.GetColumn(b);
            var Y = Grid.GetRow(b);

            int[] helper = { X, Y };

            //if (availableSpots.Contains(sender) && 
            if (carcheck.Contains("Sorry"))
            {


                sorryPawn = everyPawn.First(p => p.position[0]==Grid.GetColumn(sendButton)&& p.position[1] == Grid.GetRow(sendButton));

                pc.SetPosition(sender, null);

                if (TurnLabel.Text == "Turn: Yellow") TurnLabel.Text = "Turn: Green"; else if (TurnLabel.Text == "Turn: Green") TurnLabel.Text = "Turn: Red"; else if (TurnLabel.Text == "Turn Red") TurnLabel.Text = "Turn: Blue"; else if (TurnLabel.Text == "Turn: Blue") TurnLabel.Text = "Turn: Yellow";
                onGoingTurn = false;

                sendHome(sorryPawn);
            }
            if (selectedP != null)
            {
                pc = selectedP;
                MovePawn(selectedP, cardDeck.CardNum());
                pc.SetPosition(sender, selectedP.position);
                FrameworkElement button = (Button)sender;
                selectedP = null;
                availableSpots = null;
                //change turn
                if (TurnLabel.Text == "Turn: Yellow") TurnLabel.Text = "Turn: Green"; else if (TurnLabel.Text == "Turn: Green") TurnLabel.Text = "Turn: Red"; else if (TurnLabel.Text == "Turn: Red") TurnLabel.Text = "Turn: Blue"; else if (TurnLabel.Text == "Turn: Blue") TurnLabel.Text = "Turn: Yellow";
                onGoingTurn = false;
            }


            else
            {
                if (TurnLabel.Text == "Turn: Yellow")
                {
                    if (yp1.position[0] == helper[0] && yp1.position[1] == helper[1]) selectedP = yp1; if (yp2.position[0] == helper[0] && yp2.position[1] == helper[1]) selectedP = yp2; if (yp3.position[0] == helper[0] && yp3.position[1] == helper[1]) selectedP = yp3; if (yp4.position[0] == helper[0] && yp4.position[1] == helper[1]) selectedP = yp4;
                }
                else if (TurnLabel.Text == "Turn: Green")
                {
                    if (gp1.position[0] == helper[0] && gp1.position[1] == helper[1]) selectedP = gp1; if (gp2.position[0] == helper[0] && gp2.position[1] == helper[1]) selectedP = gp2; if (gp3.position[0] == helper[0] && gp3.position[1] == helper[1]) selectedP = gp3; if (gp4.position[0] == helper[0] && gp4.position[1] == helper[1]) selectedP = gp4;
                }
                else if (TurnLabel.Text == "Turn: Red")
                {
                    if (rp1.position[0] == helper[0] && rp1.position[1] == helper[1]) selectedP = rp1; if (rp2.position[0] == helper[0] && rp2.position[1] == helper[1]) selectedP = rp2; if (rp3.position[0] == helper[0] && rp3.position[1] == helper[1]) selectedP = rp3; if (rp4.position[0] == helper[0] && rp4.position[1] == helper[1]) selectedP = rp4;

                }
                else if (TurnLabel.Text == "Turn: Blue")
                {
                    if (bp1.position[0] == helper[0] && bp1.position[1] == helper[1]) selectedP = bp1; if (bp2.position[0] == helper[0] && bp2.position[1] == helper[1]) selectedP = bp2; if (bp3.position[0] == helper[0] && bp3.position[1] == helper[1]) selectedP = bp3; if (bp4.position[0] == helper[0] && bp4.position[1] == helper[1]) selectedP = bp4;
                }
                
            }



            turn t = new turn();
            int[] clickedPos = t.turns(sender);

            bool turn = t.OnGoingTurn();

            turn = false;// t.OnGoingTurn();
        }

        private void CheckSlide(Pawn p)
        {
            var sT = BoardGrid.Children.Where(b => b.GetType().Equals(typeof(Button)));
            List<FrameworkElement> slideTiles = new List<FrameworkElement>();

            foreach(var t in sT)
            {
                Button b = (Button)t;

                if (b.Name.Contains("Slide") && b.Name.Contains("Start"))
                {
                    slideTiles.Add(b);
                }
            }
            
            
                foreach(Button b in slideTiles)
                {

                    if (p.position[0] == Grid.GetColumn(b)&& p.position[1] == Grid.GetRow(b))
                    {
                        Slider(b, p);
                    }


                }
            


        }

        private void MiniButton_Click(object sender, RoutedEventArgs e)
        {
            turn t = new turn();
            var o = (FrameworkElement)sender;
            Grid g = (Grid)o.Parent;
            var b = (FrameworkElement)sender;
            var X = Grid.GetColumn(b);
            var Y = Grid.GetRow(b);
            Button sendButton = (Button)sender;

            int[] helper = { X, Y };

            int[] clickedPos = t.turns(sender);
            if (TurnLabel.Text.Contains("Turn: Yellow"))
            {
                if (carcheck.Contains("Sorry"))
                {
                    if (sendButton.Name.Contains("YellowStart"))
                    {
                        pc = everyPawn.First(p => p.positionName.Equals(sendButton.Name));
                    }

                }
                else if (carcheck.Contains("One") || carcheck.Contains("Two"))
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
                            pc.SetPosition(YellowSlider1End, null);
                        if (clickedPos == ypc2) ypc2 = pc.position; else if (clickedPos == ypc3) ypc3 = pc.position; else if (clickedPos == ypc4) ypc4 = pc.position; else if (clickedPos == ypc1) ypc1 = pc.position;
                    }
                }
            }

            if (TurnLabel.Text.Contains("Turn: Green"))
            {
                if (carcheck.Contains("Sorry"))
                {
                    if (sendButton.Name.Contains("GreenStart"))
                    {
                        pc = everyPawn.First(p => p.positionName.Equals(sendButton.Name));
                    }

                }
                else if (carcheck.Contains("One") || carcheck.Contains("Two"))
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
                            pc.SetPosition(GreenSlider1End, null);
                        if (clickedPos == gpc2) gpc2 = pc.position; else if (clickedPos == gpc3) gpc3 = pc.position; else if (clickedPos == gpc4) gpc4 = pc.position; else if (clickedPos == gpc1) gpc1 = pc.position;
                    }
                }
            }
            if (TurnLabel.Text.Contains("Turn: Red"))
            {
                if (carcheck.Contains("Sorry"))
                {
                    if (sendButton.Name.Contains("RedStart"))
                    {
                        pc = everyPawn.First(p => p.positionName.Equals(sendButton.Name));
                    }

                }
                else if (carcheck.Contains("One") || carcheck.Contains("Two"))
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
                            pc.SetPosition(RedSlider1End, null);
                        if (clickedPos == rpc2) rpc2 = pc.position; else if (clickedPos == rpc3) rpc3 = pc.position; else if (clickedPos == rpc4) rpc4 = pc.position; else if (clickedPos == rpc1) rpc1 = pc.position;
                    }
                }
            }
            if (TurnLabel.Text.Contains("Turn: Blue"))
            {
                if (carcheck.Contains("Sorry"))
                {
                    if (sendButton.Name.Contains("BlueStart"))
                    {
                        pc = everyPawn.First(p => p.positionName.Equals(sendButton.Name));
                    }

                }
                else if (carcheck.Contains("One") || carcheck.Contains("Two"))
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
                            pc.SetPosition(BlueSlider1End, null);
                        if (clickedPos == bpc2) bpc2 = pc.position; else if (clickedPos == bpc3) bpc3 = pc.position; else if (clickedPos == bpc4) bpc4 = pc.position; else if (clickedPos == bpc1) bpc1 = pc.position;
                    }
                }
            }
            //pc.pawnColor = yp1.pawnColor;
            Debug.WriteLine(pc.pawnColor);

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
            pc = gp1; pc.SetPosition(GreenSlider1End, pos);
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

        private void ForfeitTurnButton_Click(object sender, RoutedEventArgs e)
        {
            if (TurnLabel.Text == "Turn: Yellow") TurnLabel.Text = "Turn: Green"; else if (TurnLabel.Text == "Turn: Green") TurnLabel.Text = "Turn: Red"; else if (TurnLabel.Text == "Turn: Red") TurnLabel.Text = "Turn: Blue"; else if (TurnLabel.Text == "Turn Blue") TurnLabel.Text = "Turn: Yellow";
            onGoingTurn = false;
        }

        private void FaceDownCard_Click(object sender, RoutedEventArgs e)
        {
            Card card;
            card = (Card)cardDeck.drawCard();
            carcheck = card.ToString();
            string cardLink = "Images/" + card.ToString() + "Card.png";
            FaceUpCard.Source = (new BitmapImage(new Uri("ms-appx:///" + cardLink)));
            discardnum++;
            if (discardnum >= 45)
            {
                discardnum = 0;
            }

            Debug.WriteLine(card);
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
                

                if (pawn.pawnColor == color.red && tempPosition[0] > 2 && selectedP.position[0] <= 2)
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
                pc.SetPosition(tempPosition[0], tempPosition[1]);


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
                pawn.position = tempPosition;


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
                pawn.position = tempPosition;


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
                pawn.position = tempPosition;



            }


            pc.positionName = GetGridCell(pawn.position[0], pawn.position[1]).Name;

            CheckSlide(pawn);
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

        private void Slider(FrameworkElement space, Pawn pawn)
        {
            int longSlide = 4;
            int shortSlide = 3;
            if (!checkColorMatch(space))
            {


                if (space.Name.Contains("Green"))
                {
                    if (space.Name.Contains("2"))
                    {

                        var kicked = everyPawn.Where(p => (p.position[0] == Grid.GetColumn(GreenSlider2Middle3) || p.position[0] == Grid.GetColumn(GreenSlider2Middle2) || p.position[0] == Grid.GetColumn(GreenSlider2Middle1) || p.position[0] == Grid.GetColumn(GreenSlider2End) &&
                       (p.position[1] == Grid.GetRow(GreenSlider2Middle3) || p.position[1] == Grid.GetRow(GreenSlider2Middle2) || p.position[1] == Grid.GetRow(GreenSlider2Middle1) || p.position[1] == Grid.GetRow(GreenSlider2End))));
                        foreach (var pk in kicked)
                        {
                            sendHome(pk);
                        }

                        pawn.SetPosition(GreenSlider2End, null);

                    }
                    else
                    {
                        var kicked = everyPawn.Where(p => (p.position[0] == Grid.GetColumn(GreenSlider1Middle2) || p.position[0] == Grid.GetColumn(GreenSlider1Middle1)) || p.position[0] == Grid.GetColumn(GreenSlider1End) &&
                      (p.position[1] == Grid.GetRow(GreenSlider1Middle2) || p.position[1] == Grid.GetRow(GreenSlider1Middle1) || p.position[1] == Grid.GetRow(GreenSlider1End)));
                        foreach (var pk in kicked)
                        {
                            sendHome(pk);
                        }

                        pawn.SetPosition(GreenSlider1End, null);
                    }
                }
                else if (space.Name.Contains("Blue"))
                {
                    if (space.Name.Contains("2"))
                    {

                        var kicked = everyPawn.Where(p => (p.position[0] == Grid.GetColumn(BlueSlider2Middle3) || p.position[0] == Grid.GetColumn(BlueSlider2Middle2) || p.position[0] == Grid.GetColumn(BlueSlider2Middle1) || p.position[0] == Grid.GetColumn(BlueSlider2End) &&
                       (p.position[1] == Grid.GetRow(BlueSlider2Middle3) || p.position[1] == Grid.GetRow(BlueSlider2Middle2) || p.position[1] == Grid.GetRow(BlueSlider2Middle1) || p.position[1] == Grid.GetRow(BlueSlider2End))));
                        foreach (var pk in kicked)
                        {
                            sendHome(pk);
                        }

                        pawn.SetPosition(BlueSlider2End, null);

                    }
                    else
                    {
                        var kicked = everyPawn.Where(p => (p.position[0] == Grid.GetColumn(BlueSlider1Middle2) || p.position[0] == Grid.GetColumn(BlueSlider1Middle1)) || p.position[0] == Grid.GetColumn(BlueSlider1End) &&
                      (p.position[1] == Grid.GetRow(BlueSlider1Middle2) || p.position[1] == Grid.GetRow(BlueSlider1Middle1) || p.position[1] == Grid.GetRow(BlueSlider1End)));
                        foreach (var pk in kicked)
                        {
                            sendHome(pk);
                        }
                        pawn.SetPosition(BlueSlider1End, null);

                    }
                }
                else if (space.Name.Contains("Yellow"))
                {
                    {
                        if (space.Name.Contains("2"))
                        {

                            var kicked = everyPawn.Where(p => (p.position[0] == Grid.GetColumn(YellowSlider2Middle3) || p.position[0] == Grid.GetColumn(YellowSlider2Middle2) || p.position[0] == Grid.GetColumn(YellowSlider2Middle1) || p.position[0] == Grid.GetColumn(YellowSlider2End) &&
                           (p.position[1] == Grid.GetRow(YellowSlider2Middle3) || p.position[1] == Grid.GetRow(YellowSlider2Middle2) || p.position[1] == Grid.GetRow(YellowSlider2Middle1) || p.position[1] == Grid.GetRow(YellowSlider2End))));
                            foreach (var pk in kicked)
                            {
                                sendHome(pk);
                            }
                            pawn.SetPosition(YellowSlider2End, null);


                        }
                        else
                        {
                            var kicked = everyPawn.Where(p => (p.position[0] == Grid.GetColumn(YellowSlider1Middle2) || p.position[0] == Grid.GetColumn(YellowSlider1Middle1)) || p.position[0] == Grid.GetColumn(YellowSlider1End) &&
                          (p.position[1] == Grid.GetRow(YellowSlider1Middle2) || p.position[1] == Grid.GetRow(YellowSlider1Middle1) || p.position[1] == Grid.GetRow(YellowSlider1End)));
                            foreach (var pk in kicked)
                            {
                                sendHome(pk);
                            }
                            pawn.SetPosition(YellowSlider1End, null);

                        }
                    }
                }
                else if (space.Name.Contains("Red"))
                {
                    if (space.Name.Contains("2"))
                    {

                        var kicked = everyPawn.Where(p => (p.position[0] == Grid.GetColumn(RedSlider2Middle3) || p.position[0] == Grid.GetColumn(RedSlider2Middle2) || p.position[0] == Grid.GetColumn(RedSlider2Middle1) || p.position[0] == Grid.GetColumn(RedSlider2End) &&
                       (p.position[1] == Grid.GetRow(RedSlider2Middle3) || p.position[1] == Grid.GetRow(RedSlider2Middle2) || p.position[1] == Grid.GetRow(RedSlider2Middle1) || p.position[1] == Grid.GetRow(RedSlider2End))));
                        foreach (var pk in kicked)
                        {
                            sendHome(pk);
                        }
                        pawn.SetPosition(RedSlider2End, null);


                    }
                    else
                    {
                        var kicked = everyPawn.Where(p => (p.position[0] == Grid.GetColumn(RedSlider1Middle2) || p.position[0] == Grid.GetColumn(RedSlider1Middle1)) || p.position[0] == Grid.GetColumn(RedSlider1End) &&
                      (p.position[1] == Grid.GetRow(RedSlider1Middle2) || p.position[1] == Grid.GetRow(RedSlider1Middle1) || p.position[1] == Grid.GetRow(RedSlider1End)));
                        foreach (var pk in kicked)
                        {
                            sendHome(pk);
                        }
                        pawn.SetPosition(RedSlider1End, null);

                    }
                }



                }


        }

        private FrameworkElement GetGridCell(int row, int col)
        {
            foreach (FrameworkElement cell in BoardGrid.Children)
            {
                if (Grid.GetRow(cell) == row && Grid.GetColumn(cell) == col)
                {
                    Debug.WriteLine($"Returned {cell.Name}");
                    return cell;
                }
            }
            throw new ArgumentOutOfRangeException("Row and column must be within BoardGrid");
        }

        private void sendHome(Pawn pk)
        {
            var hG = BoardGrid.Children.Where(b => b.GetType().Equals(typeof(Grid)));
            
            List<FrameworkElement> homeTiles = new List<FrameworkElement>();
            string pkColor = checkColor(pk);


            foreach (var g in hG)
            {
                Grid grid = (Grid)g;
                foreach (var b in grid.Children.Where(c=>c.GetType().Equals(typeof(Button))))
                {
                    Button homeButton = (Button)b;

                    if (homeButton.Name.Contains(pkColor)&&homeButton.Name.Contains("Start"))
                    {
                        homeTiles.Add(homeButton);
                    }
                }
            }


            foreach(Pawn p in everyPawn.Where(e=>e.positionName.Contains("Start") && !e.positionName.Contains("Slider")&&e.positionName.Contains(pkColor)))
            {

                    homeTiles.Remove(homeTiles.First(h=> h.Name.Equals(p.positionName)));
            }



            pk.SetPosition(homeTiles[0], null);


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
