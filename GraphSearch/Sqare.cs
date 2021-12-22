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

        public Sqare(double x,double y)
        {
            X = x;
            Y = y;
            Selected = true;
        }
        public override bool HitTest(double x, double y)
        {
            if ((x < (X + (Height / 2))) && (x > (X - (Height / 2))))
                if ((y < (Y + (Height / 2))) && (y > (Y - (Height / 2))))
                    return true;
            return false;
        }

        public override bool IsOutOfBounds(Canvas canvas)
        {
            if (((X + Height) > canvas.Width) || ((Y + Height) > canvas.Height))
                return true;
            return false;
        }

        public override int GetSize() => Height;
        public override void SetSize(int size) => Height = size;
        public override void Resize(int d)
        {
            Height += d;
        }

        public override void Paint(Canvas canvas)
        {
            if (drawing == null)
            {
                drawing = new Rectangle();
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

        public override void Remove(Canvas canvas)
        {
            if (drawing != null)
            {
                canvas.Children.Remove(drawing);
            }
        }
    }
}
