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

        if (newY <= 0 || newY >= _game.Stadium.Height - 1)
        {
            _vy = -_vy; 
            newY = Y + _vy; 
        }


        if (newX <= 0 || newX >= _game.Stadium.Width - 1)
        {

            if (newX <= 0 && newY >= _game.Stadium.Height / 2 - 2 && newY <= _game.Stadium.Height / 2 + 2)
            {
                _game.ScoreGoal(_game.AwayTeam); 
                return; 
            }

            if (newX >= _game.Stadium.Width - 1 && newY >= _game.Stadium.Height / 2 - 2 && newY <= _game.Stadium.Height / 2 + 2)
            {
                _game.ScoreGoal(_game.HomeTeam); 
                return; 
            }

            _vx = -_vx; 
            newX = X + _vx; 
        }


        X = newX;
        Y = newY;
    }

}