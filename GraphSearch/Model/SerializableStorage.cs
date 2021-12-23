using System.IO;
using UniStorage;

namespace GraphSearch.Model
{
    public class SerializableStorage<T>:UniversalStoradge<T>
    {
        public void LoadComponents(string path,ShapeFactory factory)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                int count = int.Parse(sr.ReadLine());
                for(int i = 0; i < count; i++)
                {
                    var code = sr.ReadLine();
                    var shape = factory.CreateShape(code);
                    if(shape != null)
                        shape.Load(sr);
                }
            }
        }
    }
}
