using GraphSearch.Model;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphSearch
{
    public abstract class Shape : IShape
    {
        protected double X, Y;
        protected Canvas canvas;
        protected Color Color;
        bool selected;
        public string name;
        public virtual string Name { get { return name; } }
        public bool Selected { get { return selected; } }
        protected System.Windows.Shapes.Shape drawing;

        public Shape(Canvas canvas, double x,double y)
        {
            X = x;
            Y = y;
            selected = true; ;
            this.canvas = canvas;
            Paint();
        }

        public abstract bool HitTest(double x, double y);
        public abstract bool IsOutOfBounds(double dx, double dy);
        public abstract int GetSize();
        public abstract void SetSize(int size);
        public abstract void Move(double dx, double dy);
        public abstract void Paint();
        public abstract void Remove();
        public abstract void Save(StreamWriter sw);
        public abstract void Load(StreamReader sr,AbstractFactory factory);

        public Color GetColor() => Color;
        public void SetColor(Color color) { Color = color;Paint(); }
        public void Select() { selected = true;Paint(); }
        public void Unselect() { selected = false; Paint(); }

       
    }
}
