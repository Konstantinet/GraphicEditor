using System.IO;
using UniStorage;

namespace GraphSearch.Model
{
    public class SerializableStorage<T>:UniversalStoradge<IShape>
    {
        public void LoadComponents(StreamReader sr, AbstractFactory factory)
        {

            int count = int.Parse(sr.ReadLine());
            for (int i = 0; i < count; i++)
            {
                var code = sr.ReadLine();
                var shape = factory.CreateShape(code);
                if (shape != null)
                shape.Load(sr,factory);
                AddElement(shape);
            }

        }
    }
}
