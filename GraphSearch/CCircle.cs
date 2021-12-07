using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphSearch
{
    public class CCircle
    {
        public double X, Y;
        private int Radius = 50;
        public bool Selected { get; private set; }

        public Ellipse e;

        public CCircle(double x, double y)
        {
            X = x;
            Y = y;
            Selected = true;
        }
        public bool HitTest(double x,double y)
        {
            if ((Math.Pow((x-X),2) + Math.Pow((y-Y),2)) <= Math.Pow(Radius,2))
                return true;
            else
                return false;
        }
        public void Paint(Canvas canvas)
        {
            if (e == null)
            {
                e = new Ellipse();
                e.Width = Radius;
                e.Height = Radius;
                e.Fill = Brushes.White;
                Canvas.SetLeft(e, X - (Radius / 2));
                Canvas.SetTop(e, Y - (Radius / 2));
                //Canvas.SetZIndex(e,2)
                canvas.Children.Add(e);
            }
            if (Selected)
                e.Stroke = Brushes.Red;
            else
                e.Stroke = Brushes.Black;            
            canvas.InvalidateVisual();
        }

        public void Remove(Canvas canvas)
        {
            if (e != null)
            {
                canvas.Children.Remove(e);
            }
        }

        public void Select() => Selected = true;
        public void Unselect() => Selected = false;
    }
}
