using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphSearch
{
    public class Circle : Shape
    {
        public Circle(Canvas canvas,double x,double y):base(canvas,x,y)
        {
        }

        public int Radius { get; set; } = 25;

        public override bool HitTest(double x, double y)
        {
            if ((Math.Pow((x - X), 2) + Math.Pow((y - Y), 2)) <= Math.Pow(Radius, 2))
                return true;
            else
                return false;
        }

        public override bool IsOutOfBounds()
        {
            if (((X + Radius) > (canvas.ActualWidth)) || ((Y + Radius) > canvas.ActualHeight) || (X < (0 + Radius)) || (Y < (0 + Radius)))
                return true;
            return false;
        }

        public override int GetSize() => Radius;
        public override void SetSize(int size) { Radius = size; Paint(); }

        public override void Paint()
        {
            if (drawing == null)
            {
                drawing = new System.Windows.Shapes.Ellipse();
                drawing.StrokeThickness = 2;
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

        public override void Remove()
        {
            if (drawing != null)
            {
                canvas.Children.Remove(drawing);
            }
        }


    }
}

