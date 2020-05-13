using System;
using System.Drawing;
using System.Linq;

namespace ShitShooter
{
    public class Bullet : ICreature
    {
        public int Damage { get; }
        public Point Position { get; private set; }
        private readonly int speed;
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

            for (var y = startY; y >= Math.Max(0, endY); y--)
            {
                if (game.Targets.Any(target => target.Position == new Point(Position.X, y)))
                {
                    var targetToHit = game.Targets.First(target => target.Position == new Point(Position.X, y));
                    game.HitTarget(targetToHit, this);
                    return;
                }
            }
        }
    }
}