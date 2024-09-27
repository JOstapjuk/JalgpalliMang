using System;
using System.Collections.Generic;

namespace JalgpalliMang
{
    class Program
    {
        static void Main(string[] args)
        {
            Stadium stadium = new Stadium(35, 15);

            Team homeTeam = new Team("Home Team");
            Team awayTeam = new Team("Away Team");

            homeTeam.AddPlayer(new Player("A")); 
            homeTeam.AddPlayer(new Player("A"));
            homeTeam.AddPlayer(new Player("A"));

            awayTeam.AddPlayer(new Player("B"));
            awayTeam.AddPlayer(new Player("B"));
            awayTeam.AddPlayer(new Player("B"));

            Game game = new Game(homeTeam, awayTeam, stadium);

            game.Start();

            for (int i = 0; i < 3000; i++) 
            {
                game.Move();
                DisplayGameState(game);
                System.Threading.Thread.Sleep(700); 
            }

            static void DisplayGameState(Game game)
            {
                Console.Clear();
                Console.WriteLine("Current Game State:");
                DrawStadium(game);
            }

            static void DrawStadium(Game game)
            {
                for (int y = 0; y < game.Stadium.Height; y++)
                {
                    for (int x = 0; x < game.Stadium.Width; x++)
                    {
                        char displayChar = ' '; 

                        if ((int)game.Ball.X == x && (int)game.Ball.Y == y)
                            displayChar = 'O'; 

                        foreach (var player in game.HomeTeam.Players)
                        {
                            if ((int)player.X == x && (int)player.Y == y)
                                displayChar = 'H'; 
                        }

                        foreach (var player in game.AwayTeam.Players)
                        {
                            if ((int)player.X == x && (int)player.Y == y)
                                displayChar = 'A'; 
                        }

                        Console.Write(displayChar);
                    }
                    Console.WriteLine(); 
                }
            }
        }
    }
}
