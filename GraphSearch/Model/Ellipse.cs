using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphSearch
{
    class Ellipse:Shape
    {
        private static int Id = 0;
        override public string Name { get { Id++; return "Ellipse" + Id; } }
        public int XRadius { get; set; } = 30;
        public int YRadius { get; set; } = 20;

        public Ellipse(Canvas canvas, double x, double y) : base(canvas, x, y) { name = "Ellipse"; }
        public override bool HitTest(double x, double y)
        {
            if ((Math.Pow((x - X), 2)/ Math.Pow(XRadius, 2) + Math.Pow((y - Y), 2)/Math.Pow(YRadius,2)) <= 1)
                return true;
            else
                return false;
        }

        public override bool IsOutOfBounds(double dx, double dy)
        {
            if (((X+dx + XRadius) > (canvas.ActualWidth)) || ((Y +dy+ YRadius) > canvas.ActualHeight) || (X+dx < (0 + XRadius)) || (Y+dy < (0 + YRadius)))
                return true;
            return false;
        }
        public override void Move(double dx, double dy)
        {
            if (!IsOutOfBounds(dx,dy))
            {
                X = X + dx;
                Y = Y + dy;
            }
            Paint();
        }
        public override int GetSize() => YRadius;
        public override void SetSize(int size) { YRadius = size; XRadius = (int)(size * 1.5);Paint(); }
        public override void Paint()
        {
            if (drawing == null)
            {
                drawing = new System.Windows.Shapes.Ellipse();
                drawing.StrokeThickness = 2;
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
        public override void Remove()
        {
            if (drawing != null)
            {
                canvas.Children.Remove(drawing);
            }
        }
        public override void Save(StreamWriter sw)
        {
            sw.WriteLine("Ellipse");
            sw.WriteLine(X.ToString() + ' ' + Y.ToString());
            sw.WriteLine(XRadius.ToString()+' '+YRadius.ToString());
            sw.WriteLine(Color.ToString());
        }
        public override void Load(StreamReader sr,AbstractFactory factory)
        {
            var str = sr.ReadLine();
            X = double.Parse(str.Split(' ')[0]);
            Y = double.Parse(str.Split(' ')[1]);
            str = sr.ReadLine();
            XRadius = int.Parse(str.Split(' ')[0]);
            YRadius = int.Parse(str.Split(' ')[1]);
            Color = (Color)ColorConverter.ConvertFromString(sr.ReadLine());
            Paint();
        }
    }
}
