using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inno01.Model
{
    public class Graph
    {
        public List<City> Nodes { get; set; }
        public List<Flight> Edges { get; set; }
    }
}
