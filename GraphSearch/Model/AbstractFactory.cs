using GraphSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSearch
{
    public abstract class AbstractFactory
    {
        public abstract IShape CreateShape(string shape);
    }
}
