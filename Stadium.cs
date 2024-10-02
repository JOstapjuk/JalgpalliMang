using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JalgpalliMang
{
    public class Stadium
    {


        public int Width { get; }
        public int Height { get; }

        public Stadium(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public bool IsIn(double x, double y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public static void DrawStadium(Game game)
        {
            Console.Clear();


            for (int y = 0; y <= game.Stadium.Height; y++)
            {
                for (int x = 0; x <= game.Stadium.Width; x++)
                {
                    char symb = ' ';
                    Console.ForegroundColor = ConsoleColor.White; 

                    if (x == 0 || x == game.Stadium.Width - 1 || y == 0 || y == game.Stadium.Height - 1)
                    {
                        symb = '#'; 
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }


                    if (x == 0 && y >= game.Stadium.Height / 2 - 2 && y <= game.Stadium.Height / 2 + 2)
                    {
                        symb = ' ';
                    }


                    if (x == game.Stadium.Width - 1 && y >= game.Stadium.Height / 2 - 2 && y <= game.Stadium.Height / 2 + 2)
                    {
                        symb = ' ';
                    }

                    Console.Write(symb);
                }
                Console.WriteLine();
            }

            Console.ResetColor();
        }

    }

}
