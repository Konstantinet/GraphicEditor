using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphSearch
{
    public class Circle : Shape
    {
        public int Radius { get; set; } = 25;

        public Circle(double x, double y)
        {
            X = x;
            Y = y;
            Selected = true;
        }
        public override bool HitTest(double x, double y)
        {
            if ((Math.Pow((x - X), 2) + Math.Pow((y - Y), 2)) <= Math.Pow(Radius, 2))
                return true;
            else
                return false;
        }

        public override bool IsOutOfBounds(Canvas canvas)
        {
            if (((X + Radius) > (canvas.ActualWidth)) || ((Y + Radius) > canvas.ActualHeight) || (X < (0 + Radius)) || (Y < (0 + Radius)))
                return true;
            return false;
        }

        public override int GetSize() => Radius;
        public override void SetSize(int size) => Radius = size;
        public override void Resize(int d)
        {
            Radius += d;
        }

        public override void Paint(Canvas canvas)
        {
            if (drawing == null)
            {
                drawing = new System.Windows.Shapes.Ellipse();
                canvas.Children.Add(drawing);
            }
            drawing.Fill = new SolidColorBrush(Color);
            drawing.Width = Radius * 2;
            drawing.Height = Radius * 2;
            Canvas.SetLeft(drawing, X - Radius);
            Canvas.SetTop(drawing, Y - Radius);
            if (Selected)
                drawing.Stroke = Brushes.Red;
            else
                drawing.Stroke = Brushes.Black;
        }

        public override void Remove(Canvas canvas)
        {
            if (drawing != null)
            {
                canvas.Children.Remove(drawing);
            }
        }


    }
}

