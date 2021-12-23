using GraphSearch.Model;
using System.IO;
using System.Windows.Media;
using UniStorage;

namespace GraphSearch
{
    class ShapeGroup : IShape
    {
        public UniversalStoradge<IShape> shapes { get; private set; }
        private bool selected;
        public bool Selected { get { return selected; } }

        public ShapeGroup()
        {
            shapes = new UniversalStoradge<IShape>();
            Select();
        }
        public bool HitTest(double x, double y)
        {
            foreach(var shape in shapes)
            {
                if (shape.HitTest(x, y) == true)
                    return true;
            }
            return false;
        }
        public bool IsOutOfBounds(double dx, double dy)
        {
            foreach (var shape in shapes)
            {
                if (shape.IsOutOfBounds(dx,dy) == true)
                    return true;
            }
            return false;
        }
        public void Move(double dx, double dy)
        {
            if(!IsOutOfBounds(dx, dy))
            foreach (var shape in shapes)
            {
                shape.Move(dx, dy);
            }
        }
        public void Paint()
        {
            foreach (var shape in shapes)
            {
                shape.Paint();
            }
        }
        public void Select()
        {
            selected = true; 
            foreach(var shape in shapes)
            {
                shape.Select();
            }
        }
        public void Unselect()
        {
            selected = false;
            foreach (var shape in shapes)
            {
                shape.Unselect();
            }
        }
        public void Add(IShape shape)
        {
            shapes.AddElement(shape);
        }
        public void Remove()
        {
            foreach(var shape in shapes)
            {
                shape.Remove();
            }
        }
        public void SetColor(Color color) {
            foreach (var s in shapes)
                s.SetColor(color);
        }

        public void SetSize(int size)
        {
            foreach (var shape in shapes)
                shape.SetSize(size);
        }

        public void Save(StreamWriter sw)
        {
            sw.WriteLine("Group");
            sw.WriteLine(shapes.GetCount());
            foreach(var s in shapes)
            {
                s.Save(sw);
            }
        }

        public void Load(StreamReader sr , AbstractFactory factory)
        {
            int count = int.Parse(sr.ReadLine());
            for (int i = 0; i < count; i++)
            {
                var code = sr.ReadLine();
                var shape = factory.CreateShape(code);
                if (shape != null)
                    shape.Load(sr,factory);
                    Add(shape);
            }
        }
    }
}
