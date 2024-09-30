using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace JalgpalliMang
{
    class Program
    {
        static void Main(string[] args)
        {

            Stadium stadium = new Stadium(80, 25);

            Team homeTeam = new Team("Home Team");
            Team awayTeam = new Team("Away Team");

            for (int i = 1; i <= 11; i++)
            {
                awayTeam.AddPlayer(new Player($"B{i}"));
            }

            for (int i = 1; i <= 11; i++)
            {
                homeTeam.AddPlayer(new Player($"A{i}"));
            }

           

            Game game = new Game(homeTeam, awayTeam, stadium);

            game.Start();


            while (true)
            {
                game.Move();
                Stadium.DrawStadium(game);
                Player.DrawPlayers(game);

                Thread.Sleep(400);
            }

          
        }
    }
}
