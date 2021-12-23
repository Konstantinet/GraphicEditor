using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphSearch
{
    public class Circle : Shape
    {
        public Circle(Canvas canvas, double x, double y) : base(canvas, x, y) { }
        public int Radius { get; set; } = 25;

        public override bool HitTest(double x, double y)
        {
            if ((Math.Pow((x - X), 2) + Math.Pow((y - Y), 2)) <= Math.Pow(Radius, 2))
                return true;
            else
                return false;
        }
        public override bool IsOutOfBounds(double dx, double dy)
        {
            if (((X+dx + Radius) > (canvas.ActualWidth)) || ((Y+dy + Radius) > canvas.ActualHeight) || (X+dx < (0 + Radius)) || (Y+dy < (0 + Radius)))
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
        public override int GetSize() => Radius;
        public override void SetSize(int size) 
        {
            var temp = Radius;
            Radius = size;
            if (IsOutOfBounds(0, 0))
                Radius = temp;
            Paint(); 
        }
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

        public override void Save(StreamWriter sw)
        {
            sw.WriteLine("Circle");
            sw.WriteLine(X.ToString() + ' ' + Y.ToString());
            sw.WriteLine(Radius);
            sw.WriteLine(Color.ToString());
        }

        public override void Load(StreamReader sr,AbstractFactory factory)
        {
            var str = sr.ReadLine();
            X = double.Parse(str.Split(' ')[0]);
            Y = double.Parse(str.Split(' ')[1]);
            Radius = int.Parse(sr.ReadLine());
            Color = (Color)ColorConverter.ConvertFromString(sr.ReadLine());
            Paint();
        }

        


    }
}

