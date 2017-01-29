using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inno01.Model
{
    public class Edge
    {
        public Flight Flight { get; set; }
        public Node Source { get; set; }
        public Node Destination { get; set; }
        public int Weight { get; set; }
    }
}
