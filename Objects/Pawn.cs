using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;


namespace Sorry.Assets
{
    class Pawn
    {
        public Rectangle pawnRect { get; set; }
        private Random rng = new Random();
        public int[] position { get; set; }

        public Pawn()
        {
            pawnRect = genRect();
            position = new int[2];
            position[0] = 0;
            position[1] = 0;
        }
        public Pawn(Color c)
        {
            pawnRect = genRect();
            position = new int[2];
            position[0] = 0;
            position[1] = 0;
            SetColor(c);
        }


        public Rectangle genRect()
        {
            Rectangle r = new Rectangle();
            r.Width = 30;
            r.Height = 30;
            Color c = new Color();
            c = Color.FromArgb(255, (byte)rng.Next(1, 255), (byte)rng.Next(1, 255), (byte)rng.Next(1, 255));
            r.Fill = new SolidColorBrush(c);
            return r;
        }

        public void SetPosition(Object sender, int[] pos)
        {
            var o = (FrameworkElement)sender;
            Grid g = (Grid)o.Parent;

            var b = (FrameworkElement)sender;
            var X = Grid.GetColumn(b);
            var Y = Grid.GetRow(b);


            try
            {
                var GridParent = (Grid)pawnRect.Parent;
                GridParent.Children.Remove(pawnRect);

            }
            catch (Exception)
            { }

            g.Children.Add(pawnRect);
            if (pos != null)
            {
                Grid.SetColumn(pawnRect, pos[0]);
                Grid.SetRow(pawnRect, pos[1]);
                position[0] = pos[0];
                position[1] = pos[1];
            }
            else
            {
                Grid.SetColumn(pawnRect, X);
                Grid.SetRow(pawnRect, Y);
                position[0] = X;
                position[1] = Y;
            }
        }

        public void SetColor(Color c)
        {
            pawnRect.Fill = new SolidColorBrush(c);
        }

        public void SetSize(int width, int height)
        {
            pawnRect.Width = width;
            pawnRect.Height = height;
        }
    }
}
