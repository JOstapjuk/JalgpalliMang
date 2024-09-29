using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JalgpalliMang
{
    public class Team
    {
        // objects 
        public List<Player> Players { get; } = new List<Player>();
        public string Name { get; private set; }
        public Game Game { get; set; }

        // construct
        public Team(string name)
        {
            Name = name;
        }

        // mängijate loomine ja nende väljakule seadmine (juhuslik)
        public void StartGame(int width, int height, bool isHomeTeam)
        {
            Random rnd = new Random();

            // Define a buffer from the edges
            double buffer = 1; // Adjust this value if needed

            foreach (var player in Players)
            {
                double xPos;
                double yPos = buffer + rnd.NextDouble() * (height - 2 * buffer);  // Players can be placed anywhere on the Y axis with a buffer

                if (isHomeTeam)
                {
                    // Home team players on the left half of the field, avoiding the border
                    xPos = buffer + rnd.NextDouble() * ((width / 2) - buffer);  // X position from buffer to (width / 2 - buffer)
                }
                else
                {
                    // Away team players on the right half of the field, avoiding the border
                    xPos = (width / 2) + buffer + rnd.NextDouble() * ((width / 2) - buffer);  // X position from (width / 2 + buffer) to (width - buffer)
                }

                // Set the player's position
                player.SetPosition(xPos, yPos);
            }
        }

        // kui mängijad juba mingis meeskonnas, kui mitte siis mängija lisamine nimekirja
        public void AddPlayer(Player player)
        {
            if (player.Team != null) return;
            Players.Add(player);
            player.Team = this;
        }

        // palli asendi saamine sõltuvalt meeskonnast
        public (double, double) GetBallPosition()
        {
            return Game.GetBallPositionForTeam(this);
        }

        // palli kiiruse seadmine sõltuvalt meeskonnast
        public void SetBallSpeed(double vx, double vy)
        {
            Game.SetBallSpeedForTeam(this, vx, vy);
        }

        public Player GetClosestPlayerToBall()
        {
            Player closestPlayer = Players[0];
            double bestDistance = Double.MaxValue;
            foreach (var player in Players)
            {
                var distance = player.GetDistanceToBall();
                if (distance < bestDistance)
                {
                    closestPlayer = player;
                    bestDistance = distance;
                }
            }

            return closestPlayer;
        }


        // väljakul mängijate liigutamine
        public void Move()
        {
            GetClosestPlayerToBall().MoveTowardsBall();
            Players.ForEach(player => player.Move());
        }


    }
}