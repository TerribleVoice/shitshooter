using System;
using System.Drawing;

namespace ShitShooter
{
    public class Bullet : ICreature
    {
        public int Damage { get; }
        public Point Position { get; private set; }
        public readonly int speed;
        private readonly Game game;
        public Bullet(int damage, int speed, Point position, Game game)
        {
            Position = position;
            this.game = game;
            this.speed = speed;
            Damage = damage;
        }

        public void Move()
        {
            var startY = Position.Y;
            Position = new Point(Position.X, Position.Y - speed);
            var endY = Position.Y;

            for (int y = startY; y >= Math.Max(0, endY); y--)
            {
                if (game.Map[Position.X, y] is Target)
                {
                    var target = (Target) game.Map[Position.X, y];
                    game.HitTarget(target, this);
                    return;
                }
            }

            
        }

        public string GetImageFileName()
        {
            throw new System.NotImplementedException();
        }
    }
}