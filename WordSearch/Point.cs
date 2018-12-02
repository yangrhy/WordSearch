using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch
{
    class Point
    {
        public int row { get; set; }
        public int col { get; set; }

        public Point()
        {
            this.row = 0;
            this.col = 0;
        }

        public Point(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        
    }
}
