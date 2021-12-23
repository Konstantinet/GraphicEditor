using System;
using System.Collections.Generic;
using System.IO;
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
        public override bool IsOutOfBounds(double dx, double dy)
        {
            if ((((X +dx)+ Height/2) > canvas.ActualWidth) || (((Y+dy) + Height/2) > canvas.ActualHeight) || 
                ((X + dx) < (0 + Height/2)) || ((Y+dy) < (0 + Height/2)))
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
        public override void Save(StreamWriter sw)
        {
            sw.WriteLine("Square");
            sw.WriteLine(X.ToString() + ' ' + Y.ToString());
            sw.WriteLine(Height);
            sw.WriteLine(Color.ToString());
        }
        public override void Load(StreamReader sr,AbstractFactory factory)
        {
            var str = sr.ReadLine();
            X = double.Parse(str.Split(' ')[0]);
            Y = double.Parse(str.Split(' ')[1]);
            Height = int.Parse(sr.ReadLine());
            Color = (Color)ColorConverter.ConvertFromString(sr.ReadLine());
            Paint();
        }
    }
}
