using System.Drawing;

namespace ShitShooter
{
    public class Bullet
    {
        public int Damage { get; }
        public Point Position { get; private set; }
        public readonly int speed;
        private readonly Game game;
        public Bullet(int damage,  int speed, Point position)
        {
            Position = position;
            this.speed = speed;
            Damage = damage;
        }

        public void Move()
        {
            Position = new Point(Position.X, Position.Y - speed);
        }
    }
}