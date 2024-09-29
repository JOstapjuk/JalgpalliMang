using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JalgpalliMang
{
    public class Ball
    {
        // objects
        public double X { get; private set; }
        public double Y { get; private set; }

        private double _vx, _vy;

        private Game _game;

        // construct
        public Ball(double x, double y, Game game)
        {
            _game = game;
            X = x;
            Y = y;
        }

        // palli kiiruse seadistamine
        public void SetSpeed(double vx, double vy)
        {
            // Määrake kiirus esialgu (pall on löödud)
            _vx = vx;
            _vy = vy;
        }

        // palli liigutamine 
        public void Move()
        {
            double newX = X + _vx;
            double newY = Y + _vy;
            if (_game.Stadium.IsIn(newX, newY))
            {
                X = newX;
                Y = newY;
            }
            else
            {
                _vx = 0;
                _vy = 0;
            }
        }
    }
}