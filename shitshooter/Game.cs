using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace ShitShooter
{
    public class Game
    {
        public Player Player { get; private set; }
        public List<Target> Targets { get; private set; }
        public List<Bullet> Bullets { get; private set; }
        private HashSet<Bullet> bulletsToRemove;
        public ICreature[,] Map;


        public int Width => Map.GetLength(0);
        public int Height => Map.GetLength(1);

        public Game(int width, int height)
        {
            Map = new ICreature[width, height];
        }

        public void StartGame(Player player, List<Target> targetsList)
        {
            if (!PointBelongsMap(player.Position))
                throw new ArgumentException("Player is outside the map");
            Map[player.Position.X, player.Position.Y] = player;

            foreach (var target in targetsList)
            {
                if (!PointBelongsMap(target.Position))
                    throw new ArgumentException($"target ({target.Position.X},{target.Position.Y}) is outside the map");
                Map[target.Position.X, target.Position.Y] = target;
            }

            Player = player;
            Targets = targetsList;
            Bullets = new List<Bullet>();
            bulletsToRemove = new HashSet<Bullet>();
            
        }

        public bool ShouldGameEnd()
        {
            return Targets.Count == 0;
        }

        public event Action EndGame;

        public void Update()
        {
            //if (ShouldGameEnd())
           //     EndGame();
            UpdateBullets();
            UpdateTargets();
            UpdateMap();
            bulletsToRemove.Clear();
        }

        private void UpdateTargets()
        {
            var targetsToRemove = new HashSet<Target>();

            foreach (var target in Targets)
                if (target.Hp <= 0)
                    targetsToRemove.Add(target);

            foreach (var target in targetsToRemove)
            {
                Targets.Remove(target);
            }
        }
        private void UpdateBullets()
        {
            foreach (var bullet in Bullets)
            {
                bullet.Move();
                if (!PointBelongsMap(bullet.Position))
                    bulletsToRemove.Add(bullet);
            }

            foreach (var bullet in bulletsToRemove)
                Bullets.Remove(bullet);
        }

        public void UpdateMap()
        {
            var newMap = new ICreature[Width,Height];
            newMap[Player.Position.X, Player.Position.Y] = Player;
         
            foreach (var bullet in Bullets)
                newMap[bullet.Position.X, bullet.Position.Y] = bullet;

            foreach (var target in Targets)
                newMap[target.Position.X, target.Position.Y] = target;

            Map = newMap;
        }

        public void HitTarget(Target target, Bullet bullet)
        {
            bulletsToRemove.Add(bullet);
            target.Hp -= bullet.Damage;
        }

        private bool PointBelongsMap(Point point)
        {
            return point.X >= 0 && point.Y >= 0 && point.X < Width && point.Y < Height;
        }

        public ICreature this[int x, int y] => Map[x, y];
    }
}