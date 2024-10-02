
namespace JalgpalliMang;

public class Game
{
    public int HomeTeamScore { get; private set; } = 0;
    public int AwayTeamScore { get; private set; } = 0;

    public Team HomeTeam { get; }
    public Team AwayTeam { get; }
    public Stadium Stadium { get; }
    public Ball Ball { get; private set; }

   

    public Game(Team homeTeam, Team awayTeam, Stadium stadium)
    {
        HomeTeam = homeTeam;
        homeTeam.Game = this;
        AwayTeam = awayTeam;
        awayTeam.Game = this;
        Stadium = stadium;

    }

    public void Start()
    {
        Ball = new Ball(Stadium.Width / 2, Stadium.Height / 2, this); 
        HomeTeam.StartGame(Stadium.Width, Stadium.Height, true);      
        AwayTeam.StartGame(Stadium.Width, Stadium.Height, false);     
    }
    public (double, double) GetPositionForTeam(Team team, double x, double y)
    {
        // Home team sees coordinates as-is, away team sees mirrored coordinates
        return team == HomeTeam ? (x, y) : GetPositionForAwayTeam(x, y);
    }

    private (double, double) GetPositionForAwayTeam(double x, double y)
    {
        // Reverse the x-coordinate for the away team, while y remains the same
        return (Stadium.Width - x, y);
    }

    public (double, double) GetBallPositionForTeam(Team team)
    {
        return GetPositionForTeam(team, Ball.X, Ball.Y);
    }

    public void SetBallSpeedForTeam(Team team, double vx, double vy)
    {
        if (team == HomeTeam)
        {
            Ball.SetSpeed(vx, vy);
        }
        else
        {
            Ball.SetSpeed(-vx, -vy);
        }
    }

    public void ScoreGoal(Team scoringTeam)
    {
        if (scoringTeam == HomeTeam)
        {
            HomeTeamScore++;
        }
        else
        {
            AwayTeamScore++;
        }

        // Update the score display without clearing the screen
        DisplayScore();
    }

    public void DisplayScore()
    {
        Console.SetCursorPosition(0, Stadium.Height + 1); // Position below the stadium
        Console.WriteLine($"Score - Home: {HomeTeamScore} | Away: {AwayTeamScore}");
    }

    public void Move()
    {
        HomeTeam.Move();
        AwayTeam.Move();
        Ball.Move();
    }

   

}
 

