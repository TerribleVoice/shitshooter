using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace ShitShooter
{
    public class Game
    {
        public Player Player { get; private set; }
        public HashSet<Target> Targets { get; private set; }
        public HashSet<Bullet> Bullets { get; private set; }
        private HashSet<Bullet> bulletsToRemove;


        public int Width { get; }
        public int Height { get; }

        public Game(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void StartGame(Player player, HashSet<Target> targets)
        {
            if (!PointBelongsMap(player.Position))
                throw new ArgumentException("Player is outside the map");

            foreach (var target in targets)
            {
                if (!PointBelongsMap(target.Position))
                    throw new ArgumentException($"target ({target.Position.X},{target.Position.Y}) is outside the map");
            }

            Player = player;
            Targets = targets;
            Bullets = new HashSet<Bullet>();
            bulletsToRemove = new HashSet<Bullet>();

        }

        public bool ShouldGameEnd()
        {
            return Targets.Count == 0;
        }

        public event Action EndGame;

        public void Update()
        {
            if (ShouldGameEnd())
                EndGame?.Invoke();
            UpdateBullets();
            UpdateTargets();
            bulletsToRemove.Clear();
        }

        private void UpdateTargets()
        {
            var targetsToRemove = new HashSet<Target>();

            foreach (var target in Targets.Where(target => target.Hp <= 0))
                targetsToRemove.Add(target);

            foreach (var target in targetsToRemove)
                Targets.Remove(target);
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

        public void HitTarget(Target target, Bullet bullet)
        {
            bulletsToRemove.Add(bullet);
            target.Hp -= bullet.Damage;
        }

        private bool PointBelongsMap(Point point)
        {
            return point.X >= 0 && point.Y >= 0 && point.X < Width && point.Y < Height;
        }
    }
}