using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHTWords
{
    public class GridCell
    {
        public string value = null;
        public bool visited = false;

        public GridCell(string value, bool visited)
        {
            this.value = value;
            this.visited = visited;
        }
    }
}
