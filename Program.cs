using System;
using System.Collections.Generic;

namespace JalgpalliMang
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            Stadium stadium = new Stadium(100, 25);

            Team homeTeam = new Team("Home Team");
            Team awayTeam = new Team("Away Team");

            for (int i = 1; i <= 11; i++)
            {
                homeTeam.AddPlayer(new Player($"A{i}"));
            }

            for (int i = 1; i <= 11; i++)
            {
                awayTeam.AddPlayer(new Player($"B{i}"));
            }

            Game game = new Game(homeTeam, awayTeam, stadium);

            game.Start();

            for (int i = 0; i < 100; i++)
            {
                Console.Clear();

                homeTeam.Move();  // Only the closest player to the ball moves
                awayTeam.Move();  // Only the closest player to the ball moves

                // Joonista staadion ja mängijate positsioonid
                DrawStadium(game);

                System.Threading.Thread.Sleep(3000);
            }

            Console.WriteLine("Mäng läbi!");


        static void DrawStadium(Game game)
            {
                Console.Clear();

                // Joonista väljade piirid
                for (int y = 0; y <= game.Stadium.Height; y++)
                {
                    for (int x = 0; x <= game.Stadium.Width; x++)
                    {
                        char symb = ' ';

                        // Määrake kõigi tähemärkide vaikevärv
                        Console.ForegroundColor = ConsoleColor.White;

                        // Kontrolli esmalt väljakut
                        if (x == 0 || x == game.Stadium.Width - 1 || y == 0 || y == game.Stadium.Height - 1)
                        {
                            symb = '#'; // Välipiir
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }

                        // Kontrolli palli asukohta
                        if ((int)game.Ball.X == x && (int)game.Ball.Y == y)
                        {
                            symb = '\u09E6'; // Ball symbol
                            Console.ForegroundColor = ConsoleColor.Red;
                        }

                        // Kontrolli mängijaid (home team)
                        foreach (var player in game.HomeTeam.Players)
                        {
                            if ((int)player.X == x && (int)player.Y == y)
                            {
                                symb = '\u26F9'; // Player symbol (home team)
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                        }

                        // Kontrolli mängijaid (away team)
                        foreach (var player in game.AwayTeam.Players)
                        {
                            if ((int)player.X == x && (int)player.Y == y)
                            {
                                symb = '\u07C2'; // Player symbol (away team)
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            }
                        }

                        // Kirjuta märk valitud värviga
                        Console.Write(symb);
                    }
                    
                    Console.WriteLine();
                }

                Console.ResetColor();

            }
        }
    }
}
