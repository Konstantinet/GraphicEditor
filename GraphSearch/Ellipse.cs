using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphSearch
{
    class Ellipse:Shape
    {
        public int XRadius { get; set; } = 30;
        public int YRadius { get; set; } = 20;

        public Ellipse(double x, double y)
        {
            X = x;
            Y = y;
            Selected = true;
        }
        public override bool HitTest(double x, double y)
        {
            if ((Math.Pow((x - X), 2)/ Math.Pow(XRadius, 2) + Math.Pow((y - Y), 2)/Math.Pow(YRadius,2)) <= 1)
                return true;
            else
                return false;
        }

        public override bool IsOutOfBounds(Canvas canvas)
        {
            if (((X + XRadius) > (canvas.Width)) || ((Y + YRadius) > canvas.Height))
                return true;
            return false;
        }

        public override int GetSize() => YRadius;

        public override void SetSize(int size) { YRadius = size; XRadius = (int)(size * 1.5); }
        public override void Resize(int d)
        {
            XRadius += d;
            YRadius += d;
        }

        public override void Paint(Canvas canvas)
        {
            if (drawing == null)
            {
                drawing = new System.Windows.Shapes.Ellipse();
                canvas.Children.Add(drawing);
            }
            drawing.Fill = new SolidColorBrush(Color);
            drawing.Width = XRadius * 2;
            drawing.Height = YRadius * 2;
            Canvas.SetLeft(drawing, X - XRadius);
            Canvas.SetTop(drawing, Y - YRadius);
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
