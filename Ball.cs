namespace JalgpalliMang;

public class Ball
{
    public double X { get; private set; }
    public double Y { get; private set; }

    private double _vx, _vy;

    private Game _game;

    public Ball(double x, double y, Game game)
    {
        _game = game;
        X = x;
        Y = y;
        _vx = 0;
        _vy = 0;
    }

    public void SetSpeed(double vx, double vy)
    {
        _vx = vx;
        _vy = vy;
    }

    public void SetPosition(double x, double y)
    {
        X = x;
        Y = y;
    }

    public void Move()
    {
        double newX = X + _vx;
        double newY = Y + _vy;

        // Check if the ball hits the horizontal borders (top and bottom)
        if (newY <= 0 || newY >= _game.Stadium.Height - 1)
        {
            _vy = -_vy; // Reverse vertical velocity
            newY = Y + _vy; // Update the new position after bouncing
        }

        // Check if the ball hits the vertical borders (left and right)
        if (newX <= 0 || newX >= _game.Stadium.Width - 1)
        {
            // Check for a goal in the home team's goal (left side)
            if (newX <= 0 && newY >= _game.Stadium.Height / 2 - 2 && newY <= _game.Stadium.Height / 2 + 2)
            {
                _game.ScoreGoal(_game.AwayTeam); // Away team scores
                return; // Exit since we are resetting the ball
            }
            // Check for a goal in the away team's goal (right side)
            if (newX >= _game.Stadium.Width - 1 && newY >= _game.Stadium.Height / 2 - 2 && newY <= _game.Stadium.Height / 2 + 2)
            {
                _game.ScoreGoal(_game.HomeTeam); // Home team scores
                return; // Exit since we are resetting the ball
            }

            _vx = -_vx; // Reverse horizontal velocity for bouncing
            newX = X + _vx; // Update the new position after bouncing
        }

        // Update the ball's position
        X = newX;
        Y = newY;
    }

}