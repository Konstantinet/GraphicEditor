using System.IO;
using UniStorage;

namespace GraphSearch.Model
{
    public class SerializableStorage<T>:UniversalStoradge<IShape>
    {
        public void LoadComponents(object loader, StreamReader sr, ShapeFactory factory)
        {

            int count = int.Parse(sr.ReadLine());
            for (int i = 0; i < count; i++)
            {
                var code = sr.ReadLine();
                var shape = factory.CreateShape(code);
                if (shape is ShapeGroup)
                    LoadComponents(shape, sr, factory);
                else if (shape != null)
                    shape.Load(sr);
                if (loader is ShapeGroup)
                    (loader as ShapeGroup).Add(shape);
                else
                    AddElement(shape);
            }

        }
    }
}
