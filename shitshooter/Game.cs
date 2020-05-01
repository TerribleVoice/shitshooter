using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace ShitShooter
{
    class Game
    {
        public Player Player { get; private set; }
        public List<Target> Targets { get; private set; }
        public List<Bullet> Bullets { get; }
        public ICreature[,] Map;


        public int Width => Map.GetLength(0);
        public int Height => Map.GetLength(1);

        public Game(int width, int height, List<Bullet> bullets)
        {
            Map = new ICreature[width, height];
            Bullets = bullets;
        }

        public void StartGame(Player player, List<Target> targetsList)
        {
            if(!PointBelongsMap(player.Position))
                throw new ArgumentException("Player is outside the map");
            Map[player.Position.X, player.Position.Y] = player;

            foreach (var target in targetsList)
            {
                if(!PointBelongsMap(target.Position))
                    throw new ArgumentException($"target ({target.Position.X},{target.Position.Y}) is outside the map");
                Map[target.Position.X, target.Position.Y] = target;
            }

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

        private bool PointBelongsMap(Point point)
        {
            return point.X >= 0 && point.Y >= 0 && point.X < Width && point.Y < Height;
        }
    }
}