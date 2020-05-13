using System.Drawing;

namespace ShitShooter
{
    public class Player : ICreature
    {
        public Point Position { get; private set; }
        private readonly int bulletDmg;
        private readonly int bulletSpeed;
        private readonly Game game;

        public Player(Point position, Game game)
        {
            this.game = game;
            Position = position;
            bulletSpeed = 1;
            bulletDmg = 1;
        }

        public void MoveLeft()
        {
            if (Position.X > 0) 
                Position = new Point(Position.X - 1, Position.Y);
        }
        public void MoveRight()
        {
            if (Position.X < game.Width - 1)
                Position = new Point(Position.X + 1, Position.Y);
        }

        public void Shoot()
        {
            var bullet = new Bullet(bulletDmg, bulletSpeed, new Point(Position.X, Position.Y), game);
            game.Bullets.Add(bullet);
        }
    }
}