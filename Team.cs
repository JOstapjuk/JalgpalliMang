using System;
using System.Collections.Generic;

namespace JalgpalliMang;

public class Team
{
    public List<Player> Players { get; } = new List<Player>();
    public string Name { get; private set; }
    public Game Game { get; set; }

    public Team(string name)
    {
        Name = name;
    }

    public void StartGame(int width, int height, bool isHomeTeam)
    {
        Random rnd = new Random();

        const double taane = 1; // Margin from the field border

        foreach (var player in Players)
        {
            if (isHomeTeam)
            {
                // Home team spawns on the left half of the field
                player.SetPosition(
                    taane + rnd.NextDouble() * (width / 2 - 2 * taane),   // Left half for home team
                    taane + rnd.NextDouble() * (height - 2 * taane)      // Random Y position within bounds
                );
            }
            else
            {
                // Away team spawns on the right half of the field
                player.SetPosition(
                    (width / 2) + taane + rnd.NextDouble() * (width / 2 - 2 * taane),  // Right half for away team
                    taane + rnd.NextDouble() * (height - 2 * taane)                   // Random Y position within bounds
                );
            }
        }
    }

    public void AddPlayer(Player player)
    {
        if (player.Team != null) return;
        Players.Add(player);
        player.Team = this;
    }

    public (double, double) GetBallPosition()
    {
        return Game.GetBallPositionForTeam(this);
    }

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

    public void Move()
    {
        // Find the closest player for both teams
        Player closestPlayerHome = this.Game.HomeTeam.GetClosestPlayerToBall();
        Player closestPlayerAway = this.Game.AwayTeam.GetClosestPlayerToBall();

        // Move the closest player of each team towards the ball
        closestPlayerHome.MoveTowardsBall();
        closestPlayerAway.MoveTowardsBall();

        // Loop through all players in the home team
        foreach (var player in Players)
        {
            // Pass the closest player for each team to the player's Move method
            player.Move(closestPlayerHome == player ? closestPlayerHome : closestPlayerAway);
        }

        // Loop through all players in the away team
        foreach (var player in this.Game.AwayTeam.Players)
        {
            player.Move(closestPlayerHome == player ? closestPlayerHome : closestPlayerAway);
        }
    }
}