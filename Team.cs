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

        const double taane = 1; 

        foreach (var player in Players)
        {
            if (isHomeTeam)
            {

                player.SetPosition(
                    taane + rnd.NextDouble() * (width / 2 - 2 * taane),   
                    taane + rnd.NextDouble() * (height - 2 * taane)      
                );
            }
            else
            {
                player.SetPosition(
                    (width / 2) + taane + rnd.NextDouble() * (width / 2 - 2 * taane),  
                    taane + rnd.NextDouble() * (height - 2 * taane)                   
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

        Player closestPlayerHome = this.Game.HomeTeam.GetClosestPlayerToBall();
        Player closestPlayerAway = this.Game.AwayTeam.GetClosestPlayerToBall();


        closestPlayerHome.MoveTowardsBall();
        closestPlayerAway.MoveTowardsBall();


        foreach (var player in this.Game.HomeTeam.Players)
        {
            player.Move(closestPlayerHome);
        }

        foreach (var player in this.Game.AwayTeam.Players)
        {
            player.Move(closestPlayerAway);
        }
    }
}