using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHTWords
{
    public class GridPosition
    {
        public int row = 0;
        public int col = 0;

        public GridPosition(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public override bool Equals(object obj)
        {
            GridPosition gp = obj as GridPosition;

            if (gp != null)
            {
                return this == gp;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + this.row.GetHashCode();
                hash = hash * 23 + this.col.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(GridPosition a, GridPosition b)
        {
            return ((a.row == b.row) && (a.col == b.col));
        }

        public static bool operator !=(GridPosition a, GridPosition b)
        {
            return ((a.row != b.row) || (a.col != b.col));
        }

        public static bool operator >(GridPosition a, GridPosition b)
        {
            return (a.row == b.row) ? a.col > b.col : a.row > b.row;
        }

        public static bool operator <(GridPosition a, GridPosition b)
        {
            return (a.row == b.row) ? a.col < b.col : a.row < b.row;
        }

        public static bool operator >=(GridPosition a, GridPosition b)
        {
            return (a.row == b.row) ? a.col >= b.col : a.row >= b.row;
        }

        public static bool operator <=(GridPosition a, GridPosition b)
        {
            return (a.row == b.row) ? a.col <= b.col : a.row <= b.row;
        }
    }
}
