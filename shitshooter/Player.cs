using System.Drawing;

namespace ShitShooter
{
    public class Player : ICreature
    {
        public Point Position { get; private set; }
        private int bulletDmg;
        private int bulletSpeed;
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
            game.Map[Position.X, Position.Y] = null;

            if (Position.X > 0) 
                Position = new Point(Position.X - 1, Position.Y);

            game.Map[Position.X, Position.Y] = this;
        }
        public void MoveRight()
        {
            game.Map[Position.X, Position.Y] = null;

            if (Position.X < game.Width - 1)
                Position = new Point(Position.X + 1, Position.Y);
         
            game.Map[Position.X, Position.Y] = this;
        }

        public void Shoot()
        {
            var bullet = new Bullet(bulletDmg, bulletSpeed, new Point(Position.X, Position.Y), game);
            game.Bullets.Add(bullet);

            //if (game.Map[bullet.Position.X, bullet.Position.Y] is Target)
            //{
            //    var target = (Target) game.Map[bullet.Position.X, bullet.Position.Y];
            //    game.HitTarget(target, bullet);
            //}
            //else
            //    game.Map[bullet.Position.X, bullet.Position.Y] = bullet;
        }

        public string GetImageFileName()
        {
            throw new System.NotImplementedException();
        }
    }
}