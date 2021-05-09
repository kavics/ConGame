using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConGame
{
    public class Shape
    {
        public static readonly int Size = 5;

        public int OffsetLeft { get; set; }
        public int OffsetRight { get; set; }
        public bool[][] Cells { get; set; }
    }
}
