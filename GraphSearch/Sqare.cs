using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphSearch
{
    class Sqare : Shape
    {

        public int Height { get; set; }  = 50;

        public Sqare(Canvas canvas, double x, double y) : base(canvas, x, y)
        {
        }
        public override bool HitTest(double x, double y)
        {
            if ((x < (X + (Height / 2))) && (x > (X - (Height / 2))))
                if ((y < (Y + (Height / 2))) && (y > (Y - (Height / 2))))
                    return true;
            return false;
        }

        public override bool IsOutOfBounds()
        {
            if (((X + Height/2) > canvas.ActualWidth) || ((Y + Height/2) > canvas.ActualHeight) || (X < (0 + Height/2)) || (Y < (0 + Height/2)))
                return true;
            return false;
        }

        public override int GetSize() => Height;
        public override void SetSize(int size) { Height = size; Paint(); }

        public override void Paint()
        {
            if (drawing == null)
            {
                drawing = new Rectangle();
                drawing.StrokeThickness = 2;
                canvas.Children.Add(drawing);
            }
            drawing.Fill = new SolidColorBrush(Color);
            drawing.Width = Height;
            drawing.Height = Height;
            Canvas.SetLeft(drawing, X - (Height / 2));
            Canvas.SetTop(drawing, Y - (Height / 2));
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
