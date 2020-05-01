using System.Drawing;

namespace ShitShooter
{
    class Player : ICreature
    {
        public Point Position { get; private set; }
        private int bulletDmg;
        private int bulletSpeed;
        private readonly Game game;

        public Player(Point position, Game game)
        {
            this.game = game;
            Position = position;
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
            game.Bullets.Add(new Bullet(bulletDmg, bulletSpeed, new Point(Position.X + 1, Position.Y + 1)));
        }

        public string GetImageFileName()
        {
            throw new System.NotImplementedException();
        }
    }
}