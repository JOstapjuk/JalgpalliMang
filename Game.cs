using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JalgpalliMang
{
    public class Game
    {
        // objektid Game class
        public Team HomeTeam { get; }
        public Team AwayTeam { get; }
        public Stadium Stadium { get; }
        public Ball Ball { get; private set; }

        // constuct
        public Game(Team homeTeam, Team awayTeam, Stadium stadium)
        {
            HomeTeam = homeTeam;
            homeTeam.Game = this;
            AwayTeam = awayTeam;
            awayTeam.Game = this;
            Stadium = stadium;
        }

        // positsioneerimis pall ja meeskonnad väljakul
        public void Start()
        {
            // Place the ball in the middle of the field
            Ball = new Ball(Stadium.Width / 2, Stadium.Height / 2, this);

            HomeTeam.StartGame(Stadium.Width, Stadium.Height, true);
            AwayTeam.StartGame(Stadium.Width, Stadium.Height, false);
        }
        // vastasmeeskonna positsiooni saamine
        private (double, double) GetPositionForAwayTeam(double x, double y)
        {
            return (Stadium.Width - x, Stadium.Height - y);
        }

        // kodumeeskonna positsiooni saamine
        public (double, double) GetPositionForTeam(Team team, double x, double y)
        {
            return team == HomeTeam ? (x, y) : GetPositionForAwayTeam(x, y);
        }

        // palli asendi saamine sõltuvalt meeskonnast
        public (double, double) GetBallPositionForTeam(Team team)
        {
            return GetPositionForTeam(team, Ball.X, Ball.Y);
        }

        // kiiruse seadmine pallile
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


        // liikuvad meeskonnad ja pall
        public void Move()
        {
        HomeTeam.Move();
        AwayTeam.Move();
        Ball.Move();
        }
    }
}