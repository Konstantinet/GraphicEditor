using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphSearch
{
    public abstract class Shape
    {
        protected double X, Y;

        public Color Color { get; set; }
        public bool Selected { get; protected set; }

        protected System.Windows.Shapes.Shape drawing;

        public abstract bool HitTest(double x, double y);

        public abstract bool IsOutOfBounds(Canvas canvas);

        public abstract int GetSize();

        public abstract void SetSize(int size);
        public abstract void Resize(int d);

        public void Move(Canvas canvas,double dx, double dy)
        {
            X = X + dx;
            Y = Y + dy;
            if (IsOutOfBounds(canvas))
            {
                X = X - dx;
                Y = Y - dy;
            }
            Paint(canvas);
        }
        public abstract void Paint(Canvas canvas);

        public abstract void Remove(Canvas canvas);
        public void Select() => Selected = true;
        public void Unselect() => Selected = false;
    }
}
