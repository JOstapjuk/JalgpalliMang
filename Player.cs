
using System;
using System.Diagnostics;

namespace JalgpalliMang;

public class Player
{
    public List<Player> Players { get; set; }

    public string Name { get; }
    public double X { get; private set; }
    public double Y { get; private set; }
    private double _vx, _vy;
    public Team? Team { get; set; } = null;


    private const double MaxSpeed = 5;
    private const double MaxKickSpeed = 25;
    private const double BallKickDistance = 10;

    private Random _random = new Random();


    public Player(string name)
    {
        Name = name;
    }

    public Player(string name, double x, double y, Team team)
    {
        Name = name;
        X = x;
        Y = y;
        Team = team;
    }

    public void SetPosition(double x, double y)
    {
        X = x;
        Y = y;
    }

    public (double, double) GetAbsolutePosition()
    {
        return Team!.Game.GetPositionForTeam(Team, X, Y);
    }

    public double GetDistanceToBall()
    {
        var ballPosition = Team!.GetBallPosition();
        var dx = ballPosition.Item1 - X;
        var dy = ballPosition.Item2 - Y;
        var distance = Math.Sqrt(dx * dx + dy * dy);

        Debug.WriteLine($"{Name} ({Team.Name}) ({X:F2}, {Y:F2}) sees ball at ({ballPosition.Item1:F2}, {ballPosition.Item2:F2})");

        return distance;
    }

    public void MoveTowardsBall()
    {
        var ballPosition = Team!.GetBallPosition();
        var dx = ballPosition.Item1 - X;
        var dy = ballPosition.Item2 - Y;
        var distance = Math.Sqrt(dx * dx + dy * dy);

        if (distance > 0)
        {
            var ratio = MaxSpeed / distance;
            _vx = dx * ratio;
            _vy = dy * ratio;

            Debug.WriteLine($"{Name} ({Team.Name}) is moving towards ball from ({X:F2}, {Y:F2}) to ({ballPosition.Item1:F2}, {ballPosition.Item2:F2}))");


        }
    }

    public void Move(Player closestPlayer)
    {

        if (closestPlayer != this)
        {

            _vx = 0;
            _vy = 0;
            Debug.WriteLine($"{Name} ({Team.Name}) is not closest to the ball");
            return; 
        }


        if (GetDistanceToBall() < BallKickDistance)
        {

            double kickSpeedX = MaxKickSpeed * _random.NextDouble();
            double kickSpeedY = MaxKickSpeed * (_random.NextDouble() - 0.5);
            Team.SetBallSpeed(kickSpeedX, kickSpeedY);
            Debug.WriteLine($"{Name} ({Team.Name}) kicks the ball");
        }


        double newX = X + _vx;
        double newY = Y + _vy;
        var newAbsolutePosition = Team.Game.GetPositionForTeam(Team, newX, newY);


        if (Team.Game.Stadium.IsIn(newAbsolutePosition.Item1, newAbsolutePosition.Item2))
        {
            X = newX; 
            Y = newY;
            Debug.WriteLine($"{Name} ({Team.Name}) moved to ({newX:F2}, {newY:F2})");
        }
        else
        {

            _vx = 0;
            _vy = 0;
            Debug.WriteLine($"{Name} ({Team.Name}) hit the boundary");
        }
    }



    public static void DrawPlayers(Game game)
    {
        for (int y = 1; y < game.Stadium.Height; y++) 
        {
            for (int x = 1; x < game.Stadium.Width - 1; x++) 
            {
                char symb = ' ';


                if ((int)game.Ball.X == x && (int)game.Ball.Y == y)
                {
                    symb = 'O'; 
                    Console.ForegroundColor = ConsoleColor.Red; 
                }


                foreach (var player in game.HomeTeam.Players)
                {
                    if ((int)player.X == x && (int)player.Y == y)
                    {
                        symb = 'A';
                        Console.ForegroundColor = ConsoleColor.Green; 
                    }
                }

                foreach (var player in game.AwayTeam.Players)
                {
                    if ((int)player.X == x && (int)player.Y == y)
                    {
                        symb = 'B';
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                }

                if (symb != ' ')
                {
                    Console.SetCursorPosition(x, y); 
                    Console.Write(symb);
                }
            }
        }

        Console.ResetColor();
    }
}