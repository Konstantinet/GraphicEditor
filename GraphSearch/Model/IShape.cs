using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSearch.Model
{
    public interface IShape
    {
        bool Selected { get; }
        void Select();
        void Unselect();
        bool HitTest(double x, double y);
        bool IsOutOfBounds(double dx, double dy);
        void Move(double dx, double dy);
        void Paint();
        void Remove();
    }
}
