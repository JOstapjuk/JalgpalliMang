using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JalgpalliMang
{
    internal class VerticalLine : Figure
    {
        public VerticalLine(int yUp, int yDown, int x, char sym)
        {
            pList = new List<Point>();
            // Korda läbi y-koordinaatide vahemiku
            for (int y = yUp; y <= yDown; y++)
            {
                Point p = new Point(x, y, sym);
                pList.Add(p);
            }
        }
    }
}
