using System;
using System.Collections.Generic;

namespace ShitShooter
{
    class Game
    {
        public Player Player { get; private set; }
        public List<Target> Targets { get; private set; }
        public List<Bullet> Bullets { get; }

        public int Width { get; }
        public int Height { get; }

        public Game(int width, int height, List<Bullet> bullets)
        {
            Width = width;
            Height = height;
            Bullets = bullets;
        }

        public void StartGame(Player player, List<Target> targetsList)
        {
            Player = player;
            Targets = targetsList;
        }

        public bool ShouldGameEnd()
        {
            return Targets.Count == 0;
        }

        public void EndGame()
        {
            //здесь будет alert в форму что игра закончилась.
            throw new NotImplementedException();
        }

        public void HitTarget(Target target, Bullet bullet)
        {
            Bullets.Remove(bullet);
            target.Hp -= bullet.Damage;
            if (target.Hp <= 0)
                Targets.Remove(target);
        }
    }
}