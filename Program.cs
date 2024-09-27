using System;
using System.Collections.Generic;

namespace JalgpalliMang
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create stadium
            Stadium stadium = new Stadium(35, 15);

            // Create teams
            Team homeTeam = new Team("Home Team");
            Team awayTeam = new Team("Away Team");

            // Add players to home team
            homeTeam.AddPlayer(new Player("A")); // Shortened names for display
            homeTeam.AddPlayer(new Player("A"));
            homeTeam.AddPlayer(new Player("A"));

            // Add players to away team
            awayTeam.AddPlayer(new Player("B"));
            awayTeam.AddPlayer(new Player("B"));
            awayTeam.AddPlayer(new Player("B"));

            // Create game
            Game game = new Game(homeTeam, awayTeam, stadium);

            // Start the game
            game.Start();

            // Simulate some game moves
            for (int i = 0; i < 3000; i++) // Simulate 10 turns
            {
                game.Move();
                DisplayGameState(game);
                System.Threading.Thread.Sleep(700); // Pause for a second between moves
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
                        char displayChar = ' '; // Default to empty space

                        // Check if the ball is at the current position
                        if ((int)game.Ball.X == x && (int)game.Ball.Y == y)
                            displayChar = 'O'; // Ball

                        // Check for home players
                        foreach (var player in game.HomeTeam.Players)
                        {
                            if ((int)player.X == x && (int)player.Y == y)
                                displayChar = 'H'; // Home player
                        }

                        // Check for away players
                        foreach (var player in game.AwayTeam.Players)
                        {
                            if ((int)player.X == x && (int)player.Y == y)
                                displayChar = 'A'; // Away player
                        }

                        Console.Write(displayChar);
                    }
                    Console.WriteLine(); // New line for each row
                }
            }
        }
    }
}
