using System.IO;
using System.Windows.Controls;

namespace GraphSearch.Model
{
    public class ShapeFactory
    {
        Canvas canvas;
        public ShapeFactory(Canvas canvas)
        {
            this.canvas = canvas; 
        }
        public IShape CreateShape(string shape)
        {
            if (shape == "Circle")
                return new Circle(canvas,0,0);
            if (shape == "Square")
                return new Sqare(canvas, 0, 0);
            if (shape == "Ellipse")
                return new Ellipse(canvas, 0, 0);
            if (shape == "Group")
                return new ShapeGroup();
            else
                return null;

        }
    }
}
