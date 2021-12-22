using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphSearch
{
    public abstract class Shape
    {
        protected double X, Y;

        protected Canvas canvas;

        public Shape(Canvas canvas, double x,double y)
        {
            X = x;
            Y = y;
            Selected = true;
            this.canvas = canvas;
            Paint();
        }

        protected Color Color;
        public bool Selected { get; protected set; }

        protected System.Windows.Shapes.Shape drawing;

        public abstract bool HitTest(double x, double y);

        public abstract bool IsOutOfBounds();

        public abstract int GetSize();

        public abstract void SetSize(int size);

        public void Move(double dx, double dy)
        {
            X = X + dx;
            Y = Y + dy;
            if (IsOutOfBounds())
            {
                X = X - dx;
                Y = Y - dy;
            }
            Paint();
        }
        public abstract void Paint();

        public abstract void Remove();

        public Color GetColor() => Color;
        
        public void SetColor(Color color) { Color = color;Paint(); }
        public void Select() { Selected = true;Paint(); }
        public void Unselect() { Selected = false; Paint(); }
    }
}
