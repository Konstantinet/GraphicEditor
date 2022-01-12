using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GraphSearch.Model
{
    public interface IShape
    {
        string Name { get; }
        bool Selected { get; }
        void Select();
        void Unselect();
        bool HitTest(double x, double y);
        bool IsOutOfBounds(double dx, double dy);
        void Move(double dx, double dy);
        void Paint();
        void Remove();
        void SetColor(Color color);
        void SetSize(int size);

        void Save(StreamWriter sw);
        void Load(StreamReader sr,AbstractFactory factory);
    }
}
